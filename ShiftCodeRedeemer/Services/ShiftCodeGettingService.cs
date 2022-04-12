using Flurl.Http;
using ShiftCodeRedeemer.Interface;
using ShiftCodeRedeemer.Models;

namespace ShiftCodeRedeemer.Services;

public class ShiftCodeGettingService : IShiftCodeGettingService
{
    public async Task<List<OrcicornResponse>> GetCodes(string game)
    {
        return await $"https://shift.orcicorn.com/tags/{game}/index.json".GetJsonAsync<List<OrcicornResponse>>();
    }
}