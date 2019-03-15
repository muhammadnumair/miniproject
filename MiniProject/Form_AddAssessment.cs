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
    public partial class Form_AddAssessment : Form
    {
        int Id;
        public Form_AddAssessment()
        {
            InitializeComponent();
        }

        public Form_AddAssessment(string[] values)
        {
            InitializeComponent();
            Id = Convert.ToInt32(values[0]);
            text_title.Text = values[1];
            text_marks.Text = values[2];
            text_weightage.Text = values[3];
            add_button.Text = "Update Record";
        }
        private void add_button_Click(object sender, EventArgs e)
        {
            if (text_marks.Text != "" && text_title.Text != "" && text_weightage.Text != "")
            {
                SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
                conn.Open();
                SqlCommand command;
                if (add_button.Text == "Add Assessment")
                {
                    string query = "Insert into Assessment (Title, DateCreated, TotalMarks, TotalWeightage) values('" + text_title.Text + "', '" + DateTime.Now + "', '" + text_marks.Text + "', '" + text_weightage.Text + "')";
                    command = new SqlCommand(query, conn);
                    int i = command.ExecuteNonQuery();
                    if (i != 0)
                    {
                        MessageBox.Show("Assessment Record Inserted Successfully");
                    }
                    conn.Close();
                }
                else
                {
                    string query = "Update Assessment SET Title = '" + text_title.Text + "', TotalMarks = '" + text_marks.Text + "', TotalWeightage = '" + text_weightage.Text + "' where Id = '" + Id + "'";
                    command = new SqlCommand(query, conn);
                    int i = command.ExecuteNonQuery();
                    if (i != 0)
                    {
                        MessageBox.Show("Assessment Record Updated Successfully");
                    }
                    conn.Close();
                }
                foreach (Form f in Application.OpenForms)
                    f.Hide();

                Form_Assessment s = new Form_Assessment();
                s.Show();
            }
            else
            {
                error_msg.Show();
            }
        }

        private void Form_AddAssessment_Load(object sender, EventArgs e)
        {
            error_msg.Hide();
        }

        private void text_title_TextChanged(object sender, EventArgs e)
        {
            error_msg.Hide();
        }

        private void text_marks_TextChanged(object sender, EventArgs e)
        {
            error_msg.Hide();
        }

        private void text_weightage_TextChanged(object sender, EventArgs e)
        {
            error_msg.Hide();
        }
    }
}
