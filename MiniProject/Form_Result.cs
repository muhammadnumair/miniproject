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
    public partial class Form_Result : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
        double student_level = 0;
        double max_level = 0;
        int RubricId = 0;
        double total_marks = 0;
        double obtained_marks = 0;
        string assessment_name = "";
        int assessment_id = 0;
        double assessment_total_marks = 0;

        public Form_Result()
        {
            InitializeComponent();
        }
        private void StylizeGridView()
        {
            // Changing Names Of The Grid View Columns Headings
            dataGridView_Result.Columns[0].HeaderText = "Name";
            dataGridView_Result.Columns[1].HeaderText = "Contact#";
            dataGridView_Result.Columns[2].HeaderText = "Email";
            dataGridView_Result.Columns[3].HeaderText = "Assessment Title";
            dataGridView_Result.Columns[4].HeaderText = "Obtained Marks";
            dataGridView_Result.Columns[5].HeaderText = "Total Marks";
            
            // END:: NAMES

            // Changing Heading Fonts
            this.dataGridView_Result.ColumnHeadersDefaultCellStyle.Font = new Font("Trebuchet MS", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            this.dataGridView_Result.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dataGridView_Result.EnableHeadersVisualStyles = false;
            this.dataGridView_Result.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //Change cell font
            foreach (DataGridViewColumn c in dataGridView_Result.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Trebuchet MS", 14F, GraphicsUnit.Pixel);
            }
        }

        public void calculate(string RubricMeasurementId, string AssessmentComponentId)
        {
            //conn.Open();
            using (SqlCommand cmd = new SqlCommand("Select MeasurementLevel from RubricLevel where Id='" + RubricMeasurementId + "'", conn))
            {
                student_level = (int)cmd.ExecuteScalar();
            }

            using (SqlCommand cmd = new SqlCommand("Select RubricId from RubricLevel where Id='" + RubricMeasurementId + "'", conn))
            {
                RubricId = (int)cmd.ExecuteScalar();
            }

            using (SqlCommand cmd = new SqlCommand("SELECT MAX(MeasurementLevel) FROM RubricLevel WHERE RubricId='" + RubricId + "'", conn))
            {
                max_level = (int)cmd.ExecuteScalar();
            }

            using (SqlCommand cmd = new SqlCommand("SELECT TotalMarks FROM AssessmentComponent WHERE Id='" + AssessmentComponentId + "'", conn))
            {
                total_marks = (int)cmd.ExecuteScalar();
            }

            using (SqlCommand cmd = new SqlCommand("SELECT AssessmentId FROM AssessmentComponent WHERE Id='" + AssessmentComponentId + "'", conn))
            {
                assessment_id = (int)cmd.ExecuteScalar();
            }

            using (SqlCommand cmd = new SqlCommand("SELECT Title FROM Assessment WHERE Id='" + assessment_id + "'", conn))
            {
                assessment_name = (string)cmd.ExecuteScalar();
            }


            using (SqlCommand cmd = new SqlCommand("SELECT TotalMarks FROM Assessment WHERE Id='" + assessment_id + "'", conn))
            {
                assessment_total_marks = (int)cmd.ExecuteScalar();
            }

            obtained_marks = (student_level / max_level) * total_marks;
        }

        private void loadDataGridView()
        {
            conn.Open();
            //string oString = "Select Student.Id,Student.FirstName,Student.LastName,Student.RegistrationNumber,StudentResult.AssessmentComponentId,StudentResult.RubricMeasurementId,StudentResult.EvaluationDate from Student INNER JOIN StudentResult ON Student.Id = StudentResult.StudentId";
            //SqlCommand oCmd = new SqlCommand(oString, conn);
            using (SqlConnection connection001 = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True"))
            {
                connection001.Open();
                using (SqlCommand oCmd001 = new SqlCommand("Select * from Assessment", connection001))
                {
                    using (SqlDataReader oReader001 = oCmd001.ExecuteReader())
                    {
                        while (oReader001.Read())
                        {
                            using (SqlCommand cmd = new SqlCommand("SELECT Title FROM Assessment WHERE Id='" + oReader001["Id"].ToString() + "'", conn))
                            {
                                assessment_name = (string)cmd.ExecuteScalar();
                            }

                            using (SqlConnection connection = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True"))
                            {
                                connection.Open();
                                using (SqlCommand oCmd = new SqlCommand("Select * from Student", connection))
                                {
                                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                                    {
                                        while (oReader.Read())
                                        {
                                            double total_marks_in_assignment = 0;
                                            using (SqlConnection connection1 = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True"))
                                            {
                                                connection1.Open();
                                                using (SqlCommand oCmd1 = new SqlCommand("Select * from AssessmentComponent where AssessmentId = '" + oReader001["Id"].ToString() + "'", connection1))
                                                {
                                                    using (SqlDataReader oReader1 = oCmd1.ExecuteReader())
                                                    {
                                                        while (oReader1.Read())
                                                        {
                                                            using (SqlConnection connection2 = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True"))
                                                            {
                                                                connection2.Open();
                                                                using (SqlCommand oCmd2 = new SqlCommand("Select * from StudentResult where StudentId = '" + Convert.ToString(oReader["Id"]) + "' AND AssessmentComponentId = '" + Convert.ToString(oReader1["Id"]) + "'", connection2))
                                                                {
                                                                    using (SqlDataReader oReader2 = oCmd2.ExecuteReader())
                                                                    {
                                                                        while (oReader2.Read())
                                                                        {
                                                                            calculate(oReader2["RubricMeasurementId"].ToString(), oReader2["AssessmentComponentId"].ToString());
                                                                            total_marks_in_assignment += obtained_marks;
                                                                        }

                                                                        connection2.Close();
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        connection1.Close();
                                                    }
                                                }
                                            }
                                            if (total_marks_in_assignment != 0)
                                            {
                                                // YAHAN BAQI CODE ANAA HAIs
                                                this.dataGridView_Result.Rows.Add(Convert.ToString(oReader["FirstName"]) + " " + Convert.ToString(oReader["LastName"]), Convert.ToString(oReader["Contact"]), Convert.ToString(oReader["Email"]), assessment_name , total_marks_in_assignment.ToString(), assessment_total_marks.ToString());
                                            }
                                        }
                                        connection.Close();
                                    }
                                }
                            }
                        }

                        connection001.Close();
                    }
                }
            }
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            Form_AddNewResult f = new Form_AddNewResult();
            f.Show();
        }

        private void Form_Result_Load(object sender, EventArgs e)
        {
            StylizeGridView();
            loadDataGridView();
        }

        private void btn_students_Click(object sender, EventArgs e)
        {
            Form_Students f = new Form_Students();
            f.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form_CLO f = new Form_CLO();
            f.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form_Assessment f = new Form_Assessment();
            f.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form_MarkAttendence f = new Form_MarkAttendence();
            f.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form_Result f = new Form_Result();
            f.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form_report f = new Form_report();
            f.Show();
        }
    }
}
