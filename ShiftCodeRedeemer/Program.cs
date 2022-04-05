
using Microsoft.Extensions.DependencyInjection;
using ShiftCodeRedeemer;
using ShiftCodeRedeemer.Interface;
using ShiftCodeRedeemer.Services;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
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
    .BuildServiceProvider();
var service = serviceProvider.GetService<IHandlerService>();
await service.Handle();
