
namespace Hex_Control
{
    partial class RotationLength
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LinearLength = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.ActLength = new System.Windows.Forms.TextBox();
            this.RotPulses = new System.Windows.Forms.TextBox();
            this.PulseperRotations = new System.Windows.Forms.Label();
            this.RotLength = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sendCom = new System.Windows.Forms.Button();
            this.retOrigin = new System.Windows.Forms.Button();
            this.Test_String = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.s_xPos = new System.Windows.Forms.TextBox();
            this.xlabel = new System.Windows.Forms.Label();
            this.Ylabel = new System.Windows.Forms.Label();
            this.s_yPos = new System.Windows.Forms.TextBox();
            this.zlabel = new System.Windows.Forms.Label();
            this.s_zPos = new System.Windows.Forms.TextBox();
            this.wlabel = new System.Windows.Forms.Label();
            this.s_wPos = new System.Windows.Forms.TextBox();
            this.vlabel = new System.Windows.Forms.Label();
            this.s_vPos = new System.Windows.Forms.TextBox();
            this.ulabel = new System.Windows.Forms.Label();
            this.s_uPos = new System.Windows.Forms.TextBox();
            this.RecText = new System.Windows.Forms.Label();
            this.sinButton = new System.Windows.Forms.Button();
            this.yawSinButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LinearLength
            // 
            this.LinearLength.AutoSize = true;
            this.LinearLength.Location = new System.Drawing.Point(26, 26);
            this.LinearLength.Name = "LinearLength";
            this.LinearLength.Size = new System.Drawing.Size(145, 17);
            this.LinearLength.TabIndex = 0;
            this.LinearLength.Text = "Actuator Length (mm)";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(2294, 165);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(130, 22);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(2308, 170);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(244, 22);
            this.textBox2.TabIndex = 2;
            // 
            // ActLength
            // 
            this.ActLength.Location = new System.Drawing.Point(187, 23);
            this.ActLength.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ActLength.Name = "ActLength";
            this.ActLength.Size = new System.Drawing.Size(59, 22);
            this.ActLength.TabIndex = 3;
            this.ActLength.Text = "50";
            this.ActLength.TextChanged += new System.EventHandler(this.ActLength_TextChanged);
            // 
            // RotPulses
            // 
            this.RotPulses.Location = new System.Drawing.Point(187, 76);
            this.RotPulses.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RotPulses.Name = "RotPulses";
            this.RotPulses.Size = new System.Drawing.Size(59, 22);
            this.RotPulses.TabIndex = 5;
            this.RotPulses.Text = "10000";
            // 
            // PulseperRotations
            // 
            this.PulseperRotations.AutoSize = true;
            this.PulseperRotations.Location = new System.Drawing.Point(26, 79);
            this.PulseperRotations.Name = "PulseperRotations";
            this.PulseperRotations.Size = new System.Drawing.Size(107, 17);
            this.PulseperRotations.TabIndex = 4;
            this.PulseperRotations.Text = "Pulses/Rotation";
            this.PulseperRotations.Click += new System.EventHandler(this.label1_Click);
            // 
            // RotLength
            // 
            this.RotLength.Location = new System.Drawing.Point(187, 50);
            this.RotLength.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RotLength.Name = "RotLength";
            this.RotLength.Size = new System.Drawing.Size(59, 22);
            this.RotLength.TabIndex = 7;
            this.RotLength.Text = "5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Rotation Length (mm)";
            // 
            // sendCom
            // 
            this.sendCom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendCom.Location = new System.Drawing.Point(565, 327);
            this.sendCom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sendCom.Name = "sendCom";
            this.sendCom.Size = new System.Drawing.Size(140, 23);
            this.sendCom.TabIndex = 8;
            this.sendCom.Text = "Send Command";
            this.sendCom.UseVisualStyleBackColor = true;
            this.sendCom.Click += new System.EventHandler(this.sendCom_Click);
            // 
            // retOrigin
            // 
            this.retOrigin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.retOrigin.Location = new System.Drawing.Point(723, 327);
            this.retOrigin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.retOrigin.Name = "retOrigin";
            this.retOrigin.Size = new System.Drawing.Size(144, 23);
            this.retOrigin.TabIndex = 9;
            this.retOrigin.Text = "Return to Origin";
            this.retOrigin.UseVisualStyleBackColor = true;
            this.retOrigin.Click += new System.EventHandler(this.retOrigin_Click);
            // 
            // Test_String
            // 
            this.Test_String.AutoSize = true;
            this.Test_String.Location = new System.Drawing.Point(12, 275);
            this.Test_String.Name = "Test_String";
            this.Test_String.Size = new System.Drawing.Size(46, 17);
            this.Test_String.TabIndex = 10;
            this.Test_String.Text = "label1";
            this.Test_String.Click += new System.EventHandler(this.Test_String_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(289, 23);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(162, 21);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Separate Commands";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // s_xPos
            // 
            this.s_xPos.Location = new System.Drawing.Point(86, 126);
            this.s_xPos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.s_xPos.Name = "s_xPos";
            this.s_xPos.Size = new System.Drawing.Size(52, 22);
            this.s_xPos.TabIndex = 12;
            // 
            // xlabel
            // 
            this.xlabel.AutoSize = true;
            this.xlabel.Location = new System.Drawing.Point(22, 129);
            this.xlabel.Name = "xlabel";
            this.xlabel.Size = new System.Drawing.Size(53, 17);
            this.xlabel.TabIndex = 13;
            this.xlabel.Text = "X (mm)";
            // 
            // Ylabel
            // 
            this.Ylabel.AutoSize = true;
            this.Ylabel.Location = new System.Drawing.Point(166, 129);
            this.Ylabel.Name = "Ylabel";
            this.Ylabel.Size = new System.Drawing.Size(53, 17);
            this.Ylabel.TabIndex = 15;
            this.Ylabel.Text = "Y (mm)";
            // 
            // s_yPos
            // 
            this.s_yPos.Location = new System.Drawing.Point(230, 126);
            this.s_yPos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.s_yPos.Name = "s_yPos";
            this.s_yPos.Size = new System.Drawing.Size(52, 22);
            this.s_yPos.TabIndex = 14;
            // 
            // zlabel
            // 
            this.zlabel.AutoSize = true;
            this.zlabel.Location = new System.Drawing.Point(316, 129);
            this.zlabel.Name = "zlabel";
            this.zlabel.Size = new System.Drawing.Size(53, 17);
            this.zlabel.TabIndex = 17;
            this.zlabel.Text = "Z (mm)";
            // 
            // s_zPos
            // 
            this.s_zPos.Location = new System.Drawing.Point(380, 126);
            this.s_zPos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.s_zPos.Name = "s_zPos";
            this.s_zPos.Size = new System.Drawing.Size(52, 22);
            this.s_zPos.TabIndex = 16;
            // 
            // wlabel
            // 
            this.wlabel.AutoSize = true;
            this.wlabel.Location = new System.Drawing.Point(316, 170);
            this.wlabel.Name = "wlabel";
            this.wlabel.Size = new System.Drawing.Size(57, 17);
            this.wlabel.TabIndex = 23;
            this.wlabel.Text = "W (mm)";
            // 
            // s_wPos
            // 
            this.s_wPos.Location = new System.Drawing.Point(380, 167);
            this.s_wPos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.s_wPos.Name = "s_wPos";
            this.s_wPos.Size = new System.Drawing.Size(52, 22);
            this.s_wPos.TabIndex = 22;
            // 
            // vlabel
            // 
            this.vlabel.AutoSize = true;
            this.vlabel.Location = new System.Drawing.Point(166, 170);
            this.vlabel.Name = "vlabel";
            this.vlabel.Size = new System.Drawing.Size(53, 17);
            this.vlabel.TabIndex = 21;
            this.vlabel.Text = "V (mm)";
            // 
            // s_vPos
            // 
            this.s_vPos.Location = new System.Drawing.Point(229, 167);
            this.s_vPos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.s_vPos.Name = "s_vPos";
            this.s_vPos.Size = new System.Drawing.Size(52, 22);
            this.s_vPos.TabIndex = 20;
            // 
            // ulabel
            // 
            this.ulabel.AutoSize = true;
            this.ulabel.Location = new System.Drawing.Point(21, 170);
            this.ulabel.Name = "ulabel";
            this.ulabel.Size = new System.Drawing.Size(54, 17);
            this.ulabel.TabIndex = 19;
            this.ulabel.Text = "U (mm)";
            // 
            // s_uPos
            // 
            this.s_uPos.Location = new System.Drawing.Point(86, 167);
            this.s_uPos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.s_uPos.Name = "s_uPos";
            this.s_uPos.Size = new System.Drawing.Size(52, 22);
            this.s_uPos.TabIndex = 18;
            // 
            // RecText
            // 
            this.RecText.AutoSize = true;
            this.RecText.Location = new System.Drawing.Point(12, 307);
            this.RecText.Name = "RecText";
            this.RecText.Size = new System.Drawing.Size(46, 17);
            this.RecText.TabIndex = 24;
            this.RecText.Text = "label1";
            this.RecText.Click += new System.EventHandler(this.label1_Click_2);
            // 
            // sinButton
            // 
            this.sinButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sinButton.Location = new System.Drawing.Point(727, 20);
            this.sinButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sinButton.Name = "sinButton";
            this.sinButton.Size = new System.Drawing.Size(140, 23);
            this.sinButton.TabIndex = 25;
            this.sinButton.Text = "Sin Test";
            this.sinButton.UseVisualStyleBackColor = true;
            this.sinButton.Click += new System.EventHandler(this.sinButton_Click);
            // 
            // yawSinButton
            // 
            this.yawSinButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.yawSinButton.Location = new System.Drawing.Point(727, 64);
            this.yawSinButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.yawSinButton.Name = "yawSinButton";
            this.yawSinButton.Size = new System.Drawing.Size(140, 23);
            this.yawSinButton.TabIndex = 26;
            this.yawSinButton.Text = "Yaw Sin";
            this.yawSinButton.UseVisualStyleBackColor = true;
            this.yawSinButton.Click += new System.EventHandler(this.yawSinButton_Click);
            // 
            // RotationLength
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 360);
            this.Controls.Add(this.yawSinButton);
            this.Controls.Add(this.sinButton);
            this.Controls.Add(this.RecText);
            this.Controls.Add(this.wlabel);
            this.Controls.Add(this.s_wPos);
            this.Controls.Add(this.vlabel);
            this.Controls.Add(this.s_vPos);
            this.Controls.Add(this.ulabel);
            this.Controls.Add(this.s_uPos);
            this.Controls.Add(this.zlabel);
            this.Controls.Add(this.s_zPos);
            this.Controls.Add(this.Ylabel);
            this.Controls.Add(this.s_yPos);
            this.Controls.Add(this.xlabel);
            this.Controls.Add(this.s_xPos);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.Test_String);
            this.Controls.Add(this.retOrigin);
            this.Controls.Add(this.sendCom);
            this.Controls.Add(this.RotLength);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RotPulses);
            this.Controls.Add(this.PulseperRotations);
            this.Controls.Add(this.ActLength);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.LinearLength);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "RotationLength";
            this.Text = "MD BOX C#";
            this.Load += new System.EventHandler(this.RotationLength_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LinearLength;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox ActLength;
        private System.Windows.Forms.TextBox RotPulses;
        private System.Windows.Forms.Label PulseperRotations;
        private System.Windows.Forms.TextBox RotLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button sendCom;
        private System.Windows.Forms.Button retOrigin;
        private System.Windows.Forms.Label Test_String;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox s_xPos;
        private System.Windows.Forms.Label xlabel;
        private System.Windows.Forms.Label Ylabel;
        private System.Windows.Forms.TextBox s_yPos;
        private System.Windows.Forms.Label zlabel;
        private System.Windows.Forms.TextBox s_zPos;
        private System.Windows.Forms.Label wlabel;
        private System.Windows.Forms.TextBox s_wPos;
        private System.Windows.Forms.Label vlabel;
        private System.Windows.Forms.TextBox s_vPos;
        private System.Windows.Forms.Label ulabel;
        private System.Windows.Forms.TextBox s_uPos;
        private System.Windows.Forms.Label RecText;
        private System.Windows.Forms.Button sinButton;
        private System.Windows.Forms.Button yawSinButton;
    }
}

