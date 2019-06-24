using agendamed_backend.Data.EntitiesConfig;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agendamed_backend.Data
{
    public class AgendaDbContext : DbContext
    {
        public AgendaDbContext(DbContextOptions<AgendaDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CompromissoConfig());



            //Define dos as propriedades com a nomeclatura 'ID' de todas as Entidades como primary key
            foreach (var entityType in builder.Model.GetEntityTypes()
            .Where(t => t.ClrType.Name == "ID").ToList())
            {
                builder.Entity(
                    entityType.Name,
                    x =>
                    {
                        x.HasKey("ID");
                        x.Property("ID").ValueGeneratedOnAdd();
                    });
            }

        }

    }
}
