using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace FYProj
{
    public partial class Form1 : Form
    {
        EventHandler globalEventHolder; //A variable to globally store an event handeler
        int GBNum = 0; //An incrmental value added to the names of new groupboxes

        String interactionMode = "None";
        GroupBox selectedGroupBox1;

        Graphics surface;
        Pen blackPen = new Pen(Color.Black, 2);
        Pen bluePen = new Pen(Color.Blue, 2);
        Pen redPen = new Pen(Color.Red, 2);

        List<relationshipPoint> RelPoints = new List<relationshipPoint>();
        Point start, end;

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

        //http://csharphelper.com/blog/2015/05/rotate-around-a-point-other-than-the-origin-in-c/
        private Matrix rotateLine(Point anchor, int angle) {
            Matrix rotationResult = new Matrix();
            rotationResult.RotateAt(angle, anchor);
            return rotationResult;
        }

        private Point shrunkLineStartPoint(Point pointOne, Point pointTwo, double shrinkFactor)
        {
            return new Point(
                Convert.ToInt32(pointOne.X + shrinkFactor * (pointTwo.X - pointOne.X)),
                Convert.ToInt32(pointOne.Y + shrinkFactor * (pointTwo.Y - pointOne.Y))
            );
        }
        
        

        private void reDrawForm() {
            if (surface != null) {
                surface.Clear(this.BackColor);
            }
            surface = CreateGraphics();
            Pen blackPen = new Pen(Color.Black, 2);
            Pen redPen = new Pen(Color.Red, 2);

            foreach (relationshipPoint coord in RelPoints) {
                surface.DrawLine(blackPen, coord.start.X, coord.start.Y, coord.end.X, coord.end.Y);

                Graphics lineSymbol = CreateGraphics();
                int lineLength = (int)Math.Sqrt((Math.Pow((coord.start.X - coord.end.X), 2) + Math.Pow((coord.start.Y - coord.end.Y), 2)));
                int shrinkTarget = 130;
                double shrinkFactor = 1 - ((double) shrinkTarget / (double) lineLength);

                Point anchorPoint = shrunkLineStartPoint(coord.start, coord.end, shrinkFactor);
                Point shrunkEndSymbolEnds = shrunkLineStartPoint(coord.end, anchorPoint, 0.3);

                lineSymbol.Transform = rotateLine(anchorPoint, 20);
                lineSymbol.DrawLine(redPen, anchorPoint.X, anchorPoint.Y, shrunkEndSymbolEnds.X, shrunkEndSymbolEnds.Y);
                lineSymbol.Transform = rotateLine(anchorPoint, 340);
                lineSymbol.DrawLine(redPen, anchorPoint.X, anchorPoint.Y, shrunkEndSymbolEnds.X, shrunkEndSymbolEnds.Y);
            }

            
        }

        private bool isRelEstablished(GroupBox gbOne, GroupBox gbTwo) {
            foreach (relationshipPoint rel in RelPoints) {
                if ((rel.gb1 == gbOne || rel.gb1 == gbTwo) && (rel.gb2 == gbOne || rel.gb2 == gbTwo)) {
                    return true;
                }
            }
            return false;
        }

        private void recordTableRelocation(GroupBox gbOne, Point newPoint)
        {
            foreach (relationshipPoint rel in RelPoints)
            {
                if (rel.gb1 == gbOne)
                {
                    rel.start = newPoint;
                }

                if (rel.gb2 == gbOne)
                {
                    rel.end = newPoint;
                }
            }
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
                    recordTableRelocation(gb, new Point((gb.Location.X + (gb.Size.Width / 2)), (gb.Location.Y + (gb.Size.Height / 2))));
                    reDrawForm();
                }; 

                //Makes groupbox follow mouse when holding mouse click
                gb.MouseDown += new MouseEventHandler(
                    (s, e) => {
                        if(e.Button == MouseButtons.Right) {
                            gb.MouseMove += dragTableEvent;
                            gb.ForeColor = Color.Red;
                        }
                    }
                );

                //Makes groupbox stop following mouse when mouse click is released
                gb.MouseUp += new MouseEventHandler(
                    (s, e) => {
                        if (e.Button == MouseButtons.Right)
                        {
                            gb.MouseMove -= dragTableEvent;
                            gb.ForeColor = default;

                            //If the groupbox is dragged out of bounds of the client size/form, the movement on it is undone
                            if (gb.Location.X > this.ClientSize.Width || gb.Location.X < 0 || gb.Location.Y > this.ClientSize.Height || gb.Location.Y < 0)
                            {
                                gb.Location = preDragLocation;
                            }
                            else
                            {
                                preDragLocation = gb.Location;
                            }
                        }
                    }
                );

                //When groupboxes are selected while in the add relationship mode, the colour is changed to indicate that they have been selected
                gb.Click += new EventHandler(
                    async (s, e) =>
                    {
                        if (interactionMode == "AddRel1")
                        {
                            gb.BackColor = Color.Red;
                            selectedGroupBox1 = gb;
                            interactionMode = "AddRel2";

                            start = new Point((gb.Location.X + (gb.Size.Width / 2)), (gb.Location.Y + (gb.Size.Height / 2)));
                        }
                        else if (interactionMode == "AddRel2")
                        {
                            gb.BackColor = Color.Blue;
                            await Task.Delay(300);
                            gb.BackColor = default;
                            selectedGroupBox1.BackColor = default;
                            interactionMode = "None";

                            if (isRelEstablished(gb, selectedGroupBox1) == true) {
                                MessageBox.Show("There is already a relationship between these two tables!");
                            }
                            else
                            {
                                end = new Point((gb.Location.X + (gb.Size.Width / 2)), (gb.Location.Y + (gb.Size.Height / 2)));
                                relationshipPoint gbLocations = new relationshipPoint(start, end, selectedGroupBox1, gb, "Nothing");

                                RelPoints.Add(gbLocations);

                                reDrawForm();
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
            //Changes the interactions mode to "AddRel1" so the groupboxes can react to be selected
            if (interactionMode == "None")
            {
                interactionMode = "AddRel1";
            }

            //Creates event handler for drawing lines between 2 group boxes
            EventHandler drawRelationshipEvent = (s, e) =>
            {

            };
            //makes event handler globally referencable and assigns it to the form click event click 
            globalEventHolder = drawRelationshipEvent;
            this.Click += globalEventHolder;



        }

    }
}
