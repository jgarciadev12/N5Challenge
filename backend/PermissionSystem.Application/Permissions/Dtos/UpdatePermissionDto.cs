namespace PermissionSystem.Application.Permissions.Dtos
{
    public class UpdatePermissionDto
    {
        public int Id { get; set; } // necesario para actualizar
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeLastName { get; set; } = string.Empty;
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
