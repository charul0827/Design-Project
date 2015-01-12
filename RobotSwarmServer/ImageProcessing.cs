/*
 * 
 * ImageProcessing
 * This is used by the positioning system. It’s main component is a 
 * timer that initiate the analysing of a new image frame. The timer starts a 
 * sequence of functions that grabs images from the cameras, processes the images, 
 * locates the robots and updates the robots with their new positions. It also 
 * draws the automata that visualises the communication range of the robots.
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing; 
using System.Diagnostics;

using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Vision.GlyphRecognition;



namespace RobotSwarmServer
{
    public partial class ImageProcessing : UserControl
    {
        private GlyphRecognizer recognizer; // includes all known glyphs
        public List<Robot> robotsInFrame = new List<Robot>(); // includes all robots that is found in frame
        private List<Robot> robotsNotInFrame = new List<Robot>(); // includes all robots that is not found in frame
        public double glyphScalingFactor = 1; // Required by GRATF to scale up the images when using low resolution
        List<Robot> tempNeighborList = new List<Robot>(); // used to calculate neighbors

        private System.Drawing.PointF[] automataPositions;// Needed for the Neighbor Graph

        public bool firstFrame = true; // used to enable search in entire image at first frame only
        private int[] robotDetectionFailures; // logs the number of times a robot has not been found
        private int failuresAllowedSmallSize = 10; // allowed failures before searching in full scale video

        public ImageProcessing()
        {
            InitiateTimer();
            InitializeComponent();
            CreateGlyphRecognizer();
            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest;
        }
        
        // Initiates the timer that controls the positioning system
        private void InitiateTimer()
        {
            Program.timerFrameToProcessing = new Timer();
            Program.timerFrameToProcessing.Tick += new EventHandler(this.FrameToProcessingTimer_Tick); // Everytime timer ticks, frameToProcessingTimer_Tick will be called
            //Program.timerFrameToProcessing.Interval = 20;
            Program.timerFrameToProcessing.Interval = 20;
        }

        // Find glyphs in image, returns list with extracted data
        public List<ExtractedGlyphData> GetGlyphPosition(Bitmap image)
        {
            List<ExtractedGlyphData> glyphs;
            if (glyphScalingFactor != 1)
            {
                image = new Bitmap(image, (int)(image.Width * glyphScalingFactor), (int)(image.Height * glyphScalingFactor));
                glyphs = recognizer.FindGlyphs(image);
                for (int i = 0; i < glyphs.Count; i++)
                {
                    for (int j = 0; j < glyphs[i].Quadrilateral.Count; j++)
                    {
                        glyphs[i].Quadrilateral[j] = new IntPoint((int)(glyphs[i].Quadrilateral[j].X / glyphScalingFactor), (int)(glyphs[i].Quadrilateral[j].Y / glyphScalingFactor));
                    }
                }
            }
            else
                glyphs = recognizer.FindGlyphs(image);
            return glyphs;
        }

        /*
         * Find glyphs in image, returns list with extracted data 
         * uses information about specific camera and image position
         * to transform coordinates to global coordinatesystem
         */
        public List<ExtractedGlyphData> GetGlyphPosition(Bitmap image, Camera cam, int x = 0, int y = 0)
        {
            List<ExtractedGlyphData> glyphs = GetGlyphPosition(image);

            if (cam == null)
            {
                Console.WriteLine("Cam = null in GetGlyphPosition");
            }

            for (int i = 0; i < glyphs.Count(); i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    DoublePoint temp = translateCoordToGlobal(new AForge.DoublePoint((double)glyphs[i].Quadrilateral[j].X + x, (double)glyphs[i].Quadrilateral[j].Y + y), cam);
                    glyphs[i].Quadrilateral[j] = (IntPoint)temp;
                }
            }
            return glyphs;
        }

        // Uses list of found glyphs to update position, heading and neighbors of robots
        public void UpdateGlyphPosition(List<ExtractedGlyphData> glyphs)  
        {

            DoublePoint heading = new DoublePoint();        
            DoublePoint centerPoint = new DoublePoint();    
            IntPoint[] tempPointArray = new IntPoint[4];

            robotsInFrame.Clear();

            foreach (ExtractedGlyphData glyphData in glyphs)
            {
                double xTemp = 0, yTemp = 0;
                glyphData.Quadrilateral.CopyTo(tempPointArray);
                for (int i = 0; i < tempPointArray.Length; i++)
                {
                    xTemp += tempPointArray[i].X;
                    yTemp += tempPointArray[i].Y;
                }
                centerPoint.X = xTemp / tempPointArray.Length;
                centerPoint.Y = yTemp / tempPointArray.Length;
                heading.X = tempPointArray[1].X - tempPointArray[2].X;
                heading.Y = tempPointArray[1].Y - tempPointArray[2].Y;

                if (Program.robotPhysicalRadius == 0)
                {
                    // Program.robotPhysicalRadius = (int)((tempPointArray[0].DistanceTo(tempPointArray[2])) / 2);   //uncomment if using m3pi robots
                    
                    //$$$$$Changes/Additions made for RC cars$$$$$//
                    Program.robotPhysicalRadius = (int)((1.5*tempPointArray[0].DistanceTo(tempPointArray[2])));
                    //$$$$$$$$$$//

                    Program.robotRadius = (int)(Program.robotRadiusScaling * Program.robotPhysicalRadius);
                    Program.transmissionRange = (int)(Program.transmissionRangeScaling * Program.robotPhysicalRadius);
                    Program.dispersionRange = (int)(Program.dispersionRangeScaling * Program.robotPhysicalRadius);
                }
                
                foreach (Robot robot in Program.robotList)
                {
                    if (glyphData.RecognizedGlyph != null)
                    {
                        if (robot.getID().ToString() == glyphData.RecognizedGlyph.Name)
                        {
                            robotsInFrame.Add(robot);
                            robot.setDetected(true);
                            robot.setPosition(centerPoint);
                            robot.setHeading(heading);
                            robot.setCornerArray(tempPointArray);
                            robot.calculateSpeed(); // Note, calculateSpeed uses fresh positions, so this should always be placed after SetPosition()!!
                            break;
                        }
                    }
                }
            }
            robotsNotInFrame = Program.robotList.Except<Robot>(robotsInFrame).ToList<Robot>();

            // The following code is purely to detect and add neighbors for all robots in frame.
            foreach (Robot robot in robotsInFrame)
            {
                tempNeighborList.Clear();
                foreach (Robot possibleNeighbor in robotsInFrame)
                {
                    if (!robot.Equals(possibleNeighbor) && IsNeighbor(robot, possibleNeighbor))
                    {
                        tempNeighborList.Add(possibleNeighbor);
                    }
                }
                robot.setNeighbors(tempNeighborList);
            }

            automataCanvas.Refresh();
            DrawAutomata();
            foreach (Robot robotNotInFrame in robotsNotInFrame)
            {
                robotNotInFrame.setDetected(false);
            }

            if (Program.testFrame != null)
            {
                Program.testFrame.updateData("Glyph");
            }

        }

        // Draws the automata or the communication graph in the GUI
        private void DrawAutomata()
        {
            System.Drawing.Pen automataPen = new System.Drawing.Pen(System.Drawing.Color.Red, 5);
            System.Drawing.Pen linesPen = new System.Drawing.Pen(System.Drawing.Color.Black, 5);
            System.Drawing.Graphics formGraphics = automataCanvas.CreateGraphics();
            Label[] robotLabels = new Label[Program.numberOfRobots];
            float circleRadius = 10;
            System.Drawing.PointF center = new System.Drawing.PointF((float)automataCanvas.Width / 2 - circleRadius, (float)automataCanvas.Height / 2 - circleRadius);

            automataPositions = new System.Drawing.PointF[Program.numberOfRobots];

            float angleBetween = 2 * (float)Math.PI / Program.numberOfRobots;
            
            float bigRadius = ((float)Math.Min(automataCanvas.Width, automataCanvas.Height) * (float)0.8 - circleRadius * 2) / 2;

            float width = 2 * circleRadius;
            float height = 2 * circleRadius;


            for (int i = 0; i < Program.numberOfRobots; i++)
            {
                automataPositions[i] = new System.Drawing.PointF(center.X + (float)Math.Cos(angleBetween * i) * bigRadius + circleRadius, center.Y + (float)Math.Sin(angleBetween * i) * bigRadius + circleRadius);
                formGraphics.DrawEllipse(automataPen, automataPositions[i].X - circleRadius, automataPositions[i].Y - circleRadius, (float)width, (float)height);
            }

            foreach (Robot robot in Program.robotList)
            {
                if (robot.getDetected())
                {
                    foreach (Robot neighbor in robot.getNeighbors())
                    {
                        if (neighbor.getDetected())
                        {
                            formGraphics.DrawLine(linesPen, automataPositions[Program.robotList.IndexOf(robot)], automataPositions[Program.robotList.IndexOf(neighbor)]);
                        }
                    }
                }
            }
            linesPen.Dispose();
            automataPen.Dispose();
            formGraphics.Dispose();
        }

        // Verifies if "robot" is neighbor of "possibleNeighbor"
        private static bool IsNeighbor(Robot robot, Robot possibleNeighbor)
        {
            if (robot != null && possibleNeighbor != null && (robot.getPosition().DistanceTo(possibleNeighbor.getPosition())) <= Program.transmissionRange)
                return true;
            else
                return false;
        }

        // Used to create the glyphs and the GRATF glyph recognizer
        private void CreateGlyphRecognizer()
        {
            GlyphDatabase glyphDatabase = new GlyphDatabase(5);
            // Create glyph database for 5x5 glyphs. Ones represent white squares, 0 black.
            // Note that the ID needs to match the name of the robot

            glyphDatabase.Add(new Glyph("0", new byte[5, 5] {
                { 0, 0, 0, 0, 0 },
                { 0, 1, 0, 1, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 0, 0, 0, 0 } }));

            glyphDatabase.Add(new Glyph("1", new byte[5, 5] {
                { 0, 0, 0, 0, 0 },
                { 0, 1, 1, 0, 0 },
                { 0, 0, 1, 1, 0 },
                { 0, 1, 1, 0, 0 },
                { 0, 0, 0, 0, 0 } }));

            glyphDatabase.Add(new Glyph("2", new byte[5, 5] {
                { 0, 0, 0, 0, 0 },
                { 0, 1, 1, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 1, 0, 1, 0 },
                { 0, 0, 0, 0, 0 } }));

            glyphDatabase.Add(new Glyph("3", new byte[5, 5] {
                { 0, 0, 0, 0, 0 },
                { 0, 1, 1, 0, 0 },
                { 0, 0, 1, 1, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 0, 0, 0 } }));

            glyphDatabase.Add(new Glyph("4", new byte[5, 5] {
                { 0, 0, 0, 0, 0 },
                { 0, 1, 1, 1, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 1, 0, 1, 0 },
                { 0, 0, 0, 0, 0 } }));

            glyphDatabase.Add(new Glyph("5", new byte[5, 5] {
                { 0, 0, 0, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 1, 1, 1, 0 },
                { 0, 0, 1, 1, 0 },
                { 0, 0, 0, 0, 0 } }));

            glyphDatabase.Add(new Glyph("6", new byte[5, 5] {
                { 0, 0, 0, 0, 0 },
                { 0, 1, 1, 0, 0 },
                { 0, 1, 0, 1, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 0, 0, 0 } }));

            glyphDatabase.Add(new Glyph("7", new byte[5, 5] {
                { 0, 0, 0, 0, 0 },
                { 0, 0, 1, 1, 0 },
                { 0, 1, 1, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 0, 0, 0, 0 } }));

            glyphDatabase.Add(new Glyph("8", new byte[5, 5] {
                { 0, 0, 0, 0, 0 },
                { 0, 1, 1, 1, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 1, 0, 1, 0 },
                { 0, 0, 0, 0, 0 } }));

            glyphDatabase.Add(new Glyph("9", new byte[5, 5] {
                { 0, 0, 0, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 0, 0, 1, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 0, 0, 0 } }));

            recognizer = new GlyphRecognizer(glyphDatabase);
            recognizer.MaxNumberOfGlyphsToSearch = glyphDatabase.Count;
        }

        // Main event handler of positionining system, this is called by the timer
        private void FrameToProcessingTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                UpdateGlyphPosition(GetGlyphsFromSquares());
                Program.mainFrame.logPositionData();
            }
            catch (Exception error)
            {
                Console.WriteLine("Caught excetion in FrameToProcessingTimer_Tick. Message: " + error.Message);
            }


            
        }

        // Used by FrameToProcessingTimer_Tick to locate all glyphs
        private List<ExtractedGlyphData> GetGlyphsFromSquares()
        {

            List<Camera> cams = new List<Camera>(Program.cameraController.getIncludedCameras());
            List<Bitmap> images = new List<Bitmap>(CameraController.GrabAllFrames());
            int numberOfCameras = cams.Count;



            if (images.Count != numberOfCameras)
            {
                Console.WriteLine("Error: NumberOfCameras != images.Count");
                return new List<ExtractedGlyphData>();
            }

            //Console.WriteLine("Antal bilder i starten av GetGlyphs: " + images.Count);
            foreach (Bitmap image in images)
            {
                if (image == null)
                {
                    Console.WriteLine("Image = null in start of getGlyphsFromSquares");
                }
            }
            for (int i = 0; i < cams.Count; i++)
            {
                if (cams[i] == null)
                {
                    Console.WriteLine("Cam: " + i + " = null in start of getGlyphsFromSquares");
                    return new List<ExtractedGlyphData>();
                }
            }

            List<ExtractedGlyphData> glyphs = new List<ExtractedGlyphData>();

            if (firstFrame)
            {
                robotDetectionFailures = new int[Program.numberOfRobots];
                for (int i = 0; i < numberOfCameras; i++)
                {

                    List<ExtractedGlyphData> tempGlyphs = GetGlyphPosition(images[i], cams[i]);
                    foreach (ExtractedGlyphData g2 in tempGlyphs)
                        if (glyphs.Count(
                            delegate(ExtractedGlyphData g1)
                            {
                                return g1.RecognizedGlyph.Name == g2.RecognizedGlyph.Name;
                            }) == 0)
                        {
                            int robotID = Convert.ToInt32(g2.RecognizedGlyph.Name);
                            if (robotID < Program.numberOfRobots)
                            {
                                glyphs.Add(g2);
                                robotDetectionFailures[robotID] = 0;
                            }
                        }
                }
                if (glyphs.Count == Program.numberOfRobots)
                {
                    firstFrame = false;
                }
            }
            else
            {
                Bitmap square;
                Graphics g;

                // If any of the robots has not been found in atleast "failuresAllowed" tries, the whole image is analysed
                for (int i = 0; i < Program.numberOfRobots; i++)
                {
                    if (robotDetectionFailures[i] == this.failuresAllowedSmallSize){
                        firstFrame = true;
                        return GetGlyphsFromSquares();
                    }
                }

                IntPoint position;
                int left, right, top, bottom, x, y;
                foreach (Robot robot in Program.robotList)
                {
                    position = (IntPoint)robot.getPosition();
                    
                    for (int j = 0; j < numberOfCameras; j++)
                    {
                        bool foundI = false;
                        foreach (ExtractedGlyphData glyphData in glyphs)
                        {
                            if (glyphData.RecognizedGlyph != null && robot.getID().ToString() == glyphData.RecognizedGlyph.Name)
                            {
                                foundI = true;
                                break;
                            }
                        }
                        if (foundI)
                            break;

                        // Set square size
                        int squareSize = (int)(Program.squareSize * Program.robotRadius);

                        //Double the size if robot not found
                        if (!robot.getDetected()) {
                            robotDetectionFailures[robot.getID()]++;
                            squareSize *= 2;
                        }
                        else
                        {
                            robotDetectionFailures[robot.getID()] = 0;
                        }
                        if (j < 0 || j >= cams.Count)
                        {
                            Console.WriteLine("j out of bound, j = " + j + " count: " + cams.Count);
                        }
                        if (cams[j] == null)
                        {
                            Console.WriteLine("Cam nr: " + j + " == null in getGlyphsFromSquares before translateCoordFromGlobal");
                        }
                        DoublePoint localPosition = translateCoordFromGlobal((DoublePoint)position, cams[j]);

                        if (localPosition.X + squareSize / 2 > 0
                            && localPosition.X - squareSize / 2 < Program.resolution.X
                            && localPosition.Y + squareSize / 2 > 0
                            && localPosition.Y - squareSize / 2 < Program.resolution.Y)
                        {
                            x = (int)localPosition.X;
                            y = (int)localPosition.Y;
                            left = System.Math.Min(squareSize / 2, x);
                            right = System.Math.Min(squareSize / 2, Program.resolution.X - x);
                            top = System.Math.Min(squareSize / 2, y);
                            bottom = System.Math.Min(squareSize / 2, Program.resolution.Y - y);
                            
                            square = new Bitmap(left + right, top + bottom);
                            g = Graphics.FromImage(square);

                            g.DrawImage(images[j], left-x, top-y);

                            try
                            {
                                List<ExtractedGlyphData> tempGlyphs = GetGlyphPosition(square, cams[j], x - left, y - top);
                                foreach (ExtractedGlyphData g2 in tempGlyphs)
                                    if (glyphs.Count(
                                        delegate(ExtractedGlyphData g1)
                                        {
                                            return g1.RecognizedGlyph.Name == g2.RecognizedGlyph.Name;
                                        }) == 0)
                                        glyphs.Add(g2);
                                g.Dispose();
                                square.Dispose();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Failure in ImageProcessing");
                            }
                        }
                    }
                }
            }

            return glyphs;
        }

        // Rotate image
        public static Bitmap rotateImage(Bitmap img, float rotationAngle, DoublePoint rotationCoord)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.TranslateTransform((float)rotationCoord.X, (float)rotationCoord.Y);
            gfx.RotateTransform(rotationAngle);
            gfx.DrawImage(img, -(int)rotationCoord.X, -(int)rotationCoord.Y, img.Width, img.Height);
            gfx.Dispose();
            return bmp;
        }

        // Calculates the angle between the the x-axis and vector from point p1 to p2
        public static double angleX(DoublePoint p1, DoublePoint p2)
        {
            return Math.Atan2((p1.X - p2.X), (p1.Y - p2.Y));
        }

        // Used by camera calibration to calculate new position of camera
        public static DoublePoint translateNewPosition(DoublePoint p, DoublePoint refCoord,DoublePoint transCoord, double angleToRef, double sizeScaling)
        {
            double distToPoint = p.DistanceTo(refCoord);
            double angleToPoint = angleX(refCoord, p);

            double newX = transCoord.X - distToPoint * sizeScaling * Math.Sin(angleToPoint - angleToRef);
            double newY = transCoord.Y - distToPoint * sizeScaling * Math.Cos(angleToPoint - angleToRef);

            return new DoublePoint(newX, newY);
        }

        // globalCoords refer to translation from local to global coordinate system
        public static DoublePoint translateCoordToGlobal(DoublePoint p, Camera cam) 
        {
            if (cam != null) {
                DoublePoint zero = new DoublePoint(0, 0);
                double distToPoint = p.DistanceTo(zero);
                double angleToPoint = angleX(zero, p);

                double newX = cam.camPosition.X * Program.calibrationScaling.X - distToPoint * cam.sizeScaling * Math.Sin(angleToPoint - cam.angleToRef);
                double newY = cam.camPosition.Y * Program.calibrationScaling.Y - distToPoint * cam.sizeScaling * Math.Cos(angleToPoint - cam.angleToRef);

                return new DoublePoint(newX, newY);
            }
            Console.WriteLine("ERROR: A failure has accured in translateCoordToGlobal(), the camera parameter had a null value.");
            return p;
        }

        // globalCoords refer to translation from local to global coordinate system
        public static DoublePoint translateCoordFromGlobal(DoublePoint p, Camera cam) 
        {
            if (cam != null) {
                DoublePoint zero = new DoublePoint(0, 0);

                p.X -= cam.camPosition.X * Program.calibrationScaling.X;
                p.Y -= cam.camPosition.Y * Program.calibrationScaling.Y;

                double distToPoint = p.DistanceTo(zero);
                double angleToPoint = angleX(p, zero);

                double newX = distToPoint / cam.sizeScaling * Math.Sin(angleToPoint + cam.angleToRef);
                double newY = distToPoint / cam.sizeScaling * Math.Cos(angleToPoint + cam.angleToRef);

                return new DoublePoint(newX, newY);
            }
            Console.WriteLine("ERROR: A failure has accured in translateCoordFromGlobal(), the camera parameter had a null value.");
            return p;
        }
    }
}
