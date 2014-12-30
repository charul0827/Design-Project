/*
 * A basic strategy that takes the robot to a desired destination coordinate.
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AForge;
using System.Drawing;
using RobotSwarmServer.Control_Strategies;

namespace RobotSwarmServer
{
    class A2B : ControlStrategy
    {
        DoublePoint referencePoint;
        
        public A2B()
            :base ("A2B")
        {
        }
        public A2B(DoublePoint p)
            : this(p, "A2B")
        {
            referencePoint = p;
        }
        public A2B(DoublePoint p, string name)
            : base(name)
        {
            referencePoint = p;
        }

        /*
         * The overrided calculateNextMove from the Control strategy class
         */
        public override void calculateNextMove(DoublePoint robotPosition, double speed, DoublePoint heading, List<Robot> neighbors, out double referenceSpeed, out DoublePoint referenceHeading)
        {
            //referenceSpeed = speed;   //uncomment if using m3pi robots

            //$$$$$Changes/Additions for RC cars$$$$$//
            referenceSpeed = Program.testSpeed;
            //$$$$$$$$$$//

            referenceHeading = heading;

            if (referencePoint != null)
            {
                calculateNextMove(referencePoint, robotPosition, speed, heading, neighbors, out referenceSpeed, out referenceHeading);
            }
            else
            {
                throw new Exception("Reference point is not initiated");
            }
        }
        /*
         * Two other methods in order to make the robots behave according to specific parameters.
         */
        public void calculateNextMove(DoublePoint referencePoint, DoublePoint robotPosition, double speed, DoublePoint heading, List<Robot> neighbors, out double referenceSpeed, out DoublePoint referenceHeading)
        {
            calculateNextMove(referencePoint, 0, robotPosition, speed, heading, neighbors, out referenceSpeed, out referenceHeading);

        }
        public void calculateNextMove(DoublePoint referencePoint, int offset, DoublePoint robotPosition, double speed, DoublePoint heading, List<Robot> neighbors, out double referenceSpeed, out DoublePoint referenceHeading)
        {
            this.referencePoint = referencePoint;
            if (robotPosition != referencePoint)
            {
                referenceHeading = ControlStrategy.vector2Point(robotPosition, referencePoint);

                double distance = robotPosition.DistanceTo(referencePoint);
                referenceSpeed = speedRegulator(distance - offset);
            }
            else
            {
                referenceSpeed = Program.neutralSpeed;           
                referenceHeading = heading;
            }
        }

        private double speedRegulator(double distance)
        {
            //uncomment the below lines if using m3pi robots
            //double maxSpeed = 100;   //Max is approximately 100
            //double pSpeed = 200;    //200 is a standard value
            //return Math.Min(maxSpeed, pSpeed * Math.Abs(distance) / Program.resolution.X);

            //$$$$$Changes/Additions for RC cars$$$$$//
            return Program.testSpeed;
            //$$$$$$$$$$//
        }

        public override ControlStrategy cloneStrategy()
        {
            return new A2B();
        }

        public override void paintStrategy(System.Drawing.Graphics g, DoublePoint scaling)
        {
            if (referencePoint != null)
            {
                g.FillEllipse(new SolidBrush(Color.Black), new Rectangle((int)(scaling.X*referencePoint.X - 5), (int)(scaling.Y*referencePoint.Y) - 5, 10, 10));
            }
        }

        public override void initializeStrategyPanel()
        {
            throw new NotImplementedException();
        }
    }
}
