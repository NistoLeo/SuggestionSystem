using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuggestionSystem.Application;
using SuggestionSystem.Application.Services;
using SuggestionSystem.Data;
using SuggestionSystem.ExternalService;
using SuggestionSystem.PublishedLanguage.Commands;
using SuggestionSystem.PublishedLanguage.Events;
using SuggestionSystem.Data;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SuggestionSystem
{
    class Program
    {
        static IConfiguration Configuration;
        static async Task Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            
            var services = new ServiceCollection();
            var source = new CancellationTokenSource();
            var cancellationToken = source.Token;
            services.RegisterBusinessServices(Configuration);
            services.AddPaymentDataAccess(Configuration);

            services.Scan(scan => scan
                .FromAssemblyOf<GetSuggestions>()
                .AddClasses(classes => classes.AssignableTo<IValidator>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));
            services.AddScoped(typeof(IRequestPreProcessor<>), typeof(ValidationPreProcessor<>));

            services.AddScopedContravariant<INotificationHandler<INotification>, AllEventsHandler>(typeof(AccountMade).Assembly);
            services.AddMediatR(new[] { typeof(GetSuggestions).Assembly, typeof(AllEventsHandler).Assembly });
            services.AddSingleton(Configuration);


            var serviceProvider = services.BuildServiceProvider();
            var database = serviceProvider.GetRequiredService<AfterhillsContext>();
            var ibanService = serviceProvider.GetRequiredService<NewIban>();
            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var makeAccountDetails = new MakeNewAccount
            {
                UniqueIdentifier = "23",
                AccountType = "Debit",
                Valuta = "Eur"
            };


            await mediator.Send(makeAccountDetails, cancellationToken);
        }
    }
}
