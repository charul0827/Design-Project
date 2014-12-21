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
            this.strategySettingsBox = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // strategySettingsBox
            // 
            this.strategySettingsBox.AutoSize = true;
            this.strategySettingsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.strategySettingsBox.Location = new System.Drawing.Point(0, 7);
            this.strategySettingsBox.Margin = new System.Windows.Forms.Padding(0);
            this.strategySettingsBox.Name = "strategySettingsBox";
            this.strategySettingsBox.Padding = new System.Windows.Forms.Padding(0);
            this.strategySettingsBox.Size = new System.Drawing.Size(268, 168);
            this.strategySettingsBox.TabIndex = 0;
            this.strategySettingsBox.TabStop = false;
            this.strategySettingsBox.Text = "Control Strategy Settings";
            // 
            // ControlStrategy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.strategySettingsBox);
            this.Name = "ControlStrategy";
            this.Size = new System.Drawing.Size(268, 175);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox strategySettingsBox;
    }
}
