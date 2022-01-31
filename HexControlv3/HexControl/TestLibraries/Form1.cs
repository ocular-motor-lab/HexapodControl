using System;
using HexBuilders;
using System.Windows.Forms;

namespace TestLibraries
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            hexapod = new Hexapod();
            DisconnectButton.Enabled = false;
            SendButton.Enabled = false;
            ConnectButton.Enabled = false;
        }

        Hexapod hexapod;

        private void ConnectButton_Click(object sender, EventArgs e)
        {

            hexapod.Initialize();

            ConnectButton.Enabled = false;
            DisconnectButton.Enabled = true;
            SendButton.Enabled = true;
        }

        private async void SendButton_Click(object sender, EventArgs e)
        {
            string response;
            
            if (tabControl1.SelectedTab == tabPage1)
            {
                var x = Decimal.ToDouble(xValueNUD.Value);
                response = await hexapod.asyncMove(x, x, x, x, x, x);
            }
            else
            {
                response = await hexapod.ReadSetting("FN", 10, 1);
            }
            ResponseBox.AppendText(response + "\r\n");

        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            hexapod.Stop();
            ConnectButton.Enabled = true;
            DisconnectButton.Enabled = false;
            SendButton.Enabled = false;
        }

        private void chooseFilePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newFolder = new FolderBrowserDialog();//.ToString();

            if ((newFolder != null) && (newFolder.ShowDialog(this) == DialogResult.OK))
            {
                hexapod.DataFolder = newFolder.SelectedPath;
            }

            ConnectButton.Enabled = true;
        }

        private void sameValueCB_CheckedChanged(object sender, EventArgs e)
        {
            if (sameValueCB.Checked == true)
            {
                var x = xValueNUD.Value;
                xValueNUD.Value = yValueNUD.Value = zValueNUD.Value =
                vValueNUD.Value = uValueNUD.Value = wValueNUD.Value = x;

                //xValueNUD.Enabled = yValueNUD.Enabled = zValueNUD.Enabled =
                //vValueNUD.Enabled = uValueNUD.Enabled = wValueNUD.Enabled = false;
            }
        }
    }
}
