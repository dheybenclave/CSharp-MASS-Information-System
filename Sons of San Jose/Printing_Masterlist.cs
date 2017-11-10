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
    public partial class Printing_Masterlist : Form
    {
        Point p = new Point();
        Db_Utilities db = new Db_Utilities();
        public int ifpresident = 0;
        public bool istrue = true;
        public bool istrueagain = true;
        public bool istruecount = true;
        public bool counttrue = false;
        MySqlDataAdapter adapt;
        string filename;
        public Printing_Masterlist()
        {
            InitializeComponent();
            picministrylogo.BackgroundImage = picministrylogo1.BackgroundImage = picministrylogo2.BackgroundImage = imageList1.Images[0];
            picsanjoselogo.BackgroundImage = picsanjoselogo1.BackgroundImage = picsanjoselogo2.BackgroundImage = imageList1.Images[1];
            btnprint.BackgroundImage = imageList1.Images[2];
            ComboBoxDate();

            p.X = (Screen.PrimaryScreen.Bounds.Width / 2) - (this.Size.Width / 2);
            p.Y = (Screen.PrimaryScreen.Bounds.Height / 2) - (this.Size.Height / 2);
            this.Location = p;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void PrintMasterlist()
        {

            Excel._Application e_excel;
            Excel.Workbook e_worlbook;
            Excel._Worksheet e_worksheet;
            object e_missing = Missing.Value;
            adapt = new MySqlDataAdapter("SELECT p.pd_fullname, m.md_status FROM p_details p inner join m_details m "+
                                         " on p.pd_id= m.md_id WHERE m.md_status ='Active' AND m.md_retrieve ='NO' ORDER BY pd_lastname ASC;", db.OpenConnection());
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            db.CloseConnection();

            if (dt.Rows.Count > 0)
            {
                try

                {
                    if (lbldate.Text == "Date Submitted")
                    { lbldate.Text = DateTime.Now.Date.ToLongDateString(); }
                    else { lbldate.Text = lbldate.Text; }

                    e_excel = new Excel.Application();
                    e_excel.Visible = true;
                    e_worlbook = e_excel.Workbooks._Open(Application.StartupPath + "\\Reports\\masterlistreportnew", e_missing, e_missing);
                    e_worksheet = (Excel.Worksheet)e_worlbook.ActiveSheet;
                    e_worksheet.Cells[8, 2] = lbldate.Text;
                    int rows = 25;
                    int countnumber = 1;
                    int counternextpage = 1;
                    int columns = 2;
                    int rr = 25;
                    int half = 0;
                    int halfless = dt.Rows.Count / 2 + 1;
                    foreach (DataRow r in dt.Rows)
                    {
                        if (dt.Rows.Count > 38)
                        {
                            if (e_worksheet.Cells.get_Item(43, 2).Value != null)
                            {
                                columns = 3; if (istrue == true) { rows = 25; istrue = false; } else { }
                            }
                            else { }

                            if (e_worksheet.Cells.get_Item(43, 3).Value != null)
                            {
                                columns = 2; if (istrueagain == true) { counttrue = true; rows = 44; istrueagain = false; }
                            }
                            else { }

                            half = (dt.Rows.Count - 38) / 2 + 1;
                            if (counternextpage >= half)
                            {
                                columns = 3; if (istruecount == true) { rows = 44; istruecount = false; }
                            }
                            else { }
                        }
                        else if (dt.Rows.Count <= 38)
                        {
                            if (countnumber == halfless)
                            {
                                rows = 25;
                                columns = 3;
                            }
                        }
                        
                        e_worksheet.Cells[rows, columns] = countnumber++ + ". " + r[0].ToString().Replace("_", " ");
                        rows++;
                        if (counttrue == true) { counternextpage++; }

                    }

                    int lastrows = rows+2;
                    int counter = 1;
                    e_worksheet.Cells[15, 2].Value = "From                           :          " + lblcoordinator.Text ;
                    e_worksheet.Cells[22, 2].Value = "Malugod pong ipinababatid sa lahat ang bagong listahan ng mga pangalan ng "+dt.Rows.Count+" na ";
                    e_worksheet.Cells[lastrows + counter++, 2].Value = "Ang mga pangalan sa itaas ay binibigyan ng mga karapatan, pribilehiyo, tungkulin ";
                    e_worksheet.Cells[lastrows + counter++, 2].Value = "at responsibilidad na naangkop sa isang miyembro ng ministry. ";
                    e_worksheet.Cells[lastrows + counter++ + 3, 2].Value = " ";
                    e_worksheet.Cells[lastrows + counter++, 2].Value = "Ang sinumang tao na wala sa listahan ay walang pananagutan ang ministry sa ";
                    e_worksheet.Cells[lastrows + counter++, 2].Value = "anumang gawain nito.";
                    e_worksheet.Cells[lastrows + counter++ + 3, 2].Value = " ";
                    e_worksheet.Cells[lastrows + counter++, 2].Value = "Maraming salamat po.";
                    e_worksheet.Cells[lastrows + counter++ + 3, 2].Value = " ";
                    e_worksheet.Cells[lastrows + counter++, 2].Value = "Approved by.";
                    e_worksheet.Cells[lastrows + counter++, 2].Value = " ";
                    e_worksheet.Cells[lastrows + counter++, 2].Value = lblchairperson.Text;
                    e_worksheet.Cells[lastrows + counter++, 2].Value = "Chairperson, Parish Commission on Worship and Liturgy";
                    e_worksheet.Cells[lastrows + counter++ + 3, 2].Value = " ";
                    e_worksheet.Cells[lastrows + counter++, 2].Value = "Noted by:";
                    e_worksheet.Cells[lastrows + counter++ + 3, 2].Value = " ";
                    e_worksheet.Cells[lastrows + counter++, 2].Value = lblparishpriest.Text;
                    e_worksheet.Cells[lastrows + counter++, 2].Value = "Parish Priest";
                   // e_worksheet.Cells.get_Item(i, 2).Value;
                    filename = "masterlistreportnew" + DateTime.Now.Second + DateTime.Now.Date.Month + DateTime.Now.Day + DateTime.Now.Year ;
                 //   e_worlbook.SaveAs(Application.StartupPath + "\\Reports\\" +filename+ ".xlsx", e_missing, e_missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange);
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }
            }
        }
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
               // HasDate.Add(Convert.ToDateTime(dr[0].ToString()).ToString("yyyy-MM-dd"));
                cmbdate.Items.Add(Convert.ToDateTime(dr[0].ToString()).ToString("yyyy-MM-dd"));
            }
            try
            {
                cmbdate.Text = cmbdate.Items[0].ToString();
            }
            catch
            {
                // MessageBox.Show("There is no previous data for attendance!");
            }
        }

        public void PrintAttendance()
        {

            Excel._Application e_excel;
            Excel.Workbook e_worlbook;
            Excel._Worksheet e_worksheet;
            object e_missing = Missing.Value;
            string q = "SELECT p.pd_fullname, m.md_status FROM dbms_mass.attendance ad inner join p_details p ON " +
                            "p.pd_id = ad.pd_id inner join m_details m on p.pd_id= m.md_id WHERE ad.ad_date = '" + cmbdate.Text +
                             "' AND  m.md_status ='Active' AND m.md_retrieve ='NO' ORDER BY pd_lastname ASC;";

            adapt = new MySqlDataAdapter(q, db.OpenConnection());
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            db.CloseConnection();


            if (dt.Rows.Count > 0)
            {
                try
                {
                    e_excel = new Excel.Application();
                    e_excel.Visible = true;
                    e_worlbook = e_excel.Workbooks._Open(Application.StartupPath + "\\Reports\\attendancereport", e_missing, e_missing);
                    e_worksheet = (Microsoft.Office.Interop.Excel.Worksheet)e_worlbook.ActiveSheet;
                    int rows = 15;
                    int countnumber = 1;
                    int counternextpage = 1;
                    int columns = 2;
                    int halfless = dt.Rows.Count / 2 + 1;
                    e_worksheet.Cells[12, 2].Value = Convert.ToDateTime(lbldateattendance.Text).ToLongDateString();
                    e_worksheet.Cells[13, 2].Value = "PRESENT";

                    foreach (DataRow r in dt.Rows)
                    {
                       

                        if (dt.Rows.Count > 38)
                        {
                            if (e_worksheet.Cells.get_Item(43, 2).Value != null)
                            {
                                columns = 3; if (istrue == true) { rows = 15; istrue = false; } else { }
                            }
                            else { }
                        }
                        else if (dt.Rows.Count <= 38)
                        {
                            if (countnumber == halfless)
                            {
                                rows = 15;
                                columns = 3;
                            }
                        }

                        e_worksheet.Cells[rows, columns] = countnumber++ + ". " + r[0].ToString().Replace("_", " ");
                        rows++;
                        if (counttrue == true) { counternextpage++; }

                    }

                    int lastrows = rows + 2;
                    int counter = 1;
                    e_worksheet.Cells[lastrows + counter++, 2].Value = "";
                    e_worksheet.Cells[lastrows + counter++, 2].Value = "Approved by.";
                    e_worksheet.Cells[lastrows + counter++ + 3, 2].Value = " ";
                    e_worksheet.Cells[lastrows + counter++, 2].Value = lblcoordinatorattendance.Text.ToUpper();
                    e_worksheet.Cells[lastrows + counter++, 2].Value = "Coordinator, Ministry of Altar server";
                    e_worksheet.Cells[lastrows + counter++ + 3, 2].Value = " ";
                    e_worksheet.Cells[lastrows + counter++, 2].Value = lblsecretary.Text.ToUpper();
                    e_worksheet.Cells[lastrows + counter++, 2].Value = "Secretary, Ministry of Altar server";
                    // e_worksheet.Cells.get_Item(i, 2).Value;
                    filename = "attendancereport" + DateTime.Now.Second + DateTime.Now.Date.Month + DateTime.Now.Day + DateTime.Now.Year;
                  //  e_worlbook.SaveAs(Application.StartupPath + "\\Reports\\" + filename + ".xlsx", e_missing, e_missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange);
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }
            }
        }

        public void PrintTreasurer()
        {
            Excel._Application e_excel;
            Excel.Workbook e_worlbook;
            Excel._Worksheet e_worksheet;
            object e_missing = Missing.Value;

            if (lbltreasurer.Text == "Date Submitted")
            { lbltreasurer.Text = DateTime.Now.Date.ToLongDateString(); }
            else { lbltreasurer.Text = lbltreasurer.Text.ToUpper(); }

            e_excel = new Excel.Application();
            e_excel.Visible = true;
            e_worlbook = e_excel.Workbooks._Open(Application.StartupPath + "\\Reports\\treasurerreport", e_missing, e_missing);
            e_worksheet = (Excel.Worksheet)e_worlbook.ActiveSheet;
            e_worksheet.Cells[8, 2] = lbldatetreasurer.Text;

            e_worksheet.Cells[15, 2].Value = "From                           :       " + lbltreasurer.Text.ToUpper();
            e_worksheet.Cells[23, 2].Value = "o ministry ay humigit kumulang na Php "+Convert.ToDouble(txtbudget.Text)+"  sa dahilang  pangkaraniwang ";
            e_worksheet.Cells[41, 2].Value = "Sir. " +lblcoordinatortreasurer.Text.ToUpper();
            e_worksheet.Cells[41, 3].Value = "       " + lbltreasurer.Text.ToUpper();
        }
        private void btnprint_Click(object sender, EventArgs e)
        {
            if (tabcontrolprint.SelectedTab == tbmasterlist)
            {
                if (txtchairperson.Text == "" || txtcoordinator.Text == "" || txtparishpriest.Text == "")
                {
                    timer1.Start();
                    timer1.Enabled = true;
                    buttom.BackColor = Color.Tomato;
                    lblwarning.BackColor = Color.Tomato;
                    icowarning.BackgroundImage = imageList1.Images[3];
                    icowarning.BackColor = Color.Tomato;
                    lblwarning.Visible = true;
                    icowarning.Visible = true;

                }
                else
                { PrintMasterlist(); }
            }
            else if (tabcontrolprint.SelectedTab == tbattendance)
            {
                if (txtsecretary.Text == "" || txtcoordinatorattendance.Text == "")
                {
                    timer1.Start();
                    timer1.Enabled = true;
                    buttom.BackColor = Color.Tomato;
                    lblwarning.BackColor = Color.Tomato;
                    icowarning.BackgroundImage = imageList1.Images[3];
                    icowarning.BackColor = Color.Tomato;
                    lblwarning.Visible = true;
                    icowarning.Visible = true;

                }
                else
                { PrintAttendance(); }
            }
            else if (tabcontrolprint.SelectedTab == tbtreasurer)
            {
                if (txtbudget.Text == "" || txttreasurercoordinator.Text == "" || txttreasurer.Text == "")
                {
                    timer1.Start();
                    timer1.Enabled = true;
                    buttom.BackColor = Color.Tomato;
                    lblwarning.BackColor = Color.Tomato;
                    icowarning.BackgroundImage = imageList1.Images[3];
                    icowarning.BackColor = Color.Tomato;
                    lblwarning.Visible = true;
                    icowarning.Visible = true;

                }
                else
                { PrintTreasurer(); }
            }

        }


        private void Printing_Masterlist_Load(object sender, EventArgs e)
        {
            

        }

        private void close_Click(object sender, EventArgs e)
        {
            if (ifpresident == 1) { this.Close(); }
            else { ExitForm ex = new ExitForm(); ex.ShowDialog(); }
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtparishpriest_TextChanged(object sender, EventArgs e)
        {

            if (txtparishpriest.Text == "")
            {
                lblparishpriest.Text = "PARISH PRIEST";
            }
            else
            {
                lblparishpriest.Text = txtparishpriest.Text.ToUpper();
            }
        }

        private void txtcoordinator_TextChanged(object sender, EventArgs e)
        {
            if (txtcoordinator.Text == "")
            {
                lblcoordinator.Text = "Sir. COORDINATOR";
            }
            else { lblcoordinator.Text = "Sir. " + txtcoordinator.Text.ToUpper(); }
        }

        private void txtchairperson_TextChanged(object sender, EventArgs e)
        {
            if (txtchairperson.Text == "")
            {
                lblchairperson.Text = "CHAIRPERSON";
            }
            else { lblchairperson.Text = txtchairperson.Text.ToUpper(); }
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

        private void btnsecretary_MouseEnter(object sender, EventArgs e)
        {

        }

        private void btnsecretary_MouseLeave(object sender, EventArgs e)
        {

        }

        private void label53_Click(object sender, EventArgs e)
        {

        }

        private void dtattendance_ValueChanged(object sender, EventArgs e)
        { cmbdate.Text = lbldateattendance.Text; }

        private void txtcoordinatorattendance_TextChanged(object sender, EventArgs e)
        {
            if (txtcoordinatorattendance.Text == "") { lblcoordinatorattendance.Text = "Sir COORDINATOR"; }
            else{lblcoordinatorattendance.Text = txtcoordinatorattendance.Text.ToUpper();}
        }

        private void txtsecretary_TextChanged(object sender, EventArgs e)
        {
            if (txtsecretary.Text == "") { lblsecretary.Text = "SECRETARY"; }
            else { lblsecretary.Text = txtsecretary.Text.ToUpper(); }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (lbldate.Text == "Date Submmitted") { lbldate.Text = DateTime.Now.ToString("MMMM dd, yyyyy"); } 
            else { lbldate.Text = dtdmasterlistdate.Value.ToLongDateString(); }
        }

        private void cmbdate_SelectedValueChanged(object sender, EventArgs e)
        { lbldateattendance.Text = Convert.ToDateTime(cmbdate.Text).ToString("MMMM dd, yyyy"); }

        private void dttreasurer_ValueChanged(object sender, EventArgs e)
        {
            if (lbltreasurer.Text == "Date Submmitted") { lbltreasurer.Text = DateTime.Now.ToString("MMMM dd, yyyyy"); }
            else { lbltreasurer.Text = dttreasurer.Value.ToLongDateString(); }
        }

        private void txtbudget_TextChanged(object sender, EventArgs e)
        {
            if (txtbudget.Text == "") { lblbudget.Text = " BUDGET......"; }
            else { lblbudget.Text = txtbudget.Text.ToUpper(); }
        }

        private void txttreasurercoordinator_TextChanged(object sender, EventArgs e)
        {
            if (txttreasurercoordinator.Text == "") { lblcoordinatortreasurer.Text = "COORDINATOR"; }
            else { lblcoordinatortreasurer.Text = txttreasurercoordinator.Text.ToUpper(); }
        }

        private void txttreasurer_TextChanged(object sender, EventArgs e)
        {
            if (txttreasurer.Text == "") { lbltreasurer.Text = lbltreasurer1.Text =  "COORDINATOR"; }
            else { lbltreasurer.Text = lbltreasurer1.Text = txttreasurer.Text.ToUpper(); }
        }

        private void txtbudget_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;

            }
        }

        private void tbtreasurer_Click(object sender, EventArgs e)
        {

        }

        private void lblchairperson_Click(object sender, EventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }
    }
}
