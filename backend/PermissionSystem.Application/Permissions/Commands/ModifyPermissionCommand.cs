using MediatR;

namespace PermissionSystem.Application.Permissions.Commands
{
    public class ModifyPermissionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeLastName { get; set; } = string.Empty;
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
