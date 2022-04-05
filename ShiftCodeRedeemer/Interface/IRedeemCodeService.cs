using ShiftCodeRedeemer.Services;

namespace ShiftCodeRedeemer.Interface;

public interface IRedeemCodeService
{
    Task<RedemptionResponse> Redeem(CodeModel code, Config config);
}