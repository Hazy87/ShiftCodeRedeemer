using ShiftCodeRedeemer.Models;
using ShiftCodeRedeemer.Services;

namespace ShiftCodeRedeemer.Interface;

public interface INotifyService
{
    Task Notify(OrcicornCodeModel orcicornCode, string username, RedemptionResponse message, string configService);
}