using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MessagePack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using POC;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IStorageProvider storageProvider;
        public TestController(IStorageProvider storageProvider)
        {
            this.storageProvider = storageProvider;
        }

        [HttpGet]
        public Task<Content> Get()
        {
            var buildVersions = new Dictionary<string, string>();
            buildVersions.Add("A", "2.2");
            buildVersions.Add("B", "2.2");
            buildVersions.Add("C", "2.2");

            var request = new MyRequest()
            {
                Environment = "Testing 123",
                Ids = Enumerable.Range(0, 50).Select(i => Guid.NewGuid().ToString()).ToArray(),
                CurrencyCode = "USD",
                Date1 = DateTime.UtcNow,
                Date2 = DateTime.UtcNow,
                BuildVersions = buildVersions,
                Namespace = "This is a test"
            };

            return storageProvider.GetContent(request);
        }
        
    }
}
