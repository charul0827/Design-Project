namespace RobotSwarmServer
{
    partial class Settings
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
            this.applyButton = new System.Windows.Forms.Button();
            this.displayButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.transmissionBox = new System.Windows.Forms.TextBox();
            this.enableDatalog = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.collisionAvoidanceBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dispersionBox = new System.Windows.Forms.TextBox();
            this.testFrameButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numberOfRobotsBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.communicationPanel = new System.Windows.Forms.Panel();
            this.controlStrategySettingsPanel = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // applyButton
            // 
            this.applyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.applyButton.Location = new System.Drawing.Point(145, 158);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(117, 23);
            this.applyButton.TabIndex = 5;
            this.applyButton.Text = "Apply settings";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // displayButton
            // 
            this.displayButton.Location = new System.Drawing.Point(142, 13);
            this.displayButton.Name = "displayButton";
            this.displayButton.Size = new System.Drawing.Size(130, 45);
            this.displayButton.TabIndex = 11;
            this.displayButton.Text = "Show display";
            this.displayButton.UseVisualStyleBackColor = true;
            this.displayButton.Click += new System.EventHandler(this.displayButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label4.Location = new System.Drawing.Point(42, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Transmission range [% of Botsize]:";
            // 
            // transmissionBox
            // 
            this.transmissionBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.transmissionBox.Location = new System.Drawing.Point(215, 80);
            this.transmissionBox.Name = "transmissionBox";
            this.transmissionBox.Size = new System.Drawing.Size(46, 20);
            this.transmissionBox.TabIndex = 2;
            // 
            // enableDatalog
            // 
            this.enableDatalog.AutoSize = true;
            this.enableDatalog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.enableDatalog.Location = new System.Drawing.Point(6, 29);
            this.enableDatalog.Name = "enableDatalog";
            this.enableDatalog.Size = new System.Drawing.Size(132, 17);
            this.enableDatalog.TabIndex = 6;
            this.enableDatalog.Text = "Enable Datalog (*.csv)";
            this.enableDatalog.UseVisualStyleBackColor = true;
            this.enableDatalog.CheckedChanged += new System.EventHandler(this.enableDatalog_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label6.Location = new System.Drawing.Point(12, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(197, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Collision avoidance range [% of Botsize]:";
            // 
            // collisionAvoidanceBox
            // 
            this.collisionAvoidanceBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.collisionAvoidanceBox.Location = new System.Drawing.Point(215, 106);
            this.collisionAvoidanceBox.Name = "collisionAvoidanceBox";
            this.collisionAvoidanceBox.Size = new System.Drawing.Size(46, 20);
            this.collisionAvoidanceBox.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label7.Location = new System.Drawing.Point(49, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(160, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Dispersion Range [% of Botsize]:";
            // 
            // dispersionBox
            // 
            this.dispersionBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dispersionBox.Location = new System.Drawing.Point(215, 132);
            this.dispersionBox.Name = "dispersionBox";
            this.dispersionBox.Size = new System.Drawing.Size(46, 20);
            this.dispersionBox.TabIndex = 4;
            // 
            // testFrameButton
            // 
            this.testFrameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.testFrameButton.Location = new System.Drawing.Point(144, 25);
            this.testFrameButton.Name = "testFrameButton";
            this.testFrameButton.Size = new System.Drawing.Size(117, 23);
            this.testFrameButton.TabIndex = 8;
            this.testFrameButton.Text = "Show Graphs";
            this.testFrameButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.testFrameButton.UseVisualStyleBackColor = true;
            this.testFrameButton.Click += new System.EventHandler(this.testFrameButton_Click);
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.ForestGreen;
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.startButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.startButton.Location = new System.Drawing.Point(6, 13);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(130, 45);
            this.startButton.TabIndex = 18;
            this.startButton.Text = "Start simulation";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numberOfRobotsBox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.applyButton);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.transmissionBox);
            this.groupBox1.Controls.Add(this.dispersionBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.collisionAvoidanceBox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.groupBox1.Location = new System.Drawing.Point(6, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 187);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Simulation settings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.Location = new System.Drawing.Point(118, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Number of robots:";
            // 
            // numberOfRobotsBox
            // 
            this.numberOfRobotsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.numberOfRobotsBox.Location = new System.Drawing.Point(215, 37);
            this.numberOfRobotsBox.Name = "numberOfRobotsBox";
            this.numberOfRobotsBox.Size = new System.Drawing.Size(46, 20);
            this.numberOfRobotsBox.TabIndex = 14;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.testFrameButton);
            this.groupBox2.Controls.Add(this.enableDatalog);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.groupBox2.Location = new System.Drawing.Point(6, 413);
            this.groupBox2.MaximumSize = new System.Drawing.Size(268, 59);
            this.groupBox2.MinimumSize = new System.Drawing.Size(268, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(268, 59);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Diagnostic tools";
            // 
            // communicationPanel
            // 
            this.communicationPanel.Location = new System.Drawing.Point(6, 267);
            this.communicationPanel.Name = "communicationPanel";
            this.communicationPanel.Size = new System.Drawing.Size(268, 140);
            this.communicationPanel.TabIndex = 21;
            // 
            // controlStrategySettingsPanel
            // 
            this.controlStrategySettingsPanel.Location = new System.Drawing.Point(6, 479);
            this.controlStrategySettingsPanel.Name = "controlStrategySettingsPanel";
            this.controlStrategySettingsPanel.Size = new System.Drawing.Size(268, 175);
            this.controlStrategySettingsPanel.TabIndex = 22;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.controlStrategySettingsPanel);
            this.Controls.Add(this.communicationPanel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.displayButton);
            this.Name = "Settings";
            this.Size = new System.Drawing.Size(281, 661);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button displayButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox transmissionBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox collisionAvoidanceBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox dispersionBox;
        public System.Windows.Forms.CheckBox enableDatalog;
        private System.Windows.Forms.Button testFrameButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel communicationPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox numberOfRobotsBox;
        private System.Windows.Forms.Panel controlStrategySettingsPanel;
    }
}
