using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AForge;
using RobotSwarmServer.Control_Strategies;

namespace RobotSwarmServer 
{
    class Dispersion1W: ControlStrategy
    {
        public Dispersion1W()
            : base("Dispersion1W")
        {
        }

        public override void calculateNextMove(DoublePoint robotPosition, double speed, DoublePoint heading, List<Robot> neighbors, out double referenceSpeed, out DoublePoint referenceHeading)
        {
            DoublePoint vectorBetweenRobots;
            DoublePoint summaryVector = new DoublePoint(0, 0);
            double distanceBetweenRobots;
            double dispersionSpeed;

            List<Robot> neighborsTemp = new List<Robot>();
            neighborsTemp.AddRange(neighbors);

            foreach (Robot neighbor in neighborsTemp)
            {
                if (neighbor.getDetected())
                {
                    distanceBetweenRobots = robotPosition.DistanceTo(neighbor.getPosition());
                    vectorBetweenRobots = robotPosition - neighbor.getPosition();

                    if (distanceBetweenRobots - Program.dispersionRange < 0)
                    {
                        dispersionSpeed = speedRegulator(distanceBetweenRobots - Program.dispersionRange);
                    }
                    else
                    {
                        dispersionSpeed = 0;
                    }

                    summaryVector += (vectorBetweenRobots / distanceBetweenRobots) * dispersionSpeed;
                }
            }

            referenceSpeed = summaryVector.EuclideanNorm();

            // If the length of the heading vector is non-zero it's also safe to normalize the heading.
            if (summaryVector.EuclideanNorm() != 0)
            {
                referenceHeading = summaryVector / summaryVector.EuclideanNorm();
            }
            else
            {
                referenceHeading = heading;
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
            return new Dispersion1W();
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
