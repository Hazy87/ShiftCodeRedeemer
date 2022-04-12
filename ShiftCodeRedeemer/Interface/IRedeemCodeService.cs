using ShiftCodeRedeemer.Models;
using ShiftCodeRedeemer.Services;

namespace ShiftCodeRedeemer.Interface;

public interface IRedeemCodeService
{
    Task<RedemptionResponse> Redeem(OrcicornCodeModel orcicornCode, Config config, string service);
}