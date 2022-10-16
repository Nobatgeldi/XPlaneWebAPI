namespace QTGTest
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.altitude = new System.Windows.Forms.TextBox();
            this.speed = new System.Windows.Forms.TextBox();
            this.v_speed = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // altitude
            // 
            this.altitude.Location = new System.Drawing.Point(137, 14);
            this.altitude.Name = "altitude";
            this.altitude.Size = new System.Drawing.Size(100, 20);
            this.altitude.TabIndex = 1;
            // 
            // speed
            // 
            this.speed.Location = new System.Drawing.Point(137, 51);
            this.speed.Name = "speed";
            this.speed.Size = new System.Drawing.Size(100, 20);
            this.speed.TabIndex = 2;
            // 
            // v_speed
            // 
            this.v_speed.Location = new System.Drawing.Point(137, 88);
            this.v_speed.Name = "v_speed";
            this.v_speed.Size = new System.Drawing.Size(100, 20);
            this.v_speed.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.v_speed);
            this.Controls.Add(this.speed);
            this.Controls.Add(this.altitude);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox altitude;
        private System.Windows.Forms.TextBox speed;
        private System.Windows.Forms.TextBox v_speed;
    }
}

