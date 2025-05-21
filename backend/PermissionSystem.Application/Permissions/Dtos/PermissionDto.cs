namespace PermissionSystem.Application.Permissions.Dtos
{
    public class PermissionDto
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeLastName { get; set; } = string.Empty;
        public DateTime PermissionDate { get; set; }
        public string PermissionTypeDescription { get; set; } = string.Empty;
    }
}
