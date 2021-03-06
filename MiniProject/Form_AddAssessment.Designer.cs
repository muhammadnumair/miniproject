﻿namespace MiniProject
{
    partial class Form_AddAssessment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.text_title = new System.Windows.Forms.TextBox();
            this.text_marks = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.add_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.text_weightage = new System.Windows.Forms.TextBox();
            this.error_msg = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.5659F));
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 15);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(351, 46);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Trebuchet MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(3, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(283, 35);
            this.label6.TabIndex = 4;
            this.label6.Text = "Add New Assessment";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.text_title, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.text_marks, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.add_button, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.text_weightage, 0, 5);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 80);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 7;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.38135F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.9025F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.60211F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66045F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.60211F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66045F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.19103F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(351, 268);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "Title";
            // 
            // text_title
            // 
            this.text_title.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_title.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.text_title.Location = new System.Drawing.Point(3, 33);
            this.text_title.Name = "text_title";
            this.text_title.Size = new System.Drawing.Size(345, 26);
            this.text_title.TabIndex = 0;
            this.text_title.TextChanged += new System.EventHandler(this.text_title_TextChanged);
            // 
            // text_marks
            // 
            this.text_marks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_marks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_marks.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.text_marks.Location = new System.Drawing.Point(3, 106);
            this.text_marks.Name = "text_marks";
            this.text_marks.Size = new System.Drawing.Size(345, 26);
            this.text_marks.TabIndex = 1;
            this.text_marks.TextChanged += new System.EventHandler(this.text_marks_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.label2.Location = new System.Drawing.Point(3, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 22);
            this.label2.TabIndex = 17;
            this.label2.Text = "Total Marks";
            // 
            // add_button
            // 
            this.add_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.add_button.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.add_button.Location = new System.Drawing.Point(3, 222);
            this.add_button.Name = "add_button";
            this.add_button.Size = new System.Drawing.Size(164, 43);
            this.add_button.TabIndex = 3;
            this.add_button.Text = "Add Assessment";
            this.add_button.UseVisualStyleBackColor = true;
            this.add_button.Click += new System.EventHandler(this.add_button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.label3.Location = new System.Drawing.Point(3, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 22);
            this.label3.TabIndex = 18;
            this.label3.Text = "Total Weightage";
            // 
            // text_weightage
            // 
            this.text_weightage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_weightage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_weightage.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.text_weightage.Location = new System.Drawing.Point(3, 178);
            this.text_weightage.Name = "text_weightage";
            this.text_weightage.Size = new System.Drawing.Size(345, 26);
            this.text_weightage.TabIndex = 2;
            this.text_weightage.TextChanged += new System.EventHandler(this.text_weightage_TextChanged);
            // 
            // error_msg
            // 
            this.error_msg.AutoSize = true;
            this.error_msg.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.error_msg.ForeColor = System.Drawing.Color.Red;
            this.error_msg.Location = new System.Drawing.Point(48, 363);
            this.error_msg.Name = "error_msg";
            this.error_msg.Size = new System.Drawing.Size(278, 22);
            this.error_msg.TabIndex = 9;
            this.error_msg.Text = "Please Fill In All The Required Fields";
            // 
            // Form_AddAssessment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(375, 421);
            this.Controls.Add(this.error_msg);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "Form_AddAssessment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add New Assessment";
            this.Load += new System.EventHandler(this.Form_AddAssessment_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox text_title;
        private System.Windows.Forms.TextBox text_marks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox text_weightage;
        private System.Windows.Forms.Button add_button;
        private System.Windows.Forms.Label error_msg;
    }
}