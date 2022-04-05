using ShiftCodeRedeemer.Services;

namespace ShiftCodeRedeemer;

public class CodeModel
{
    public string Code { get; set; }
    public string Reward { get; set; }
    public RedemptionResponse RedemptionResponse { get; set; }
}