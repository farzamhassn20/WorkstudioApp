
namespace WindowsFormsApp1
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
            this.components = new System.ComponentModel.Container();
            this.textBox_AllLog = new System.Windows.Forms.TextBox();
            this.appfront = new System.Windows.Forms.TextBox();
            this.timer_1sec = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.timer_10sec = new System.Windows.Forms.Timer(this.components);
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.timer10_min = new System.Windows.Forms.Timer(this.components);
            this.timer2min = new System.Windows.Forms.Timer(this.components);
            this.textBox_ActiveBrowserWindowURL = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer_random = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // textBox_AllLog
            // 
            this.textBox_AllLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_AllLog.Location = new System.Drawing.Point(12, 586);
            this.textBox_AllLog.Name = "textBox_AllLog";
            this.textBox_AllLog.ReadOnly = true;
            this.textBox_AllLog.Size = new System.Drawing.Size(238, 29);
            this.textBox_AllLog.TabIndex = 2;
            this.textBox_AllLog.Text = "Mouse Position";
            this.textBox_AllLog.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // appfront
            // 
            this.appfront.BackColor = System.Drawing.SystemColors.Menu;
            this.appfront.Location = new System.Drawing.Point(12, 67);
            this.appfront.Multiline = true;
            this.appfront.Name = "appfront";
            this.appfront.Size = new System.Drawing.Size(328, 513);
            this.appfront.TabIndex = 7;
            // 
            // timer_1sec
            // 
            this.timer_1sec.Enabled = true;
            this.timer_1sec.Interval = 1000;
            this.timer_1sec.Tick += new System.EventHandler(this.timer1_tick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 39);
            this.button2.TabIndex = 8;
            this.button2.Text = "Start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(111, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 39);
            this.button1.TabIndex = 9;
            this.button1.Text = "Show running apps";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(428, 13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(93, 38);
            this.button3.TabIndex = 10;
            this.button3.Text = "Clear";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(527, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 25);
            this.label1.TabIndex = 11;
            this.label1.Text = "00:00:00";
            // 
            // timer_10sec
            // 
            this.timer_10sec.Interval = 10000;
            this.timer_10sec.Tick += new System.EventHandler(this.timer_10sec_Tick);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(210, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(93, 39);
            this.button4.TabIndex = 12;
            this.button4.Text = "Take Window Screenshot";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(309, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(113, 39);
            this.button5.TabIndex = 13;
            this.button5.Text = "Take Active Window Screenshot";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // timer10_min
            // 
            this.timer10_min.Interval = 600000;
            this.timer10_min.Tick += new System.EventHandler(this.timer10_min_Tick);
            // 
            // timer2min
            // 
            this.timer2min.Interval = 120000;
            this.timer2min.Tick += new System.EventHandler(this.timer2min_Tick);
            // 
            // textBox_ActiveBrowserWindowURL
            // 
            this.textBox_ActiveBrowserWindowURL.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox_ActiveBrowserWindowURL.Location = new System.Drawing.Point(346, 67);
            this.textBox_ActiveBrowserWindowURL.Multiline = true;
            this.textBox_ActiveBrowserWindowURL.Name = "textBox_ActiveBrowserWindowURL";
            this.textBox_ActiveBrowserWindowURL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_ActiveBrowserWindowURL.Size = new System.Drawing.Size(277, 513);
            this.textBox_ActiveBrowserWindowURL.TabIndex = 14;
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            // 
            // timer_random
            // 
            this.timer_random.Interval = 10000;
            this.timer_random.Tick += new System.EventHandler(this.timer_random_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(628, 627);
            this.Controls.Add(this.textBox_ActiveBrowserWindowURL);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.appfront);
            this.Controls.Add(this.textBox_AllLog);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox_AllLog;
        private System.Windows.Forms.TextBox appfront;
        private System.Windows.Forms.Timer timer_1sec;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer_10sec;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Timer timer10_min;
        private System.Windows.Forms.Timer timer2min;
        private System.Windows.Forms.TextBox textBox_ActiveBrowserWindowURL;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer_random;
    }
}

