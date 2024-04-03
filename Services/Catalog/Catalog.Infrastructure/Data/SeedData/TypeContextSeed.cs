using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{

    public static class TypeContextSeed
    {

        public static void SeedData(IMongoCollection<ProductType> typeCollection)
        {

            bool checkTypes = typeCollection.Find(b => true).Any();

           //Container path
            string path = Path.Combine("Data", "SeedData", "types.json");

            if (!checkTypes)
            {
                //local path
                //var data = File.ReadAllText("../Catalog.Infrastructure/Data/SeedData/types.json");
                
                var typesData = File.ReadAllText(path);

                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

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
