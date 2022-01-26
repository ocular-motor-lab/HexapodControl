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
        }

        Hexapod hexapod;

        private void ConnectButton_Click(object sender, EventArgs e)
        {

            hexapod = new Hexapod();
            hexapod.Init();
        }

        private async void SendButton_Click(object sender, EventArgs e)
        {
            //var x = Double.Parse(xValue.Text);
            
            //var response = await hexapod.Move(x, x, x, x, x, x);
            var response = await hexapod.ReadSetting("FN", 1, 1);
            ResponseBox.AppendText(response + "\r\n");
        }

        private void selectDataFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newFolder = new FolderBrowserDialog().ShowDialog().ToString();

            if (newFolder != null)
            {
                hexapod.DataFolder = newFolder;
            }
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            hexapod.Stop();
        }
    }
}
