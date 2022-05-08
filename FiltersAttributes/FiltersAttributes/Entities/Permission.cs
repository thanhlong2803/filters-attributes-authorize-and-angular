namespace FiltersAttributes.Entities
{
    public class Permission : EntityBase
    {
        public long RoleId { get; set; }
        public string? Name { get; set; }
        public Type Type { get; set; }
    }
}
