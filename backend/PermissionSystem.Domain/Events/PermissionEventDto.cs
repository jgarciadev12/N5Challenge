namespace PermissionSystem.Domain.Events
{
    public class PermissionEventDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string NameOperation { get; set; } = string.Empty;
    }
}
