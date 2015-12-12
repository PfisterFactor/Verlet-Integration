using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerletIntegration
{
    public class RopeConstraint : PointToPointConstraint
    {
        public RopeConstraint(PointMass A, PointMass B, double distance = 0) : base(A, B, distance) {  }

        public override void solve()
        {
            if (RodConstraint.distanceBetweenSquared(PointA, PointB) >= _distanceSquared)
            {
                pointToPointSolve();

            }
        }
    }
}
