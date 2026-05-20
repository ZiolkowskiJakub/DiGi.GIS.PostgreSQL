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

            List<AdministrativeAreal2DReference>? administrativeAreal2DReferences = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencesByAdministrativeArealTypeAsync(Enums.AdministrativeArealType.Subdivison);
            if(administrativeAreal2DReferences is null || administrativeAreal2DReferences.Count == 0)
            {
                return false;
            }

            foreach(AdministrativeAreal2DReference administrativeAreal2DReference in administrativeAreal2DReferences)
            {
                AdministrativeAreal2DReferencePath? administrativeAreal2DReferencePath = await administrativeAreal2DPostgreSQLConverter.GetAdministrativeAreal2DReferencePathAsync(administrativeAreal2DReference);

                List<Building2DReference>? building2DReferences = await building2DPostgreSQLConverter.GetBuilding2DReferencesAsync(administrativeAreal2DReference.CountyId.Value);
                if(building2DReferences is null || building2DReferences.Count == 0)
                {
                    //TODO: Finish implementation
                    throw new System.NotImplementedException();
                }
            }

            


            throw new NotImplementedException();
        }
    }
}