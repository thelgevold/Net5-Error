using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC
{
    public interface IStorageProvider
    {
        Task<Content> GetContent(MyRequest request);
    }

    public class StorageProvider : IStorageProvider
    {
        private readonly IKeyProvider keyProvider;
        public StorageProvider(IKeyProvider keyProvider)
        {
            this.keyProvider = keyProvider;
        }
        public Task<Content> GetContent(MyRequest request)
        {
            var storedContents = new Content();

            // Working Code
            // storedContents.Keys = new Dictionary<string, System.Guid>();

            // foreach(var id in request.Ids)
            // {
            //     var key = keyProvider.GenerateKey(
            //            id,
            //            request.Date1,
            //            request.Date2,
            //            request.Environment,
            //            request.CurrencyCode,
            //            request.Namespace,
            //            request.BuildVersions
            //         );

            //     storedContents.Keys.Add(id, key);
            // }

            // This code fails
            storedContents.Keys = request.Ids.ToDictionary(
                k => k,
                v =>   keyProvider.GenerateKey(
                       v,
                       request.Date1,
                       request.Date2,
                       request.Environment,
                       request.CurrencyCode,
                       request.Namespace,
                       request.BuildVersions
                    )
                );
            
            return Task.FromResult(storedContents);
        }
    }
}