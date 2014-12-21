/*
 * 
 * Camera
 * Used to handle a single camera. When starting a new camera it is 
 * initiated as an object of the Camera class. This object includes 
 * all information about the camera.
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
using System.Diagnostics;

using AForge;
using AForge.Video;
using AForge.Video.DirectShow;

namespace RobotSwarmServer
{
    public partial class Camera : UserControl
    {
        // Variabels to control camera status
        public int camNumber;
        public Bitmap img;
        private Bitmap imgCache;
        private static Bitmap darkImage;
        public VideoCaptureDevice videoSource = null;
        public bool isIncluded = true;
        //---------------

        // Paramaters from calibration to specify camera position
        public double angleToRef = 0;
        public double sizeScaling = 1;
        public DoublePoint camPosition = new DoublePoint();
        //---------------

        // Used by calibration
        public DoublePoint camCoordUpperLeft = new DoublePoint();
        public DoublePoint camCoordUpperRight = new DoublePoint();
        public DoublePoint camCoordLowerLeft = new DoublePoint();
        public DoublePoint camCoordLowerRight = new DoublePoint();
        public int IdUpperLeft = -1;
        public int IdUpperRight = -1;
        public int IdLowerLeft = -1;
        public int IdLowerRight = -1;
        //---------------

        public Camera(VideoCaptureDevice videoSource, int cameraNumber)
        {
            InitializeComponent();

            img = new Bitmap(10, 10);
            imgCache = new Bitmap(10, 10);
            darkImage = new Bitmap(cameraImagePanel.Width, cameraImagePanel.Height);
            Graphics g = Graphics.FromImage(darkImage);
            g.FillRectangle(new SolidBrush(Color.FromArgb(0,0,0)), 0, 0, darkImage.Width, darkImage.Height);

            initVideoSource(videoSource);
            camNumber = cameraNumber;
        }

        // initiate the cameras
        private void initVideoSource(VideoCaptureDevice videoSource)
        {
            this.videoSource = videoSource;
            this.videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
        }

        // Control camera status
        public void startCamera()
        {
            if (videoSource.IsRunning)
                videoSource.Stop();
            videoSource.Start();
        }
        public void stopCamera()
        {
            if (videoSource.IsRunning)
            {
                videoSource.Stop();
            }
        }
        public bool isRunning()
        {
            return videoSource.IsRunning;
        }

        //eventhandler if new frame is ready
        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Boolean done = false;
            while (!done)
            {
                try
                {
                    imgCache = img;
                    img = (Bitmap)eventArgs.Frame.Clone();
                    imgCache.Dispose();
                    done = true;
                    if (Program.testFrame != null)
                    {
                        Program.testFrame.updateData("Camera");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("ERROR in try-statement in Camera");
                    // IGNORE
                }
            }
        }
        
        //close the device safely
        private void CloseVideoSource()
        {
            if (!(videoSource == null))
            {
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource = null;
                }
            }
        }
 
        //prevent sudden close while device is running
        private void Display_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseVideoSource();
        }

        // preview of the camera on the settings tab
        public void updateSmallImage(){
            Bitmap copy = CameraController.GrabOneFrame(this);
            cameraImagePanel.BackgroundImage = new Bitmap(copy, new Size(cameraImagePanel.Width, cameraImagePanel.Height));
            copy.Dispose();
        }

        //Checkbox to include camera in simulation
        private void includeCameraBox_CheckedChanged(object sender, EventArgs e)
        {
            isIncluded = includeCameraBox.Checked;

            if (isIncluded)
            {
                cameraStatusLabel.ForeColor = Color.Green;
                cameraStatusLabel.Text = "Camera Included";
            }
            else
            {
                cameraStatusLabel.ForeColor = Color.Red;
                cameraStatusLabel.Text = "Camera Excluded";
            }
            Program.cameraController.updateIncludedCameraList();
            Program.cameraController.updateResolutionList();
            if (Program.cameraController.getIncludedCameras().Count == 1)
            {
                Program.cameraCalibrated = true;
            }
            else
            {
                Program.cameraCalibrated = false;
            }
        }

        // sets the resolution of the camera
        public void setResolution(IntPoint resolution)
        {
            for (int i = 0; i < videoSource.VideoCapabilities.Length; i++)
            {
                if (videoSource.VideoCapabilities[i].FrameSize.Width == resolution.X && videoSource.VideoCapabilities[i].FrameSize.Height == resolution.Y)
                {
                    videoSource.VideoResolution = videoSource.VideoCapabilities[i];
                    if (videoSource.IsRunning)
                    {
                        videoSource.Stop();
                        videoSource.Start();
                    }
                    return;
                }
            }
            throw new FormatException("ERROR: Resolution " + resolution.X + "x" + resolution.Y + " is not supported by all included cameras.");
        }

        // returns list of all available resolutions
        public VideoCapabilities[] getVideoCapabilities()
        {
            return videoSource.VideoCapabilities;
        }

    }
}

