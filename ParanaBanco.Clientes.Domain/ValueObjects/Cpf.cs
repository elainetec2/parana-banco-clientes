using System;
using System.Collections.Generic;
using System.Text;

namespace ParanaBanco.Clientes.Domain.ValueObjects
{
    public sealed record Cpf
    {
        public string Valor { get; }

        public Cpf(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException("CPF é obrigatório.", nameof(valor));
                        
            var entrada = valor.Trim();

            var cpf = entrada.Length == 14
                   && entrada[3] == '.'
                   && entrada[7] == '.'
                   && entrada[11] == '-'
                   ? entrada.Replace(".", "").Replace("-", "")
                   : entrada;

            if (cpf.Length != 11 ||
                    cpf.Any(c => c < '0' || c > '9') ||
                    !EhValido(cpf))
                throw new ArgumentException("CPF inválido.", nameof(valor));

            Valor = cpf;
        }

        private static bool EhValido(string cpf)
        {
            if (cpf.Length != 11)
                return false;

            if (cpf.Distinct().Count() == 1)
                return false;

            for (int digito = 9; digito < 11; digito++)
            {
                int soma = 0;

                for (int i = 0; i < digito; i++)
                    soma += (cpf[i] - '0') * (digito + 1 - i);

                int resto = (soma * 10) % 11;

                if (resto == 10)
                    resto = 0;

                if (resto != cpf[digito] - '0')
                    return false;
            }

            return true;
        }

        public override string ToString() => Valor;
    }
}
