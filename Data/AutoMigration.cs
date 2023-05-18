using Microsoft.EntityFrameworkCore;

namespace PokedexApi.Data
{
    public static class AutoMigration
    {
        public static void AutoMigrate(this WebApplication app)
        {
            using(var scope = app.Services.CreateScope())
            {
                using(var appContext = scope.ServiceProvider.GetRequiredService<PokedexDbContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
        }
    }
}
