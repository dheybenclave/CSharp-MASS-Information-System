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

namespace Sons_of_San_Jose
{
    public partial class ExitForm : Form
    {
        public string pid, imgdelete;
        public bool userretrieve, ifrestore, ifdelete = false;
   
        DataTable table = new DataTable();
        MySqlDataAdapter adapt;
        MySqlCommand cmd;
        Db_Utilities db = new Db_Utilities();

        public ExitForm()
        {
            InitializeComponent();
        }
          
        private void btnno_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnyes_Click(object sender, EventArgs e)
        {

            try
            {
                if (pid != null)
                {
                    if (userretrieve == true)
                    {
                        string query = "UPDATE `dbms_mass`.`m_details` SET `md_retrieve`='YES' WHERE `md_id`='" + pid + "' ;";
                        cmd = new MySqlCommand(query, db.OpenConnection());
                        cmd.ExecuteNonQuery();
                        db.CloseConnection();
                        MasterList.isUpdated = MasterList.isDelete = true;
                        this.Close();
                    }
                    else { }

                    if (ifdelete == true)
                    {
                        Retrieve_Member.ifrestore = false;
                        cmd = new MySqlCommand("DeleteAll", db.OpenConnection());
                        cmd.CommandType = CommandType.StoredProcedure;
                        try
                        {
                            cmd.Parameters.Add(new MySqlParameter("?pid", pid));
                            File.Delete(Application.StartupPath + "\\Pictures\\" + imgdelete);
                        }
                        catch { }
                        cmd.ExecuteNonQuery();
                        db.CloseConnection();
                         Retrieve_Member.isDelete = MasterList.isUpdated = true;
                        this.Close();
                    }
                    else {}
                    
                    if (ifrestore == true)
                    {

                        string query = "UPDATE `dbms_mass`.`m_details` SET `md_retrieve`='NO' WHERE `md_id`='" + pid + "' ;";
                        cmd = new MySqlCommand(query, db.OpenConnection());
                        cmd.ExecuteNonQuery();
                        db.CloseConnection();
                        Retrieve_Member.ifrestore = true;
                  
                        this.Close();
                    }
                    else { }
                }
                else
                { Application.ExitThread(); userretrieve = ifrestore = false; }
            }
            catch { userretrieve = ifrestore = false; }
        }

        private void ExitForm_Load(object sender, EventArgs e)
        {

        }

        private void panel_Click(object sender, EventArgs e)
        {

        }
    }
}
