namespace NexEPA.Core.Utilities
{
    using System;
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

        public static decimal ConvertDecimal(object value)
        {
            decimal result = 0;

            if (value != null)
            {
                bool isSuccessful = decimal.TryParse(value.ToString(), out result);

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

        public static string StarNumberToPercent(string starNumber)
        {
            decimal percent = CommonHelper.ConvertDecimal(starNumber) * 2 * 10;
            string result = string.Empty;

            if (percent > 0)
            {
                result = percent.ToString();
            }

            return result;
        }

        public static string PercentToStarNumber(string percent)
        {
            decimal starNumber = Math.Round(CommonHelper.ConvertDecimal(percent) / (2 * 10), 1);
            string result = string.Empty;

            if (starNumber > 0)
            {
                result = starNumber.ToString();
            }

            return result;
        }
    }
}
