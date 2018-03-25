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
    public partial class Create_Account : Form
    {
        public static bool isCorrect = false;
        Db_Utilities db = new Db_Utilities();
        MySqlDataAdapter adapt;
        DataTable table = new DataTable();
        MySqlCommand cmd = new MySqlCommand();
        public Create_Account()
        {
            InitializeComponent();
            groupBox1.Enabled = false;
            Position();

        }

        private void rectangleShape2_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void rectangleShape2_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void rectangleShape2_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
           
            this.Close();
            Login_form.ActiveForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "on")
            {
                button2.Text = "off";
                txtpass.UseSystemPasswordChar = false;
            }
            else
            {
                button2.Text = "on";
                txtpass.UseSystemPasswordChar = true;
            }
        }

        private void btnaccept_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
        }
        public void Position()
        {
            cmbposition.Items.Clear();
            string[] position = { "Coordinator", "Co-Coordinator", "Secretary", "Treasurer", "Commitee" };
            List<string> dataposition = new List<string>();
            adapt = new MySqlDataAdapter("SELECT user_position FROM dbms_mass.user ;", db.OpenConnection());
            adapt.Fill(table);
            foreach (DataRow r in table.Rows)
            {dataposition.Add(r[0].ToString());}
            foreach (string s in position)
            {
                if (!dataposition.Contains(s))
                    cmbposition.Items.Add(s);
            }

        }

        private void Create_Account_Load(object sender, EventArgs e)
        {
            Point p = new Point();
            p.X = (Screen.PrimaryScreen.Bounds.Width / 2) - (this.Size.Width / 2);
            p.Y = (Screen.PrimaryScreen.Bounds.Height / 2) - (this.Size.Height / 2);
            this.Location = p;
        }

        private void btncreate_Click(object sender, EventArgs e)
        {
            if (txtname.Text != "" && txtpass.Text != "" && cmbposition.Text != "")
            {
                cmd = new MySqlCommand("CreateUser", db.OpenConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    cmd.Parameters.Add(new MySqlParameter("?user_name", txtname.Text.Replace("'","''")));
                    cmd.Parameters.Add(new MySqlParameter("?user_password", txtpass.Text.Replace("'", "''")));
                    cmd.Parameters.Add(new MySqlParameter("?user_position", cmbposition.Text));
                    cmd.ExecuteNonQuery();
                    db.CloseConnection();
                    Position();
                    lblnotification.Visible = true;
                    lblnotification.Text = "Create user Success! .";
                    cmbposition.Text = "";
                }
                catch { }

               
            }
            else
            {
                lblnotification.Visible = true;
                lblnotification.Text = "Please Fill All Details! .";
            }

        }

        private void txtname_TextChanged(object sender, EventArgs e)
        { lblnotification.Visible = false; }

        private void txtpass_TextChanged(object sender, EventArgs e)
        { lblnotification.Visible = false; }

        private void cmbposition_SelectedValueChanged(object sender, EventArgs e)
        { lblnotification.Visible = false; }

        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && (e.KeyChar != ' '))
            {
                e.Handled = true;

            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }
    }
}
