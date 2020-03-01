using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Employees.Interfaces;
using Employees.Model;

namespace Employees.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employees;

        public EmployeeController(IEmployeeService employeeDatabase)
        {
            _employees = employeeDatabase;
        }

        [HttpGet]
        [Route("")]
        public async Task<Employee[]> List()
        {
            return await _employees.List();
        }
        [HttpGet]
        [Route("{employeeId}")]
        public async Task<IActionResult> Get(int employeeId)
        {

            var found = await _employees.FindById((int)employeeId);

            if (found == null)
            {
                return NotFound();
            }

            return Ok(found);

        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            var result = await _employees.Create(employee);
            return CreatedAtRoute(result.EmployeeId, result);

        }

        [HttpPut]
        [Route("{employeeId}")]
        public async Task<IActionResult> Update(int employeeId, Employee employee)
        {
            if (employeeId != employee.EmployeeId)
            {
                return Conflict(new ConflictMessage() { Message = "Employeeid missmatch. Body does not match uri", Uri = employeeId, Body = employee.EmployeeId });
            }

        
            var result = await _employees.Update(employee);

            if (result.Exists)
            {
                return Ok(result.Employee);
            } else
            {
                return NotFound();
            }

        }

        [HttpDelete]
        [Route("{employeeId}")]
        public async Task<IActionResult> Delete(int employeeId)
        {
            await _employees.Delete(employeeId);

            return Ok();

        }
        private class ConflictMessage
        {
            public string Message { get; set; }
            public int Uri { get; set; }
            public int Body { get; set; }
        }
    }
}