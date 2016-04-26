using System;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Net.Http;

namespace Caching
{
    using DataType = Dictionary<string, Dictionary<string, string>>;

    public class MemoryCache
    {
        private readonly string _path;
        private DataType _data;

        public MemoryCache(string path, string storageUrl, string storageAccountName, string storageKey, string containerName)
        {
            var cachePath = path + "/cache";
            _path = cachePath + "/data.json";

            if (File.Exists(_path) && new FileInfo(_path).Length > 0)
            {
                return;
            }

            Directory.CreateDirectory(cachePath);

            var httpclient = new HttpClient();            

            var blobclient = new CloudBlobClient(new Uri(storageUrl), 
                                                 new StorageCredentials(storageAccountName, storageKey));

            var container = blobclient.GetContainerReference(containerName);

            var lookup = container.ListBlobsSegmentedAsync(null, true, BlobListingDetails.All, null, null, null, null).Result.Results
                        .Select(f => f.Uri.ToString())
                        .GroupBy(u => u.GetCategory())
                        .ToDictionary(g => g.Key,
                                      g => g.ToDictionary(u => u.GetConcept(),
                                                          u => httpclient.GetStringAsync(u).Result));

            var json = JsonConvert.SerializeObject(lookup);

            File.WriteAllText(_path, json);
        }

        public Task<DataType> GetDataAsync()
        {
            if (_data == null)
            {
                _data = JsonConvert.DeserializeObject<DataType>(File.ReadAllText(_path));
            }

            return Task.FromResult(_data);
        }
    }

    public static class StringExtensions
    {
        /// Snags the name of the category from the given path.
        /// The name comes after the last '/'.
        public static string GetCategory(this string path)
        {
            var splitted = path.Split('/');
            return splitted[splitted.Length - 2];
        }

        /// Snags the name of the file from the path.
        ///
        /// File format looks like this:
        ///
        /// ../*/{conceptName}.md
        public static string GetConcept(this string path)
        {
            int end = 0;
            int begin = 0;

            for (int i = path.Length - 1; i > 0; i--)
            {
                if (path[i] == '.')
                {
                    end = i;
                    continue;
                }
                else if (path[i] == '/')
                {
                    begin = i + 1;
                    break;
                }
            }

            return path.Substring(begin, end - begin);
        }
    }

}