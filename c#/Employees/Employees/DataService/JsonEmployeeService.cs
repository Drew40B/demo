using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Employees.Interfaces;
using Employees.Model;
using Employees.Lib;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Employees.DataService

{
    public class JsonEmployeeService : IEmployeeService
    {

        const string EMPLOYEES_DIR = "employees";

        private Dictionary<int, Employee> _employees;

        public async Task Init()
        {
            await this.Load();
        }
        public async Task Load(DirectoryInfo employeesDir = null)
        {
            var dir = employeesDir ?? Utils.findRootCommon().GetDirectories().FirstOrDefault(d => d.Name == EMPLOYEES_DIR);

            if (!dir.Exists)
            {
                throw new Exception($"Unable to find {EMPLOYEES_DIR}");
            }

            _employees = new Dictionary<int, Employee>();

            var files = dir.GetFiles("*.json");

            foreach (FileInfo file in files)
            {
                using (FileStream fs = File.OpenRead(file.FullName))
                {
                    var employee = await deserializeEmployee(fs);
                    _employees.Add(employee.EmployeeId, employee);

                }
            }

        }

        public async Task<Employee> deserializeEmployee(Stream input)
        {
            var employee = await JsonSerializer.DeserializeAsync<Employee>(input);
            return employee;
        }


#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<Employee> FindById(int empoloyeeId)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return this._employees?.GetValueOrDefault(empoloyeeId);
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<Employee> FindRoot()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return _employees.FirstOrDefault(kvp => kvp.Value.SupervisorId == 0).Value;
        }

        public Task<Employee[]> List()
        {
            throw new NotImplementedException();
        }

        public Task<Employee> Create(Employee empoloyee)
        {
            throw new NotImplementedException();
        }

        public Task<(bool Exists, Employee Employee)> Update(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
