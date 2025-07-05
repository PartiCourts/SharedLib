namespace PartiCourts.SharedLib.Models
{
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Represents a district court and its properties, including the number of judges, partisanship, and senior eligible judges.
    /// Implements the <see cref="ICourt"/> interface.
    /// </summary>
    public class DistrictCourt : ICourt
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DistrictCourt"/> class.
        /// </summary>
        public DistrictCourt()
        {
            this.Name = string.Empty;
            this.Abbreviation = string.Empty;
            this.ChiefJudge = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DistrictCourt"/> class with specified parameters.
        /// </summary>
        /// <param name="name">The name of the district court.</param>
        /// <param name="abbreviation">The abbreviation of the district court.</param>
        /// <param name="courtOfAppeal">The court of appeal the district court is part of.</param>
        /// <param name="maxJudges">The maximum number of judges allowed in the district court.</param>
        /// <param name="chiefJudge">The name of the chief judge of the district court.</param>
        public DistrictCourt(string name, string abbreviation, string courtOfAppeal, int maxJudges, string chiefJudge)
        {
            this.Name = name;
            this.Abbreviation = abbreviation;
            this.MaxJudges = maxJudges;
            this.ChiefJudge = chiefJudge;
            this.ActiveJudges = 0;
            this.MakeAppeal(courtOfAppeal);
            this.DEMRetiring = 0;
            this.GOPRetiring = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DistrictCourt"/> class with specified parameters.
        /// </summary>
        /// <param name="id">The unique identifier for the district court.</param>
        /// <param name="name">The name of the district court.</param>
        /// <param name="abbreviation">The abbreviation of the district court.</param>
        /// <param name="courtOfAppeal">The court of appeal the district court is part of.</param>
        /// <param name="activeJudges">The number of active judges in the district court.</param>
        /// <param name="maxJudges">The maximum number of judges allowed in the district court.</param>
        /// <param name="chiefJudge">The name of the chief judge of the district court.</param>
        /// <param name="seniorEligibleJudges">The number of senior eligible judges in the district court.</param>
        /// <param name="dem">The number of Democratic judges in the district court.</param>
        /// <param name="gop">The number of Republican judges in the district court.</param>
        /// <param name="demRetiring">The number of Democratic judges that are retiring in the district court.</param>
        /// <param name="gopRetiring">The number of Republican judges that are retiring in the district court.</param>
        public DistrictCourt(
            int id,
            string name,
            string abbreviation,
            int courtOfAppeal,
            int activeJudges,
            int maxJudges,
            string chiefJudge,
            int seniorEligibleJudges,
            int dem,
            int gop,
            int demRetiring,
            int gopRetiring)
        {
            this.Id = id;
            this.Name = name;
            this.Abbreviation = abbreviation;
            this.CourtOfAppeal = courtOfAppeal;
            this.ActiveJudges = activeJudges;
            this.MaxJudges = maxJudges;
            this.ChiefJudge = chiefJudge;
            this.SeniorEligibleJudges = seniorEligibleJudges;
            this.DEMJudges = dem;
            this.GOPJudges = gop;
            this.DEMRetiring = demRetiring;
            this.GOPRetiring = gopRetiring;
        }

        /// <inheritdoc/>
        public int Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; }

        /// <summary>
        /// Gets the abbreviation of this court.
        /// </summary>
        public string Abbreviation { get; }

        /// <summary>
        /// Gets the id of the circuit court this district court is under.
        /// </summary>
        public int CourtOfAppeal { get; private set; }

        /// <inheritdoc/>
        public int ActiveJudges { get; set; }

        /// <inheritdoc/>
        public int MaxJudges { get; }

        /// <inheritdoc/>
        public string ChiefJudge { get; set; }

        /// <inheritdoc/>
        public int SeniorEligibleJudges { get; set; }

        /// <inheritdoc/>
        public int DEMJudges { get; set; }

        /// <inheritdoc/>
        public int GOPJudges { get; set; }

        /// <inheritdoc/>
        public int DEMRetiring { get; set; }

        /// <inheritdoc/>
        public int GOPRetiring { get; set; }

        /// <summary>
        /// Sets the court of appeal based on the given appeal value.
        /// </summary>
        /// <param name="appeal">The court of appeal information as a string.</param>
        public void MakeAppeal(string appeal)
        {
            if (appeal == "D.C.")
            {
                this.CourtOfAppeal = 0;
                return;
            }

            string digits = appeal.Substring(0, appeal.Length - 2); // 1st, 3rd, 10th, ...
            this.CourtOfAppeal = int.Parse(digits);
        }

        /// <inheritdoc/>
        public int GetNumberOfVacancies() => this.MaxJudges - this.ActiveJudges;

        /// <inheritdoc/>
        public void SetPartisanshipOfCourt(List<Judge> judges)
        {
            this.DEMJudges = judges.Count(judge => judge.Partisanship == 1);
            this.GOPJudges = this.ActiveJudges - this.DEMJudges;
        }

        /// <inheritdoc/>
        public int FindPartisanshipOfCourt()
        {
            if (this.DEMJudges > this.GOPJudges)
            {
                return 1;
            }
            else if (this.DEMJudges < this.GOPJudges)
            {
                return -1;
            }

            return 0;
        }

        /// <inheritdoc/>
        public void SetChiefJudge(List<Judge> judges)
        {
            Judge? temp = judges.FirstOrDefault(judge => judge.IsChief);

            if (temp != null)
            {
                this.ChiefJudge = temp.Name;
            }
            else
            {
                this.ChiefJudge = string.Empty;
            }
        }

        /// <inheritdoc/>
        public void SetNumOfSeniorEligibles(List<Judge> judges)
        {
            this.SeniorEligibleJudges = judges.Count(judge => judge.IsEligibleSeniorStatus());
        }

        /// <inheritdoc/>
        public string GetNoWhiteSpace()
        {
            return this.Name.Replace(" ", "_");
        }

        /// <inheritdoc/>
        public void AddToRetiring(List<Judge> judges)
        {
            this.DEMRetiring = 0;
            this.GOPRetiring = 0;
            foreach (Judge judge in judges)
            {
                if (judge.IsRetiring)
                {
                    if (judge.Partisanship == 1)
                    {
                        this.DEMRetiring += 1;
                    }
                    else
                    {
                        this.GOPRetiring += 1;
                    }
                }
            }
        }

        /// <summary>
        /// Sets the ID of the district court from a GeoJSON file.
        /// </summary>
        /// <param name="path">The file path of the GeoJSON data.</param>
        /// <exception cref="Exception">Thrown if the ID cannot be generated from the GeoJSON data.</exception>
        public void SetIdFromGeoJson(string path)
        {
            string geoJsonContent = File.ReadAllText(path);
            JObject geoJson = JObject.Parse(geoJsonContent);
            if (geoJson["type"]?.ToString() == "FeatureCollection")
            {
                if (geoJson["features"] != null)
                {
                    foreach (var feature in geoJson["features"] !)
                    {
                        var properties = feature["properties"];
                        if (properties != null && properties["NAME"] != null && properties["NAME"] !.ToString() == this.Name)
                        {
                            if (properties["FID"] != null && properties["FID"] !.ToString() != null)
                            {
                                this.Id = int.Parse(properties["FID"] !.ToString());
                                return;
                            }
                        }
                    }
                }
            }

            throw new Exception($"Couldn't generate ID of {this.Name}");
        }
    }
}
