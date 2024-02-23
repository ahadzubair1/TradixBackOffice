using AspNetCoreHero.Boilerplate.Application.Features.Brands.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.Brands.Commands.Delete;
using AspNetCoreHero.Boilerplate.Application.Features.Brands.Commands.Update;
using AspNetCoreHero.Boilerplate.Application.Features.Brands.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Brands.Queries.GetById;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Api.Controllers.v1
{
    public class BonusController : BaseApiController<BonusController>
    {

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateBrandCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateBrandCommand command)
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
            return Ok(await _mediator.Send(new DeleteBrandCommand { Id = id }));
        }
    }
}