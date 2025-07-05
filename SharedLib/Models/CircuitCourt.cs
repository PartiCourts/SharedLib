namespace PartiCourts.SharedLib.Models
{
    /// <summary>
    /// Represents a circuit court and its properties, including the number of judges, partisanship, and senior eligible judges.
    /// Implements the <see cref="ICourt"/> interface.
    /// </summary>
    public class CircuitCourt : ICourt
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CircuitCourt"/> class with default values.
        /// </summary>
        public CircuitCourt()
        {
            this.Name = string.Empty;
            this.SupervisingJustice = string.Empty;
            this.ChiefJudge = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircuitCourt"/> class with the specified values.
        /// </summary>
        /// <param name="id">The unique identifier for the court.</param>
        /// <param name="name">The name of the court.</param>
        /// <param name="supervisingJustice">The supervising justice for the court.</param>
        /// <param name="maxJudges">The maximum number of judges in the court.</param>
        public CircuitCourt(int id, string name, string supervisingJustice, int maxJudges)
        {
            this.Id = id;
            this.Name = name;
            this.ChiefJudge = string.Empty;
            this.ActiveJudges = 0;
            this.DEMJudges = 0;
            this.GOPJudges = 0;
            this.SeniorEligibleJudges = 0;
            this.SupervisingJustice = supervisingJustice;
            this.MaxJudges = maxJudges;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircuitCourt"/> class with all details about the court.
        /// </summary>
        /// <param name="id">The unique identifier for the court.</param>
        /// <param name="name">The name of the court.</param>
        /// <param name="supervisingJustice">The supervising justice for the court.</param>
        /// <param name="chiefJudge">The name of the chief judge.</param>
        /// <param name="activeJudges">The number of active judges in the court.</param>
        /// <param name="maxJudges">The maximum number of judges in the court.</param>
        /// <param name="seniorEligibleJudges">The number of senior-eligible judges.</param>
        /// <param name="dem">The number of Democratic judges.</param>
        /// <param name="gop">The number of Republican judges.</param>
        /// <param name="demRetiring">The number of Democratic judges that are retiring.</param>
        /// <param name="gopRetiring">The number of Republican judges that are retiring.</param>
        public CircuitCourt(int id, string name, string supervisingJustice, string chiefJudge, int activeJudges, int maxJudges, int seniorEligibleJudges, int dem, int gop, int demRetiring, int gopRetiring)
        {
            this.Id = id;
            this.Name = name;
            this.SupervisingJustice = supervisingJustice;
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
        /// Gets the name of the supervising Justice of this circuit court.
        /// </summary>
        public string SupervisingJustice { get; }

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
        /// Gets the numbers of vacancies.
        /// </summary>
        /// <returns>The number of vacancies.</returns>
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
            return this.GetCircuitCourtName().Replace(" ", "_");
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
        /// Gets the name of the circuit court based on its ID.
        /// </summary>
        /// <returns>The name of the circuit court corresponding to the ID.</returns>
        public string GetCircuitCourtName()
        {
            string[] courtNames =
            {
                "District of Columbia Circuit",
                "First Circuit",
                "Second Circuit",
                "Third Circuit",
                "Fourth Circuit",
                "Fifth Circuit",
                "Sixth Circuit",
                "Seventh Circuit",
                "Eighth Circuit",
                "Ninth Circuit",
                "Tenth Circuit",
                "Eleventh Circuit",
            };

            return courtNames[this.Id];
        }
    }
}