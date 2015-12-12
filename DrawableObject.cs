using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace VerletIntegration
{
    public abstract class DrawableObject
    {
        public static List<DrawableObject> activeObjects => Constraint.activeObjects.Union<DrawableObject>(PointMass.activeObjects).ToList();
        public void delete()
        {
            activeObjects.Remove(this);
        }
        public abstract void draw(RenderWindow window);
        public bool isDrawn
        {
            get;
            set;
        }
    }
}
