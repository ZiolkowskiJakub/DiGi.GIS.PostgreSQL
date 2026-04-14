using DiGi.GIS.PostgreSQL.Classes;
using System.Collections.Generic;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Create
    {
        public static List<GIS.Classes.YearBuiltData>? YearBuiltDatas(this IEnumerable<YearBuilt>? yearBuilts)
        {
            if (yearBuilts is null)
            {
                return null;
            }

            Dictionary<string, List<GIS.Interfaces.IYearBuilt>> dictionary = [];
            foreach (YearBuilt yearBuilt in yearBuilts)
            {
                if (yearBuilt?.Reference is not string reference)
                {
                    continue;
                }

                if (yearBuilt.ToDiGi() is not GIS.Interfaces.IYearBuilt yearBuilt_GIS)
                {
                    continue;
                }

                if (!dictionary.TryGetValue(reference, out List<GIS.Interfaces.IYearBuilt>? yearBuilts_Reference) || yearBuilts_Reference is null)
                {
                    yearBuilts_Reference = [];
                    dictionary[reference] = yearBuilts_Reference;
                }

                yearBuilts_Reference.Add(yearBuilt_GIS);
            }

            List<GIS.Classes.YearBuiltData> result = [];
            foreach (KeyValuePair<string, List<GIS.Interfaces.IYearBuilt>> keyValuePair in dictionary)
            {
                GIS.Classes.YearBuiltData yearBuiltData = new(keyValuePair.Key);
                foreach (GIS.Interfaces.IYearBuilt yearBuilt in keyValuePair.Value)
                {
                    yearBuiltData.Add(yearBuilt);
                }

                result.Add(yearBuiltData);
            }

            return result;
        }
    }
}