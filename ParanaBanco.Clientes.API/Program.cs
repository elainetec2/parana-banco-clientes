using ParanaBanco.Clientes.Application.UseCases.CadastrarCliente;
using ParanaBanco.Clientes.Application.UseCases.ObterClientePorId;
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
builder.Services.AddScoped<ObterClientePorIdUseCase>();

var app = builder.Build();

// Documentação disponível em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/openapi/v1.json",
            "Paraná Banco - Clientes API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
