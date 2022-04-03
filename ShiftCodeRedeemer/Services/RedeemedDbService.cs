using System.Text.Json;
using ShiftCodeRedeemer.Interface;
using ShiftCodeRedeemer.Models;

namespace ShiftCodeRedeemer.Services;

public class RedeemedDbService : IRedeemDbService
{
    private ConfigModel _config;

    public RedeemedDbService()
    {
        
    }

    public async Task Redeemed(CodeModel code, string configUsername)
    {
        var db = await GetDb();
        if (!db.UserCodes.ContainsKey(configUsername))
            db.UserCodes[configUsername] = new List<CodeModel>();
        
        if (db.UserCodes[configUsername].All(x => x.Code != code.Code))
            db.UserCodes[configUsername].Add(code);
    }

    private async Task<ConfigModel> GetDb()
    {
        if (_config != null)
            return await Task.FromResult(_config);
        if (!File.Exists("Redeemed.json"))
            File.WriteAllText("Redeemed.json", JsonSerializer.Serialize(new ConfigModel(){UserCodes = new Dictionary<string, List<CodeModel>>()}));
        using FileStream stream = File.OpenRead("Redeemed.json");
        _config = await JsonSerializer.DeserializeAsync<ConfigModel>(stream);
        return _config;
    }

    public async Task<List<CodeModel>> GetRedeemedCodes(Config config)
    {
        var db = await GetDb();
        if (db.UserCodes.ContainsKey(config.Username))
            return db.UserCodes[config.Username];
        return new List<CodeModel>();
    }
}