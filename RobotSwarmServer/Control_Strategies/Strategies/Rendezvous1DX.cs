/*
 * Gather the robots along a line parallel to the x-axis.
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
    public class Rendezvous1DX : ControlStrategy
    {

        public Rendezvous1DX()
            : base("Rendezvous1DX")
        {
        }

        public override void calculateNextMove(DoublePoint robotPosition, double speed, DoublePoint heading, List<Robot> neighbors, out double referenceSpeed, out DoublePoint referenceHeading)
        {
            DoublePoint rendezvousPosition = new DoublePoint(robotPosition.X, robotPosition.Y);

            // Caluclate the mean X position of the robots.
            foreach (Robot neighbor in neighbors)
            {
                if (neighbor.getDetected())
                {
                    rendezvousPosition.X += neighbor.getPosition().X;
                }
            }
            rendezvousPosition.X = rendezvousPosition.X / (neighbors.Count + 1);

            // Set reference speed according to disance to the rendezvous point.
            referenceSpeed = speedRegulator((rendezvousPosition - robotPosition).EuclideanNorm());

            referenceHeading.X = rendezvousPosition.X - robotPosition.X;
            referenceHeading.Y = 0;

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
            return new Rendezvous1DX();
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
