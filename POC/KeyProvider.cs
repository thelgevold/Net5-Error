using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace POC
{
    public interface IKeyProvider
    {
        Guid GenerateKey(
            string id,
            DateTime valuationDate,
            DateTime? insertedDate,
            string environment,
            string currencyCode,
            string ns,
            Dictionary<string, string> buildVersions
        );
    }

    public class KeyProvider : IKeyProvider
    {
        public Guid GenerateKey(string id, DateTime date1, DateTime? date2, string environment, string currencyCode, string ns, Dictionary<string, string> buildVersions)
        {
            var buildVersionString = string.Join(',', buildVersions.OrderBy(bv => bv.Key).Select(bv => $"{bv.Key}:{bv.Value}"));

            var key = new Key(id, date1, date2, environment, currencyCode, ns, buildVersionString); 

            var str = JsonConvert.SerializeObject(key);
            var bytes = Encoding.UTF8.GetBytes(str);

            var hashBytes = ComputeHash(bytes);

            return new Guid(hashBytes);
        }

        private byte[] ComputeHash(byte[] bytes)
        {
           using(var md5Hasher = MD5.Create())
           {
               return md5Hasher.ComputeHash(bytes);
           }     
        }
    }

    public class Key
    {
        public Key(
            string id,
            DateTime date1,
            DateTime? date2,
            string environment,
            string currencyCode,
            string ns,
            string buildVersions)
        {
            Id = id;
            Date1 = date1;
            Date2 = date2;
            Environment = environment;
            CurrencyCode = currencyCode;
            Namespace = ns;
            BuildVersions = buildVersions;
        }

        public string Id {get; set;}
        public DateTime Date1  {get; set;}
        DateTime? Date2  {get; set;}
        string Environment  {get; set;}
        string CurrencyCode  {get; set;}
        string Namespace  {get; set;}
        string BuildVersions  {get; set;}
    }
}