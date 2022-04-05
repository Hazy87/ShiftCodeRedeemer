using ShiftCodeRedeemer.Interface;
using Telegram.Bots;
using Telegram.Bots.Requests;

namespace ShiftCodeRedeemer.Services;

public class NotifyService : INotifyService
{
    private readonly IBotClient _botClient;

    public NotifyService(IBotClient botClient)
    {
        _botClient = botClient;
    }
    public async Task Notify(CodeModel code, string username, RedemptionResponse message)
    {
        await Send($"Tried to redeem {code.Code} for {code.Reward} got {message.ToString()}", int.Parse(Environment.GetEnvironmentVariable("ChatId")));
    }

    public async Task Send(string myMessage, long chatId)
    {
        SendText request = new(chatId: chatId, text: myMessage);

        var response = await _botClient.HandleAsync(request);

        if (response.Ok)
        {
            var message = response.Result;

            Console.WriteLine(message.Id);
            Console.WriteLine(message.Text);
            Console.WriteLine(message.Date.ToString("G"));
        }
        else
        {
            var failure = response.Failure;

            Console.WriteLine(failure.Description);
        }
    }
}