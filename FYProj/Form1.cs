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
    public partial class Form1 : Form
    {
        EventHandler globalEventHolder; //A variable to globally store an event handeler
        int GBNum = 0; //An incrmental value added to the names of new groupboxes

        Point begining, end;
        bool drawingRel = false;
        bool startRecorded = false;

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
        }

        //New Table button
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

                Point preDragLocation = gb.Location;

                MouseEventHandler dragTableEvent = (s, e) => {
                    //Moves groupbox to mouse location while moving the mouse
                    //(this event handler is added when you click down/hold mouse click on a groupbox and removed when you click up/release mouse click)
                    gb.Location = this.PointToClient(new Point(Cursor.Position.X - 15, Cursor.Position.Y - 10));
                }; 

                //Makes groupbox follow mouse when holding mouse click
                gb.MouseDown += new MouseEventHandler(
                    (s, e) => {
                        if(e.Button == MouseButtons.Right) {
                            gb.MouseMove += dragTableEvent;
                        }
                    }
                );

                //Makes groupbox stop following mouse when mouse click is released
                gb.MouseUp += new MouseEventHandler(
                    (s, e) => {
                        if(e.Button == MouseButtons.Right) {
                            gb.MouseMove -= dragTableEvent;

                            //If the groupbox is dragged out of bounds of the client size/form, the movement on it is undone
                            if (gb.Location.X > this.ClientSize.Width || gb.Location.X < 0 || gb.Location.Y > this.ClientSize.Height || gb.Location.Y < 0)
                            {
                                gb.Location = preDragLocation;
                            }
                            else {
                                preDragLocation = gb.Location;
                            }
                        }
                    }
                );

                this.Controls.Add(gb);
            };
            //Stores the eventhandeler object in the global eventhandler object "globalEventHolder" 
            globalEventHolder = addTableEvent;
            //When form is clicked again, the eventhandler in triggered (this adds the eventhandler to the form's click event)
            this.Click += globalEventHolder;
        }

        //Add Relationship Button
        private void button2_Click(object sender, EventArgs e)
        {
            //Creates event handler for drawing lines between 2 group boxes
            EventHandler drawRelationshipEvent = (s, e) => {

                /*
                MouseEventHandler recStart = new MouseEventHandler( //this.MouseDown
                    (s, e) => {
                        if (startRecorded == false)
                        {
                            MessageBox.Show(PointToClient(Cursor.Position).ToString());
                            begining = this.PointToClient(Cursor.Position);
                            startRecorded = true;
                        }
                    }
                );
                this.MouseDown += recStart;

                Graphics surface = CreateGraphics();
                Pen pen1 = new Pen(Color.Black, 2);

                MouseEventHandler drawPreview = new MouseEventHandler( //this.MouseMove
                    (s, e) => {
                        surface.DrawLine(pen1, begining.X, begining.Y, this.PointToClient(Cursor.Position).X, this.PointToClient(Cursor.Position).Y);
                        surface = CreateGraphics();
                    }
                );
                this.MouseMove += drawPreview;

                MouseEventHandler recEnd = new MouseEventHandler( //this.MouseUp
                   (s, e) => {
                       if (startRecorded == true)
                       {
                           end = this.PointToClient(Cursor.Position);
                           startRecorded = false;
                           this.MouseDown -= recStart;
                           this.MouseMove -= drawPreview;
                       }
                   }
               );
                this.MouseUp += recEnd;


                surface.DrawLine(pen1, begining.X, begining.Y, end.X, end.Y);
                //this.MouseDown -= recStart;
                //this.MouseUp -= recEnd;
                //this.MouseMove -= drawPreview;
                */

            };
            //makes event handler globally referencable and assigns it to the form click event click 
            globalEventHolder = drawRelationshipEvent;
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








        private void DrawLineEvent(object sender, PaintEventArgs e, int pointOneX, int pointOneY, int PointTwoX, int PointTwoY) {
            Pen blackPen = new Pen(Color.Black, 3);

            Point point1 = new Point(pointOneX, pointOneY);
            Point point2 = new Point(PointTwoX, PointTwoY);

            e.Graphics.DrawLine(blackPen, point1, point2);
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







        //TEST CODE
            //this.Click += new System.EventHandler(this.Form1_Click);
            //The above added to form1.designer.cs
            //MessageBox.Show("TEST");

         */
        }
}
