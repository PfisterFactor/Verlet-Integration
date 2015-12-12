using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;

namespace VerletIntegration
{
    public static class Input
    {
        public static void onWindowClose(object sender, EventArgs e)
        {
            Program.gameRunning = false;
        }
        public static void onMousePressed(object sender, EventArgs e)
        {
            Program.mouseClick = true;

        }
        public static void onMouseRelease(object sender, EventArgs e)
        {
            Program.mouseClick = false;
        }
        public static void onMouseMove(object sender, MouseMoveEventArgs e)
        {
            Program.mousePos = new Vector2f(e.X, e.Y);
        }
    }
}
