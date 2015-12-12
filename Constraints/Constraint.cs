using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace VerletIntegration.Constraints
{
    public abstract class Constraint : DrawableObject
    {
        new public static List<Constraint> activeObjects = new List<Constraint>();

        public static void drawAll(bool yesNo)
        {
            foreach (Constraint C in Constraint.activeObjects)
            {
                C.isDrawn = yesNo;
            }
        }
        public PointMass PointA
        {
            get;
            protected set;
        }
        public override abstract void draw(RenderWindow window);
        public virtual bool isConstrained(PointMass P)
        {
            return Object.ReferenceEquals(PointA, P);
        }
        public abstract void solve();
        protected Constraint()
        {
            activeObjects.Add(this);
        }
    }
}
