using agendamed_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agendamed_backend.Data.EntitiesConfig
{
    public class CompromissoConfig : IEntityTypeConfiguration<Compromisso>
    {

        public void Configure(EntityTypeBuilder<Compromisso> builder)
        {
           
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Nascimento).IsRequired();
            builder.Property(x => x.Inicio).IsRequired();
            builder.Property(x => x.Fim).IsRequired();
            builder.Property(x => x.Observacao).HasMaxLength(5000);

            builder.HasIndex(x => new { x.Inicio, x.Fim });

        }
    }
}
