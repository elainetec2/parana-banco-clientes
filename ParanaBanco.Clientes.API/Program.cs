using ParanaBanco.Clientes.Application.UseCases.CadastrarCliente;
using ParanaBanco.Clientes.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Configuração dos controllers
builder.Services.AddControllers();

// Documentação OpenAPI
builder.Services.AddOpenApi();

// Infraestrutura: EF Core, RabbitMQ e MassTransit
builder.Services.AddInfrastructure(builder.Configuration);

// Casos de uso da aplicação
builder.Services.AddScoped<CadastrarClienteUseCase>();

var app = builder.Build();

// Documentação disponível em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
