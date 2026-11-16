using System;
using System.Collections.Generic;
using System.Text;
using ParanaBanco.Clientes.Domain.ValueObjects;

namespace ParanaBanco.Clientes.UnitTests.ValueObjects
{
    public class EmailTests
    {
        [Theory]
        [InlineData("maria@email.com", "maria@email.com")]
        [InlineData("MARIA@EMAIL.COM", "maria@email.com")]
        [InlineData("  maria@email.com  ", "maria@email.com")]
        public void Construtor_DeveNormalizarEmail_QuandoValido(
            string entrada, string esperado)
        {
            // Act
            var email = new Email(entrada);

            // Assert
            Assert.Equal(esperado, email.Valor);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("emailinvalido")]
        [InlineData("@email.com")]
        [InlineData("maria@")]
        [InlineData("maria email@email.com")]
        public void Construtor_DeveLancarExcecao_QuandoEmailInvalido(
            string entrada)
        {
            // Act
            var acao = () => new Email(entrada);

            // Assert
            Assert.Throws<ArgumentException>(acao);
        }

        [Fact]
        public void DoisEmailsComMesmoValor_DevemSerIguais()
        {
            // Arrange
            var email1 = new Email("MARIA@EMAIL.COM");
            var email2 = new Email("maria@email.com");

            // Assert
            Assert.Equal(email1, email2);
        }
    }
}
