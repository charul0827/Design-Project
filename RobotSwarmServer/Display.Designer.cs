namespace RobotSwarmServer
{
    partial class Display
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.paintStrategyBox = new System.Windows.Forms.CheckBox();
            this.displayVideoBox = new System.Windows.Forms.CheckBox();
            this.enableGraphicsBox = new System.Windows.Forms.CheckBox();
            this.graphicsBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graphicsBox)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.paintStrategyBox);
            this.splitContainer1.Panel1.Controls.Add(this.displayVideoBox);
            this.splitContainer1.Panel1.Controls.Add(this.enableGraphicsBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.graphicsBox);
            this.splitContainer1.Size = new System.Drawing.Size(802, 629);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // paintStrategyBox
            // 
            this.paintStrategyBox.AutoSize = true;
            this.paintStrategyBox.Location = new System.Drawing.Point(114, 5);
            this.paintStrategyBox.Name = "paintStrategyBox";
            this.paintStrategyBox.Size = new System.Drawing.Size(92, 17);
            this.paintStrategyBox.TabIndex = 24;
            this.paintStrategyBox.Text = "Paint Strategy";
            this.paintStrategyBox.UseVisualStyleBackColor = true;
            // 
            // displayVideoBox
            // 
            this.displayVideoBox.AutoSize = true;
            this.displayVideoBox.Checked = true;
            this.displayVideoBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.displayVideoBox.Location = new System.Drawing.Point(212, 5);
            this.displayVideoBox.Name = "displayVideoBox";
            this.displayVideoBox.Size = new System.Drawing.Size(88, 17);
            this.displayVideoBox.TabIndex = 23;
            this.displayVideoBox.Text = "Enable video";
            this.displayVideoBox.UseVisualStyleBackColor = true;
            this.displayVideoBox.CheckedChanged += new System.EventHandler(this.displayVideoBox_CheckedChanged);
            // 
            // enableGraphicsBox
            // 
            this.enableGraphicsBox.AutoSize = true;
            this.enableGraphicsBox.Checked = true;
            this.enableGraphicsBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableGraphicsBox.Location = new System.Drawing.Point(12, 5);
            this.enableGraphicsBox.Name = "enableGraphicsBox";
            this.enableGraphicsBox.Size = new System.Drawing.Size(96, 17);
            this.enableGraphicsBox.TabIndex = 22;
            this.enableGraphicsBox.Text = "Draw Graphics";
            this.enableGraphicsBox.UseVisualStyleBackColor = true;
            // 
            // graphicsBox
            // 
            this.graphicsBox.BackColor = System.Drawing.Color.Transparent;
            this.graphicsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphicsBox.Location = new System.Drawing.Point(0, 0);
            this.graphicsBox.Margin = new System.Windows.Forms.Padding(0);
            this.graphicsBox.Name = "graphicsBox";
            this.graphicsBox.Size = new System.Drawing.Size(802, 603);
            this.graphicsBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.graphicsBox.TabIndex = 4;
            this.graphicsBox.TabStop = false;
            this.graphicsBox.Click += new System.EventHandler(this.graphicsBox_Click);
            // 
            // Display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(802, 629);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Display";
            this.Text = "Display";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Display_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Display_FormClosed);
            this.Load += new System.EventHandler(this.Display_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.graphicsBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.PictureBox graphicsBox;
        private System.Windows.Forms.CheckBox displayVideoBox;
        private System.Windows.Forms.CheckBox enableGraphicsBox;
        private System.Windows.Forms.CheckBox paintStrategyBox;



    }
}