using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AForge;
using System.Windows.Forms;
using System.Drawing;
using RobotSwarmServer.Control_Strategies.Strategies.Collision_Avoidance.FIFO;

namespace RobotSwarmServer.Control_Strategies.Strategies
{
    class FollowPath : ControlStrategy
    {
        /*
         * This strategy a certain path existing of points and follows them.
         */

        //Static field
        public static Random random = new Random();

        //Strategy Panel field
        CheckBox directionBox;

        //Strategy fields
        DoublePoint[] points;
        FIFO fifoStrategy;
        A2B goToPoint = new A2B();

        Boolean onLine = false;
        private int pointCount = 0;
        private int direction = 1;    //uncomment if using m3pi robots
        
        //$$$$$Changes/Additions for RC cars$$$$$//
        //private int direction;
        //$$$$$$$$$$//

        protected int closeUpLimit;

        public FollowPath(string name, DoublePoint[] points)
            : this(name, points, 100)
        {
        }
        public FollowPath(string name, DoublePoint[] points, int closeUpLimit)
            : base(name)
        {
            this.points = points;
            //direction = 1;    //uncomment if using m3pi robots    

            this.closeUpLimit = closeUpLimit;

            initializeStrategyPanel();
        }
        public FollowPath(string name, DoublePoint[] points, FIFO fifoStrategy)
            : this(name, points, fifoStrategy, 100)
        {
        }
        public FollowPath(string name, DoublePoint[] points, FIFO fifoStrategy, int closeUpLimit)
            : this(name, points, closeUpLimit)
        {
            this.fifoStrategy = fifoStrategy;
        }


        public void setDirection(int direction)
        {
            this.direction = direction;
            
        }

        /*
         * This function firstly calculates the closest point, drives towards it and finally starts the path.
         */
        public override void calculateNextMove(AForge.DoublePoint robotPosition, double speed, AForge.DoublePoint heading, List<Robot> neighbors, out double referenceSpeed, out AForge.DoublePoint referenceHeading)
        {
            if (!onLine)
            {
                pointCount = 0;
                double distance = robotPosition.DistanceTo(points[pointCount]);

                for (int i = 0; i < points.Count(); i++)
                {
                    if (distance > points[i].DistanceTo(robotPosition))
                    {
                        pointCount = i;
                        distance = points[pointCount].DistanceTo(robotPosition);
                    }
                }
                Console.WriteLine(closeUpLimit);
                if (distance < closeUpLimit)
                {
                    onLine = true;
                }
                else
                {
                    goToPoint.calculateNextMove(points[pointCount], robotPosition, speed, heading, neighbors, out referenceSpeed, out referenceHeading);
                    return;
                }
            }
            
            if (onLine)
            {
                double distance = robotPosition.DistanceTo(points[pointCount]);
                if (distance < closeUpLimit)
                {
                    if (direction == 1)
                    {
                        pointCount = (pointCount >= points.Count() - 1 ? 0 : pointCount + direction);
                    }
                    else
                    {
                        //pointCount = (pointCount >= 1 ? points.Count() - 1 : pointCount + direction);
                        pointCount = (pointCount > 1 ? pointCount - 1 : points.Count() - 1);
                    }
                }

                goToPoint.calculateNextMove(points[pointCount], robotPosition, speed, heading, neighbors, out referenceSpeed, out referenceHeading);
                if (fifoStrategy != null)
                {
                    fifoStrategy.calculateNextMove(robotPosition, referenceSpeed, referenceHeading, neighbors, out referenceSpeed, out referenceHeading);
                }
                return;
            }

            //referenceSpeed = speed;
            referenceSpeed = Program.testSpeed;
            referenceHeading = heading;
        }

        public override ControlStrategy cloneStrategy()
        {
            if (this.fifoStrategy != null)
            {
                FIFO fifoStrategy = (FIFO)this.fifoStrategy.cloneStrategy();
                return new FollowPath(name, points, fifoStrategy, closeUpLimit);
            }
            else
            {
                return new FollowPath(name, points, closeUpLimit);
            }
            
        }

        private void directionBox_CheckedChanged(object sender, EventArgs e)
        {
            setDirection(-direction);
           
        }

        public override void paintStrategy(System.Drawing.Graphics g, DoublePoint scaling)
        {
            foreach (DoublePoint p in points)
            {
                g.FillEllipse(new SolidBrush(Color.Black), new Rectangle((int)(scaling.X*p.X-5), (int)(scaling.Y*p.Y-5), 10,10));
            }

            if (fifoStrategy != null)
            {
                fifoStrategy.paintStrategy(g, scaling);
            }
        }

        //Static functions to create shapes
        public static DoublePoint[] createLine(DoublePoint a, DoublePoint b, int nrOfPoints)
        {
            return null;
        }
        public static DoublePoint[] createCirclePoints(int radius, DoublePoint center, int nrPoints)
        {
            DoublePoint[] circlePoints = new DoublePoint[nrPoints];

            double angle = 0;
            double angleStep = 2 * ControlStrategy.PI / nrPoints;

            for (int i = 0; i < nrPoints; i++)
            {
                circlePoints[i] = new DoublePoint(center.X + (radius * Math.Cos(angle)), center.Y + (radius * Math.Sin(angle)));

                angle += angleStep;
            }

            return circlePoints;
        }


        public static DoublePoint[] createEllipsePoints(int radiusX, int radiusY, DoublePoint center, int nrPoints)
        {
            DoublePoint[] ellipsePoints = new DoublePoint[nrPoints];
            double angle = 0;
            double angleStep = 2 * ControlStrategy.PI / nrPoints;
            for (int i = 0; i < nrPoints; i++)
            {
                ellipsePoints[i] = new DoublePoint(center.X + (radiusX * Math.Cos(angle)), center.Y + (radiusY * Math.Sin(angle)));
                angle += angleStep;
            }
            return ellipsePoints;
        }


        public static DoublePoint[] createRectanglePoints(DoublePoint startPoint, int hSide, int vSide)
        {
            DoublePoint[] rectanglePoints = new DoublePoint[4];

            rectanglePoints[0] = startPoint;
            rectanglePoints[1] = new DoublePoint(startPoint.X + hSide, startPoint.Y);
            rectanglePoints[2] = new DoublePoint(startPoint.X + hSide, startPoint.Y + vSide);
            rectanglePoints[3] = new DoublePoint(startPoint.X, startPoint.Y + vSide);

            return rectanglePoints;
        }

        public static DoublePoint[] createLinePoints(DoublePoint startPoint, DoublePoint stopPoint, int nrPoints)
        {
            DoublePoint[] linePoints = new DoublePoint[nrPoints];

            double distanceX = Math.Abs(stopPoint.X - startPoint.X)/nrPoints;
            double distanceY = Math.Abs(stopPoint.Y - startPoint.Y)/nrPoints;

            for (int i = 0; i < nrPoints; i++)
            {
                linePoints[i].X = startPoint.X + i * distanceX;
                linePoints[i].Y = startPoint.Y + i * distanceY;
            }
                        
            return linePoints;
        }

        public static DoublePoint[] createXYAxis8(int radius, DoublePoint intersectionPoint, int nrPointsPerCircle)
        {
            DoublePoint[] infLine = new DoublePoint[2 * (nrPointsPerCircle + 5)];
            double angle = ControlStrategy.PI / 2;
            double angleStep = ControlStrategy.PI / (2 * (nrPointsPerCircle - 1));

            infLine[0] = intersectionPoint;
            infLine[1] = new DoublePoint(intersectionPoint.X, intersectionPoint.Y + radius / 2);
            infLine[2] = new DoublePoint(intersectionPoint.X, intersectionPoint.Y + radius*3/4);

            for (int i = 3; i < nrPointsPerCircle + 3; i++)
            {
                infLine[i] = new DoublePoint(intersectionPoint.X + radius * Math.Cos(angle), intersectionPoint.Y + radius * Math.Sin(angle));
                angle -= angleStep;
            }
            infLine[nrPointsPerCircle + 3] = new DoublePoint(intersectionPoint.X + radius * 3 / 4, intersectionPoint.Y);
            infLine[nrPointsPerCircle + 4] = new DoublePoint(intersectionPoint.X + radius / 2, intersectionPoint.Y);
            infLine[nrPointsPerCircle + 5] = intersectionPoint;
            infLine[nrPointsPerCircle + 6] = new DoublePoint(intersectionPoint.X - radius / 2, intersectionPoint.Y);
            infLine[nrPointsPerCircle + 7] = new DoublePoint(intersectionPoint.X - radius*3/4, intersectionPoint.Y);

            angle = ControlStrategy.PI;
            for (int i = nrPointsPerCircle + 8; i < 8 + 2*nrPointsPerCircle; i++)
            {
                infLine[i] = new DoublePoint(intersectionPoint.X + radius * Math.Cos(angle), intersectionPoint.Y + radius * Math.Sin(angle));
                angle += angleStep;
            }

            infLine[8 + 2 * nrPointsPerCircle] = new DoublePoint(intersectionPoint.X, intersectionPoint.Y - radius * 3 / 4);
            infLine[9 + 2*nrPointsPerCircle] = new DoublePoint(intersectionPoint.X, intersectionPoint.Y - radius / 2);

            return infLine;
        }

        public override void initializeStrategyPanel()
        {
            directionBox = new CheckBox();
            directionBox.Text = "Direction";
            directionBox.Location = new System.Drawing.Point(10, 20);
            directionBox.CheckedChanged += directionBox_CheckedChanged;
            

            this.getStrategySettingsBox().Controls.Add(directionBox);
        }
    }
}
