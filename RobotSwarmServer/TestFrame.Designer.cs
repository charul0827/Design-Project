namespace RobotSwarmServer
{
    partial class TestFrame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.cameraChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.showLogsButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cameraValue = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.glyphValue = new System.Windows.Forms.Label();
            this.glyphChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.cameraChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glyphChart)).BeginInit();
            this.SuspendLayout();
            // 
            // cameraChart
            // 
            chartArea1.Name = "cArea";
            this.cameraChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.cameraChart.Legends.Add(legend1);
            this.cameraChart.Location = new System.Drawing.Point(135, 12);
            this.cameraChart.Name = "cameraChart";
            series1.ChartArea = "cArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Camera";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.cameraChart.Series.Add(series1);
            this.cameraChart.Size = new System.Drawing.Size(553, 282);
            this.cameraChart.TabIndex = 0;
            this.cameraChart.Text = "cameraChart";
            // 
            // showLogsButton
            // 
            this.showLogsButton.Location = new System.Drawing.Point(12, 12);
            this.showLogsButton.Name = "showLogsButton";
            this.showLogsButton.Size = new System.Drawing.Size(117, 67);
            this.showLogsButton.TabIndex = 1;
            this.showLogsButton.Text = "Show Logs";
            this.showLogsButton.UseVisualStyleBackColor = true;
            this.showLogsButton.Click += new System.EventHandler(this.showLogsButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 214);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Camera FPS";
            // 
            // cameraValue
            // 
            this.cameraValue.AutoSize = true;
            this.cameraValue.Font = new System.Drawing.Font("Lucida Fax", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cameraValue.Location = new System.Drawing.Point(22, 243);
            this.cameraValue.Name = "cameraValue";
            this.cameraValue.Size = new System.Drawing.Size(82, 14);
            this.cameraValue.TabIndex = 3;
            this.cameraValue.Text = "Not initialized";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 287);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Glyph FPS";
            // 
            // glyphValue
            // 
            this.glyphValue.AutoSize = true;
            this.glyphValue.Font = new System.Drawing.Font("Lucida Fax", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glyphValue.Location = new System.Drawing.Point(22, 317);
            this.glyphValue.Name = "glyphValue";
            this.glyphValue.Size = new System.Drawing.Size(82, 14);
            this.glyphValue.TabIndex = 5;
            this.glyphValue.Text = "Not initialized";
            // 
            // glyphChart
            // 
            chartArea2.Name = "cArea";
            this.glyphChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.glyphChart.Legends.Add(legend2);
            this.glyphChart.Location = new System.Drawing.Point(135, 302);
            this.glyphChart.Name = "glyphChart";
            series2.ChartArea = "cArea";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Glyph";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.YValuesPerPoint = 2;
            this.glyphChart.Series.Add(series2);
            this.glyphChart.Size = new System.Drawing.Size(553, 282);
            this.glyphChart.TabIndex = 7;
            this.glyphChart.Text = "glyphChart";
            // 
            // TestFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 596);
            this.Controls.Add(this.glyphChart);
            this.Controls.Add(this.glyphValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cameraValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.showLogsButton);
            this.Controls.Add(this.cameraChart);
            this.Name = "TestFrame";
            this.Text = "TestFrame";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestFrame_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TestFrame_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.cameraChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glyphChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart cameraChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart glyphChart;
        private System.Windows.Forms.Button showLogsButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label cameraValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label glyphValue;
    }
}