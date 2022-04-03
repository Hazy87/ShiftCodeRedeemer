using ShiftCodeRedeemer.Interface;

namespace ShiftCodeRedeemer.Services;

public class ConfigService : IConfigService
{
    public List<Config> GetConfig()
    {
        var configs = new List<Config>();
        configs.Add(new Config
        {
            Username = Environment.GetEnvironmentVariable("Username"),
            Password = Environment.GetEnvironmentVariable("Password"),
            Games = Environment.GetEnvironmentVariable("Games").Split(",")
        });
        return configs;
    }
}