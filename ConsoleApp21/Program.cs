using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp21
{
    class Program
    {
        static void Main(string[] args)
        {
            var order = new Order()
            {
                Id = 1,
                DataCreated = DateTime.UtcNow,
                Itens = new List<Product>() { new Product() { Id = 2, Name = "book salads", Price = 9.30m }, }
            };

            var resultA = JsonSerializer.Serialize<Order>(order);
            
            var jsonSerializerOptionsB = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true
            };
            var resultB = JsonSerializer.Serialize<Order>(order, jsonSerializerOptionsB);

            var jsonSerializerOptionsC = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = new SnakeCasePropertyNamingPolicy(),
                WriteIndented = true,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true
            };
            var resultC = JsonSerializer.Serialize<Order>(order, jsonSerializerOptionsC);
            

            var jsonSerializerOptionsResultA = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = new SnakeCasePropertyNamingPolicy(),
                WriteIndented = true,
                AllowTrailingCommas=true,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true
            };
            var valueA = "{ \"id\":1,\"data_created\":\"2019-10-20T02:45:56.0976212Z\",\"itens\":[{\"id\":2,\"name\":\"book salads\",\"price\":9.30}],} ";
            var orderA = JsonSerializer.Deserialize<Order>(valueA, jsonSerializerOptionsResultA);

            var jsonSerializerOptionsResultB = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = new SnakeCasePropertyNamingPolicy(),
                WriteIndented = true,
                AllowTrailingCommas = false,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true
            };
            var valueB = "{ \"id\":1,\"data_created\":\"2019-10-20T02:45:56.0976212Z\",\"itens\":[{\"id\":2,\"name\":\"book salads\",\"price\":9.30}],} "; //exta comma
            var orderB = JsonSerializer.Deserialize<Order>(valueA, jsonSerializerOptionsResultB);

        }
    }
    public class SnakeCasePropertyNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            var index = 0;
            var builder = new StringBuilder();
            foreach (var character in name.ToCharArray())
            {
                if (char.IsUpper(character) && index > 0)
                    builder.Append('_');

                builder.Append(character);

                index++;
            }

            return builder.ToString().ToLower();
        }
    }


    public class Order
    {
        public int Id { get; set; }
        public DateTime DataCreated { get; set; }
        public List<Product> Itens { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
