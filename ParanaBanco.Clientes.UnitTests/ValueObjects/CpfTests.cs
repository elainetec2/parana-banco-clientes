using System;
using System.Collections.Generic;
using System.Text;
using ParanaBanco.Clientes.Domain.ValueObjects;

namespace ParanaBanco.Clientes.UnitTests.ValueObjects
{
    public class CpfTests
    {
        [Theory]
        [InlineData("52998224725")]
        [InlineData("529.982.247-25")]
        public void Construtor_DeveCriarCpf_QuandoValido(string valor)
        {
            // Act
            var cpf = new Cpf(valor);

            // Assert
            Assert.Equal("52998224725", cpf.Valor);
        }

        [Theory]
        [InlineData("11111111111")]
        [InlineData("12345678900")]
        [InlineData("123")]
        [InlineData("abc")]
        [InlineData("529ABC98224725")]
        public void Construtor_DeveLancarExcecao_QuandoCpfInvalido(
            string valor)
        {
            // Act
            var acao = () => new Cpf(valor);

            // Assert
            Assert.Throws<ArgumentException>(acao);
        }

        [Fact]
        public void DoisCpfsComMesmoValor_DevemSerIguais()
        {
            // Arrange
            var cpf1 = new Cpf("52998224725");
            var cpf2 = new Cpf("529.982.247-25");

            // Assert
            Assert.Equal(cpf1, cpf2);
        }
    }
}
