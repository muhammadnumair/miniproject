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
    public partial class Form_Rubrics : Form
    {
        int CloId;
        SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
        public Form_Rubrics(int id)
        {
            CloId = id;
            InitializeComponent();
        }

        private void StylizeGridView()
        {
            // Changing Names Of The Grid View Columns Headings
            dataGridView_Rubric.Columns[0].HeaderText = "Id";
            dataGridView_Rubric.Columns[1].HeaderText = "Details";
            dataGridView_Rubric.Columns[2].HeaderText = "Levels";
            dataGridView_Rubric.Columns[3].HeaderText = "Edit";
            dataGridView_Rubric.Columns[4].HeaderText = "Delete";
            // END:: NAMES

            // Changing Heading Fonts
            this.dataGridView_Rubric.ColumnHeadersDefaultCellStyle.Font = new Font("Trebuchet MS", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            this.dataGridView_Rubric.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dataGridView_Rubric.EnableHeadersVisualStyles = false;
            this.dataGridView_Rubric.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //Change cell font
            foreach (DataGridViewColumn c in dataGridView_Rubric.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Trebuchet MS", 14F, GraphicsUnit.Pixel);
            }
        }

        private void loadDataGridView()
        {
            conn.Open();
            string query = "Select Id,Details from Rubric WHERE CloId = '"+CloId+ "'";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView_Rubric.DataSource = dt;
            conn.Close();

            //Buttons

            //Rubrics
            DataGridViewButtonColumn rubricsButtonColumn = new DataGridViewButtonColumn();
            rubricsButtonColumn.Name = "Levels";
            rubricsButtonColumn.Text = "Levels";
            rubricsButtonColumn.DefaultCellStyle.NullValue = "Levels";
            int columnIndex2 = 2;
            if (dataGridView_Rubric.Columns["Levels"] == null)
            {
                dataGridView_Rubric.Columns.Insert(columnIndex2, rubricsButtonColumn);
            }

            //Edit
            DataGridViewButtonColumn EditButtonColumn = new DataGridViewButtonColumn();
            EditButtonColumn.Name = "Edit";
            EditButtonColumn.Text = "Edit";
            EditButtonColumn.DefaultCellStyle.NullValue = "Edit";
            int columnIndex = 3;
            if (dataGridView_Rubric.Columns["Edit"] == null)
            {
                dataGridView_Rubric.Columns.Insert(columnIndex, EditButtonColumn);
            }

            //Delete
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.DefaultCellStyle.NullValue = "Delete";
            int columnIndex1 = 4;
            if (dataGridView_Rubric.Columns["Delete"] == null)
            {
                dataGridView_Rubric.Columns.Insert(columnIndex1, deleteButtonColumn);
            }
        }

        private void dataGridView_CLO_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0 && e.ColumnIndex == 4)
            {
                var confirmResult = MessageBox.Show("Rubric & all its levels will get deleted, Are you sure to delete this item??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    int row_index = e.RowIndex;
                    DataGridViewRow selectedRow = dataGridView_Rubric.Rows[row_index];
                    string a = Convert.ToString(selectedRow.Cells["Id"].Value);
                    conn.Open();
                    string query1 = "DELETE FROM RubricLevel WHERE RubricId = '" + a + "'";
                    SqlCommand command1 = new SqlCommand(query1, conn);
                    command1.ExecuteNonQuery();
                    string query = "DELETE FROM Rubric WHERE Id = '" + a + "'";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                    loadDataGridView();
                }
                else
                {
                    // If 'No', do something here.
                }
            }
            else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0 && e.ColumnIndex == 3)
            {
                int row_index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridView_Rubric.Rows[row_index];
                string a = Convert.ToString(selectedRow.Cells["Id"].Value);
                string[] values;
                values = new string[7];
                using (SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True"))
                {
                    string oString = "Select * from Rubric where Id='" + a + "'";
                    SqlCommand oCmd = new SqlCommand(oString, conn);
                    conn.Open();
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            values[0] = oReader["Id"].ToString();
                            values[1] = oReader["Details"].ToString();
                            values[2] = oReader["CloId"].ToString();
                            break;
                        }

                        conn.Close();
                    }
                }
                Form_AddRubric s = new Form_AddRubric(values);
                s.Show();
            }
            else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
              e.RowIndex >= 0 && e.ColumnIndex == 2)
            {
                int row_index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridView_Rubric.Rows[row_index];
                int Id = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                Form_Levels f = new Form_Levels(Id);
                f.Show();
                this.Hide();
            }
        }

        private void Form_Rubrics_Load(object sender, EventArgs e)
        {
            loadDataGridView();
            StylizeGridView();
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            Form_AddRubric f = new Form_AddRubric(CloId);
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form_CLO f = new Form_CLO();
            f.Show();
            this.Hide();
        }

        private void btn_students_Click(object sender, EventArgs e)
        {
            Form_Students f = new Form_Students();
            f.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form_Assessment f = new Form_Assessment();
            f.Show();
            this.Hide();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

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
