using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicControl
{
    public static class Values
    {

        public static int micpicked = 0;
        public static int prefvol = 85;

        public static string micname;

        public static ContextMenu tm;

        public static int error;
        //just some fucking global values
        public static void readUserSettings()
        {
            try
            {
                string cache = File.ReadAllText("config.txt");
                string[] settings = cache.Split(';');

                prefvol = Convert.ToInt32(settings[0]);
                micpicked = Convert.ToInt32(settings[1]);
            }

            catch(Exception)
            {
                micpicked = 0;
                prefvol = 85;
                MessageBox.Show("No config.txt found, setting normal values");
            }
        }
    }
}
