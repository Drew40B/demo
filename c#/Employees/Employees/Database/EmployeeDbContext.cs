using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employees.Model;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Employees.Database
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

            //Note: This is required to seed the in memory database correctly.
            this.Database.EnsureCreated();

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // EF cannot map string[] so create a custom converter.
            builder.Entity<Employee>().Property(e => e.Names).HasConversion(
                v => JsonSerializer.Serialize(v, null),
                v => JsonSerializer.Deserialize<string[]>(v, null)
               );


            builder.Entity<Employee>().HasData(
                    new Employee() { EmployeeId = 1, SupervisorId = 0, Names = new[] { "John", "Charles", "Davis" }, Role = "CIO" },
                    new Employee() { EmployeeId = 2, SupervisorId = 1, Names = new[] { "Fred",  "Camble" }, Role = "VP" },
                    new Employee() { EmployeeId = 3, SupervisorId = 1, Names = new[] { "Anne", "Sandoval",  }, Role = "VP" },
                    new Employee() { EmployeeId = 4, SupervisorId = 1, Names = new[] { "Jessica", "Jane", "Simpson" }, Role = "VP" }
                    );

        }

        

        public  DbSet<Employee> Employee { get; set; }
    }
}
