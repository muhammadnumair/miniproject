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
            dataGridView_Rubric.Columns[0].HeaderText = "Delete";
            dataGridView_Rubric.Columns[1].HeaderText = "Edit";
            dataGridView_Rubric.Columns[2].HeaderText = "Levels";
            dataGridView_Rubric.Columns[3].HeaderText = "Id";
            dataGridView_Rubric.Columns[4].HeaderText = "Details";
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
            StylizeGridView();
        }

        private void dataGridView_CLO_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                int row_index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridView_Rubric.Rows[row_index];
                string a = Convert.ToString(selectedRow.Cells["Id"].Value);
                conn.Open();
                string query = "DELETE FROM Rubric WHERE Id = '" + a + "'";
                SqlCommand command = new SqlCommand(query, conn);
                command.ExecuteNonQuery();
                conn.Close();
                loadDataGridView();
            }
            else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0 && e.ColumnIndex == 1)
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
                string Id = Convert.ToString(selectedRow.Cells["Id"].Value);
            }
        }

        private void Form_Rubrics_Load(object sender, EventArgs e)
        {
            loadDataGridView();
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
    }
}
