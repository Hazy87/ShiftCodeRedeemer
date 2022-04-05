using HtmlAgilityPack;
using ShiftCodeRedeemer.Interface;

namespace ShiftCodeRedeemer.Services;

public class HtmlParser : IHtmlParser
{
    public (string inp, string form_code, string check, string service) GetEntitlementDetails(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var inp = doc.DocumentNode.SelectNodes("//input[@name='authenticity_token']").Single().Attributes["value"].Value;
        var form_code = doc.DocumentNode.SelectNodes("//input[@id='archway_code_redemption_code']").Single().Attributes["value"].Value;
        var check = doc.DocumentNode.SelectNodes("//input[@id='archway_code_redemption_check']").Single().Attributes["value"].Value;
        var service = doc.DocumentNode.SelectNodes("//input[@id='archway_code_redemption_service']").Single().Attributes["value"].Value;
        return (inp,form_code,check,service);
    }

    public string GetRedemptionResponse(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        return doc.DocumentNode.SelectNodes("//div[@class='alert notice']").Single().ChildNodes[1].InnerText;
    }
}