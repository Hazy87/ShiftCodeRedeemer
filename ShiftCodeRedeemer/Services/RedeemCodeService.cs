using ShiftCodeRedeemer.Interface;

namespace ShiftCodeRedeemer.Services;

public class RedeemCodeService : IRedeemCodeService
{
    private readonly IShiftClient _shiftClient;

    public RedeemCodeService(IShiftClient shiftClient)
    {
        _shiftClient = shiftClient;
    }
    public async Task<RedemptionResponse> Redeem(OrcicornCodeModel orcicornCode, Config config, string service)
    {
        await _shiftClient.Login(config.Username, config.Password);
        return await _shiftClient.GetRedemptionForm(orcicornCode.Code, service);
    }
}