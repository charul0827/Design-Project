using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AForge;
using RobotSwarmServer.Control_Strategies;

namespace RobotSwarmServer
{
    /*
    * This strategy averages all neighbors positions and tries to arrive at that point
    */
    class Rendezvous2D : ControlStrategy
    {
        public Rendezvous2D()
            : base("Rendezvous2D")
        {
        }

        /*
         * Firstly the positions are averaged and later on uses the strategy A2B to get to the calculated point.
         */
        public override void calculateNextMove(DoublePoint robotPosition, double speed, DoublePoint head, List<Robot> neighbors, out double referenceSpeed, out DoublePoint referenceHeading)
        {
            DoublePoint rendezvousPosition = new DoublePoint(robotPosition.X, robotPosition.Y);
            List<Robot> neighborsTemp = new List<Robot>();
            neighborsTemp.AddRange(neighbors);

            // Caluclate the mean X and Y position of the robots.
            foreach (Robot neighbor in neighborsTemp)
            {
                if (neighbor.getDetected())
                {
                    rendezvousPosition += neighbor.getPosition();
                }
            }
            rendezvousPosition = rendezvousPosition / (neighborsTemp.Count + 1);

            // Set reference speed according to disance to the rendezvous point.
            referenceSpeed = speedRegulator((rendezvousPosition - robotPosition).EuclideanNorm());

            referenceHeading = rendezvousPosition - robotPosition;

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
            return new Rendezvous2D();
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
