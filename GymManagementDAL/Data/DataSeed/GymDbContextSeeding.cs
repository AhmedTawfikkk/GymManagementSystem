using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.DataSeed
{
    public static class GymDbContextSeeding
    {

        public static bool SeedData(GymDbContext dbContext)
        {
            try
            {
                var HasPlans = dbContext.Plans.Any();
                var HasCategories = dbContext.categories.Any();
                if (!HasPlans)
                {
                    var plans = LoadDataFromJson<Plan>("plans.json");
                    if (plans.Any()) 
                    {
                        dbContext.Plans.AddRange(plans);
                    }
                }
                if (!HasCategories)
                {
                    var categories = LoadDataFromJson<Category>("categories.json");
                    if (categories.Any())
                    {
                        dbContext.categories.AddRange(categories);
                    }
                }
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"seeding failed - {ex}");
                return false;

            }
            
        }
        

        private static List<T> LoadDataFromJson<T>(string fileName)
        {
           var filepath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files", fileName);
            if(!File.Exists(filepath))
            {
                throw new FileNotFoundException("Data seed file not found", filepath);
            }
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            string jsonData = File.ReadAllText(filepath);
            var data =JsonSerializer.Deserialize<List<T>>(jsonData);
            return data ?? new List<T>();
        }
    }
}
