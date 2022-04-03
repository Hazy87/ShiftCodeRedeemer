using ShiftCodeRedeemer.Interface;

namespace ShiftCodeRedeemer.Services;

public class RedeemCodeService : IRedeemCodeService
{
    private readonly IShiftClient _shiftClient;

    public RedeemCodeService(IShiftClient shiftClient)
    {
        _shiftClient = shiftClient;
    }
    public async Task Redeem(CodeModel code, Config config)
    {
        await _shiftClient.Login(config.Username, config.Password);
        
        await _shiftClient.GetRedemptionForm(code.Code);
    }
}