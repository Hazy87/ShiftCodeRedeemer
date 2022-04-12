using ShiftCodeRedeemer.Models;

namespace ShiftCodeRedeemer.Interface;

public interface IShiftCodeGettingService
{
    Task<List<OrcicornResponse>> GetCodes(string game);
}