# AiChatBot
Telegram Chat Bot with Llama.net Ai. Can answer directly in Telegram chat in one session. After each question Llama restarts.

Just start app and type your question in chat. You need bot token to make app work. You can get token from @BotFather, when you create your Telegram bot.

Bot using libraries: Telegram.Bot, Telegram.Bots.Extension.Pollings, Llama.net.

By default bot using ggml model "llama-2-7b-guanaco-qlora.ggmlv3.q5_0.bin", which you can download from resources like Hugging Face.

I'm thinking about adding save session func and database for each session+chat_id in future updates.
