namespace PermissionSystem.Application.Permissions.Dtos
{
    public class CreatePermissionDto
    {
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeLastName { get; set; } = string.Empty;
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
