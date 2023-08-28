using LLama.Common;
using LLama;
using Telegram.Bot.Types;

namespace AiChatBot 
{
    internal class Prompt
    {
        private static string modelPath = Path.Combine(AppContext.BaseDirectory, "llama-2-7b-guanaco-qlora.ggmlv3.q5_0.bin");
        public static StatelessExecutor ex = new StatelessExecutor(new LLamaModel(new ModelParams(modelPath, contextSize: 256, gpuLayerCount: 20)));
        public static InferenceParams inferenceParams = new InferenceParams() { Temperature = 0.6f, AntiPrompts = new List<string> { }, MaxTokens = 50 };
        public static string InitAi(Message message) 
        {          
            SaveSession(new ChatSession(ex), message.Chat.Id); 
            
            return string.Join("", ex.Infer(message.Text.Trim(), inferenceParams));            
        }
        internal static void SaveSession(ChatSession session, long id)
        {
            session.SaveSession(Path.Combine(AppContext.BaseDirectory, $"{id}"));
        }
    }
}
   
