using Flurl.Http;
using ShiftCodeRedeemer.Services;

namespace ShiftCodeRedeemer.Interface;

public interface IShiftClient
{
    Task<string> GetToken(string path);
    Task<string> Login(string username, string password);
    Task<RedemptionResponse> GetRedemptionForm(string code, string configServices);
}