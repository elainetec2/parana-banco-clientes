using System;
using System.Collections.Generic;
using System.Text;

namespace ParanaBanco.Clientes.Application.DTOs
{
    public sealed record ClienteResponse(
    Guid Id,
    string Nome,
    string Cpf,
    string Email,
    DateTime CriadoEm);
}
