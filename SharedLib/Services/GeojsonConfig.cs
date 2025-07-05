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
        /// Input path for district court GeoJSON file.
        /// </summary>
        private static string GeoDCInputPath = "../../../../PrepareData/sources/dc_boundaries.geojson";

        /// <summary>
        /// Output path for processed district court GeoJSON file.
        /// </summary>
        private static string GeoDCOutputPath = "../../../../DisplayMaps/sources/dc_usable.geojson";

        /// <summary>
        /// Input path for circuit court GeoJSON file.
        /// </summary>
        private static string GeoCCInputPath = "../../../../PrepareData/sources/cc_boundaries.geojson";

        /// <summary>
        /// Output path for processed circuit court GeoJSON file.
        /// </summary>
        private static string GeoCCOutputPath = "../../../../DisplayMaps/sources/cc_usable.geojson";

        /// <summary>
        /// Processes the district court GeoJSON file, enriching it with data from a list of district courts.
        /// </summary>
        /// <param name="courts">List of district courts containing additional data to enrich the GeoJSON file.</param>
        public static void CreateUsableGeojsonDC(List<DistrictCourt> courts)
        {
            string geo1Content = File.ReadAllText(GeoDCInputPath);
            var geo1 = JsonConvert.DeserializeObject<GeoJson>(geo1Content);
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
                File.WriteAllText(GeoDCOutputPath, newGeoContent);
            }
        }

        /// <summary>
        /// Processes the circuit court GeoJSON file, enriching it with data from a list of circuit courts.
        /// </summary>
        /// <param name="courts">List of circuit courts containing additional data to enrich the GeoJSON file.</param>
        public static void CreateUsableGeojsonCC(List<CircuitCourt> courts)
        {
            string geo1Content = File.ReadAllText(GeoCCInputPath);
            var geo1 = JsonConvert.DeserializeObject<GeoJson>(geo1Content);
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
                File.WriteAllText(GeoCCOutputPath, newGeoContent);
            }
        }
    }
}
