using ShiftCodeRedeemer.Services;

namespace ShiftCodeRedeemer.Interface;

public interface INotifyService
{
    Task Notify(CodeModel code, string username, RedemptionResponse message);
}