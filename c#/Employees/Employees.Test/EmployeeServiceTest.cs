using Microsoft.EntityFrameworkCore;
using Employees.Database;
using Xunit;
using System.Threading.Tasks;
using Employees.DataService;
using Employees.Model;

namespace Employees.Test
{
    public class EmployeeServiceTest
    {
        private async Task<EmployeeDbContext> CreateContext(bool seed = true)
        {
            var options = new DbContextOptionsBuilder<EmployeeDbContext>()
            .UseInMemoryDatabase(databaseName: $"UnitTest_{this.GetType().Name}")
            .Options;

            if (seed)
            {
                using (var context = new EmployeeDbContext(options))
                {
                    // Ensure we start with a clean database
                    context.Database.EnsureDeleted();

                    // Seed some data
                    context.Employee.AddRange(
                        new Model.Employee() { EmployeeId = 1, Role = "R1", SupervisorId = 0 },
                        new Model.Employee() { EmployeeId = 2, Role = "R2", SupervisorId = 1 },
                        new Model.Employee() { EmployeeId = 3, Role = "R3", SupervisorId = 1 }
                    );

                    await context.SaveChangesAsync();
                }
            }
            //Return a different context
            return new EmployeeDbContext(options);
        }

        [Fact]
        public async Task FindById_Found()
        {
            using (var context = await CreateContext())
            {
                var service = new EmployeeService(context);
                var found = await service.FindById(2);
                Assert.NotNull(found);
                Assert.Equal(2, found.EmployeeId);
                Assert.Equal("R2", found.Role);
            }
        }

        [Fact]
        public async Task FindById_NotFound()
        {
            using (var context = await CreateContext())
            {
                var service = new EmployeeService(context);
                var found = await service.FindById(-99);
                Assert.Null(found);
            }
        }

        [Fact]
        public async Task FindRoot()
        {
            using (var context = await CreateContext())
            {
                var service = new EmployeeService(context);
                var found = await service.FindRoot();
                Assert.NotNull(found);
            }
        }

        [Fact]
        public async Task List()
        {
            using (var context = await CreateContext())
            {
                var service = new EmployeeService(context);
                var results = await service.List();
                Assert.Equal(3, results.Length);
            }
        }

        [Fact]
        public async Task Create()
        {
            using (var context = await CreateContext())
            {
                var service = new EmployeeService(context);

                var employee = new Employee() { Role = "New", SupervisorId = 1 };
                var results = await service.Create(employee);

                Assert.Equal(4, employee.EmployeeId);
            }
        }

        [Fact]
        public async Task Update_Exists()
        {
            using (var context = await CreateContext())
            {
                var service = new EmployeeService(context);

                var employee = await context.Employee.FirstOrDefaultAsync(e => e.EmployeeId == 2);
                employee.Role = "Updated";

                var results = await service.Update(employee);

                Assert.True(results.Exists);
                Assert.Equal("Updated", results.Employee.Role);
            }

            // Verify DB updated
            using (var context = await CreateContext(false))
            {
                var employee = await context.Employee.FirstOrDefaultAsync(e => e.EmployeeId == 2);
                Assert.Equal("Updated", employee.Role);
            }
        }

        [Fact]
        public async Task Update_NotExists()
        {
            using (var context = await CreateContext())
            {
                var service = new EmployeeService(context);

                var employee = new Employee();
                employee.Role = "Updated";
                employee.EmployeeId = -99;

                var results = await service.Update(employee);

                Assert.False(results.Exists);
                Assert.Null(results.Employee);
            }
        }

        [Fact]
        public async Task Delete_Exists()
        {
            using (var context = await CreateContext())
            {
                var service = new EmployeeService(context);
                var results = await service.Delete(2);

                Assert.True(results);
            }

            // Verify DB deleted
            using (var context = await CreateContext(false))
            {
                var employee = await context.Employee.FirstOrDefaultAsync(e => e.EmployeeId == 2);
                Assert.Null(employee);
            }
        }

        [Fact]
        public async Task Delete_NotExists()
        {
            using (var context = await CreateContext())
            {
                var service = new EmployeeService(context);
               var results = await service.Delete(-99);

                Assert.False(results);
            }
        }
    }
}
