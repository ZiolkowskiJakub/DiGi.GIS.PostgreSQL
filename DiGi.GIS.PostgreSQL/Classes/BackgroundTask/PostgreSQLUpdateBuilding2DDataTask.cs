using DiGi.Core.Classes;
using DiGi.GIS.PostgreSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.GIS.PostgreSQL.Classes
{
    public class PostgreSQLUpdateBuilding2DDataTask : ReportableBackgroundTask<long>, IGISPostgreSQLObject
    {
        private readonly GISPostgreSQLConverterManager gISPostgreSQLConverterManager;

        /// <summary>
        /// Constructor with Dependency Injection.
        /// </summary>
        public PostgreSQLUpdateBuilding2DDataTask(GISPostgreSQLConverterManager gISPostgreSQLConverterManager)
        {
            this.gISPostgreSQLConverterManager = gISPostgreSQLConverterManager ?? throw new ArgumentNullException(nameof(gISPostgreSQLConverterManager));
        }

        /// <summary>
        /// Concrete implementation of the background work.
        /// </summary>
        protected override async Task<bool> ExecuteAsync(IProgress<long> progress, CancellationToken cancellationToken)
        {
            Building2DTablePostgreSQLConverter? building2DTablePostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<Building2DTablePostgreSQLConverter>();
            if(building2DTablePostgreSQLConverter is null)
            {
                return false;
            }

            Building2DPostgreSQLConverter? building2DPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<Building2DPostgreSQLConverter>();
            if (building2DPostgreSQLConverter is null)
            {
                return false;
            }

            AdministrativeAreal2DPostgreSQLConverter? administrativeAreal2DPostgreSQLConverter = gISPostgreSQLConverterManager.GetPostgreSQLConverter<AdministrativeAreal2DPostgreSQLConverter>();
            if (administrativeAreal2DPostgreSQLConverter is null)
            {
                return false;
            }

            List<AdministrativeAreal2DReference>? administrativeAreal2DReferences = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(Enums.AdministrativeArealType.Subdivison, cancellationToken: cancellationToken);
            if(administrativeAreal2DReferences is null || administrativeAreal2DReferences.Count == 0)
            {
                return false;
            }

            foreach(AdministrativeAreal2DReference administrativeAreal2DReference in administrativeAreal2DReferences)
            {
                if (administrativeAreal2DReference.CountyId is not int countyId)
                {
                    continue;
                }

                AdministrativeAreal2DReferencePath? administrativeAreal2DReferencePath = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathAsync(administrativeAreal2DReference);

                AdministrativeAreal2DReference? AdministrativeAreal2DReference_Subdivision = administrativeAreal2DReferencePath?[Enums.AdministrativeArealType.Subdivison];

                List<AdministrativeAreal2D>? administrativeAreal2Ds = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DsByIdsAsync(administrativeAreal2DReferencePath?.AdministrativeAreal2DReferences?.ConvertAll(x => x.Id));

                List<Building2DReference>? building2DReferences = await building2DPostgreSQLConverter.GetBuilding2DReferencesAsync(administrativeAreal2DReference.CountyId.Value, AdministrativeAreal2DReference_Subdivision?.Id, cancellationToken: cancellationToken);
                if (building2DReferences is null || building2DReferences.Count == 0)
                {
                    continue;
                }

                List<Building2D>? building2Ds = await building2DPostgreSQLConverter.GetBuilding2DsByBuilding2DReferences(building2DReferences);
                if(building2Ds is null || building2Ds.Count == 0)
                {
                    continue;
                }

                Core.IO.Table.Classes.Table table = new();

                IO.Modify.Update(table, countyId, AdministrativeAreal2DReference_Subdivision?.Id, building2Ds?.ConvertAll(x => x.ToDiGi()!), administrativeAreal2Ds: administrativeAreal2Ds?.ConvertAll(x => x.ToDiGi()!));


            }

            return false;
        }
    }
}