namespace ShiftCodeRedeemer.Interface;

public interface IRedeemDbService
{
    Task Redeemed(CodeModel code, string configUsername);
    Task<List<CodeModel>> GetRedeemedCodes(Config config);
}