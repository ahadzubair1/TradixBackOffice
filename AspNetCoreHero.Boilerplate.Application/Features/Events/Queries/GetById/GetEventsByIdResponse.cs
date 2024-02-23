namespace AspNetCoreHero.Boilerplate.Application.Features.Events.Queries.GetById
{
    public class GetEventsByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Tax { get; set; }
        public string Description { get; set; }
    }
}