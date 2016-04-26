using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Caching;
using Microsoft.Extensions.OptionsModel;
using BuildDemoApi;

namespace api.Controllers
{
    [Route("topics")]
    public class ConceptsController : Controller
    {
        private readonly MemoryCache _cache;

        public ConceptsController(IApplicationEnvironment appEnv, IOptions<AzureAppSettings> appSettings)
        {
            _cache = new MemoryCache(appEnv.ApplicationBasePath, appSettings.Value.StorageUrl, appSettings.Value.AccountName, appSettings.Value.Key, appSettings.Value.ContainerName);
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