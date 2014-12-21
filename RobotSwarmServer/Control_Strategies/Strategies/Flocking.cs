using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AForge;
using RobotSwarmServer.Control_Strategies;
namespace RobotSwarmServer.Control_Strategies.Strategies
{
    class Flocking : ControlStrategy
    {

        /*
         * This strategy turns the nearby robots into a flock with corresponding behaviour
         */
        public Flocking()
            : base("Flocking")
        {

        }

        /*
         * This function averages the speed and the heading of the robot and it's neighbors and applies it.
         */
        public override void calculateNextMove(DoublePoint robotPosition, double speed, DoublePoint heading, List<Robot> neighbors, out double referenceSpeed, out DoublePoint referenceHeading)
        {
            double flockingSpeed = speed;
            DoublePoint flockingHeading = heading;

            foreach (Robot neighbor in neighbors)
            {
                flockingSpeed += neighbor.getSpeed();
                flockingHeading += neighbor.getHeading();
            }

            flockingSpeed /= (neighbors.Count + 1);
            flockingHeading /= (neighbors.Count + 1);

            referenceSpeed = flockingSpeed;
            referenceHeading = flockingHeading;
        }

        public override ControlStrategy cloneStrategy()
        {
            return new Flocking();
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
