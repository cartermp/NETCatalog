using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NETCatalog.API;
using Microsoft.Extensions.PlatformAbstractions;

namespace NETCatalog.Api.Controllers
{
    [Route("topics")]
    public class ConceptsController : Controller
    {
        private readonly MemoryCache _cache;

        public ConceptsController(IOptions<AzureOptions> options)
        {
            //_cache = new MemoryCache(appEnv.ApplicationBasePath, 
            //                         options.Value.StorageUrl, 
            //                         options.Value.AccountName, 
            //                         options.Value.Key, 
            //                         options.Value.ContainerName);
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var data = await _cache.GetDataAsync();
            return JsonConvert.SerializeObject(data.Keys);
        }

        [HttpGet("{category}")]
        public async Task<string> Get(string category)
        {
            var data = await _cache.GetDataAsync();

            if (!data.ContainsKey(category))
            {
                return $"{category} does not exist!";
            }

            return JsonConvert.SerializeObject(data[category].Keys);
        }

        [HttpGet("{category}/{concept}")]
        public async Task<string> Get(string category, string concept)
        {
            var data = await _cache.GetDataAsync();

            if (!data.ContainsKey(category))
            {
                return $"{category} does not exist!";
            }

            if (!data[category].Any(c => c.Key.ToUpper() == concept.ToUpper()))
            {
                return $"{concept} is not a part of {category}!";
            }

            return data[category][concept];
        }
    }
}