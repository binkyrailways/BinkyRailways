namespace BinkyRailways.WinApp.Utils
{
    internal static class MathUtil
    {
        /// <summary>
        /// Calculate the sum of all values between start and start + length - 1
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        internal static int Sum(int[] values, int start, int length)
        {
            int sum = 0;
            for (int i = 0; i < length; i++)
            {
                sum += values[start + i];
            }
            return sum;
        }

        /// <summary>
        /// Calculate the sum of all values between start and start + length - 1
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        internal static float Sum(float[] values, int start, int length)
        {
            float sum = 0;
            for (int i = 0; i < length; i++)
            {
                sum += values[start + i];
            }
            return sum;
        }

        /// <summary>
        /// Calculate the sum of all values between start and start + length - 1
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        internal static long Sum(long[] values, int start, int length)
        {
            long sum = 0;
            for (int i = 0; i < length; i++)
            {
                sum += values[start + i];
            }
            return sum;
        }

        /// <summary>
        /// Calculate the sum of all values between start and start + length - 1
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        internal static double Sum(double[] values, int start, int length)
        {
            double sum = 0;
            for (int i = 0; i < length; i++)
            {
                sum += values[start + i];
            }
            return sum;
        }

        /// <summary>
        /// Calculate the sum of all values 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        internal static int Sum(int[] values) { return Sum(values, 0, values.Length); }

        /// <summary>
        /// Calculate the sum of all values 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        internal static float Sum(float[] values) { return Sum(values, 0, values.Length); }

        /// <summary>
        /// Calculate the sum of all values 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        internal static long Sum(long[] values) { return Sum(values, 0, values.Length); }

        /// <summary>
        /// Calculate the sum of all values 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        internal static double Sum(double[] values) { return Sum(values, 0, values.Length); }
    }
}
