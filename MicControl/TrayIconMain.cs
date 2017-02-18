using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using NAudio.Wave;
using NAudio.Mixer;
using System.Reflection;
using MicControl.Properties;

namespace MicControl
{
    public class MicControlTray : Form
    {
        static Timer t;
        [STAThread]
        public static void Main()
        {
            Application.Run(new MicControlTray());
        }

        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;

        public MicControlTray()
        {
            // Create a simple tray menu with only one item.
            trayMenu = new ContextMenu();
            Values.tm = trayMenu;
            
            Values.tm.MenuItems.Add("Vol: " + Values.prefvol);
            Values.tm.MenuItems.Add("Mic: " + Values.micname);
            Values.tm.MenuItems.Add("Open Settings", OnSettings);
            Values.tm.MenuItems.Add("Exit", OnExit);

            Values.tm.MenuItems[0].Enabled = false;
            Values.tm.MenuItems[1].Enabled = false;
            
            //remove for further devs
            Values.tm.MenuItems[0].Visible = false;
            Values.tm.MenuItems[1].Visible = false;


            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            trayIcon = new NotifyIcon();
            trayIcon.Text = "Mic Control";


            trayIcon.Icon = Resources.mic;
           

            // Add menu to tray icon and show it.
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.
            t = new Timer();
            t.Enabled = true;
            t.Interval = 500;
            //t.Tick += T_Tick;
            t.Tick += (s, exp) => setVol();
            base.OnLoad(e);
        }

        protected void OnSettings(object sender, EventArgs e)
        {
            Form f = new formSettings();
            f.Show();
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                // Release the icon resource.
                trayIcon.Icon = null;
                trayIcon.Dispose();
            }

            base.Dispose(isDisposing);
        }

        static private void T_Tick(object sender, EventArgs e)
        {
            setVol();
            refreshTray();
        }

        public void pickMicrophone()
        {
            formSettings micpickDialog = new formSettings();
            micpickDialog.ShowDialog();
        }

        static public void setVol()
        {
            try
            {
                int waveInDeviceNumber = Values.micpicked;
                var mixerLine = new MixerLine((IntPtr)waveInDeviceNumber, 0, MixerFlags.WaveIn);

                foreach (var control in mixerLine.Controls)
                {
                    if (control.ControlType == MixerControlType.Volume)
                    {
                        UnsignedMixerControl volumeControl = control as UnsignedMixerControl;
                        volumeControl.Percent = Values.prefvol;
                        break;
                    }
                }
            }

            catch(Exception)
            {
                if(Values.error == 0)
                {
                    Values.error = 1;
                    MessageBox.Show("==========PLEASE READ========== \n" +
                                "This message only shows up ONCE per run, \n" +
                                "it means the selected mic does not work. \n" +
                                "Set the mic to 'Default' in your settings. \n" +
                                "Afterwards restart the program. \n" +
                                "===============================" );
                }
            }
        }

        static public void refreshTray()
        {
            Values.tm.MenuItems[0].Text = Convert.ToString(Values.prefvol);
            Values.tm.MenuItems[0].Text = Values.micname;
        }
    }
}