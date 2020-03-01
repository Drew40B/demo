using System.Threading.Tasks;
using Employees.Database;
using Employees.Interfaces;
using Employees.Model;
using Microsoft.EntityFrameworkCore;

namespace Employees.DataService
{
    public class EmployeeService : IEmployeeService
    {
        private EmployeeDbContext _context;

        public EmployeeService(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> FindById(int empoloyeeId)
        {
            return await _context.Employee.FirstOrDefaultAsync(e => e.EmployeeId == empoloyeeId);
        }

        public async Task<Employee> FindRoot()
        {
            return await _context.Employee.AsQueryable().FirstOrDefaultAsync(e => e.SupervisorId == 0);

        }

        // noop
        public async Task Init() => await Task.CompletedTask;

        public async Task<Employee[]> List()
        {
            return await _context.Employee.ToArrayAsync();
        }

        public async Task<Employee> Create(Employee employee)
        {
            var result = await _context.Employee.AddAsync(employee);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<(bool Exists, Employee Employee)> Update(Employee employee)
        {
            if (! await Exists(employee.EmployeeId))
            {
                return (Exists: false, Employee: null);
            }

            var result = _context.Employee.Update(employee);
            await _context.SaveChangesAsync();

            return (Exists: true, Employee: result.Entity);
        }

        public async Task<bool> Exists(int EmployeeId)
        {
            return await _context.Employee.AsNoTracking().AnyAsync(e => e.EmployeeId == EmployeeId);
        }

        public async Task<bool> Delete(int employeeId)
        {
            var found = await FindById(employeeId);
        
            if (found == null)
            {
                return false;
            }

            _context.Employee.Remove(found);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
