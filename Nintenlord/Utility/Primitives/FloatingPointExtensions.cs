using System;
using System.Collections.Generic;

namespace Nintenlord.Utility.Primitives
{
    public static class FloatingPointExtensions
    {
        public static IEnumerable<int> GetIntegersBetween(float min, float max)
        {
            if (min > max)
            {
                throw new ArgumentException("min is larger than max");
            }
            min = (float)Math.Ceiling(min);
            max = (float)Math.Floor(max);
            int minI = (int)min;
            int maxI = (int)max;

            for (int i = minI; i <= maxI; i++)
            {
                yield return i;
            }
        }

        public static bool IsInRange(this float val, float min, float max)
        {
            return val >= min && val <= max;
        }

        public static IEnumerable<float> GetFloats(int n)
        {
            for (int i = 0; i <= n; i++)
            {
                yield return i / (float)n;
            }
        }

        public static double Lerp(double a, double b, double t)
        {
            return a + t * (b - a);
        }

        public static float Lerp(float a, float b, float t)
        {
            return a + t * (b - a);
        }

        public static float Clamp(this float value, float min, float max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(min), "Min > max");
            }
            return min > value ? min : max < value ? max : value;
        }

        public static double Clamp(this double value, double min, double max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(min), "Min > max");
            }
            return min > value ? min : max < value ? max : value;
        }

        public static double SigmoidRP(this double value, double min, double max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(min), "Min > max");
            }
            var exponent = Math.Exp(value * 10 - 5);

            var sigmoid = exponent / (1 + exponent);

            return min + sigmoid * (max - min);
        }

        public static float SigmoidRP(this float value, float min, float max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(min), "Min > max");
            }
            var exponent = (float)Math.Exp(value * 10 - 5);

            var sigmoid = exponent / (1 + exponent);

            return min + sigmoid * (max - min);
        }
    }
}
