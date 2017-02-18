using System;
using System.Windows.Forms;
using NAudio.Wave;
using MaterialSkin.Controls;
using MaterialSkin;
using System.Diagnostics;

namespace IronShell
{
    public partial class formSettings : MaterialForm
    {
        int waveInDevices = WaveIn.DeviceCount;

        public formSettings()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Amber800, Primary.Grey700, Primary.Red900, Accent.LightBlue200, TextShade.WHITE);

            Values.readUserSettings();
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            for (int waveInDevice = 0; waveInDevice < waveInDevices; waveInDevice++)
            {
                WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(waveInDevice);
                richTextBox1.AppendText("Device " + waveInDevice + " " + deviceInfo.ProductName + ", " + deviceInfo.Channels + " channels \n");
                Values.micname = deviceInfo.ProductName;
            }

            lblCreator.Text = "by /id/beggarboy";            

            txtMic.Text = Convert.ToString(Values.micpicked);
            txtVol.Text = Convert.ToString(Values.prefvol);
        }

        private void cmdConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                int number = Convert.ToInt32(txtMic.Text);
                int vol = Convert.ToInt32(txtVol.Text);

                if (vol >= 100) vol = 100;
                else if (vol < 0) vol = 0;

                if (number <= waveInDevices && !(number < 0))
                {
                    Values.micpicked = number;
                    Values.prefvol = vol;
                    this.Close();
                }

                else MessageBox.Show("No microphone assigned to that number!") ;
            }
            
            catch (Exception exp)
            {
                MessageBox.Show("Something went wrong! Exception is: \n " + exp);
            }

        }

        private void lblCreator_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://steamcommunity.com/id/beggarboy");
        }
    }
}
