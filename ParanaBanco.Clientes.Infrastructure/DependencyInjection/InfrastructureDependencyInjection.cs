using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParanaBanco.Clientes.Application.Interfaces;
using ParanaBanco.Clientes.Infrastructure.Persistence;
using ParanaBanco.Clientes.Infrastructure.Persistence.Respositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParanaBanco.Clientes.Infrastructure.DependencyInjection
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ClientesDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("ClientesDb")));

            services.AddMassTransit(x =>
            {
                x.AddEntityFrameworkOutbox<ClientesDbContext>(o =>
                {
                    o.UseSqlServer();
                    o.UseBusOutbox();
                });

                x.AddOptions<MassTransitHostOptions>()
                 .Configure(options =>
                 {
                     options.WaitUntilStarted = false;
                 });

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddScoped<IClienteRepository, ClienteRepository>();

            services.AddScoped<
                ICadastroClientePersistence,
                CadastroClientePersistence>();

            return services;
        }
    }
}
