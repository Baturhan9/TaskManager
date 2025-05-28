using System.Text.Json.Serialization;
using TaskManager.Consts;

namespace TaskManager.Api.Models
{
    public class AssignmentModel
    {
        public int UserId { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TaskRoles UserRole { get; set; }
    }
}