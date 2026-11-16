using System;
using System.Collections.Generic;
using System.Text;
using ParanaBanco.Clientes.Domain.ValueObjects;

namespace ParanaBanco.Clientes.Domain.Entities
{
    public class Cliente
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public Cpf Cpf { get; private set; } = null!;
        public Email Email { get; private set; } = null!;
        public DateTime CriadoEm { get; private set; }

        private Cliente() { }

        public Cliente(string nome, string cpf, string email)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException(
                    "Nome é obrigatório.", nameof(nome));

            Id = Guid.NewGuid();
            Nome = nome.Trim();
            Cpf = new Cpf(cpf);
            Email = new Email(email);
            CriadoEm = DateTime.UtcNow;
        }
    }
}
