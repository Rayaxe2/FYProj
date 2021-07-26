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
        int selectedRow;
        public TableControl()
        {
            InitializeComponent();
            dataGridView1.Rows.Add();

            dataGridView1.ClearSelection();
            //dataGridView1.RowsDefaultCellStyle.SelectionBackColor = Color.White;
            //dataGridView1.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridView1.Height = 60;
            this.Height = dataGridView1.Height + textBox1.Height + button1.Height + 40;
            moveAddCellUIDown(-165);
            dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.BackColor = Color.Gray;
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex == dataGridView1.RowCount - 1)
            {
                selectedRow = dataGridView1.RowCount - 1;
                dataGridView1.ClearSelection();
                checkBox1.Checked = false;
                textBox1.Text = "";
            }
            else {
                textBox1.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();

                if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value != null)
                {
                    if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString() == "Yes")
                    {
                        checkBox1.Checked = true;
                    }
                }
                else {
                    checkBox1.Checked = false;
                }
                selectedRow = dataGridView1.CurrentRow.Index;

            }
            
            //MessageBox.Show(dataGridView1.CurrentCell.RowIndex.ToString());
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex == dataGridView1.RowCount - 1)
            {
                dataGridView1.ClearSelection();

                checkBox1.Checked = false;
                textBox1.Text = "";
            }
            //selectedRow = dataGridView1.CurrentRow.Index;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex == dataGridView1.RowCount - 1)
            {
                dataGridView1.ClearSelection();
                checkBox1.Checked = false;
                textBox1.Text = "";
            }
            //selectedRow = dataGridView1.CurrentRow.Index;
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((dataGridView1.RowCount - 1) != selectedRow) {
                string nameBeforeEdit = dataGridView1.Rows[selectedRow].Cells[0].Value.ToString();

                dataGridView1.Rows[selectedRow].Cells[0].Value = textBox1.Text;
                if (checkBox1.Checked == true) {
                    dataGridView1.Rows[selectedRow].Cells[1].Value = "Yes";
                }
                else {
                    dataGridView1.Rows[selectedRow].Cells[1].Value = "";
                }

                //MainForm.myERModel.findEntity(this.Parent.Text).removeField(new EntityField(textBox1.Text, checkBox1.Checked));
                MainForm.myERModel.findEntity(this.Parent.Text).editField(nameBeforeEdit, textBox1.Text, checkBox1.Checked);

                checkBox1.Checked = false;
                textBox1.Text = "";
                dataGridView1.ClearSelection();
                selectedRow = dataGridView1.RowCount - 1;
            }
            
        }   

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please provide a name for the field");
            }
            else
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.RowCount - 2].Cells["Column1"].Value = textBox1.Text;
                if (checkBox1.Checked == true)
                {
                    dataGridView1.Rows[dataGridView1.RowCount - 2].Cells["Column2"].Value = "Yes";
                }

                MainForm.myERModel.findEntity(this.Parent.Text).addField(new EntityField(textBox1.Text, checkBox1.Checked));

                dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.BackColor = Color.Gray;
                dataGridView1.Rows[dataGridView1.RowCount - 2].DefaultCellStyle.BackColor = default;

                dataGridView1.Height = dataGridView1.Height + 33;
                moveAddCellUIDown(33);
                textBox1.Text = "";
                checkBox1.Checked = false;
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((dataGridView1.RowCount - 1) != selectedRow) {
                MainForm.myERModel.findEntity(this.Parent.Text).removeField(dataGridView1.Rows[selectedRow].Cells[0].Value.ToString());

                dataGridView1.Rows.RemoveAt(selectedRow);

                moveAddCellUIDown(-33);
                dataGridView1.Height = dataGridView1.Height - 33;
                checkBox1.Checked = false;
                textBox1.Text = "";
                dataGridView1.ClearSelection();
                selectedRow = dataGridView1.RowCount - 1;
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

        private void TableControl_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
