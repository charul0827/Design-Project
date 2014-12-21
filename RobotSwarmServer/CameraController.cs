/*
 * 
 * CameraController
 * Controls all connected cameras: start/stop, their resolution and the 
 * functions used to grab image frames from the cameras. The class also 
 * includes the calibration of the cameras and the GUI-controls used to 
 * select which cameras to use. 
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

using AForge.Video.DirectShow;
using AForge.Video;
using AForge;
using AForge.Vision.GlyphRecognition;

namespace RobotSwarmServer
{
    public partial class CameraController : UserControl
    {
        private List<Camera> allCameras = new List<Camera>(); // All cameras that is connected to computer
        
        private List<Camera> includedCameras = new List<Camera>(); // Cameras that is included in simulation
        
        private List<IntPoint> resolutionList = new List<IntPoint>();

        private int sequetialCameraDelay = 300; // Used while starting cameras to prevent overload of computer

        private Timer updateSmallImages; // timer to controll camera previews in settings tab

        private IntPoint preferedCameraResolution; // used in calibration

        public CameraController()
        {
            InitializeComponent();

            this.samplingTimeBox.Text = Program.timerFrameToProcessing.Interval.ToString();
            this.squareSizeBox.Text = (Program.squareSize*100).ToString();
            
            updateSmallImages = new Timer();
            updateSmallImages.Tick += new EventHandler(this.updateSmallImages_Tick); // Everytime timer ticks, frameToProcessingTimer_Tick will be called
            updateSmallImages.Interval = 1000;

            useCamerasForSettings();
        }
        //Loads the camera panel for the bottom of the tab
        public void loadCameraPanel()
        {
            //Adds all the cameras to the camera panel
            cameraPanel.Controls.Clear();
            cameraPanel.ColumnCount = allCameras.Count();
            for (int i = 0; i < allCameras.Count(); i++)
            {
                cameraPanel.Controls.Add(allCameras.ElementAt(i), i, 0);
            }
            updateIncludedCameraList();  
            updateResolutionList();

            
            if (allCameras.Count == 1)
            {
                Program.cameraCalibrated = true;
                Program.imgSize = Program.resolution;
            }
        }

        // Get all camera devices
        private void loadCamList()
        {
            try
            {
                FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                stopCameras();
                allCameras.Clear();
                if (videoDevices.Count == 0)
                {
                    throw new ApplicationException();
                }

                int i = 0;
                foreach (FilterInfo device in videoDevices)
                {
                    allCameras.Add(new Camera(new VideoCaptureDevice(device.MonikerString), i));
                    allCameras[i].videoSource.SetCameraProperty(CameraControlProperty.Focus, 0, CameraControlFlags.Manual);  //Sets focus to 0 and turns off autofocus
                    i++;
                }
            }
            catch (ApplicationException)
            {
                cameraStatusLabel.Text = "No capture device on your system!";
            }
        }
        
        //Update the included camera list
        public void updateIncludedCameraList()  //Ändrar till public /Oskar
        {
            includedCameras.Clear();

            foreach (Camera cam in allCameras)
            {
                if (cam.isIncluded)
                {
                    includedCameras.Add(cam);
                }
            }
        }


        // Controls the small image of the camera settings tab
        public void setShowSmallCameraImage(bool showImage)
        {
            updateSmallImages.Enabled = showImage;
        }
        public void useCamerasForSettings()
        {
            if (!Program.isSimulating)
            {
                loadCamList();
                loadCameraPanel();

                cameraStatusLabel.Text = "Number of cameras: " + allCameras.Count();

                startAllCameras();
                updateSmallImages.Enabled = true;
            }
        }
        private void updateSmallImages_Tick(object sender, EventArgs e)
        {
            foreach (Camera cam in allCameras)
            {
                cam.updateSmallImage();
            }
        }

        // Starts positioning system, cameras, image processing, glyph recognition and so on
        public bool startPositioningSystem()
        {
            this.updateIncludedCameraList();
            if (includedCameras.Count != 0)
            {
                if (includedCameras.Count == 1)
                {
                    Program.imgSize = Program.resolution;
                    Program.cameraCalibrated = true;
                }
                updateSmallImages.Enabled = false;
                startIncludedCameras();
                
                Program.calibrationScaling = new DoublePoint((double)Program.resolution.X / (double)Program.calibrationResolution.X, 
                                                                        (double)Program.resolution.Y / (double)Program.calibrationResolution.Y);
                cameraStatusLabel.Text = "Simulation is live";
                Program.imageProcessing.firstFrame = true;
                Program.robotPhysicalRadius = 0;

                Program.squareSize = Convert.ToDouble(this.squareSizeBox.Text)/100;
                Program.timerFrameToProcessing.Interval = Convert.ToInt32(this.samplingTimeBox.Text);
                Program.timerFrameToProcessing.Enabled = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        // Toggle start and stop button
        public void startAllCameras()
        {
            if (allCameras.Count > 0)
            {
                foreach (Camera cam in allCameras)
                {
                    cam.startCamera();

                    // Delay used because lab-computer can't start multiple cameras simultaneously
                    System.Threading.Thread.Sleep(this.sequetialCameraDelay);
                }
            }
        
        }

        // Start the cameras included in the simulation
        public void startIncludedCameras()
        {
            stopCameras();
            for (int i = 0; i < includedCameras.Count; i++)
            {
                Camera cam = includedCameras[i];
                cam.setResolution(Program.resolution);
                cam.startCamera();
                // Delay used because lab-computer can't start multiple cameras simultaneously
                System.Threading.Thread.Sleep(this.sequetialCameraDelay);
            }
        }

        // Stop all cameras
        public void stopCameras(){

            updateSmallImages.Enabled = false;

            foreach (Camera cam in allCameras)
            {
                if (cam != null)
                {
                    cam.stopCamera();
                }
            }
        }

        // Returns all the camera objects (in a list) that should be included in the final image
        public List<Camera> getIncludedCameras()
        {
            return includedCameras;
        }
        
        // Grab one frame from specific camera
        public static Bitmap GrabOneFrame(Camera cam)
        {
            Bitmap image = null;
            Boolean done = false;
            while (!done)
            {
                try
                {
                    if (cam.img != null)
                    {
                        image = cam.img.Clone() as Bitmap;
                        done = true;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("ERROR in try-statement in CameraController");
                    // IGNORE
                }
            }
            if(image == null)
            {
                Console.WriteLine("Image = null in GrabOneFrame");
            }
            return image;

        }

        // Grab one frame from each camera
        public static List<Bitmap> GrabAllFrames()
        {
            List<Camera> includedCameras = new List<Camera>(Program.cameraController.getIncludedCameras());
            List<Bitmap> images = new List<Bitmap>();

            for (int i = 0; i < includedCameras.Count; i++)
            {
                images.Add(GrabOneFrame(includedCameras[i]));
            }

            //Console.WriteLine("Antal bilder efter GrabOneFrame i GrabAllFrames: " + images.Count + " Antal kameror: " + includedCameras.Count);

            return images;
        }

        // Grab frame from all cameras and merge to one image
        public Bitmap GrabMergedFrame()
        {
            List<Camera> activeCameras = new List<Camera>(includedCameras);

            double imgScaling = 0.2 * Program.imageProcessing.glyphScalingFactor;
            Bitmap bigImg = new Bitmap((int)(Program.imgSize.X * Program.calibrationScaling.X * imgScaling), (int)(Program.imgSize.Y * Program.calibrationScaling.Y * imgScaling));
            Graphics g = Graphics.FromImage(bigImg);
            for (int i = 0; i < activeCameras.Count; i++)
            {
                Bitmap img = CameraController.GrabOneFrame(activeCameras[i]);
                img = new Bitmap(img, new Size((int)(img.Width * imgScaling), (int)(img.Height * imgScaling)));
                Bitmap rotatedImg = ImageProcessing.rotateImage(img, (float)(activeCameras[i].angleToRef * 180 / Math.PI), new DoublePoint(0, 0));
                Bitmap resizedImg = new Bitmap(rotatedImg, new Size((int)(rotatedImg.Width * activeCameras[i].sizeScaling), (int)(rotatedImg.Height * activeCameras[i].sizeScaling)));
                g.DrawImage(resizedImg, new System.Drawing.Point((int)(activeCameras[i].camPosition.X * Program.calibrationScaling.X * imgScaling), (int)(activeCameras[i].camPosition.Y * Program.calibrationScaling.Y * imgScaling)));
                img.Dispose();
                rotatedImg.Dispose();
                resizedImg.Dispose();
            }
            g.Dispose();

            return bigImg;
        }

        // Refresh button for connected cameras
        private void updateCamerasButton_Click(object sender, EventArgs e)
        {
            useCamerasForSettings(); //Equivalent with updating the camera settings panel
            Program.cameraCalibrated = false;
            MessageBox.Show("Warning! All cameras is now uncalibrated. Please calibrate!");
            if (Program.cameraController.loadCalibration())
            {
                Program.cameraCalibrated = true;
            }
            else
            {
                MessageBox.Show("The current configuration of cameras no longer match the last calibration, please recalibrate the camerasystem or reconfigure the settings and load the calibration manually.");
                Program.cameraCalibrated = false;
            }
        }

        // Used to update resolution list when cameras are updated so that the list only 
        // contains resolutions that all included cameras support.
        public void updateResolutionList()
        {
            resolutionList.Clear();
            resolutionDropDown.Items.Clear();
            List<VideoCapabilities> tempCap = new List<VideoCapabilities>();
            if (includedCameras.Count > 1)
            {
                tempCap = includedCameras[0].getVideoCapabilities().ToList();
                for (int k = 1; k < includedCameras.Count; k++)
                {
                    List<VideoCapabilities> tempCap2 = includedCameras[k].getVideoCapabilities().ToList();
                    foreach (VideoCapabilities cap in tempCap)
                    {
                        if (!tempCap2.Contains(cap))
                        {
                            tempCap.Remove(cap);
                        }
                    }
                }
                foreach (VideoCapabilities cap in tempCap)
                {
                    resolutionList.Add(new IntPoint(cap.FrameSize.Width, cap.FrameSize.Height));
                    resolutionDropDown.Items.Add(cap.FrameSize.Width + "x" + cap.FrameSize.Height);
                    if (cap.FrameSize.Width == Program.resolution.X && cap.FrameSize.Height == Program.resolution.Y)
                    {
                        resolutionDropDown.SelectedIndex = resolutionDropDown.Items.Count - 1;
                    }
                }
            }
            else if (includedCameras.Count == 1)
            {
                foreach (VideoCapabilities cap in includedCameras[0].getVideoCapabilities())
                {
                    resolutionList.Add(new IntPoint(cap.FrameSize.Width, cap.FrameSize.Height));
                    resolutionDropDown.Items.Add(cap.FrameSize.Width + "x" + cap.FrameSize.Height);
                    if (cap.FrameSize.Width == Program.resolution.X && cap.FrameSize.Height == Program.resolution.Y)
                    {
                        resolutionDropDown.SelectedIndex = resolutionDropDown.Items.Count - 1;
                    }
                }
            }

            //If the standard full HD resolution is not supported by the cameras, the supported 
            //resolution which covers the largest area is chosen as standard.
            if (!resolutionList.Contains(Program.fullHD))
            {
                int maxImageArea = 0;
                foreach (IntPoint res in resolutionList)
                {
                    if (maxImageArea < res.X * res.Y)
                    {
                        maxImageArea = res.X * res.Y;
                        Program.resolution = res;
                    }
                }
                Program.calibrationResolution = Program.resolution;
            }
            else
            {
                Program.resolution = Program.fullHD;
                Program.calibrationResolution = Program.fullHD;
            }
            setCameraResolution(Program.resolution);

        }

        // Dropdown for camera resolution
        private void resolutionDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            int capabilityID = resolutionDropDown.SelectedIndex;
            setCameraResolution(resolutionList[capabilityID]);
        }

        // Change resolution of all cameras
        public void setCameraResolution(IntPoint resolution)
        {
            Program.resolution = resolution;
            Program.calibrationScaling.X = (double)resolution.X / (double)Program.calibrationResolution.X; 
            Program.calibrationScaling.Y = (double)resolution.Y / (double)Program.calibrationResolution.Y;
            Program.imageProcessing.glyphScalingFactor = (double)2000 / (double)Program.resolution.X;
            Program.robotPhysicalRadius = 0;
            
            foreach (Camera cam in allCameras.FindAll(
                delegate(Camera cam)
                {
                    return cam.isIncluded;
                }))
            {
                cam.setResolution(resolution);
            }
        }




        /*
         * 
         * FOLLOWING METHODS IS USED BY CALIBRATION ONLY
         * 
         */

        /*
         * Method that calibrates (aligns) the cameras. This means placing them in a global coordinate system.
         * This is done by giving them a base coordinate (upper left coordinate, since X-axis directed right, Y-axis directed down),
         * a rotation and a size scaling. The base coordinate defines the cameras translation (offset position).  
         * The rotation is how much the camera needs to be rotated (in radians) to match the other cameras. 
         * The size scaling is how much the camera needs to be resized to match the other cameras where scaling = 1 means no resizing.
         */
        private void setCameraCoord(object sender, EventArgs e)
        {
            //Turns off the timer since this method only should be called once.
            ((Timer)sender).Dispose();
            List<Camera> activeCameras = Program.cameraController.getIncludedCameras();

            //firstCamera should be the upper left camera in the global coordinate system (X-axis directed right, Y-axis directed down) 
            //with base coordinates(upper left) = (0,0), rotation = 0 and size scaling = 1. upperLeftCamera, upperCamera and leftCamera are
            //temporary cameras used to determine the firstCamera. They are all initialized do the first active camera.
            Camera firstCamera = activeCameras[0], upperLeftCamera = activeCameras[0], upperCamera = activeCameras[0], leftCamera = activeCameras[0];
            bool upperLeftCameraFound = false, upperCameraFound = false, leftCameraFound = false;

            //Calibration is always done in full HD to get maximum precision. But if the prefered resolution in the robot platform is 
            //lower than full HD, "calibrationScaling" is used to convert correct values.
            double calibrationScaling = (double)Program.resolution.Y / (double)Program.calibrationResolution.Y;

            //When calibrating a camera, glyphs could be placed at max 4 positions, one in each image quadrant.
            //This loop finds the glyphs in each camera and saves their coordinats in camCoordUpperLeft, camCoordUpperRight
            //camCoordLowerLeft or camCoordLowerRight depending on in what quadrant of the camera image they are found.
            for (int i = 0; i < activeCameras.Count; i++ )
            {
                Camera cam = activeCameras[i];

                //Recieves all glyphs found in the camera image
                List<ExtractedGlyphData> glyphs = Program.imageProcessing.GetGlyphPosition(CameraController.GrabOneFrame(cam));

                foreach (ExtractedGlyphData glyphData in glyphs)
                {
                    IntPoint[] glyphCorners = new IntPoint[4];
                    //Recieves the coordinates of the 4 corners of the current glyph
                    glyphData.Quadrilateral.CopyTo(glyphCorners);
                    //Calculates the center coordinate of the glyph
                    DoublePoint glyphCoord = (DoublePoint)(glyphCorners[0] + glyphCorners[1] + glyphCorners[2] + glyphCorners[3]) / 4;
                    int glyphID = Convert.ToInt32(glyphData.RecognizedGlyph.Name);

                    if (glyphCoord.X <= Program.resolution.X * 0.5)
                    {
                        //Glyph found in 2nd quadrant
                        if (glyphCoord.Y <= Program.resolution.Y * 0.5)
                        {
                            cam.camCoordUpperLeft = glyphCoord;
                            cam.IdUpperLeft = glyphID;
                        }
                        //Glyph found in 3rd quadrant
                        else if (glyphCoord.Y >= Program.resolution.Y * 0.5)
                        {
                            cam.camCoordLowerLeft = glyphCoord;
                            cam.IdLowerLeft = glyphID;
                        }
                    }
                    else if (glyphCoord.X >= Program.resolution.X * 0.5)
                    {
                        //Glyph found in 1st quadrant
                        if (glyphCoord.Y <= Program.resolution.Y * 0.5)
                        {
                            cam.camCoordUpperRight = glyphCoord;
                            cam.IdUpperRight = glyphID;
                        }
                        //Glyph found in 4th quadrant
                        else if (glyphCoord.Y >= Program.resolution.Y * 0.5)
                        {
                            cam.camCoordLowerRight = glyphCoord;
                            cam.IdLowerRight = glyphID;
                        }
                    }
                }

                MessageBox.Show("Cam number: " + cam.camNumber + "\r\n" +
                                "UpperLeft:  " + cam.camCoordUpperLeft.ToString() + "  " + "ID:  " + cam.IdUpperLeft + "\r\n" +
                                "UpperRight:  " + cam.camCoordUpperRight.ToString() + "  " + "ID:  " + cam.IdUpperRight + "\r\n" +
                                "LowerLeft:  " + cam.camCoordLowerLeft.ToString() + "  " + "ID:  " + cam.IdLowerLeft + "\r\n" +
                                "LowerRight:  " + cam.camCoordLowerRight.ToString() + "  " + "ID:  " + cam.IdLowerRight);

                //Determines if the camera is a middle camera, upper left camera, upper camera or left camera.
                //Id = -1 means that no glyphs where found in that quadrant
                if (cam.IdUpperLeft != -1 && cam.IdUpperRight != -1 && cam.IdLowerLeft != -1 && cam.IdLowerRight != -1)
                {
                    //Middle camera found
                }
                else if (cam.IdUpperRight != -1 && cam.IdLowerLeft != -1 && cam.IdLowerRight != -1)
                {
                    upperLeftCameraFound = true;
                    upperLeftCamera = cam;
                }
                else if (cam.IdLowerLeft != -1 && cam.IdLowerRight != -1)
                {
                    upperCameraFound = true;
                    upperCamera = cam;
                }
                else if (cam.IdUpperRight != -1 && cam.IdLowerRight != -1)
                {
                    leftCameraFound = true;
                    leftCamera = cam;
                }
            }

            //Determines the first camera.
            if (upperLeftCameraFound)
            {
                firstCamera = upperLeftCamera;
            }
            else if (upperCameraFound)
            {
                firstCamera = upperCamera;
            }
            else if (leftCameraFound)
            {
                firstCamera = leftCamera;
            }
            else
            {
                MessageBox.Show("No first camera was found!\r\nRearrange the glyphs and try again!");
            }

            List<Camera> calibratedCams = new List<Camera>();
            List<Camera> unCalibratedCams = new List<Camera>();

            //Places the first camera in calibratedCams, which means that the camera has been calibrated and
            //placed in the global coordinate system. Also sets the calibration parameters of the first camera.
            calibratedCams.Add(firstCamera);
            calibratedCams[0].angleToRef = 0;
            calibratedCams[0].sizeScaling = 1;
            calibratedCams[0].camPosition = new DoublePoint(0, 0);

            //Places the rest of the cameras in unCalibratedCams
            unCalibratedCams = activeCameras.Except<Camera>(calibratedCams).ToList<Camera>();

            //Determines the calibration parameters of the cameras, i.e placing them in the global coordinate system and
            //adding them to the sortedCamList.
            for (int i = 0; i < calibratedCams.Count; i++)
            {
                Camera cam1 = calibratedCams[i];
                for (int j = 0; j < unCalibratedCams.Count; j++)
                {
                    Camera cam2 = unCalibratedCams[j];

                    //Uncalibrated cam2 is above cam1
                    if (cam1.IdUpperLeft == cam2.IdLowerLeft && cam1.IdUpperRight == cam2.IdLowerRight &&
                        cam1.IdUpperLeft != -1 && cam2.IdLowerLeft != -1 && cam1.IdUpperRight != -1 && cam2.IdLowerRight != -1)
                    {
                        cam2.angleToRef = angleDiff(cam1.camCoordUpperLeft, cam1.camCoordUpperRight, cam2.camCoordLowerLeft, cam2.camCoordLowerRight);
                        cam2.sizeScaling = distDiff(cam1.camCoordUpperLeft, cam1.camCoordUpperRight, cam2.camCoordLowerLeft, cam2.camCoordLowerRight);
                        cam2.camPosition = ImageProcessing.translateNewPosition(new DoublePoint(0, 0), cam2.camCoordLowerLeft, cam1.camCoordUpperLeft, cam2.angleToRef, cam2.sizeScaling);

                        cam2.camCoordUpperLeft = ImageProcessing.translateCoordToGlobal(cam2.camCoordUpperLeft, cam2);
                        cam2.camCoordUpperRight = ImageProcessing.translateCoordToGlobal(cam2.camCoordUpperRight, cam2);
                        cam2.camCoordLowerLeft = ImageProcessing.translateCoordToGlobal(cam2.camCoordLowerLeft, cam2);
                        cam2.camCoordLowerRight = ImageProcessing.translateCoordToGlobal(cam2.camCoordLowerRight, cam2);

                        calibratedCams.Add(cam2);
                        unCalibratedCams.Remove(cam2);
                        j--;
                    }
                    //Uncalibrated cam2 is below cam1
                    else if (cam1.IdLowerLeft == cam2.IdUpperLeft && cam1.IdLowerRight == cam2.IdUpperRight &&
                            cam1.IdLowerLeft != -1 && cam2.IdUpperLeft != -1 && cam1.IdLowerRight != -1 && cam2.IdUpperRight != -1)
                    {
                        cam2.angleToRef = angleDiff(cam1.camCoordLowerLeft, cam1.camCoordLowerRight, cam2.camCoordUpperLeft, cam2.camCoordUpperRight);
                        cam2.sizeScaling = distDiff(cam1.camCoordLowerLeft, cam1.camCoordLowerRight, cam2.camCoordUpperLeft, cam2.camCoordUpperRight);
                        cam2.camPosition = ImageProcessing.translateNewPosition(new DoublePoint(0, 0), cam2.camCoordUpperLeft, cam1.camCoordLowerLeft, cam2.angleToRef, cam2.sizeScaling);

                        cam2.camCoordUpperLeft = ImageProcessing.translateCoordToGlobal(cam2.camCoordUpperLeft, cam2);
                        cam2.camCoordUpperRight = ImageProcessing.translateCoordToGlobal(cam2.camCoordUpperRight, cam2);
                        cam2.camCoordLowerLeft = ImageProcessing.translateCoordToGlobal(cam2.camCoordLowerLeft, cam2);
                        cam2.camCoordLowerRight = ImageProcessing.translateCoordToGlobal(cam2.camCoordLowerRight, cam2);

                        calibratedCams.Add(cam2);
                        unCalibratedCams.Remove(cam2);
                        j--;
                    }
                    //Uncalibrated cam2 is left of cam1
                    else if (cam1.IdLowerLeft == cam2.IdLowerRight && cam1.IdUpperLeft == cam2.IdUpperRight &&
                            cam1.IdLowerLeft != -1 && cam2.IdLowerRight != -1 && cam1.IdUpperLeft != -1 && cam2.IdUpperRight != -1)
                    {
                        cam2.angleToRef = angleDiff(cam1.camCoordLowerLeft, cam1.camCoordUpperLeft, cam2.camCoordLowerRight, cam2.camCoordUpperRight);
                        cam2.sizeScaling = distDiff(cam1.camCoordLowerLeft, cam1.camCoordUpperLeft, cam2.camCoordLowerRight, cam2.camCoordUpperRight);
                        cam2.camPosition = ImageProcessing.translateNewPosition(new DoublePoint(0, 0), cam2.camCoordLowerRight, cam1.camCoordLowerLeft, cam2.angleToRef, cam2.sizeScaling);

                        cam2.camCoordUpperLeft = ImageProcessing.translateCoordToGlobal(cam2.camCoordUpperLeft, cam2);
                        cam2.camCoordUpperRight = ImageProcessing.translateCoordToGlobal(cam2.camCoordUpperRight, cam2);
                        cam2.camCoordLowerLeft = ImageProcessing.translateCoordToGlobal(cam2.camCoordLowerLeft, cam2);
                        cam2.camCoordLowerRight = ImageProcessing.translateCoordToGlobal(cam2.camCoordLowerRight, cam2);

                        calibratedCams.Add(cam2);
                        unCalibratedCams.Remove(cam2);
                        j--;
                    }
                    //Uncalibrated cam2 is right of cam1
                    else if (cam1.IdUpperRight == cam2.IdUpperLeft && cam1.IdLowerRight == cam2.IdLowerLeft &&
                            cam1.IdUpperRight != -1 && cam2.IdUpperLeft != -1 && cam1.IdLowerRight != -1 && cam2.IdLowerLeft != -1)
                    {
                        cam2.angleToRef = angleDiff(cam1.camCoordUpperRight, cam1.camCoordLowerRight, cam2.camCoordUpperLeft, cam2.camCoordLowerLeft);
                        cam2.sizeScaling = distDiff(cam1.camCoordUpperRight, cam1.camCoordLowerRight, cam2.camCoordUpperLeft, cam2.camCoordLowerLeft);
                        cam2.camPosition = ImageProcessing.translateNewPosition(new DoublePoint(0, 0), cam2.camCoordUpperLeft, cam1.camCoordUpperRight, cam2.angleToRef, cam2.sizeScaling);

                        cam2.camCoordUpperLeft = ImageProcessing.translateCoordToGlobal(cam2.camCoordUpperLeft, cam2);
                        cam2.camCoordUpperRight = ImageProcessing.translateCoordToGlobal(cam2.camCoordUpperRight, cam2);
                        cam2.camCoordLowerLeft = ImageProcessing.translateCoordToGlobal(cam2.camCoordLowerLeft, cam2);
                        cam2.camCoordLowerRight = ImageProcessing.translateCoordToGlobal(cam2.camCoordLowerRight, cam2);

                        calibratedCams.Add(cam2);
                        unCalibratedCams.Remove(cam2);
                        j--;
                    }
                }
            }

            //To be able to draw an image of all the cameraimages merged, the size of the combined images needs
            //to be determined. This size is saved in imgSize.
            foreach (Camera cam in activeCameras)
            {
                Program.imgSize.X = System.Math.Max((int)cam.camPosition.X + Program.resolution.X, Program.imgSize.X);
                Program.imgSize.Y = System.Math.Max((int)cam.camPosition.Y + Program.resolution.Y, Program.imgSize.Y);
            }

            //Saves the calibration parameters to a save file so you don't have to calebrate the cameras every time.
            saveToFile();

            //Restore the cameras to their prefered resolution.
            Program.cameraController.setCameraResolution(preferedCameraResolution);
            Program.cameraCalibrated = true;
            Program.cameraController.setShowSmallCameraImage(true);
        }

        // Used by configuration to calculate angle between two cameras
        public static double angleDiff(DoublePoint leftUp, DoublePoint leftDown, DoublePoint rightUp, DoublePoint rightDown)
        {
            double leftAngle = Math.Atan2((leftUp.Y - leftDown.Y), (leftUp.X - leftDown.X));
            double rightAngle = Math.Atan2((rightUp.Y - rightDown.Y), (rightUp.X - rightDown.X));
            double angleDiff = (leftAngle - rightAngle);
            return angleDiff;
        }

        // Used by configuration to calculate distance between two cameras
        public static double distDiff(DoublePoint leftUp, DoublePoint leftDown, DoublePoint rightUp, DoublePoint rightDown)
        {
            double distLeft = Math.Sqrt(Math.Pow(leftUp.X - leftDown.X, 2) + Math.Pow(leftUp.Y - leftDown.Y, 2));
            double distRight = Math.Sqrt(Math.Pow(rightUp.X - rightDown.X, 2) + Math.Pow(rightUp.Y - rightDown.Y, 2));
            double sizeChange = distLeft / distRight;
            return sizeChange;
        }

        // Saves calibration settings to file
        private void saveToFile()
        {
            Program.cameraController.updateIncludedCameraList();
            List<Camera> activeCameras = Program.cameraController.getIncludedCameras();
            string saveText = activeCameras.Count.ToString();

            for (int i = 0; i < activeCameras.Count; i++)
            {
                saveText = (saveText + "\r\n" +
                            activeCameras[i].angleToRef.ToString() + "\r\n" +
                            activeCameras[i].sizeScaling.ToString() + "\r\n" +
                            activeCameras[i].camPosition.X.ToString() + "\r\n" +
                            activeCameras[i].camPosition.Y.ToString());
            }

            System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\..\Settings.txt");
            file.WriteLine(saveText);
            file.Close();
        }

        // Load calibration file
        public bool loadCalibration()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\Settings.txt");
            Program.cameraController.updateIncludedCameraList();
            List<Camera> activeCameras = Program.cameraController.getIncludedCameras();

            if (Convert.ToInt32(lines[0]) != activeCameras.Count)
                return false;

            for (int i = 0, j = 1; i < activeCameras.Count; i++, j += 4)
            {
                try
                {
                    activeCameras[i].angleToRef = Convert.ToDouble(lines[j]);
                    activeCameras[i].sizeScaling = Convert.ToDouble(lines[j + 1]);
                    activeCameras[i].camPosition.X = Convert.ToDouble(lines[j + 2]);
                    activeCameras[i].camPosition.Y = Convert.ToDouble(lines[j + 3]);
                }
                catch (FormatException)
                {
                    Console.WriteLine("ERROR: Unable to convert to a Double during camera configuration.");
                    return false;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("ERROR: Unable to convert to a Double during camera configuration (Outside the range of a Double).");
                    return false;
                }
            }
            foreach (Camera cam in activeCameras)
            {
                Program.imgSize.X = System.Math.Max((int)cam.camPosition.X + Program.calibrationResolution.X, Program.imgSize.X);
                Program.imgSize.Y = System.Math.Max((int)cam.camPosition.Y + Program.calibrationResolution.Y, Program.imgSize.Y);
            }
            return true;
        }

        // Button used to start the calibration
        private void calibrateButton_Click(object sender, EventArgs e)
        {
            List<Camera> activeCameras = Program.cameraController.getIncludedCameras();
            if (activeCameras.Count > 0)
            {
                //Saves the prefered camera resolotion so that it can be restored after the calibration. 
                //This is necessary since the calibration always uses full HD, unlike the robot platform
                //which can have an arbitrary resolution.
                preferedCameraResolution = Program.resolution;

                Program.timerFrameToProcessing.Enabled = false;
                Program.cameraController.updateIncludedCameraList();
                Program.cameraController.setShowSmallCameraImage(false);

                //Sets the camera resolution to full HD to achieve maximum precision.
                Program.cameraController.setCameraResolution(Program.calibrationResolution);
                Program.imageProcessing.glyphScalingFactor = 1;

                //Sets a timer of 1000 ms before calling the "setCameraCoord" to give the cameras time to start and 
                //change resolution before using them. After 1000 ms the "setCameraCoord" is called.
                Timer calibrateTimer = new Timer();
                calibrateTimer.Tick += new EventHandler(this.setCameraCoord);
                calibrateTimer.Interval = 1000;
                calibrateTimer.Enabled = true;
            }
            else if (activeCameras.Count == 1)
            {
                MessageBox.Show("Only one camera was included!\r\nThe calibration process is only used to align multiple cameras to each other and is not necessary if only one camera is used.");
            }
            else
            {
                MessageBox.Show("No camera was included!\r\nPlease connect one or more cameras to the computer and select the included cameras with the checkboxes bellow.");
            }
        }

        // Button used to load calibration manually
        private void load_button_Click(object sender, EventArgs e)
        {
            if (loadCalibration())
            {
                MessageBox.Show("Camera settings loaded succesfully!");
                Program.cameraCalibrated = true;
            }
            else
            {
                MessageBox.Show("The current configuration of cameras no longer match the last calibration, please recalibrate the camerasystem or reconfigure the settings and load the calibration manually.");
                Program.cameraCalibrated = false;
            }
        }

    }
}
