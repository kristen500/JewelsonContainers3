﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.services;
using WebMVC.ViewModels;

namespace WebMVC.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _service;
        public CatalogController(ICatalogService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(int? page, int? brandFilterApplied, int? typesFilterApplied)
        {
            var itemsOnPage = 10;
            var catalog = await _service.GetCatalogItemsAsync(page ?? 0, itemsOnPage,
                brandFilterApplied, typesFilterApplied);
            var vm = new CatalogIndexViewModel
            {
                Brands = await _service.GetBrandsAsync(),
                Types = await _service.GetTypesAsync(),
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

        [Authorize]
        public IActionResult About()
        {
            return View();
        }

    }
}
