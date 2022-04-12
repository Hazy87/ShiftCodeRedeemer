using ShiftCodeRedeemer.Models;
using ShiftCodeRedeemer.Services;

namespace ShiftCodeRedeemer.Interface;

public interface IRedeemDbService
{
    Task Redeemed(string code, string platform, string configUsername, RedemptionResponse redemptionResponse,
        string reward);
    Task<List<RedeemedCodeModel>> GetRedeemedCodes(Config config);
}