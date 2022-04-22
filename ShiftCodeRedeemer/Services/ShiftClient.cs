using System.Text.Json;
using Flurl.Http;
using HtmlAgilityPack;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Logging;
using Polly;
using ShiftCodeRedeemer.Interface;

namespace ShiftCodeRedeemer.Services;

public class ShiftClient : IShiftClient
{
    private readonly IHtmlParser _htmlParser;
    private readonly ILogger<ShiftClient> _logger;
    private string base_url = "https://shift.gearboxsoftware.com";
    private CookieSession _session;

    public ShiftClient(IHtmlParser htmlParser, ILogger<ShiftClient> logger)
    {
        _htmlParser = htmlParser;
        _logger = logger;
        _session = new CookieSession("https://shift.gearboxsoftware.com");
    }
    public async Task<string> GetToken(string path)
    {
        var html = await _session.Request($"{base_url}{path}").GetStringAsync();
        _logger.LogDebug("GetToken got response: {html}", html);
        
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
        var loginResponse = await response.ResponseMessage.Content.ReadAsStringAsync();
        _logger.LogDebug("Login got response {loginResponse}", loginResponse);
        return loginResponse;
    }

    public async Task<RedemptionResponse> GetRedemptionForm(string code, string service)
    {
        var token = await GetToken("/code_redemptions/new");
        var polly = await Policy.Handle<Exception>().WaitAndRetryAsync(4, x => new TimeSpan(0, 0, 30)).ExecuteAndCaptureAsync(async () =>
        {
            var html = await _session.Request($"{base_url}/entitlement_offer_codes?code={code}")
                .WithHeader("x-csrf-token", token)
                .WithHeader("x-requested-with", "XMLHttpRequest").GetStringAsync();
            if (html.Trim() == "This code is not available for your account")
                return RedemptionResponse.NotAvailableForYouAccount;
            if (html.Trim() == "This SHiFT code has expired")
                return RedemptionResponse.Expired;
            var entitlementDetails = _htmlParser.GetEntitlementDetails(html, service);
            if (string.IsNullOrEmpty(entitlementDetails.service))
                return RedemptionResponse.NotAvailableForThisService;
            return await RedeemForm(entitlementDetails.inp, entitlementDetails.form_code, entitlementDetails.check,
                service);
        });
        return polly.Result;

    }

    public async Task<RedemptionResponse> RedeemForm(string inp, string form_code, string check, string service)
    {
        var polly = await Policy.Handle<Exception>().WaitAndRetryAsync(4, x => new TimeSpan(0, 0, 30)).ExecuteAndCaptureAsync(
            async () =>
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
                var redemptionResponse = _htmlParser.GetRedemptionResponse(respstring);
                _logger.LogDebug("RedeemForm got response {respstring}", respstring);

                _logger.LogInformation($"The code : {form_code} gives the message {redemptionResponse}");
                return ResponseMapper.Map(redemptionResponse);
            });
        return polly.Result;
    }
}