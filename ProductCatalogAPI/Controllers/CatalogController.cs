using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Data;
using ProductCatalogAPI.Domain;

namespace ProductCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {

        private readonly CatalogContext _context;
        private readonly IConfiguration _config;
        public CatalogController(CatalogContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        
        [HttpGet("[action")]
        public async Task<IActionResult> CatalogTypes()
        {
          var types = await _context.CatalogTypes.ToListAsync();
            return Ok(types);
        }

        [HttpGet("[action")]
        public async Task<IActionResult> Catalogbrands()
        {
            var Brands = await _context.CatalogBrands.ToListAsync();
            return Ok(Brands);
        }

        [HttpGet("[action}")]
        public async Task<IActionResult> Items(
            [FromQuery]int pageIndex = 0, [FromQuery]int pageSize = 6)
        {
          var items= await  _context.Catalog
                .OrderBy(c => c.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            items = ChangePictureUrl(items);
            return Ok(items);   
        }

        private List<CatalogItem> ChangePictureUrl(List<CatalogItem> items)
        {
            items.ForEach(item => item.PictureUrl = item.PictureUrl
                                        .Replace("http://externalcatalogbaseurltobereplaced",
                                        _config["ExternalBaseUrl"]));
            return items;
        }
    }
}
