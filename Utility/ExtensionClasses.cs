using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace VerletIntegration
{
    public static class ExtensionClasses
    {
        public static Vector2f Multiply(this Vector2f a, float b)
        {
            return new Vector2f(a.X * b, a.Y * b);
        }
        //Thanks stack overflow, probably wont need generics though...
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
    }
}
