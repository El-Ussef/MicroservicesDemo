// See https://aka.ms/new-console-template for more information

using MassTransit;
using MessageBus.Contract;
using MessageBus.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var serviceProvider = new ServiceCollection()
    .AddSingleton(typeof(INotificationPublisher<>), typeof(NotificationPublisher<>));

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton(typeof(INotificationPublisher<>), typeof(NotificationPublisher<>));
        services.AddMassTransit(ctx =>
        {
            ctx.UsingRabbitMq();
        });

    })
    .Build();

    