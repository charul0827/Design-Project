/*
 * 
 * Communication
 * Handles the communication with the Wixel/Arduino. Incorporates a Timer that sends new 
 * commands to the robots at a given interval. This interval is defined in the GUI.
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
using System.IO.Ports;

namespace RobotSwarmServer
{
    public partial class Communication : UserControl
    {
        // Communication variables
        //private byte[] buffer = new byte[2*Program.numberOfRobots + 2];   //uncomment if using m3pi robots
        private SerialPort port;
        private string portName;        
        
        //$$$$$Changes/Additions for RC cars$$$$$//
        private byte[] xPrevious = new byte[1];
        private byte[] yPrevious = new byte[1];
        private bool firstTime = true;
        //$$$$$$$$$$//

        public Communication()
        {
            InitializeComponent();                                          //initializes all components in the system, e.g. camera and GUI; This method can be found in Camera.Designer.cs
            InitiateTimer();                                                //This method is written in this class only; see below.
            RefreshPortList();
        }
        
        // Initiate timer to control communication
        private void InitiateTimer()
        {
            int desiredUpdateRate = 1;

            Program.timerSendPackage = new Timer();                         //Timer() initializes a new instance of the Timer class, and sets all the properties to their initial values.
            Program.timerSendPackage.Tick += new EventHandler(this.Send);
            Program.timerSendPackage.Interval = desiredUpdateRate;
            tickRateBox.Text = desiredUpdateRate.ToString();
            Program.timerSendPackage.Enabled = true;
        }

        // Refresh the content of the port list.
        private void RefreshPortList(object sender = null, EventArgs e = null)
        {
            string[] ports = SerialPort.GetPortNames();
            lstPorts.Items.Clear();

            foreach (string port in ports)
            {
                lstPorts.Items.Add(port);
            }

            EnableOpenButton();
        }
        
        // Open-Close port       
        private void OpenPort(object sender, EventArgs e)
        {
            if (buttonOpenClose.Text == "Start communication")
            {
                portName = (String)lstPorts.SelectedItem;
                try
                {
                    //port = new SerialPort(portName, 9600);    //uncomment if using m3pi robots
                    
                    //$$$$$Changes/Additions for RC cars$$$$$//
                    port = new SerialPort("COM9", 115200);
                    //$$$$$$$$$$//

                    if (port != null)
                    {
                        port.ReadTimeout = 300;
                        
                        //uncomment the below lines if using m3pi robots
                        //port.DataBits = 8;
                        //port.StopBits = StopBits.One;
                        //port.Parity = Parity.None;
                        
                        port.Open();
                        
                        // Set desired update rate
                        Program.timerSendPackage.Interval = Convert.ToInt32(tickRateBox.Text);
                        tickRateBox.Text = Program.timerSendPackage.Interval.ToString();
                        
                        buttonOpenClose.Text = "Stop communication";
                    }
                    else
                    {
                        MessageBox.Show("Failed to open port " + portName, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        port = null;
                        lstPorts.SelectedIndex = -1;
                        RefreshPortList();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Port could not be opened. Refreshing.", "Error opening port", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    port = null;
                    lstPorts.SelectedIndex = -1;
                    RefreshPortList();
                }
            }
            else
            {
                try
                {
                    port.Close();
                }
                catch (Exception)
                {                    
                    MessageBox.Show("Port could not be closed. Refreshing.", "Error closing port", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    port = null;
                }
                port = null;
                labelSendStatus.Text = "";
                buttonOpenClose.Text = "Start communication";
                RefreshPortList();
            }
        }

        // Change state of the open button
        private void EnableOpenButton(object sender = null, EventArgs e = null)
        {
            buttonOpenClose.Enabled = lstPorts.SelectedIndex >= 0;
        }

        // Send data package.
        private void Send(object sender, EventArgs e)
        {
            //$$$$$Changes/Additions for RC cars$$$$$//
            if (firstTime)
            {
                xPrevious[0] = (byte)(200);
                yPrevious[0] = (byte)(200);
                firstTime = false;
            }
            //$$$$$$$$$$//

            try
            {
                if (port != null)
                {
                    //Uncomment the below section if using m3pi robots
                    /*// Steering command. First and last byte needs to be 0 for the robots to accept the package.
                    buffer[0] = (byte)0;
                    buffer[2*Program.numberOfRobots + 1] = (byte)0; */

                    //$$$$$Changes/Additions for RC cars$$$$$//
                    byte[] x = new byte[1];
                    byte[] y = new byte[1];
                    //$$$$$$$$$$//

                    // If not halted send robots control signals otherwize send zeros to stop all the robots.
                    if (!Program.controlStrategies.halted)
                    {
                        //Update robots
                        for (int i = 0; i < Program.numberOfRobots; i++)
                        {
                            Program.robotList[i].updateRobot();
                        }
                        Program.mainFrame.updateRobotTable();

                        //Uncomment the below section if using m3pi robots
                        // Build buffer with all the robots control signals.
                        /*for (int i = 0; i < Program.robotList.Count; i++)
                        {
                            Robot robot = Program.robotList[i];
                            //buffer[2 * i + 1] = ParseToSend(robot.getMotorSignals()[0]);    
                            //buffer[2 * i + 2] = ParseToSend(robot.getMotorSignals()[1]);
                        }*/
                        // Update output text.
                        /* if (labelSendStatus.Text == "")
                        {
                            labelSendStatus.Text = "Sending " + buffer.Length.ToString() + " bytes...";
                        }
                        else
                        {
                            labelSendStatus.Text = "";
                        }*/
                        
                        //$$$$$Changes/Additions for RC cars$$$$$//                
                        Robot robot = Program.robotList[0];
                        x[0] = (byte)(robot.getMotorSignals()[0]);                      
                        y[0] = (byte)(robot.getMotorSignals()[1]);
                        //$$$$$$$$$$//        
                    }
                    else
                    {
                        //Uncomment the below section if using m3pi robots
                        // Fill the buffer with "zeros" 
                        /*for (int i = 0; i < 2*Program.robotList.Count; i++)
                        {
                            //buffer[i + 1] = ParseToSend(0);
                        }*/
                        // Update output text.
                        /*if (labelSendStatus.Text == "")
                        {
                            labelSendStatus.Text = "Robots stopped!";
                        }
                        else
                        {
                            labelSendStatus.Text = "";
                        }*/

                        //$$$$$Changes/Additions for RC cars$$$$$// 
                        x[0] = (byte)(Program.neutralSpeed);
                        y[0] = (byte)(Program.neutralSteer);
                        //$$$$$$$$$$//                          
                    }

                    //Uncomment the below line if using m3pi robots
                    // Send package to COM (Wixel)
                    // port.Write(buffer, 0, buffer.Length);             

                    //$$$$$Changes/Additions for RC cars$$$$$// 
                    if (x[0] != xPrevious[0] || y[0] != yPrevious[0])
                    {
                        Console.WriteLine("I'm sending");
                        port.Write(x, 0, 1);
                        port.Write(y, 0, 1);
                        xPrevious[0] = x[0];
                        yPrevious[0] = y[0];
                    }
                    //$$$$$$$$$$// 
                }
            }
            catch (StackOverflowException error)
            {
                Console.WriteLine("Communication: Could not send data package!  " + error.Message);
            }
        }

        //Uncomment the below section if using m3pi robots
        // Prepare data to be sent with Wixel.
        /*private byte ParseToSend(int input)
        {
            // Is the signal within the allowed range?
            if (Math.Abs(input) <= 127)
            {
                return Convert.ToByte(input + 128);
            }
            else
            {
                return Convert.ToByte((Math.Sign(input) * 127) + 128);
            }
        }*/

        // Change buffer size if number of robots change
        public void changeBufferSize()
        {
            //buffer = new byte[2*Program.numberOfRobots + 2];  //Uncomment if using m3pi robots
        }

    }
}
