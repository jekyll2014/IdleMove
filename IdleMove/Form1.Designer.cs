
namespace IdleMove
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.checkBox_enable = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_time = new System.Windows.Forms.TextBox();
            this.checkBox_enableHotKey = new System.Windows.Forms.CheckBox();
            this.checkBox_enableSchedule = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_from = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_to = new System.Windows.Forms.TextBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // checkBox_enable
            // 
            this.checkBox_enable.AutoSize = true;
            this.checkBox_enable.Location = new System.Drawing.Point(12, 12);
            this.checkBox_enable.Name = "checkBox_enable";
            this.checkBox_enable.Size = new System.Drawing.Size(59, 17);
            this.checkBox_enable.TabIndex = 0;
            this.checkBox_enable.Text = "Enable";
            this.checkBox_enable.UseVisualStyleBackColor = true;
            this.checkBox_enable.CheckedChanged += new System.EventHandler(this.CheckBox_enable_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Idle time";
            // 
            // textBox_time
            // 
            this.textBox_time.Location = new System.Drawing.Point(61, 35);
            this.textBox_time.Name = "textBox_time";
            this.textBox_time.Size = new System.Drawing.Size(69, 20);
            this.textBox_time.TabIndex = 2;
            this.textBox_time.Leave += new System.EventHandler(this.TextBox_time_Leave);
            // 
            // checkBox_enableHotKey
            // 
            this.checkBox_enableHotKey.AutoSize = true;
            this.checkBox_enableHotKey.Location = new System.Drawing.Point(12, 61);
            this.checkBox_enableHotKey.Name = "checkBox_enableHotKey";
            this.checkBox_enableHotKey.Size = new System.Drawing.Size(118, 17);
            this.checkBox_enableHotKey.TabIndex = 0;
            this.checkBox_enableHotKey.Text = "Enable ESC hotkey";
            this.checkBox_enableHotKey.UseVisualStyleBackColor = true;
            this.checkBox_enableHotKey.CheckedChanged += new System.EventHandler(this.CheckBox_enableHotKey_CheckedChanged);
            // 
            // checkBox_enableSchedule
            // 
            this.checkBox_enableSchedule.AutoSize = true;
            this.checkBox_enableSchedule.Location = new System.Drawing.Point(12, 84);
            this.checkBox_enableSchedule.Name = "checkBox_enableSchedule";
            this.checkBox_enableSchedule.Size = new System.Drawing.Size(105, 17);
            this.checkBox_enableSchedule.TabIndex = 0;
            this.checkBox_enableSchedule.Text = "Enable schedule";
            this.checkBox_enableSchedule.UseVisualStyleBackColor = true;
            this.checkBox_enableSchedule.CheckedChanged += new System.EventHandler(this.CheckBox_enableSchedule_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "From";
            // 
            // textBox_from
            // 
            this.textBox_from.Location = new System.Drawing.Point(45, 107);
            this.textBox_from.Name = "textBox_from";
            this.textBox_from.Size = new System.Drawing.Size(85, 20);
            this.textBox_from.TabIndex = 2;
            this.textBox_from.Leave += new System.EventHandler(this.TextBox_from_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "To";
            // 
            // textBox_to
            // 
            this.textBox_to.Location = new System.Drawing.Point(45, 133);
            this.textBox_to.Name = "textBox_to";
            this.textBox_to.Size = new System.Drawing.Size(85, 20);
            this.textBox_to.TabIndex = 2;
            this.textBox_to.Leave += new System.EventHandler(this.TextBox_to_Leave);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.NotifyIcon1_DoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(144, 172);
            this.Controls.Add(this.textBox_to);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_from);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_time);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox_enableSchedule);
            this.Controls.Add(this.checkBox_enableHotKey);
            this.Controls.Add(this.checkBox_enable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "IdleMove";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_enable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_time;
        private System.Windows.Forms.CheckBox checkBox_enableHotKey;
        private System.Windows.Forms.CheckBox checkBox_enableSchedule;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_from;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_to;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

