using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace People.Model
{
    public class Employee
    {
        [JsonPropertyName("id")]
        public int EmployeeId { get; set; }

        [JsonPropertyName("names")]
        public string[] Names { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("supervisorId")]
        public int SupervisorId { get; set; }
    }
}
