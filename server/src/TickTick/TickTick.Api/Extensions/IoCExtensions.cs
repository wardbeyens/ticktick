using MediatR;
using TickTick.Api.Services;
using TickTick.Models;
using TickTick.Repositories.Base;

namespace TickTick.Api
{
    public static class IoCExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IPersonsService, PersonsService>();
            services.AddTransient<IRepository<Person>, Repository<Person>>();

            services.AddMediatR(System.Reflection.Assembly.GetAssembly(typeof(IoCExtensions)));

            // Zelf toegevoegd en gemaakt
            services.AddTransient<ISongsService, SongsService>();
            services.AddTransient<IRepository<Song>, Repository<Song>>();
        }
    }
}
