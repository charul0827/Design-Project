/*
 * Gather the robots along a line parallel to the y-axis.
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AForge;
using RobotSwarmServer.Control_Strategies;

namespace RobotSwarmServer
{
    class Rendezvous1DY : ControlStrategy
    {

        public Rendezvous1DY()
            : base("Rendezvous1DY")
        {
        }

        public override void calculateNextMove(DoublePoint robotPosition, double speed, DoublePoint heading, List<Robot> neighbors, out double referenceSpeed, out DoublePoint referenceHeading)
        {
            DoublePoint rendezvousPosition = new DoublePoint(robotPosition.X, robotPosition.Y);
            //List<Robot> neighborsTemp = new List<Robot>();
            //neighborsTemp.AddRange(neighbors);

            // Caluclate the mean Y position of the robots.
            foreach (Robot neighbor in neighbors)
            {
                if (neighbor.getDetected())
                {
                    rendezvousPosition.Y += neighbor.getPosition().Y;
                }
            }
            rendezvousPosition.Y = rendezvousPosition.Y / (neighbors.Count + 1);

            // Set reference speed according to disance to the rendezvous point.
            referenceSpeed = speedRegulator((rendezvousPosition - robotPosition).EuclideanNorm());

            referenceHeading.X = 0;
            referenceHeading.Y = rendezvousPosition.Y - robotPosition.Y;

            // If the length of the heading vector is non-zero it's also safe to normalize the heading.
            if (referenceHeading.EuclideanNorm() != 0)
            {
                // Normalize referenceHeading.
                referenceHeading = referenceHeading / referenceHeading.EuclideanNorm();
            }
        }

        private double speedRegulator(double distance)
        {
            double maxSpeed = 80;   //Max is approximately 80
            double pSpeed = 200;    //200 is a standard value

            return Math.Min(maxSpeed, pSpeed * Math.Abs(distance) / Program.resolution.X);
        }

        public override ControlStrategy cloneStrategy()
        {
            return new Rendezvous1DY();
        }

        public override void paintStrategy(System.Drawing.Graphics g, DoublePoint scaling)
        {
        }

        public override void initializeStrategyPanel()
        {
            throw new NotImplementedException();
        }
    }
}
