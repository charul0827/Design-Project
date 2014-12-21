/*
 * CollisionFreeNavigastion is a form of collision avoidance that lets the robots detect in advance if 
 * they are on a collision course. It's theory can be read about in J. Snape, J. van den Berg, S. J. Guy, D. Manocha (2011). "The hybrid
 * reciprocal velocity obstacle".
 */

using AForge;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace RobotSwarmServer.Control_Strategies.Strategies.Collision_Avoidance
{
    class CollisionFreeNavigation : ControlStrategy
    {
        Vector robotPos, velocity, velocityPoint;
        List<Robot> neighborRobot;
        List<VelocityObstacle> obstacleList;
        List<Vector> permissibleVelocities;
        Vector zero = new Vector(0, 0);

        //Speedscaling temporarily increases the length of the velocityvector to make the 
        //robots detect collision courses earlier. 5 is a good value.
        double velocityScaling = 5;
        //speedScaling scales the maximum speed of the robots to get a more stable behaviour. 0.5 is a good value.
        double speedScaling = 0.5;
        //radiusScaling scales the size of the collision radius to get more margin from collision. 1.3 is a good value
        double radiusScaling = 1.3;
        //vertexOffsetScaling is used to translate VO vertex from the robot position to an average of the 2 colliding robots
        //velocities. By setting it to 0 the translation is disabled. This is disabled because the behaviour were more stable
        //that way. In future development it may be prefered to use this.
        double vertexOffsetScaling = 0;


        public CollisionFreeNavigation() 
            : base("CollisionAvoidance")
        {

        }

        public override void calculateNextMove(DoublePoint position, double speed, DoublePoint head, List<Robot> neighbors, out double refSpeed, out DoublePoint refHeading)
        {
            permissibleVelocities = new List<Vector>();
            //Converts heading from DoublePoint to Vector since Vector is more usable.
            Vector heading = new Vector(head.X, head.Y);
            //Creates the velocityvector by combining the heading and speed of the robot.
            velocity = heading * speed * velocityScaling;
            robotPos = new Vector(position.X, position.Y);
            //velocityPoint contains the coordinates of the end of the velocityvector.
            velocityPoint = robotPos + velocity;
            neighborRobot = neighbors;

            //Step 1 is to calculate the forbidden area, the Velocity Obstacle VO.
            calculateVO();

            //If the velocityvector is inside the VO it has to be changed.
            if (!pointOutsideVO(velocityPoint))
            {
                //Step 2 is  to calculate the permissible (allowed) velocities that is outside the VO.
                calculatePermissibleVelocities();
                /*
                 * If ther exists any permissible velocities, the one closest to the prefered velocity is chosen.
                 */
                if (permissibleVelocities.Count != 0)
                {
                    Vector newVelocityPoint = permissibleVelocities[0];
                    //To calculate the normal length of a vector, a costly square root has to be performed. Therefore LengthSquared, which is faster, is used instead.
                    double previousDist = (newVelocityPoint - velocityPoint).LengthSquared;
                    foreach (Vector permissibleVelocityPoint in permissibleVelocities)
                    {
                        double thisDist = (permissibleVelocityPoint - velocityPoint).LengthSquared;
                        if (thisDist < previousDist)
                        {
                            newVelocityPoint = permissibleVelocityPoint;
                        }
                        previousDist = thisDist;
                    }
                    velocity = newVelocityPoint - robotPos;
                }
                //If no permissible velocity exists, the velocity is set to zero.
                else
                {
                    velocity = zero;
                }
            }

            refHeading = new DoublePoint(velocity.X, velocity.Y);
            refSpeed = Math.Min(Program.robotMaxSpeed * speedScaling, velocity.Length / velocityScaling);
        }

        /*
         * Calculates a forbidden area, the velocity obstacle VO. If the velocity vector is inside 
         * this, the robot will collide with a neighbor robot. The VO is shaped like a cone with its vertex
         * at the robot position and the 2 edges tangent to the collision radius of the neighbor robot.
         * The cone is defined by 2 vectors representing the 2 edges and a vector point representing its vertex.
         */
        private void calculateVO()
        {
            double collisionRadius = 2 * Program.robotRadius * radiusScaling;
            obstacleList = new List<VelocityObstacle>();

            foreach (Robot neighbor in neighborRobot)
            {
                Vector neighborHeading = new Vector(neighbor.getHeading().X, neighbor.getHeading().Y);
                Vector neighborVelocity = neighborHeading * neighbor.getSpeed() * velocityScaling;
                Vector neighborVector = new Vector(neighbor.getPosition().X, neighbor.getPosition().Y);
                Vector vectorToNeighbor = neighborVector - robotPos;
                double radiusRelativeDist = collisionRadius / vectorToNeighbor.Length;
                //The VOangle is half of the angle of the cone that defines the VO.
                double VOangle;
                //Arcsinus is not defined for values larger than 1. Though if the robot drives inside the neighbor
                //robot's collision radius the radiusRelativeDist gets larger than 1. In that case the VOangle is set to 90 degrees.
                if (radiusRelativeDist <= 1)
                {
                    VOangle = Math.Asin(radiusRelativeDist);
                }
                else
                {
                    VOangle = Math.PI / 2;
                }

                double sinAngle = Math.Sin(VOangle);
                double cosAngle = Math.Cos(VOangle);

                Matrix rotationMatrixClockwise = new Matrix(cosAngle, sinAngle, -sinAngle, cosAngle, 0, 0);
                Matrix rotationMatrixCounterClockwise = new Matrix(cosAngle, -sinAngle, sinAngle, cosAngle, 0, 0);

                Vector rightEdge = vectorToNeighbor * rotationMatrixClockwise;
                Vector leftEdge = vectorToNeighbor * rotationMatrixCounterClockwise;
                
                Vector coneVertex = robotPos + vertexOffsetScaling * (neighborVelocity + velocity) / (2 * speedScaling);
                VelocityObstacle tempVO = new VelocityObstacle(rightEdge, leftEdge, vectorToNeighbor, coneVertex);

                /*
                 * An additional method which is used to enlarge the VO additionally. If the velocity vector is inside VO and left
                 * of the centerline, the VO is enlarged to the right and if the velocity vector is inside VO and right of the
                 * centerline, the VO is enlarged to the left. This makes it easier for the robot to chose the correct side to
                 * drive pass the neighbor. It is further explained in (J. Snape, J. van den Berg, S. J. Guy, D. Manocha (2011) "The hybrid
                 * reciprocal velocity obstacle") where it is called "The hybrid reciprocal velocity obstacle". It is disabled here since 
                 * it didn't work according to plan but should work better with some adjustments.
                 */
                /*
                int test = pointRelativeCenterline(velocityPoint, temp);
                //test = -1 means that the velocity vector is inside VO and on the left side of the centerline
                if (test == -1)
                {
                    tempVO.vertex = findLineIntersection(robotPos + neighborVelocity, temp.rightEdge, temp.vertex, temp.leftEdge);
                }
                //test = -1 means that the velocity vector is inside VO and on the right side of the centerline
                else if (test == 1)
                {
                    tempVO.vertex = findLineIntersection(robotPos + neighborVelocity, temp.leftEdge, temp.vertex, temp.rightEdge);
                }*/

                obstacleList.Add(tempVO);
            }
        }

        /*
         * Calculates the permissible velocities for the robot. Permissible velocities is outside the VO and should be as 
         * close to the prefered velocity as possible. They consists partly of the intersections between the edges of VO,
         * and partly of the velocity vector projected on the edges of VO.
         */ 
        private void calculatePermissibleVelocities()
        {
            //Calculates the permissible velocities in the intersections between the edges of VO
            for (int i = 0; i < obstacleList.Count - 1; i++)
            {
                VelocityObstacle VO1 = obstacleList[i];
                for (int j = i + 1; j < obstacleList.Count; j++)
                {
                    VelocityObstacle VO2 = obstacleList[j];
                    Vector possibleVelocity = findRayIntersection(VO1.vertex, VO1.leftEdge, VO2.vertex, VO2.leftEdge);
                    if (possibleVelocity != zero)
                    {
                        if (pointOutsideVO(possibleVelocity))
                        {
                            permissibleVelocities.Add(possibleVelocity);
                        }
                    }

                    possibleVelocity = findRayIntersection(VO1.vertex, VO1.leftEdge, VO2.vertex, VO2.rightEdge);
                    if (possibleVelocity != zero)
                    {
                        if (pointOutsideVO(possibleVelocity))
                        {
                            permissibleVelocities.Add(possibleVelocity);
                        }
                    }

                    possibleVelocity = findRayIntersection(VO1.vertex, VO1.rightEdge, VO2.vertex, VO2.leftEdge);
                    if (possibleVelocity != zero)
                    {
                        if (pointOutsideVO(possibleVelocity))
                        {
                            permissibleVelocities.Add(possibleVelocity);
                        }
                    }

                    possibleVelocity = findRayIntersection(VO1.vertex, VO1.rightEdge, VO2.vertex, VO2.rightEdge);
                    if (possibleVelocity != zero)
                    {
                        if (pointOutsideVO(possibleVelocity))
                        {
                            permissibleVelocities.Add(possibleVelocity);
                        }
                    }
                }
            }

            //Calculates the permissible velocities that is the velocity vector projected on the edges of VO
            foreach (VelocityObstacle VO in obstacleList)
            {
                Vector pointOnEdge = projectPointOnRay(VO.vertex, VO.leftEdge, velocityPoint);
                if (pointOnEdge != zero)
                {
                    if (pointOutsideVO(pointOnEdge))
                    {
                        permissibleVelocities.Add(pointOnEdge);
                    }
                }

                pointOnEdge = projectPointOnRay(VO.vertex, VO.rightEdge, velocityPoint);
                if (pointOnEdge != zero)
                {
                    if (pointOutsideVO(pointOnEdge))
                    {
                        permissibleVelocities.Add(pointOnEdge);
                    }
                }
            }
        }

        /*
         * Calculates the intersection between two rays and returns the intersection point as a Vector type.
         * Ray 1 = start point: Vector p1, direction: Vector v1
         * Ray 2 = start point: Vector p2, direction: Vector v2
         * Returns a zero vector if the rays are parallel or if the intersection is behind the rays start point
         */ 
        private Vector findRayIntersection(Vector p1, Vector v1, Vector p2, Vector v2)
        {
            double denominator = Vector.Determinant(v1, v2);
            double numerator = Vector.Determinant(v2, p1 - p2);
            //If the denominator is zero the rays are parallel
            if (denominator == 0)
            {
                return zero;  
            }
            else
            {
                double scaling1 = numerator / denominator;
                double scaling2 = (p1.X - p2.X + scaling1 * v1.X) / v2.X;
                //If either of the scaling variables is negative the intersection is behind ray start point
                if (scaling1 < 0 || scaling2 < 0)
                {
                    return zero; 
                }
                //Intersection is valid
                else
                {
                    return p1 + scaling1 * v1;
                }
            }

        }

        /*
         * Calculates the intersection between two lines and returns the intersection point as a Vector type.
         * Line 1 = point on line: Vector p1, direction: Vector v1
         * Line 2 = point on line: Vector p2, direction: Vector v2
         * Returns a zero vector if the lines are parallel
         */
        private Vector findLineIntersection(Vector p1, Vector v1, Vector p2, Vector v2)
        {
            double denominator = Vector.Determinant(v1, v2);
            double numerator = Vector.Determinant(v2, p1 - p2);
            //Lines are parallel
            if (denominator == 0)
            {
                return zero;  
            }
            else
            {
                return p1 + (numerator / denominator) * v1;
            }
        }

        /*
         * Determines if the point p is outside the VO.
         * Returns true if outside VO
         * Returns false if inside VO
         */ 
        private bool pointOutsideVO(Vector p)
        {
            foreach (VelocityObstacle VO in obstacleList)
            {
                Vector pToVertex = p - VO.vertex;
                double uncertaintyAngleRange = 0.02;
                double pAngle = angleToXAxis(pToVertex);
                double lAngle = angleToXAxis(VO.leftEdge);
                double rAngle = angleToXAxis(VO.rightEdge);

                //Fix the angles so that they can be compared
                if (lAngle > rAngle)
                {
                    if (pAngle < 0)
                    {
                        lAngle -= Math.PI * 2;
                    }
                    else
                    {
                        rAngle += Math.PI * 2;
                    }
                }

                //If (lAngle < pAngle < rAngle) return false. An uncertaintyAngleRange is added since some permissible
                //velocities that is positioned exactly on the edge should be counted as outside the VO.
                if ((lAngle + uncertaintyAngleRange) < pAngle && pAngle < (rAngle - uncertaintyAngleRange))
                {
                    return false;
                }
            }
            return true;
        }

        /*
         * Detirmines if the velocity vector is on the left or right side of the VO if its inside the VO.
         * Returns -1 if the velocity vector is inside the VO and left of the centerline
         * Returns 1 if the velocity vector is inside the VO and right of the centerline
         * returns 0 if the velocity vector is outside the VO
         */
        private int pointRelativeCenterline(Vector p, VelocityObstacle VO)
        {
            Vector pToVertex = p - VO.vertex;
            double pAngle = angleToXAxis(pToVertex);
            double lAngle = angleToXAxis(VO.leftEdge);
            double rAngle = angleToXAxis(VO.rightEdge);
            double centerlineAngle = angleToXAxis(VO.centerline);

            //Fix the angles so that they can be compared
            if (lAngle > rAngle)
            {
                if (pAngle < 0)
                {
                    lAngle -= Math.PI * 2;
                }
                else
                {
                    rAngle += Math.PI * 2;
                }
                if (centerlineAngle < lAngle)
                {
                    centerlineAngle += Math.PI * 2;
                }
                else if (centerlineAngle > rAngle)
                {
                    centerlineAngle -= Math.PI * 2;
                }
            }

            if (lAngle < pAngle && pAngle < rAngle)
            {
                if (pAngle < centerlineAngle)
                {
                    return -1;
                }
                return 1;
            }
            return 0;
        }

        /*
         * Calculates the projection of a point on a ray
         * Returns the projection point if its valid
         * Returns a zero vector if the projected point is behind the ray start point
         */ 
        private Vector projectPointOnRay(Vector rayPoint, Vector rayDirection, Vector point)
        {
            double scaling = ((point - rayPoint) * rayDirection);
            //Point is behind ray start point
            if (scaling < 0)
            {
                return zero; 
            }
            else
            {
                return rayPoint + scaling * rayDirection / rayDirection.LengthSquared;
            }
        }


        private double angleToXAxis(Vector v)
        {
            return Math.Atan2(v.Y, v.X);
        }

        public override ControlStrategy cloneStrategy()
        {
            return new CollisionFreeNavigation();
        }


        public override void paintStrategy(Graphics g, DoublePoint scaling)
        {
            try
            {
                //Paints Velocity Obstacles
                foreach (VelocityObstacle VO in obstacleList)
                {
                    if (VO.rightEdge != null && VO.vertex != null && VO.leftEdge != null)
                    {
                        paintRay(VO.vertex, VO.rightEdge, Color.Red, g, scaling);
                        paintRay(VO.vertex, VO.leftEdge, Color.Black, g, scaling);
                    }
                }

                //Paints permissible velocities
                foreach (Vector v in permissibleVelocities)
                {
                    if (robotPos != null)
                    {
                        drawPoint(v, Color.Green, g, scaling);
                    }
                }

                //Paint robot velocity
                if (velocity != null && robotPos != null)
                {
                    paintRay(robotPos, velocity, Color.Blue, g, scaling);

                    if (!pointOutsideVO(robotPos + velocity))
                    {
                        drawPoint(robotPos + velocity, Color.Red, g, scaling);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught Exception :" + e.Message);
            }
        }

        private void paintRay(Vector point, Vector directionVector, Color color, Graphics g, DoublePoint scale)
        {
            System.Drawing.Point startPoint = new System.Drawing.Point(Convert.ToInt32(point.X * scale.X), Convert.ToInt32(point.Y * scale.Y));
            System.Drawing.Point directionPoint = new System.Drawing.Point(startPoint.X + Convert.ToInt32(directionVector.X * scale.X), startPoint.Y + Convert.ToInt32(directionVector.Y * scale.Y));
            Pen pen = new Pen(color);
            g.DrawLine(pen, startPoint, directionPoint);
        }
        private void drawPoint(Vector point, Color color, Graphics g, DoublePoint scale)
        {
            Brush robotBrush = new SolidBrush(color);
            System.Drawing.Point p = new System.Drawing.Point(Convert.ToInt32((point.X) * scale.X), Convert.ToInt32((point.Y) * scale.Y));
            g.FillEllipse(robotBrush, new Rectangle(p, new System.Drawing.Size(Convert.ToInt32(scale * 6), Convert.ToInt32(scale * 6))));
        }

        public override void initializeStrategyPanel()
        {
            throw new NotImplementedException();
        }
    }
}
