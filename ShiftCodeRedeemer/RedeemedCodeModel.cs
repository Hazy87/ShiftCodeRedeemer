using ShiftCodeRedeemer.Services;

namespace ShiftCodeRedeemer;

public class RedeemedCodeModel
{
    public string Code { get; set; }
    public string Reward { get; set; }
    public string Platform { get; set; }
    public RedemptionResponse RedemptionResponse { get; set; }
    
}