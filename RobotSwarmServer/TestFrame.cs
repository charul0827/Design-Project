using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RobotSwarmServer
{
    public partial class TestFrame : Form
    {
        public TestFrame()
        {
            InitializeComponent();

            dataPoints[0].Add(new DataPoint(DateTime.Now.ToOADate(), 0));
            dataPoints[1].Add(new DataPoint(DateTime.Now.ToOADate(), 0));

            timer.Interval = 500;
            timer.Enabled = true;
            timer.Tick += new EventHandler(timerUpdate);

            timer.Start();
        }
        public void timerUpdate(Object sender, EventArgs e)
        {
            updateChart("Camera");
            updateChart("Glyph");
        }

        public void updateData(String seriesName)
        {
            int seriesNumber = seriesName == "Camera" ? 0 : 1;
            DateTime now = DateTime.Now;
            double timeDiff = Math.Abs((now.Second * 1000 + now.Millisecond) - (lastUpdate[seriesNumber].Second * 1000 + lastUpdate[seriesNumber].Millisecond));
            double fps = 1000 / timeDiff;
            lastUpdate[seriesNumber] = DateTime.Now;

            dataPoints[seriesNumber].Add(new DataPoint(now.ToOADate(), Math.Round(fps, 3)));
        }

        private void updateChart(String seriesName){
            Series series;
            ChartArea area;
            int idNr;
            if (seriesName == "Camera")
            {
                series = cameraChart.Series["Camera"];
                area = cameraChart.ChartAreas["cArea"];
                idNr = 0;
            }
            else //seriesName == Glyph
            {
                series = glyphChart.Series["Glyph"];
                area = glyphChart.ChartAreas["cArea"];
                idNr = 1;
            }

            DataPointCollection points = series.Points;
            if (points.Count() >= dataPoints[idNr].Count())
            {
                dataPoints[idNr].Add(new DataPoint(DateTime.Now.ToOADate(), 0));
            }

            for (int i = points.Count(); i < dataPoints[idNr].Count(); i++)
            {
                points.Add(dataPoints[idNr].ElementAt(i));
            }
            
            area.AxisX.IntervalType = DateTimeIntervalType.Seconds;
            area.AxisX.Interval = 5;
            area.AxisX.LabelStyle.Format = "ss";
            area.AxisX.Minimum = DateTime.Now.AddMinutes(-1).ToOADate();

            if (idNr == 0)
            {
                cameraValue.Text = points.Last().YValues[0].ToString();
            }
            else
            {
                glyphValue.Text = points.Last().YValues[0].ToString();
            }
        }

        private void showLogsButton_Click(object sender, EventArgs e)
        {
            if (showLogsButton.Text == "Show Logs")
            {

                String[] cameraLogList = new String[dataPoints[0].Count()], glyphLogList = new String[dataPoints[1].Count()];
    
                DateTime xValue;
                for (int i = 0; i < cameraLogList.Length; i++)
                {
                    xValue = DateTime.FromOADate(dataPoints[0].ElementAt(i).XValue);
                    cameraLogList[i] = xValue.Second+":"+xValue.Millisecond + "       " + dataPoints[0].ElementAt(i).YValues[0];
                }

                Console.WriteLine(glyphLogList.Length);

                for (int i = 0; i < glyphLogList.Length; i++)
                {
                    xValue = DateTime.FromOADate(dataPoints[1].ElementAt(i).XValue);
                    glyphLogList[i] = xValue.Second + ":" + xValue.Millisecond + "       " + dataPoints[1].ElementAt(i).YValues[0];
                }
    
                cameraLog = new LogDisplay("Camera", cameraLogList);
                glyphLog = new LogDisplay("Glyph", glyphLogList);


            
                showLogsButton.Text = "Close Logs";
                cameraLog.BringToFront();
                cameraLog.Visible = true;

                glyphLog.BringToFront();
                glyphLog.Visible = true;
            }
            else
            {
                showLogsButton.Text = "Show Logs";
                cameraLog.Close();
                cameraLog.Dispose();

                glyphLog.Close();
                glyphLog.Dispose();
            }
        }

        public void prepareToClose()
        {
            timer.Stop();
        }
        LogDisplay cameraLog;
        LogDisplay glyphLog;

        DateTime[] lastUpdate = {DateTime.Now, DateTime.Now};
        List<DataPoint>[] dataPoints = {new List<DataPoint>(), new List<DataPoint>()}; //Camera and Glyph data points

        Timer timer = new Timer();

        private void TestFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            prepareToClose();
        }

        private void TestFrame_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.testFrame = null;
        }
    }
}
