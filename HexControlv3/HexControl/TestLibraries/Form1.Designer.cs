
namespace TestLibraries
{
    partial class Form1
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
            this.ConnectButton = new System.Windows.Forms.Button();
            this.SendButton = new System.Windows.Forms.Button();
            this.ResponseBox = new System.Windows.Forms.TextBox();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseFilePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.sameValueCB = new System.Windows.Forms.CheckBox();
            this.xValueNUD = new System.Windows.Forms.NumericUpDown();
            this.yValueNUD = new System.Windows.Forms.NumericUpDown();
            this.xValueLBL = new System.Windows.Forms.Label();
            this.yValueLBL = new System.Windows.Forms.Label();
            this.zValueLBL = new System.Windows.Forms.Label();
            this.zValueNUD = new System.Windows.Forms.NumericUpDown();
            this.wValueLBL = new System.Windows.Forms.Label();
            this.wValueNUD = new System.Windows.Forms.NumericUpDown();
            this.uValueLBL = new System.Windows.Forms.Label();
            this.vValueLBL = new System.Windows.Forms.Label();
            this.uValueNUD = new System.Windows.Forms.NumericUpDown();
            this.vValueNUD = new System.Windows.Forms.NumericUpDown();
            this.menuStrip2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xValueNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yValueNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zValueNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wValueNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uValueNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vValueNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // ConnectButton
            // 
            this.ConnectButton.BackColor = System.Drawing.Color.Chartreuse;
            this.ConnectButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ConnectButton.Location = new System.Drawing.Point(741, 35);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(94, 59);
            this.ConnectButton.TabIndex = 1;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = false;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // SendButton
            // 
            this.SendButton.BackColor = System.Drawing.SystemColors.Control;
            this.SendButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SendButton.Location = new System.Drawing.Point(741, 111);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(93, 59);
            this.SendButton.TabIndex = 2;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = false;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // ResponseBox
            // 
            this.ResponseBox.Location = new System.Drawing.Point(62, 290);
            this.ResponseBox.Multiline = true;
            this.ResponseBox.Name = "ResponseBox";
            this.ResponseBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResponseBox.Size = new System.Drawing.Size(773, 224);
            this.ResponseBox.TabIndex = 3;
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.BackColor = System.Drawing.Color.Red;
            this.DisconnectButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DisconnectButton.Location = new System.Drawing.Point(741, 185);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(94, 61);
            this.DisconnectButton.TabIndex = 5;
            this.DisconnectButton.Text = "DisConn";
            this.DisconnectButton.UseVisualStyleBackColor = false;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(894, 28);
            this.menuStrip2.TabIndex = 6;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chooseFilePathToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // chooseFilePathToolStripMenuItem
            // 
            this.chooseFilePathToolStripMenuItem.Name = "chooseFilePathToolStripMenuItem";
            this.chooseFilePathToolStripMenuItem.Size = new System.Drawing.Size(200, 26);
            this.chooseFilePathToolStripMenuItem.Text = "Choose File Path";
            this.chooseFilePathToolStripMenuItem.Click += new System.EventHandler(this.chooseFilePathToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(60, 31);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(642, 232);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.wValueLBL);
            this.tabPage1.Controls.Add(this.wValueNUD);
            this.tabPage1.Controls.Add(this.uValueLBL);
            this.tabPage1.Controls.Add(this.vValueLBL);
            this.tabPage1.Controls.Add(this.uValueNUD);
            this.tabPage1.Controls.Add(this.vValueNUD);
            this.tabPage1.Controls.Add(this.zValueLBL);
            this.tabPage1.Controls.Add(this.zValueNUD);
            this.tabPage1.Controls.Add(this.yValueLBL);
            this.tabPage1.Controls.Add(this.xValueLBL);
            this.tabPage1.Controls.Add(this.yValueNUD);
            this.tabPage1.Controls.Add(this.xValueNUD);
            this.tabPage1.Controls.Add(this.sameValueCB);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(634, 199);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Movement";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(634, 199);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Registries";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // sameValueCB
            // 
            this.sameValueCB.AutoSize = true;
            this.sameValueCB.Location = new System.Drawing.Point(6, 108);
            this.sameValueCB.Name = "sameValueCB";
            this.sameValueCB.Size = new System.Drawing.Size(108, 24);
            this.sameValueCB.TabIndex = 0;
            this.sameValueCB.Text = "Same Value";
            this.sameValueCB.UseVisualStyleBackColor = true;
            this.sameValueCB.CheckedChanged += new System.EventHandler(this.sameValueCB_CheckedChanged);
            // 
            // xValueNUD
            // 
            this.xValueNUD.DecimalPlaces = 2;
            this.xValueNUD.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.xValueNUD.Location = new System.Drawing.Point(34, 6);
            this.xValueNUD.Maximum = new decimal(new int[] {
            480,
            0,
            0,
            0});
            this.xValueNUD.Name = "xValueNUD";
            this.xValueNUD.Size = new System.Drawing.Size(67, 27);
            this.xValueNUD.TabIndex = 3;
            // 
            // yValueNUD
            // 
            this.yValueNUD.DecimalPlaces = 2;
            this.yValueNUD.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.yValueNUD.Location = new System.Drawing.Point(34, 39);
            this.yValueNUD.Maximum = new decimal(new int[] {
            480,
            0,
            0,
            0});
            this.yValueNUD.Name = "yValueNUD";
            this.yValueNUD.Size = new System.Drawing.Size(67, 27);
            this.yValueNUD.TabIndex = 5;
            // 
            // xValueLBL
            // 
            this.xValueLBL.AutoSize = true;
            this.xValueLBL.Location = new System.Drawing.Point(6, 8);
            this.xValueLBL.Name = "xValueLBL";
            this.xValueLBL.Size = new System.Drawing.Size(21, 20);
            this.xValueLBL.TabIndex = 6;
            this.xValueLBL.Text = "X:";
            // 
            // yValueLBL
            // 
            this.yValueLBL.AutoSize = true;
            this.yValueLBL.Location = new System.Drawing.Point(6, 41);
            this.yValueLBL.Name = "yValueLBL";
            this.yValueLBL.Size = new System.Drawing.Size(20, 20);
            this.yValueLBL.TabIndex = 7;
            this.yValueLBL.Text = "Y:";
            // 
            // zValueLBL
            // 
            this.zValueLBL.AutoSize = true;
            this.zValueLBL.Location = new System.Drawing.Point(6, 73);
            this.zValueLBL.Name = "zValueLBL";
            this.zValueLBL.Size = new System.Drawing.Size(21, 20);
            this.zValueLBL.TabIndex = 9;
            this.zValueLBL.Text = "Z:";
            // 
            // zValueNUD
            // 
            this.zValueNUD.DecimalPlaces = 2;
            this.zValueNUD.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.zValueNUD.Location = new System.Drawing.Point(34, 71);
            this.zValueNUD.Maximum = new decimal(new int[] {
            480,
            0,
            0,
            0});
            this.zValueNUD.Name = "zValueNUD";
            this.zValueNUD.Size = new System.Drawing.Size(67, 27);
            this.zValueNUD.TabIndex = 8;
            // 
            // wValueLBL
            // 
            this.wValueLBL.AutoSize = true;
            this.wValueLBL.Location = new System.Drawing.Point(108, 73);
            this.wValueLBL.Name = "wValueLBL";
            this.wValueLBL.Size = new System.Drawing.Size(26, 20);
            this.wValueLBL.TabIndex = 15;
            this.wValueLBL.Text = "W:";
            // 
            // wValueNUD
            // 
            this.wValueNUD.DecimalPlaces = 2;
            this.wValueNUD.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.wValueNUD.Location = new System.Drawing.Point(136, 71);
            this.wValueNUD.Maximum = new decimal(new int[] {
            480,
            0,
            0,
            0});
            this.wValueNUD.Name = "wValueNUD";
            this.wValueNUD.Size = new System.Drawing.Size(67, 27);
            this.wValueNUD.TabIndex = 14;
            // 
            // uValueLBL
            // 
            this.uValueLBL.AutoSize = true;
            this.uValueLBL.Location = new System.Drawing.Point(108, 41);
            this.uValueLBL.Name = "uValueLBL";
            this.uValueLBL.Size = new System.Drawing.Size(22, 20);
            this.uValueLBL.TabIndex = 13;
            this.uValueLBL.Text = "U:";
            // 
            // vValueLBL
            // 
            this.vValueLBL.AutoSize = true;
            this.vValueLBL.Location = new System.Drawing.Point(108, 8);
            this.vValueLBL.Name = "vValueLBL";
            this.vValueLBL.Size = new System.Drawing.Size(21, 20);
            this.vValueLBL.TabIndex = 12;
            this.vValueLBL.Text = "V:";
            // 
            // uValueNUD
            // 
            this.uValueNUD.DecimalPlaces = 2;
            this.uValueNUD.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.uValueNUD.Location = new System.Drawing.Point(136, 39);
            this.uValueNUD.Maximum = new decimal(new int[] {
            480,
            0,
            0,
            0});
            this.uValueNUD.Name = "uValueNUD";
            this.uValueNUD.Size = new System.Drawing.Size(67, 27);
            this.uValueNUD.TabIndex = 11;
            // 
            // vValueNUD
            // 
            this.vValueNUD.DecimalPlaces = 2;
            this.vValueNUD.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.vValueNUD.Location = new System.Drawing.Point(136, 6);
            this.vValueNUD.Maximum = new decimal(new int[] {
            480,
            0,
            0,
            0});
            this.vValueNUD.Name = "vValueNUD";
            this.vValueNUD.Size = new System.Drawing.Size(67, 27);
            this.vValueNUD.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 561);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.DisconnectButton);
            this.Controls.Add(this.ResponseBox);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.menuStrip2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xValueNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yValueNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zValueNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wValueNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uValueNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vValueNUD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.TextBox ResponseBox;
        private System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseFilePathToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label wValueLBL;
        private System.Windows.Forms.NumericUpDown wValueNUD;
        private System.Windows.Forms.Label uValueLBL;
        private System.Windows.Forms.Label vValueLBL;
        private System.Windows.Forms.NumericUpDown uValueNUD;
        private System.Windows.Forms.NumericUpDown vValueNUD;
        private System.Windows.Forms.Label zValueLBL;
        private System.Windows.Forms.NumericUpDown zValueNUD;
        private System.Windows.Forms.Label yValueLBL;
        private System.Windows.Forms.Label xValueLBL;
        private System.Windows.Forms.NumericUpDown yValueNUD;
        private System.Windows.Forms.NumericUpDown xValueNUD;
        private System.Windows.Forms.CheckBox sameValueCB;
    }
}

