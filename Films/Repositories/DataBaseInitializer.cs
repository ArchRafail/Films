using Microsoft.EntityFrameworkCore;

namespace Films.Repositories
{
    public static class DataBaseInitializer
    {
        public static async Task InitializeDatabaseAsync(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var filmsDbContext = serviceScope.ServiceProvider.GetService<FilmsDbContext>();
            await filmsDbContext.Database.MigrateAsync();
        }
    }
}
