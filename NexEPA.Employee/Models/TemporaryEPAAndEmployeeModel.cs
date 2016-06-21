namespace NexEPA.Employee.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The temporary model contain employees and EPA information.
    /// </summary>
    public class TemporaryEPAAndEmployeeModel
    {
        public IEnumerable<EmployeeModel> Employees { get; set; }

        public IEnumerable<TemporaryEPAModel> RawEPAs { get; set; }
    }
}
