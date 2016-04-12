namespace NexEPA.Core.Utilities
{
    using System.IO;
    using Ionic.Zlib;

    public static class CommonHelper
    {
        public static int ConvertInt(object value)
        {
            int result = 0;

            if (value != null)
            {
                bool isSuccessful = int.TryParse(value.ToString(), out result);

                if (!isSuccessful)
                {
                    result = 0;
                }
            }

            return result;
        }

        public static byte[] DeflateByte(byte[] content)
        {
            byte[] zippedContent = null;

            if (content != null)
            {
                using (var output = new MemoryStream())
                {
                    using (var compressor = new DeflateStream(output, CompressionMode.Compress, CompressionLevel.BestSpeed))
                    {
                        compressor.Write(content, 0, content.Length);
                    }

                    zippedContent = output.ToArray();
                }
            }

            return zippedContent;
        }
    }
}
