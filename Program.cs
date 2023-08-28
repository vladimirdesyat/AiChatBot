using Telegram.Bot;
using Telegram.Bot.Polling;

namespace AiChatBot
{
    class Program
    {
        static async Task Main()
        {
            var telegramBot = new TelegramBot();

            var firstName = (await telegramBot.botClient.GetMeAsync()).FirstName;
            
            Console.WriteLine($"Started bot {firstName}");

            await DataBase.CreateTable();

            telegramBot.botClient.StartReceiving(
                telegramBot.HandleUpdateAsync,
                telegramBot.HandleErrorAsync,
                new ReceiverOptions
                {
                    AllowedUpdates = { }
                },
                new CancellationTokenSource().Token
            );           

            Console.ReadLine();
        }
    }
}
