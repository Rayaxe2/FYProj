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
    public partial class TableControl : UserControl
    {
        public TableControl()
        {
            InitializeComponent();
            dataGridView1.Rows.Add();
            dataGridView1.RowsDefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.Height = 60;
            this.Height = dataGridView1.Height + textBox1.Height + button1.Height + 40;
            moveAddCellUIDown(-165);
            dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.BackColor = Color.Gray;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void TableControl_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please provide a name");
            }
            else
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.RowCount - 2].Cells["Column1"].Value = textBox1.Text;
                if (checkBox1.Checked == true)
                {
                    dataGridView1.Rows[dataGridView1.RowCount - 2].Cells["Column2"].Value = "\u221A";
                }
                dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.BackColor = Color.Gray;
                dataGridView1.Rows[dataGridView1.RowCount - 2].DefaultCellStyle.BackColor = default;

                dataGridView1.Height = dataGridView1.Height + 33;

                moveAddCellUIDown(32);
                textBox1.Text = "";
                checkBox1.Checked = false;
            }
        }

        //Helper Functions
        private void moveAddCellUIDown(int distanceDown)
        {
            textBox1.Location = new Point(textBox1.Location.X, textBox1.Location.Y + distanceDown);
            checkBox1.Location = new Point(checkBox1.Location.X, checkBox1.Location.Y + distanceDown);
            button1.Location = new Point(button1.Location.X, button1.Location.Y + distanceDown);
            button2.Location = new Point(button2.Location.X, button2.Location.Y + distanceDown);
            button3.Location = new Point(button3.Location.X, button3.Location.Y + distanceDown);
        }
    }
}
