using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using VerletIntegration.Constraints;
using VerletIntegration.Utility;
using SFML.Graphics;
namespace VerletIntegration.Shapes
{
    public abstract class Shape
    {
        public PointMass[] Points
        {
            get;
            protected set;
        }
        public Constraint[] Constraints
        {
            get;
            protected set;
        }
        int Vertices => Points.Length;
        public void moveTo(Vector2f pos)
        {
            foreach (PointMass P in Points)
            {
                Vector2f distanceFromFirstPoint = (Points[0].Position - P.Position);
                P.Position = pos+distanceFromFirstPoint;
                P.LastPosition = P.Position;
            }
        }
        public bool intersecting(PointMass point)
        {
            return intersecting(point.Position); 
        }
        //wat
        public bool intersecting(Vector2f point)
        {
            bool isInside = false;

            for (int i = 0, j = Points.Length - 1; i < Points.Length; j = i++)

            {
                if (((Points[i].Y > point.Y) != (Points[j].Y > point.Y)) &&
                (point.X < (Points[j].X - Points[i].X) * (point.Y - Points[i].Y) / (Points[j].Y - Points[i].Y) + Points[i].X))

                {

                    isInside = !isInside;

                }

            }

            return isInside;
        }
        private Vector2f[] getAxisToTest(Shape other)
        {
            var axisToTest = new List<Vector2f>();
            var axisToTest2 = new List<Vector2f>();
            for (int i = 0; i < this.Points.Length - 1; i++)
            {
                Vector2f normal = (this.Points[i + 1].Position - this.Points[i].Position);
                axisToTest.Add(normal);
            }
            for (int i = 0; i < other.Points.Length - 1; i++)
            {
                Vector2f normal = (other.Points[i + 1].Position - other.Points[i].Position);
                axisToTest2.Add(normal);
            }
            return axisToTest.Union<Vector2f>(axisToTest2).ToArray();
        }
        public bool intersecting(Shape other,RenderWindow w)
        {
            Vector2f[] axisArray = getAxisToTest(other);
            
            foreach (Vector2f axis in axisArray)
            {
                double[] projectedThis = Points.Select(x => x.Position.DotProduct(axis)).ToArray();
                double[] projectedOther = other.Points.Select(x => x.Position.DotProduct(axis)).ToArray();

                double thisMin = projectedThis.Min();
                double thisMax = projectedThis.Max();
                double otherMin = projectedOther.Min();
                double otherMax = projectedOther.Max();
                //VertexArray a = new VertexArray(PrimitiveType.LinesStrip);
                //a.Append(new Vertex(axis.Multiply(-1000),Color.Red));
                //a.Append(new Vertex(new Vector2f(400,300), Color.Red));
                //a.Append(new Vertex(axis.Multiply(1000), Color.Red));
                //w.Draw(a);
                
                if (otherMin > thisMax || thisMin > otherMax)
                    return false;
                    
            }
            return true;

        }
    }
}
