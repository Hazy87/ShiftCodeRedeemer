using ShiftCodeRedeemer.Services;

namespace ShiftCodeRedeemer.Interface;

public interface IRedeemDbService
{
    Task Redeemed(CodeModel code, string configUsername, RedemptionResponse redemptionResponse);
    Task<List<CodeModel>> GetRedeemedCodes(Config config);
}