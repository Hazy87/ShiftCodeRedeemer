using ShiftCodeRedeemer.Interface;
using ShiftCodeRedeemer.Models;
using System.Text.Json;

namespace ShiftCodeRedeemer.Services;

public class RedeemedDbService : IRedeemDbService
{
    private ConfigModel _config;

    public async Task Redeemed(string code, string platform, string configUsername,
        RedemptionResponse redemptionResponse, string reward)
    {
        var db = await GetDb();
        if (!db.UserCodes.ContainsKey(configUsername))
            db.UserCodes[configUsername] = new List<RedeemedCodeModel>();

        if (db.UserCodes[configUsername].Count(x => x.Code != code && x.Platform != platform) == 0)
            db.UserCodes[configUsername].Add(new RedeemedCodeModel{RedemptionResponse = redemptionResponse,Code = code, Platform = platform, Reward = reward});
        else
            db.UserCodes[configUsername].Single(x => x.Code == code && x.Platform == platform).RedemptionResponse = redemptionResponse;
        await SaveDb(db);
    }

    private async Task SaveDb(ConfigModel db)
    {
        File.WriteAllText("Redeemed.json", JsonSerializer.Serialize(db));
    }

    private async Task<ConfigModel> GetDb()
    {
        if (_config != null)
            return await Task.FromResult(_config);
        if (!File.Exists("Redeemed.json"))
            File.WriteAllText("Redeemed.json",
                JsonSerializer.Serialize(new ConfigModel() { UserCodes = new Dictionary<string, List<RedeemedCodeModel>>() }));
        using var stream = File.OpenRead("Redeemed.json");
        _config = await JsonSerializer.DeserializeAsync<ConfigModel>(stream);
        return _config;
    }

    public async Task<List<RedeemedCodeModel>> GetRedeemedCodes(Config config)
    {
        var db = await GetDb();
        if (db.UserCodes.ContainsKey(config.Username))
            return db.UserCodes[config.Username].Where(x => x.RedemptionResponse != RedemptionResponse.Unknown && x.RedemptionResponse != RedemptionResponse.Other).ToList();
        return new List<RedeemedCodeModel>();
    }
}