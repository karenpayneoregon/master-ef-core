using System.Linq;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace EntityLibraryCore5.Data
{
    public partial class NorthwindContext
    {
        public static string BuildConnection()
        {

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var sections = configuration.GetSection("database").GetChildren().ToList();

            return
                $"Data Source={sections[1].Value};" +
                $"Initial Catalog={sections[0].Value};" +
                $"Integrated Security={sections[2].Value}";

        }
    }
}