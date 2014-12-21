namespace RobotSwarmServer
{
    partial class ImageProcessing
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
            this.automataCanvas = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // automataCanvas
            // 
            this.automataCanvas.AutoSize = true;
            this.automataCanvas.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.automataCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.automataCanvas.Location = new System.Drawing.Point(0, 0);
            this.automataCanvas.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.automataCanvas.Name = "automataCanvas";
            this.automataCanvas.Size = new System.Drawing.Size(319, 321);
            this.automataCanvas.TabIndex = 6;
            // 
            // ImageProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.automataCanvas);
            this.Name = "ImageProcessing";
            this.Size = new System.Drawing.Size(319, 321);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel automataCanvas;

    }
}