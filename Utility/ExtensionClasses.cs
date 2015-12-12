using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace VerletIntegration.Utility
{
    public static class ExtensionClasses
    {
        public static Vector2f Multiply(this Vector2f a, float b)
        {
            return new Vector2f(a.X * b, a.Y * b);
        }
        public static Vector2f Normalize(this Vector2f a)
        {
            double length = Math.Sqrt((a.X * a.X) + (a.Y * a.Y));

            if (length != 0)
                return new Vector2f((float)(a.X / length), (float)(a.Y / length));
            else
                return a;
        }
        public static double DotProduct(this Vector2f a, Vector2f b)
        {
            return (a.X * b.X) + (a.Y * b.Y);
        }

        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
        
    }
}
