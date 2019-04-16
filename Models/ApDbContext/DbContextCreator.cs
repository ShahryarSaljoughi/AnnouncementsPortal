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
    class DbContextCreator: IDesignTimeDbContextFactory<ApDbContext>
    {
        public ApDbContext CreateDbContext(string[] args)
        {
            var folderPath = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(folderPath, "appsettings.json");
            var connectionString =
                new ConfigurationBuilder().AddJsonFile(filePath).Build()["ConnectionStrings:Local"];
            
            var optionsBuilder = new DbContextOptionsBuilder<ApDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new ApDbContext(optionsBuilder.Options);
            
        }
    }
}
