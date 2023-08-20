﻿using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AiChatBot
{
    internal class TelegramBot
    {
        public ITelegramBotClient botClient = new TelegramBotClient("Token");
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {                
                if (message?.Text?.ToLower() == "/start")
                {
                    Console.WriteLine(message.Text);
                    await botClient.SendTextMessageAsync(message.Chat, "Hi, how can i help you?");
                    return;
                }
                else if (message?.Text != null && message.Text.ToLower() != "/start")
                {
                    Console.WriteLine(message.Text);
                    var input = Llama.Ai(message.Text);

                    await botClient.SendTextMessageAsync(message.Chat, input.Result);

                    return;
                }
            }
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonSerializer.Serialize(exception));
        }
    }
}
