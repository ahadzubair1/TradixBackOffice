namespace AspNetCoreHero.Boilerplate.Domain.Entities.App
{
    public class Product : AuditableEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public Guid BrandId { get; set; }
        public virtual Brand Brand { get; set; }
    }
}