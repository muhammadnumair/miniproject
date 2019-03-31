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
    public partial class Form_Students : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");

        public Form_Students()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void add_button_Click(object sender, EventArgs e)
        {
            Form_AddNewStudent f = new Form_AddNewStudent();
            f.Show();
        }
        private void StylizeGridView()
        {
            // Changing Names Of The Grid View Columns Headings
            dataGridView_Students.Columns[0].HeaderText = "Id";
            dataGridView_Students.Columns[1].HeaderText = "First Name";
            dataGridView_Students.Columns[2].HeaderText = "Last Name";
            dataGridView_Students.Columns[3].HeaderText = "Contact#";
            dataGridView_Students.Columns[4].HeaderText = "Email";
            dataGridView_Students.Columns[5].HeaderText = "Registeration#";
            dataGridView_Students.Columns[6].HeaderText = "Edit";
            dataGridView_Students.Columns[7].HeaderText = "Delete";
            // END:: NAMES

            // Changing Heading Fonts
            this.dataGridView_Students.ColumnHeadersDefaultCellStyle.Font = new Font("Trebuchet MS", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            this.dataGridView_Students.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dataGridView_Students.EnableHeadersVisualStyles = false;
            this.dataGridView_Students.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //Change cell font
            foreach (DataGridViewColumn c in dataGridView_Students.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Trebuchet MS", 14F, GraphicsUnit.Pixel);
            }
        }
        private void loadDataGridView(int value = 0, string search = "")
        {
            //dataGridView_Students.DataSource = null;
            //dataGridView_Students.Refresh();
            if (value == 0)
            {
                conn.Open();
                string query = "Select Id,FirstName,LastName,Contact,Email,RegistrationNumber from Student";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView_Students.DataSource = dt;
                conn.Close();
            }
            else if(value == 5 || value == 6)
            {
                conn.Open();
                string query = "Select Id,FirstName,LastName,Contact,Email,RegistrationNumber from Student where Status = '"+value+"'";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView_Students.DataSource = dt;
                conn.Close();
            }
            if (search != "")
            {
                MessageBox.Show("Hello G");
                conn.Open();
                string query = "Select Id,FirstName,LastName,Contact,Email,RegistrationNumber from Student where (FirstName LIKE '"+search+ "' OR LastName LIKE '" + search + "' OR Contact LIKE '" + search + "' OR Email LIKE '" + search + "' OR RegistrationNumber LIKE '" + search + "')";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView_Students.DataSource = dt;
                conn.Close();
            }

            //Buttons
            //Edit
            DataGridViewButtonColumn EditButtonColumn = new DataGridViewButtonColumn();
            EditButtonColumn.Name = "Edit";
            EditButtonColumn.Text = "Edit";
            EditButtonColumn.DefaultCellStyle.NullValue = "Edit";
            int columnIndex = 6;
            if (dataGridView_Students.Columns["Edit"] == null)
            {
                dataGridView_Students.Columns.Insert(columnIndex, EditButtonColumn);
            }

            //Delete
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.DefaultCellStyle.NullValue = "Delete";
            int columnIndex1 = 7;
            if (dataGridView_Students.Columns["Delete"] == null)
            {
                dataGridView_Students.Columns.Insert(columnIndex1, deleteButtonColumn);
            }
        }

        private void MiniProject_Load(object sender, EventArgs e)
        {
            loadDataGridView();
            StylizeGridView();
        }

        private void btn_students_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //Form_Students f = new Form_Students();
            //f.Show();
        }

        private void dataGridView_Students_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0 && e.ColumnIndex == 7)
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this item??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    int row_index = e.RowIndex;
                    DataGridViewRow selectedRow = dataGridView_Students.Rows[row_index];
                    string a = Convert.ToString(selectedRow.Cells["Id"].Value);
                    conn.Open();
                    string query = "DELETE FROM Student WHERE Id = '" + a + "'";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                    loadDataGridView();
                }
            }
            else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0 && e.ColumnIndex == 6)
            {
                int row_index = e.RowIndex;
                DataGridViewRow selectedRow = dataGridView_Students.Rows[row_index];
                string a = Convert.ToString(selectedRow.Cells["Id"].Value);
                string[] values;
                values = new string[7];
                using (SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True"))
                {
                    string oString = "Select * from Student where Id='"+a+"'";
                    SqlCommand oCmd = new SqlCommand(oString, conn);
                    conn.Open();
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            values[0] = oReader["Id"].ToString();
                            values[1] = oReader["FirstName"].ToString();
                            values[2] = oReader["LastName"].ToString();
                            values[3] = oReader["Contact"].ToString();
                            values[4] = oReader["Email"].ToString();
                            values[5] = oReader["RegistrationNumber"].ToString();
                            values[6] = oReader["Status"].ToString();
                            break;
                        }

                        conn.Close();
                    }
                }
                Form_AddNewStudent s = new Form_AddNewStudent(values);
                s.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form_CLO f = new Form_CLO();
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
            Form_Result f = new Form_Result();
            f.Show();
            this.Hide();
        }

        private void combo_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(filter_by_status.Text == "Active")
            {
                loadDataGridView(5);
            }
            else if(filter_by_status.Text == "InActive")
            {
                loadDataGridView(6);
            }else if (filter_by_status.Text == "All")
            {
                loadDataGridView(0);
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            string search = text_search.Text;
            if(search != "")
            {
                MessageBox.Show("Search");
                loadDataGridView(0, search);
            }
        }

        private void text_search_TextChanged(object sender, EventArgs e)
        {
            if(text_search.Text == "")
            {
                loadDataGridView(0);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form_MarkAttendence f = new Form_MarkAttendence();
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
