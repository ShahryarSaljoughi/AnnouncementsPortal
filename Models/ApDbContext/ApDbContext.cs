using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Models.Entities;
using Remotion.Linq.Clauses;

namespace Models.ApDbContext
{
    public class APDbContext: DbContext
    {
        public APDbContext()
        {
            
        }


        // used in DbContextCreator
        public APDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var asm = Assembly.GetExecutingAssembly();
            var types = from t in asm.GetTypes()
                where t.IsClass && t.Namespace == "Models.Entities"
                select t;
            foreach (var type in types)
            {
               modelBuilder.Entity(type);
            }
        }
    }
}
