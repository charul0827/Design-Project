using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using AForge;
using RobotSwarmServer.Control_Strategies;

namespace RobotSwarmServer
{
    public static class Program
    {
        // Number of robots needs to be changed prior to start of application
        public static int numberOfRobots = 1;   //default value of number of robots

        //The maximum speed of the m3pi robots
        public static double robotMaxSpeed = 100;
        
        //$$$$$Changes/additions for RC car$$$$$//
        //PWM constants for RC-car
        /*public static int neutralSpeed = 74;
        public static int neutralSteer = 76;
        public static int testSpeed = 60;*/
        public static int neutralSpeed = 125;
        public static int neutralSteer = 76;
        public static int testSpeed = 103;

        public static int minSpeed = 40;
        //public static int maxSpeed = 100;
        public static int maxSpeed = 130;
        public static int minSteer = 10;
        public static int maxSteer = 134;
        //$$$$$$$$$$//

        // Parameters, these can be changed from the application
        public static double squareSize = 2.0;
        public static double transmissionRangeScaling = 11;
        public static double robotRadiusScaling = 1.7;
        public static double dispersionRangeScaling = 7;

        // These depends on the "Scaling" variables above.
        public static double robotPhysicalRadius = 0;
        public static int robotRadius;
        public static int transmissionRange;
        public static int dispersionRange;

        // The objects of the different modules in the system
        public static MainFrame mainFrame; // main GUI
        public static Settings settings; // settings GUI
        public static Communication communication; // communication protocol
        public static CameraController cameraController; // controls all cameras
        public static ImageProcessing imageProcessing; // the image processing of the positioning system
        public static Display displayFrame; // display frame that gives visual feedback about the simulation
        public static TestFrame testFrame; // Can be used in testing and debuging the performance
        public static ControlStrategies controlStrategies;  //Klass för kontrolstrategier

        public static List<ControlStrategy> strategyList;   // Strategi lista
        public static List<Robot> robotList; // Contains all robots
        public static Timer timerFrameToProcessing; // timer used by positionoing system
        public static Timer timerSendPackage; // timer used by communication protocol

        // These variables are used for the data-logging
        public static bool enableDatalog = false;
        public static string dataLogPath;


        public static bool isSimulating = false; // the status of the simulation
        public static int activeStrategyId = -1; // the current strategy that is used
        public static IntPoint referencePoint = new IntPoint(100, 100); // the referencepoint that is used (controlled by GUI)

        public static IntPoint fullHD = new IntPoint(1920, 1080);    //Standard full HD resolution
        public static IntPoint resolution = new IntPoint(1920, 1080); // default resolution for simulation
        public static IntPoint imgSize = new IntPoint( 0, 0 ); // controls the size of the merged image (the total size of all included cameras)
        public static IntPoint calibrationResolution = new IntPoint(1920, 1080); // resolution for calibration
        public static DoublePoint calibrationScaling = new DoublePoint((double)Program.resolution.X / (double)Program.calibrationResolution.X,
                                                                        (double)Program.resolution.Y / (double)Program.calibrationResolution.Y); // used when calibration settings 
        public static bool cameraCalibrated = false; // controls the calibration status



        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainFrame = new MainFrame();
           Application.Run(mainFrame);
        }
    }
}
