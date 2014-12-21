/*
 * VelocityObstacle defines a velocity obstacle used by the Collision-free navigation strategy
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RobotSwarmServer.Control_Strategies.Strategies.Collision_Avoidance
{
    class VelocityObstacle
    {
        public Vector leftEdge, rightEdge, centerline, vertex;
        public VelocityObstacle(Vector rEdge, Vector lEdge, Vector center, Vector a)
        {
            rightEdge = rEdge;
            leftEdge = lEdge;
            centerline = center;
            vertex = a;
        }
    }
}
