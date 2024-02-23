namespace AspNetCoreHero.Boilerplate.Application.Features.Tickets.Queries.GetAllPaged
{
    public class GetAllTicketsResponse
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Guid AssignedTo { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedDate { get;set; }
    }
}