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
    public partial class Form_MarkAttendence : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
        public Form_MarkAttendence()
        {
            InitializeComponent();
        }

        private void add_button_Click(object sender, EventArgs e)
        {

        }

        private void Form_Attendence_Load(object sender, EventArgs e)
        {
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
        }

        private void loadDataGridView()
        {
            conn.Open();
            string query = "Select Id,FirstName,LastName,Contact,Email,RegistrationNumber from Student where Status = '5'";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView_Students.DataSource = dt;
            conn.Close();

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
            
            int columnIndex = 6;
            if (dataGridView_Students.Columns["Edit"] == null)
            {
                dataGridView_Students.Columns.Insert(columnIndex, ComboColumn);
            }

            for (int index = 0; index < dataGridView_Students.Rows.Count; index++)
            {
                dataGridView_Students.Rows[index].Cells[6].Value = ComboColumn.Items[0];
            }

            StylizeGridView();
        }
    }
}
