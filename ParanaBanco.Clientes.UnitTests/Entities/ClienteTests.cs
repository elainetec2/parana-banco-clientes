using System;
using System.Collections.Generic;
using System.Text;
using ParanaBanco.Clientes.Domain.Entities;

namespace ParanaBanco.Clientes.UnitTests.Entities
{
    public class ClienteTests
    {
        [Fact]
        public void Construtor_DeveCriarCliente_QuandoDadosValidos()
        {
            // Arrange
            var nome = "Maria Silva";
            var cpf = "529.982.247-25";
            var email = "MARIA@EMAIL.COM";

            // Act
            var cliente = new Cliente(nome, cpf, email);

            // Assert
            Assert.NotEqual(Guid.Empty, cliente.Id);
            Assert.Equal("Maria Silva", cliente.Nome);
            Assert.Equal("52998224725", cliente.Cpf.Valor);
            Assert.Equal("maria@email.com", cliente.Email.Valor);
            Assert.True(cliente.CriadoEm <= DateTime.UtcNow);
            Assert.True(cliente.CriadoEm > DateTime.UtcNow.AddMinutes(-1));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Construtor_DeveLancarExcecao_QuandoNomeInvalido(
            string nome)
        {
            // Act
            var acao = () => new Cliente(
                nome,
                "52998224725",
                "maria@email.com");

            // Assert
            Assert.Throws<ArgumentException>(acao);
        }

        [Fact]
        public void Construtor_DeveLancarExcecao_QuandoCpfInvalido()
        {
            // Act
            var acao = () => new Cliente(
                "Maria Silva",
                "11111111111",
                "maria@email.com");

            // Assert
            Assert.Throws<ArgumentException>(acao);
        }

        [Fact]
        public void Construtor_DeveLancarExcecao_QuandoEmailInvalido()
        {
            // Act
            var acao = () => new Cliente(
                "Maria Silva",
                "52998224725",
                "emailinvalido");

            // Assert
            Assert.Throws<ArgumentException>(acao);
        }

        [Fact]
        public void Construtor_DeveRemoverEspacosDoNome()
        {
            // Act
            var cliente = new Cliente(
                "  Maria Silva  ",
                "52998224725",
                "maria@email.com");

            // Assert
            Assert.Equal("Maria Silva", cliente.Nome);
        }
    }
}
