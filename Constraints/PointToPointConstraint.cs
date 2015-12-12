using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace VerletIntegration
{
    public abstract class PointToPointConstraint : Constraint
    {

        public PointMass PointB
        {
            get;
            protected set;
        }
        protected double _distanceSquared;
        protected double _distance;

        protected static double distanceBetween(PointMass A, PointMass B)
        {
            return Math.Sqrt(distanceBetweenSquared(A, B));
        }
        protected static double distanceBetweenSquared(PointMass A, PointMass B)
        {
            //Distance formula (without sqrt) (X1-X2)^2 + (Y1-Y2)^2
            return (A.X - B.X) * (A.X - B.X) + (A.Y - B.Y) * (A.Y - B.Y);

        }
        public override bool isConstrained(PointMass P)
        {
            return Object.ReferenceEquals(PointA,P) || Object.ReferenceEquals(PointB,P);
        }
        protected void pointToPointSolve()
        {
            double diffX = PointA.X - PointB.X;
            double diffY = PointA.Y - PointB.Y;
            double d = Math.Sqrt(diffX * diffX + diffY * diffY);

            //Difference scalar
            double difference = (_distance - d) / d;

            //Translation for each PointMass. They'll be pushed 1/2 the required distance to match their resting distances.
            double translateX = diffX * 0.5 * difference;
            double translateY = diffY * 0.5 * difference;

            PointA.Position.X += (float)translateX;
            PointA.Position.Y += (float)translateY;

            PointB.Position.X -= (float)translateX;
            PointB.Position.Y -= (float)translateY;
        }
        protected PointToPointConstraint(PointMass A, PointMass B, double distance = 0)
        {
            PointA = A;
            PointB = B;
            if (distance == 0)
            {

                _distance = RodConstraint.distanceBetween(PointA, PointB);

            }
            else _distance = distance;
            _distanceSquared = _distance * _distance;
            Constraint.activeObjects.Add(this);
        }
        public override void draw(RenderWindow window)
        {
            if (!isDrawn) return;
            Vertex one = new Vertex(PointA.Position, Color.Red);
            Vertex two = new Vertex(PointB.Position, Color.Blue);

            VertexArray drawArray = new VertexArray(PrimitiveType.Lines, 2);
            drawArray.Append(one);
            drawArray.Append(two);
            window.Draw(drawArray);
        }

        
    }
}
