using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Models.ApDbContext;
using TeachersInformationCrawler.Contracts;
using TeachersInformationCrawler.Implementations;

namespace CrawlerJob
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            ConfigureDependencies(containerBuilder);
            var container = containerBuilder.Build();
            var serviceProvider = new AutofacServiceProvider(container);
            Console.WriteLine("Hello World!");


            using (var scope = container.BeginLifetimeScope())
            {
                var crawler = scope.Resolve<ICrawler>();
                await crawler.StartCrawlingAsync();
            }
        }

        static void ConfigureDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<Crawler>().As<ICrawler>();
            builder.Register<APDbContext>((c, p) => new DbContextCreator().CreateDbContext());
        }
    }
}
