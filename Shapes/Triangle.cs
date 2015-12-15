using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using VerletIntegration.Shapes;
using VerletIntegration.Constraints;

namespace VerletIntegration.Shapes
{
    public class Triangle : Shape
    {
        public Triangle(PointMass[] points) : base(points)
        {
            if (points.Length != 3) throw new ArgumentOutOfRangeException();

            Vector2f max = new Vector2f(
                (float)Points.Max<PointMass, double>(x => x.Position.X),
                (float)Points.Max<PointMass, double>(x => x.Position.Y));

            Vector2f min = new Vector2f(
                (float)Points.Min<PointMass, double>(x => x.Position.X),
                (float)Points.Min<PointMass, double>(x => x.Position.Y));

            Width = max.X - min.X;
            Height = max.Y - min.Y;

            Constraints = new Constraint[3] 
            { new RodConstraint(Points[0], Points[1]), new RodConstraint(Points[1], Points[2]), new RodConstraint(Points[2], Points[1])};
        }
    }
}
