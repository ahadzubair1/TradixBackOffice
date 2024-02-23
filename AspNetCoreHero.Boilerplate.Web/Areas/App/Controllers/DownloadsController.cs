using AspNetCoreHero.Boilerplate.Application.Features.Brands.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.Brands.Commands.Delete;
using AspNetCoreHero.Boilerplate.Application.Features.Brands.Commands.Update;
using AspNetCoreHero.Boilerplate.Application.Features.Brands.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Brands.Queries.GetById;
using AspNetCoreHero.Boilerplate.Web.Areas.App.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Areas.App.Controllers
{
    [Area("App")]
    public class DownloadsController : BaseController<DownloadsController>
    {
        public IActionResult Index()
        {
            var model = new DownloadsViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            //var response = await _mediator.Send(new GetAllDownloadsCachedQuery());
            //if (response.Succeeded)
            //{
            //    var viewModel = _mapper.Map<List<DownloadsViewModel>>(response.Data);
            //    return PartialView("_ViewAll", viewModel);
            //}
            return null;
        }

        public async Task<JsonResult> OnGetCreateOrEdit(Guid id)
        {
            return null;
            //var brandsResponse = await _mediator.Send(new GetAllDownloadsCachedQuery());

            //if (id == Guid.Empty)
            //{
            //    var brandViewModel = new BrandViewModel();
            //    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", brandViewModel) });
            //}
            //else
            //{
            //    var response = await _mediator.Send(new GetDownloadsByIdQuery() { Id = id });
            //    if (response.Succeeded)
            //    {
            //        var brandViewModel = _mapper.Map<DownloadsViewModel>(response.Data);
            //        return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", brandViewModel) });
            //    }
            //    return null;
            //}
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(Guid id, DownloadsViewModel brand)
        {
            return null;
            //if (ModelState.IsValid)
            //{
            //    if (id == Guid.Empty)
            //    {
            //        var createBrandCommand = _mapper.Map<CreateDownloadsCommand>(brand);
            //        var result = await _mediator.Send(createBrandCommand);
            //        if (result.Succeeded)
            //        {
            //            id = result.Data;
            //            _notify.Success($"Brand with ID {result.Data} Created.");
            //        }
            //        else _notify.Error(result.Message);
            //    }
            //    else
            //    {
            //        var updateBrandCommand = _mapper.Map<UpdateDownloadsCommand>(brand);
            //        var result = await _mediator.Send(updateBrandCommand);
            //        if (result.Succeeded) _notify.Information($"Brand with ID {result.Data} Updated.");
            //    }
            //    var response = await _mediator.Send(new GetAllDownloadsCachedQuery());
            //    if (response.Succeeded)
            //    {
            //        var viewModel = _mapper.Map<List<BrandViewModel>>(response.Data);
            //        var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
            //        return new JsonResult(new { isValid = true, html = html });
            //    }
            //    else
            //    {
            //        _notify.Error(response.Message);
            //        return null;
            //    }
            //}
            //else
            //{
            //    var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", brand);
            //    return new JsonResult(new { isValid = false, html = html });
            //}
        }

        //[HttpPost]
        [HttpDelete]
        public async Task<JsonResult> OnPostDelete(Guid id)
        {
            return null;
            //var deleteCommand = await _mediator.Send(new DeleteDownloadsCommand { Id = id });
            //if (deleteCommand.Succeeded)
            //{
            //    _notify.Information($"Brand with Id {id} Deleted.");
            //    var response = await _mediator.Send(new GetAllDownloadsCachedQuery());
            //    if (response.Succeeded)
            //    {
            //        var viewModel = _mapper.Map<List<DownloadsViewModel>>(response.Data);
            //        var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
            //        return new JsonResult(new { isValid = true, html = html });
            //    }
            //    else
            //    {
            //        _notify.Error(response.Message);
            //        return null;
            //    }
            //}
            //else
            //{
            //    _notify.Error(deleteCommand.Message);
            //    return null;
            //}
        }
    }
}