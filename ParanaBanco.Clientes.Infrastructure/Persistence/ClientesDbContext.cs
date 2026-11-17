using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ParanaBanco.Clientes.Domain.Entities;
using MassTransit;

namespace ParanaBanco.Clientes.Infrastructure.Persistence
{
    public sealed class ClientesDbContext : DbContext
    {
        public ClientesDbContext(
            DbContextOptions<ClientesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes => Set<Cliente>();

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplica os mapeamentos das entidades do domínio
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ClientesDbContext).Assembly);

            // Configura as tabelas do Transactional Outbox
            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }
    }
}
