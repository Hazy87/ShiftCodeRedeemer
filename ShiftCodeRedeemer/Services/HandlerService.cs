using System.Timers;
using Microsoft.Extensions.Logging;
using ShiftCodeRedeemer.Interface;
using ShiftCodeRedeemer.Models;
using Timer = System.Threading.Timer;

namespace ShiftCodeRedeemer.Services;

public class HandlerService : IHandlerService
{
    private readonly IShiftCodeGettingService _codeGettingService;
    private readonly IConfigService _configService;
    private readonly IRedeemService _redeemService;
    private readonly ILogger<HandlerService> _logger;

    public HandlerService(IShiftCodeGettingService codeGettingService, IConfigService configService, IRedeemService redeemService, ILogger<HandlerService> logger)
    {
        _codeGettingService = codeGettingService;
        _configService = configService;
        _redeemService = redeemService;
        _logger = logger;
    }

    public async Task Handle()
    {
        while (true)
        {
            await RunJob();
            await Task.Delay(18000000);
        }
    }

    private async Task RunJob()
    {
        var configs = _configService.GetConfig();
        foreach (var config in configs)
        {
            _logger.LogInformation($"Running for {config.Username}");
            await RedeemForConfig(config);
        }
    }

    private async Task RedeemForConfig(Config config)
    {
        var codesToRedeem = await GetCodesForGames(config);

        await _redeemService.Redeem(config, codesToRedeem);
    }

    private async Task<List<OrcicornCodeModel>> GetCodesForGames(Config config)
    {
        var codesToRedeem = new List<OrcicornCodeModel>();
        foreach (var game in config.Games)
        {
            var codes = await _codeGettingService.GetCodes(game);
            _logger.LogInformation("Working on {game}", game);
            codesToRedeem.AddRange(codes.SelectMany(x => x.Codes));
        }

        return codesToRedeem;
    }
}