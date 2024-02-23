using AspNetCoreHero.Boilerplate.Application.Features.Products.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.Products.Commands.Delete;
using AspNetCoreHero.Boilerplate.Application.Features.Products.Commands.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using AspNetCoreHero.Boilerplate.Application.Features.Tickets.Queries.GetById;
using AspNetCoreHero.Boilerplate.Application.Features.Tickets.Queries.GetAllPaged;
using AspNetCoreHero.Boilerplate.Application.Features.Tickets.Commands.Create;

namespace AspNetCoreHero.Boilerplate.Api.Controllers.v1
{
    public class SupportController : BaseApiController<SupportController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var products = await _mediator.Send(new GetAllTicketsQuery(pageNumber, pageSize));
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _mediator.Send(new GetTicketByIdQuery() { Id = id });
            return Ok(product);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateTicketsCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateTicketCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteProductCommand { Id = id }));
        }
    }
}
