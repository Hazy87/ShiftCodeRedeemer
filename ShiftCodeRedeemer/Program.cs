
using Microsoft.Extensions.DependencyInjection;
using ShiftCodeRedeemer;
using ShiftCodeRedeemer.Interface;
using ShiftCodeRedeemer.Services;
using Telegram.Bots;

// See https://aka.ms/new-console-template for more information
var botApiToken = Environment.GetEnvironmentVariable("BOTAPITOKEN") ?? throw new ArgumentNullException("Environment.GetEnvironmentVariable(\"BOTAPITOKEN\")");

var serviceProvider = new ServiceCollection()
    .AddSingleton<IShiftCodeGettingService, ShiftCodeGettingService>()
    .AddSingleton<IHandlerService, HandlerService>()
    .AddSingleton<IConfigService, ConfigService>()
    .AddSingleton<INotifyService, NotifyService>()
    .AddSingleton<IRedeemService, RedeemService>()
    .AddSingleton<IRedeemCodeService, RedeemCodeService>()
    .AddSingleton<IRedeemDbService, RedeemedDbService>()
    .AddSingleton<IHtmlParser, HtmlParser>()
    .AddSingleton<IShiftClient, ShiftClient>()
    .AddBotClient(botApiToken).Services
    .BuildServiceProvider();
var service = serviceProvider.GetService<IHandlerService>();
await service.Handle();
