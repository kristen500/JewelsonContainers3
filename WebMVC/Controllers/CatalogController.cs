using Microsoft.AspNetCore.Mvc;
using WebMVC.services;
using WebMVC.ViewModels;

namespace WebMVC.Controllers
{

    public class CatalogController : Controller
    {
        private readonly ICatalogService _Service;
        public CatalogController(ICatalogService service)
        {
            _Service = service;
        }
        public async Task <IActionResult> Index(int? page, int? brandFilterApplied, int? typesFilterApplied)
        {
            var itemsOnPage = 10;
            var catalog = await _Service.GetCatalogItemsAsync(page ?? 0, itemsOnPage, brandFilterApplied, typesFilterApplied);
            var vm = new CatalogIndexViewModel
            {
                Brands = await _Service.GetBrandsAsync(),
                Types = await _Service.GetTypesAsync(),
                CatalogItems = catalog.Data,
                PaginationInfo = new PaginationInfo
                {
                    ActualPage = catalog.PageIndex,
                    TotalItems = catalog.Count,
                    ItemsPerPage = catalog.PageSize,
                    TotalPages = (int)Math.Ceiling((decimal)catalog.Count / itemsOnPage)
                },
                BrandFilterApplied = brandFilterApplied,
                TypesFilterApplied = typesFilterApplied

            };

            return View(vm);
        }
    }
}

