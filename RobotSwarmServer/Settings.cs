/*
 * 
 * Settings
 * This includes most of the simulation settings.
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
using RobotSwarmServer.Control_Strategies;

namespace RobotSwarmServer
{
    public partial class Settings : UserControl
    {
        // ----- Callibration settings
        //private double oldGlyphScalingFactor;

        public Settings()
        {
            InitializeComponent();
            InitializeDataBoxes();

            this.communicationPanel.Controls.Add(Program.communication);
            Program.communication.Dock = DockStyle.Fill;
        }

        // Set default values
        private void InitializeDataBoxes()
        {
            this.numberOfRobotsBox.Text = Program.numberOfRobots.ToString();
            this.transmissionBox.Text = (Program.transmissionRangeScaling * 100).ToString();
            this.collisionAvoidanceBox.Text = (Program.robotRadiusScaling * 100).ToString();
            this.dispersionBox.Text = (Program.dispersionRangeScaling * 100).ToString();
        }

        // Apply button - Updates settings
        private void applyButton_Click(object sender, EventArgs e)
        {
           try
           {
               // Number of robots
               if (Convert.ToInt32(this.numberOfRobotsBox.Text) != Program.numberOfRobots)
               {
                   if (!Program.isSimulating)
                   {

                       Program.numberOfRobots = Convert.ToInt32(this.numberOfRobotsBox.Text);
                       Program.mainFrame.CreateRobots();
                       Program.communication.changeBufferSize();
                   }
                   else
                   {
                       MessageBox.Show("Number of robots can not be changed when the simulation is running.");
                       this.numberOfRobotsBox.Text = Program.numberOfRobots.ToString();
                   }
               }

                // Transmission settings
                Program.transmissionRangeScaling = Convert.ToDouble(this.transmissionBox.Text) / 100;
                Program.transmissionRange = (int)(Program.transmissionRangeScaling * Program.robotPhysicalRadius);
                this.transmissionBox.Text = (Program.transmissionRangeScaling * 100).ToString();

                // Collision Avoidance settings
                Program.robotRadiusScaling = Convert.ToDouble(this.collisionAvoidanceBox.Text) / 100;
                Program.robotRadius = (int)(Program.robotRadiusScaling * Program.robotPhysicalRadius);
                this.collisionAvoidanceBox.Text = (Program.robotRadiusScaling * 100).ToString();

                // Dispersion settings
                Program.dispersionRangeScaling = Convert.ToDouble(this.dispersionBox.Text) / 100;
                Program.dispersionRange = (int)(Program.dispersionRangeScaling * Program.robotPhysicalRadius);
                this.dispersionBox.Text = (Program.dispersionRangeScaling * 100).ToString();

            }

            catch (FormatException)
            {

                // Number of robots
                if (Convert.ToInt32(this.numberOfRobotsBox.Text) != Program.numberOfRobots)
                {
                    this.numberOfRobotsBox.Text = Program.numberOfRobots.ToString();
                    Program.mainFrame.CreateRobots();
                }

                // Transmission
                transmissionBox.Text = (Program.transmissionRangeScaling * 100).ToString();
                
                // Collision Avoidance
                collisionAvoidanceBox.Text = (Program.robotRadiusScaling * 100).ToString();
                
                // Dispersion
                dispersionBox.Text = (Program.dispersionRangeScaling * 100).ToString();
                
            }
            if (Program.enableDatalog)
            {
                int strategyNumber = Program.activeStrategyId;
                if (strategyNumber != -1)
                {
                    // This writes the data on the form (Number of robots,Control strategy,Transmission range,Dispersion range,Collision avoidance radius,Radius of physical robot,referencePoint.X,referencePoint.Y)
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(Program.dataLogPath, true))
                    {
                        file.WriteLine(Program.numberOfRobots + "," + Program.strategyList[strategyNumber].getName() + "," + Program.transmissionRange.ToString() + "," + Program.dispersionRange.ToString() + "," + Program.robotRadius.ToString() + "," + Program.robotPhysicalRadius.ToString("n0") + "," + Program.referencePoint.X + "," + Program.referencePoint.Y);
                    }
                }
            }
        }

        // Show display frame
        private void displayButton_Click(object sender, EventArgs e)
        {
            if (Program.displayFrame == null)
            {
                Program.displayFrame = new Display();
                Program.displayFrame.Visible = true;

            }
        }

        // Show test frame
        private void testFrameButton_Click(object sender, EventArgs e)
        {
            if (Program.testFrame == null)
            {
                Program.testFrame = new TestFrame();
                Program.testFrame.Visible = true;
            }
            Program.testFrame.BringToFront();
        }

        // Enable data log checkbox
        private void enableDatalog_CheckedChanged(object sender, EventArgs e)
        {
            if (enableDatalog.Checked)
            {
                Program.dataLogPath = @"../../Data Log/" + DateTime.Now.ToString("yyyy'_'MM'_'dd'_'HH'_'mm'_'ss") + ".csv";
            }
            Program.enableDatalog = enableDatalog.Checked;
        }

        // Start simulation
        private void startButton_Click(object sender, EventArgs e)
        {

            if (startButton.Text == "Stop simulation")
            {
                Program.timerFrameToProcessing.Enabled = false;
                Program.isSimulating = false;

                Program.cameraController.useCamerasForSettings();
                if (Program.cameraController.getIncludedCameras().Count == 1 || Program.cameraController.loadCalibration())
                {
                    Program.cameraCalibrated = true;
                }
                else
                {
                    MessageBox.Show("The current configuration of cameras no longer match the last calibration, please recalibrate the camerasystem or reconfigure the settings and load the calibration manually.");
                    Program.cameraCalibrated = false;
                }

                this.startButton.BackColor = System.Drawing.Color.ForestGreen;
                startButton.Text = "Start simulation";
            }
            else if (Program.cameraCalibrated || Program.cameraController.getIncludedCameras().Count == 1)
            {
                if (Program.cameraController.startPositioningSystem())
                {
                    if (Program.enableDatalog)
                    {
                        Program.dataLogPath = @"../../Data Log/" + DateTime.Now.ToString("yyyy'_'MM'_'dd'_'HH'_'mm'_'ss") + ".csv";
                    }
                    this.startButton.BackColor = System.Drawing.Color.Firebrick;
                    startButton.Text = "Stop simulation";
                    Program.isSimulating = true;
                }
                else
                {
                    MessageBox.Show("No cameras included");
                }
            }
            else
            {
                MessageBox.Show("Warning: Cameras are not calibrated. Please calibrate!");
            }
        }
        
        // include strategy specific settings
        public void updateControlStrategySettings(){
            ControlStrategy currentStrategy = Program.strategyList.ElementAt(Program.activeStrategyId);

            controlStrategySettingsPanel.Controls.Clear();
            controlStrategySettingsPanel.Controls.Add(currentStrategy);
        }
    }
}
