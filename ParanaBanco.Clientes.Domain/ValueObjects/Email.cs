using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace ParanaBanco.Clientes.Domain.ValueObjects
{
    public sealed record Email
    {
        public string Valor { get; }

        public Email(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException(
                    "E-mail é obrigatório.", nameof(valor));

            var email = valor.Trim();

            if (!EhValido(email))
                throw new ArgumentException(
                    "E-mail inválido.", nameof(valor));

            Valor = email.ToLowerInvariant();
        }

        private static bool EhValido(string email)
        {
            if (email.Any(char.IsWhiteSpace))
                return false;

            if (!MailAddress.TryCreate(email, out var endereco))
                return false;

            return endereco.Address.Equals(
                email,
                StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString() => Valor;
    }
}
