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
    public partial class Form_Levels : Form
    {
        int RubricId;
        SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
        public Form_Levels(int id)
        {
            RubricId = id;
            InitializeComponent();
        }

        private void StylizeGridView()
        {
            // Changing Names Of The Grid View Columns Headings
            dataGridView_Level.Columns[0].HeaderText = "Id";
            dataGridView_Level.Columns[1].HeaderText = "Details";
            dataGridView_Level.Columns[2].HeaderText = "Measurement Level";
            dataGridView_Level.Columns[3].HeaderText = "Edit";
            dataGridView_Level.Columns[4].HeaderText = "Delete";
            // END:: NAMES

            // Changing Heading Fonts
            this.dataGridView_Level.ColumnHeadersDefaultCellStyle.Font = new Font("Trebuchet MS", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            this.dataGridView_Level.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dataGridView_Level.EnableHeadersVisualStyles = false;
            this.dataGridView_Level.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //Change cell font
            foreach (DataGridViewColumn c in dataGridView_Level.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Trebuchet MS", 14F, GraphicsUnit.Pixel);
            }
        }

        private void loadDataGridView()
        {
            conn.Open();
            string query = "Select Id,Details,MeasurementLevel from RubricLevel WHERE RubricId = '" + RubricId + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView_Level.DataSource = dt;
            conn.Close();

            //Edit
            DataGridViewButtonColumn EditButtonColumn = new DataGridViewButtonColumn();
            EditButtonColumn.Name = "Edit";
            EditButtonColumn.Text = "Edit";
            EditButtonColumn.DefaultCellStyle.NullValue = "Edit";
            int columnIndex = 3;
            if (dataGridView_Level.Columns["Edit"] == null)
            {
                dataGridView_Level.Columns.Insert(columnIndex, EditButtonColumn);
            }

            //Delete
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.DefaultCellStyle.NullValue = "Delete";
            int columnIndex1 = 4;
            if (dataGridView_Level.Columns["Delete"] == null)
            {
                dataGridView_Level.Columns.Insert(columnIndex1, deleteButtonColumn);
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

        private void dataGridView_Level_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0 && e.ColumnIndex == 4)
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this item??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    int row_index = e.RowIndex;
                    DataGridViewRow selectedRow = dataGridView_Level.Rows[row_index];
                    string a = Convert.ToString(selectedRow.Cells["Id"].Value);
                    conn.Open();
                    string query = "DELETE FROM RubricLevel WHERE Id = '" + a + "'";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                    loadDataGridView();
                }
            }
            else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0 && e.ColumnIndex == 3)
            {
                int row_index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridView_Level.Rows[row_index];
                string a = Convert.ToString(selectedRow.Cells["Id"].Value);
                string[] values;
                values = new string[7];
                using (SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True"))
                {
                    string oString = "Select * from RubricLevel where Id='" + a + "'";
                    SqlCommand oCmd = new SqlCommand(oString, conn);
                    conn.Open();
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            values[0] = oReader["Id"].ToString();
                            values[1] = oReader["Details"].ToString();
                            values[2] = oReader["MeasurementLevel"].ToString();
                            values[3] = oReader["RubricId"].ToString();
                            break;
                        }

                        conn.Close();
                    }
                }
                Form_AddLevel s = new Form_AddLevel(values);
                s.Show();
            }
        }

        private void Form_Levels_Load(object sender, EventArgs e)
        {
            loadDataGridView();
            StylizeGridView();
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            Form_AddLevel f = new Form_AddLevel(RubricId);
            f.Show();
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
