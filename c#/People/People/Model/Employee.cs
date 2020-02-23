using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace People.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string[] Names { get; set; }

        public string Role { get; set; }

        public int SupervisorId { get; set; }
    }
}
