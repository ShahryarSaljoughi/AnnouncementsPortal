using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Models.ApDbContext
{
    public class DbContextCreator: IDesignTimeDbContextFactory<APDbContext>
    {
        public APDbContext CreateDbContext(string[] args)
        {
            var folderPath = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(folderPath, "appsettings.json");
            var connectionString =
                new ConfigurationBuilder().AddJsonFile(filePath).Build()["ConnectionStrings:Local"];
            
            var optionsBuilder = new DbContextOptionsBuilder<APDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new APDbContext(optionsBuilder.Options);
            
        }
        public APDbContext CreateDbContext()
        {
            var folderPath = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(folderPath, "appsettings.json");
            var connectionString =
                new ConfigurationBuilder().AddJsonFile(filePath).Build()["ConnectionStrings:Local"];

            var optionsBuilder = new DbContextOptionsBuilder<APDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new APDbContext(optionsBuilder.Options);

        }
    }
}
