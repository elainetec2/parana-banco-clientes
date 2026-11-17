using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using ParanaBanco.Clientes.Application.Interfaces;
using ParanaBanco.Clientes.Application.UseCases.ObterClientePorId;
using ParanaBanco.Clientes.Domain.Entities;

namespace ParanaBanco.Clientes.UnitTests.UseCases.ObterClientePorId
{
    public class ObterClientePorIdUseCaseTests
    {
        private readonly Mock<IClienteRepository> _repositoryMock = new();

        private ObterClientePorIdUseCase CriarUseCase()
        {
            return new ObterClientePorIdUseCase(
                _repositoryMock.Object);
        }

        [Fact]
        public async Task ExecutarAsync_DeveRetornarCliente_QuandoEncontrado()
        {
            // Arrange
            var cliente = new Cliente(
                "Maria Silva",
                "52998224725",
                "maria@email.com");

            _repositoryMock
                .Setup(x => x.ObterPorIdAsync(
                    cliente.Id,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(cliente);

            // Act
            var resultado = await CriarUseCase().ExecutarAsync(cliente.Id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(cliente.Id, resultado.Id);
            Assert.Equal("Maria Silva", resultado.Nome);
            Assert.Equal("52998224725", resultado.Cpf);
            Assert.Equal("maria@email.com", resultado.Email);

            _repositoryMock.Verify(
                x => x.ObterPorIdAsync(
                    cliente.Id,
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task ExecutarAsync_DeveRetornarNull_QuandoClienteNaoExiste()
        {
            // Arrange
            var clienteId = Guid.NewGuid();

            _repositoryMock
                .Setup(x => x.ObterPorIdAsync(
                    clienteId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((Cliente?)null);

            // Act
            var resultado = await CriarUseCase().ExecutarAsync(clienteId);

            // Assert
            Assert.Null(resultado);

            _repositoryMock.Verify(
                x => x.ObterPorIdAsync(
                    clienteId,
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
