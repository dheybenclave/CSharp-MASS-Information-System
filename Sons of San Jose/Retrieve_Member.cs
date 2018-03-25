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

namespace Sons_of_San_Jose
{
    public partial class Retrieve_Member : Form
    {
        Db_Utilities db = new  Db_Utilities();
        DataTable table = new DataTable();
        MySqlDataAdapter adapt;
        public Retrieve_Member()
        {
            InitializeComponent();
            ListviewThrow();
            btndelete.BackgroundImage = imageList1.Images[9];
            btnrestore.BackgroundImage = imageList1.Images[10];
        }
        
        public static bool isUpdated = false;
        public static bool isDelete = false;
        public static bool ifrestore = false;
        string searchval;

           public string refresh;
        //for personal data;
        string pid, fullname, address, birthday, birthplace, age, email, contactnumber, batch, photo, mothername, motheroccupation, fathername, fatheroccupation, contactnumbermother, contactnumberfather;
        /**************************/

        //for  educational data
        string eid, primaryschool, primaryyear, secondaryschool, secondaryyear, tertiaryschool, tertiaryyear, tertiarycourse, vocationalschool, vocationalyear, vocationalcourse, honors;
        /**************************/

        //for ministry data
        string mid, rank, position, dateofinvest, dateofpromote, otherministries, awards, violations, sacraments,status;
        /**************************/

        private void btnclearsearch_Click(object sender, EventArgs e)
        {
            reset();
            txtsearch.Text = "";
        }

        private void Retrieve_Member_Load(object sender, EventArgs e)
        {

        }

        private void close_Click(object sender, EventArgs e)
        { this.Close(); }

        private void minimize_Click(object sender, EventArgs e)
        { this.WindowState = FormWindowState.Minimized; }

        public void reset()
        {
            lblfullname.Text = "Name : ";
            lbladdress.Text = "Address : ";
            lblbirthdaybirthplace.Text = "Birthday and Birthplace : ";
            lblage.Text = "Age : ";
            lblemail.Text = "Email : ";
            lblcontactnumber.Text = "Contact Number : ";
            lblbatch.Text = "Batch and Class : ";
            picturebox.ImageLocation = null;
            lblmothername.Text = "Mother Name : ";
            lblmotheroccupation.Text = "Occupation : ";
            lblfathername.Text = "Father Name : ";
            lblfatheroccupation.Text = "Occupation : ";
            lblparentscontactnumber.Text = "Parents Contact Number : ";
            lblprimaryschool.Text = "Primary School : ";
            lblprimaryyear.Text = "Year : ";
            lblsecondaryschool.Text = "Secondary School : ";
            lblsecondaryyear.Text = "Year : ";
            lbltertiaryschool.Text = "Tertiary School : ";
            lbltertiaryyear.Text = "Year : ";
            lbltertiarycourse.Text = "Course : ";
            lblvocationalschool.Text = "Vocational School : ";
            lblvocationalyear.Text = "Year : ";
            lblvocationalcourse.Text = "Course : ";
            lblrank.Text = "Rank : ";
            lblposition.Text = "Position : ";
            lblmstatus.Text = "Status : ";
            lbldateofinvestiture.Text = "Date of Investiture : ";
            lbldateofpromotion.Text = "Date of Promotion : ";

            lsthonors.Items.Clear();
            lstministries.Items.Clear();
            lstawards.Items.Clear();
            lstviolations.Items.Clear();
            lstsacraments.Items.Clear();
        }

        public void ListviewThrow()
        {
            string p_details = "select * from  p_details p inner join e_details e on  e.ed_id = p.pd_id " +
           "inner join m_details m on m.md_id = p.pd_id WHERE m.md_retrieve ='YES'  ORDER BY pd_lastname ASC ;";
            AllDetails(p_details);
        }

        public void AllDetails(string query)
        {
            table.Clear();
            adapt = new MySqlDataAdapter(query, db.OpenConnection());
            db.CloseConnection();
            adapt.Fill(table);

            foreach (DataRow rd in table.Rows)
            {
                ListViewItem itm = new ListViewItem(rd["pd_id"].ToString());
                itm.SubItems.Add(rd["pd_fullname"].ToString().Replace("_", " "));
                itm.SubItems.Add(rd["pd_address"].ToString());
                itm.SubItems.Add(rd["pd_birthday"].ToString());
                itm.SubItems.Add(rd["pd_birthplace"].ToString());
                itm.SubItems.Add(rd["pd_age"].ToString());
                itm.SubItems.Add(rd["pd_email"].ToString());
                itm.SubItems.Add(rd["pd_contactnumber"].ToString());
                itm.SubItems.Add(rd["pd_batch"].ToString());
                itm.SubItems.Add(rd["pd_photo"].ToString());
                itm.SubItems.Add(rd["pd_mothername"].ToString());
                itm.SubItems.Add(rd["pd_motheroccupation"].ToString());
                itm.SubItems.Add(rd["pd_fathername"].ToString());
                itm.SubItems.Add(rd["pd_fatheroccupation"].ToString());
                itm.SubItems.Add(rd["pd_contactnumbermother"].ToString());
                itm.SubItems.Add(rd["pd_contactnumberfather"].ToString());
                itm.SubItems.Add(rd["ed_primary_school"].ToString());
                itm.SubItems.Add(rd["ed_primary_year"].ToString());
                itm.SubItems.Add(rd["ed_secondary_school"].ToString());
                itm.SubItems.Add(rd["ed_secondary_year"].ToString());
                itm.SubItems.Add(rd["ed_tertiary_school"].ToString());
                itm.SubItems.Add(rd["ed_tertiary_year"].ToString());
                itm.SubItems.Add(rd["ed_tertiary_course"].ToString());
                itm.SubItems.Add(rd["ed_vocational_school"].ToString());
                itm.SubItems.Add(rd["ed_vocational_year"].ToString());
                itm.SubItems.Add(rd["ed_vocational_course"].ToString());
                itm.SubItems.Add(rd["ed_honors"].ToString());
                itm.SubItems.Add(rd["md_rank"].ToString());
                itm.SubItems.Add(rd["md_position"].ToString());
                itm.SubItems.Add(rd["md_dateofinvest"].ToString());
                itm.SubItems.Add(rd["md_dateofpromote"].ToString());
                itm.SubItems.Add(rd["md_otherministries"].ToString());
                itm.SubItems.Add(rd["md_awards"].ToString());
                itm.SubItems.Add(rd["md_violations"].ToString());
                itm.SubItems.Add(rd["md_sacraments"].ToString());
                itm.SubItems.Add(rd["md_status"].ToString());
                itm.SubItems.Add(rd["md_retrieve"].ToString());
                lstmasterlist.Items.Add(itm);
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

        private void lstmasterlist_Click(object sender, EventArgs e)
        {
            try
            {
                pid = lstmasterlist.FocusedItem.SubItems[0].Text;
                fullname = lstmasterlist.FocusedItem.SubItems[1].Text;
                address = lstmasterlist.FocusedItem.SubItems[2].Text;
                birthday = lstmasterlist.FocusedItem.SubItems[3].Text;
                birthplace = lstmasterlist.FocusedItem.SubItems[4].Text;
                age = lstmasterlist.FocusedItem.SubItems[5].Text;
                email = lstmasterlist.FocusedItem.SubItems[6].Text;
                contactnumber = lstmasterlist.FocusedItem.SubItems[7].Text;
                batch = lstmasterlist.FocusedItem.SubItems[8].Text;
                photo = lstmasterlist.FocusedItem.SubItems[9].Text;
                mothername = lstmasterlist.FocusedItem.SubItems[10].Text;
                motheroccupation = lstmasterlist.FocusedItem.SubItems[11].Text;
                fathername = lstmasterlist.FocusedItem.SubItems[12].Text;
                fatheroccupation = lstmasterlist.FocusedItem.SubItems[13].Text;
                contactnumbermother = lstmasterlist.FocusedItem.SubItems[14].Text;
                contactnumberfather = lstmasterlist.FocusedItem.SubItems[15].Text;
                primaryschool = lstmasterlist.FocusedItem.SubItems[16].Text;
                primaryyear = lstmasterlist.FocusedItem.SubItems[17].Text;
                secondaryschool = lstmasterlist.FocusedItem.SubItems[18].Text;
                secondaryyear = lstmasterlist.FocusedItem.SubItems[19].Text;
                tertiaryschool = lstmasterlist.FocusedItem.SubItems[20].Text;
                tertiaryyear = lstmasterlist.FocusedItem.SubItems[21].Text;
                tertiarycourse = lstmasterlist.FocusedItem.SubItems[22].Text;
                vocationalschool = lstmasterlist.FocusedItem.SubItems[23].Text;
                vocationalyear = lstmasterlist.FocusedItem.SubItems[24].Text;
                vocationalcourse = lstmasterlist.FocusedItem.SubItems[25].Text;
                honors = lstmasterlist.FocusedItem.SubItems[26].Text;
                rank = lstmasterlist.FocusedItem.SubItems[27].Text;
                position = lstmasterlist.FocusedItem.SubItems[28].Text;
                dateofinvest = lstmasterlist.FocusedItem.SubItems[29].Text;
                dateofpromote = lstmasterlist.FocusedItem.SubItems[30].Text;
                otherministries = lstmasterlist.FocusedItem.SubItems[31].Text;
                awards = lstmasterlist.FocusedItem.SubItems[32].Text;
                violations = lstmasterlist.FocusedItem.SubItems[33].Text;
                sacraments = lstmasterlist.FocusedItem.SubItems[34].Text;
                status = lstmasterlist.FocusedItem.SubItems[35].Text;

                lblfullname.Text = "Name : " + fullname;
                lbladdress.Text = "Address : " + address;
                lblbirthdaybirthplace.Text = "Birthday and Birthplace : " + birthday + " ( " + birthplace + " ) ";
                lblage.Text = "Age : " + age + " year's old";
                lblemail.Text = "Email : " + email;
                lblcontactnumber.Text = "Contact Number : " + contactnumber;
                lblbatch.Text = "Batch and Class : " + batch;
                picturebox.ImageLocation = Application.StartupPath + "\\Pictures\\" + photo;
                lblmothername.Text = "Mother Name : " + mothername;
                lblmotheroccupation.Text = "Occupation : " + motheroccupation;
                lblfathername.Text = "Father Name : " + fathername;
                lblfatheroccupation.Text = "Occupation : " + fatheroccupation;
                lblparentscontactnumber.Text = "Parents Contact Number : " + contactnumbermother + " / " + contactnumberfather;
                lblprimaryschool.Text = "Primary School : " + primaryschool;
                lblprimaryyear.Text = "Year : " + primaryyear;
                lblsecondaryschool.Text = "Secondary School : " + secondaryschool;
                lblsecondaryyear.Text = "Year : " + secondaryyear;
                lbltertiaryschool.Text = "Tertiary School : " + tertiaryschool;
                lbltertiaryyear.Text = "Year : " + tertiaryyear;
                lbltertiarycourse.Text = "Course : " + tertiarycourse;
                lblvocationalschool.Text = "Vocational School : " + vocationalschool;
                lblvocationalyear.Text = "Year : " + vocationalyear;
                lblvocationalcourse.Text = "Course : " + vocationalcourse;
                lblrank.Text = "Rank : " + rank;
                lblposition.Text = "Position : " + position;
                lblmstatus.Text = "Status : " + status;
                lbldateofinvestiture.Text = "Date of Investiture : " + dateofinvest;
                lbldateofpromotion.Text = "Date of Promotion : " + dateofpromote;

                lsthonors.Items.Clear();
                lstministries.Items.Clear();
                lstawards.Items.Clear();
                lstviolations.Items.Clear();
                lstsacraments.Items.Clear();
                string[] donelsthonors = honors.Split('|');
                foreach (string lst in donelsthonors)
                {
                    if (!lst.Equals(""))
                    {
                        ListViewItem itm = new ListViewItem("");
                        itm.SubItems.Add(lst);
                        lsthonors.Items.Add(itm);
                    }
                }
                string[] donelstotherministries = otherministries.Split('|');
                foreach (string lst in donelstotherministries)
                {
                    if (!lst.Equals(""))
                    {
                        ListViewItem itm = new ListViewItem("");
                        itm.SubItems.Add(lst);
                        lstministries.Items.Add(itm);
                    }
                }
                string[] donelstawards = awards.Split('|');
                foreach (string lst in donelstawards)
                {
                    if (!lst.Equals(""))
                    {
                        ListViewItem itm = new ListViewItem("");
                        itm.SubItems.Add(lst);
                        lstawards.Items.Add(itm);
                    }
                }
                string[] donelstviolations = violations.Split('|');
                foreach (string lst in donelstviolations)
                {
                    if (!lst.Equals(""))
                    {
                        ListViewItem itm = new ListViewItem("");
                        itm.SubItems.Add(lst);
                        lstviolations.Items.Add(itm);
                    }
                }
                string[] donelstsacraments = sacraments.Split('|');
                foreach (string lst in donelstsacraments)
                {
                    if (!lst.Equals(""))
                    {
                        ListViewItem itm = new ListViewItem("");
                        itm.SubItems.Add(lst);
                        lstsacraments.Items.Add(itm);
                    }
                }
            }
            catch { }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (pid != null)
            {
                ExitForm ef = new ExitForm();
                ef.lbltxt.Text = "Do you want to permanently delete this member?";
                ef.lbltxt.Location = new Point(15, 30);
                ef.ifdelete = true;
                ef.pid = pid;
                ef.imgdelete = photo;
                ef.ShowDialog();
                timer1.Start();
            }
            else
            {
                timer1.Start();
                buttom.BackColor = Color.Red;
                lblwarning.Text = "Please select Member before you Delete !.";
                lblwarning.BackColor = Color.Red;
                icowarning.BackgroundImage = imageList1.Images[5];
                icowarning.BackColor = Color.Red;
                lblwarning.Visible = true;
                icowarning.Visible = true;
            }
        }

        private void btnrestore_Click(object sender, EventArgs e)
        {

            if (pid != null)
            {
                ExitForm ef = new ExitForm();
                ef.lbltxt.Text = "Do you want to restore this member?";
                ef.lbltxt.Location = new Point(40, 30);
                ef.ifrestore = true;
                ef.pid = pid;
                ef.imgdelete = photo;
                ef.ShowDialog();

            }
            else
            {
                timer1.Enabled = true;
                timer1.Start();
                lblwarning.Visible = true;
                icowarning.Visible = true;
                buttom.BackColor =  lblwarning.BackColor  =   icowarning.BackColor = Color.Red;
                lblwarning.Text = "Please select Member before you Restore !.";
                icowarning.BackgroundImage = imageList1.Images[5];
            }
            MasterList.isDelete = MasterList.isUpdated = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            if (isUpdated == true)
            {
                timer1.Start();
                lblwarning.Text = "Restore Member Success !.";
                buttom.BackColor = icowarning.BackColor = lblwarning.BackColor = Color.MediumVioletRed;
                icowarning.BackgroundImage = imageList1.Images[6];
                lblwarning.Visible = true;
                icowarning.Visible = true;
                lstmasterlist.Items.Clear();
                ListviewThrow();
                isUpdated = false;
            }

            if (isDelete == true)
            {

                timer1.Start();
                lblwarning.Text = "Delete Member Success! .";
                buttom.BackColor = icowarning.BackColor = lblwarning.BackColor = Color.Lime;
                icowarning.BackgroundImage = imageList1.Images[6];
                lblwarning.Visible = true;
                icowarning.Visible = true;
                lstmasterlist.Items.Clear();
                ListviewThrow();
                reset();
                isDelete = false;
            }
            if (ifrestore == true) {
                timer1.Start();
                lblwarning.Text = "Restore Member Success! .";
                buttom.BackColor = icowarning.BackColor = lblwarning.BackColor = Color.Lime;
                icowarning.BackgroundImage = imageList1.Images[6];
                lblwarning.Visible = true;
                icowarning.Visible = true;
                lstmasterlist.Items.Clear();
                ListviewThrow();
                reset();
                isDelete = false;
                lstmasterlist.Items.Clear(); 
                ListviewThrow(); 
                ifrestore = false; }
        }

        private void buttom_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblsearch_Click(object sender, EventArgs e)
        {
            txtsearch.Focus();
            lblsearch.Visible = false;
        }

        private void txtsearch_Click(object sender, EventArgs e)
        {
            txtsearch.Focus();
            lblsearch.Visible = false;
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            if (txtsearch.Text.ToString().Trim().Length == 0)
            {
                lblsearch.Visible = true;
            }
            else
            {
                txtsearch.Focus();
                lblsearch.Visible = false;
            }
            string search = txtsearch.Text.Replace("'", "''").Replace(" ", "_");
            lstmasterlist.Items.Clear();
            searchval = "select * from  p_details p inner join e_details e on  e.ed_id = p.pd_id " +
               "inner join m_details m on m.md_id = p.pd_id  Where ( pd_fullname Like '%" + search + "%' OR  pd_address LIKE'%" + search + "%'" +
               "OR  pd_birthday LIKE'%" + search + "%' OR  pd_birthplace LIKE'%" + search + "%'" +
               "OR  pd_age LIKE'%" + search + "%' OR  pd_email LIKE'%" + search + "%'" +
               "OR  pd_contactnumber LIKE'%" + search + "%' OR  pd_batch LIKE'%" + search + "%'" +
               "OR  pd_mothername LIKE'%" + search + "%' OR  pd_motheroccupation LIKE'%" + search + "%'" +
               "OR  pd_fathername LIKE'%" + search + "%' OR  pd_fatheroccupation LIKE'%" + search + "%'" +
               "OR  pd_contactnumbermother LIKE'%" + search + "%' OR  pd_contactnumberfather LIKE'%" + search + "%'" +
               "OR  ed_primary_school LIKE'%" + search + "%' OR  ed_primary_year LIKE'%" + search + "%'" +
               "OR  ed_secondary_school LIKE'%" + search + "%' OR  ed_secondary_year LIKE'%" + search + "%'" +
               "OR  ed_tertiary_school LIKE'%" + search + "%' OR  ed_tertiary_year LIKE'%" + search + "%'" +
               "OR  ed_tertiary_course LIKE'%" + search + "%' OR  ed_vocational_school LIKE'%" + search + "%'" +
               "OR  ed_vocational_year LIKE'%" + search + "%' OR  ed_vocational_course LIKE'%" + search + "%'" +
               "OR  ed_honors LIKE'%" + search + "%' OR  md_rank LIKE'%" + search + "%'" +
               "OR  md_position LIKE'%" + search + "%' OR  md_dateofinvest LIKE'%" + search + "%'" +
               "OR  md_dateofpromote LIKE'%" + search + "%' OR  md_otherministries LIKE'%" + search + "%'" +
               "OR  md_awards LIKE'%" + search + "%' OR  md_violations LIKE'%" + search + "%'" +
               "OR  md_sacraments LIKE'%" + search + "%' OR  md_status LIKE'" + search + "%' " + " ) AND m.md_retrieve ='YES' ORDER BY pd_lastname ASC";
            AllDetails(searchval);
        }
    }
}
