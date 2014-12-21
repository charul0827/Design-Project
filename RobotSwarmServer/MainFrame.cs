/*
 * 
 * MainFrame
 * Includes the main frame that incorporates the GUI. Also includes functions 
 * for the Data log and to update the table with robot parameters.
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotSwarmServer
{
    public partial class MainFrame : Form
    {
        public MainFrame()
        {
            initSimulationPanel();
            initCameraSettingsPanel();
        }

        // Creates the tabs of GUI
        private void initSimulationPanel(){
            Program.communication = new Communication();
            Program.imageProcessing = new ImageProcessing();
            Program.robotList = new List<Robot>();
            Program.controlStrategies = new ControlStrategies();
            Program.settings = new Settings();

            // This creates a folder named "Data Log" at the root folder of the project.
            System.IO.Directory.CreateDirectory(@"../../Data Log/");

            InitializeComponent();

            CreateRobots();

            // Adds the settings form to simulation tab
            this.settingsPanel.Controls.Add(Program.settings);
            Program.settings.Dock = DockStyle.Fill;

            // Adds automata to top, right panel in simulation tab
            this.automataPanel.Controls.Add(Program.imageProcessing);
            Program.imageProcessing.Dock = DockStyle.Fill;

            // Adds the controlstrategies to simulation tab
            this.strategyPanel.Controls.Add(Program.controlStrategies);     //Creates controlStrategy panel
            Program.controlStrategies.Dock = DockStyle.Fill;
            
        }
        private void initCameraSettingsPanel(){
            Program.cameraController = new CameraController();

            this.cameraSettingsTab.Controls.Add(Program.cameraController);
            Program.cameraController.Dock = DockStyle.Fill;
        }

        // Creates and updates the robot objects
        public void CreateRobots()
        {
            this.robotTable.Rows.Clear();
            this.robotTable.Rows.Add(Program.numberOfRobots);

            Program.robotList.Clear();
            for (int i = 0; i < Program.numberOfRobots; i++)
            {
                Program.robotList.Add(new Robot(i));
            }
        }

        // Updates the robot table in GUI with new data
        public void updateRobotTable()
        {

            foreach (Robot robot in Program.robotList)
            {
                AForge.DoublePoint position = robot.getPosition();
                int[] motorSignal = robot.getMotorSignals();
                this.robotTable.Rows[robot.getID()].Cells[0].Value = robot.getID();
                this.robotTable.Rows[robot.getID()].Cells[1].Value = robot.getDetected();
                this.robotTable.Rows[robot.getID()].Cells[2].Value = robot.isBlocked();
                this.robotTable.Rows[robot.getID()].Cells[3].Value = "(" + position.X + ", " + position.Y + ")";
                this.robotTable.Rows[robot.getID()].Cells[4].Value = robot.getHeading();
                this.robotTable.Rows[robot.getID()].Cells[5].Value = robot.getSpeed();
                this.robotTable.Rows[robot.getID()].Cells[6].Value = String.Join(", ", motorSignal);

                List<Robot> neighbors = robot.getNeighbors();
                string neighborsTemp = "None";
                if (neighbors.Count != 0)
                {
                    neighborsTemp = "";
                    foreach (Robot neighbor in neighbors)
                        neighborsTemp += neighbor.getID().ToString() + ", ";
                }
                this.robotTable.Rows[robot.getID()].Cells[7].Value = neighborsTemp;
            }
        }

        // Handle the closed event of the mainframe
        private void MainFrame_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.cameraController.stopCameras();
            Program.timerFrameToProcessing.Enabled = false;
            Program.timerSendPackage.Enabled = false;
            Program.timerFrameToProcessing.Dispose();
            Program.timerSendPackage.Dispose();
            if (Program.displayFrame != null) 
                Program.displayFrame.Dispose();
        }

        // Logs data when changing active strategy
        public void logStrategyData()
        {
            CultureInfo ci = CultureInfo.GetCultureInfo("en-US");
            NumberFormatInfo nfi = (NumberFormatInfo)ci.NumberFormat.Clone();
            nfi.NumberGroupSeparator = "";
            if (Program.enableDatalog && Program.activeStrategyId != -1)
            {
                // This writes the data on the form (Type, Timestamp, Number of robots,Control strategy,Transmission range,Dispersion range,Collision avoidance radius,Radius of physical robot,referencePoint.X,referencePoint.Y)
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(Program.dataLogPath, true))
                {
                    file.WriteLine(0 + "," + DateTime.Now.Ticks + "," + Program.numberOfRobots + "," + Program.transmissionRange.ToString() + "," + Program.dispersionRange.ToString() + "," + Program.robotRadius.ToString() + "," + Program.robotPhysicalRadius.ToString("n0") + "," + Program.referencePoint.X + "," + Program.referencePoint.Y);
                }
            }
        }

        // Logs data each time the positinoing system calculates new positions
        public void logPositionData()
        {
            CultureInfo ci = CultureInfo.GetCultureInfo("en-US");
            NumberFormatInfo nfi = (NumberFormatInfo)ci.NumberFormat.Clone();
            nfi.NumberGroupSeparator = "";
            if (Program.enableDatalog)
            {
                // This writes the data on the form (Type, Timestamp, 1Xpos,1Ypos,1Heading,2Xpos,2Ypos,2Heading,etc...)
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(Program.dataLogPath, true))
                {
                    file.Write(1 + "," + DateTime.Now.Ticks);
                    foreach (Robot robot in Program.robotList)
                    {
                        file.Write("," + robot.getPosition().X.ToString("n0", nfi) + "," + robot.getPosition().Y.ToString("n0", nfi) + "," + (Math.Atan2(robot.getHeading().Y, robot.getHeading().X) * 180 / Math.PI).ToString("n0", nfi));
                        //Console.WriteLine(robot.GetPosition().X.ToString("n0", nfi) + "," + robot.GetPosition().Y.ToString("n0", nfi) + "," + (Math.Atan2(robot.GetHeading().Y, robot.GetHeading().X) * 180 / Math.PI).ToString("n0", nfi));
                    }
                    //file.Write(Program.transmissionRange.ToString() + "," + Program.dispersionRange.ToString() + "," + Program.robotRadius.ToString() + "," + Program.robotPhysicalRadius.ToString("n0"));
                    file.WriteLine();
                }
            }
        }
    }
}
