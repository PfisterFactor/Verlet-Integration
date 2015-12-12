using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.System;

namespace VerletIntegration
{
    class RodConstraint : PointToPointConstraint
    {



        public RodConstraint(PointMass A, PointMass B, double distance = 0) : base(A, B, distance) { }

        public override void solve()
        {
            if (RodConstraint.distanceBetweenSquared(PointA, PointB) != _distanceSquared)
            {
                pointToPointSolve();
                
            }
        }
    }
}
