using Microsoft.KernelMemory.DataFormats;
using OpenCvSharp;
using Sdcb.OpenVINO.PaddleOCR;
using Sdcb.OpenVINO.PaddleOCR.Models;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaddleSharp.KernelMemory
{
    /// <summary>
    /// Represents an OCR engine using OpenVINO PaddleOCR.
    /// </summary>
    /// <param name="model"></param>
    public class OpenVINOPaddleOCREngine(FullOcrModel model) : IOcrEngine
    {
        private readonly FullOcrModel model = model;

        /// <inheritdoc/>
        public async Task<string> ExtractTextFromImageAsync(Stream imageContent, CancellationToken cancellationToken = default)
        {
            using var all = new PaddleOcrAll(model);
            using var memoryStream = new MemoryStream();
            await imageContent.CopyToAsync(memoryStream);
            using Mat src = Cv2.ImDecode(memoryStream.ToArray(), ImreadModes.Color);
            PaddleOcrResult result = all.Run(src);
            return result.Text;
        }
    }
}
