using Telegram.Bot;
using Telegram.Bot.Types;

namespace AiChatBot
{
    internal class TelegramBot
    {
        public ITelegramBotClient botClient = new TelegramBotClient("Token");
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type != Telegram.Bot.Types.Enums.UpdateType.Message
                || string.IsNullOrWhiteSpace(update.Message?.Text))
                return;
            if (update.Message.Text.ToLower() == "/start")
            {
                await botClient.SendTextMessageAsync(update.Message.Chat, "Hi, how can i help you?");
                await DataBase.Query(update.Message.Chat.Id);
            }
            else 
            {
                await botClient.SendTextMessageAsync(
                    update.Message.Chat, Prompt.InitAi(update.Message));
            }
        
        }

        public async Task<Task> HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.Error.WriteLine(exception);
            return Task.CompletedTask;
        }
    }
}
