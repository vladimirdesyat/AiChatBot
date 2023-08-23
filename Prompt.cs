using LLama.Common;
using LLama;
using Telegram.Bot.Types;
using System;
using Telegram.Bot.Requests;

namespace AiChatBot 
{
    internal class Prompt : IDisposable
    {
        public static string modelPath = Path.Combine(AppContext.BaseDirectory, "llama-2-7b-guanaco-qlora.ggmlv3.q5_0.bin");
        public static StatelessExecutor ex = new StatelessExecutor(new LLamaModel(new ModelParams(modelPath, contextSize: 256, gpuLayerCount: 20)));
        public static InferenceParams inferenceParams = new InferenceParams() { Temperature = 0.6f, AntiPrompts = new List<string> { }, MaxTokens = 50 };
        public async Task<string> Ai(Message message) 
        {
            ChatSession session = new ChatSession(ex);            
            await SaveSession(session, message.Chat.Id);            
            return string.Join("", ex.Infer(message.Text.Trim(), inferenceParams));            
        }

        public static async Task SaveSession(ChatSession session, long id)
        {
            session.SaveSession(Path.Combine(AppContext.BaseDirectory, $"{id}"));
        }

        public void Dispose()
        {
            DisposeManagedResources();
            Console.WriteLine("LlamaSharp has been deleted.");
        }

        protected virtual void DisposeManagedResources() { }
    }
}
   
