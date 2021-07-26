using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FYProj
{
    public partial class renameTableForm : Form
    {
        public string input;
        public bool actionCanceled = false;
        public renameTableForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please provide a valid name");
            }
            else if (MainForm.myERModel.findEntity(textBox1.Text) != null)
            {
                MessageBox.Show("A table with this name already exists, please input a different name for the table");
            }
            else
            {
                input = textBox1.Text;
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            actionCanceled = true;
            this.Close();
        }
    }
}
