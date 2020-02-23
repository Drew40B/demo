using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using People.Model;

namespace People.Interfaces
{
    interface IEmployeeDatabase
    {
         Task<Employee> findById(int empoloyeeId);

        Task Init();
    }
}
