namespace RobotSwarmServer
{
    partial class CameraController
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
            this.label1 = new System.Windows.Forms.Label();
            this.updateCamerasButton = new System.Windows.Forms.Button();
            this.cameraStatusLabel = new System.Windows.Forms.Label();
            this.cameraPanel = new System.Windows.Forms.TableLayoutPanel();
            this.resolutionDropDown = new System.Windows.Forms.ComboBox();
            this.samplingTimeBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.load_button = new System.Windows.Forms.Button();
            this.calibrateButton = new System.Windows.Forms.Button();
            this.squareSizeBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(1, -160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 9;
            // 
            // updateCamerasButton
            // 
            this.updateCamerasButton.Location = new System.Drawing.Point(6, 87);
            this.updateCamerasButton.Name = "updateCamerasButton";
            this.updateCamerasButton.Size = new System.Drawing.Size(97, 23);
            this.updateCamerasButton.TabIndex = 56;
            this.updateCamerasButton.Text = "Update Cameras";
            this.updateCamerasButton.UseVisualStyleBackColor = true;
            this.updateCamerasButton.Click += new System.EventHandler(this.updateCamerasButton_Click);
            // 
            // cameraStatusLabel
            // 
            this.cameraStatusLabel.AutoSize = true;
            this.cameraStatusLabel.Location = new System.Drawing.Point(236, 92);
            this.cameraStatusLabel.Name = "cameraStatusLabel";
            this.cameraStatusLabel.Size = new System.Drawing.Size(111, 13);
            this.cameraStatusLabel.TabIndex = 58;
            this.cameraStatusLabel.Text = "Cameras not initialized";
            // 
            // cameraPanel
            // 
            this.cameraPanel.ColumnCount = 1;
            this.cameraPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.cameraPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.cameraPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cameraPanel.Location = new System.Drawing.Point(0, 116);
            this.cameraPanel.Name = "cameraPanel";
            this.cameraPanel.RowCount = 1;
            this.cameraPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 207F));
            this.cameraPanel.Size = new System.Drawing.Size(934, 207);
            this.cameraPanel.TabIndex = 59;
            // 
            // resolutionDropDown
            // 
            this.resolutionDropDown.FormattingEnabled = true;
            this.resolutionDropDown.Location = new System.Drawing.Point(109, 89);
            this.resolutionDropDown.Name = "resolutionDropDown";
            this.resolutionDropDown.Size = new System.Drawing.Size(121, 21);
            this.resolutionDropDown.TabIndex = 60;
            this.resolutionDropDown.SelectedIndexChanged += new System.EventHandler(this.resolutionDropDown_SelectedIndexChanged);
            // 
            // samplingTimeBox
            // 
            this.samplingTimeBox.Location = new System.Drawing.Point(191, 14);
            this.samplingTimeBox.Name = "samplingTimeBox";
            this.samplingTimeBox.Size = new System.Drawing.Size(46, 20);
            this.samplingTimeBox.TabIndex = 61;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 62;
            this.label2.Text = "Camera sampling rate [ms]:";
            // 
            // load_button
            // 
            this.load_button.Location = new System.Drawing.Point(331, 41);
            this.load_button.Name = "load_button";
            this.load_button.Size = new System.Drawing.Size(143, 23);
            this.load_button.TabIndex = 64;
            this.load_button.Text = "Load last calibration";
            this.load_button.UseVisualStyleBackColor = true;
            this.load_button.Click += new System.EventHandler(this.load_button_Click);
            // 
            // calibrateButton
            // 
            this.calibrateButton.Location = new System.Drawing.Point(331, 12);
            this.calibrateButton.Name = "calibrateButton";
            this.calibrateButton.Size = new System.Drawing.Size(143, 23);
            this.calibrateButton.TabIndex = 63;
            this.calibrateButton.Text = "Calibrate multiple cameras";
            this.calibrateButton.UseVisualStyleBackColor = true;
            this.calibrateButton.Click += new System.EventHandler(this.calibrateButton_Click);
            // 
            // squareSizeBox
            // 
            this.squareSizeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.squareSizeBox.Location = new System.Drawing.Point(191, 40);
            this.squareSizeBox.Name = "squareSizeBox";
            this.squareSizeBox.Size = new System.Drawing.Size(46, 20);
            this.squareSizeBox.TabIndex = 65;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label3.Location = new System.Drawing.Point(3, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 13);
            this.label3.TabIndex = 66;
            this.label3.Text = "Square size to GRATF [% of Botsize]:";
            // 
            // CameraController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.squareSizeBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.load_button);
            this.Controls.Add(this.calibrateButton);
            this.Controls.Add(this.samplingTimeBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.resolutionDropDown);
            this.Controls.Add(this.cameraPanel);
            this.Controls.Add(this.cameraStatusLabel);
            this.Controls.Add(this.updateCamerasButton);
            this.Controls.Add(this.label1);
            this.Name = "CameraController";
            this.Size = new System.Drawing.Size(934, 323);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private void AddCameraPanels()
        {
            this.cameraPanel.SuspendLayout();
            this.SuspendLayout();

            // robotPanel
            this.cameraPanel.ColumnCount = allCameras.Count;

            for (int i = 1; i <= allCameras.Count; i++)
            {
                this.cameraPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, (float)100 / (float)allCameras.Count));
            }

            this.cameraPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button updateCamerasButton;
        private System.Windows.Forms.Label cameraStatusLabel;
        private System.Windows.Forms.TableLayoutPanel cameraPanel;
        private System.Windows.Forms.ComboBox resolutionDropDown;
        private System.Windows.Forms.TextBox samplingTimeBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button load_button;
        private System.Windows.Forms.Button calibrateButton;
        private System.Windows.Forms.TextBox squareSizeBox;
        private System.Windows.Forms.Label label3;
    }
}
