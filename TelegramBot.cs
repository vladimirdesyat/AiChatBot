using DocumentFormat.OpenXml.Office2010.Excel;
using Telegram.Bot;
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
                    _ = DataBase.Query(update.Message.Chat.Id);
                }
                else if (!string.IsNullOrEmpty(update.Message?.Text) && update.Message.Text.ToLower() != "/start")
                {
                    var input = new Prompt();
                    await botClient.SendTextMessageAsync(update.Message.Chat, Prompt.Ai(update.Message.Text).Result);

                    input.Dispose();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
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
