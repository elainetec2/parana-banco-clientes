using System;
using System.Collections.Generic;
using System.Text;

namespace ParanaBanco.Clientes.Application.DTOs
{
    public sealed record CadastrarClienteRequest(
    string Nome,
    string Cpf,
    string Email);
}
