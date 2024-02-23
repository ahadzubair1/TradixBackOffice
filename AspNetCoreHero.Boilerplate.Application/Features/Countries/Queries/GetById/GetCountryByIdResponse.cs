namespace AspNetCoreHero.Boilerplate.Application.Features.Countries.Queries.GetById
{
    public class GetCountryByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Alpha2Code { get; set; }
        public string? Alpha3Code { get; set; }
        public string? DialingCode { get; set; }
        public string? Flag { get; set; }
        public DefaultIdType? RiskTypeId { get; set; }
        public bool AllowUserRegistration { get; set; } = true;
    }
}