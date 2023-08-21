using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace AiChatBot
{
    internal class TelegramBot
    {
        public ITelegramBotClient botClient = new TelegramBotClient("Token");
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {                
                if (update.Message?.Text?.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(update.Message.Chat, "Hi, how can i help you?");
                }
                else if (!string.IsNullOrEmpty(update.Message?.Text) && update.Message.Text.ToLower() != "/start")
                {
                    var input = Llama.Ai(update.Message.Text);
                    await botClient.SendTextMessageAsync(update.Message.Chat, input.Result);
                    input.Dispose();
                    Thread.Sleep(1000);
                }
            }
        }

        public async Task<Task> HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.Error.WriteLine(exception);
            return Task.CompletedTask;
        }
    }
}
