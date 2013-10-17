using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShortCutRemover
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var drives = from drive in DriveInfo.GetDrives() //search removable drives
                         where drive.DriveType == DriveType.Removable
                         select drive;
            cmbBx1.DataSource = drives.ToList(); //add removable drives to combobox
        }

        private void cmbBx1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lbl1_Click(object sender, EventArgs e)
        {

        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = new Process { StartInfo = startInfo };

            String drive = cmbBx1.Text;
            char[] arr = drive.ToCharArray();
            String crctDrv ="";

            for (int i = 0; i < arr.Length-1;i++ )
            {
                crctDrv = crctDrv + arr[i];
            }
            
             

            process.Start();
            process.StandardInput.WriteLine(@crctDrv);
            process.StandardInput.WriteLine(@"del *.lnk");
            process.StandardInput.WriteLine(@"attrib -s -r -h *.* /s /d /l");
            process.StandardInput.WriteLine("exit");

            process.WaitForExit();

            MessageBox.Show("Your pen drive is recovered!");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
