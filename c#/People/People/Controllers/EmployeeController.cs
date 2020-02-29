using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using People.Interfaces;
using People.Model;

namespace People.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeDatabase _employees;

        public EmployeeController(IEmployeeDatabase employeeDatabase)
        {
            _employees = employeeDatabase;
        }


        [HttpGet]
        public async Task<Employee> Get(int? employeeId = null)
        {
            if (employeeId is null)
            {
                return await _employees.findRoot();
            }

            return await _employees.findById((int) employeeId);
        }


    }
}