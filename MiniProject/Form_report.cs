using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data.SqlClient;

namespace MiniProject
{
    public partial class Form_report : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True");
        double student_level = 0;
        double max_level = 0;
        int RubricId = 0;
        double total_marks = 0;
        double obtained_marks = 0;
        string assessment_name = "";
        int assessment_id = 0;
        double assessment_total_marks = 0;
        public Form_report()
        {
            InitializeComponent();
            error_msg.Hide();
        }

        public void calculate(string RubricMeasurementId, string AssessmentComponentId)
        {
            //conn.Open();
            using (SqlCommand cmd = new SqlCommand("Select MeasurementLevel from RubricLevel where Id='" + RubricMeasurementId + "'", conn))
            {
                student_level = (int)cmd.ExecuteScalar();
            }

            using (SqlCommand cmd = new SqlCommand("Select RubricId from RubricLevel where Id='" + RubricMeasurementId + "'", conn))
            {
                RubricId = (int)cmd.ExecuteScalar();
            }

            using (SqlCommand cmd = new SqlCommand("SELECT MAX(MeasurementLevel) FROM RubricLevel WHERE RubricId='" + RubricId + "'", conn))
            {
                max_level = (int)cmd.ExecuteScalar();
            }

            using (SqlCommand cmd = new SqlCommand("SELECT TotalMarks FROM AssessmentComponent WHERE Id='" + AssessmentComponentId + "'", conn))
            {
                total_marks = (int)cmd.ExecuteScalar();
            }

            using (SqlCommand cmd = new SqlCommand("SELECT AssessmentId FROM AssessmentComponent WHERE Id='" + AssessmentComponentId + "'", conn))
            {
                assessment_id = (int)cmd.ExecuteScalar();
            }

            using (SqlCommand cmd = new SqlCommand("SELECT Title FROM Assessment WHERE Id='" + assessment_id + "'", conn))
            {
                assessment_name = (string)cmd.ExecuteScalar();
            }
            

            using (SqlCommand cmd = new SqlCommand("SELECT TotalMarks FROM Assessment WHERE Id='" + assessment_id + "'", conn))
            {
                assessment_total_marks = (int)cmd.ExecuteScalar();
            }

            obtained_marks = (student_level / max_level) * total_marks;
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            try
            {
                if(combo_assessment.Text != "")
                {
                    error_msg.Hide();
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "PDF File|*.pdf";
                    saveFileDialog1.Title = "Save an Image File";
                    saveFileDialog1.ShowDialog();
                    if (saveFileDialog1.FileName != "")
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT Title FROM Assessment WHERE Id='" + (combo_assessment.SelectedItem as ComboboxItem).Value.ToString() + "'", conn))
                        {
                            assessment_name = (string)cmd.ExecuteScalar();
                        }

                        // PDF PRINTING HERE
                        Document document = new Document();
                        BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);

                        BaseFont bf1 = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        iTextSharp.text.Font font1 = new iTextSharp.text.Font(bf1, 10, iTextSharp.text.Font.NORMAL);

                        PdfWriter.GetInstance(document, new FileStream(saveFileDialog1.FileName, FileMode.Create));
                        document.Open();
                        Paragraph p = new Paragraph("Assesment Result Report");
                        p.Alignment = Element.ALIGN_CENTER;
                        document.Add(p);
                        p = new Paragraph("Assesment Name: " + assessment_name);
                        p.Alignment = Element.ALIGN_CENTER;
                        document.Add(p);
                        p = new Paragraph("  ");
                        p.Alignment = Element.ALIGN_CENTER;
                        document.Add(p);
                        PdfPTable table = new PdfPTable(5);
                        PdfPCell cell = new PdfPCell(new Phrase("Name", font1));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(242, 243, 244);
                        cell.HorizontalAlignment = 0;
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Phone#", font1));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(242, 243, 244);
                        cell.HorizontalAlignment = 0;
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Email", font1));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(242, 243, 244);
                        cell.HorizontalAlignment = 0;
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Obtained Marks", font1));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(242, 243, 244);
                        cell.HorizontalAlignment = 0;
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Total Marks", font1));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(242, 243, 244);
                        cell.HorizontalAlignment = 0;
                        table.AddCell(cell);
                        using (SqlConnection connection = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True"))
                        {
                            connection.Open();
                            using (SqlCommand oCmd = new SqlCommand("Select * from Student", connection))
                            {
                                using (SqlDataReader oReader = oCmd.ExecuteReader())
                                {
                                    while (oReader.Read())
                                    {
                                        double total_marks_in_assignment = 0;
                                        using (SqlConnection connection1 = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True"))
                                        {
                                            connection1.Open();
                                            using (SqlCommand oCmd1 = new SqlCommand("Select * from AssessmentComponent where AssessmentId = '" + (combo_assessment.SelectedItem as ComboboxItem).Value.ToString() + "'", connection1))
                                            {
                                                using (SqlDataReader oReader1 = oCmd1.ExecuteReader())
                                                {
                                                    while (oReader1.Read())
                                                    {
                                                        using (SqlConnection connection2 = new SqlConnection("Data Source=NUMAIRPC;Initial Catalog=ProjectB;Integrated Security=True"))
                                                        {
                                                            connection2.Open();
                                                            using (SqlCommand oCmd2 = new SqlCommand("Select * from StudentResult where StudentId = '" + Convert.ToString(oReader["Id"]) + "' AND AssessmentComponentId = '"+Convert.ToString(oReader1["Id"])+"'", connection2))
                                                            {
                                                                using (SqlDataReader oReader2 = oCmd2.ExecuteReader())
                                                                {
                                                                    while (oReader2.Read())
                                                                    {
                                                                        calculate(oReader2["RubricMeasurementId"].ToString(), oReader2["AssessmentComponentId"].ToString());
                                                                        total_marks_in_assignment += obtained_marks;
                                                                    }

                                                                    connection2.Close();
                                                                }
                                                            }
                                                        }
                                                    }

                                                    connection1.Close();
                                                }
                                            }
                                        }
                                        if (total_marks_in_assignment != 0)
                                        {
                                            // YAHAN BAQI CODE ANAA HAI
                                            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(oReader["FirstName"]) + " " + Convert.ToString(oReader["LastName"]), font)));
                                            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(oReader["Contact"]), font)));
                                            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(oReader["Email"]), font)));
                                            table.AddCell(new PdfPCell(new Phrase(total_marks_in_assignment.ToString(), font)));
                                            table.AddCell(new PdfPCell(new Phrase(assessment_total_marks.ToString(), font)));
                                        }
                                    }
                                    document.Add(table);
                                    document.Close();
                                    connection.Close();
                                    MessageBox.Show("PDF File Generated Successfully");
                                    this.Hide();
                                }
                            }
                        }
                    }
                }
                else
                {
                    error_msg.Text = "Please Fill In All The Required Fields";
                    error_msg.Show();
                }
            }
            catch(Exception a)
            {
                error_msg.Text = "File with this name is open close it first";
                error_msg.Show();
            }
        }

        private void Form_report_Load(object sender, EventArgs e)
        {
            conn.Open();
            string query2 = "Select * from Assessment";
            SqlCommand command2 = new SqlCommand(query2, conn);
            using (SqlDataReader reader = command2.ExecuteReader())
            {
                while (reader.Read())
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = Convert.ToString(reader["Title"]);
                    item.Value = Convert.ToString(reader["Id"]);
                    
                    combo_assessment.Items.Add(item);

                    //combo_clo.SelectedIndex = 0;
                }
            }
        }

        private void combo_assessment_SelectedIndexChanged(object sender, EventArgs e)
        {
            error_msg.Hide();
        }
    }
}
