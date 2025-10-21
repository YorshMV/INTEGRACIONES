using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using IntegrationAppFullStaticToken.Services;
using IntegrationAppFullStaticToken.Workers;

Host.CreateDefaultBuilder(args)
    .UseWindowsService() 
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<IBsaleService, BsaleService>();
        services.AddSingleton<IBsaleBoletaServiceV2, BsaleBoletaServiceV2>();
        services.AddHostedService<IntegrationWorker>();
        services.AddHttpClient<OAuthService>();
        services.AddHttpClient<BsaleService>();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
        logging.AddEventLog(settings =>
        {
            settings.SourceName = "IntegrationBsaleSap";
        });
    })
    .Build()
    .Run();
