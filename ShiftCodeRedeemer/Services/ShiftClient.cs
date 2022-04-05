using System.Text.Json;
using Flurl.Http;
using HtmlAgilityPack;
using ShiftCodeRedeemer.Interface;

namespace ShiftCodeRedeemer.Services;

public class ShiftClient : IShiftClient
{
    private readonly IHtmlParser _htmlParser;
    private string base_url = "https://shift.gearboxsoftware.com";
    private CookieSession _session;

    public ShiftClient(IHtmlParser htmlParser)
    {
        _htmlParser = htmlParser;
        _session = new CookieSession("https://shift.gearboxsoftware.com");
    }
    public async Task<string> GetToken(string path)
    {
        var html = await _session.Request($"{base_url}{path}").GetStringAsync();
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        return doc.DocumentNode.SelectNodes("//meta[@name='csrf-token']")[0].Attributes.Single(x => x.Name == "content").Value;
    }

    public async Task<string> Login(string username, string password)
    {
        var token = await GetToken("/home");
        var response = await _session.Request($"{base_url}/sessions")
            .WithHeader("Referer", $"{base_url}/home")
            .PostMultipartAsync(x =>
            {
                x.AddString("authenticity_token", token)
                    .AddString("user[email]", username)
                    .AddString("user[password]", password)
                    .AddString("commit", "SIGN IN");
            });
        var respstring = await response.ResponseMessage.Content.ReadAsStringAsync();

        return "";
        
    }

    public async Task<string> GetRedemptionForm(string code)
    {
        var token = await GetToken("/code_redemptions/new");
        var html =await  _session.Request($"{base_url}/entitlement_offer_codes?code={code}").WithHeader("x-csrf-token", token)
            .WithHeader("x-requested-with", "XMLHttpRequest").GetStringAsync();

        var entitlementDetails = _htmlParser.GetEntitlementDetails(html);
        await RedeemForm(entitlementDetails.inp, entitlementDetails.form_code, entitlementDetails.check,
            entitlementDetails.service);
        return "";
    }

    public async Task<string> RedeemForm(string inp, string form_code, string check, string service)
    {
        var response = await _session.Request($"{base_url}/code_redemptions")
            .WithHeader("Referer", $"{base_url}/new")
            .PostMultipartAsync(x =>
            {
                x.AddString("authenticity_token", inp);
                x.AddString("archway_code_redemption[code]", form_code);
                x.AddString("archway_code_redemption[check]", check);
                x.AddString("archway_code_redemption[service]", service);
            });
        var respstring = await response.ResponseMessage.Content.ReadAsStringAsync();
        return respstring;
    }
}