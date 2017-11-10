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

namespace Sons_of_San_Jose
{
    public partial class MasterList : Form
    {
        Db_Utilities db = new Db_Utilities();
        MySqlCommand cmd;
        DataTable table = new DataTable();
        MySqlDataAdapter adapt;

        public static bool isUpdated = false;
        public static bool isDelete = false;
        string searchval ,combosearchvalue ="";

        public MasterList()
        {
            InitializeComponent();
            //yythis.Location = new Point(President_form.ActiveForm.DesktopLocation.X, President_form.ActiveForm.DesktopLocation.Y);
            btnadd.BackgroundImage = imageList1.Images[0];
            btnupdate.BackgroundImage = imageList1.Images[1];
            btndelete.BackgroundImage = imageList1.Images[2];
            btnmasterlist.BackgroundImage = imageList1.Images[6];
            btnretrieve.BackgroundImage = imageList1.Images[7];
            btnprint.BackgroundImage = imageList1.Images[9];
            ListviewThrow();
            txtsearch2.Enabled = false;
            lblsearch2.Enabled = false;
            lblsearch2.BackColor = Color.WhiteSmoke;
            lblcmbsearch.BackColor = Color.WhiteSmoke;
            cmbcategorysearch.Enabled = false;
            groupBox3.Visible = false;
            headmasterlist.Location = new Point(986, 135);
            lblmasterlist.Location = new Point(1127, 135);
            lstmasterlist.Location = new Point(990, 160);
            lstmasterlist.Size = new Size(355, 490);
        }

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

        public void ListviewThrow()
        {
            string p_details = "select * from  p_details p inner join e_details e on  e.ed_id = p.pd_id " +
           "inner join m_details m on m.md_id = p.pd_id WHERE m.md_retrieve ='NO'  ORDER BY pd_lastname ASC ;";
            AllDetails(p_details);
        }
        public void ComboboxSearchLoad()
        {   //select * from  p_details p inner join e_details e on  e.ed_id = p.pd_id 
            //inner join m_details m on m.md_id = p.pd_id
            //where (p.pd_fullname LIKE'%bensurto%' OR m.md_sacraments LIKE'%dheo%') AND
           //e.ed_tertiary_course LIKE'%2019%' OR e.ed_tertiary_school LIKE'%2019%' OR e.ed_tertiary_year LIKE'%2010%';
            switch (cmbcategorysearch.Text)
            {
                case "None": combosearchvalue = ""; break;
                case "Fullname": combosearchvalue = "pd_fullname"; break;
                case "Address": combosearchvalue = "pd_address"; break;
                case "Birthday": combosearchvalue = "pd_birthday"; break;
                case "Birthplace": combosearchvalue = "pd_birthplace"; break;
                case "Age": combosearchvalue = "pd_age"; break;
                case "Email": combosearchvalue = "pd_email"; break;
                case "Contact Number": combosearchvalue = "pd_contactnumber"; break;
                case "Batch Name and Class": combosearchvalue = "pd_batch"; break;
                case "Sacraments": combosearchvalue = "md_sacraments"; break;
                case "Parents Name": combosearchvalue = "pd_fathername-pd_mothername"; break;
                case "Parents Occupation": combosearchvalue = "pd_fatheroccupation-pd_motheroccupation"; break;
                case "Parents Contact Number": combosearchvalue = "pd_contactnumbermother-pd_contactnumberfather"; break;
                case "Primary Educational Details": combosearchvalue = "ed_primary_school-ed_primary_year"; break;
                case "Secondary Educational Details": combosearchvalue = "ed_secondary_school-ed_secondary_year"; break;
                case "Tertiary Educational Details": combosearchvalue = "ed_tertiary_school-ed_tertiary_year-ed_tertiary_course"; break;
                case "Vocational Educational Details": combosearchvalue = "ed_vocational_school-ed_vocational_year-ed_vocational_course"; break;
                case "Educational Honors": combosearchvalue = "ed_honors"; break;
                case "Rank": combosearchvalue = "md_rank"; break;
                case "Position": combosearchvalue = "md_position"; break;
                case "Status": combosearchvalue = "md_status"; break;
                case "Date of Investiture": combosearchvalue = "md_dateofinvest"; break;
                case "Date of Latest Promotion": combosearchvalue = "md_dateofpromote"; break;
                case "Other Ministry Name or Position": combosearchvalue = "md_otherministries"; break;
                case "Ministry Awards": combosearchvalue = "md_awards"; break;
                case "Ministry Violations": combosearchvalue = "md_violations"; break;
            }
            //searchval
            //where (p.pd_fullname LIKE'%bensurto%' OR m.md_sacraments LIKE'%dheo%') AND
            //e.ed_tertiary_course LIKE'%2019%' OR e.ed_tertiary_school LIKE'%2019%' OR e.ed_tertiary_year LIKE'%2010%';
        }

        private void txtsearch2_TextChanged(object sender, EventArgs e)
        {
            lstmasterlist.Items.Clear();
            if (txtsearch2.Text == "")
            {
                lblsearch2.Visible = true;
            }
            else { lblsearch2.Visible = false; }
            MultiSearch();
        }

        public void MultiSearch()
        {
            string search1 = txtsearch.Text.Replace("'", "''");
            string search2 = txtsearch2.Text.Replace("'", "''");
            string q = "select * from  p_details p inner join e_details e on  e.ed_id = p.pd_id " +
               "inner join m_details m on m.md_id = p.pd_id  Where ( pd_fullname Like '%" + search1 + "%' OR  pd_address LIKE'%" + search1 + "%'" +
               "OR  pd_birthday LIKE'%" + search1 + "%' OR  pd_birthplace LIKE'%" + search1 + "%'" +
               "OR  pd_age LIKE'%" + search1 + "%' OR  pd_email LIKE'%" + search1 + "%'" +
               "OR  pd_contactnumber LIKE'%" + search1 + "%' OR  pd_batch LIKE'%" + search1 + "%'" +
               "OR  pd_mothername LIKE'%" + search1 + "%' OR  pd_motheroccupation LIKE'%" + search1 + "%'" +
               "OR  pd_fathername LIKE'%" + search1 + "%' OR  pd_fatheroccupation LIKE'%" + search1 + "%'" +
               "OR  pd_contactnumbermother LIKE'%" + search1 + "%' OR  pd_contactnumberfather LIKE'%" + search1 + "%'" +
               "OR  ed_primary_school LIKE'%" + search1 + "%' OR  ed_primary_year LIKE'%" + search1 + "%'" +
               "OR  ed_secondary_school LIKE'%" + search1 + "%' OR  ed_secondary_year LIKE'%" + search1 + "%'" +
               "OR  ed_tertiary_school LIKE'%" + search1 + "%' OR  ed_tertiary_year LIKE'%" + search1 + "%'" +
               "OR  ed_tertiary_course LIKE'%" + search1 + "%' OR  ed_vocational_school LIKE'%" + search1 + "%'" +
               "OR  ed_vocational_year LIKE'%" + search1 + "%' OR  ed_vocational_course LIKE'%" + search1 + "%'" +
               "OR  ed_honors LIKE'%" + search1 + "%' OR  md_rank LIKE'%" + search1 + "%'" +
               "OR  md_position LIKE'%" + search1 + "%' OR  md_dateofinvest LIKE'%" + search1 + "%'" +
               "OR  md_dateofpromote LIKE'%" + search1 + "%' OR  md_otherministries LIKE'%" + search1 + "%'" +
               "OR  md_awards LIKE'%" + search1 + "%' OR  md_violations LIKE'%" + search1 + "%'" +
               "OR  md_sacraments LIKE'%" + search1 + "%' OR  md_status LIKE'" + search1 + "%' " ;
            string query;
            if (txtsearch2.Text != "" || txtsearch2.Text != " ")
            {
                if (cmbcategorysearch.Text == "Parents Name" || cmbcategorysearch.Text == "Parents Occupation" ||
                    cmbcategorysearch.Text == "Parents Contact Number" || cmbcategorysearch.Text == "Primary Educational Details" ||
                    cmbcategorysearch.Text == "Secondary Educational Details")
                {                  
                    query = q +
                              ") AND (" + combosearchvalue.Split('-')[0] + " LIKE'%" + search2 + "%' OR " +
                              combosearchvalue.Split('-')[1] + " LIKE'%" + search2+ "%') AND m.md_retrieve ='NO' ORDER BY pd_lastname ASC;";
                }
                else if (cmbcategorysearch.Text == "Tertiary Educational Details" || cmbcategorysearch.Text == "Vocational Educational Details")
                {
                    query = q +
                            ") AND (" + combosearchvalue.Split('-')[0] + " LIKE'%" + search2 + "%' OR " +
                                        combosearchvalue.Split('-')[1] + " LIKE'%" + search2 + "%' OR " +
                                        combosearchvalue.Split('-')[2] + " LIKE'%" + search2 + "%') AND m.md_retrieve ='NO' ORDER BY pd_lastname ASC;";
                }
                else
                {
                    query = q +
                              ") AND " + combosearchvalue + " LIKE'%" + search2 + "%' AND m.md_retrieve ='NO' ORDER BY pd_lastname ASC;";
                }
            }
            else
            {
                string p = q + " AND m.md_retrieve ='NO' ORDER BY pd_lastname ASC;";
                query = p; 
            }

            AllDetails(query);
        }
        private void cmbcategorysearch_SelectedValueChanged(object sender, EventArgs e)
        {

            if ((cmbcategorysearch.Text == "") || (cmbcategorysearch.Text == "None"))
            {
                txtsearch2.Text = "";
                txtsearch2.Enabled = false;
                lblsearch2.BackColor = Color.WhiteSmoke;
                lblcmbsearch.Visible = true;
            }
            else
            {
                lblcmbsearch.Visible = false;
                lblsearch2.BackColor = Color.White;
                txtsearch2.Enabled = true;
                ComboboxSearchLoad();
            }
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
                itm.SubItems.Add(rd["pd_fullname"].ToString().Replace("_"," "));
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

        //Move, MX, MY;


        private void MasterList_Load(object sender, EventArgs e)
        {
        }
        private void Header_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Header_MouseUp(object sender, MouseEventArgs e)
        { //TMove = 0;
        }

        private void Header_MouseDown(object sender, MouseEventArgs e)
        { //TMove = 1; 
        }

        private void btnmasterlist_MouseLeave(object sender, EventArgs e)
        {
            lblstatus.Text = "";
            btnmasterlist.BorderColor = Color.Green;
            btnmasterlist.BackColor = Color.SeaGreen;

        }

        Add_Members am = new Add_Members();
        private void btnaddingmember_Click(object sender, EventArgs e)
        {
            am.lblheader.Location = new Point(916, 18);
            am.lblheader.Text = "Adding Member's";
            am.ShowDialog();
        }

        private void close_Click(object sender, EventArgs e)
        { this.Close(); }

        private void minimize_Click(object sender, EventArgs e)
        { this.WindowState = FormWindowState.Minimized; }

        private void btnadd_Click(object sender, EventArgs e)
        {
            am.lblheader.Location = new Point(916, 18);
            am.WindowState = this.WindowState;
            am.lblheader.Text = "Adding Member's";
            Add_Members.pid = null;
            am.ShowDialog();
        }
        public void UpdateFunction()
        {
            if (pid != null)
            {
                am.lblheader.Text = "Updating Member's";
                Add_Members.pid = pid;
                am.WindowState = this.WindowState;
                am.lblheader.Location = new Point(901, 18);
                am.ShowDialog();
                pid = null;
            }
            else
            {
                timer1.Start();
                buttom.BackColor = Color.MediumVioletRed;
                lblwarning.Text = "Please select Member before you Update !.";
                lblwarning.BackColor = Color.MediumVioletRed;
                icowarning.BackgroundImage = imageList1.Images[8];
                icowarning.BackColor = Color.MediumVioletRed;
                lblwarning.Visible = true;
                icowarning.Visible = true;

            }
        }
        private void btnupdate_Click(object sender, EventArgs e)
        {
            UpdateFunction();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (pid != null)
            { 
                ExitForm ef = new ExitForm();
                ef.lbltxt.Text = "Do you want to delete this member?";
                ef.lbltxt.Location = new Point(70, 25);
                ef.userretrieve = true;
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
                icowarning.BackgroundImage = imageList1.Images[8];
                icowarning.BackColor = Color.Red;
                lblwarning.Visible = true;
                icowarning.Visible = true;
            }

        }
        private void txtsearch2_Enter(object sender, EventArgs e)
        {
            lblsearch2.Visible = false;
        }

        private void lblsearch2_Click_1(object sender, EventArgs e)
        {
            txtsearch2.Focus();
            lblsearch2.Visible = false;
        }


        private void lblsearch2_Click(object sender, EventArgs e)
        {
            txtsearch2.Focus();
            lblsearch2.Visible = false;
        }
        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            if (txtsearch.Text == "")
            {
                groupBox3.Visible = false;
                lblsearch.Visible = true;
                lblcmbsearch.BackColor = Color.WhiteSmoke;
                cmbcategorysearch.Enabled = false;
                txtsearch2.Text = "";
                cmbcategorysearch.Text = "None";
                headmasterlist.Location = new Point(986, 135);
                lblmasterlist.Location = new Point(1127, 135);
                lstmasterlist.Location = new Point(990, 160);
                lstmasterlist.Size = new Size(355, 490);
            }
            else 
            {
                headmasterlist.Location = new Point(986, 167);
                lblmasterlist.Location = new Point(1127, 168);
                lstmasterlist.Location = new Point(990, 190);
                lstmasterlist.Size = new Size(355, 460);
                groupBox3.Visible = true;
                lblsearch.Visible = false;
                lblcmbsearch.BackColor = Color.White;
                cmbcategorysearch.Enabled = true;
            }

            string search = txtsearch.Text.Replace("'", "''").Replace(" ","_");
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
                "OR  md_sacraments LIKE'%" + search + "%' OR  md_status LIKE'" + search + "%' " + " ) AND m.md_retrieve ='NO' ORDER BY pd_lastname ASC";
            AllDetails(searchval);
        }

        private void lblsearch_Click(object sender, EventArgs e)
        {
            txtsearch.Focus();
            lblsearch.Visible = false;
        }

        private void txtsearch_Enter(object sender, EventArgs e)
        { lblsearch.Visible = false; }

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

        public void reset()
        {
            lblfullname.Text = "Name : ";
            lbladdress.Text = "Address : ";
            lblbirthdaybirthplace.Text = "Birthday and Birthplace : ";
            lblage.Text = "Age : ";
            lblemail.Text = "Email : " ;
            lblcontactnumber.Text = "Contact Number : ";
            lblbatch.Text = "Batch and Class : " ;
            picturebox.ImageLocation = null;
            lblmothername.Text = "Mother Name : ";
            lblmotheroccupation.Text = "Occupation : ";
            lblfathername.Text = "Father Name : " ;
            lblfatheroccupation.Text = "Occupation : ";
            lblparentscontactnumber.Text = "Parents Contact Number : ";
            lblprimaryschool.Text = "Primary School : " ;
            lblprimaryyear.Text = "Year : " ;
            lblsecondaryschool.Text = "Secondary School : ";
            lblsecondaryyear.Text = "Year : ";
            lbltertiaryschool.Text = "Tertiary School : ";
            lbltertiaryyear.Text = "Year : " ;
            lbltertiarycourse.Text = "Course : ";
            lblvocationalschool.Text = "Vocational School : " ;
            lblvocationalyear.Text = "Year : ";
            lblvocationalcourse.Text = "Course : ";
            lblrank.Text = "Rank : ";
            lblposition.Text = "Position : ";
            lblmstatus.Text = "Status : ";
            lbldateofinvestiture.Text = "Date of Investiture : ";
            lbldateofpromotion.Text = "Date of Promotion : " ;

            lsthonors.Items.Clear();
            lstministries.Items.Clear();
            lstawards.Items.Clear();
            lstviolations.Items.Clear();
            lstsacraments.Items.Clear();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (isUpdated)
            {
                lstmasterlist.Items.Clear();
                ListviewThrow();
                isUpdated = false;
            }
            if (isDelete)
            {
                timer1.Enabled = true;
                timer1.Start();
                lstmasterlist.Items.Clear();
                ListviewThrow();
                reset();
                isDelete = false;
            }
        }
        private void btnprint_Click(object sender, EventArgs e)
        {
            Printing_Masterlist pm = new Printing_Masterlist();
            pm.ifpresident = 1;
            pm.WindowState = FormWindowState.Maximized;
            pm.ShowDialog();
        }

        private void lstmasterlist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                UpdateFunction();
        }

        private void btnclearsearch_Click(object sender, EventArgs e)
        {
            reset();
            txtsearch2.Text = "";
            cmbcategorysearch.Text = "None";
            txtsearch.Text = "";
        }

        private void btnsecretary_Click(object sender, EventArgs e)
        {
            Retrieve_Member rm = new Retrieve_Member();
            rm.Location = this.Location;
            rm.WindowState = FormWindowState.Maximized;
            rm.ShowDialog();
        }

        private void btnsecretary_MouseEnter(object sender, EventArgs e)
        {
            lblstatus.Text = "Retrieve Member";
            btnretrieve.BorderColor = Color.White;
            btnretrieve.BackColor = Color.White;
        }

        private void btnsecretary_MouseLeave(object sender, EventArgs e)
        {
            lblstatus.Text = " ";
            btnretrieve.BorderColor = Color.Green;
            btnretrieve.BackColor = Color.SeaGreen;
        }

        private void cmbcategorysearch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblheader_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar)  && (e.KeyChar != ' ' ))
            {
                e.Handled = true;

            }
        }

    }
}

