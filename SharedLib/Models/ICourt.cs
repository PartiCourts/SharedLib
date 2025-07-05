namespace PartiCourts.SharedLib.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface representing the structure of a court, defining required properties and methods for implementation.
    /// </summary>
    public interface ICourt
    {
        /// <summary>
        /// Gets or Sets unique ID of this court.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets name of this court.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or Sets the number of active judges of this court.
        /// </summary>
        int ActiveJudges { get; set; }

        /// <summary>
        /// Gets the number of judges of this court if there were no vacancies.
        /// </summary>
        int MaxJudges { get; }

        /// <summary>
        /// Gets or Sets the name of the Chief Judge of this court.
        /// </summary>
        string ChiefJudge { get; set; }

        /// <summary>
        /// Gets or Sets the number of active judges in this court that are eligible for senior status.
        /// </summary>
        int SeniorEligibleJudges { get; set; }

        /// <summary>
        /// Gets or Sets the number of judges appointed by a Democratic President.
        /// </summary>
        int DEMJudges { get; set; }

        /// <summary>
        /// Gets or Sets the number of judges appointed by a Republican President.
        /// </summary>
        int GOPJudges { get; set; }

        /// <summary>
        /// Gets or Sets the number of retiring judges that were appointed by a Democratic President.
        /// </summary>
        int DEMRetiring { get; set; }

        /// <summary>
        /// Gets or Sets the number of retiring judges that were appointed by a Republican President.
        /// </summary>
        int GOPRetiring { get; set; }

        /// <summary>
        /// Calculates and returns the number of vacant judge positions in the court.
        /// </summary>
        /// <returns>The number of vacancies in this court.</returns>
        int GetNumberOfVacancies();

        /// <summary>
        /// Sets the partisanship (Democratic and Republican) of the court based on the given list of judges.
        /// </summary>
        /// <param name="judges">A list of judges to determine partisanship.</param>
        void SetPartisanshipOfCourt(List<Judge> judges);

        /// <summary>
        /// Determines the overall partisanship of the court based on the number of Democratic and Republican judges.
        /// </summary>
        /// <returns>
        /// 1 if the court is Democratic, -1 if Republican, or 0 if evenly split.
        /// </returns>
        int FindPartisanshipOfCourt();

        /// <summary>
        /// Finds and sets the chief judge from a list of judges.
        /// </summary>
        /// <param name="judges">A list of judges from which to identify the chief judge.</param>
        void SetChiefJudge(List<Judge> judges);

        /// <summary>
        /// Finds and sets the number of senior-eligible judges from the given list of judges.
        /// </summary>
        /// <param name="judges">A list of judges to check for senior eligibility.</param>
        void SetNumOfSeniorEligibles(List<Judge> judges);

        /// <summary>
        /// Returns the name of the court with all spaces replaced by underscores.
        /// </summary>
        /// <returns>The name of the court with no white spaces.</returns>
        string GetNoWhiteSpace();

        /// <summary>
        /// Adds the retiring judge to the corresponding party retirement count.
        /// </summary>
        /// <param name="judges">List of judges from this court.</param>
        void AddToRetiring(List<Judge> judges);
    }
}
