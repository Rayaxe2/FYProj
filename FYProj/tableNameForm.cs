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
    public partial class tableNameForm : Form
    {
        public bool actionCanceled = false;
        public string tableName;

        public tableNameForm()
        {
            InitializeComponent();
        }

        private void tableNameForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            actionCanceled = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") {
                MessageBox.Show("Please provide a name");
            }
            else if (MainForm.myERModel.findEntity(textBox1.Text) != null) {
                MessageBox.Show("A table with this name already exists, please input a different name for the table");
            }
            else {
                tableName = textBox1.Text;
                this.Close();
            }
        }
    }
}
