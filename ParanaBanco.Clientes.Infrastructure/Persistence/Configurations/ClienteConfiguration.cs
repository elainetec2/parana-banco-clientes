using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParanaBanco.Clientes.Domain.Entities;

namespace ParanaBanco.Clientes.Infrastructure.Persistence.Configurations
{
    public sealed class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedNever();

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.CriadoEm)
                .IsRequired();

            builder.OwnsOne(c => c.Cpf, cpf =>
            {
                cpf.Property(p => p.Valor)
                    .HasColumnName("Cpf")
                    .HasMaxLength(11)
                    .IsRequired();

                cpf.HasIndex(p => p.Valor)
                    .IsUnique();
            });

            builder.OwnsOne(c => c.Email, email =>
            {
                email.Property(e => e.Valor)
                    .HasColumnName("Email")
                    .HasMaxLength(254)
                    .IsRequired();
            });

            builder.Navigation(c => c.Cpf).IsRequired();
            builder.Navigation(c => c.Email).IsRequired();
        }
    }
}
