/*
 * 
 * Control_Strategies\ControlStrategies
 * This handles all control strategies. This includes the GUI-controls
 * that is used to set which control strategy to use.
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
using RobotSwarmServer.Control_Strategies.Strategies;
using RobotSwarmServer.Control_Strategies.Strategies.Collision_Avoidance.FIFO;
using RobotSwarmServer.Control_Strategies;

namespace RobotSwarmServer
{
    public partial class ControlStrategies : UserControl
    {

        public bool halted = false; // halts the active strategy

        public ControlStrategies()
        {
            Program.strategyList = new List<ControlStrategy>();
            InitializeComponent();
            loadStrategies();

            pointXBox.Text = Program.referencePoint.X.ToString();
            pointYBox.Text = Program.referencePoint.Y.ToString();
        }

        //Creates the different strategies and saves them in a list
        private void loadStrategies()
        {
            Program.strategyList.Add(new StandStill());
            Program.strategyList.Add(new Dispersion1W());
            Program.strategyList.Add(new Dispersion2W());
            Program.strategyList.Add(new Rendezvous1DX());
            Program.strategyList.Add(new Rendezvous1DY());
            Program.strategyList.Add(new Rendezvous2D());
            Program.strategyList.Add(new PointFromInput());
            Program.strategyList.Add(new CrossMiddlePoint());
            Program.strategyList.Add(new RandomMovement());
            Program.strategyList.Add(new Flocking());
            Program.strategyList.Add(new FollowPath("Follow Circle", FollowPath.createCirclePoints(350, new AForge.DoublePoint(1000,500), 30)));
            Program.strategyList.Add(new FollowPath("Follow Square", FollowPath.createRectanglePoints(new AForge.DoublePoint(200, 200), 800, 500)));
            Program.strategyList.Add(new FollowPath("Traffic with FIFO", FollowPath.createXYAxis8(400, new AForge.DoublePoint(964, 543), 5), new FIFO(new CollisionArea(new Point(864,443),new Size(200,200)))));
            Program.strategyList.Add(new FlockingDemostration(350));
            Program.strategyList.Add(new FollowPath("Follow Ellipse", FollowPath.createEllipsePoints(750, 350, new AForge.DoublePoint(1920 / 2, 1080 / 2), 30)));
            //Program.strategyList.Add(new FollowPath("Follow Ellipse", FollowPath.createEllipsePoints(750, 350, new AForge.DoublePoint(1920 / 2, 1080 / 2), 15)));
            Program.strategyList.Add(new FollowPath("Follow Line", FollowPath.createLinePoints(new AForge.DoublePoint(100, 1080/2), new AForge.DoublePoint(1700, 1080/2), 10)));
            //..add strategies here

            loadStrategyList();
        }
        
        //Loads the graphical listbox with strategies
        private void loadStrategyList()
        {
            strategyListBox.Items.Clear();
            foreach (ControlStrategy strategy in Program.strategyList)
            {
                strategyListBox.Items.Add(strategy.getName());
            }
        }

        public void updateReferencePoint()
        {
            this.pointXBox.Text = Program.referencePoint.X.ToString();
            this.pointYBox.Text = Program.referencePoint.Y.ToString();
        }

        //Set each robot with the chosen strategy
        private void applyStrategyButton_Click(object sender, EventArgs e)
        {
            int strategyNumber = strategyListBox.SelectedIndex;
            if (strategyNumber != -1)
            {
                Program.activeStrategyId = strategyNumber;
                this.activeStrategy.Text = Program.strategyList[strategyNumber].getName();
                Program.settings.updateControlStrategySettings();

               foreach (Robot rob in Program.robotList)
                {
                    rob.setStrategy(Program.strategyList[strategyNumber]);
                }
               // Program.robotList[0].setStrategy(Program.strategyList[strategyNumber]);

                Program.mainFrame.logStrategyData();
            }
        }

        private void setPointButton_Click(object sender, EventArgs e)
        {
            Program.referencePoint.X = Convert.ToInt32(pointXBox.Text);
            Program.referencePoint.Y = Convert.ToInt32(pointYBox.Text);
            // This writes the data on the form (Number of robots,Control strategy,Transmission range,Dispersion range,Collision avoidance radius,Radius of physical robot,referencePoint.X,referencePoint.Y)
            int strategyNumber = Program.activeStrategyId;
            Program.mainFrame.logStrategyData();
        }

        // Change state of the halt button.
        private void HaltCommunication(object sender, EventArgs e)
        {
            if (buttonHaltUnhalt.Text == "Halt")
            {
                buttonHaltUnhalt.Text = "Unhalt";
                halted = true;
            }
            else
            {
                buttonHaltUnhalt.Text = "Halt";
                halted = false;
            }
        }
    }
}
