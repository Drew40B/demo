
using People.Lib;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.IO;

namespace PeopleTest
{
  public  class EmployeeDatabaseTest
    {
        [Fact]
        public async void DeserializeEmployee_HappyPath()
        {
            var db = new EmployeeDatabase();

            using (FileStream fs = File.OpenRead("./dataFiles/sampleEmployee.json"))
            {
                var employee = await db.deserializeEmployee(fs);
                Assert.NotNull(employee);
                Assert.Equal(18, employee.EmployeeId);
                Assert.Equal(2,employee.Names.Length);
                Assert.Equal("Alexis", employee.Names[0]);
                Assert.Equal("White", employee.Names[1]);
                Assert.Equal("Senior VP", employee.Role);
                Assert.Equal(17, employee.SupervisorId);
            }

        }

     
        }

    }
}
