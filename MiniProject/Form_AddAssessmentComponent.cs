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
    public partial class Form_AddAssessmentComponent : Form
    {
        int AssessmentId;
        int Id;
        public Form_AddAssessmentComponent(int id)
        {
            AssessmentId = id;
            InitializeComponent();
        }
        public Form_AddAssessmentComponent(string[] values)
        {
            InitializeComponent();
            Id = Convert.ToInt32(values[0]);
            text_name.Text = values[1];
            text_marks.Text = values[2];
            AssessmentId = Convert.ToInt32(values[3]);
            add_button.Text = "Update Record";
        }

        private void Form_AddAssessmentComponent_Load(object sender, EventArgs e)
        {
            error_msg.Hide();
            SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
            conn.Open();
            string query1 = "Select * from Clo";
            SqlCommand command = new SqlCommand(query1, conn);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = Convert.ToString(reader["Name"]);
                    item.Value = Convert.ToString(reader["Id"]); ;

                    combo_clo.Items.Add(item);

                    //combo_clo.SelectedIndex = 0;
                }
            }
        }

        private void combo_clo_SelectedIndexChanged(object sender, EventArgs e)
        {
            error_msg.Hide();
            comboc_rubric.Items.Clear();
            comboc_rubric.ResetText();
            SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
            conn.Open();
            string query1 = "Select * from Rubric where CloId = '"+ (combo_clo.SelectedItem as ComboboxItem).Value.ToString() + "'";
            SqlCommand command = new SqlCommand(query1, conn);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = Convert.ToString(reader["Details"]);
                    item.Value = Convert.ToString(reader["Id"]); ;

                    comboc_rubric.Items.Add(item);

                    //combo_clo.SelectedIndex = 0;
                }
            }
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            if(text_marks.Text != "" && text_name.Text != "" && combo_clo.Text != "" && comboc_rubric.Text != "")
            {
                SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
                conn.Open();
                SqlCommand command;
                if (add_button.Text == "Add Question")
                {
                    string query = "Insert into AssessmentComponent (Name, RubricId, TotalMarks, DateCreated, DateUpdated, AssessmentId) values('" + text_name.Text + "', '" + (comboc_rubric.SelectedItem as ComboboxItem).Value.ToString() + "', '" + text_marks.Text + "', '" + DateTime.Now + "', '" + DateTime.Now + "', '" + AssessmentId + "')";
                    command = new SqlCommand(query, conn);
                    int i = command.ExecuteNonQuery();
                    if (i != 0)
                    {
                        MessageBox.Show("Question Record Inserted Successfully");
                    }
                    conn.Close();
                }
                else
                {
                    string query = "Update Assessment SET Name = '" + text_name.Text + "', TotalMarks = '" + text_marks.Text + "', RubricId = '" + (comboc_rubric.SelectedItem as ComboboxItem).Value.ToString() + "' where Id = '" + Id + "'";
                    command = new SqlCommand(query, conn);
                    int i = command.ExecuteNonQuery();
                    if (i != 0)
                    {
                        MessageBox.Show("Question Record Updated Successfully");
                    }
                    conn.Close();
                }
                foreach (Form f in Application.OpenForms)
                    f.Hide();

                Form_AssessmentComponents s = new Form_AssessmentComponents(AssessmentId);
                s.Show();
            }
            else
            {
                error_msg.Show();
            }
        }

        private void text_name_TextChanged(object sender, EventArgs e)
        {
            error_msg.Hide();
        }

        private void text_marks_TextChanged(object sender, EventArgs e)
        {
            error_msg.Hide();
        }

        private void comboc_rubric_SelectedIndexChanged(object sender, EventArgs e)
        {
            error_msg.Hide();
        }
    }
}
