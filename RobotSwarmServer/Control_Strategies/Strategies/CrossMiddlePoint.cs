/*
 * A strategy that makes the robot cross the field through a common point. This is used to
 * test the Collision-free navigation strategy
 */ 

using AForge;
using RobotSwarmServer.Control_Strategies.Strategies.Collision_Avoidance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSwarmServer.Control_Strategies.Strategies
{
    class CrossMiddlePoint : ControlStrategy
    {
        A2B goToPoint;
        CollisionFreeNavigation collisionStrategy;
        DoublePoint pointAcrossField;
        DoublePoint startPoint;
        DoublePoint nextPoint;
        bool pointCalculated = false;
        public bool pointReached = false;

        public CrossMiddlePoint()
            : base("CrossMiddlePoint")
        {
            goToPoint = new A2B();
            collisionStrategy = new CollisionFreeNavigation();
        }

        public override void calculateNextMove(DoublePoint robotPosition, double speed, DoublePoint heading, List<Robot> neighbors, out double referenceSpeed, out DoublePoint referenceHeading)
        {
            if (!pointCalculated)
            {                
                pointAcrossField = calculatePointAcrossField(robotPosition, neighbors);
                nextPoint = pointAcrossField;
                startPoint = robotPosition;
                pointCalculated = true;
                pointReached = false;
            }

            if (robotPosition.DistanceTo(nextPoint) < 20)
            {
                pointReached = true;
                referenceHeading = heading;
                referenceSpeed = 0;
            }
            else
            {
                goToPoint.calculateNextMove(nextPoint, robotPosition, speed, heading, neighbors, out referenceSpeed, out referenceHeading);
                collisionStrategy.calculateNextMove(robotPosition, referenceSpeed, referenceHeading, neighbors, out referenceSpeed, out referenceHeading);
            }

        }

        private DoublePoint calculatePointAcrossField(DoublePoint robotPos, List<Robot> neighbors)
        {
            DoublePoint rondezvousPosition = new DoublePoint(robotPos.X, robotPos.Y);

            foreach (Robot neighbor in neighbors)
            {
                rondezvousPosition += neighbor.getPosition();
            }
            rondezvousPosition /= (neighbors.Count + 1);

            return rondezvousPosition + rondezvousPosition - robotPos;
        }

        public override ControlStrategy cloneStrategy()
        {
            return new CrossMiddlePoint();
        }


        public override void paintStrategy(System.Drawing.Graphics g, DoublePoint scaling)
        {
            collisionStrategy.paintStrategy(g, scaling);
        }

        public override void initializeStrategyPanel()
        {
            throw new NotImplementedException();
        }
    }
}
