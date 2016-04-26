using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var url = "http://dotnet-buildtwentysixteendemo.azurewebsites.net/topics";
            
            var client = new HttpClient();
            
            string json = string.Empty;
            
            switch (args.Length)
            {
                case 0:
                    json = client.GetStringAsync(url).Result;
                    var categories = JsonConvert.DeserializeObject<string[]>(json);
                                        
                    Console.WriteLine("Categories you can learn more about:\n");
                    foreach (var c in categories)
                    {
                        Console.WriteLine($"\t{c}");
                    }
                    
                    break;
                case 1:
                    json = client.GetStringAsync($"{url}/{args[0]}").Result;
                    var concepts = JsonConvert.DeserializeObject<string[]>(json);
                    
                    Console.WriteLine($"Concepts under /{args[0]}/ you can learn more about:\n");
                    foreach (var c in concepts)
                    {
                        Console.WriteLine($"\t{c}");
                    }
                    
                    break;
                case 2:
                    var concept = client.GetStringAsync($"{url}/{args[0]}/{args[1]}").Result;
                    Console.WriteLine(concept);
                    break;
            }
        }
    }
}
