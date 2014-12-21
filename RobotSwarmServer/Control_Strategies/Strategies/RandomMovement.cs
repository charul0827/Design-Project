using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AForge;

namespace RobotSwarmServer.Control_Strategies.Strategies
{
    class RandomMovement : ControlStrategy
    {

        /*
         * A simple strategy that just moves randomly, could in the future be combined with borderavoidance to be implemented in the real platform.
         */

        Random random;
        BorderAvoidance borderAvoidance = new BorderAvoidance(500);

        double maxSpeed, maxAngSpeed;
        DoublePoint position, heading;
        static Pen pen = new Pen(new SolidBrush(Color.Black));

        public RandomMovement()
            : base("Random Movement")
        {
            random = new Random();

            this.maxSpeed = 1;
            this.maxAngSpeed = ControlStrategy.PI / 8;

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
        }

        /*
         * This function doesn't take a random number each time, instead it adds a smaller random number to the existing speed and heading.
         */
        public override void calculateNextMove(DoublePoint robotPosition, double speed, DoublePoint heading, List<Robot> neighbors, out double referenceSpeed, out DoublePoint referenceHeading)
        {
            this.heading = heading;
            this.position = robotPosition;
            referenceSpeed = speed + (2 * random.NextDouble() - 1) * this.maxSpeed / 10;
            referenceHeading = ControlStrategy.ang2Point(ControlStrategy.point2Ang(heading) + this.maxAngSpeed * (2 * random.NextDouble() - 1));
            borderAvoidance.calculateNextMove(robotPosition, referenceSpeed, referenceHeading, neighbors, out referenceSpeed, out referenceHeading);
        }
        public override ControlStrategy cloneStrategy()
        {
            return this;
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