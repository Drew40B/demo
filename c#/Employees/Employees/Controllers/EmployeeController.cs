using System.Threading.Tasks;
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

        /// <summary>
        /// Get a list of employees
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<Employee[]> List()
        {
            return await _employees.List();
        }

        /// <summary>
        /// Gets a specific employee by their id
        /// </summary>
        /// <param name="employeeId">employeeId to search for</param>
        /// <returns>Employee associated with employee ID or null if not found</returns>
        [HttpGet]
        [Route("{employeeId}")]
        public async Task<ActionResult<Employee>> Get(int employeeId)
        {

            var found = await _employees.FindById((int)employeeId);

            if (found == null)
            {
                return NotFound();
            }

            return Ok(found);

        }

        /// <summary>
        /// Creates an employee
        /// </summary>
        /// <param name="employee">employee to be created</param>
        /// <returns>newly create employee including their employeeId</returns>
        [HttpPost]
        public async Task<ActionResult<Employee>> Create(Employee employee)
        {
            var result = await _employees.Create(employee);
            return CreatedAtRoute(result.EmployeeId, result);

        }

        /// <summary>
        /// Updates an emplyee
        /// </summary>
        /// <param name="employeeId">emplyee id to be updated</param>
        /// <param name="employee">employee to be updated</param>
      /// <returns>updatedemployee</returns>
        [HttpPut]
        [Route("{employeeId}")]
        public async Task<ActionResult<Employee>> Update(int employeeId, Employee employee)
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

        /// <summary>
        /// Deletes the desired employee
        /// </summary>
        /// <param name="employeeId">Id of the employee to delete</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{employeeId}")]
        public async Task<ActionResult<Employee>> Delete(int employeeId)
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