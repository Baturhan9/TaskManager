using TaskManager.Consts;

namespace TaskManager.Api.Models
{
    public class AssignmentModel
    {
        public int UserId { get; set; }
        public TaskRoles UserRole { get; set; }
    }
}