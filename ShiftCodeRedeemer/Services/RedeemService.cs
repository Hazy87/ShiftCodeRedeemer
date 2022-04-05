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
    public async Task Redeem(Config config, List<CodeModel> codesToRedeem)
    {
        var redeemedCodes = await _redeemDbService.GetRedeemedCodes(config);
        foreach (var code in codesToRedeem.Where(x => redeemedCodes.All(y => y.Code != x.Code)))
        {
            var message = await _redeemCodeService.Redeem(code, config);
            
            await _notifyService.Notify(code, config.Username, message);

            await _redeemDbService.Redeemed(code, config.Username);
        }
    }
}