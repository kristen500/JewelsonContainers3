using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebMVC.infrastructure;
using WebMVC.Models;

namespace WebMVC.services
{
    public class Catalogservice : ICatalogService
    {
        private readonly IHttpClient _httpClient;
        private readonly string _baseUrl;
        public Catalogservice(IConfiguration config, IHttpClient client)
        {
            _httpClient = client;
            _baseUrl = $"{config["CatalogUrl"]}/api/Catalog";
        }
        public async Task<IEnumerable<SelectListItem>> GetBrandsAsync()
        {
            var brandUri = APIPaths.Catalog.GetAllBrands(_baseUrl);
            var datastring = await _httpClient.GetStringAsync(brandUri);
            var items = new List<SelectListItem>()
            {
                new SelectListItem
                {
                Value = null,
                Text = "All",
                Selected = true,
                }
            };
           var brands = JArray.Parse(datastring);
            foreach (var item in brands)
            {
                items.Add(new SelectListItem
                {
                    Value = item.Value<string>("id"),
                    Text = item.Value<string>("brand")
                });
            }
            return items;
        }

        public async Task<Catalog> GetCatalogItemsAsync(int page, int size, int? brand, int? type)
        {
            var catalogItemsUri = APIPaths.Catalog.GetAllCatalogItems(_baseUrl, page, size, brand, type);
            var datastring = await _httpClient.GetStringAsync(catalogItemsUri);
            return JsonConvert.DeserializeObject<Catalog>(datastring);
        }

        public async Task<IEnumerable<SelectListItem>> GetTypesAsync()
        {
            var typesUri = APIPaths.Catalog.GetAllTypes(_baseUrl);
            var datastring = await _httpClient.GetStringAsync(typesUri);
            var items = new List<SelectListItem>()
            {
                new SelectListItem
                {
                Value = null,
                Text = "All",
                Selected = true,
                }
            };
            var types = JArray.Parse(datastring);
            foreach (var item in types)
            {
                items.Add(new SelectListItem
                {
                    Value = item.Value<string>("id"),
                    Text = item.Value<string>("type")
                });
            }
            return items;
        }
    }
}
