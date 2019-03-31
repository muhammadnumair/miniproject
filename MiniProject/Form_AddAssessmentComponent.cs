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
        string RubricId;
        string CloId;
        string CloName;
        string RubricName;
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
            RubricId = Convert.ToString(values[4]);
            CloId = Convert.ToString(values[5]);
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
                    item.Value = Convert.ToString(reader["Id"]);
                    if(CloId != null)
                    {
                        if(CloId == Convert.ToString(reader["Id"]))
                        {
                            CloName = Convert.ToString(reader["Name"]);
                        }
                    }
                    combo_clo.Items.Add(item);

                    //combo_clo.SelectedIndex = 0;
                }
            }
            if (CloId != null)
            {
                combo_clo.SelectedIndex = combo_clo.FindStringExact(CloName);
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
                    if (RubricId != null)
                    {
                        if (RubricId == Convert.ToString(reader["Id"]))
                        {
                            RubricName = Convert.ToString(reader["Details"]);
                        }
                    }
                    comboc_rubric.Items.Add(item);

                    //combo_clo.SelectedIndex = 0;
                }
            }

            if(RubricId != null)
            {
                comboc_rubric.SelectedIndex = comboc_rubric.FindStringExact(RubricName);
            }
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            if(text_marks.Text != "" && text_name.Text != "" && combo_clo.Text != "" && comboc_rubric.Text != "" && text_marks.Text.All(c => char.IsDigit(c)))
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
                    string query = "Update AssessmentComponent SET Name = '" + text_name.Text + "', TotalMarks = '" + text_marks.Text + "', RubricId = '" + (comboc_rubric.SelectedItem as ComboboxItem).Value.ToString() + "', DateUpdated = '"+DateTime.Now+"' where Id = '" + Id + "'";
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
                if (!text_marks.Text.All(c => char.IsDigit(c)))
                {
                    error_msg.Text = "Marks must be a numeric value";
                }
                else
                {
                    error_msg.Text = "Please fill in all the required fields";
                }
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
