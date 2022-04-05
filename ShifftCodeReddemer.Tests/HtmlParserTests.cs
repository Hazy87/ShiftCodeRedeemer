using ShiftCodeRedeemer.Services;
using Xunit;

namespace ShiftCodeRedemeer.Tests
{
    public class HtmlParserTests
    {
        private readonly HtmlParser _service;

        public HtmlParserTests()
        {
            _service = new HtmlParser();
        }
        [Fact]
        public void GetEntitlementDetails_Should_Return_Correct_Inp()
        {
            var html =
                "      <h2>Tiny Tina&#39;s Wonderlands</h2>\n          <form class=\"new_archway_code_redemption\" id=\"new_archway_code_redemption\" action=\"/code_redemptions\" accept-charset=\"UTF-8\" method=\"post\"><input name=\"utf8\" type=\"hidden\" value=\"&#x2713;\" /><input type=\"hidden\" name=\"authenticity_token\" value=\"3OLEq9j7SY9RUrFGuRJSauxX25ovmsFRjWY/QDFNUs1RDoEb0JfrzfnouvgN21MLvVchsE1fILveDVVwXSlmxQ==\" />\n            <input value=\"BTX3T-6RTWZ-K5BW5-3BBB3-3TFCZ\" type=\"hidden\" name=\"archway_code_redemption[code]\" id=\"archway_code_redemption_code\" />\n            <input value=\"80ee700823675fa94f8c8d4e29fd5199653c43a7478e82ef0532d5c3414ce273\" type=\"hidden\" name=\"archway_code_redemption[check]\" id=\"archway_code_redemption_check\" />\n            <input value=\"xboxlive\" type=\"hidden\" name=\"archway_code_redemption[service]\" id=\"archway_code_redemption_service\" />\n            <input value=\"daffodil\" type=\"hidden\" name=\"archway_code_redemption[title]\" id=\"archway_code_redemption_title\" />\n            <input type=\"submit\" name=\"commit\" value=\"Redeem for Xbox Live\" class=\"submit_button redeem_button\" data-disable-with=\"Redeem for Xbox Live\" />\n</form>          <br>\n";
            (string inp, string form_code, string check, string service) entitlementDetails = _service.GetEntitlementDetails(html);
            Assert.Equal("3OLEq9j7SY9RUrFGuRJSauxX25ovmsFRjWY/QDFNUs1RDoEb0JfrzfnouvgN21MLvVchsE1fILveDVVwXSlmxQ==", entitlementDetails.inp);
        }
        [Fact]
        public void GetEntitlementDetails_Should_Return_Correct_FormCode()
        {
            var html =
                "      <h2>Tiny Tina&#39;s Wonderlands</h2>\n          <form class=\"new_archway_code_redemption\" id=\"new_archway_code_redemption\" action=\"/code_redemptions\" accept-charset=\"UTF-8\" method=\"post\"><input name=\"utf8\" type=\"hidden\" value=\"&#x2713;\" /><input type=\"hidden\" name=\"authenticity_token\" value=\"3OLEq9j7SY9RUrFGuRJSauxX25ovmsFRjWY/QDFNUs1RDoEb0JfrzfnouvgN21MLvVchsE1fILveDVVwXSlmxQ==\" />\n            <input value=\"BTX3T-6RTWZ-K5BW5-3BBB3-3TFCZ\" type=\"hidden\" name=\"archway_code_redemption[code]\" id=\"archway_code_redemption_code\" />\n            <input value=\"80ee700823675fa94f8c8d4e29fd5199653c43a7478e82ef0532d5c3414ce273\" type=\"hidden\" name=\"archway_code_redemption[check]\" id=\"archway_code_redemption_check\" />\n            <input value=\"xboxlive\" type=\"hidden\" name=\"archway_code_redemption[service]\" id=\"archway_code_redemption_service\" />\n            <input value=\"daffodil\" type=\"hidden\" name=\"archway_code_redemption[title]\" id=\"archway_code_redemption_title\" />\n            <input type=\"submit\" name=\"commit\" value=\"Redeem for Xbox Live\" class=\"submit_button redeem_button\" data-disable-with=\"Redeem for Xbox Live\" />\n</form>          <br>\n";
            (string inp, string form_code, string check, string service) entitlementDetails = _service.GetEntitlementDetails(html);
            Assert.Equal("BTX3T-6RTWZ-K5BW5-3BBB3-3TFCZ", entitlementDetails.form_code);
        }
        [Fact]
        public void GetEntitlementDetails_Should_Return_Correct_FormCheck()
        {
            var html =
                "      <h2>Tiny Tina&#39;s Wonderlands</h2>\n          <form class=\"new_archway_code_redemption\" id=\"new_archway_code_redemption\" action=\"/code_redemptions\" accept-charset=\"UTF-8\" method=\"post\"><input name=\"utf8\" type=\"hidden\" value=\"&#x2713;\" /><input type=\"hidden\" name=\"authenticity_token\" value=\"3OLEq9j7SY9RUrFGuRJSauxX25ovmsFRjWY/QDFNUs1RDoEb0JfrzfnouvgN21MLvVchsE1fILveDVVwXSlmxQ==\" />\n            <input value=\"BTX3T-6RTWZ-K5BW5-3BBB3-3TFCZ\" type=\"hidden\" name=\"archway_code_redemption[code]\" id=\"archway_code_redemption_code\" />\n            <input value=\"80ee700823675fa94f8c8d4e29fd5199653c43a7478e82ef0532d5c3414ce273\" type=\"hidden\" name=\"archway_code_redemption[check]\" id=\"archway_code_redemption_check\" />\n            <input value=\"xboxlive\" type=\"hidden\" name=\"archway_code_redemption[service]\" id=\"archway_code_redemption_service\" />\n            <input value=\"daffodil\" type=\"hidden\" name=\"archway_code_redemption[title]\" id=\"archway_code_redemption_title\" />\n            <input type=\"submit\" name=\"commit\" value=\"Redeem for Xbox Live\" class=\"submit_button redeem_button\" data-disable-with=\"Redeem for Xbox Live\" />\n</form>          <br>\n";
            (string inp, string form_code, string check, string service) entitlementDetails = _service.GetEntitlementDetails(html);
            Assert.Equal("80ee700823675fa94f8c8d4e29fd5199653c43a7478e82ef0532d5c3414ce273", entitlementDetails.check);
        }

        [Fact]
        public void GetEntitlementDetails_Should_Return_Correct_FormService()
        {
            var html =
                "      <h2>Tiny Tina&#39;s Wonderlands</h2>\n          <form class=\"new_archway_code_redemption\" id=\"new_archway_code_redemption\" action=\"/code_redemptions\" accept-charset=\"UTF-8\" method=\"post\"><input name=\"utf8\" type=\"hidden\" value=\"&#x2713;\" /><input type=\"hidden\" name=\"authenticity_token\" value=\"3OLEq9j7SY9RUrFGuRJSauxX25ovmsFRjWY/QDFNUs1RDoEb0JfrzfnouvgN21MLvVchsE1fILveDVVwXSlmxQ==\" />\n            <input value=\"BTX3T-6RTWZ-K5BW5-3BBB3-3TFCZ\" type=\"hidden\" name=\"archway_code_redemption[code]\" id=\"archway_code_redemption_code\" />\n            <input value=\"80ee700823675fa94f8c8d4e29fd5199653c43a7478e82ef0532d5c3414ce273\" type=\"hidden\" name=\"archway_code_redemption[check]\" id=\"archway_code_redemption_check\" />\n            <input value=\"xboxlive\" type=\"hidden\" name=\"archway_code_redemption[service]\" id=\"archway_code_redemption_service\" />\n            <input value=\"daffodil\" type=\"hidden\" name=\"archway_code_redemption[title]\" id=\"archway_code_redemption_title\" />\n            <input type=\"submit\" name=\"commit\" value=\"Redeem for Xbox Live\" class=\"submit_button redeem_button\" data-disable-with=\"Redeem for Xbox Live\" />\n</form>          <br>\n";
            (string inp, string form_code, string check, string service) entitlementDetails = _service.GetEntitlementDetails(html);
            Assert.Equal("xboxlive", entitlementDetails.service);
        }
    }
}