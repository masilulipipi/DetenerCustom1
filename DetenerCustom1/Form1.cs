using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Diagnostics;
using Microsoft.Win32;

namespace DetenerCustom1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                rkApp.DeleteValue("Prosegur - SmartSheepConsole");
            }
            catch
            {
                MessageBox.Show("No existe en el inicio", "No existe en el inicio ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ServiceController sc = new ServiceController("Prosegur - SpoolProcessor [SmartSheep_Service]");

            try
            {
                if (sc != null && sc.Status == ServiceControllerStatus.Running)
                {
                    sc.Stop();
                }
                sc.WaitForStatus(ServiceControllerStatus.Stopped);
                sc.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al detener el servicio:");
                Console.WriteLine(ex.Message);
            }

            // Process.Start("taskkill", "/IM [iexplore].exe");

            foreach (Process proceso in Process.GetProcesses())
            {
                if (proceso.ProcessName == "Client")
                {
                    proceso.Kill();
                }
                if (proceso.ProcessName == "SmartSheepConsole")
                {
                    proceso.Kill();
                }
                if (proceso.ProcessName == "SpoolProcessor_Service")
                {
                    proceso.Kill();
                }
                timer1.Start();


            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
            timer1.Stop();
        }
    }
}
