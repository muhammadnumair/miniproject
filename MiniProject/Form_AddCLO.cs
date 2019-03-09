using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MiniProject
{
    public partial class Form_AddCLO : Form
    {
        int Id;
        public Form_AddCLO()
        {
            InitializeComponent();
        }

        public Form_AddCLO(string[] values)
        {
            InitializeComponent();
            Id = Convert.ToInt32(values[0]);
            text_title.Text = values[1];
            add_button.Text = "Update Record";
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
            conn.Open();

            if (add_button.Text == "Add CLO")
            {
                string query = "Insert into Clo (Name, DateCreated, DateUpdated) values('" + text_title.Text + "', '" + DateTime.Today + "', '" + DateTime.Today + "')";
                SqlCommand command = new SqlCommand(query, conn);
                int i = command.ExecuteNonQuery();
                if (i != 0)
                {
                    MessageBox.Show("CLO Record Inserted Successfully");
                }
                conn.Close();
            }
            else
            {
                string query = "Update Clo SET Name = '"+text_title.Text+"', DateUpdated = '"+DateTime.Today+ "' where Id = '" + Id + "'";
                SqlCommand command = new SqlCommand(query, conn);
                int i = command.ExecuteNonQuery();
                if (i != 0)
                {
                    MessageBox.Show("CLO Record Updated Successfully");
                }
                conn.Close();
            }
            foreach (Form f in Application.OpenForms)
                f.Hide();

            Form_CLO s = new Form_CLO();
            s.Show();
        }
    }
}
