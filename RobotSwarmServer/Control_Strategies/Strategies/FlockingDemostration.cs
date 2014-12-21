
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSwarmServer.Control_Strategies.Strategies
{
    class FlockingDemostration : ControlStrategy
    {

        /*
         * A simple strategy to demostrate the flocking strategy
         */

        int startFlockingRange;
        bool startFlocking;
        Rendezvous2D rendevouzStrategy;
        Flocking flockingStrategy;

        public FlockingDemostration(int startFlockingRange)
            : base("FlockingDemostration")
        {
            this.startFlockingRange = startFlockingRange;
            this.rendevouzStrategy = new Rendezvous2D();
            this.flockingStrategy = new Flocking();
        }

        /*
         * This function firstly applies the rendevouz, and when the robots are in a certain range from each other the flocking strategy
         */
        public override void calculateNextMove(AForge.DoublePoint robotPosition, double speed, AForge.DoublePoint heading, List<Robot> neighbors, out double referenceSpeed, out AForge.DoublePoint referenceHeading)
        {
            bool startFlocking = false;

            double distance;
            for (int i = 0; i < neighbors.Count; i++)
            {
                distance = robotPosition.DistanceTo(neighbors[i].getPosition());
                if (distance < startFlockingRange)
                {
                    startFlocking = true;
                }
            }

            if (startFlocking)
            {
                flockingStrategy.calculateNextMove(robotPosition, speed, heading, neighbors, out referenceSpeed, out referenceHeading);
            }
            else
            {
                rendevouzStrategy.calculateNextMove(robotPosition, speed, heading, neighbors, out referenceSpeed, out referenceHeading);
            }
        }

        public override ControlStrategy cloneStrategy()
        {
            return new FlockingDemostration(startFlockingRange);
        }

        public override void initializeStrategyPanel()
        {
        }

        public override void paintStrategy(Graphics g, AForge.DoublePoint scaling)
        {

        }
    }
}
