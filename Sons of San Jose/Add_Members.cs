using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace Sons_of_San_Jose
{
    public partial class Add_Members : Form
    {
        Db_Utilities db = new Db_Utilities();
        MySqlConnection connection;
        MySqlCommand cmd;
        DataTable table = new DataTable();
        public static string pid;
        int now = DateTime.Now.Year;
        int counterdate = 1980;
        bool ifdatenow = false;
        string status = "";
        public Add_Members()
        {
            InitializeComponent();
            // this.Location= new Point(President_form.ActiveForm.DesktopLocation.X, President_form.ActiveForm.DesktopLocation.Y); 
            btnmasterlist.BackgroundImage = imageList1.Images[3];
            btnsave.BackgroundImage = imageList1.Images[6];
            btnaddimage.BackgroundImage = imageList1.Images[7];


                timer2.Enabled = true;
                timer2.Start();

                pnlfill.AutoScrollPosition = new Point(0,0);
        }

        public static void reset(Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                c.GetType().Name.Equals("TextBox");
                c.Text = "";
                c.GetType().Name.Equals("PictureBox");
            }
        }
        string allhonors;
        string allministries;
        string allawards;
        string allviolations;
        string allsacraments;

        string pathimage;
        string donepath;
        string donebday;
        string donedateofinvest;
        string donedateofpromote;
        int cons_id = 0;
   


        private void Add_Members_Load(object sender, EventArgs e)
        {

            if (pid != null)
            {
                UpdateDetails();
                btnsave.BackColor = Color.MediumVioletRed;
                btnsave.BorderColor = Color.DeepPink;
                btnsave.BackgroundImage = imageList1.Images[8];
            }
            else
            {
                btnsave.BackColor = Color.DodgerBlue;
                btnsave.BorderColor = Color.RoyalBlue;
                btnsave.BackgroundImage =imageList1.Images[6];

                picturebox.ImageLocation = null;
                txtfname.Text = null;
                txtlname.Text = null;
                txtmname.Text = null;
                txtaddress.Text = null;
                txtage.Text = null;
                txtbatch.Text = null;
                txtbirthplace.Text = null;
                txtclassof.Text = null;
                txtcontactnumber.Text = null;
                txtcontactnumberfather.Text = null;
                txtcontactnumbermother.Text = null;
                txtemail.Text = null;
                txtfathername.Text = null;
                txtfatheroccupation.Text = null;
                txtmothername.Text = null;
                txtmotheroccupation.Text = null;
                txtprimary.Text = null;
                txtprimaryyear.Text = null;
                txtsecondary.Text = null;
                txtsecondaryyear.Text = null;
                txtteriaryyear.Text = null;
                txttertiary.Text = null;
                txttertiarycourse.Text = null;
                txtvocational.Text = null;
                txtvocationalcourse.Text = null;
                txtvocationalyear.Text = null;
                allawards = null;
                allhonors = null;
                allministries = null;
                allsacraments = null;
                allviolations = null;
                lstawards.Items.Clear();
                lsthonors.Items.Clear();
                lstministries.Items.Clear();
                lstsacraments.Items.Clear();
                lstviolations.Items.Clear();
            }

            btndel.Enabled = false;
            btndel1.Enabled = false;
            btndel2.Enabled = false;
            btndel3.Enabled = false;
            btndel4.Enabled = false;
        }

        private void minimize_Click(object sender, EventArgs e)
        { this.WindowState = FormWindowState.Minimized; }
        private void close_Click(object sender, EventArgs e)
        { this.Close(); }

        private void Header_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Header_MouseUp(object sender, MouseEventArgs e)
        { }

        private void Header_MouseDown(object sender, MouseEventArgs e)
        { }

        private void txthonors_Enter(object sender, EventArgs e)
        {
            lblhonors.Visible = false;
        }

        public void BrowseImage()
        {
            cons_id = 0;

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filter = "JPG files|*.jpg|PNG files|*.png";
            int count = 1;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pathimage = dlg.FileName;
                string name = dlg.SafeFileName.ToString();//dhe.png
                donepath = cons_id.ToString() + "." + name.Split('.')[1];

                picturebox.ImageLocation = pathimage;
            }
            else { }

        }

        public string formaltext(string s)
        {
            string retval = "";
            string[] fname = s.Split(' ');

            for (int i = 0; i < fname.Length; i++)
            {
                if (fname[i] != "")
                {

                    string fletter = fname[i].Substring(0, 1).ToUpper();
                    string rletter = fname[i].Substring(1).ToLower();
                    retval += fletter + rletter + " ";
                }
            }
            retval.Trim();
            return retval;

        }

        private void picturebox_Click(object sender, EventArgs e)
        {
            picturebox.SendToBack();
            BrowseImage();
        }

        private void btnaddimage_Click(object sender, EventArgs e)
        {
            btnaddimage.FlatAppearance.BorderColor = Color.SeaGreen;
            BrowseImage();
        }

        private void txthonors_TextChanged(object sender, EventArgs e)
        {
            if (txthonors.Text == "")
            {
                lblhonors.Visible = true;
                btnadd.Enabled = false;
            }
            else { lblhonors.Visible = false; btnadd.Enabled = true; }
        }

        private void lblhonors_Click(object sender, EventArgs e)
        {
            lblhonors.Visible = false;
            txthonors.Focus();
        }

        private void btnadddel_Click(object sender, EventArgs e)
        {
            if (txthonors.Text.ToString().Trim().Length != 0)
            {
                int count = 0;
                ListViewItem itm = new ListViewItem(count++.ToString());
                itm.SubItems.Add(txthonors.Text);
                lsthonors.Items.Add(itm);
                allhonors += txthonors.Text + "|";
                txthonors.Text = "";
            }
        }

        string focuslist;
        string focuslist1;
        string focuslist2;
        string focuslist3;
        string focuslist4;
        string updatepath;

        private void lsthonors_Click(object sender, EventArgs e)
        {
            btndel.Enabled = true;
            focuslist = lsthonors.FocusedItem.SubItems[1].Text;
            txthonors.Text = focuslist;
            grpadding.Text = "Deleting";
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            lsthonors.Items.Remove(lsthonors.FocusedItem);
            grpadding.Text = "Adding";
            txthonors.Text = null;
            btndel.Enabled = false;
            if (allhonors.Contains(txthonors.Text))
            {
                string dd = allhonors.Replace(focuslist + "|", null);
                allhonors = dd;
            }
            txthonors.Text = null;
        }


        private void txtministries_TextChanged(object sender, EventArgs e)
        {
            if (txtministries.Text == "")
            {
                lblministries.Visible = true;
                btnadd1.Enabled = false;
            }
            else { lblministries.Visible = false; btnadd1.Enabled = true; }
        }

        private void lblministries_Click(object sender, EventArgs e)
        {
            lblministries.Visible = false;
            txtministries.Focus();
        }

        private void lstministries_Click(object sender, EventArgs e)
        {
            btndel1.Enabled = true;
            focuslist1 = lstministries.FocusedItem.SubItems[1].Text;
            txtministries.Text = focuslist1;
            grpadding1.Text = "Deleting";
        }

        private void btnadd1_Click(object sender, EventArgs e)
        {
            if (txtministries.Text != "")
            {
                int count = 1;
                ListViewItem itm = new ListViewItem(count++.ToString());
                itm.SubItems.Add(txtministries.Text);
                lstministries.Items.Add(itm);
                allministries += txtministries.Text + "|";
                txtministries.Text = "";
            }
        }

        private void btndel1_Click(object sender, EventArgs e)
        {
            lstministries.Items.Remove(lstministries.FocusedItem);
            grpadding1.Text = "Other Minitries...";
            txtministries.Text = null;
            btndel1.Enabled = false;
            if (allministries.Contains(txtministries.Text))
            {
                string dd1 = allministries.Replace(focuslist1 + "|", null);
                allministries = dd1;
            }
            txtministries.Text = null;
        }

        private void txtawards_TextChanged(object sender, EventArgs e)
        {
            if (txtawards.Text == "")
            {
                lblawards.Visible = true;
                btnadd2.Enabled = false;
            }
            else { lblawards.Visible = false; btnadd2.Enabled = true; }
        }

        private void lblawards_Click(object sender, EventArgs e)
        {
            lblawards.Visible = false;
            txtawards.Focus();
        }

        private void lstawards_Click(object sender, EventArgs e)
        {
            btndel2.Enabled = true;
            focuslist2 = lstawards.FocusedItem.SubItems[1].Text;
            txtawards.Text = focuslist2;
            grpadding2.Text = "Deleting";
        }

        private void btnadd2_Click(object sender, EventArgs e)
        {
            if (txtawards.Text != "")
            {
                int count = 1;
                ListViewItem itm = new ListViewItem(count++.ToString());
                itm.SubItems.Add(txtawards.Text);
                lstawards.Items.Add(itm);
                allawards += txtawards.Text + "|";
                txtawards.Text = "";
            }
        }

        private void btndel2_Click(object sender, EventArgs e)
        {
            lstawards.Items.Remove(lstawards.FocusedItem);
            grpadding2.Text = "Awards Recieveds...";
            txtawards.Text = null;
            btndel2.Enabled = false;
            if (allawards.Contains(txtawards.Text))
            {
                string dd2 = allawards.Replace(focuslist2 + "|", null);
                allawards = dd2;
            }
            txtawards.Text = null;
        }

        private void txtviolations_TextChanged(object sender, EventArgs e)
        {
            if (txtviolations.Text == "")
            {
                lblviolations.Visible = true;
                btnadd3.Enabled = false;
            }
            else { lblviolations.Visible = false; btnadd3.Enabled = true; }
        }

        private void lblviolations_Click(object sender, EventArgs e)
        {
            lblviolations.Visible = false;
            txtviolations.Focus();
        }

        private void lstviolations_Click(object sender, EventArgs e)
        {
            btndel3.Enabled = true;
            focuslist3 = lstviolations.FocusedItem.SubItems[1].Text;
            txtviolations.Text = focuslist3;
            grpadding3.Text = "Deleting";
        }

        private void btnadd3_Click(object sender, EventArgs e)
        {
            if (txtviolations.Text != "")
            {
                int count = 1;
                ListViewItem itm = new ListViewItem(count++.ToString());
                itm.SubItems.Add(txtviolations.Text);
                lstviolations.Items.Add(itm);
                allviolations += txtviolations.Text + "|";
                txtviolations.Text = "";
            }
        }

        private void btndel3_Click(object sender, EventArgs e)
        {
            lstviolations.Items.Remove(lstviolations.FocusedItem);
            grpadding3.Text = "Awards Recieveds...";
            txtviolations.Text = null;
            btndel3.Enabled = false;
            if (allviolations.Contains(txtviolations.Text))
            {
                string dd3 = allviolations.Replace(focuslist3 + "|", null);
                allviolations = dd3;

            }
            txtviolations.Text = null;
        }

        private void txtsacraments_TextChanged(object sender, EventArgs e)
        {
            if (txtsacraments.Text == "")
            {
                lblsacraments.Visible = true;
                btnadd4.Enabled = false;
            }
            else { lblsacraments.Visible = false; btnadd4.Enabled = true; }
        }

        private void lblsacraments_Click(object sender, EventArgs e)
        {
            lblsacraments.Visible = false;
            txtsacraments.Focus();
        }

        private void lstsacraments_Click(object sender, EventArgs e)
        {
            btndel4.Enabled = true;
            focuslist4 = lstsacraments.FocusedItem.SubItems[1].Text;
            txtsacraments.Text = focuslist4;
            grpadding4.Text = "Deleting";
        }

        private void btnadd4_Click(object sender, EventArgs e)
        {
            if (txtsacraments.Text != "")
            {
                int count = 1;
                ListViewItem itm = new ListViewItem(count++.ToString());
                itm.SubItems.Add(txtsacraments.Text);
                lstsacraments.Items.Add(itm);
                allsacraments += txtsacraments.Text + "|";
                txtsacraments.Text = "";
            }
        }

        private void btndel4_Click(object sender, EventArgs e)
        {
            lstsacraments.Items.Remove(lstsacraments.FocusedItem);
            grpadding4.Text = "Awards Recieveds...";
            txtsacraments.Text = null;
            btndel4.Enabled = false;
            if (allsacraments.Contains(txtsacraments.Text))
            {
                string dd4 = allsacraments.Replace(focuslist4 + "|", null);
                allsacraments = dd4;
            }
            txtsacraments.Text = null;
        }

        public void UpdateDetails()
        {
            string p = "select * from  p_details p inner join e_details e on  e.ed_id = p.pd_id " +
                         "inner join m_details m on m.md_id = p.pd_id WHERE pd_id ='" + pid + "';";

            cmd = new MySqlCommand(p, db.OpenConnection());
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                
                while (dr.Read())
                {
                    updatepath = Application.StartupPath + "\\Pictures\\" + dr["pd_photo"].ToString();
                    picturebox.ImageLocation = updatepath;
                    string fullname = dr["pd_fullname"].ToString();
                    txtaddress.Text = dr["pd_address"].ToString();
                    dtbday.Text = dr["pd_birthday"].ToString();
                    txtbirthplace.Text = dr["pd_birthplace"].ToString();
                    txtage.Text = dr["pd_age"].ToString();
                    txtemail.Text = dr["pd_email"].ToString();
                    txtcontactnumber.Text = dr["pd_contactnumber"].ToString();
                    string batch = dr["pd_batch"].ToString();
                    txtmothername.Text = dr["pd_mothername"].ToString();
                    txtmotheroccupation.Text = dr["pd_motheroccupation"].ToString();
                    txtfathername.Text = dr["pd_fathername"].ToString();
                    txtfatheroccupation.Text = dr["pd_fatheroccupation"].ToString();
                    txtcontactnumbermother.Text = dr["pd_contactnumbermother"].ToString();
                    txtcontactnumberfather.Text = dr["pd_contactnumberfather"].ToString();
                    txtprimary.Text = dr["ed_primary_school"].ToString();
                    txtprimaryyear.Text = dr["ed_primary_year"].ToString();
                    txtsecondary.Text = dr["ed_secondary_school"].ToString();
                    txtsecondaryyear.Text = dr["ed_secondary_year"].ToString();
                    txttertiary.Text = dr["ed_tertiary_school"].ToString();
                    txtteriaryyear.Text = dr["ed_tertiary_year"].ToString();
                    txttertiarycourse.Text = dr["ed_tertiary_course"].ToString();
                    txtvocational.Text = dr["ed_vocational_school"].ToString();
                    txtvocationalyear.Text = dr["ed_vocational_year"].ToString();
                    txtvocationalcourse.Text = dr["ed_vocational_course"].ToString();
                    allhonors = dr["ed_honors"].ToString();
                    cmbrank.Text = dr["md_rank"].ToString();
                    cmbposition.Text = dr["md_position"].ToString();
                    dtdateofinvest.Text = dr["md_dateofinvest"].ToString();
                    dtdateofpromotion.Text = dr["md_dateofpromote"].ToString();
                    allministries = dr["md_otherministries"].ToString();
                    allawards = dr["md_awards"].ToString();
                    allviolations = dr["md_violations"].ToString();
                    allsacraments = dr["md_sacraments"].ToString();
                    string stats = dr["md_status"].ToString();

                    if (stats == "Active") { rdactive.Checked = true; }
                    else if (stats == "In-Active") { rdinactive.Checked = true; }
                    else if (stats == "Leave") { rdleave.Checked = true; }
                    else { }
                    //          MessageBox.Show(allawards+ allhonors+ allministries+ allsacraments+ allviolations);

                    lsthonors.Items.Clear();
                    lstministries.Items.Clear();
                    lstawards.Items.Clear();
                    lstviolations.Items.Clear();
                    lstsacraments.Items.Clear();

                    string[] fname = fullname.Split(' ');
                    List<string> listfname = new List<string>();
                    
                    foreach (string f in fname)
                    {
                        if (!f.Equals("") || !f.Equals(" "))
                        {
                            listfname.Add(f);
                        }
                    }

                    txtfname.Text = listfname[0].Replace("_"," ");
                    txtlname.Text = listfname[2];
                    string ldot = listfname[1];
                    txtmname.Text = ldot.Replace(".", "");


                    string[] batchclass = batch.Split(' ');
                    List<string> listbatch = new List<string>();
                    foreach (string f in batchclass)
                    {
                        if (!f.Equals("") || !f.Equals(" "))
                        {
                            listbatch.Add(f);

                        }
                    }
                    txtbatch.Text = batch.Split('-')[0];
                    txtclassof.Text = batch.Split('-')[1];

                    string[] donelsthonors = allhonors.Split('|');
                    foreach (string lst in donelsthonors)
                    {
                        if (!lst.Equals("") && !lst.Equals(" "))
                        {
                            ListViewItem itm = new ListViewItem("");
                            itm.SubItems.Add(lst);
                            lsthonors.Items.Add(itm);
                        }
                    }
                    string[] donelstotherministries = allministries.Split('|');
                    foreach (string lst in donelsthonors)
                    {
                        if (!lst.Equals("") && !lst.Equals(" "))
                        {
                            ListViewItem itm = new ListViewItem("");
                            itm.SubItems.Add(lst);
                            lstministries.Items.Add(itm);
                        }
                    }
                    string[] donelstawards = allawards.Split('|');
                    foreach (string lst in donelstawards)
                    {
                        if (!lst.Equals("") && !lst.Equals(" "))
                        {
                            ListViewItem itm = new ListViewItem("");
                            itm.SubItems.Add(lst);
                            lstawards.Items.Add(itm);
                        }
                    }
                    string[] donelstviolations = allviolations.Split('|');
                    foreach (string lst in donelstviolations)
                    {
                        if (!lst.Equals("") && !lst.Equals(" "))
                        {
                            ListViewItem itm = new ListViewItem("");
                            itm.SubItems.Add(lst);
                            lstviolations.Items.Add(itm);
                        }
                    }
                    string[] donelstsacraments = allsacraments.Split('|');
                    foreach (string lst in donelstsacraments)
                    {
                        if (!lst.Equals("") && !lst.Equals(" "))
                        {
                            ListViewItem itm = new ListViewItem("");
                            itm.SubItems.Add(lst);
                            lstsacraments.Items.Add(itm);
                        }
                    }
                }

            }
            dr.Close();
            db.CloseConnection();

        }
        public void UpdateDetailsSave()
        {
            cons_id = Convert.ToInt32(pid);

            string finalpath;
            if (donepath != null)
            {
                finalpath = cons_id.ToString() + "." + donepath.Split('.')[1];
                File.Delete(Application.StartupPath + "\\Pictures\\" + finalpath);
                File.Copy(pathimage, Application.StartupPath + "\\pictures\\" + finalpath, true);
            }
            else {
                    finalpath = cons_id.ToString() + "." + updatepath.Split('.')[1];
            }
           

            try
            {
                cmd = new MySqlCommand("UpdateDetailsSave", db.OpenConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    cmd.Parameters.Add(new MySqlParameter("?pd_id1", pid));
                    cmd.Parameters.Add(new MySqlParameter("?pd_fullname", txtfname.Text.Trim() + " " + txtmname.Text.Trim() + ". " + txtlname.Text.Trim()));
                    cmd.Parameters.Add(new MySqlParameter("?pd_address", txtaddress.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_birthday", donebday));
                    cmd.Parameters.Add(new MySqlParameter("?pd_birthplace", txtbirthplace.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_age", txtage.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_email", txtemail.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_batch", txtbatch.Text + " - " + txtclassof.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_photo", finalpath));
                    cmd.Parameters.Add(new MySqlParameter("?pd_mothername", txtmothername.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_motheroccupation", txtmotheroccupation.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_fathername", txtfathername.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_fatheroccupation", txtfatheroccupation.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_contactnumber", txtcontactnumber.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_contactnumbermother", txtcontactnumbermother.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_contactnumberfather", txtcontactnumberfather.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_lastname", txtlname.Text));

                    cmd.Parameters.Add(new MySqlParameter("?ed_id", pid));
                    cmd.Parameters.Add(new MySqlParameter("?ed_primary_school", txtprimary.Text));
                    cmd.Parameters.Add(new MySqlParameter("?ed_primary_year", txtprimaryyear.Text));
                    cmd.Parameters.Add(new MySqlParameter("?ed_secondary_school", txtsecondary.Text));
                    cmd.Parameters.Add(new MySqlParameter("?ed_secondary_year", txtsecondaryyear.Text));
                    cmd.Parameters.Add(new MySqlParameter("?ed_tertiary_school", txttertiary.Text));
                    cmd.Parameters.Add(new MySqlParameter("?ed_tertiary_year", txtteriaryyear.Text));
                    cmd.Parameters.Add(new MySqlParameter("?ed_tertiary_course", txttertiarycourse.Text));
                    cmd.Parameters.Add(new MySqlParameter("?ed_vocational_school", txtvocational.Text));
                    cmd.Parameters.Add(new MySqlParameter("?ed_vocational_year", txtvocationalyear.Text));
                    cmd.Parameters.Add(new MySqlParameter("?ed_vocational_course", txtvocationalcourse.Text));
                    cmd.Parameters.Add(new MySqlParameter("?ed_honors", allhonors));

                    cmd.Parameters.Add(new MySqlParameter("?md_id", pid));
                    cmd.Parameters.Add(new MySqlParameter("?md_rank", cmbrank.Text));
                    cmd.Parameters.Add(new MySqlParameter("?md_position", cmbposition.Text));
                    cmd.Parameters.Add(new MySqlParameter("?md_dateofinvest", donedateofinvest));
                    cmd.Parameters.Add(new MySqlParameter("?md_dateofpromote", donedateofpromote));
                    cmd.Parameters.Add(new MySqlParameter("?md_otherministries", allministries));
                    cmd.Parameters.Add(new MySqlParameter("?md_awards", allawards));
                    cmd.Parameters.Add(new MySqlParameter("?md_violations", allviolations));
                    cmd.Parameters.Add(new MySqlParameter("?md_sacraments", allsacraments));
                    cmd.Parameters.Add(new MySqlParameter("?md_status", status));
                    cmd.Parameters.Add(new MySqlParameter("?md_retrieve", "NO"));
                    cmd.ExecuteNonQuery();
                    db.CloseConnection();


                   //MessageBox.Show(allawards + allhonors + allministries + allsacraments + allviolations);
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

        }
        public void Saveall()
        {
            string finalpath;
            try
            {
               
                if (donepath != null)
                {
                    string q = "SELECT pd_id FROM dbms_mass.p_details order  by pd_id desc limit 1 ;";

                    cmd = new MySqlCommand(q, db.OpenConnection());
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows) { while (dr.Read()) { cons_id = (int)dr[0] + 1; } }
                    finalpath = cons_id.ToString() + "." + donepath.Split('.')[1];
                    File.Copy(pathimage, Application.StartupPath + "\\pictures\\" + finalpath, true);
                }
                else
                {
                    finalpath = "";
                }



                cmd = new MySqlCommand("Saveall", db.OpenConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {

                    cmd.Parameters.Add(new MySqlParameter("?ed_primary_school", txtprimary.Text));
                    cmd.Parameters.Add(new MySqlParameter("?ed_primary_year", txtprimaryyear.Text));
                    cmd.Parameters.Add(new MySqlParameter("?ed_secondary_school", txtsecondary.Text));
                    cmd.Parameters.Add(new MySqlParameter("?ed_secondary_year", txtsecondaryyear.Text));
                    cmd.Parameters.Add(new MySqlParameter("?ed_tertiary_school", txttertiary.Text));
                    cmd.Parameters.Add(new MySqlParameter("?ed_tertiary_year", txtteriaryyear.Text));
                    cmd.Parameters.Add(new MySqlParameter("?ed_tertiary_course", txttertiarycourse.Text));
                    cmd.Parameters.Add(new MySqlParameter("?ed_vocational_school", txtvocational.Text));
                    cmd.Parameters.Add(new MySqlParameter("?ed_vocational_year", txtvocationalyear.Text));
                    cmd.Parameters.Add(new MySqlParameter("?ed_vocational_course", txtvocationalcourse.Text));
                    cmd.Parameters.Add(new MySqlParameter("?ed_honors", allhonors));

                    cmd.Parameters.Add(new MySqlParameter("?md_rank", cmbrank.Text));
                    cmd.Parameters.Add(new MySqlParameter("?md_position", cmbposition.Text));
                    cmd.Parameters.Add(new MySqlParameter("?md_dateofinvest", donedateofinvest));
                    cmd.Parameters.Add(new MySqlParameter("?md_dateofpromote", donedateofpromote));
                    cmd.Parameters.Add(new MySqlParameter("?md_otherministries", allministries));
                    cmd.Parameters.Add(new MySqlParameter("?md_awards", allawards));
                    cmd.Parameters.Add(new MySqlParameter("?md_violations", allviolations));
                    cmd.Parameters.Add(new MySqlParameter("?md_sacraments", allsacraments));
                    cmd.Parameters.Add(new MySqlParameter("?md_status", status));
                    cmd.Parameters.Add(new MySqlParameter("?md_retrieve", "NO"));

                    cmd.Parameters.Add(new MySqlParameter("?pd_fullname", txtfname.Text + " " + txtmname.Text + ". " + txtlname.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_address", txtaddress.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_birthday", donebday));
                    cmd.Parameters.Add(new MySqlParameter("?pd_birthplace", txtbirthplace.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_age", txtage.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_email", txtemail.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_contactnumber", txtcontactnumber.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_batch", txtbatch.Text + " - " + txtclassof.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_photo", finalpath));
                    cmd.Parameters.Add(new MySqlParameter("?pd_mothername", txtmothername.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_motheroccupation", txtmotheroccupation.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_fathername",txtfathername.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_fatheroccupation", txtfatheroccupation.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_contactnumbermother", txtcontactnumbermother.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_contactnumberfather", txtcontactnumberfather.Text));
                    cmd.Parameters.Add(new MySqlParameter("?pd_lastname", txtlname.Text));

                    cmd.ExecuteNonQuery();
                    db.CloseConnection();

                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }      

        private void btnsave_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();

            /*select *
                from  p_details p
                inner join e_details e
                on  e.ed_id = p.pd_id
                inner join m_details m
                on m.md_id = p.pd_id;
             * **/

            if ((txtfname.Text.ToString().Trim().Length == 0) || (txtlname.Text.ToString().Trim().Length == 0)
                || (txtmname.Text.ToString().Trim().Length == 0) || (dtbday.Text.ToString().Trim().Length == 0) ||
                (txtage.Text.ToString().Trim().Length == 0) || (txtbatch.Text.ToString().Trim().Length == 0) ||
                (txtclassof.Text.ToString().Trim().Length == 0) || (txtaddress.Text.ToString().Trim().Length == 0) ||
                (txtmothername.Text.ToString().Trim().Length == 0) || (txtfathername.Text.ToString().Trim().Length  == 0))
            {
                buttom.BackColor = Color.Tomato;

                lblwarning.Visible = true;
                lblwarning.Text = "Please Complete the Personal Details !.";
                icowarning.Visible = true;
            }
            else if ((txtfname.Text.ToString().Trim().Length == 0) || (txtlname.Text.ToString().Trim().Length == 0) || (txtmname.Text.ToString().Trim().Length == 0) ||
                (dtbday.Text.ToString().Trim().Length == 0) || (txtbatch.Text.ToString().Trim().Length == 0) || (txtclassof.Text.ToString().Trim().Length == 0) ||
                (txtmothername.Text.ToString().Trim().Length == 0) || (txtfathername.Text.ToString().Trim().Length == 0) || (cmbrank.Text.ToString().Trim().Length == 0) || (cmbposition.Text.ToString().Trim().Length == 0))
            {
                buttom.BackColor = Color.Tomato;
                lblwarning.Visible = true;
                lblwarning.Text = "All data that has ( * ) need to fill !.";
                icowarning.Visible = true;
            }
            else
            {

                string d = dtbday.Value.ToLongDateString();
                int dd = d.IndexOf(",");
                donebday = d.Remove(0, dd + 2);
                string d1 = dtdateofinvest.Value.ToLongDateString();
                int dd1 = d1.IndexOf(",");
                donedateofinvest = d1.Remove(0, dd1 + 2);
                string d2 = dtdateofpromotion.Value.ToLongDateString();
                int dd2 = d2.IndexOf(",");
                donedateofpromote = d2.Remove(0, dd2 + 2);

                txtfname.Text = formaltext(txtfname.Text).Replace(" ", "_").Trim();
                txtlname.Text = formaltext(txtlname.Text).Trim();
                txtmname.Text = formaltext(txtmname.Text).Trim();
                txtaddress.Text = formaltext(txtaddress.Text);
                txtbirthplace.Text = formaltext(txtbirthplace.Text);
                txtbatch.Text = formaltext(txtbatch.Text);
                txtclassof.Text = formaltext(txtclassof.Text);
                txtmothername.Text = formaltext(txtmothername.Text);
                txtmotheroccupation.Text = formaltext(txtmotheroccupation.Text);
                txtfathername.Text = formaltext(txtfathername.Text);
                txtfatheroccupation.Text = formaltext(txtfatheroccupation.Text);
                txtprimary.Text = formaltext(txtprimary.Text);
                txtprimaryyear.Text = formaltext(txtprimaryyear.Text);
                txtsecondary.Text = formaltext(txtsecondary.Text);
                txtsecondaryyear.Text = formaltext(txtsecondaryyear.Text);
                txttertiary.Text = formaltext(txttertiary.Text);
                txtteriaryyear.Text = formaltext(txtteriaryyear.Text);
                txttertiarycourse.Text = formaltext(txttertiarycourse.Text);
                txtvocational.Text = formaltext(txtvocational.Text);
                txtvocationalyear.Text = formaltext(txtvocationalyear.Text);
                txtvocationalcourse.Text = formaltext(txtvocationalcourse.Text);
                
                if (lblheader.Text == "Adding Member's")
                {
                    buttom.BackColor = Color.Lime;
                    lblwarning.BackColor = Color.Lime;
                    icowarning.BackColor = Color.Lime;
                    lblwarning.Text = "Adding Member's Success";
                    icowarning.BackgroundImage = imageList1.Images[4];
                    MasterList.isUpdated = true;
                    Saveall();
                }
                else if (lblheader.Text == "Updating Member's")
                {
                    buttom.BackColor = Color.MediumVioletRed;
                    lblwarning.BackColor = Color.MediumVioletRed;
                    icowarning.BackColor = Color.MediumVioletRed;
                    lblwarning.Text = "Updating Member's Success";
                    icowarning.BackgroundImage = imageList1.Images[4];
                    MasterList.isUpdated = true;
                    UpdateDetailsSave();
                }



                lblwarning.Visible = true;
                icowarning.Visible = true;

            }

            //  MessageBox.Show(allhonors + "\n" + allministries + "\n" + allawards + "\n" + allviolations + "\n" + allsacraments);
            txtfname.Text = txtfname.Text.Replace("_", " ").Trim();
            txtlname.Text = txtlname.Text.Replace("_", " ").Trim();
            txtmname.Text = txtmname.Text.Replace("_", " ").Trim();
            txtbirthplace.Text = txtbirthplace.Text.Replace("_", " ");
            txtbatch.Text = txtbatch.Text.Replace("_", " ");
            txtclassof.Text = txtclassof.Text.Replace("_", " ");
            txtmothername.Text = txtmothername.Text.Replace("_", " ");
            txtmotheroccupation.Text = txtmotheroccupation.Text.Replace("_", " ");
            txtfathername.Text = txtfathername.Text.Replace("_", " ");
            txtfatheroccupation.Text = txtfatheroccupation.Text.Replace("_", " ");
            txtprimary.Text = txtprimary.Text.Replace("_", " ");
            txtprimaryyear.Text = txtprimaryyear.Text.Replace("_", " ");
            txtsecondary.Text = txtsecondary.Text.Replace("_", " ");
            txtsecondaryyear.Text = txtsecondaryyear.Text.Replace("_", " ");
            txttertiary.Text = txttertiary.Text.Replace("_", " ");
            txtteriaryyear.Text = txtteriaryyear.Text.Replace("_", " ");
            txttertiarycourse.Text = txttertiarycourse.Text.Replace("_", " ");
            txtvocational.Text = txtvocational.Text.Replace("_", " ");
            txtvocationalyear.Text = txtvocationalyear.Text.Replace("_", " ");
            txtvocationalcourse.Text = txtvocationalcourse.Text.Replace("_", " ");
        }

        private void btnbrowseimage_Click(object sender, EventArgs e)
        {
            BrowseImage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lsthonors.Items.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] d = allhonors.Split('|');
            foreach (string dd in d)
            {
                ListViewItem itm = new ListViewItem("");
                itm.SubItems.Add(dd);
                lsthonors.Items.Add(itm);
            }
        }

        private void dtbday_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            if (date.Month >= dtbday.Value.Month && date.Day >= dtbday.Value.Day && date.Year >= dtbday.Value.Year)
            {
                txtage.Text = (date.Year - dtbday.Value.Year).ToString();
            }
            else
            {
                txtage.Text = (date.Year - 1 - dtbday.Value.Year).ToString();
            }
            int age = Convert.ToInt32(txtage.Text);
            if (age < 0)
                txtage.Text = "0";
        }
        private void dtdateofinvest_ValueChanged(object sender, EventArgs e)
        {
        }

        private void dtdateofpromotion_ValueChanged(object sender, EventArgs e)
        {
        }

        private void txtmothername_TextChanged(object sender, EventArgs e)
        {
            //if ((txtmothername.Text == "deceased") || (txtmothername.Text == "none"))
            //{
            //    txtmotheroccupation.Text = "None";
            //    txtcontactnumbermother.Text = "None";
            //}
            //else
            //{ }
        }

        private void txtfathername_TextChanged(object sender, EventArgs e)
        {
        //    if ((txtfathername.Text == "deceased") || (txtfathername.Text == "none"))
        //    {
        //        txtfatheroccupation.Text = "None";
        //        txtcontactnumberfather.Text = "None";
        //    }
        //    else
        //    { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int seconds = Convert.ToInt32(lblnum.Text);
            lblnum.Text = Convert.ToString(seconds - 1);
            if (lblnum.Text == "0")
            {
                buttom.BackColor = Color.Honeydew;
                timer1.Stop();
                lblwarning.BackColor = Color.Tomato;
                lblwarning.Visible = false;
                icowarning.BackColor = Color.Tomato;
                icowarning.BackgroundImage = imageList1.Images[5];
                icowarning.Visible = false;
                lblnum.Text = "5";
            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            if (counterdate != now)
            {
                counterdate++;
                txtprimaryyear.Items.Add(counterdate);
                txtsecondaryyear.Items.Add(counterdate);
                txtvocationalyear.Items.Add(counterdate);
                txtteriaryyear.Items.Add(counterdate);
            }
        }

        private void txtage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Header_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtfname_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtfname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && (e.KeyChar != ' '))
            {
                e.Handled = true;

            }
        }

        private void rdactive_CheckedChanged(object sender, EventArgs e)
        {
            status = "Active";
        }

        private void rdinactive_CheckedChanged(object sender, EventArgs e)
        {
            status = "In Active";
        }

        private void rdleave_CheckedChanged(object sender, EventArgs e)
        {
            status = "Leave";
        }

    }
}
