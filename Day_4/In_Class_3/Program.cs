using System.Collections.Generic;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text.Json;
 
namespace BlockingExample
{
    public class Pet
    {
        public long id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public Category category { get; set; }
        public List<string> photoUrls { get; set; }
        public List<Tag> tags { get; set; }
 
    }
 
    public class Category
    {
        public long id { get; set; }
        public string name { get; set; }
    }
 
    public class Tag
    {
        public long id { get; set; }
        public string name { get; set; }
    }
 
    class Program
    {
        static async Task Main(string[] args)
        {
            using (HttpClient client = new HttpClient())
            {
                string[] urls =
                {
                    "https://petstore.swagger.io/v2/pet/findByStatus?status=available",
                    "https://petstore.swagger.io/v2/pet/findByStatus?status=pending",
                    "https://petstore.swagger.io/v2/pet/findByStatus?status=sold"
                };
                var start = DateTime.Now;
 
                var tasks = new List<Task<HttpResponseMessage>>();
                foreach (var url in urls)
                {
                    tasks.Add(client.GetAsync(url));
                }
 
                HttpResponseMessage[] responses = await Task.WhenAll(tasks);
 
                for (int i = 0; i < responses.Length; i++)
                {
                    string responseBody = await responses[i].Content.ReadAsStringAsync();
                    var pets = JsonSerializer.Deserialize<List<Pet>>(responseBody);
                    if (pets != null && pets.Count > 0)
                    {
                        var pet = pets[0];
                        Console.WriteLine($"Example Id: {pet.id}, Name: {pet.name}, Category: {pet.category?.name}, Tags: {string.Join(", ", pet.tags?.ConvertAll(t => t.name) ?? new List<string>())}");
                    }
                }
 
                var end = DateTime.Now;
                Console.WriteLine($"Total time(blocking): {(end - start).TotalSeconds:F2} seconds");
 
            }
        }
    }
}
 