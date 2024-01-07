using Microsoft.KernelMemory.DataFormats;
using OpenCvSharp;
using Sdcb.PaddleInference;
using Sdcb.PaddleOCR;
using Sdcb.PaddleOCR.Models;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PaddleSharp.KernelMemory
{
    /// <summary>
    /// Represents an OCR engine using PaddleSharp.
    /// </summary>
    /// <param name="model">PaddleSharp Ocr Model</param>
    public class PaddleSharpOcrEngine(FullOcrModel model) : IOcrEngine
    {
        private readonly FullOcrModel _model = model;

        /// <inheritdoc/>
        public async Task<string> ExtractTextFromImageAsync(Stream imageContent, CancellationToken cancellationToken = default)
        {
            using var all = new PaddleOcrAll(_model, PaddleDevice.Mkldnn());
            using var memoryStream = new MemoryStream();
            await imageContent.CopyToAsync(memoryStream);
            using Mat src = Cv2.ImDecode(memoryStream.ToArray(), ImreadModes.Color);
            PaddleOcrResult result = all.Run(src);
            return result.Text;
        }
    }
}
