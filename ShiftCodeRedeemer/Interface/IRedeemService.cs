namespace ShiftCodeRedeemer.Interface;

public interface IRedeemService
{
    Task Redeem(Config config, List<CodeModel> codesToRedeem);
}