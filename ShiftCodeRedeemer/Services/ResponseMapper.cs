namespace ShiftCodeRedeemer.Services;

public class ResponseMapper
{
    public static RedemptionResponse Map(string response)
    {
        switch (response)
        {
            case "This SHiFT code has already been redeemed":
                return RedemptionResponse.AlreadyRedeemed;
            default:
                return RedemptionResponse.Other;
        }
    }
}