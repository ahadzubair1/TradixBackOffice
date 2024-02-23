namespace AspNetCoreHero.Boilerplate.Application.Features.Tickets.Queries.GetById
{
    public class GetTicketByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public Guid BrandId { get; set; }
    }
}