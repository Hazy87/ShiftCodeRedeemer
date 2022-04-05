namespace ShiftCodeRedeemer.Services;

public class ResponseMapper
{
    public static RedemptionResponse Map(string response)
    {
        switch (response)
        {
            case "This SHiFT code has already been redeemed":
                return RedemptionResponse.AlreadyRedeemed;
            case "To continue to redeem SHiFT codes, please launch a SHiFT-enabled title first!":
                return RedemptionResponse.EnableShiftTitleFirst;
            default:
                return RedemptionResponse.Other;
        }
    }
}