using Microsoft.AspNetCore.ResponseCompression;
using System.IO;
using System.IO.Compression;

namespace Project.WebApi.Helpers
{
    /// <summary>
    /// Brotli algorithm to handle API response compression to improve the requests performance.
    /// </summary>
    public class BrotliCompressorProvider : ICompressionProvider
    {
        public string EncodingName => "br";
        public bool SupportsFlush => true;

        public Stream CreateStream(Stream outputStream)
            => new BrotliStream(outputStream, CompressionLevel.Optimal, true);
    }
}
