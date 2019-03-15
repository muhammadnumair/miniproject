﻿using System;
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
    public partial class Form_AddRubric : Form
    {
        int CloId;
        int Id;
        public Form_AddRubric(int id)
        {
            CloId = id;
            InitializeComponent();
        }

        public Form_AddRubric(string[] values)
        {
            InitializeComponent();
            Id = Convert.ToInt32(values[0]);
            text_details.Text = values[1];
            CloId = Convert.ToInt32(values[2]);
            add_button.Text = "Update Record";
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            if(text_details.Text != "")
            {
                SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
                conn.Open();

                if (add_button.Text == "Add Rubric")
                {
                    string query = "Insert into Rubric (Details, CloId) values('" + text_details.Text + "', '" + CloId + "')";
                    SqlCommand command = new SqlCommand(query, conn);
                    int i = command.ExecuteNonQuery();
                    if (i != 0)
                    {
                        MessageBox.Show("Rubric Record Inserted Successfully");
                    }
                    conn.Close();
                }
                else
                {
                    string query = "Update Rubric SET Details = '" + text_details.Text + "' where Id = '" + Id + "'";
                    SqlCommand command = new SqlCommand(query, conn);
                    int i = command.ExecuteNonQuery();
                    if (i != 0)
                    {
                        MessageBox.Show("Rubric Record Updated Successfully");
                    }
                    conn.Close();
                }
                foreach (Form f in Application.OpenForms)
                    f.Hide();

                Form_Rubrics s = new Form_Rubrics(CloId);
                s.Show();
            }
            else
            {
                error_msg.Show();
            }
        }

        private void text_details_TextChanged(object sender, EventArgs e)
        {
            error_msg.Hide();
        }

        private void Form_AddRubric_Load(object sender, EventArgs e)
        {
            error_msg.Hide();
        }
    }
}
