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
    public class BrandController : BaseApiController<BrandController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var brands = await _mediator.Send(new GetAllBrandsCachedQuery());
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var brand = await _mediator.Send(new GetBrandByIdQuery() { Id = id });
            return Ok(brand);
        }

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