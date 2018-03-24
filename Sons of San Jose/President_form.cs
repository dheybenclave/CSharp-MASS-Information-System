using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sons_of_San_Jose
{
    public partial class President_form : Form
    {
        public President_form()
        {
            InitializeComponent();

            // this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            btnmasterlist.BackgroundImage = imageList1.Images[0];
            btntreasurer.BackgroundImage = imageList1.Images[1];
            btnauditor.BackgroundImage = imageList1.Images[2];
            btnsecreatry.BackgroundImage = imageList1.Images[3];
            btnsettings.BackgroundImage = imageList1.Images[4];
        }
        int TMove;
        int MX;
        int MY;
        private void President_form_Load(object sender, EventArgs e)
        {
        }

        private void minimize_Click(object sender, EventArgs e)
        { this.WindowState = FormWindowState.Minimized; }

        private void close_Click(object sender, EventArgs e)
        { ExitForm ef = new ExitForm(); ef.ShowDialog(); }

        private void btnsecretary_MouseEnter(object sender, EventArgs e)
        {
            lblstatus.Text = "Secretary";
            btnsecreatry.BorderColor = Color.White;
            btnsecreatry.BackColor = Color.White;
        }

        private void btnsecretary_MouseLeave(object sender, EventArgs e)
        {
            lblstatus.Text = "";
            btnsecreatry.BorderColor = Color.Green;
            btnsecreatry.BackColor = Color.SeaGreen;
        }

        private void btntreasurer_MouseEnter(object sender, EventArgs e)
        {
            lblstatus.Text = "Treasurer";
            btntreasurer.BorderColor = Color.White;
            btntreasurer.BackColor = Color.White;
        }
        private void btntreasurer_MouseLeave(object sender, EventArgs e)
        {
            lblstatus.Text = "";
            btntreasurer.BorderColor = Color.Green;
            btntreasurer.BackColor = Color.SeaGreen;
        }

        private void btnauditor_MouseEnter(object sender, EventArgs e)
        {
            lblstatus.Text = "Auditor";
            btnauditor.BorderColor = Color.White;
            btnauditor.BackColor = Color.White;
        }

        private void btnauditor_MouseLeave(object sender, EventArgs e)
        {
            lblstatus.Text = "";
            btnauditor.BorderColor = Color.Green;
            btnauditor.BackColor = Color.SeaGreen;
        }

        private void btnmasterlist_MouseEnter(object sender, EventArgs e)
        {
            lblstatus.Text = "Master List";
            btnmasterlist.BorderColor = Color.White;
            btnmasterlist.BackColor = Color.White;
        }

        private void btnmasterlist_MouseLeave(object sender, EventArgs e)
        {
            lblstatus.Text = "";
            btnmasterlist.BorderColor = Color.Green;
            btnmasterlist.BackColor = Color.SeaGreen;
        }

        private void btnsettings_MouseEnter(object sender, EventArgs e)
        {
            lblstatus.Text = "Settings";
            btnsettings.BorderColor = Color.White;
            btnsettings.BackColor = Color.White;
        }

        private void btnsettings_MouseLeave(object sender, EventArgs e)
        {
            lblstatus.Text = "";
            btnsettings.BorderColor = Color.Green;
            btnsettings.BackColor = Color.SeaGreen;
        }

        private void btnmasterlist_Click(object sender, EventArgs e)
        {
            MasterList ml = new MasterList();
            ml.Location = this.Location;
            ml.ShowDialog();
        }
        private void btnsecreatry_Click(object sender, EventArgs e)
        {
            Secretary s = new Secretary();
            s.Location = this.Location;
            s.ifpresident = 1;
            s.ShowDialog();
        }

        private void btnsettings_Click(object sender, EventArgs e)
        {
            Settings set = new Settings();
            set.Location = this.Location;
            set.ShowDialog();
        }

        private void btntreasurer_Click(object sender, EventArgs e)
        {
            Printing_Masterlist pm = new Printing_Masterlist();
            pm.ShowInTaskbar = false;
            pm.ShowIcon = false;
            pm.ifpresident = 1;
            pm.tabcontrolprint.SelectedTab = pm.tbtreasurer;
            pm.ShowDialog();
        }

        private void btnauditor_Click(object sender, EventArgs e)
        {
            Committee cm = new Committee();
            cm.ifpresident = 1;
            cm.ShowInTaskbar = false;
            cm.ShowIcon = false;
            cm.Location = this.Location;
            cm.ShowDialog();
            
        }

    }

}
