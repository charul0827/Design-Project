using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AForge;
using RobotSwarmServer.Control_Strategies;

namespace RobotSwarmServer
{
    class StandStill : ControlStrategy
    {
        public StandStill()
            : base("StandStill")
        {
        }

        public override void calculateNextMove(DoublePoint robotPosition, double speed, DoublePoint heading, List<Robot> neighbors, out double referenceSpeed, out DoublePoint referenceHeading)
        {
            referenceHeading = heading;
            referenceSpeed = Program.neutralSpeed;
        }

        public override ControlStrategy cloneStrategy()
        {
            return new StandStill();
        }

        public override void paintStrategy(System.Drawing.Graphics g, DoublePoint scaling)
        {
        }

        public override void initializeStrategyPanel()
        {
        }
    }
}
