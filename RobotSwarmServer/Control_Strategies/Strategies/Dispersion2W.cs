/*
 * Makes the robots position themselves equidistant to each other. This often results in that 
 * the robots positioning themselves in a circle.
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
    public class Dispersion2W : ControlStrategy
    {
        public Dispersion2W()
            : base("Dispersion2W")
        {
        }

        public override void calculateNextMove(DoublePoint robotPosition, double speed, DoublePoint heading, List<Robot> neighbors, out double referenceSpeed, out DoublePoint referenceHeading)
        {
            DoublePoint vectorBetweenRobots;
            DoublePoint summaryVector = new DoublePoint(0, 0);
            double distanceBetweenRobots;
            double dispersionSpeed;

            foreach (Robot neighbor in neighbors)
            {
                if (neighbor.getDetected())
                {
                    distanceBetweenRobots = robotPosition.DistanceTo(neighbor.getPosition());

                    if (distanceBetweenRobots < Program.dispersionRange)
                    {
                        vectorBetweenRobots = robotPosition - neighbor.getPosition();
                    }
                    else
                    {
                        vectorBetweenRobots = neighbor.getPosition() - robotPosition;
                    }

                    dispersionSpeed = speedRegulator(distanceBetweenRobots - Program.dispersionRange);
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
            double maxSpeed = 60;   //Max is approximately 80
            double pSpeed = 80;    //200 is a standard value

            return Math.Min(maxSpeed, pSpeed * Math.Abs(distance) / Program.resolution.X);
        }

        public override ControlStrategy cloneStrategy()
        {
            return new Dispersion2W();
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
