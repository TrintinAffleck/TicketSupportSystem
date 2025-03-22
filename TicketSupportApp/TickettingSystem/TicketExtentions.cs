using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicketSystem.BLL;
using TicketSystem.DAL;

namespace TickettingSystem
{
    public static class TicketExtentions
    {
        public static void AddBackendDependencies(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<TicketSupportContext>(options);
            services.AddTransient<TicketServices>(ServiceProvider =>
            {
                var context = ServiceProvider.GetService<TicketSupportContext>() ?? throw new Exception("Ticket Service was null");
                return new TicketServices(context);
            });
        }
    }
}
