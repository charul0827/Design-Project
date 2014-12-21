/*
 * 
 * Control_Strategies\ControlStrategy
 * This is an abstract class that defines the structure of all the control strategies 
 * that is implemented in the system. This is used to ensure that all strategies 
 * follows the given interface that defines input and outputs from the strategies.
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using RobotSwarmServer.Control_Strategies.Strategies.Collision_Avoidance.FIFO;

namespace RobotSwarmServer.Control_Strategies
{
    public abstract partial class ControlStrategy : UserControl
    {
        protected String name; // name of strategy
        public static double PI = 3.14; // pi constant

        public ControlStrategy(string name)
        {
            InitializeComponent();

            this.name = name;
        }

        // abstract function used to calculate the next move from each strategy
        public abstract void calculateNextMove(DoublePoint robotPosition, double speed, DoublePoint heading, List<Robot> neighbors, out double referenceSpeed, out DoublePoint referenceHeading);

        // abstract function used to clone a strategy object
        public abstract ControlStrategy cloneStrategy();

        // returns the name of the strategy
        public String getName()
        {
            return name;
        }

        public abstract void paintStrategy(System.Drawing.Graphics g, DoublePoint scaling);
        public abstract void initializeStrategyPanel();
        protected GroupBox getStrategySettingsBox()
        {
            return this.strategySettingsBox;
        }
        
        
        
        /*
         * Static functions
         */

        //Return the angle from positive x-axis to the given point
        public static double point2Ang(DoublePoint heading) 
        {
            double angHeading = Math.Atan2(heading.Y, heading.X);

            angHeading = angHeading >= 0 ? angHeading : 2 * PI + angHeading;

            return angHeading;
        }
        public static DoublePoint ang2Point(double ang)
        {
            DoublePoint heading = new DoublePoint(Math.Cos(ang), Math.Sin(ang));
            heading /= heading.EuclideanNorm();

            return heading;
        }
        
        // Return the vector pointing from p1 to p2.
        public static DoublePoint vector2Point(DoublePoint p1, DoublePoint p2)
        {
            DoublePoint vector = new DoublePoint(p2.X - p1.X, p2.Y - p1.Y);
            vector /= vector.EuclideanNorm();

            return vector;
        }
        
        /*
         * Returns the angle that heading p1 have to change to arrive at p2's value
         * p1 and p2 are angles
         */
        public static double angleDifferance(DoublePoint p1, DoublePoint p2)
        {
            double aDiff = point2Ang(p2) - point2Ang(p1);

            if (Math.Abs(aDiff) > ControlStrategy.PI)
            {
                aDiff = -Math.Sign(aDiff) * (2 * ControlStrategy.PI - Math.Abs(aDiff));
            }

            return aDiff;
        }

        // Controls if the robot is heading towards a collision area, used by FIFO
        public static bool isAtHeading(CollisionArea area, DoublePoint robotPosition, DoublePoint robotHeading)
        {
            DoublePoint corner1, corner2;

            if (robotPosition.X > area.getX())
            {
                if (robotPosition.X > area.getRight())
                {
                    if (robotPosition.Y > area.getBottom())
                    {
                        corner1 = area.getPoint(4);
                        corner2 = area.getPoint(12);
                    }
                    else if (robotPosition.Y < area.getY())
                    {
                        corner1 = area.getPoint(8);
                        corner2 = area.getPoint(0);
                    }
                    else
                    {
                        corner1 = area.getPoint(8);
                        corner2 = area.getPoint(12);
                    }
                }
                else
                {
                    if (robotPosition.Y > area.getBottom())
                    {
                        corner1 = area.getPoint(4);
                        corner2 = area.getPoint(8);
                    }
                    else
                    {
                        corner1 = area.getPoint(12);
                        corner2 = area.getPoint(0);
                    }
                }
            }
            else
            {
                if (robotPosition.Y > area.getBottom())
                {
                    corner1 = area.getPoint(8);
                    corner2 = area.getPoint(0);
                }
                else if (robotPosition.Y < area.getY())
                {
                    corner1 = area.getPoint(4);
                    corner2 = area.getPoint(12);
                }
                else
                {
                    corner1 = area.getPoint(4);
                    corner2 = area.getPoint(0);
                }
            }

            //Largest angle
            DoublePoint corner1Heading = new DoublePoint(corner1.X - robotPosition.X, corner1.Y - robotPosition.Y);
            //Smallest angle
            DoublePoint corner2Heading = new DoublePoint(corner2.X - robotPosition.X, corner2.Y - robotPosition.Y);
            double cornerAngDiff = Math.Abs(angleDifferance(corner1Heading, corner2Heading));
            double corner1AngDiff = Math.Abs(angleDifferance(robotHeading, corner1Heading));
            double corner2AngDiff = Math.Abs(angleDifferance(robotHeading, corner2Heading));

            area.setHeadingEdges(corner1, corner2);

            if (cornerAngDiff > corner1AngDiff && cornerAngDiff > corner2AngDiff && (corner1AngDiff + corner2AngDiff) < PI)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

