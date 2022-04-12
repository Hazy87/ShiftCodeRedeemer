using ShiftCodeRedeemer.Models;

namespace ShiftCodeRedeemer.Interface;

public interface IRedeemService
{
    Task Redeem(Config config, List<OrcicornCodeModel> codesToRedeem);
}