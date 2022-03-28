using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContextSeed
    {
        public static async Task SeedAsync(StreamerDbContext context, 
            ILogger<StreamerDbContextSeed> logger)
        {
            if (!context.Streamers!.Any())//para saber si tiene datos en su interior
            {
                context.Streamers!.AddRange(GetPreconfiguredStreamer());
                await context.SaveChangesAsync();
                logger.LogInformation("Estamos insertando nuevos records a la bd {context}", 
                    typeof(StreamerDbContext).Name);
            }
        }

        private static IEnumerable<Streamer> GetPreconfiguredStreamer()
        {
            return new List<Streamer>
            {
                new Streamer{ CreatedBy = " Davisilio", Nombre = "Maxi HBD", Url = "https://www.jw.org"},
                new Streamer{ CreatedBy = " Davisilio", Nombre = "Amazon VIP", Url = "https://www.jw.org"},
            };
        }
    }
}
