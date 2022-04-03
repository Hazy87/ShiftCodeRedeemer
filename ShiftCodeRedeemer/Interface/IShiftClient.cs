using Flurl.Http;

namespace ShiftCodeRedeemer.Interface;

public interface IShiftClient
{
    Task<string> GetToken(string path);
    Task<string> Login(string username, string password);
    Task<string> GetRedemptionForm(string code);
}