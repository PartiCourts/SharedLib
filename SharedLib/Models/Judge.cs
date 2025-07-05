namespace PartiCourts.SharedLib.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a judge and their details, including their partisanship, eligibility for senior status, and years of service.
    /// </summary>
    public class Judge
    {
        private const int SENIORINT = 80; // See Rule of 80
        private const int RETIREMENTAGE = 65;
        private readonly List<string> GOPLIST = new List<string> { "Reagan", "G.H.W. Bush", "G.W. Bush", "Trump" };
        private readonly List<string> DEMLIST = new List<string> { "Clinton", "Obama", "Biden" };

        /// <summary>
        /// Initializes a new instance of the <see cref="Judge"/> class with default values.
        /// </summary>
        public Judge()
        {
            this.Name = string.Empty;
            this.Title = string.Empty;
            this.AppointedBy = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Judge"/> class with specified properties.
        /// </summary>
        /// <param name="name">The name of the judge.</param>
        /// <param name="isCircuit">Indicates whether the judge is a circuit judge.</param>
        /// <param name="court">The court the judge belongs to.</param>
        /// <param name="yearOfBirth">The birth year of the judge.</param>
        /// <param name="title">The title of the judge.</param>
        /// <param name="appointedBy">The president who appointed the judge.</param>
        /// <param name="appointmentYear">The year the judge was appointed.</param>
        /// <param name="isChief">Indicates whether the judge is the chief judge.</param>
        /// <param name="isRetiring">Indicates whether the judge is retiring.</param>
        public Judge(string name, bool isCircuit, int court, int yearOfBirth, string title, string appointedBy, int appointmentYear, bool isChief, bool isRetiring)
        {
            this.Name = name;
            this.IsCircuitJudge = isCircuit;
            this.Court = court;
            this.Title = title;
            this.AppointedBy = appointedBy;
            this.YearOfBirth = yearOfBirth;
            this.AppointmentYear = appointmentYear;
            this.IsChief = isChief;
            this.SetPartisanship();
            this.IsRetiring = isRetiring;
        }

        /// <summary>
        /// Gets or Sets the name of this judge.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether this judge is a judge on a circuit court.
        /// </summary>
        public bool IsCircuitJudge { get; set; }

        /// <summary>
        /// Gets or Sets the ID of the court the judge was appointed in.
        /// </summary>
        public int Court { get; set; }

        /// <summary>
        /// Gets or Sets the offical title of this judge (Chief Judge, District Judge, Circuit Judge, ...).
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets the name of the President that appointed this judge.
        /// </summary>
        public string AppointedBy { get; set; }

        /// <summary>
        /// Gets or Sets the birth year of this judge.
        /// </summary>
        public int YearOfBirth { get; set; }

        /// <summary>
        /// Gets or Sets the year this judge was appointed.
        /// </summary>
        public int AppointmentYear { get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether this judge is the chief judge of their court.
        /// </summary>
        public bool IsChief { get; set; }

        /// <summary>
        /// Gets or Sets the partisanship of this judge. 1 if appointed by a Democratic President, -1 if appointed by a Republican President.
        /// </summary>
        public int Partisanship { get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether this judge is retiring.
        /// </summary>
        public bool IsRetiring { get; set; }

        /// <summary>
        /// Gets the current age of the judge.
        /// </summary>
        /// <returns>The judge's current age.</returns>
        public int GetAge()
        {
            return DateTime.Now.Year - this.YearOfBirth;
        }

        /// <summary>
        /// Gets the number of years the judge has served since their appointment.
        /// </summary>
        /// <returns>The number of years the judge has served.</returns>
        public int GetYearsOfService()
        {
            return DateTime.Now.Year - this.AppointmentYear;
        }

        /// <summary>
        /// Determines whether the judge is eligible for senior status based on the Rule of 80 and the retirement age.
        /// </summary>
        /// <returns>True if the judge is eligible for senior status; otherwise, false.</returns>
        public bool IsEligibleSeniorStatus()
        {
            if (this.GetAge() >= RETIREMENTAGE)
            {
                if (this.GetAge() + this.GetYearsOfService() >= SENIORINT)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Calculates when the judge will be eligible for senior status based on their age and years of service.
        /// </summary>
        /// <returns>The number of years until the judge is eligible for senior status.</returns>
        public int YearsForEligibilityForSeniorStatus()
        {
            int ruleOf80 = this.GetAge() + this.GetYearsOfService() >= SENIORINT ? 0 : SENIORINT - (this.GetAge() + this.GetYearsOfService());
            int addedYears = this.GetAge() >= RETIREMENTAGE ? 0 : RETIREMENTAGE - this.GetAge();

            return ruleOf80 + addedYears;
        }

        /// <summary>
        /// Determines the judge's partisanship based on the president who appointed them.
        /// </summary>
        /// <exception cref="Exception">Thrown when the president who appointed the judge is not recognized.</exception>
        public void SetPartisanship()
        {
            // Handles the case where two different presidents appointed the judge
            if (this.AppointedBy.Contains('/'))
            {
                this.AppointedBy = this.AppointedBy.Split('/')[0].Trim();
            }

            if (this.DEMLIST.Contains(this.AppointedBy))
            {
                this.Partisanship = 1;
            }
            else if (this.GOPLIST.Contains(this.AppointedBy))
            {
                this.Partisanship = -1;
            }
            else
            {
                throw new Exception($"Judge was appointed by a president not accounted for. " +
                    $"{this.Name} appointed by {this.AppointedBy} in court #{this.Court}");
            }
        }
    }
}
