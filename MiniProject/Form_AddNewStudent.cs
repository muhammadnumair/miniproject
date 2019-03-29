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
using System.Text.RegularExpressions;

namespace MiniProject
{
    public partial class Form_AddNewStudent : Form
    {
        int Id;
        public Form_AddNewStudent()
        {
            InitializeComponent();
        }

        public Form_AddNewStudent(string[] values)
        {
            InitializeComponent();
            Id = Convert.ToInt32(values[0]);
            text_fname.Text = values[1];
            text_lname.Text = values[2];
            text_cnumber.Text = values[3];
            text_email.Text = values[4];
            text_rnumber.Text = values[5];
            if (values[6] == "5")
            {
                combo_status.SelectedIndex = combo_status.FindStringExact("Active");
            }else if (values[6] == "6")
            {
                combo_status.SelectedIndex = combo_status.FindStringExact("InActive");
            }
            add_button.Text = "Update Record";
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (text_email.Text != "" && text_fname.Text != "" && text_lname.Text != "" && text_cnumber.Text != "" && text_rnumber.Text != "" && combo_status.Text != "" && combo_status.Text != "Select Status" && reg.IsMatch(text_email.Text) && text_cnumber.Text.All(c => char.IsDigit(c)))
            {
                SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
                conn.Open();
                string query1 = "Select Top(1) * from Lookup where Name = '" + combo_status.Text + "'";
                SqlCommand command = new SqlCommand(query1, conn);
                int LookupId = 0;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Send these to your WinForms textboxes
                        LookupId = Convert.ToInt32(reader["LookupId"]);
                        break;
                    }
                }

                if (add_button.Text == "Add Student")
                {
                    string query = "Insert into Student (FirstName, LastName, Contact, Email, RegistrationNumber, Status) values('" + text_fname.Text + "', '" + text_lname.Text + "', '" + text_cnumber.Text + "', '" + text_email.Text + "', '" + text_rnumber.Text + "', '" + LookupId + "')";
                    command = new SqlCommand(query, conn);
                    int i = command.ExecuteNonQuery();
                    if (i != 0)
                    {
                        MessageBox.Show("Student Record Inserted Successfully");
                    }
                    conn.Close();
                }
                else
                {
                    string query = "Update Student SET FirstName = '" + text_fname.Text + "', LastName = '" + text_lname.Text + "', Contact = '" + text_cnumber.Text + "', Email = '" + text_email.Text + "', RegistrationNumber = '" + text_rnumber.Text + "', Status = '" + LookupId + "' where Id = '" + Id + "'";
                    command = new SqlCommand(query, conn);
                    int i = command.ExecuteNonQuery();
                    if (i != 0)
                    {
                        MessageBox.Show("Student Record Updated Successfully");
                    }
                    conn.Close();
                }
                foreach (Form f in Application.OpenForms)
                    f.Hide();

                Form_Students s = new Form_Students();
                s.Show();
            }
            else
            {
                if (!text_cnumber.Text.All(c => char.IsDigit(c) && text_cnumber.Text != ""))
                {
                    error_msg.Text = "Contact Number Must Be Digits";
                }
                else if (!reg.IsMatch(text_email.Text) && text_email.Text != "") {
                    error_msg.Text = "Email is not in correct format";
                }
                else
                {
                    error_msg.Text = "Please Fill In All The Required Fields";
                }
                error_msg.Show();
            }
        }

        private void Form_AddNewStudent_Load(object sender, EventArgs e)
        {
            error_msg.Hide();
        }

        private void text_fname_TextChanged(object sender, EventArgs e)
        {
            error_msg.Hide();
        }

        private void text_lname_TextChanged(object sender, EventArgs e)
        {
            error_msg.Hide();
        }

        private void text_cnumber_TextChanged(object sender, EventArgs e)
        {
            error_msg.Hide();
        }

        private void text_email_TextChanged(object sender, EventArgs e)
        {
            error_msg.Hide();
        }

        private void text_rnumber_TextChanged(object sender, EventArgs e)
        {
            error_msg.Hide();
        }

        private void combo_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            error_msg.Hide();
        }
    }
}
