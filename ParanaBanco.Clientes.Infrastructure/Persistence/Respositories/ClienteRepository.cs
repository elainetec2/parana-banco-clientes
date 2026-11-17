using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ParanaBanco.Clientes.Application.Interfaces;
using ParanaBanco.Clientes.Domain.Entities;

namespace ParanaBanco.Clientes.Infrastructure.Persistence.Respositories
{
    public sealed class ClienteRepository : IClienteRepository
    {
        private readonly ClientesDbContext _context;

        public ClienteRepository(ClientesDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(
            Cliente cliente,
            CancellationToken cancellationToken = default)
        {
            await _context.Clientes.AddAsync(
                cliente,
                cancellationToken);
        }

        public async Task<Cliente?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    c => c.Id == id,
                    cancellationToken);
        }

        public async Task<bool> ExisteCpfAsync(
            string cpf,
            CancellationToken cancellationToken = default)
        {
            return await _context.Clientes
                .AnyAsync(
                    c => c.Cpf.Valor == cpf,
                    cancellationToken);
        }
    }
}
