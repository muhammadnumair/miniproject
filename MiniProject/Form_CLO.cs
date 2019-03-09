using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniProject
{
    public partial class Form_CLO : Form
    {
        public Form_CLO()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form_CLO f = new Form_CLO();
            f.Show();
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            add_clo ac = new add_clo();
            ac.Show();
        }
    }
}
