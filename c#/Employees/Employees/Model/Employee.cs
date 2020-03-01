using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employees.Model
{
    //Note: Normally Employee would have 2 classes. One to represent the database and a second to represent what is exposed.
    // Automapper would be used to convert these 2 objects. The advantage to this is that you don't expose you database schema.
    //For simplicity I have combound them into one class.
    public class Employee
    {
        [JsonPropertyName("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        [JsonPropertyName("names")]
        public string[] Names { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("supervisorId")]
        public int SupervisorId { get; set; }
    }
}
