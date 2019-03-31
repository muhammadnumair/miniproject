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
    public partial class Form_Assessment : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
        public Form_Assessment()
        {
            InitializeComponent();
        }

        private void StylizeGridView()
        {
            // Changing Names Of The Grid View Columns Headings
            dataGridView_Assessment.Columns[0].HeaderText = "Id";
            dataGridView_Assessment.Columns[1].HeaderText = "Title";
            //dataGridView_Assessment.Columns[5].HeaderText = "Date Created";
            dataGridView_Assessment.Columns[2].HeaderText = "Total Marks";
            dataGridView_Assessment.Columns[3].HeaderText = "Total Weightage";
            dataGridView_Assessment.Columns[4].HeaderText = "Questions";
            dataGridView_Assessment.Columns[5].HeaderText = "Edit";
            dataGridView_Assessment.Columns[6].HeaderText = "Delete";
            // END:: NAMES

            // Changing Heading Fonts
            this.dataGridView_Assessment.ColumnHeadersDefaultCellStyle.Font = new Font("Trebuchet MS", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            this.dataGridView_Assessment.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dataGridView_Assessment.EnableHeadersVisualStyles = false;
            this.dataGridView_Assessment.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //Change cell font
            foreach (DataGridViewColumn c in dataGridView_Assessment.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Trebuchet MS", 14F, GraphicsUnit.Pixel);
            }
        }

        private void loadDataGridView()
        {
            conn.Open();
            string query = "Select Id,Title,TotalMarks,TotalWeightage from Assessment";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView_Assessment.DataSource = dt;
            conn.Close();

            //Buttons

            //Rubrics
            DataGridViewButtonColumn rubricsButtonColumn = new DataGridViewButtonColumn();
            rubricsButtonColumn.Name = "Questions";
            rubricsButtonColumn.Text = "Questions";
            rubricsButtonColumn.DefaultCellStyle.NullValue = "Questions";
            int columnIndex2 = 4;
            if (dataGridView_Assessment.Columns["Questions"] == null)
            {
                dataGridView_Assessment.Columns.Insert(columnIndex2, rubricsButtonColumn);
            }

            //Edit
            DataGridViewButtonColumn EditButtonColumn = new DataGridViewButtonColumn();
            EditButtonColumn.Name = "Edit";
            EditButtonColumn.Text = "Edit";
            EditButtonColumn.DefaultCellStyle.NullValue = "Edit";
            int columnIndex = 5;
            if (dataGridView_Assessment.Columns["Edit"] == null)
            {
                dataGridView_Assessment.Columns.Insert(columnIndex, EditButtonColumn);
            }

            //Delete
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.DefaultCellStyle.NullValue = "Delete";
            int columnIndex1 = 6;
            if (dataGridView_Assessment.Columns["Delete"] == null)
            {
                dataGridView_Assessment.Columns.Insert(columnIndex1, deleteButtonColumn);
            }
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            Form_AddAssessment f = new Form_AddAssessment();
            f.Show();
        }

        private void dataGridView_Assessment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0 && e.ColumnIndex == 6)
            {
                int row_index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridView_Assessment.Rows[row_index];
                string a = Convert.ToString(selectedRow.Cells["Id"].Value);
                conn.Open();
                string query = "DELETE FROM Assessment WHERE Id = '" + a + "'";
                SqlCommand command = new SqlCommand(query, conn);
                command.ExecuteNonQuery();
                conn.Close();
                loadDataGridView();
            }
            else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0 && e.ColumnIndex == 5)
            {
                int row_index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridView_Assessment.Rows[row_index];
                string a = Convert.ToString(selectedRow.Cells["Id"].Value);
                string[] values;
                values = new string[7];
                using (SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True"))
                {
                    string oString = "Select * from Assessment where Id='" + a + "'";
                    SqlCommand oCmd = new SqlCommand(oString, conn);
                    conn.Open();
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            values[0] = oReader["Id"].ToString();
                            values[1] = oReader["Title"].ToString();
                            values[2] = oReader["TotalMarks"].ToString();
                            values[3] = oReader["TotalWeightage"].ToString();
                            break;
                        }

                        conn.Close();
                    }
                }
                Form_AddAssessment s = new Form_AddAssessment(values);
                s.Show();
            }
            else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
               e.RowIndex >= 0 && e.ColumnIndex == 4)
            {
                int row_index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridView_Assessment.Rows[row_index];
                int Id = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                Form_AssessmentComponents f = new Form_AssessmentComponents(Id);
                f.Show();
                this.Hide();
            }
        }

        private void Form_Assessment_Load(object sender, EventArgs e)
        {
            loadDataGridView();
            StylizeGridView();
        }

        private void btn_students_Click(object sender, EventArgs e)
        {
            Form_Students f = new Form_Students();
            f.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form_CLO f = new Form_CLO();
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
