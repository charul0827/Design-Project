using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using AForge;

namespace RobotSwarmServer.Control_Strategies.Strategies
{
    public class BorderAvoidance : ControlStrategy
    {
        A2B returnToCenter;

        int robotMaxPosition;
        public BorderAvoidance(int simulationArea)
            : base("Border Avoidance")
        {
            this.returnToCenter = new A2B(new DoublePoint(0, 0), "Return to center");
            this.robotMaxPosition = simulationArea;
        }

        public override void calculateNextMove(DoublePoint robotPosition, double speed, DoublePoint heading, List<Robot> neighbors, out double referenceSpeed, out DoublePoint referenceHeading)
        {
            referenceSpeed = speed;
            referenceHeading = heading;

            if (Math.Abs(robotPosition.X) > robotMaxPosition || Math.Abs(robotPosition.Y) > robotMaxPosition)
            {
                returnToCenter.calculateNextMove(robotPosition, speed, heading, neighbors, out referenceSpeed, out referenceHeading);
            }
        }

        public void setSimulationArea(int robotMaxPosition)
        {
            this.robotMaxPosition = robotMaxPosition;
        }

        public override ControlStrategy cloneStrategy()
        {
            return new BorderAvoidance(robotMaxPosition);
        }


        public override void paintStrategy(Graphics g, DoublePoint scaling)
        {
        }

        public override void initializeStrategyPanel()
        {
            throw new NotImplementedException();
        }
    }
}
