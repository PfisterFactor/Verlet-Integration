using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using VerletIntegration.Utility;
namespace VerletIntegration
{
    public class PointMass : DrawableObject
    {
        new public static List<PointMass> activeObjects = new List<PointMass>();
        public Vector2f Position;

        public double X => Position.X;

        public double Y => Position.Y;

        public Vector2f LastPosition;

        public Vector2f Velocity;

        public Vector2f Acceleration;

        public static void drawAll(bool yesNo)
        {
            foreach (PointMass P in PointMass.activeObjects)
            {
                P.isDrawn = yesNo;
            }
        }

        public PointMass()
        {
            Position = new Vector2f(0, 0);
            LastPosition = Position;
            Velocity = new Vector2f(0, 0);
            Acceleration = new Vector2f(0, 0);
        }
        public PointMass(Vector2f position)
        {
            Position = position;
            LastPosition = position;
            Velocity = new Vector2f(0, 0);
            Acceleration = new Vector2f(0, 0);
            activeObjects.Add(this);
        }
        public PointMass(Vector2f position, Vector2f acceleration)
        {
            Position = position;
            LastPosition = position;
            Velocity = new Vector2f(0, 0);
            Acceleration = acceleration;
            activeObjects.Add(this);
        }
        public void update(double delta)
        {
            //Gravity
            Acceleration += new Vector2f(0f, 10f);
            Velocity = (Position - LastPosition);
            LastPosition = Position;
            Position += Velocity + Acceleration.Multiply((float)delta);
            Acceleration = new Vector2f(0f,0f);

        }

        public override void draw(RenderWindow window)
        {
            if (!isDrawn) return;

            CircleShape C = new CircleShape(5f);
            C.Position = Position - new Vector2f(5f, 5f);
            C.FillColor = Color.Green;

            window.Draw(C);
        }
    }
}
