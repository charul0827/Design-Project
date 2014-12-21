/*
 * 
 * Display
 * This includes the functions that controls the display frame. 
 * Includes a Timer that updates the frame at a low interval.
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using AForge;
using System.Windows;
using RobotSwarmServer.Control_Strategies.Strategies;

namespace RobotSwarmServer
{

#pragma warning disable 168 //Disables the warnings about variable not used.
    public partial class Display : Form
    {

        private System.Timers.Timer updateDisplayTimer; // Async timer that updates the display
        private bool displayUpdating = false; // Used to prevent software collisions when using async timer
        public DoublePoint displayScaling = new DoublePoint(1, 1); // makes the video the same siaze as display frame

        // -----------------------------------------------

        public Display()
        {
            InitializeComponent();

            updateDisplayTimer = new System.Timers.Timer();
            updateDisplayTimer.Interval = 1000;
            updateDisplayTimer.Elapsed += new System.Timers.ElapsedEventHandler(updateDisplayTimer_Tick);
            updateDisplayTimer.Enabled = true;
        }

        // eventhandler from timer, updates the display frame
        private void updateDisplayTimer_Tick(object sender, EventArgs e)
        {
            if (displayUpdating)
            {
                return;
            }
            displayUpdating = true;

            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Lowest;

            //Console.WriteLine("Display Start");
            Console.WriteLine("Img size: " + Program.imgSize);
            displayScaling.X = (double)this.graphicsBox.Width / ((double)Program.imgSize.X * Program.calibrationScaling.X);
            displayScaling.Y = (double)this.graphicsBox.Height / ((double)Program.imgSize.Y * Program.calibrationScaling.Y);

            Program.cameraController.updateIncludedCameraList();
            if (Program.displayFrame != null && Program.cameraCalibrated)
            {
                if (displayVideoBox.Checked)
                {
                    List<Camera> includedCameras = Program.cameraController.getIncludedCameras();
                    if (includedCameras.Count > 0)
                    {
                        Bitmap image = new Bitmap(10, 10);
                        if (includedCameras.Count > 1)
                        {
                            image = Program.cameraController.GrabMergedFrame();
                        }
                        else if (includedCameras.Count == 1)
                        {
                            image = CameraController.GrabOneFrame(includedCameras[0]);
                        }
                        else
                        {
                            MessageBox.Show("No camera selected");
                        }
                        this.splitContainer1.Panel2.BackgroundImage = new Bitmap(image, new System.Drawing.Size(this.graphicsBox.Width, this.graphicsBox.Height));
                        image.Dispose();
                    }
                }

                if (enableGraphicsBox.Checked)
                {
                    this.aRefresh();
                    this.DrawSquare(Program.imageProcessing.robotsInFrame);
                    this.DrawNeighborLink(Program.imageProcessing.robotsInFrame);
                    this.DrawCircle(Program.imageProcessing.robotsInFrame);
                }
                if (paintStrategyBox.Checked)
                {
                    Graphics g = this.graphicsBox.CreateGraphics();
                    foreach (Robot robot in Program.robotList)
                    {
                        if (robot.currentStrategy != null)
                        {
                            robot.currentStrategy.paintStrategy(g, displayScaling);
                        }
                    }

                    g.Dispose();
                }
            }





            displayUpdating = false;
        }

        // Force update of the graphics box
        delegate void aRefreshCallBack();
        public void aRefresh()
        {
            if (updateDisplayTimer.Enabled && this.graphicsBox.InvokeRequired)
            {
                try
                {
                    aRefreshCallBack d = new aRefreshCallBack(aRefresh);
                    this.Invoke(d, new object[] { });
                }
                catch(Exception e){ }
            }
            else
            {
                this.graphicsBox.Refresh();
            }
        }


        // functions used to draw graphical representation of positinoing system data
        public void DrawSquare(List<Robot> robotArray) // draws the robots
        {
            for (int i = 0; i < robotArray.Count; i++)
            {
                Robot robot = robotArray[i];
                AForge.IntPoint[] cornerArray = robot.getCornerArray();
                try
                {
                    Pen myPen = new Pen(Color.Red, 3);
                    Graphics formGraphics = graphicsBox.CreateGraphics();


                    formGraphics.DrawLine(myPen, (int)((double)cornerArray[0].X * displayScaling.X), (int)((double)cornerArray[0].Y * displayScaling.Y), (int)((double)cornerArray[1].X * displayScaling.X), (int)((double)cornerArray[1].Y * displayScaling.Y));
                    myPen.Color = Color.Blue;
                    formGraphics.DrawLine(myPen, (int)((double)cornerArray[1].X * displayScaling.X), (int)((double)cornerArray[1].Y * displayScaling.Y), (int)((double)cornerArray[2].X * displayScaling.X), (int)((double)cornerArray[2].Y * displayScaling.Y));
                    myPen.Color = Color.Green;
                    formGraphics.DrawLine(myPen, (int)((double)cornerArray[2].X * displayScaling.X), (int)((double)cornerArray[2].Y * displayScaling.Y), (int)((double)cornerArray[3].X * displayScaling.X), (int)((double)cornerArray[3].Y * displayScaling.Y));
                    myPen.Color = Color.Yellow;
                    formGraphics.DrawLine(myPen, (int)((double)cornerArray[3].X * displayScaling.X), (int)((double)cornerArray[3].Y * displayScaling.Y), (int)((double)cornerArray[0].X * displayScaling.X), (int)((double)cornerArray[0].Y * displayScaling.Y));
                    
                    myPen.Dispose();
                    formGraphics.Dispose();
                }
                catch (Exception ex)
                {
                    //TODO Perhaps something should be done about this
                    Console.WriteLine("DrawSquare exception");
                }
            }
        }
        public void DrawCircle(List<Robot> robotArray) // draws circle to indicate collision, transmission and dispersion range
        {
            try
            {

                Robot robot;

                // This draws the Transmission circles
                Pen myPen = new Pen(Color.Red, 1);
                Graphics formGraphics = graphicsBox.CreateGraphics();
                int width = 2 * (int)((double)Program.transmissionRange * displayScaling.X);
                int height = 2 * (int)((double)Program.transmissionRange * displayScaling.Y);
                for (int i = 0; i < robotArray.Count; i++)
                {
                    robot = robotArray[i];
                    int x = (int)((robot.getPosition().X - Program.transmissionRange) * displayScaling.X);
                    int y = (int)((robot.getPosition().Y - Program.transmissionRange) * displayScaling.Y);
                    formGraphics.DrawEllipse(myPen, x, y, width, height);
                }

                // This draws the Robot collision avoidance circles
                width = 2 * (int)((double)Program.robotRadius * displayScaling.X);
                height = 2 * (int)((double)Program.robotRadius * displayScaling.Y);
                myPen.Color = Color.Fuchsia;
                for (int i = 0; i < robotArray.Count; i++)
                {
                    robot = robotArray[i];
                    int x = (int)((robot.getPosition().X - Program.robotRadius) * displayScaling.X);
                    int y = (int)((robot.getPosition().Y - Program.robotRadius) * displayScaling.Y);
                    formGraphics.DrawEllipse(myPen, x, y, width, height);
                }

                // This draws the Dispersion Circles
                width = 2 * (int)((double)Program.dispersionRange * displayScaling.X);
                height = 2 * (int)((double)Program.dispersionRange * displayScaling.Y);
                myPen.Color = Color.Cyan;
                for (int i = 0; i < robotArray.Count; i++)
                {
                    robot = robotArray[i];
                    int x = (int)((robot.getPosition().X - Program.dispersionRange) * displayScaling.X);
                    int y = (int)((robot.getPosition().Y - Program.dispersionRange) * displayScaling.Y);
                    formGraphics.DrawEllipse(myPen, x, y, width, height);
                }
                myPen.Dispose();
                formGraphics.Dispose();
            }
            catch (Exception ex)
            {
                //Ignored
                Console.WriteLine("DrawCircle exception");
            }
        }
        public void DrawNeighborLink(List<Robot> robotArray) // draws line between all neighbors 
        {
            try
            {
                Pen myPen = new Pen(Color.Coral, 3);
                Graphics formGraphics = graphicsBox.CreateGraphics();
                Robot robot;

                for (int i = 0; i < robotArray.Count; i++)
                {
                    robot = robotArray[i];
                    if (robot.getNeighbors() != null)
                    {
                        Robot neighbor;
                        List<Robot> neighbors = robot.getNeighbors();
                        for (int j = 0; j < neighbors.Count; j++)
                        {
                            neighbor = neighbors[j];
                            formGraphics.DrawLine(myPen, (int)(robot.getPosition().X * displayScaling.X), (int)(robot.getPosition().Y * displayScaling.Y), (int)(neighbor.getPosition().X * displayScaling.X), (int)(neighbor.getPosition().Y * displayScaling.Y));
                        }
                    }
                }
                myPen.Dispose();
                formGraphics.Dispose();
            }
            catch (Exception ex)
            {
                //Ignored
                Console.WriteLine("DrawNeighbourLink exception");
            }
        } 
        //------------------------------------
        
        // Handle the closing event of the display frame
        private void Display_FormClosing(object sender, FormClosingEventArgs e)
        {
            updateDisplayTimer.Enabled = false;
            Program.displayFrame = null;
        }
        private void Display_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        // Saves the coordinates from a mouse cursor when clicking in the displayframe and scales
        // them according to the displayScaling. Used by PointFromInput strategy
        private void graphicsBox_Click(object sender, EventArgs e)
        {
            System.Drawing.Point p = graphicsBox.PointToClient(Cursor.Position);

            DoublePoint p1 = new DoublePoint(p.X, p.Y);

            p1.X = p1.X / displayScaling.X;
            p1.Y = p1.Y / displayScaling.Y;

            Program.referencePoint.X = (int)p1.X;
            Program.referencePoint.Y = (int)p1.Y;
          
            Program.controlStrategies.updateReferencePoint();

            Program.mainFrame.logStrategyData();
        }

        private void displayVideoBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender as CheckBox).Checked)
            {
                this.splitContainer1.Panel2.BackgroundImage = null;
            }
        }

        private void Display_Load(object sender, EventArgs e)
        {

        }
    }
}
