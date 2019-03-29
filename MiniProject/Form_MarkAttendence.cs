using System;
using System.Collections;
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
    public partial class Form_MarkAttendence : Form
    {
        ArrayList rows_ids = new ArrayList();
        int edit = 0;
        int on_edit = 0;
        SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
        public Form_MarkAttendence()
        {
            InitializeComponent();
            edit = 0;
        }

        public Form_MarkAttendence(string t)
        {
            InitializeComponent();
            on_edit = 1;
            attendence_date.Value = Convert.ToDateTime(t);


        }

        private void add_button_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
            conn.Open();
            if(add_button.Text == "Mark Attendence") {
                string query = "Insert into ClassAttendance (AttendanceDate) OUTPUT INSERTED.Id values('" + Convert.ToDateTime(attendence_date.Text) + "')";
                SqlCommand command = new SqlCommand(query, conn);
                int attendence_id = (int)command.ExecuteScalar();

                foreach (DataGridViewRow row in dataGridView_Students.Rows)
                {
                    if (row.Cells["Id"].Value != null)
                    {
                        string Id = Convert.ToString(row.Cells["Id"].Value);
                        string SelectedText = Convert.ToString((row.Cells["comboBoxAttendence"] as DataGridViewComboBoxCell).FormattedValue.ToString());
                        //MessageBox.Show(SelectedText);
                        int attendence_status = 1;
                        if (SelectedText == "Absent")
                        {
                            attendence_status = 2;
                        }
                        else if (SelectedText == "Leave")
                        {
                            attendence_status = 3;
                        }
                        else if (SelectedText == "Late")
                        {
                            attendence_status = 4;
                        }
                        string query2 = "Insert into StudentAttendance (AttendanceId,StudentId,AttendanceStatus) values('" + attendence_id + "', '" + Id + "', '" + attendence_status + "')";
                        SqlCommand command2 = new SqlCommand(query2, conn);
                        command2.ExecuteNonQuery();
                        MessageBox.Show("Attendence Marked Successfully");
                    }
                }
            }else if(add_button.Text == "Update Attendence")
            {
                string attendenceId = "";
                string oString = "Select * from ClassAttendance where AttendanceDate='" + Convert.ToDateTime(attendence_date.Text) + "'";
                SqlCommand oCmd = new SqlCommand(oString, conn);
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        attendenceId = oReader["Id"].ToString();
                        break;
                    }
                }

                foreach (int i in rows_ids)
                {
                    var row = dataGridView_Students.Rows[i];
                    string Id = Convert.ToString(row.Cells["Id"].Value);
                    string SelectedText = Convert.ToString((row.Cells["comboBoxAttendence"] as DataGridViewComboBoxCell).FormattedValue.ToString());
                    MessageBox.Show(SelectedText);
                    int attendence_status = 1;
                    if (SelectedText == "Absent")
                    {
                        attendence_status = 2;
                    }
                    else if (SelectedText == "Leave")
                    {
                        attendence_status = 3;
                    }
                    else if (SelectedText == "Late")
                    {
                        attendence_status = 4;
                    }

                    string query2 = "UPDATE StudentAttendance Set AttendanceStatus = '"+ attendence_status + "' where StudentId = '"+ Id + "' AND AttendanceId = '"+ attendenceId + "'";
                    MessageBox.Show(query2);
                    SqlCommand command2 = new SqlCommand(query2, conn);
                    command2.ExecuteNonQuery();
                }
                MessageBox.Show("Updated Successfully");
            }
            
            conn.Close();
        }

        private void Form_Attendence_Load(object sender, EventArgs e)
        {
            
            error_msg.Hide();
            conn.Open();
            edit = 0;
            add_button.Text = "Mark Attendence";
            string oString = "Select * from ClassAttendance where AttendanceDate='" + Convert.ToDateTime(attendence_date.Text) + "'";
            SqlCommand oCmd = new SqlCommand(oString, conn);
            using (SqlDataReader oReader = oCmd.ExecuteReader())
            {
                if (oReader.HasRows)
                {
                    
                    edit = 1;
                    error_msg.Show();
                    add_button.Text = "Update Attendence";
                }
            }
            conn.Close();
            loadDataGridView();
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
            //dataGridView_Students.Columns[6].HeaderText = "Actions";
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
            dataGridView_Students.CellValueChanged +=
             new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
            dataGridView_Students.CurrentCellDirtyStateChanged +=
                 new EventHandler(dataGridView1_CurrentCellDirtyStateChanged);
        }

        void dataGridView1_CurrentCellDirtyStateChanged(object sender,
        EventArgs e)
        {
            if (this.dataGridView_Students.IsCurrentCellDirty)
            {
                // This fires the cell value changed handler below
                dataGridView_Students.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!rows_ids.Contains(e.RowIndex))
            {
                rows_ids.Add(e.RowIndex);
            }
        }

        private void loadDataGridView()
        {
            conn.Open();
            string query = "Select Id,FirstName,LastName,Contact,Email,RegistrationNumber from Student where Status = '5'";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView_Students.DataSource = dt;

            //Buttons
            //Edit
            DataGridViewComboBoxColumn ComboColumn = new DataGridViewComboBoxColumn();
            ComboColumn.Name = "comboBoxAttendence";
            ComboboxItem i = new ComboboxItem();
            i.Text = "Present";
            i.Value = "1";
            ComboColumn.Items.Add(i);
            ComboboxItem i2 = new ComboboxItem();
            i2.Text = "Absent";
            i2.Value = "2";
            ComboColumn.Items.Add(i2);
            ComboboxItem i3 = new ComboboxItem();
            i3.Text = "Leave";
            i3.Value = "3";
            ComboColumn.Items.Add(i3);
            ComboboxItem i4 = new ComboboxItem();
            i4.Text = "Late";
            i4.Value = "4";
            ComboColumn.Items.Add(i4);
            string attendenceId = "";
            string oString = "Select * from ClassAttendance where AttendanceDate='" + Convert.ToDateTime(attendence_date.Text) + "'";
            SqlCommand oCmd = new SqlCommand(oString, conn);
            using (SqlDataReader oReader = oCmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    attendenceId = oReader["Id"].ToString();
                    break;
                }
            }

            int columnIndex = 6;
            if (dataGridView_Students.Columns["Edit"] == null)
            {
                dataGridView_Students.Columns.Insert(columnIndex, ComboColumn);
            }
            if(edit == 1)
            {
                foreach (DataGridViewRow row in dataGridView_Students.Rows)
                {
                    if (row.Cells["Id"].Value != null)
                    {
                        string Id = Convert.ToString(row.Cells["Id"].Value);
                        string query4 = "SELECT * FROM StudentAttendance Where StudentId = '" + Id + "' AND AttendanceId = '" + attendenceId + "'";
                        SqlCommand oCmd2 = new SqlCommand(query4, conn);
                        using (SqlDataReader oReader = oCmd2.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                int index = 0;
                                int status = Convert.ToInt32(oReader["AttendanceStatus"]);
                                if (status == 2)
                                {
                                    index = 1;
                                }
                                else if (status == 3)
                                {
                                    index = 2;
                                }
                                else if (status == 4)
                                {
                                    index = 3;
                                }
                                row.Cells[6].Value = ComboColumn.Items[index];
                            }
                        }
                    }
                }
            }
            else
            {
                for (int index = 0; index < dataGridView_Students.Rows.Count; index++)
                {
                    dataGridView_Students.Rows[index].Cells[6].Value = ComboColumn.Items[0];
                }
            }

            StylizeGridView();
            conn.Close();
        }

        private void dataGridView_Students_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if(e.Exception.Message == "DataGridViewComboBoxCell value is not valid.")
            {
                object value = dataGridView_Students.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (!((DataGridViewComboBoxColumn)dataGridView_Students.Columns[e.ColumnIndex]).Items.Contains(value))
                {
                    ((DataGridViewComboBoxColumn)dataGridView_Students.Columns[e.ColumnIndex]).Items.Add(value);
                    e.ThrowException = false;
                }
            }
        }

        private void attendence_date_ValueChanged(object sender, EventArgs e)
        {
            if(on_edit == 0)
            {
                error_msg.Hide();
                add_button.Text = "Mark Attendence";
                edit = 0;
                conn.Open();
                string oString = "Select * from ClassAttendance where AttendanceDate='" + Convert.ToDateTime(attendence_date.Text) + "'";
                SqlCommand oCmd = new SqlCommand(oString, conn);
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    if (oReader.HasRows)
                    {
                        add_button.Text = "Update Attendence";
                        error_msg.Show();
                        edit = 1;
                    }
                }
                Form_MarkAttendence f = new Form_MarkAttendence(attendence_date.Text);
                f.Show();
                this.Hide();
                conn.Close();
            }
            on_edit = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Form_MarkAttendence f = new Form_MarkAttendence();
            //f.Show();
            //this.Hide();
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

        private void button4_Click(object sender, EventArgs e)
        {
            Form_Result f = new Form_Result();
            f.Show();
            this.Hide();
        }
    }
}
