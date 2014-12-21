namespace RobotSwarmServer.Control_Strategies
{
    partial class ControlStrategy
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
            this.fifo1 = new RobotSwarmServer.Control_Strategies.Strategies.Collision_Avoidance.FIFO.FIFO();
            this.SuspendLayout();
            // 
            // fifo1
            // 
            this.fifo1.AutoSize = true;
            this.fifo1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.fifo1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.fifo1.Location = new System.Drawing.Point(74, 33);
            this.fifo1.MaximumSize = new System.Drawing.Size(250, 150);
            this.fifo1.Name = "fifo1";
            this.fifo1.Size = new System.Drawing.Size(109, 77);
            this.fifo1.TabIndex = 0;
            // 
            // ControlStrategy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.fifo1);
            this.MaximumSize = new System.Drawing.Size(250, 150);
            this.Name = "ControlStrategy";
            this.Size = new System.Drawing.Size(246, 146);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Strategies.Collision_Avoidance.FIFO.FIFO fifo1;
    }
}
