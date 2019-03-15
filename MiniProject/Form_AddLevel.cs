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
    public partial class Form_AddLevel : Form
    {
        int RubricId;
        int Id;
        public Form_AddLevel(int id)
        {
            RubricId = id;
            InitializeComponent();
        }

        public Form_AddLevel(string[] values)
        {
            InitializeComponent();
            Id = Convert.ToInt32(values[0]);
            text_details.Text = values[1];
            text_level.Text = values[2];
            RubricId = Convert.ToInt32(values[3]);
            add_button.Text = "Update Record";
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            if(text_details.Text != "" && text_level.Text != "")
            {
                SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
                conn.Open();

                if (add_button.Text == "Add Level")
                {
                    string query = "Insert into RubricLevel (RubricId, Details, MeasurementLevel) values('" + RubricId + "','" + text_details.Text + "', '" + text_level.Text + "')";
                    SqlCommand command = new SqlCommand(query, conn);
                    int i = command.ExecuteNonQuery();
                    if (i != 0)
                    {
                        MessageBox.Show("Level Record Inserted Successfully");
                    }
                    conn.Close();
                }
                else
                {
                    string query = "Update RubricLevel SET Details = '" + text_details.Text + "',MeasurementLevel = '" + text_level.Text + "'  where Id = '" + Id + "'";
                    SqlCommand command = new SqlCommand(query, conn);
                    int i = command.ExecuteNonQuery();
                    if (i != 0)
                    {
                        MessageBox.Show("Level Record Updated Successfully");
                    }
                    conn.Close();
                }
                foreach (Form f in Application.OpenForms)
                    f.Hide();

                Form_Levels s = new Form_Levels(RubricId);
                s.Show();
            }
            else
            {
                error_msg.Show();
            }
        }

        private void Form_AddLevel_Load(object sender, EventArgs e)
        {
            error_msg.Hide();
        }

        private void text_details_TextChanged(object sender, EventArgs e)
        {
            error_msg.Hide();
        }

        private void text_level_TextChanged(object sender, EventArgs e)
        {
            error_msg.Hide();
        }
    }
}
