using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Runtime.InteropServices;
using Sons_of_San_Jose.Properties;

namespace Sons_of_San_Jose
{
    public partial class Secretary : Form
    {
        public int ifpresident;
        Db_Utilities db = new Db_Utilities();
        MySqlDataAdapter adapt;
        DataTable table = new DataTable();
        MySqlCommand cmd;
        Point p = new Point();
        List<string> HasDate = new List<string>();
        List<int> present = new List<int>();
        List<string> chkdidperson = new List<string>();
        Dictionary<string, bool> idIsChecked = new Dictionary<string, bool>();
        string filter, donefill, pd_id_lst, ad_id,filename = "";
        public bool istrue, counttrue = false;

        
        public Secretary()
        {
            InitializeComponent();
          
            p.X = (Screen.PrimaryScreen.Bounds.Width / 2) - (this.Size.Width / 2);
            p.Y = (Screen.PrimaryScreen.Bounds.Height / 2) - (this.Size.Height / 2);
            this.Location = p;
            dtaddattendance.Value = DateTime.Now;
            AllFullname();
            ComboBoxDate();
            LoadImages();
            LoadPresent();
            timer2.Enabled = true;
            timer2.Start();
            btnsecretary.BackgroundImage = imageList1.Images[11];
        }
        public void LoadImages()
        {
            //btnsecretary.BackgroundImage = imageList1.Images[11];
            btnmasterlistview.BackgroundImage = imageList1.Images[9];
            btnaddtoattendace.BackgroundImage = imageList1.Images[8];
            btnprint.BackgroundImage = imageList1.Images[12];
            btnsettings.BackgroundImage = imageList1.Images[14];
        }
        private void close_Click(object sender, EventArgs e)
        {
            if (ifpresident == 1) this.Close(); 
            else { ExitForm ex = new ExitForm(); ex.ShowDialog();}
        }
        private void minimize_Click(object sender, EventArgs e)
        { this.WindowState = FormWindowState.Minimized; }


        public void ComboBoxDate()
        {
            cmbdate.Items.Clear();
            DataTable dt = new DataTable();
            dt.Clear();
            adapt = new MySqlDataAdapter("SELECT distinct(ad_date) FROM attendance ORDER BY ad_date DESC;", db.OpenConnection());
            adapt.Fill(dt);
            db.CloseConnection();

            foreach (DataRow dr in dt.Rows)
            {
                HasDate.Add(Convert.ToDateTime(dr[0].ToString()).ToString("yyyy-MM-dd"));
                cmbdate.Items.Add(Convert.ToDateTime(dr[0].ToString()).ToString("yyyy-MM-dd"));
            }
            try
            {
                cmbdate.Text = cmbdate.Items[0].ToString();
                rdpresent.Select();
            }
            catch
            {
                // MessageBox.Show("There is no previous data for attendance!");
            }
        }
        public void LoadPresent()
        {
            present.Clear();
            rdpresent.Checked = true;
            lstmasterlistright.Items.Clear();
            string q = "SELECT ad.ad_id,ad.pd_id, p.pd_fullname, m.md_status FROM dbms_mass.attendance ad inner join p_details p ON " +
                            "p.pd_id = ad.pd_id inner join  m_details m ON  p.pd_id = m.md_id WHERE ad.ad_date = '" + cmbdate.Text + "' AND m.md_status ='Active' AND m.md_retrieve ='NO' ORDER BY pd_lastname ASC;";
            adapt = new MySqlDataAdapter(q, db.OpenConnection());
            DataTable dtt = new DataTable();
            dtt.Clear();
            adapt.Fill(dtt);
            db.CloseConnection();
            foreach (DataRow r in dtt.Rows)
            {
                present.Add(Convert.ToInt32((r[1])));
                ListViewItem itm = new ListViewItem(r[0].ToString());
                itm.SubItems.Add(r[1].ToString());
                itm.SubItems.Add(r[2].ToString().Replace("_", " "));
                lstmasterlistright.Items.Add(itm);
            }
        }

        public void LoadAbsent()
        {
            rdabsent.Checked = true;
            lstmasterlistright.Items.Clear();
            DataTable tab = new DataTable();
            tab.Clear();

            for (int i = 0; i < present.Count; i++)
            {
                filter += "'" + present[i] + "',";
                donefill = "(" + filter + ")";

            }
         
            donefill = donefill.Replace(",)", ")");
            string q = "SELECT p.pd_id, p.pd_fullname, m.md_status FROM dbms_mass.p_details p inner join  m_details m ON " +
                        "  p.pd_id = m.md_id WHERE pd_id NOT IN" + donefill + " AND m.md_status ='Active' AND m.md_retrieve ='NO' ORDER BY pd_lastname ASC;";
            adapt = new MySqlDataAdapter(q, db.OpenConnection());
            adapt.Fill(tab);
            db.CloseConnection();
            foreach (DataRow r in tab.Rows)
            {
                ListViewItem itm = new ListViewItem("");
                itm.SubItems.Add(r[0].ToString());
                itm.SubItems.Add(r[1].ToString().Replace("_", " "));
                lstmasterlistright.Items.Add(itm);
            }
            present.Clear();
            filter = "";
        }


        public void AllFullname()
        {
            table.Clear();
            idIsChecked.Clear();
            adapt = new MySqlDataAdapter("SELECT p.pd_id, p.pd_fullname, m.md_status FROM p_details p inner join" +
                                            " m_details m ON  p.pd_id = m.md_id WHERE m.md_status ='Active'  AND m.md_retrieve ='NO' ORDER BY pd_lastname ASC;", db.OpenConnection());
            db.CloseConnection();
            adapt.Fill(table);

            foreach (DataRow rd in table.Rows)
            {
                ListViewItem itm = new ListViewItem(rd["pd_fullname"].ToString().Replace("_", " "));
                itm.SubItems.Add(rd["pd_id"].ToString());
                lstattendancemasterlist.Items.Add(itm);
                idIsChecked.Add(rd["pd_id"].ToString(), false);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            int seconds = Convert.ToInt32(lblnum.Text);
            lblnum.Text = Convert.ToString(seconds - 1);
            if (lblnum.Text == "0")
            {
                buttom.BackColor = Color.Honeydew;
                timer1.Stop();
                lblwarning.Visible = false;
                icowarning.Visible = false;
                lblnum.Text = "5";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkselectall_CheckedChanged(object sender, EventArgs e)
        {

            if (chkselectall.Checked == true)
            {
                for (int i = 0; i < lstattendancemasterlist.Items.Count; i++)
                { lstattendancemasterlist.Items[i].Checked = true; }
            }
            else
            {
                for (int i = 0; i < lstattendancemasterlist.Items.Count; i++)
                { lstattendancemasterlist.Items[i].Checked = false; }
            }
        }

        private void btnsecretary_Click(object sender, EventArgs e)
        {

            if (!HasDate.Contains(dtaddattendance.Value.ToString("yyyy-MM-dd")))
            {
                foreach (string s in idIsChecked.Keys)
                {
                    try
                    {
                        if (idIsChecked[s])
                            cmd = new MySqlCommand("SaveAttendance", db.OpenConnection());
                        cmd.CommandType = CommandType.StoredProcedure;
                        try
                        {
                            cmd.Parameters.Add(new MySqlParameter("?pd_id", s));
                            cmd.Parameters.Add(new MySqlParameter("?ad_date",dtaddattendance.Value.ToString("yyyy-MM-dd") ));
                            cmd.ExecuteNonQuery();
                            db.CloseConnection();
                            ComboBoxDate();
                            LoadPresent();
                            timer1.Enabled = true;
                            timer1.Start();
                            icowarning.Visible = true;
                            lblwarning.Visible = true;
                            icowarning.BackgroundImage = imageList1.Images[5];
                            lblwarning.Text = " Add Attendance Success! .";
                            buttom.BackColor = icowarning.BackColor = lblwarning.BackColor = Color.Lime;
                        }
                        catch (Exception ex) {  }
                    }
                    catch (Exception ex) { }  
                }
            }
            else
            {
                timer1.Enabled = true;
                timer1.Start();
                icowarning.Visible = true;
                lblwarning.Visible = true;
                icowarning.BackgroundImage = imageList1.Images[4];
                lblwarning.Text = Convert.ToDateTime(dtaddattendance.Text).ToString("MMMM dd, yyyy") +" is Already Exsit! .";
                buttom.BackColor = icowarning.BackColor = lblwarning.BackColor = Color.Tomato;
            }
            chkselectall.Checked = false;
            for (int i = 0; i < lstattendancemasterlist.Items.Count; i++)
            { lstattendancemasterlist.Items[i].Checked = false; }
        }


        private void timer2_Tick(object sender, EventArgs e)
        {
            lblcurrenttime.Text = DateTime.Now.ToLongTimeString();
            lblcurrentdate.Text = DateTime.Now.ToLongDateString();

        }

        private void Secretary_Load(object sender, EventArgs e)
        {

            if (ifpresident == 1) { btnmasterlistview.Visible = false; this.ShowInTaskbar = false; btnsettings.Visible = false;  }
            else { btnmasterlistview.Visible = true; this.ShowInTaskbar = true; btnsettings.Visible = true; }
        }

        private void cmbdate_SelectedValueChanged(object sender, EventArgs e)
        {
            lbldateselected.Text = cmbdate.Text;
        }

        private void rdpresent_CheckedChanged(object sender, EventArgs e)
        {
            if (rdpresent.Checked == true) { lblstatusattendance.Text = "Present"; lbladdto.Text = "MOVE TO ABSENT"; }
            else { lblstatusattendance.Text = "Absent"; lbladdto.Text = "MOVE TO PRESENT"; }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {

        }

        private void btndelete_Click(object sender, EventArgs e)
        {

        }

        private void cmbdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPresent();
            if (rdpresent.Checked == true) { LoadPresent(); }
            if (rdabsent.Checked == true) { LoadAbsent(); }
        }
        private void btnsecretary_MouseEnter(object sender, EventArgs e)
        {
        }

        private void btnsecretary_MouseLeave(object sender, EventArgs e)
        {
        }

        private void btnmasterlistview_Click(object sender, EventArgs e)
        {
            MasterList ml = new MasterList();
            ml.ShowDialog();
        }

        private void lstmasterlistright_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstmasterlistright_Click(object sender, EventArgs e)
        {
            try
            {
                pd_id_lst = lstmasterlistright.FocusedItem.SubItems[1].Text;
                ad_id = lstmasterlistright.FocusedItem.SubItems[0].Text;
                DataTable t = new DataTable();
                t.Clear();
                string query = "SELECT p.pd_id, p.pd_fullname, p.pd_photo, m.md_position, m.md_status FROM p_details p INNER JOIN m_details m ON " +
                                "m.md_id = p.pd_id WHERE p.pd_id ='" + pd_id_lst + "' AND m.md_status ='Active' AND m.md_retrieve ='NO' ;";

                adapt = new MySqlDataAdapter(query, db.OpenConnection());
                adapt.Fill(t);
                db.CloseConnection();

                foreach (DataRow r in t.Rows)
                {
                    lblfullname.Text = r[1].ToString().Replace("_", " ");
                    pbphoto.ImageLocation = Application.StartupPath + "\\pictures\\" + r[2].ToString();
                    lblposition.Text = r[3].ToString();
                }
            }
            catch { }
        }

        private void rdabsent_CheckedChanged(object sender, EventArgs e)
        {
            if (rdabsent.Checked == true) { lblstatusattendance.Text = "Absent"; lbladdto.Text = "MOVE TO PRESENT"; }
            else { lblstatusattendance.Text = "Present"; lbladdto.Text = "MOVE TO ABSENT"; }
        }

        private void rdabsent_Click(object sender, EventArgs e)
        { LoadAbsent(); }

        private void rdpresent_Click(object sender, EventArgs e)
        { LoadPresent(); }

        private void lblsearch_Click(object sender, EventArgs e)
        { lblsearch.Visible = false; txtsearch.Focus(); }

        private void txtsearch_Enter(object sender, EventArgs e)
        { lblsearch.Visible = false; }

        private void txtsearch2_Enter(object sender, EventArgs e)
        { lblsearch2.Visible = false; }

        List<int> allmembers = new List<int>();

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            allmembers.Clear();
            string q = "SELECT p.pd_id, p.pd_fullname FROM p_details p inner join  m_details m ON p.pd_id = m.md_id "+
                    " WHERE pd_fullname LIKE'%" + txtsearch.Text + "%' AND m.md_status ='Active' AND m.md_retrieve ='NO' ORDER BY pd_lastname ASC;";
            adapt = new MySqlDataAdapter(q, db.OpenConnection());
            table = new DataTable();
            table.Clear();
            lstattendancemasterlist.Items.Clear();
            adapt.Fill(table);
            db.CloseConnection();
            List<string> chkdid = new List<string>();
            chkdid.AddRange(chkdidperson);
            foreach (DataRow rd in table.Rows)
            {
                ListViewItem itm = new ListViewItem(rd["pd_fullname"].ToString().Replace("_", " "));
                itm.SubItems.Add(rd["pd_id"].ToString());

                allmembers.Add(Convert.ToInt32(rd["pd_id"].ToString()));

                itm.Checked = idIsChecked[rd["pd_id"].ToString()];
                lstattendancemasterlist.Items.Add(itm);
            }

        }

        private void lstattendancemasterlist_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            idIsChecked[e.Item.SubItems[1].Text] = e.Item.Checked;
        }

        private void lblsearch2_Click(object sender, EventArgs e)
        { lblsearch2.Visible = false; txtsearch2.Focus(); }

        private void txtsearch2_TextChanged(object sender, EventArgs e)
        {
            if (txtsearch2.Text == "")
            {
                lblsearch2.Visible = true;
                lstmasterlistright.Items.Clear();
                if (rdpresent.Checked == true) { LoadPresent(); } else { LoadAbsent(); }
            }
            else
            {
                lblsearch2.Visible = false;
                string selectedquery = "";
                if (rdpresent.Checked == true)
                {
                    string forpresent = "SELECT ad.ad_id,ad.pd_id, p.pd_fullname, m.md_status FROM dbms_mass.attendance ad INNER JOIN p_details p ON " +
                               "p.pd_id = ad.pd_id INNER JOIN m_details m ON p.pd_id = m.md_id WHERE ad.ad_date = '" + cmbdate.Text +
                                "' AND p.pd_fullname LIKE'%" + txtsearch2.Text + "%' AND m.md_status ='Active' AND m.md_retrieve ='NO'  ORDER BY pd_lastname ASC;";
                    selectedquery = forpresent;
                }
                else
                {
                    string forabsent = "SELECT p.pd_id, p.pd_fullname, m.md_status FROM dbms_mass.p_details p inner join m_details m "+
                                       "ON p.pd_id = m.md_id WHERE ( pd_id NOT IN " + donefill +
                                       " AND pd_fullname LIKE'%" + txtsearch2.Text + "%' ) AND m.md_status ='Active' AND m.md_retrieve ='NO' ORDER BY pd_lastname ASC;";
                    selectedquery = forabsent;
                }
                adapt = new MySqlDataAdapter(selectedquery, db.OpenConnection());
                table = new DataTable();
                table.Clear();
                lstmasterlistright.Items.Clear();
                adapt.Fill(table);
                db.CloseConnection();
                foreach (DataRow rd in table.Rows)
                {
                    ListViewItem itm;
                    if (rdpresent.Checked == true)
                    {
                        itm = new ListViewItem(rd[0].ToString());
                        itm.SubItems.Add(rd[1].ToString());
                        itm.SubItems.Add(rd[2].ToString().Replace("_", " "));
                    }
                    else
                    {
                        itm = new ListViewItem("0");
                        itm.SubItems.Add(rd[0].ToString());
                        itm.SubItems.Add(rd[1].ToString().Replace("_", " "));
                    }
                    lstmasterlistright.Items.Add(itm);
                }
            }
        }

        private void rdpresentview_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdabsentview_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            Printing_Masterlist pm = new Printing_Masterlist();
            pm.tabcontrolprint.SelectedTab = pm.tbattendance;
            pm.pnlhiding2.Size = pm.tabcontrolprint.Size;
            if (ifpresident == 1) { pm.ifpresident = 1; }
            else { pm.ifpresident = 0; }
            pm.ShowDialog();
           
        }

        private void btnaddto_Click(object sender, EventArgs e)
        {
            if (lbladdto.Text == "MOVE TO ABSENT")
            {
                string q = "DELETE FROM `dbms_mass`.`attendance` WHERE `ad_id`='"+ad_id+"';";
                cmd = new MySqlCommand(q, db.OpenConnection());
                cmd.ExecuteNonQuery();
                LoadPresent();
            }
            else if (lbladdto.Text == "MOVE TO PRESENT")
            {
                if (pd_id_lst != null)
                {
                    cmd = new MySqlCommand("SaveAttendance", db.OpenConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        cmd.Parameters.Add(new MySqlParameter("?pd_id", pd_id_lst));
                        cmd.Parameters.Add(new MySqlParameter("?ad_date", lbldateselected.Text));
                        cmd.ExecuteNonQuery();
                        db.CloseConnection();
                        LoadPresent();
                    }
                    catch { }

                }
                LoadAbsent();
            }
            pd_id_lst = "";
            pbphoto.ImageLocation = Application.StartupPath + "//Images//insert.png";
            lblfullname.Text = "FULL NAME";
            lblposition.Text = "position";
        }

        private void btnsettings_Click(object sender, EventArgs e)
        {
            Settings._y = this.Location.Y;
            Settings._x = this.Location.X;
            Settings ss = new Settings();
            ss.ShowDialog();
        }

        private void btnsettings_MouseEnter(object sender, EventArgs e)
        {
          // lblstatus.Text = "Settings";
            btnsettings.BorderColor = Color.White;
            btnsettings.BackColor = Color.White;
        }

        private void btnsettings_MouseLeave(object sender, EventArgs e)
        {
            lblstatus.Text = "";
            btnsettings.BorderColor = Color.Green;
            btnsettings.BackColor = Color.SeaGreen;
        }

        private void lbldateselected_Click(object sender, EventArgs e)
        {

        }

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && (e.KeyChar != ' '))
            {
                e.Handled = true;

            }
        }
    }
}
