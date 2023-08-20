using LLama;
using LLama.Common;

namespace AiChatBot 
{
    internal class Llama
    {
        static string modelPath = AppContext.BaseDirectory + "llama-2-7b-guanaco-qlora.ggmlv3.q8_0.bin";
        public static async Task<string> Ai(string prompt) 
        {
            var list = new List<string>();
            StatelessExecutor ex = new StatelessExecutor(new LLamaModel(new ModelParams(modelPath, contextSize: 1000, gpuLayerCount: 20)));
            InferenceParams inferenceParams = new InferenceParams() { Temperature = 0.6f, AntiPrompts = new List<string> { }, MaxTokens = 100};
            while (true)
            {
                prompt = prompt.Trim();
                foreach (var text in ex.Infer(prompt, inferenceParams))
                {
                    list.Add(text);
                }
                return string.Join(" ", list.ToArray());
            }
        }
    }
}
   
