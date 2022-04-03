using ShiftCodeRedeemer.Interface;

namespace ShiftCodeRedeemer.Services;

public class HandlerService : IHandlerService
{
    private readonly IShiftCodeGettingService _codeGettingService;
    private readonly IConfigService _configService;
    private readonly IRedeemService _redeemService;

    public HandlerService(IShiftCodeGettingService codeGettingService, IConfigService configService, IRedeemService redeemService)
    {
        _codeGettingService = codeGettingService;
        _configService = configService;
        _redeemService = redeemService;
    }

    public async Task Handle()
    {
        var configs = _configService.GetConfig();
        foreach (var config in configs)
        {
            var codesToRedeem = new List<CodeModel>();
            foreach (var game in config.Games)
            {
                var codes  = await _codeGettingService.GetCodes(game);
                codesToRedeem.AddRange(codes.SelectMany(x => x.Codes));
            }

            await _redeemService.Redeem(config, codesToRedeem);
        }
    }
}