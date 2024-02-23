namespace AspNetCoreHero.Boilerplate.Domain.Entities.App
{
    public class Brand : AuditableEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Tax { get; set; }
    }
}