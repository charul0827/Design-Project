using RobotSwarmServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSwarmServer.Control_Strategies.Strategies.Collision_Avoidance.FIFO
{
    class FIFO : ControlStrategy
    {
        A2B stopStrategy;
        CollisionArea collisionArea;

        List<int> queue;

        AForge.DoublePoint robotPosition;

        AForge.DoublePoint corner1, corner2;
        bool inter = false;

        public FIFO()
            : base("FIFO")
        {
            stopStrategy = new A2B();
            queue = new List<int>();
        }
        public FIFO(CollisionArea collisionArea)
            : this()
        {
            this.collisionArea = collisionArea;
        }
    
        public override void calculateNextMove(AForge.DoublePoint robotPosition, double speed, AForge.DoublePoint heading, List<Robot> neighbors, out double referenceSpeed, out AForge.DoublePoint referenceHeading)
        {
            if (collisionArea != null)
            {
                calculateNextMove(collisionArea, robotPosition, speed, heading, neighbors, out referenceSpeed, out referenceHeading);
            }
            else
            {
                throw new Exception("CollisionArea is null");
            }
        }
        public void calculateNextMove(CollisionArea collisionArea, AForge.DoublePoint robotPosition, double speed, AForge.DoublePoint heading, List<Robot> neighbors, out double referenceSpeed, out AForge.DoublePoint referenceHeading)
        {
            bool go = true;

            if (ControlStrategy.isAtHeading(collisionArea, robotPosition, heading))
            {
                int distance = collisionArea.distance2Middle(robotPosition);

                if (queue.Count == 0)
                {
                    foreach (Robot neighbor in neighbors)
                    {
                        if (ControlStrategy.isAtHeading(collisionArea, neighbor.getPosition(), neighbor.getHeading()))
                        {
                            if (distance > collisionArea.distance2Robot(neighbor))
                            {
                                queue.Add(neighbor.getID());
                                go = false;
                            }
                        }
                    }
                }
                else
                {
                    foreach (Robot neighbor in neighbors)
                    {
                        if (ControlStrategy.isAtHeading(collisionArea, neighbor.getPosition(), neighbor.getHeading()))
                        {
                            foreach (int ID in queue)
                            {
                                if(ID == neighbor.getID())
                                {
                                    go = false;
                                    break;
                                }
                            }
                        }
                    }
                }
                foreach (Robot neighbor in neighbors)
                {
                    if (collisionArea.intersects(neighbor.getPosition()))
                    {
                        go = false;
                        break;
                    }
                }
            }
            else
            {
                queue.Clear();
            }

            if (go)
            {
                referenceSpeed = speed;
                referenceHeading = heading;
            }
            else
            {
                stopStrategy.calculateNextMove(collisionArea.getNearestPoint(robotPosition), Program.robotRadius, robotPosition, speed, heading, neighbors, out referenceSpeed, out referenceHeading);
            }

            corner1 = collisionArea.getHeadingEdges(robotPosition, heading)[0];
            corner2 = collisionArea.getHeadingEdges(robotPosition, heading)[1];
            this.robotPosition = robotPosition;
        }

        public override ControlStrategy cloneStrategy()
        {
            if (collisionArea != null)
            {
                return new FIFO(collisionArea);
            }
            else
            {
                return new FIFO();
            }
        }

        public override void paintStrategy(Graphics g, AForge.DoublePoint scaling)
        {
            if (collisionArea != null)
            {
                Rectangle paintRectangle = new Rectangle((int)(scaling.X * collisionArea.getX()),
                (int)(scaling.Y * collisionArea.getY()),
                (int)(scaling.X * collisionArea.getWidth()), (int)(scaling.Y * collisionArea.getHeight()));
                g.FillRectangle(new SolidBrush(Color.FromArgb(50, 255, 0, 0)), paintRectangle);
            }

            if (robotPosition != null && corner1 != null && corner2 != null)
            {
                g.DrawLine(new Pen(new SolidBrush(Color.Black), 2), 
                    new Point((int)(scaling.X * robotPosition.X), (int)(scaling.Y * robotPosition.Y)),
                    new Point((int)(scaling.X * corner1.X), (int)(scaling.Y * corner1.Y)));
                g.DrawLine(new Pen(new SolidBrush(Color.Black), 2), new Point((int)(scaling.X * robotPosition.X), (int)(scaling.Y * robotPosition.Y)),
                    new Point((int)(scaling.X * corner2.X), (int)(scaling.Y * corner2.Y)));
            }
        }

        public override void initializeStrategyPanel()
        {
            throw new NotImplementedException();
        }
    }
}
