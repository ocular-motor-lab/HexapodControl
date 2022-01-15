using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HexapodControlv2;

namespace TestHexapodControl
{

    public partial class Form1 : Form
    {
        Class1 hexapod;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {

            hexapod = new Class1();
            hexapod.Connect();

            textBoxLog.Text = textBoxLog.Text + "connected" + Environment.NewLine;
            textBoxLog.ScrollToCaret();
        }

        int length = 20;
        private void buttonSend_Click(object sender, EventArgs e)
        {
            length = 00;
            for (int i = 0; i < 100; i++)
            {
                length++;
                hexapod.SendCommand(length, length, length, length, length, length);
            }


            textBoxLog.Text = textBoxLog.Text + "Command sent" + Environment.NewLine;
            textBoxLog.ScrollToCaret();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            hexapod.ResetChair();
            textBoxLog.Text = textBoxLog.Text + "Reset complete" + Environment.NewLine;
            textBoxLog.ScrollToCaret();
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            hexapod.Disconnect();
            textBoxLog.Text = textBoxLog.Text + "Disconnected " + Environment.NewLine;
            textBoxLog.ScrollToCaret();
        }

        private void textBoxLog_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
