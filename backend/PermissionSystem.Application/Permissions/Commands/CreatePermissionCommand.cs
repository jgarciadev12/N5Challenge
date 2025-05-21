using MediatR;

namespace PermissionSystem.Application.Permissions.Commands
{
    public class CreatePermissionCommand : IRequest<int>
    {
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeLastName { get; set; } = string.Empty;
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
