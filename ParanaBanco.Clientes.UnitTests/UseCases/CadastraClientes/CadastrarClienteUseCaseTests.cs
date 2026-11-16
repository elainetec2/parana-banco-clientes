using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using ParanaBanco.Clientes.Application.DTOs;
using ParanaBanco.Clientes.Application.Events;
using ParanaBanco.Clientes.Application.Interfaces;
using ParanaBanco.Clientes.Application.UseCases.CadastrarCliente;
using ParanaBanco.Clientes.Domain.Entities;

namespace ParanaBanco.Clientes.UnitTests.UseCases.CadastraClientes
{
    public class CadastrarClienteUseCaseTests
    {
        private readonly Mock<IClienteRepository> _repositoryMock = new();
        private readonly Mock<ICadastroClientePersistence> _persistenceMock = new();

        private CadastrarClienteUseCase CriarUseCase()
        {
            return new CadastrarClienteUseCase(
                _repositoryMock.Object,
                _persistenceMock.Object);
        }

        [Fact]
        public async Task ExecutarAsync_DeveCadastrarCliente_QuandoDadosValidos()
        {
            // Arrange
            var request = new CadastrarClienteRequest(
                "Maria Silva",
                "529.982.247-25",
                "MARIA@EMAIL.COM");

            _repositoryMock
                .Setup(x => x.ExisteCpfAsync(
                    "52998224725",
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var resultado = await CriarUseCase().ExecutarAsync(request);

            // Assert
            Assert.NotEqual(Guid.Empty, resultado.Id);
            Assert.Equal("Maria Silva", resultado.Nome);
            Assert.Equal("52998224725", resultado.Cpf);
            Assert.Equal("maria@email.com", resultado.Email);

            _persistenceMock.Verify(
                x => x.SalvarClienteComEventoAsync(
                    It.Is<Cliente>(c => c.Id == resultado.Id),
                    It.Is<ClienteCadastrado>(e =>
                        e.ClienteId == resultado.Id &&
                        e.Cpf == resultado.Cpf &&
                        e.EventId != Guid.Empty),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task ExecutarAsync_DeveLancarExcecao_QuandoCpfJaExiste()
        {
            // Arrange
            var request = new CadastrarClienteRequest(
                "Maria Silva",
                "52998224725",
                "maria@email.com");

            _repositoryMock
                .Setup(x => x.ExisteCpfAsync(
                    request.Cpf,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var acao = () => CriarUseCase().ExecutarAsync(request);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(acao);

            _persistenceMock.Verify(
                x => x.SalvarClienteComEventoAsync(
                    It.IsAny<Cliente>(),
                    It.IsAny<ClienteCadastrado>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task ExecutarAsync_NaoDevePersistir_QuandoCpfInvalido()
        {
            // Arrange
            var request = new CadastrarClienteRequest(
                "Maria Silva",
                "11111111111",
                "maria@email.com");

            // Act
            var acao = () => CriarUseCase().ExecutarAsync(request);

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(acao);

            _persistenceMock.Verify(
                x => x.SalvarClienteComEventoAsync(
                    It.IsAny<Cliente>(),
                    It.IsAny<ClienteCadastrado>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}
