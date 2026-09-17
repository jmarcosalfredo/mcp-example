using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using leads.api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace leads.api.Configurations.EntityConfigurations
{
    public class LeadDbConfiguration : IEntityTypeConfiguration<Lead>
    {
        public void Configure(EntityTypeBuilder<Lead> builder)
        {
            builder.Property(l => l.NomeCompleto)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(l => l.Email)
                .IsRequired()
                .HasMaxLength(250);
            builder.HasIndex(l => l.Email)
                .IsUnique();

            builder.Property(l => l.Telefone)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(l => l.CondicaoUm)
                .IsRequired();

            builder.Property(l => l.CondicaoDois)
                .IsRequired();
        }
    }
}
