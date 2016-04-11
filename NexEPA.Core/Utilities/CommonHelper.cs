namespace NexEPA.Core.Utilities
{
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
    }
}
