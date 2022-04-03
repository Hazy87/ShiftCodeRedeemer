namespace ShiftCodeRedeemer.Interface;

public interface INotifyService
{
    Task Notify(CodeModel code, string configUsername);
}