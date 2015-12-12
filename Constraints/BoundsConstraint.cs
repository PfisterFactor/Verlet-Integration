using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using VerletIntegration.Utility;
namespace VerletIntegration.Constraints
{
    public class BoundsConstraint : Constraint
    {
        
        public Rect Size
        {
            get;
            protected set;
        }
        public bool isGlobal
        {
            get;
            protected set;
        }
        public float Friction
        {
            get;
            set;
        }
        
        public BoundsConstraint(Rect size, PointMass point,float friction = 1) : base()
        {
            PointA = point;
            Size = size;
            Friction = friction;
        }
        public BoundsConstraint(Rect size, float friction = 1)
        {
            Size = size;
            isGlobal = true;
            PointA = null;
            Friction = friction;
        }
        public override void solve()
        {

            Action<PointMass> miniSolve = new Action<PointMass>(point => {

                if (point.Position.X > Size.X + Size.Width)
                {
                    point.Position.X = Size.X + Size.Width ;
                    point.Velocity.X = -point.Velocity.X;
                    point.LastPosition += (point.Position - point.LastPosition).Multiply(Friction);
                }
                else if (point.Position.X < Size.X)
                {
                    point.Position.X = Size.X;
                    point.Velocity.X = -point.Velocity.X;
                    point.LastPosition += (point.Position - point.LastPosition).Multiply(Friction);
                }
                if (point.Position.Y > Size.Y + Size.Height)
                {
                    point.Position.Y = Size.Y + Size.Height;
                    point.Velocity.Y = -point.Velocity.Y;
                    point.LastPosition += (point.Position - point.LastPosition).Multiply(Friction);
                }
                else if (point.Position.Y < Size.Y)
                {
                    point.Position.Y = Size.Y;
                    point.Velocity.Y = -point.Velocity.Y;
                    point.LastPosition += (point.Position - point.LastPosition).Multiply(Friction);
                }
                

            });

            if (isGlobal)
            {
                foreach (PointMass p in PointMass.activeObjects)
                {
                    miniSolve(p);
                }
            }
            else
                miniSolve(PointA);
            
        }

        public override void draw(RenderWindow window)
        {
            if (!isDrawn) return;

            Vertex one = new Vertex(new Vector2f(Size.X, Size.Y), Color.Red);
            Vertex two = new Vertex(new Vector2f(Size.X + Size.Width, Size.Y), Color.Blue);
            Vertex three = new Vertex(new Vector2f(Size.X + Size.Width, Size.Y + Size.Height), Color.Green);
            Vertex four = new Vertex(new Vector2f(Size.X, Size.Y + Size.Height), Color.Yellow);

            VertexArray drawArray = new VertexArray(PrimitiveType.LinesStrip);
            drawArray.Append(one);
            drawArray.Append(two);
            drawArray.Append(three);
            drawArray.Append(four);

            window.Draw(drawArray);
        }
    }
}
