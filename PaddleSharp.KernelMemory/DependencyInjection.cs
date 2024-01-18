using Microsoft.Extensions.DependencyInjection;
using Microsoft.KernelMemory.DataFormats;
using PaddleSharp.KernelMemory;


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
               this IKernelMemoryBuilder builder, Sdcb.PaddleOCR.Models.FullOcrModel model)
    {
        builder.Services.AddPaddleSharpOcr(model);
        return builder;
    }


    /// <summary>
    /// Add OpenVINO OCR services to the KernelMemoryBuilder.
    /// </summary>
    public static IKernelMemoryBuilder WithOpenVINOPaddleOcr(
                      this IKernelMemoryBuilder builder, Sdcb.OpenVINO.PaddleOCR.Models.FullOcrModel model)
    {
        builder.Services.AddOpenVINOPaddleOcr(model);
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
        this IServiceCollection services, Sdcb.PaddleOCR.Models.FullOcrModel model)
    {
        return services
            .AddSingleton<IOcrEngine>(new PaddleSharpOcrEngine(model));
    }

    /// <summary>
    /// Adds a singleton OpenVINO OCR engine to the service collection.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    public static IServiceCollection AddOpenVINOPaddleOcr(
               this IServiceCollection services, Sdcb.OpenVINO.PaddleOCR.Models.FullOcrModel model)
    {
        return services
            .AddSingleton<IOcrEngine>(new OpenVINOPaddleOCREngine(model));
    }
}