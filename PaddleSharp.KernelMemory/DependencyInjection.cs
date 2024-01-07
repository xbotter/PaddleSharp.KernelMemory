using Microsoft.Extensions.DependencyInjection;
using Microsoft.KernelMemory.DataFormats;
using PaddleSharp.KernelMemory;
using Sdcb.PaddleOCR.Models;

namespace Microsoft.KernelMemory;
public static partial class KernelMemoryBuilderExtensions
{
    /// <summary>
    /// Adds PaddleSharp OCR services to the KernelMemoryBuilder.
    /// </summary>
    /// <param name="builder">The KernelMemoryBuilder to add services to.</param>
    /// <param name="model">The FullOcrModel to use for the OCR engine.</param>
    /// <returns>The same KernelMemoryBuilder for chaining.</returns>

    public static IKernelMemoryBuilder WithPaddleSharpOcr(
               this IKernelMemoryBuilder builder, FullOcrModel model)
    {
        builder.Services.AddPaddleSharpOcr(model);
        return builder;
    }
}

public static partial class DependencyInjection
{
    /// <summary>
    /// Adds a singleton PaddleSharp OCR engine to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <param name="model">The FullOcrModel to use for the OCR engine.</param>
    /// <returns>The same IServiceCollection for chaining.</returns>
    public static IServiceCollection AddPaddleSharpOcr(
        this IServiceCollection services, FullOcrModel model)
    {
        return services
            .AddSingleton<IOcrEngine>(new PaddleSharpOcrEngine(model));
    }
}