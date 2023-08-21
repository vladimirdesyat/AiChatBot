using LLama;
using LLama.Common;

namespace AiChatBot 
{
    internal class Llama
    {
        static string modelPath = AppContext.BaseDirectory + "llama-2-7b-guanaco-qlora.ggmlv3.q5_0.bin";
        public static async Task<string> Ai(string prompt) 
        {
            var ex = new StatelessExecutor(new LLamaModel(new ModelParams(modelPath, contextSize: 256, gpuLayerCount: 20)));
            var inferenceParams = new InferenceParams() { Temperature = 0.6f, AntiPrompts = new List<string> { }, MaxTokens = 50};
            return string.Join("", ex.Infer(prompt.Trim(), inferenceParams));            
        }
    }
}
   
