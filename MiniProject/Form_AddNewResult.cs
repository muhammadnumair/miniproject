using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniProject
{
    public partial class Form_AddNewResult : Form
    {
        string StudentId;
        string StudentReg;
        string AssessmentId;
        string AssessmentTitle;
        string ACId;
        string ACName;
        string RubricId;
        string RubricName;
        SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
        int Id;

        public Form_AddNewResult()
        {
            InitializeComponent();
        }

        public Form_AddNewResult(string[] values)
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void Form_AddNewResult_Load(object sender, EventArgs e)
        {
            error_msg.Hide();
            SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
            conn.Open();
            string query1 = "Select * from Student";
            SqlCommand command = new SqlCommand(query1, conn);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = Convert.ToString(reader["RegistrationNumber"]);
                    item.Value = Convert.ToString(reader["Id"]);
                    if (StudentId != null)
                    {
                        if (StudentId == Convert.ToString(reader["Id"]))
                        {
                            StudentReg = Convert.ToString(reader["RegistrationNumber"]);
                        }
                    }
                    combo_student.Items.Add(item);

                    //combo_clo.SelectedIndex = 0;
                }
            }
            if (StudentId != null)
            {
                combo_assessment.SelectedIndex = combo_assessment.FindStringExact(StudentReg);
            }

            string query2 = "Select * from Assessment";
            SqlCommand command2 = new SqlCommand(query2, conn);
            using (SqlDataReader reader = command2.ExecuteReader())
            {
                while (reader.Read())
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = Convert.ToString(reader["Title"]);
                    item.Value = Convert.ToString(reader["Id"]);
                    if (AssessmentId != null)
                    {
                        if (AssessmentId == Convert.ToString(reader["Id"]))
                        {
                            AssessmentTitle = Convert.ToString(reader["Title"]);
                        }
                    }
                    combo_assessment.Items.Add(item);

                    //combo_clo.SelectedIndex = 0;
                }
            }
            if (AssessmentId != null)
            {
                combo_assessment.SelectedIndex = combo_assessment.FindStringExact(AssessmentTitle);
            }
        }

        private void combo_assessment_SelectedIndexChanged(object sender, EventArgs e)
        {
            error_msg.Hide();
            comboc_assessment_component.Items.Clear();
            comboc_assessment_component.ResetText();
            SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
            conn.Open();
            string query1 = "Select * from AssessmentComponent where AssessmentId = '" + (combo_assessment.SelectedItem as ComboboxItem).Value.ToString() + "'";
            SqlCommand command = new SqlCommand(query1, conn);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = Convert.ToString(reader["Name"]);
                    item.Value = Convert.ToString(reader["Id"]); ;
                    if (ACId != null)
                    {
                        if (ACId == Convert.ToString(reader["Id"]))
                        {
                            ACName = Convert.ToString(reader["Name"]);
                        }
                    }
                    comboc_assessment_component.Items.Add(item);
                }
            }

            if (ACId != null)
            {
                comboc_assessment_component.SelectedIndex = comboc_assessment_component.FindStringExact(ACName);
            }
        }

        private void comboc_assessment_component_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
            conn.Open();
            string rubricId = "";
            string oString = "Select * from AssessmentComponent where id='" + (comboc_assessment_component.SelectedItem as ComboboxItem).Value.ToString() + "'";
            SqlCommand oCmd = new SqlCommand(oString, conn);
            using (SqlDataReader oReader = oCmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    rubricId = oReader["RubricId"].ToString();
                    break;
                }
            }

            error_msg.Hide();
            combo_mesure_level.Items.Clear();
            combo_mesure_level.ResetText();

            string query1 = "Select * from RubricLevel where RubricId = '" + rubricId + "'";
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
                    combo_mesure_level.Items.Add(item);
                }
            }

            if (RubricId != null)
            {
                comboc_assessment_component.SelectedIndex = comboc_assessment_component.FindStringExact(ACName);
            }
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            if (combo_assessment.Text != "" && combo_student.Text != "" && comboc_assessment_component.Text != "" && combo_mesure_level.Text != "")
            {
                SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
                conn.Open();
                SqlCommand command;
                if (add_button.Text == "Add Result")
                {
                    string query = "Insert into StudentResult (StudentId, AssessmentComponentId, RubricMeasurementId, EvaluationDate) values('"+(combo_student.SelectedItem as ComboboxItem).Value.ToString() + "', '" + (comboc_assessment_component.SelectedItem as ComboboxItem).Value.ToString() + "', '" + (combo_mesure_level.SelectedItem as ComboboxItem).Value.ToString() + "', '" + DateTime.Now + "')";
                    command = new SqlCommand(query, conn);
                    int i = command.ExecuteNonQuery();
                    if (i != 0)
                    {
                        MessageBox.Show("Result Inserted Successfully");
                    }
                    conn.Close();
                }
                else
                {
                    string query = "Update StudentResult SET StudentId = '"+ (combo_student.SelectedItem as ComboboxItem).Value.ToString() + "', AssessmentComponentId = '"+ (comboc_assessment_component.SelectedItem as ComboboxItem).Value.ToString() + "',  RubricMeasurementId = '"+ (combo_mesure_level.SelectedItem as ComboboxItem).Value.ToString() + "'";
                    command = new SqlCommand(query, conn);
                    int i = command.ExecuteNonQuery();
                    if (i != 0)
                    {
                        MessageBox.Show("Result Updated Successfully");
                    }
                    conn.Close();
                }
                foreach (Form f in Application.OpenForms)
                    f.Hide();

                Form_Result s = new Form_Result();
                s.Show();
            }
            else
            {
                error_msg.Show();
            }
        }
    }
}
