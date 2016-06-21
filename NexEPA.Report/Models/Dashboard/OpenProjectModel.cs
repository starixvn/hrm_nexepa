namespace NexEPA.Report.Models.Dashboard
{
    using NexEPA.Employee.Models;
    using NexEPA.Project.Models;

    /// <summary>
    /// Open Project Model.
    /// </summary>
    public class OpenProjectModel
    {
        public ProjectModel Project { get; set; }

        public EmployeeModel Evaluator { get; set; }

        public string Roles { get; set; }
    }
}
