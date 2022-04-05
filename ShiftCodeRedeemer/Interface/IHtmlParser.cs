namespace ShiftCodeRedeemer.Interface;

public interface IHtmlParser
{
    (string inp, string form_code, string check, string service) GetEntitlementDetails(string html);
    string GetRedemptionResponse(string html);
}