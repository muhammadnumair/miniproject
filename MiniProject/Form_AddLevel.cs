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
        string RubricName;
        string CloId;
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
            combo_rubrics.Enabled = true;
            SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
            conn.Open();
            string query2 = "Select * from Rubric where Id = '"+RubricId+"'";
            SqlCommand command2 = new SqlCommand(query2, conn);
            using (SqlDataReader reader = command2.ExecuteReader())
            {
                while (reader.Read())
                {
                    CloId = Convert.ToString(reader["CloId"]);
                    break;
                }
            }

            string query1 = "Select * from Rubric where CloId = '"+CloId+"'";
            SqlCommand command = new SqlCommand(query1, conn);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = Convert.ToString(reader["Details"]);
                    item.Value = Convert.ToString(reader["Id"]);
                    if (Convert.ToString(RubricId) == Convert.ToString(reader["Id"]))
                    {
                        RubricName = Convert.ToString(reader["Details"]);
                    }
                    combo_rubrics.Items.Add(item);

                    //combo_clo.SelectedIndex = 0;
                }
            }
            if (RubricName != null)
            {
                combo_rubrics.SelectedIndex = combo_rubrics.FindStringExact(RubricName);
            }
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            if(text_details.Text != "" && text_level.Text != "" && text_level.Text.All(c => char.IsDigit(c)))
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
                    string query = "Update RubricLevel SET Details = '" + text_details.Text + "',MeasurementLevel = '" + text_level.Text + "', RubricId = '"+ (combo_rubrics.SelectedItem as ComboboxItem).Value.ToString() + "'  where Id = '" + Id + "'";
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
                if(!text_level.Text.All(c => char.IsDigit(c)))
                {
                    error_msg.Text = "Level Must Be Numeric Value";
                }
                else
                {
                    error_msg.Text = "Please fill in all required fields";
                }
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
