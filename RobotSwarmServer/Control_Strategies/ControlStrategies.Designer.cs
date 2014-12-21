namespace RobotSwarmServer
{
    partial class ControlStrategies
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
            this.strategyListBox = new System.Windows.Forms.ListBox();
            this.applyStrategyButton = new System.Windows.Forms.Button();
            this.pointXBox = new System.Windows.Forms.TextBox();
            this.pointXLabel = new System.Windows.Forms.Label();
            this.setPointButton = new System.Windows.Forms.Button();
            this.pointYBox = new System.Windows.Forms.TextBox();
            this.pointYLabel = new System.Windows.Forms.Label();
            this.buttonHaltUnhalt = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.activeStrategy = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // strategyListBox
            // 
            this.strategyListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.strategyListBox.FormattingEnabled = true;
            this.strategyListBox.Location = new System.Drawing.Point(6, 59);
            this.strategyListBox.Name = "strategyListBox";
            this.strategyListBox.Size = new System.Drawing.Size(256, 173);
            this.strategyListBox.TabIndex = 0;
            // 
            // applyStrategyButton
            // 
            this.applyStrategyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.applyStrategyButton.Location = new System.Drawing.Point(136, 238);
            this.applyStrategyButton.Name = "applyStrategyButton";
            this.applyStrategyButton.Size = new System.Drawing.Size(126, 26);
            this.applyStrategyButton.TabIndex = 1;
            this.applyStrategyButton.Text = "Apply Strategy";
            this.applyStrategyButton.UseVisualStyleBackColor = true;
            this.applyStrategyButton.Click += new System.EventHandler(this.applyStrategyButton_Click);
            // 
            // pointXBox
            // 
            this.pointXBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.pointXBox.Location = new System.Drawing.Point(30, 297);
            this.pointXBox.Name = "pointXBox";
            this.pointXBox.Size = new System.Drawing.Size(34, 20);
            this.pointXBox.TabIndex = 3;
            // 
            // pointXLabel
            // 
            this.pointXLabel.AutoSize = true;
            this.pointXLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.pointXLabel.Location = new System.Drawing.Point(7, 300);
            this.pointXLabel.Name = "pointXLabel";
            this.pointXLabel.Size = new System.Drawing.Size(17, 13);
            this.pointXLabel.TabIndex = 4;
            this.pointXLabel.Text = "X:";
            // 
            // setPointButton
            // 
            this.setPointButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.setPointButton.Location = new System.Drawing.Point(137, 294);
            this.setPointButton.Name = "setPointButton";
            this.setPointButton.Size = new System.Drawing.Size(77, 24);
            this.setPointButton.TabIndex = 5;
            this.setPointButton.Text = "Save point";
            this.setPointButton.UseVisualStyleBackColor = true;
            this.setPointButton.Click += new System.EventHandler(this.setPointButton_Click);
            // 
            // pointYBox
            // 
            this.pointYBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.pointYBox.Location = new System.Drawing.Point(93, 297);
            this.pointYBox.Name = "pointYBox";
            this.pointYBox.Size = new System.Drawing.Size(38, 20);
            this.pointYBox.TabIndex = 6;
            // 
            // pointYLabel
            // 
            this.pointYLabel.AutoSize = true;
            this.pointYLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.pointYLabel.Location = new System.Drawing.Point(70, 300);
            this.pointYLabel.Name = "pointYLabel";
            this.pointYLabel.Size = new System.Drawing.Size(17, 13);
            this.pointYLabel.TabIndex = 7;
            this.pointYLabel.Text = "Y:";
            // 
            // buttonHaltUnhalt
            // 
            this.buttonHaltUnhalt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.buttonHaltUnhalt.Location = new System.Drawing.Point(6, 239);
            this.buttonHaltUnhalt.Name = "buttonHaltUnhalt";
            this.buttonHaltUnhalt.Size = new System.Drawing.Size(125, 25);
            this.buttonHaltUnhalt.TabIndex = 26;
            this.buttonHaltUnhalt.Text = "Halt";
            this.buttonHaltUnhalt.Click += new System.EventHandler(this.HaltCommunication);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.activeStrategy);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.strategyListBox);
            this.groupBox1.Controls.Add(this.pointYLabel);
            this.groupBox1.Controls.Add(this.buttonHaltUnhalt);
            this.groupBox1.Controls.Add(this.pointYBox);
            this.groupBox1.Controls.Add(this.applyStrategyButton);
            this.groupBox1.Controls.Add(this.setPointButton);
            this.groupBox1.Controls.Add(this.pointXLabel);
            this.groupBox1.Controls.Add(this.pointXBox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.groupBox1.Location = new System.Drawing.Point(0, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 330);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control Strategies";
            // 
            // activeStrategy
            // 
            this.activeStrategy.AutoSize = true;
            this.activeStrategy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.activeStrategy.Location = new System.Drawing.Point(98, 31);
            this.activeStrategy.Name = "activeStrategy";
            this.activeStrategy.Size = new System.Drawing.Size(52, 20);
            this.activeStrategy.TabIndex = 30;
            this.activeStrategy.Text = "None";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(7, 278);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Custom referencepoint";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(2, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Active strategy: ";
            // 
            // ControlStrategies
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.MaximumSize = new System.Drawing.Size(269, 404);
            this.MinimumSize = new System.Drawing.Size(269, 404);
            this.Name = "ControlStrategies";
            this.Size = new System.Drawing.Size(269, 404);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox strategyListBox;
        private System.Windows.Forms.Button applyStrategyButton;
        private System.Windows.Forms.TextBox pointXBox;
        private System.Windows.Forms.Label pointXLabel;
        private System.Windows.Forms.Button setPointButton;
        private System.Windows.Forms.TextBox pointYBox;
        private System.Windows.Forms.Label pointYLabel;
        private System.Windows.Forms.Button buttonHaltUnhalt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label activeStrategy;
    }
}
