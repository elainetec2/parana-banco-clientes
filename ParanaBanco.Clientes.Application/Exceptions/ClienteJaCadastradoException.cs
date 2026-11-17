using System;
using System.Collections.Generic;
using System.Text;

namespace ParanaBanco.Clientes.Application.Exceptions
{    public sealed class ClienteJaCadastradoException : Exception
    {
        public ClienteJaCadastradoException()
            : base("Já existe um cliente cadastrado com este CPF.")
        {
        }
    }
}
