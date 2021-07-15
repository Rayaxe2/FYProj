using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace FYProj
{
    public partial class Form1 : Form
    {
        EventHandler globalEventHolder;
        int GBNum = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Click(object sender, EventArgs e)
        {
            //If the globalEventHolder eventhandler is associated to the click event for the form, it is removed
            this.Click -= globalEventHolder;

            //TEST CODE
            //this.Click += new System.EventHandler(this.Form1_Click);
            //The above added to form1.designer.cs
            //MessageBox.Show("TEST");
        }

        //When Button is clicked....
        private void button1_Click(object sender, EventArgs e)
        {
            //Creates the schematics for an eventhandler and stores it in the object addTableEvent
            EventHandler addTableEvent = (s, e) => {
                //Creates new groupbox compoment for the form
                GroupBox gb = new GroupBox();
                gb.Name = "Test " + GBNum.ToString();
                //Places the groupbox on the form relative to where the mouse is when clicked on the form
                gb.Location = this.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y));
                gb.AutoSize = true;
                gb.Text = "Test " + GBNum.ToString();
                GBNum += 1;

                MouseEventHandler dragTableEvent = (s, e) => {
                    //Moves groupbox to mouse location while moving the mouse
                    //(this event handler is added when you click down/hold mouse click on a groupbox and removed when you click up/release mouse click)
                    gb.Location = this.PointToClient(new Point(Cursor.Position.X - 15, Cursor.Position.Y - 10));
                }; 

                //Makes groupbox follow mouse when holding mouse click
                gb.MouseDown += new MouseEventHandler(
                    (s, e) => {
                        gb.MouseMove += dragTableEvent;
                    }
                );

                //Makes groupbox stop following mouse when mouse click is released
                gb.MouseUp += new MouseEventHandler(
                    (s, e) => {
                        gb.MouseMove -= dragTableEvent;
                    }
                );

                this.Controls.Add(gb);
            };
            //Stores the eventhandeler object in the global eventhandler object "globalEventHolder" 
            globalEventHolder = addTableEvent;
            //When form is clicked again, the eventhandler in triggered (this adds the eventhandler to the form's click event)
            this.Click += globalEventHolder;
        }



        //TEST CODE
        /*
            Label lbl = new Label();
            lbl.Name = "Label";
            lbl.Location = new Point(100, 100);
            lbl.AutoSize = true;
            lbl.Text = "Test";
            this.Controls.Add(lbl);

            GroupBox gb = new GroupBox();
            gb.Name = "Label";
            gb.Location = new Point(110, 101);
            gb.AutoSize = true;
            gb.Text = "Test";
            this.Controls.Add(gb);

            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;

            MessageBox.Show(y.ToString());






        private void Form1_Load(object sender, EventArgs e)
        {
            //SetupClickEvents(this);
        }

        private void SetupClickEvents(Control container)
        {
            foreach (Control control in container.Controls)
            {
                control.Click += HandleClicks;
            }
        }
        private void HandleClicks(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            MessageBox.Show(string.Format("{0} was clicked!", control.Name));
        }








        gb.Click += new EventHandler(
                    (s, e) => {
                        MessageBox.Show("REEEEEE");
                    }
                );








        MouseEventHandler dragTableEvent = (s, e) => {
                    //gb.Location = (Point) Point.Subtract(this.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y)), (Size) gb.Location);
                    //gb.Location = Point.Add(this.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y)), (Size)Point.Subtract(this.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y)), (Size)gb.Location));
                    /* gb.Location = Point.Add(
                        gb.Location, 
                        (Size) Point.Subtract(
                            this.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y)), 
                            (Size) gb.Location
                        )
                    ); 
                    if (!(this.Bounds.Contains(this.PointToClient(MousePosition)))) { 
                        gb.Location = this.PointToClient(new Point(Cursor.Position.X - 50, Cursor.Position.Y - 50));
                    }

                    Point oldLocation = gb.Location;
                    gb.Location = this.PointToClient(new Point(Cursor.Position.X - (Cursor.Position.X - oldLocation.X), Cursor.Position.Y - (Cursor.Position.Y - oldLocation.Y)));
                    MessageBox.Show(gb.Location.ToString());

        gb.Location = this.PointToClient(new Point(Cursor.Position.X - 15, Cursor.Position.Y - 10));
                };

         */
    }
}
