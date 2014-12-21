using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AForge;

namespace RobotSwarmServer.Control_Strategies.Strategies.Collision_Avoidance.FIFO
{
    public class CollisionArea
    {
        Rectangle area;
        DoublePoint middlePoint;

        DoublePoint[] points; //Clockwise from down left corner
        DoublePoint[] headingEdges;
        /*
         * Location at the lower right corner
         */
        public CollisionArea(System.Drawing.Point location, Size size)
        {
            area = new Rectangle(location, size);
            middlePoint = new DoublePoint(location.X + area.Width/2, location.Y + area.Height/2);

            points = new DoublePoint[16];
            points[0] = new DoublePoint(area.X, area.Y);
            points[1] = new DoublePoint(area.X, area.Y + area.Height / 4);
            points[2] = new DoublePoint(area.X, area.Y + area.Height / 2);
            points[3] = new DoublePoint(area.X, area.Bottom - area.Height / 4);
            points[4] = new DoublePoint(area.X, area.Bottom);
            points[5] = new DoublePoint(area.X + area.Width / 4, area.Bottom);
            points[6] = new DoublePoint(area.X + area.Width / 2, area.Bottom);
            points[7] = new DoublePoint(area.Right - area.Width / 4, area.Bottom);
            points[8] = new DoublePoint(area.Right, area.Bottom); //Upper right corner
            points[9] = new DoublePoint(area.Right, area.Bottom - area.Height / 4);
            points[10] = new DoublePoint(area.Right, area.Y + area.Height / 2);
            points[11] = new DoublePoint(area.Right, area.Y + area.Height / 4);
            points[12] = new DoublePoint(area.Right, area.Y);
            points[13] = new DoublePoint(area.Right - area.Width / 4, area.Y);
            points[14] = new DoublePoint(area.X + area.Width / 2, area.Y);
            points[15] = new DoublePoint(area.X + area.Width/4, area.Y);

            headingEdges = new DoublePoint[2];
        }
        /*
         *  Returns the distance the robot have to travel to get to the area's middle if it would not change it's heading
         *  To get a correct answer, the area have to be quadratic
         */
        public int distance2Robot(Robot robot)
        {
            int distance = distance2Middle(robot.getPosition());
            return distance;
        }
        public int distance2Middle(AForge.DoublePoint point)
        {
            return (int)middlePoint.DistanceTo(point);
        }
        /*
         *  Returns the time it would take for the robot at current speed and heading to reach the area
         */
        public int time2Robot(Robot robot)
        {
            return time2Point(robot.getPosition(), robot.getSpeed());
        }
        public int time2Point(AForge.DoublePoint point, double speed)
        {
            int distance = distance2Middle(point);
            int time = (int)(distance / speed);

            return time;
        }

        public AForge.DoublePoint getNearestPoint(AForge.DoublePoint point)
        {
            DoublePoint closestPoint = points[0];
            double distance2P, minDistance = closestPoint.DistanceTo(point);
            foreach (DoublePoint p in points)
            {
                distance2P = point.DistanceTo(p) ;
                if (distance2P < minDistance)
                {
                    closestPoint = p;
                    minDistance = closestPoint.DistanceTo(point);
                }
            }
            
            return closestPoint;
        }

        public bool intersects(Rectangle rect){
            return area.IntersectsWith(rect);
        }
        public bool intersects(DoublePoint robotPosition)
        {
            return (area.X < robotPosition.X && area.Right > robotPosition.X && area.Y < robotPosition.Y && area.Bottom > robotPosition.Y);
        }
        
        public int getX()
        {
            return area.X;
        }
        public int getY()
        {
            return area.Y;
        }
        public int getWidth()
        {
            return area.Width;
        }
        public int getHeight()
        {
            return area.Height;
        }
        public int getRight()
        {
            return area.Right;
        }
        public int getBottom()
        {
            return area.Bottom;
        }
        public Size getSize()
        {
            return area.Size;
        }

        public DoublePoint getPoint(int index)
        {
            return points[index];
        }
        public DoublePoint[] getHeadingEdges(AForge.DoublePoint robotPosition, AForge.DoublePoint robotHeading)
        {
            ControlStrategy.isAtHeading(this, robotPosition, robotHeading);
            return headingEdges;
        }

        internal void setHeadingEdges(DoublePoint corner1, DoublePoint corner2)
        {
            headingEdges[0] = corner1;
            headingEdges[1] = corner2;
        }
    }
}
