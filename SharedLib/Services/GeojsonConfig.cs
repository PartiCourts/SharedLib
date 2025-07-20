namespace PartiCourts.SharedLib.Services
{
    using Newtonsoft.Json;
    using PartiCourts.SharedLib.Models;

    /// <summary>
    /// Static class for managing GeoJSON configuration and processing.
    /// </summary>
    public static class GeojsonConfig
    {
        /// <summary>
        /// Processes the district court GeoJSON file, enriching it with data from a list of district courts.
        /// </summary>
        /// <param name="courts">List of district courts containing additional data to enrich the GeoJSON file.</param>
        /// <param name="inputFile">Contents of the geojson file that contains district boundaries.</param>
        /// <returns>Content of viewable geojson file.</returns>
        public static string CreateUsableGeojsonDC(List<DistrictCourt> courts, string inputFile)
        {
            var geo1 = JsonConvert.DeserializeObject<GeoJson>(inputFile);
            if (geo1 != null)
            {
                // Create a new GeoJSON object to store enriched features
                GeoJson geo2 = new GeoJson(geo1.Type, "dcourts", geo1.Crs, new List<Feature>());

                foreach (Feature feature in geo1.Features)
                {
                    long fid = (long)feature.Properties["FID"];
                    DistrictCourt? currentCourt = courts.Find(court => court.Id == fid);
                    if (currentCourt != null)
                    {
                        // Enrich feature properties with court data
                        Dictionary<string, object> newProperties = new Dictionary<string, object>
                    {
                        { "FID", fid },
                        { "NAME", (string)feature.Properties["NAME"] },
                        { "CHIEF_JUDGE", currentCourt.ChiefJudge },
                        { "ACTIVE_JUDGES", currentCourt.ActiveJudges },
                        { "SENIOR_ELIGIBLE_JUDGES", currentCourt.SeniorEligibleJudges },
                        { "VACANCIES", currentCourt.GetNumberOfVacancies() },
                        { "DEMJUDGES", currentCourt.DEMJudges },
                        { "GOPJUDGES", currentCourt.GOPJudges },
                        { "PARTISANSHIP", currentCourt.FindPartisanshipOfCourt() },
                        { "DEMRETIRING", currentCourt.DEMRetiring },
                        { "GOPRETIRING", currentCourt.GOPRetiring },
                    };
                        Feature newFeature = new Feature(feature.Type, newProperties, feature.Geometry);
                        geo2.Features.Add(newFeature);
                    }
                }

                string newGeoContent = JsonConvert.SerializeObject(geo2, Formatting.Indented);
                return newGeoContent;
            }

            return string.Empty;
        }

        /// <summary>
        /// Processes the circuit court GeoJSON file, enriching it with data from a list of circuit courts.
        /// </summary>
        /// <param name="courts">List of circuit courts containing additional data to enrich the GeoJSON file.</param>
        /// <param name="inputFile">Contents of the geojson file that contains district boundaries.</param>
        /// <returns>Content of viewable geojson file.</returns>
        public static string CreateUsableGeojsonCC(List<CircuitCourt> courts, string inputFile)
        {
            var geo1 = JsonConvert.DeserializeObject<GeoJson>(inputFile);
            if (geo1 != null)
            {
                // Create a new GeoJSON object to store enriched features
                GeoJson geo2 = new GeoJson(geo1.Type, "ccourts", geo1.Crs, new List<Feature>());

                foreach (Feature feature in geo1.Features)
                {
                    string name = (string)feature.Properties["NAME"];
                    CircuitCourt? currentCourt = courts.Find(court => string.Compare(court.GetCircuitCourtName(), name, StringComparison.OrdinalIgnoreCase) == 0);
                    if (currentCourt != null)
                    {
                        // Enrich feature properties with court data
                        Dictionary<string, object> newProperties = new Dictionary<string, object>
                    {
                        { "NAME", name },
                        { "SUPERVISING_JUSTICE", currentCourt.SupervisingJustice },
                        { "CHIEF_JUDGE", currentCourt.ChiefJudge },
                        { "ACTIVE_JUDGES", currentCourt.ActiveJudges },
                        { "SENIOR_ELIGIBLE_JUDGES", currentCourt.SeniorEligibleJudges },
                        { "VACANCIES", currentCourt.GetNumberOfVacancies() },
                        { "DEMJUDGES", currentCourt.DEMJudges },
                        { "GOPJUDGES", currentCourt.GOPJudges },
                        { "PARTISANSHIP", currentCourt.FindPartisanshipOfCourt() },
                        { "DEMRETIRING", currentCourt.DEMRetiring },
                        { "GOPRETIRING", currentCourt.GOPRetiring },
                    };
                        Feature newFeature = new Feature(feature.Type, newProperties, feature.Geometry);
                        geo2.Features.Add(newFeature);
                    }
                }

                string newGeoContent = JsonConvert.SerializeObject(geo2, Formatting.Indented);
                return newGeoContent;
            }

            return string.Empty;
        }
    }
}
