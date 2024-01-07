using Microsoft.KernelMemory;
using Sdcb.PaddleOCR.Models.Local;

var memory = new KernelMemoryBuilder()
    .WithAzureOpenAITextGeneration(new AzureOpenAIConfig()
    {
        APIKey = Env.Var("OPENAI_APIKEY"),
        Endpoint = Env.Var("OPENAI_ENDPOINT"),
        Deployment = "gpt-35-turbo-16k",
        Auth = AzureOpenAIConfig.AuthTypes.APIKey
    })
    .WithAzureOpenAITextEmbeddingGeneration(new AzureOpenAIConfig()
    {
        APIKey = Env.Var("OPENAI_APIKEY"),
        Endpoint = Env.Var("OPENAI_ENDPOINT"),
        Deployment = "text-embedding-ada-002",
        Auth = AzureOpenAIConfig.AuthTypes.APIKey
    })
    .WithPaddleSharpOcr(LocalFullModels.EnglishV3)
    .Build();

await memory.ImportDocumentAsync("./kernel_memory_readme.png");

var question = "What's Kernel Memory?";

var answer = await memory.AskAsync(question);

Console.WriteLine($"Q: {question}");
Console.WriteLine($"A: {answer.Result}");