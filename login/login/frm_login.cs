using System;
using Microsoft.VisualBasic;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace login
{
    public partial class frm_login : Form
    {
        public frm_login()
        {
            InitializeComponent();
        }

        SqlConnection sql_con = new SqlConnection(@"Data Source =.\SQLEXPRESS; Initial Catalog = login_app1; Integrated Security = True"); 

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Strings.Len(Strings.Trim(txtName.Text)) == 0)
            {
                Bunifu.Snackbar.Show(this, "Invalid Username", 3000, Snackbar.Views.SnackbarDesigner.MessageTypes.Warning);
                txtName.Focus();
                return;
            }
            if (Strings.Len(Strings.Trim(txtPassword.Text)) == 0)
            {
                Bunifu.Snackbar.Show(this, "Invalid Password", 3000, Snackbar.Views.SnackbarDesigner.MessageTypes.Warning);
                txtPassword.Focus();
                return;
            }

            try
            {
                sql_con.Open();

                string query = "SELECT* FROM user_info WHERE name_user = '" + txtName.Text.Trim() + "' AND password_user = '" + txtPassword.Text.Trim() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, sql_con);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                if (dtbl.Rows.Count == 1)
                {
                    this.Hide();
                    frm_main obj = new frm_main();
                    obj.Show();
                }
                else
                {
                    Bunifu.Snackbar.Show(this, "Check your username and password!", 3000, Snackbar.Views.SnackbarDesigner.MessageTypes.Error);
                }
                sql_con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                if (sql_con != null && sql_con.State == ConnectionState.Open)
                {
                    sql_con.Close();
                    sql_con.Dispose();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
