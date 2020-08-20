using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComparatorSQL
{
    public partial class FormDBSettings : Form
    {
        public FormDBSettings()
        {
            InitializeComponent();
            textBoxServer.Text = Properties.Settings.Default.dbServer;
            textBoxBDName.Text = Properties.Settings.Default.dbName;
            textBoxUser.Text = Properties.Settings.Default.dbUsername;
            textBoxPass.Text = Properties.Settings.Default.dbPass;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.dbServer = textBoxServer.Text;
            Properties.Settings.Default.dbName = textBoxBDName.Text;
            Properties.Settings.Default.dbUsername = textBoxUser.Text;
            Properties.Settings.Default.dbPass = textBoxPass.Text;

            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
