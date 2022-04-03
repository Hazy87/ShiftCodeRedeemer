namespace ShiftCodeRedeemer.Interface;

public interface IRedeemCodeService
{
    Task Redeem(CodeModel code, Config config);
}