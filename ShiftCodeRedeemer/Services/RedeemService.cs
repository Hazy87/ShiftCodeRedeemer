using ShiftCodeRedeemer.Interface;

namespace ShiftCodeRedeemer.Services;

public class RedeemService : IRedeemService
{
    private readonly IRedeemCodeService _redeemCodeService;
    private readonly INotifyService _notifyService;
    private readonly IRedeemDbService _redeemDbService;

    public RedeemService(IRedeemCodeService redeemCodeService, INotifyService notifyService, IRedeemDbService redeemDbService)
    {
        _redeemCodeService = redeemCodeService;
        _notifyService = notifyService;
        _redeemDbService = redeemDbService;
    }
    public async Task Redeem(Config config, List<OrcicornCodeModel> codesToRedeem)
    {
        var redeemedCodes = await _redeemDbService.GetRedeemedCodes(config);
        foreach (var platform in config.Services)
        {
            foreach (var code in codesToRedeem.Where(x => redeemedCodes.All(y => y.Code != x.Code) && (x.Platform == platform || x.Platform == "Universal")))
            {
            

                var redemptionResponse = await _redeemCodeService.Redeem(code, config, platform);

                await _notifyService.Notify(code, config.Username, redemptionResponse, platform);

                await _redeemDbService.Redeemed(code.Code, platform, config.Username, redemptionResponse, code.Reward);
            }
        }
    }
}