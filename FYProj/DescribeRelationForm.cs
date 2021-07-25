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
    public partial class Form2 : Form
    {

        public string multiplicity, participation;
        public bool actionCanceled = false;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            actionCanceled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) {
                MessageBox.Show("Please select a Multiplicity");
            }
            else if (comboBox2.SelectedItem == null) {
                MessageBox.Show("Please select a Participation");
            }
            else {
                multiplicity = comboBox1.SelectedItem.ToString().Replace(" ", string.Empty);
                participation = comboBox2.SelectedItem.ToString();

                this.Close();
            }
            
        }
    }
}
