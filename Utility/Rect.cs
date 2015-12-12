using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerletIntegration.Utility
{
    public struct Rect
    {
        public int X;
        public int Y;
        public float Width;
        public float Height;

        public Rect(int x, int y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }

}
