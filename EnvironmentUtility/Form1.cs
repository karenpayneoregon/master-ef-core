using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnvironmentUtility
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Shown += OnShown;
        }

        private void OnShown(object sender, EventArgs e)
        {
            EnvironmentComboBox.DataSource = new List<string>() { "DEVELOPMENT", "TEST", "PRODUCTION" };
        }

        private async void SetButton_Click(object sender, EventArgs e)
        {
            var start = new ProcessStartInfo
            {
                FileName = "setx",
                RedirectStandardOutput = true,
                Arguments = "OED_ENVIRONMENT \"DEVLOPMENT\"",
                CreateNoWindow = true
            };

            using var process = Process.Start(start);
            using var reader = process.StandardOutput;

            process.EnableRaisingEvents = true;
            await process.WaitForExitAsync();
            Debug.WriteLine("DONE");
        }
    }
}
