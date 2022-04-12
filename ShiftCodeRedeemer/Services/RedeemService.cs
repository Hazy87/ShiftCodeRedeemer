using Microsoft.Extensions.Logging;
using ShiftCodeRedeemer.Interface;
using ShiftCodeRedeemer.Models;

namespace ShiftCodeRedeemer.Services;

public class RedeemService : IRedeemService
{
    private readonly IRedeemCodeService _redeemCodeService;
    private readonly INotifyService _notifyService;
    private readonly IRedeemDbService _redeemDbService;
    private readonly ILogger<RedeemService> _logger;

    public RedeemService(IRedeemCodeService redeemCodeService, INotifyService notifyService, IRedeemDbService redeemDbService, ILogger<RedeemService> logger)
    {
        _redeemCodeService = redeemCodeService;
        _notifyService = notifyService;
        _redeemDbService = redeemDbService;
        _logger = logger;
    }
    public async Task Redeem(Config config, List<OrcicornCodeModel> codesToRedeem)
    {
        var redeemedCodes = await _redeemDbService.GetRedeemedCodes(config);
        foreach (var platform in config.Services)
        {
            var codesLeftToRedeem = codesToRedeem.Where(x => redeemedCodes.All(y => y.Code != x.Code) && (x.Platform == platform || x.Platform == "Universal"));
            if (!codesLeftToRedeem.Any())
            {
                _logger.LogInformation("No codes to try for {service}", platform);
                continue;
            }

            _logger.LogInformation("Going to try and redeem {codesToRedeem}", codesLeftToRedeem.Select(x => x.Code));
            foreach (var code in codesLeftToRedeem)
            {

                var redemptionResponse = await _redeemCodeService.Redeem(code, config, platform);

                await _notifyService.Notify(code, config.Username, redemptionResponse, platform);

                await _redeemDbService.Redeemed(code.Code, platform, config.Username, redemptionResponse, code.Reward);
            }
        }
    }
}