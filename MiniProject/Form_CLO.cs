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
using System.Collections;

namespace MiniProject
{
    public partial class Form_CLO : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
        public Form_CLO()
        {
            InitializeComponent();
        }
        private void StylizeGridView()
        {
            //Changing Names Of The Grid View Columns Headings
            dataGridView_CLO.Columns[0].HeaderText = "Id";
            dataGridView_CLO.Columns[1].HeaderText = "Name";
            dataGridView_CLO.Columns[2].HeaderText = "Creation Date";
            dataGridView_CLO.Columns[3].HeaderText = "Updated On";
            dataGridView_CLO.Columns[4].HeaderText = "Edit";
            dataGridView_CLO.Columns[5].HeaderText = "Delete";
            dataGridView_CLO.Columns[6].HeaderText = "Rubrics";
            // END:: NAMES

            // Changing Heading Fonts
            this.dataGridView_CLO.ColumnHeadersDefaultCellStyle.Font = new Font("Trebuchet MS", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            this.dataGridView_CLO.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dataGridView_CLO.EnableHeadersVisualStyles = false;
            this.dataGridView_CLO.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //Change cell font
            foreach (DataGridViewColumn c in dataGridView_CLO.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Trebuchet MS", 14F, GraphicsUnit.Pixel);
            }
        }
        private void loadDataGridView()
        {
            conn.Open();
            string query = "Select Id,Name,DateCreated,DateUpdated from Clo";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView_CLO.DataSource = dt;
            conn.Close();

            //Buttons

            //Rubrics
            DataGridViewButtonColumn rubricsButtonColumn = new DataGridViewButtonColumn();
            rubricsButtonColumn.Name = "Rubrics";
            rubricsButtonColumn.Text = "Rubrics";
            rubricsButtonColumn.DefaultCellStyle.NullValue = "Rubrics";
            int columnIndex2 = 4;
            if (dataGridView_CLO.Columns["Rubrics"] == null)
            {
                dataGridView_CLO.Columns.Insert(columnIndex2, rubricsButtonColumn);
            }

            //Edit
            DataGridViewButtonColumn EditButtonColumn = new DataGridViewButtonColumn();
            EditButtonColumn.Name = "Edit";
            EditButtonColumn.Text = "Edit";
            EditButtonColumn.DefaultCellStyle.NullValue = "Edit";
            int columnIndex = 5;
            if (dataGridView_CLO.Columns["Edit"] == null)
            {
                dataGridView_CLO.Columns.Insert(columnIndex, EditButtonColumn);
            }
            
            //Delete
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.DefaultCellStyle.NullValue = "Delete";
            int columnIndex1 = 6;
            if (dataGridView_CLO.Columns["Delete"] == null)
            {
                dataGridView_CLO.Columns.Insert(columnIndex1, deleteButtonColumn);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form_CLO f = new Form_CLO();
            f.Show();
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            Form_AddCLO ac = new Form_AddCLO();
            ac.Show();
        }

        private void dataGridView_Students_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0 && e.ColumnIndex == 6)
            {
                var confirmResult = MessageBox.Show("CLO & all its rubrics will get deleted, Are you sure to delete this item??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    int row_index = e.RowIndex;
                    DataGridViewRow selectedRow = dataGridView_CLO.Rows[row_index];
                    string a = Convert.ToString(selectedRow.Cells["Id"].Value);
                    SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
                    conn.Open();
                    string query1 = "Select * from Rubric where CloId='" + a + "'";
                    SqlCommand cmd = new SqlCommand(query1, conn);
                    ArrayList al = new ArrayList();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        string Id = 0.ToString();
                        while (reader.Read())
                        {
                            MessageBox.Show(Convert.ToString(reader["Id"]));
                            //Id = reader["Id"].ToString();
                            al.Add(Convert.ToString(reader["Id"]));
                        }
                    }

                    foreach (string i in al)
                    {
                        string query4 = "DELETE FROM RubricLevel WHERE RubricId = '" + i + "'";
                        SqlCommand command1 = new SqlCommand(query4, conn);
                        command1.ExecuteNonQuery();
                    }

                    string query2 = "DELETE FROM Rubric WHERE CloId = '" + a + "'";
                    SqlCommand command2 = new SqlCommand(query2, conn);
                    command2.ExecuteNonQuery();

                    string query = "DELETE FROM Clo WHERE Id = '" + a + "'";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                    loadDataGridView();
                }
            }
            else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0 && e.ColumnIndex == 5)
            {
                int row_index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridView_CLO.Rows[row_index];
                string a = Convert.ToString(selectedRow.Cells["Id"].Value);
                string[] values;
                values = new string[7];
                using (SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True"))
                {
                    string oString = "Select * from Clo where Id='" + a + "'";
                    SqlCommand oCmd = new SqlCommand(oString, conn);
                    conn.Open();
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            values[0] = oReader["Id"].ToString();
                            values[1] = oReader["Name"].ToString();
                            break;
                        }

                        conn.Close();
                    }
                }
                Form_AddCLO s = new Form_AddCLO(values);
                s.Show();
            }else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
               e.RowIndex >= 0 && e.ColumnIndex == 4)
            {
                int row_index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridView_CLO.Rows[row_index];
                int Id = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                Form_Rubrics f = new Form_Rubrics(Id);
                f.Show();
                this.Hide();
            }
        }

        private void Form_CLO_Load(object sender, EventArgs e)
        {
            loadDataGridView();
            StylizeGridView();
        }

        private void form_heading_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

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

        private void button3_Click(object sender, EventArgs e)
        {
            Form_Attendence f = new Form_Attendence();
            f.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form_Result f = new Form_Result();
            f.Show();
            this.Hide();
        }
    }
}
