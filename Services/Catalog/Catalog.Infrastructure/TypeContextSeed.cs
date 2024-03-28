using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infrastructure
{

    public static class TypeContextSeed
    {

        public static void SeedData(IMongoCollection<Product> typeCollection)
        {

            bool checkTypes = typeCollection.Find(b => true).Any();

            string path = Path.Combine("Data", "SeedData", "types.json");

            if (!checkTypes)
            {
                var typesData = File.ReadAllText(path);

                var types = JsonSerializer.Deserialize<List<Product>>(typesData);

                if (types != null)
                {
                    foreach (var item in types)
                    {
                        typeCollection.InsertOneAsync(item);
                    }
                }
            }
        }

    }
}
