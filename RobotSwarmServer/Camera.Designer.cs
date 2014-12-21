namespace RobotSwarmServer
{
    partial class Camera
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.includeCameraBox = new System.Windows.Forms.CheckBox();
            this.cameraImagePanel = new RobotSwarmServer.DoubleBufferedPanel();
            this.cameraStatusLabel = new System.Windows.Forms.Label();
            this.cameraImagePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // includeCameraBox
            // 
            this.includeCameraBox.AutoSize = true;
            this.includeCameraBox.Checked = true;
            this.includeCameraBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.includeCameraBox.Location = new System.Drawing.Point(0, 6);
            this.includeCameraBox.Name = "includeCameraBox";
            this.includeCameraBox.Size = new System.Drawing.Size(61, 17);
            this.includeCameraBox.TabIndex = 11;
            this.includeCameraBox.Text = "Include";
            this.includeCameraBox.UseVisualStyleBackColor = true;
            this.includeCameraBox.CheckedChanged += new System.EventHandler(this.includeCameraBox_CheckedChanged);
            // 
            // cameraImagePanel
            // 
            this.cameraImagePanel.Controls.Add(this.cameraStatusLabel);
            this.cameraImagePanel.Location = new System.Drawing.Point(0, 23);
            this.cameraImagePanel.Name = "cameraImagePanel";
            this.cameraImagePanel.Size = new System.Drawing.Size(199, 153);
            this.cameraImagePanel.TabIndex = 10;
            // 
            // cameraStatusLabel
            // 
            this.cameraStatusLabel.AutoSize = true;
            this.cameraStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.cameraStatusLabel.ForeColor = System.Drawing.Color.Green;
            this.cameraStatusLabel.Location = new System.Drawing.Point(3, 3);
            this.cameraStatusLabel.Name = "cameraStatusLabel";
            this.cameraStatusLabel.Size = new System.Drawing.Size(102, 13);
            this.cameraStatusLabel.TabIndex = 9;
            this.cameraStatusLabel.Text = "Camera Included";
            // 
            // Camera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.includeCameraBox);
            this.Controls.Add(this.cameraImagePanel);
            this.Name = "Camera";
            this.Size = new System.Drawing.Size(202, 176);
            this.cameraImagePanel.ResumeLayout(false);
            this.cameraImagePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label cameraStatusLabel;
        private DoubleBufferedPanel cameraImagePanel;
        private System.Windows.Forms.CheckBox includeCameraBox;

    }
}
