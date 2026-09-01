using DiGi.BDL.Enums;
using DiGi.GIS.Classes;
using System.Collections.Generic;

namespace DiGi.GIS.PostgreSQL
{
    public static partial class Query
    {
        /// <summary>
        /// Retrieves the demographic population yearly data series from the statistical data collection, scaling BDL values to individual counts if applicable.
        /// </summary>
        /// <param name="statisticalDataCollection">The statistical data collection containing demographic information.</param>
        /// <returns>A <see cref="StatisticalYearlyDoubleData"/> containing normalized yearly population counts, or null if no population series is found.</returns>
        public static StatisticalYearlyDoubleData? Population(this StatisticalDataCollection? statisticalDataCollection)
        {
            if (statisticalDataCollection is null)
            {
                return null;
            }

            StatisticalYearlyDoubleData? bdlData = GIS.Query.StatisticalData<StatisticalYearlyDoubleData>(statisticalDataCollection, Variable.population_thousand_persons);
            if (bdlData is not null)
            {
                List<KeyValuePair<short, double>> values = [];
                foreach (short year in bdlData.Years)
                {
                    if (bdlData.TryGetValue(year, out double val))
                    {
                        values.Add(new KeyValuePair<short, double>(year, val * 1000.0));
                    }
                }

                return new StatisticalYearlyDoubleData("Population", "Population", values);
            }

            return statisticalDataCollection.GetStatisticalData("Population") as StatisticalYearlyDoubleData;
        }
    }
}
