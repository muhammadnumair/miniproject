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
    public partial class Form_AssessmentComponents : Form
    {
        int AssessmentId;
        SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
        public Form_AssessmentComponents(int id)
        {
            AssessmentId = id;
            InitializeComponent();
        }

        private void StylizeGridView()
        {
            // Changing Names Of The Grid View Columns Headings
            dataGridView_Questions.Columns[0].HeaderText = "Id";
            dataGridView_Questions.Columns[1].HeaderText = "Name";
            dataGridView_Questions.Columns[2].HeaderText = "Total Marks";
            dataGridView_Questions.Columns[3].HeaderText = "Date Created";
            dataGridView_Questions.Columns[4].HeaderText = "Date Updated";
            dataGridView_Questions.Columns[5].HeaderText = "Edit";
            dataGridView_Questions.Columns[6].HeaderText = "Delete";
            // END:: NAMES

            // Changing Heading Fonts
            this.dataGridView_Questions.ColumnHeadersDefaultCellStyle.Font = new Font("Trebuchet MS", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            this.dataGridView_Questions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dataGridView_Questions.EnableHeadersVisualStyles = false;
            this.dataGridView_Questions.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //Change cell font
            foreach (DataGridViewColumn c in dataGridView_Questions.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Trebuchet MS", 14F, GraphicsUnit.Pixel);
            }
        }

        private void loadDataGridView()
        {
            conn.Open();
            string query = "Select Id,Name,TotalMarks,DateCreated,DateUpdated from AssessmentComponent WHERE AssessmentId = '" + AssessmentId + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView_Questions.DataSource = dt;
            conn.Close();

            //Edit
            DataGridViewButtonColumn EditButtonColumn = new DataGridViewButtonColumn();
            EditButtonColumn.Name = "Edit";
            EditButtonColumn.Text = "Edit";
            EditButtonColumn.DefaultCellStyle.NullValue = "Edit";
            int columnIndex = 5;
            if (dataGridView_Questions.Columns["Edit"] == null)
            {
                dataGridView_Questions.Columns.Insert(columnIndex, EditButtonColumn);
            }

            //Delete
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.DefaultCellStyle.NullValue = "Delete";
            int columnIndex1 = 6;
            if (dataGridView_Questions.Columns["Delete"] == null)
            {
                dataGridView_Questions.Columns.Insert(columnIndex1, deleteButtonColumn);
            }
        }

        private void Form_AssessmentComponents_Load(object sender, EventArgs e)
        {
            loadDataGridView();
            StylizeGridView();
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            Form_AddAssessmentComponent f = new Form_AddAssessmentComponent(AssessmentId);
            f.Show();
        }

        private void dataGridView_Questions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0 && e.ColumnIndex == 6)
            {
                int row_index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridView_Questions.Rows[row_index];
                string a = Convert.ToString(selectedRow.Cells["Id"].Value);
                conn.Open();
                string query = "DELETE FROM AssessmentComponent WHERE Id = '" + a + "'";
                SqlCommand command = new SqlCommand(query, conn);
                command.ExecuteNonQuery();
                conn.Close();
                loadDataGridView();
            }
            else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0 && e.ColumnIndex == 5)
            {
                int row_index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridView_Questions.Rows[row_index];
                string a = Convert.ToString(selectedRow.Cells["Id"].Value);
                string[] values;
                values = new string[7];
                using (SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True"))
                {
                    string oString = "Select * from AssessmentComponent where Id='" + a + "'";
                    SqlCommand oCmd = new SqlCommand(oString, conn);
                    conn.Open();
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            values[0] = oReader["Id"].ToString();
                            values[1] = oReader["Name"].ToString();
                            values[2] = oReader["TotalMarks"].ToString();
                            values[3] = oReader["AssessmentId"].ToString();
                            values[4] = oReader["RubricId"].ToString();
                            break;
                        }

                        conn.Close();
                    }
                }
                string ooString = "Select * from Rubric where Id='" + values[4] + "'";
                SqlCommand ooCmd = new SqlCommand(ooString, conn);
                conn.Open();
                using (SqlDataReader oReader = ooCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        values[5] = oReader["CloId"].ToString();
                        break;
                    }

                    conn.Close();
                }
                Form_AddAssessmentComponent s = new Form_AddAssessmentComponent(values);
                s.Show();
            }
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
