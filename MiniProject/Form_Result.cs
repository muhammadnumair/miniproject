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

        public Form_Result()
        {
            InitializeComponent();
        }
        private void StylizeGridView()
        {
            // Changing Names Of The Grid View Columns Headings
            dataGridView_Result.Columns[0].HeaderText = "Id";
            dataGridView_Result.Columns[1].HeaderText = "First Name";
            dataGridView_Result.Columns[2].HeaderText = "Last Name";
            dataGridView_Result.Columns[3].HeaderText = "Registeration#";
            dataGridView_Result.Columns[4].HeaderText = "Obtained Marks";
            dataGridView_Result.Columns[5].HeaderText = "Total Marks";
            dataGridView_Result.Columns[6].HeaderText = "Evaluation Date";
            
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

            obtained_marks = (student_level / max_level) * total_marks;
        }

        private void loadDataGridView()
        {
            conn.Open();

            //string oString = "Select Student.Id,Student.FirstName,Student.LastName,Student.RegistrationNumber,StudentResult.AssessmentComponentId,StudentResult.RubricMeasurementId,StudentResult.EvaluationDate from Student INNER JOIN StudentResult ON Student.Id = StudentResult.StudentId";
            //SqlCommand oCmd = new SqlCommand(oString, conn);
            using (SqlConnection connection = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True"))
            {
                connection.Open();
                using (SqlCommand oCmd = new SqlCommand("Select Student.Id,Student.FirstName,Student.LastName,Student.RegistrationNumber,StudentResult.AssessmentComponentId,StudentResult.RubricMeasurementId,StudentResult.EvaluationDate from Student INNER JOIN StudentResult ON Student.Id = StudentResult.StudentId", connection))
                {
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            calculate(oReader["RubricMeasurementId"].ToString(), oReader["AssessmentComponentId"].ToString());
                            //MessageBox.Show(Convert.ToString(student_level/max_level));
                            //ADDING ROWS
                            this.dataGridView_Result.Rows.Add(oReader["Id"].ToString(), oReader["FirstName"].ToString(), oReader["LastName"].ToString(), oReader["RegistrationNumber"].ToString(), obtained_marks,total_marks, oReader["EvaluationDate"].ToString());
                        }

                        connection.Close();
                    }
                }
            }
            //Buttons

            ////Edit
            //DataGridViewButtonColumn EditButtonColumn = new DataGridViewButtonColumn();
            //EditButtonColumn.Name = "Edit";
            //EditButtonColumn.Text = "Edit";
            //EditButtonColumn.DefaultCellStyle.NullValue = "Edit";
            //int columnIndex = 7;
            //if (dataGridView_Result.Columns["Edit"] == null)
            //{
            //    dataGridView_Result.Columns.Insert(columnIndex, EditButtonColumn);
            //}

            ////Delete
            //DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            //deleteButtonColumn.Name = "Delete";
            //deleteButtonColumn.Text = "Delete";
            //deleteButtonColumn.DefaultCellStyle.NullValue = "Delete";
            //int columnIndex1 = 8;
            //if (dataGridView_Result.Columns["Delete"] == null)
            //{
            //    dataGridView_Result.Columns.Insert(columnIndex1, deleteButtonColumn);
            //}

            //dataGridView_Result.Columns[7].HeaderText = "Edit";
            //dataGridView_Result.Columns[8].HeaderText = "Delete";

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
