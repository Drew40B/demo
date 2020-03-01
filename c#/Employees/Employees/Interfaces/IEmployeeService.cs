using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employees.Model;

namespace Employees.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee[]> List();

        Task<Employee> FindById(int empoloyeeId);

        Task<Employee> FindRoot();

        Task<Employee> Create(Employee empoloyee);

        Task<(bool Exists,Employee Employee)> Update(Employee employee);

        Task<bool> Delete(int employeeId);

        Task Init();
    }
}
