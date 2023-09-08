using RabbitWorker.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<ReceiverWorker>();
    })
    .Build();

host.Run();