namespace ShiftCodeRedeemer.Interface;

public interface IRedeemCodeService
{
    Task<string> Redeem(CodeModel code, Config config);
}