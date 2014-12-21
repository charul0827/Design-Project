/*
 * A strategy where you can use the mouse to steer the robots across the field by clicking
 * in the displayFrame. It uses the basic strategy A2B to take the robot to the desired destination.
 * It also implements the collision avoidance strategy Collision-free navigation to make the robots
 * avoid collisions between each other. 
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AForge;
using RobotSwarmServer.Control_Strategies.Strategies.Collision_Avoidance;
using RobotSwarmServer.Control_Strategies;

namespace RobotSwarmServer
{
    class PointFromInput : ControlStrategy
    {
        A2B toPoint;
        CollisionFreeNavigation collisionStrategy;

        public PointFromInput()
            : base("PointFromInput")
        {
            toPoint = new A2B();
            collisionStrategy = new CollisionFreeNavigation();
        }

        public override void calculateNextMove(DoublePoint robotPosition, double speed, DoublePoint heading, List<Robot> neighbors, out double referenceSpeed, out DoublePoint referenceHeading)
        {
            toPoint.calculateNextMove(Program.referencePoint, robotPosition, speed, heading, neighbors, out referenceSpeed, out referenceHeading);
            collisionStrategy.calculateNextMove(robotPosition, referenceSpeed, referenceHeading, neighbors, out referenceSpeed, out referenceHeading);
        }

        public override ControlStrategy cloneStrategy()
        {
            return new PointFromInput();
        }

        public override void paintStrategy(System.Drawing.Graphics g, DoublePoint scaling)
        {
            toPoint.paintStrategy(g, scaling);
        }

        public override void initializeStrategyPanel()
        {
            throw new NotImplementedException();
        }
    }
}
