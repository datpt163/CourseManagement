using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTCourseManagement.Application
{
    public static class Extension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //services.AddCommands();
            //services.AddSingleton<ISampleEntityFactory, SampleEntityFactory>();

            //services.Scan(b => b.FromAssemblies(typeof(ISampleEntityItemsPolicy).Assembly)
            //    .AddClasses(c => c.AssignableTo<ISampleEntityItemsPolicy>())
            //    .AsImplementedInterfaces()
            //    .WithSingletonLifetime());

            return services;
        }
    }

}
