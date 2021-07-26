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
    public partial class MainForm : Form
    {
        ERModel myERModel = new ERModel();

        EventHandler globalEventHolder; //Stores a globally recognised event 
        EventHandler noEvent = (s, e) => {};
        int GBNum = 0; //An incrmental value added to the names of new groupboxes

        String interactionMode = "None"; //Indicates the active mode during GUI interactions
        GroupBox selectedGroupBox1; //Globally holds a reference to an assigned groupbox control/object

        Graphics formCanvas; //Used to draw relationship lines on the form
        //Different coloured pens used for graphics
        Pen blackPen = new Pen(Color.Black, 2);
        Pen bluePen = new Pen(Color.Blue, 2);
        Pen redPen = new Pen(Color.Red, 2);

        //Stores the coordinates of groupboxes which hold tables that have been given a relationship
        //This is used to determine where to redraw the lines and symbols between tables/groupboxes with relationships when they are moved in on the form
        List<relationshipPoint> RelPoints = new List<relationshipPoint>();
        Point start, end; //Globally stores a starting point and and ending point

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Form click event
        private void Form1_Click(object sender, EventArgs e)
        {
            //If the globalEventHolder eventhandler is associated to the click event for the form, it is removed when the form is clicked
            this.Click -= globalEventHolder;
            enableMainFormButtons();
        }

        //New Table button - button1 click event
        private void button1_Click(object sender, EventArgs e)
        {
            bool actionCanceled;
            string tableName;



            using (tableNameForm nameTable = new tableNameForm())
            {
                nameTable.ShowDialog();
                tableName = nameTable.tableName;
                actionCanceled = nameTable.actionCanceled;
            }

            if (actionCanceled == false)
            {
                disableMainFormButtons();

                myERModel.addEntity(new Entity(tableName));

                //Creates the schematics for an eventhandler which adds a new groupbox to a click location on a form.
                EventHandler addTableEvent = (s, e) =>
                {
                    //Creates a new groupbox compoment for the form
                    GroupBox gb = new GroupBox();
                    gb.Name = tableName + GBNum.ToString(); //GBNum adds a unique numerical value to the name
                    gb.Font = new Font(gb.Font.Name, 10);
                    //Places the groupbox on the form relative to where the mouse is when clicked on the form
                    gb.Location = this.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y));
                    gb.AutoSize = true; //The Groupbox resized automatically
                    gb.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    gb.Padding = new Padding(5);
                    gb.Margin = new Padding(2);
                    gb.Text = tableName;
                    GBNum += 1; //This is incremented so the next groupbox has a unique suffix to add to it's name
                    Point preDragLocation = gb.Location; //Holds the original location the groupbox was placed in

                    Control innerTable = new TableControl();

                    gb.Controls.Add(innerTable);
                    gb.Controls.Find(innerTable.Name, false)[0].Location = new Point(9, 50);

                    //This eventhandler is later added to the newly created groupbox to make it dragable with a right click input
                    MouseEventHandler dragTableEvent = (s, e) =>
                    {
                    //Moves groupbox to mouse location while moving the mouse
                    //(this event handler is added when you click down/hold mouse click on a groupbox and removed when you click up/release mouse click)
                    gb.Location = this.PointToClient(new Point(Cursor.Position.X - 15, Cursor.Position.Y - 10));
                        recordTableRelocation(gb, new Point((gb.Location.X + (gb.Size.Width / 2)), (gb.Location.Y + (gb.Size.Height / 2))));
                        reDrawForm();
                    };

                    //Makes groupbox follow mouse when holding mouse click
                    gb.MouseDown += new MouseEventHandler(
                        (s, e) =>
                        {
                        //Adds the aforemention event handler to mouse move event so, while the groupbox is being dragged with right clicked...
                        //The user can see the group box move with the mosue and updates being made to the for's appearance
                        if (e.Button == MouseButtons.Right)
                            {
                                gb.MouseMove += dragTableEvent;
                                gb.ForeColor = Color.Red; //Changes the colour of the groupbox's title while it is being right clicked/moved
                        }
                        }
                    );

                    //Makes groupbox stop following mouse when mouse click is released
                    gb.MouseUp += new MouseEventHandler(
                        (s, e) =>
                        {
                        //Removes aforementioned event handler once the right click button has been released so the groupbox doesn't follow the mouse
                        if (e.Button == MouseButtons.Right)
                            {
                                gb.MouseMove -= dragTableEvent;
                                gb.ForeColor = default; //sets the colour of the group box title to it's default colour

                            //If the groupbox is dragged out of bounds of the client size/form, the movement on it is undone
                            //This prevents the user from placing the groupbox out of view
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
                    //The mode interaction mode is changed to reflect the state of the interaction and selected groupboxes and their locations are recorded
                    gb.Click += new EventHandler(
                        async (s, e) => //aysnc lamda function used so when the second groupbox is selected, it's background colour could stay blue for a while (via a task delay) before setting it back to it's default colour and recording details
                        {
                        //In the "AddRel1" interaction mode, the first selected groupbox's location is recorded and the mode is set to "AddRel2"
                        if (interactionMode == "AddRel1")
                            {
                                gb.BackColor = Color.Red; //The first selected groupbox has it's background colour temporarily set to red
                            selectedGroupBox1 = gb; //The selected groupbox is globally recorded to remember later
                            interactionMode = "AddRel2";

                                start = new Point((gb.Location.X + (gb.Size.Width / 2)), (gb.Location.Y + (gb.Size.Height / 2))); //The center of the groupbox is stored as it's location
                        }
                        //In the "AddRel2" interaction mode, the second selected groupbox's location is recorded and the mode is set back to "None"
                        else if (interactionMode == "AddRel2")
                            {
                                gb.BackColor = Color.Blue; //Temporarilily makes the background of the selected groupbox blue
                            await Task.Delay(300); //Pauses the execution for a while before setting the groupbox's background colour back to it's default
                            gb.BackColor = default;
                                selectedGroupBox1.BackColor = default;
                                interactionMode = "None";

                            //Prevents the establishment of a relationship tables that already have a relationship
                            if (isRelEstablished(gb, selectedGroupBox1) == true)
                                {
                                    MessageBox.Show("There is already a relationship between these two tables!");
                                }
                            //Add support later
                            else if (gb == selectedGroupBox1)
                                {
                                    MessageBox.Show("Self relationships are not supported yet!");
                                }
                            //If the relationship is new/unrecorded, it is recorded in the list of "relationshipPoints" (RelPoints)
                            else
                                {
                                    string MultAndPart = "Unspecified";
                                    bool actionCanceled;
                                //Opens form where user sepcifies the relationship between tables
                                using (Form2 DescRelationForm = new Form2())
                                    {
                                        DescRelationForm.ShowDialog();
                                        MultAndPart = DescRelationForm.multiplicity + " " + DescRelationForm.participation;
                                        actionCanceled = DescRelationForm.actionCanceled;
                                    }

                                    if (actionCanceled == false)
                                    {
                                        end = new Point((gb.Location.X + (gb.Size.Width / 2)), (gb.Location.Y + (gb.Size.Height / 2)));
                                        relationshipPoint gbLocations = new relationshipPoint(start, end, selectedGroupBox1, gb, MultAndPart);

                                        RelPoints.Add(gbLocations);

                                        myERModel.addRelationship(
                                            new Relationship(
                                                MultAndPart.Split(' ')[0], 
                                                MultAndPart.Split(' ')[1], 
                                                myERModel.findEntity(gb.Text), 
                                                myERModel.findEntity(selectedGroupBox1.Text)
                                            )
                                        );

                                        //The graphics on the from are redrawn to add graphics to represent the new relationship
                                        reDrawForm();
                                    }
                                }
                            }
                            else if (interactionMode == "RemoveRel1")
                            {
                                gb.BackColor = Color.Green;
                                selectedGroupBox1 = gb;
                                interactionMode = "RemoveRel2";
                            }
                            else if (interactionMode == "RemoveRel2")
                            {
                                gb.BackColor = Color.Yellow;
                                await Task.Delay(300);
                                gb.BackColor = default;
                                selectedGroupBox1.BackColor = default;
                                interactionMode = "None";

                                if (isRelEstablished(gb, selectedGroupBox1) == false)
                                {
                                    MessageBox.Show("There is no relationship between the selected tables!");
                                }
                            //Add support later
                            else if (gb == selectedGroupBox1)
                                {
                                    MessageBox.Show("Self relationships are not supported yet!");
                                }
                                else
                                {
                                    relationshipPoint relation = findEstablishedRel(gb, selectedGroupBox1);

                                    if (relation == null)
                                    {
                                        MessageBox.Show("There is no relationship between the selected tables!");
                                    }
                                    else
                                    {
                                        RelPoints.Remove(relation);
                                    }

                                    myERModel.removeRelationship(
                                        myERModel.findRelationship(
                                            gb.Text,
                                            selectedGroupBox1.Text
                                        )
                                    );

                                    reDrawForm();
                                }
                            }
                            else if (interactionMode == "deleteTable")
                            {
                                gb.BackColor = Color.White;
                                await Task.Delay(300);

                                List<relationshipPoint> tableRels = findAllEstablishedRels(gb);
                                foreach (relationshipPoint relation in tableRels)
                                {
                                    RelPoints.Remove(relation);
                                }
                                this.Controls.Remove(gb);
                                interactionMode = "None";

                                myERModel.removeEntity(
                                    myERModel.findEntity(gb.Text)
                                );

                                reDrawForm();
                            }
                        }
                    );
                    //The above event handlers are added to the newly created groupboxes
                    this.Controls.Add(gb);

                };
                //Stores the eventhandeler object in the global eventhandler object "globalEventHolder" 
                globalEventHolder = addTableEvent;
                //When form is clicked again, the eventhandler in triggered (this adds the eventhandler to the form's click event)
                this.Click += globalEventHolder;
            }
        }

        //Add Relationship Button
        private void button2_Click(object sender, EventArgs e)
        {
            globalEventHolder = noEvent;

            //Changes the interactions mode to "AddRel1" so the groupboxes can react to be selected
            if (interactionMode == "None")
            {
                interactionMode = "AddRel1";
            }

            /*
            //Creates event handler for drawing lines between 2 group boxes
            EventHandler drawRelationshipEvent = (s, e) =>
            {

            };
            //makes event handler globally referencable and assigns it to the form click event click 
            globalEventHolder = drawRelationshipEvent;
            this.Click += globalEventHolder;
            */

        }

        private void button3_Click(object sender, EventArgs e)
        {
            globalEventHolder = noEvent;
            if (interactionMode == "None")
            {
                interactionMode = "RemoveRel1";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            globalEventHolder = noEvent;
            if (interactionMode == "None")
            {
                interactionMode = "deleteTable";
            }
        }

        //HELPER FUNCTIONS

        //Clears the form (if it has graphics on it) and redraws/draws relevant graphics onto the form
        private void reDrawForm()
        {
            if (formCanvas != null)
            {
                formCanvas.Clear(this.BackColor);
            }
            formCanvas = CreateGraphics();
            Pen blackPen = new Pen(Color.Black, 2);
            Pen redPen = new Pen(Color.Red, 2);

            //For each relationship between groupboxes recorded in relPoints, lines between the relationships are drawn and the relavant symbols are added to the lines
            foreach (relationshipPoint relationship in RelPoints)
            {
                Point[] clippedPoints = drawRelationshipLines(relationship);
                //formCanvas.DrawLine(blackPen, coord.start.X, coord.start.Y, coord.end.X, coord.end.Y);
                drawLineSymbols(clippedPoints[0], clippedPoints[1], relationship.relType);
            }
        }

        private Point[] drawRelationshipLines(relationshipPoint GBCoords)
        {
            double preciseLineAngle = Math.Abs(((Math.Atan2((GBCoords.start.X - GBCoords.end.X), (GBCoords.start.Y - GBCoords.end.Y)) * 180 / Math.PI) + 180) - 360);
            double lineAngle = Math.Round(preciseLineAngle, 1);
            int lineLength = (int)Math.Sqrt((Math.Pow((GBCoords.start.X - GBCoords.end.X), 2) + Math.Pow((GBCoords.start.Y - GBCoords.end.Y), 2)));

            Point[][] clipPoints = GBCoords.getGBPoints();
            Point startPoint, endPoint;

            double progressTowardNextClip = 0;

            double GBOneWidth = GBCoords.gb1.Width;
            double GBTwoWidth = GBCoords.gb2.Width;
            double GBOneHieght = GBCoords.gb1.Height;
            double GBTwoHieght = GBCoords.gb2.Height;

            /*
            Top-mid to Bottom-mid (|): 337.6 - 22.5
            Top-right to Bottom-left (/): 22.6 - 67.5
            Right-mid to Left-mid (-): 67.6 - 112.5
            Right-bottom to Left-top (\): 112.5 - 157.5
            Bottom-mid to Top-mid (|): 157.6 - 202.5
            Bottom-left to Top-right (/): 202.6 - 247.5
            Left-mid to Right-mid (-): 247.6 - 292.5
            Left-top to Right-bottom (\): 292.6 - 337.5
             */

            //Bottom-mid to Top-mid(|): 337.6 - 22.5
            if (lineAngle >= 337.6 || (lineAngle >= 0 && lineAngle <= 22.5))
            {
                startPoint = clipPoints[0][5];
                endPoint = clipPoints[1][1];


                if (lineAngle < 360 && lineAngle >= 22.5)
                {
                    progressTowardNextClip = ((360 - lineAngle) / 22.5);
                    startPoint.X = startPoint.X + (int)((GBTwoWidth / 2) * progressTowardNextClip);
                    //endPoint.X = endPoint.X - (int)((GBOneWidth / 2) * progressTowardNextClip);
                }
                else
                {
                    progressTowardNextClip = (lineAngle / 22.5);
                    startPoint.X = startPoint.X - (int)((GBTwoWidth / 2) * progressTowardNextClip);
                    //endPoint.X = endPoint.X + (int)((GBOneWidth / 2) * progressTowardNextClip);
                }
            }


            //Bottom-left to Top-right (/): 22.6 - 67.5
            else if (lineAngle >= 22.6 && lineAngle <= 67.5)
            {
                startPoint = clipPoints[0][6];
                endPoint = clipPoints[1][2];

                if (lineAngle > 22.5 && lineAngle <= 45)
                {
                    progressTowardNextClip = (lineAngle - 22.6) / 22.5;
                    endPoint.X = (int)(endPoint.X - (GBOneWidth / 2)) + (int)((GBOneWidth / 2) * progressTowardNextClip);
                }
                else
                {
                    progressTowardNextClip = ((lineAngle - 45.1) / 22.5);
                    endPoint.Y = endPoint.Y + (int)((GBOneHieght / 2) * progressTowardNextClip);
                    //endPoint.X = (int) (endPoint.X - (GBOneWidth / 4)) + (int)((GBOneWidth / 4) * progressTowardNextClip);
                }
            }


            //Left-mid Right-mid (-): 67.6 - 112.5
            else if (lineAngle >= 67.6 && lineAngle <= 112.5)
            {
                startPoint = clipPoints[0][7];
                endPoint = clipPoints[1][3];


                if (lineAngle >= 67.5 && lineAngle <= 90)
                {
                    progressTowardNextClip = ((lineAngle - 67.6) / 22.5);
                    startPoint.Y = (int)(startPoint.Y + (GBTwoHieght / 2)) - (int)((GBOneHieght / 2) * progressTowardNextClip);
                    //endPoint.Y = (int) (endPoint.Y - (GBOneHieght / 2)) + (int)((GBOneHieght / 2) * progressTowardNextClip);
                }
                else
                {
                    progressTowardNextClip = ((lineAngle - 90.1) / 22.5);
                    startPoint.Y = (int)(startPoint.Y) - (int)((GBTwoHieght / 2) * progressTowardNextClip);
                    //endPoint.Y = (int)(endPoint.Y) + (int)((GBOneHieght / 2) * progressTowardNextClip);
                }
            }


            //Left-top to Right-bottom (\): 112.6 - 157.5
            else if (lineAngle >= 112.6 && lineAngle <= 157.5)
            {
                startPoint = clipPoints[0][0];
                endPoint = clipPoints[1][4];

                if (lineAngle >= 112.5 && lineAngle <= 135)
                {
                    progressTowardNextClip = ((lineAngle - 112.6) / 22.5);
                    endPoint.Y = (int)(endPoint.Y - (GBOneHieght / 2)) + (int)((GBOneHieght / 2) * progressTowardNextClip);
                }
                else
                {
                    progressTowardNextClip = ((lineAngle - 135.1) / 22.5);
                    endPoint.X = (int)(endPoint.X) - (int)((GBOneWidth / 2) * progressTowardNextClip);
                }
            }


            //Top-mid to Bottom-mid (|): 157.6 - 202.5
            else if (lineAngle >= 157.6 && lineAngle <= 202.5)
            {
                startPoint = clipPoints[0][1];
                endPoint = clipPoints[1][5];


                if (lineAngle >= 157.5 && lineAngle <= 180)
                {
                    progressTowardNextClip = ((lineAngle - 157.6) / 22.5);
                    startPoint.X = (int)(startPoint.X - (GBTwoWidth / 2)) + (int)((GBTwoWidth / 2) * progressTowardNextClip);
                    //endPoint.X = (int) (endPoint.X + (GBOneWidth / 2)) - (int)((GBOneWidth / 2) * progressTowardNextClip);
                }
                else
                {
                    progressTowardNextClip = ((lineAngle - 180.1) / 22.5);
                    startPoint.X = (int)startPoint.X + (int)((GBTwoWidth / 2) * progressTowardNextClip);
                    //endPoint.X = (int) startPoint.X - (int)((GBOneWidth / 2) * progressTowardNextClip);
                }

            }


            //Top-right to Bottom-left (/): 202.6 - 247.5
            else if (lineAngle >= 202.6 && lineAngle <= 247.5)
            {
                startPoint = clipPoints[0][2];
                endPoint = clipPoints[1][6];

                if (lineAngle >= 202.5 && lineAngle <= 225)
                {
                    progressTowardNextClip = ((lineAngle - 202.6) / 22.5);
                    endPoint.X = (int)(endPoint.X + (GBOneWidth / 2)) - (int)((GBOneWidth / 2) * progressTowardNextClip);
                }
                else
                {
                    progressTowardNextClip = ((lineAngle - 225.1) / 22.5);
                    endPoint.Y = (int)endPoint.Y - (int)((GBOneHieght / 2) * progressTowardNextClip);
                }
            }


            //Right-mid to Left-mid (-): 247.6 - 292.5
            else if (lineAngle >= 247.6 && lineAngle <= 292.5)
            {
                startPoint = clipPoints[0][3];
                endPoint = clipPoints[1][7];


                if (lineAngle > 247.5 && lineAngle < 270)
                {
                    progressTowardNextClip = ((lineAngle - 247.6) / 22.5);
                    startPoint.Y = (int)(startPoint.Y - (GBTwoHieght / 2)) + (int)((GBTwoHieght / 2) * progressTowardNextClip);
                    //endPoint.Y = (int)(endPoint.Y + (GBOneHieght / 2)) - (int)((GBOneHieght / 2) * progressTowardNextClip);
                }
                else
                {
                    progressTowardNextClip = ((lineAngle - 270.1) / 22.5);
                    startPoint.Y = (int)(startPoint.Y) + (int)((GBTwoHieght / 2) * progressTowardNextClip);
                    //endPoint.Y = (int)(endPoint.Y) - (int)((GBOneHieght / 2) * progressTowardNextClip);
                }

            }


            //Right-bottom to Left-top (\): 292.6 - 337.5
            else if (lineAngle >= 292.6 && lineAngle <= 337.5)
            {
                startPoint = clipPoints[0][4];
                endPoint = clipPoints[1][0];

                if (lineAngle > 292.5 && lineAngle < 315)
                {
                    progressTowardNextClip = ((lineAngle - 292.6) / 22.5);
                    endPoint.Y = (int)(endPoint.Y + (GBOneHieght / 2)) - (int)((GBOneHieght / 2) * progressTowardNextClip);
                }
                else
                {
                    progressTowardNextClip = ((lineAngle - 315.1) / 22.5);
                    endPoint.X = (int)(endPoint.X) + (int)((GBOneWidth / 2) * progressTowardNextClip);
                }
            }

            else {
                startPoint = clipPoints[0][5];
                endPoint = clipPoints[1][1];

                progressTowardNextClip = (lineAngle / 22.5);
                startPoint.X = startPoint.X - (int)((GBTwoWidth / 2) * progressTowardNextClip);
            }

            formCanvas.DrawLine(blackPen, startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
            return new Point[] { startPoint, endPoint };
        }

        //Draws the relevants symbols on the relationship lines (symbols that indicate participation and multiplicity)
        private void drawLineSymbols(Point lineStart, Point LineEnd, string relType)
        {
            Graphics lineSymbol = CreateGraphics();

            Point[] anchors = calcSymbolPostions(lineStart, LineEnd, 30); //130 //Calculates the cooridnates, relative to the relationship line, that symbols will be placed on/near
            drawMultiplicity(relType.Split(' ')[0], anchors[0], anchors[1], anchors[2], anchors[3], lineSymbol); //Draws multiplicity symbols 

            anchors = calcSymbolPostions(lineStart, LineEnd, 59); 
            drawParticipation(relType.Split(' ')[1], anchors[0], anchors[1], anchors[2], anchors[3], lineSymbol); //Draws participation symbols 
        }

        //http://csharphelper.com/blog/2015/05/rotate-around-a-point-other-than-the-origin-in-c/
        //Uses the Matrix library/type to give a rotated perspective of a point (used to transform graphic variables so lines and shapes can be drawns in a given orientation/angle)
        private Matrix rotateLine(Point anchor, int angle)
        {
            Matrix rotationResult = new Matrix();
            rotationResult.RotateAt(angle, anchor);
            return rotationResult;
        }

        //Given 2 points, a calculation is made for the location of a point if it was brought propotionally closer to ther other - (the extent to which is dependant on the "shinkFactor" parameter) -
        //With consideration of the angle that the 2 points are offset by
        //In theory, this could be used to find a point that can be used to expand the line while maintining it's angle
        private Point shrinkStartPoint(Point pointOne, Point pointTwo, double shrinkFactor)
        {
            if (shrinkFactor >= Int32.MaxValue) {
                return new Point(
                Convert.ToInt32(pointOne.X * (pointTwo.X - pointOne.X)),
                Convert.ToInt32(pointOne.Y * (pointTwo.Y - pointOne.Y))
            );
            }
            return new Point(
                Convert.ToInt32(pointOne.X + shrinkFactor * (pointTwo.X - pointOne.X)),
                Convert.ToInt32(pointOne.Y + shrinkFactor * (pointTwo.Y - pointOne.Y))
            );
        }

        //Calculates the start and end points of the relationship line symbols relative to the line's location
        private Point[] calcSymbolPostions(Point lineStart, Point lineEnd, int shrinkTarget) {
            int lineLength = (int)Math.Sqrt((Math.Pow((lineStart.X - lineEnd.X), 2) + Math.Pow((lineStart.Y - lineEnd.Y), 2)));
            double shrinkFactor = 1 - ((double)shrinkTarget / (double)lineLength); //Used to find a point that would shrink the line to a certain pixel size (shrinkTarget)

            //The anchors will be the start point of the symbols
            Point anchorPointOne = shrinkStartPoint(lineStart, lineEnd, shrinkFactor);
            Point anchorPointTwo = shrinkStartPoint(lineEnd, lineStart, shrinkFactor);

            /*
            //The landing points will be the end points of the symbols 
            Point landingPointOne = shrinkStartPoint(lineEnd, anchorPointOne, 0.3);
            Point landingPointTwo = shrinkStartPoint(lineStart, anchorPointTwo, 0.3);

            return new Point[] { anchorPointOne, anchorPointTwo, landingPointOne, landingPointTwo };
            */

            return new Point[] { anchorPointOne, anchorPointTwo, lineEnd, lineStart };
        }

        //https://vertabelo.com/blog/crow-s-foot-notation/
        //Draws the multiplicity symbols using the calculated anchor points and landing points
        private void drawMultiplicity(String multiplicity, Point startAnchor, Point endAnchor, Point startLanding, Point endLanding, Graphics lineSymbol) {
            Point shrunkLandings;
            Point shrunkAnchor;

            //The symbol drawn depends on the relationship's multiplicity type
            switch (multiplicity) {
                case "OneToMany":
                    //Draws a short lines that intersects the relationship lines near where the relationship line touches the groupbox with the "one to..." relationship
                    shrunkAnchor = shrinkStartPoint(endLanding, endAnchor, 0.55); //Added to move the intersecting line close to the groupbox - for asktetics 
                    shrunkLandings = shrinkStartPoint(endAnchor, endLanding, 0.9); //Shortens interecting line

                    lineSymbol.Transform = rotateLine(shrunkAnchor, 90);
                    lineSymbol.DrawLine(redPen, shrunkAnchor.X, shrunkAnchor.Y, shrunkLandings.X, shrunkLandings.Y);
                    lineSymbol.Transform = rotateLine(shrunkAnchor, 270);
                    lineSymbol.DrawLine(redPen, shrunkLandings.X, shrunkLandings.Y, shrunkAnchor.X, shrunkAnchor.Y);

                    //Draws a crows put symbol near where the relationship line touches the groupbox with the "... to many" relationship
                    lineSymbol.Transform = rotateLine(startAnchor, 0);
                    lineSymbol.DrawLine(redPen, startAnchor.X, startAnchor.Y, startLanding.X, startLanding.Y);
                    lineSymbol.Transform = rotateLine(startAnchor, 20);
                    lineSymbol.DrawLine(redPen, startAnchor.X, startAnchor.Y, startLanding.X, startLanding.Y);
                    lineSymbol.Transform = rotateLine(startAnchor, 340);
                    lineSymbol.DrawLine(redPen, startAnchor.X, startAnchor.Y, startLanding.X, startLanding.Y);
                    break;

                case "OneToOne":
                    //Draws an intersecting line on one end of the relationship line
                    shrunkAnchor = shrinkStartPoint(endLanding, endAnchor, 0.55);
                    shrunkLandings = shrinkStartPoint(endAnchor, endLanding, 0.9);

                    lineSymbol.Transform = rotateLine(shrunkAnchor, 90);
                    lineSymbol.DrawLine(redPen, shrunkAnchor.X, shrunkAnchor.Y, shrunkLandings.X, shrunkLandings.Y);
                    lineSymbol.Transform = rotateLine(shrunkAnchor, 270);
                    lineSymbol.DrawLine(redPen, shrunkLandings.X, shrunkLandings.Y, shrunkAnchor.X, shrunkAnchor.Y);

                    //Draws another intersecting line on the other end of the relationship line
                    shrunkAnchor = shrinkStartPoint(startLanding, startAnchor, 0.55);
                    shrunkLandings = shrinkStartPoint(startAnchor, startLanding, 0.9);

                    lineSymbol.Transform = rotateLine(shrunkAnchor, 90);
                    lineSymbol.DrawLine(redPen, shrunkAnchor.X, shrunkAnchor.Y, shrunkLandings.X, shrunkLandings.Y);
                    lineSymbol.Transform = rotateLine(shrunkAnchor, 270);
                    lineSymbol.DrawLine(redPen, shrunkLandings.X, shrunkLandings.Y, shrunkAnchor.X, shrunkAnchor.Y);
                    break;

                case "ManyToOne":
                    //Draws a crows foot on one one end of the relationship line
                    lineSymbol.DrawLine(redPen, startAnchor.X, startAnchor.Y, startLanding.X, startLanding.Y);
                    lineSymbol.Transform = rotateLine(startAnchor, 20);
                    lineSymbol.DrawLine(redPen, startAnchor.X, startAnchor.Y, startLanding.X, startLanding.Y);
                    lineSymbol.Transform = rotateLine(startAnchor, 340);
                    lineSymbol.DrawLine(redPen, startAnchor.X, startAnchor.Y, startLanding.X, startLanding.Y);

                    //Draws another intersecting line on the other end of the relationship line
                    shrunkAnchor = shrinkStartPoint(endLanding, endAnchor, 0.55);
                    shrunkLandings = shrinkStartPoint(endAnchor, endLanding, 0.9);

                    lineSymbol.Transform = rotateLine(shrunkAnchor, 90);
                    lineSymbol.DrawLine(redPen, shrunkAnchor.X, shrunkAnchor.Y, shrunkLandings.X, shrunkLandings.Y);
                    lineSymbol.Transform = rotateLine(shrunkAnchor, 270);
                    lineSymbol.DrawLine(redPen, shrunkLandings.X, shrunkLandings.Y, shrunkAnchor.X, shrunkAnchor.Y);
                    break;

                case "ManyToMany":
                    //Draws a crows foot on one end of the relationship line
                    lineSymbol.DrawLine(redPen, startAnchor.X, startAnchor.Y, startLanding.X, startLanding.Y);
                    lineSymbol.Transform = rotateLine(startAnchor, 20);
                    lineSymbol.DrawLine(redPen, startAnchor.X, startAnchor.Y, startLanding.X, startLanding.Y);
                    lineSymbol.Transform = rotateLine(startAnchor, 340);
                    lineSymbol.DrawLine(redPen, startAnchor.X, startAnchor.Y, startLanding.X, startLanding.Y);

                    //Draws a crows foot on the other end of the relationship line
                    lineSymbol.Transform = rotateLine(endAnchor, 0);
                    lineSymbol.DrawLine(redPen, endAnchor.X, endAnchor.Y, endLanding.X, endLanding.Y);
                    lineSymbol.Transform = rotateLine(endAnchor, 20);
                    lineSymbol.DrawLine(redPen, endAnchor.X, endAnchor.Y, endLanding.X, endLanding.Y);
                    lineSymbol.Transform = rotateLine(endAnchor, 340);
                    lineSymbol.DrawLine(redPen, endAnchor.X, endAnchor.Y, endLanding.X, endLanding.Y);
                    break;

                default:
                    break;
            }
        }

        private void drawParticipation(String participation, Point startAnchor, Point endAnchor, Point startLanding, Point endLanding, Graphics lineSymbol)
        {
            Point shrunkLandings;
            double lineAngle;
            Rectangle circleBounds;

            switch (participation)
            {
                //Draws 2 circles in the calculated anchored locations (close to the respective/related multiplicity symbols)
                case "Optional-Optional":
                    //Draws Circle
                    //The circle is drawn using the coordinates of an undrawn rectangle (a rectangle object) which has coorodinates relative to the anchor 
                    //The angle of the relationship line  is used to make the orientation of the rectangle relective of the relationship line's angle
                    //This is so that the circle that is eventually drawn with it's coordinates intersects the relationship line and follows it at a fixed point
                    lineAngle = Math.Abs(((Math.Atan2((startAnchor.X - startLanding.X), (startAnchor.Y - startLanding.Y)) * 180 / Math.PI) + 180) - 360);
                    lineSymbol.Transform = rotateLine(startAnchor, (int) lineAngle + 45);
                    circleBounds = new Rectangle(startAnchor, new Size(25, 25));;
                    lineSymbol.FillEllipse(new SolidBrush(this.BackColor), circleBounds);
                    lineSymbol.DrawEllipse(redPen, circleBounds);

                    //Draws another Circle
                    lineAngle = Math.Abs(((Math.Atan2((endAnchor.X - endLanding.X), (endAnchor.Y - endLanding.Y)) * 180 / Math.PI) + 180) - 360);
                    lineSymbol.Transform = rotateLine(endAnchor, (int)lineAngle + 45);
                    circleBounds = new Rectangle(endAnchor, new Size(25, 25));
                    //lineSymbol.DrawRectangle(redPen, circleBounds);
                    lineSymbol.FillEllipse(new SolidBrush(this.BackColor), circleBounds);
                    lineSymbol.DrawEllipse(redPen, circleBounds);
                    break;

                //Draws an interecting line and a circle at appropriate locations on the relationship line
                case "Optional-Manditory":
                    //Draws Circle
                    lineAngle = Math.Abs(((Math.Atan2((endAnchor.X - endLanding.X), (endAnchor.Y - endLanding.Y)) * 180 / Math.PI) + 180) - 360);
                    lineSymbol.Transform = rotateLine(endAnchor, (int)lineAngle + 45);
                    circleBounds = new Rectangle(endAnchor, new Size(25, 25));
                    lineSymbol.FillEllipse(new SolidBrush(this.BackColor), circleBounds);
                    lineSymbol.DrawEllipse(redPen, circleBounds);

                    //Draws interecting line
                    startAnchor = shrinkStartPoint(startLanding, startAnchor, 0.54);
                    shrunkLandings = shrinkStartPoint(startAnchor, startLanding, 0.43);

                    lineSymbol.Transform = rotateLine(startAnchor, 90);
                    lineSymbol.DrawLine(redPen, startAnchor.X, startAnchor.Y, shrunkLandings.X, shrunkLandings.Y);
                    lineSymbol.Transform = rotateLine(startAnchor, 270);
                    lineSymbol.DrawLine(redPen, shrunkLandings.X, shrunkLandings.Y, startAnchor.X, startAnchor.Y);
                    break;

                //Draws a circles and an intersecting line at appropriate locations on the relationship line
                case "Manditory-Optional":
                    //Draws Circle
                    lineAngle = Math.Abs(((Math.Atan2((startAnchor.X - startLanding.X), (startAnchor.Y - startLanding.Y)) * 180 / Math.PI) + 180) - 360);
                    lineSymbol.Transform = rotateLine(startAnchor, (int)lineAngle + 45);
                    circleBounds = new Rectangle(startAnchor, new Size(25, 25));
                    lineSymbol.FillEllipse(new SolidBrush(this.BackColor), circleBounds);
                    lineSymbol.DrawEllipse(redPen, circleBounds);

                    //Draws interecting line
                    endAnchor = shrinkStartPoint(endLanding, endAnchor, 0.54);
                    shrunkLandings = shrinkStartPoint(endAnchor, endLanding, 0.43);

                    lineSymbol.Transform = rotateLine(endAnchor, 90);
                    lineSymbol.DrawLine(redPen, endAnchor.X, endAnchor.Y, shrunkLandings.X, shrunkLandings.Y);
                    lineSymbol.Transform = rotateLine(endAnchor, 270);
                    lineSymbol.DrawLine(redPen, shrunkLandings.X, shrunkLandings.Y, endAnchor.X, endAnchor.Y);
                    break;

                //Draws interecting lines at appropriate locations on the relationship line
                case "Manditory-Manditory":
                    //Draws interecting line
                    startAnchor = shrinkStartPoint(startLanding, startAnchor, 0.54); //0.44
                    shrunkLandings = shrinkStartPoint(startAnchor, startLanding, 0.43); //0.47

                    lineSymbol.Transform = rotateLine(startAnchor, 90);
                    lineSymbol.DrawLine(redPen, startAnchor.X, startAnchor.Y, shrunkLandings.X, shrunkLandings.Y);
                    lineSymbol.Transform = rotateLine(startAnchor, 270);
                    lineSymbol.DrawLine(redPen, shrunkLandings.X, shrunkLandings.Y, startAnchor.X, startAnchor.Y);

                    //Draws another interecting line
                    endAnchor = shrinkStartPoint(endLanding, endAnchor, 0.54);
                    shrunkLandings = shrinkStartPoint(endAnchor, endLanding, 0.43);

                    lineSymbol.Transform = rotateLine(endAnchor, 90);
                    lineSymbol.DrawLine(redPen, endAnchor.X, endAnchor.Y, shrunkLandings.X, shrunkLandings.Y);
                    lineSymbol.Transform = rotateLine(endAnchor, 270);
                    lineSymbol.DrawLine(redPen, shrunkLandings.X, shrunkLandings.Y, endAnchor.X, endAnchor.Y);
                    break;

                default:
                    break;
            }
        }

        private void disableMainFormButtons() {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
        }

        private void enableMainFormButtons() {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
        }

        //Checks if 2 groupboxes have a relationship associated to them/the tables they are associated with
        private bool isRelEstablished(GroupBox gbOne, GroupBox gbTwo)
        {
            foreach (relationshipPoint rel in RelPoints)
            {
                if ((rel.gb1 == gbOne || rel.gb1 == gbTwo) && (rel.gb2 == gbOne || rel.gb2 == gbTwo))
                {
                    return true;
                }
            }
            return false;
        }

        private relationshipPoint findEstablishedRel(GroupBox gbOne, GroupBox gbTwo)
        {
            foreach (relationshipPoint rel in RelPoints)
            {
                if ((rel.gb1 == gbOne || rel.gb1 == gbTwo) && (rel.gb2 == gbOne || rel.gb2 == gbTwo))
                {
                    return rel;
                }
            }
            return null;
        }

        private List<relationshipPoint> findAllEstablishedRels(GroupBox gbOne)
        {
            List <relationshipPoint> matchingRels = new List<relationshipPoint>();
            foreach (relationshipPoint rel in RelPoints)
            {
                if ((rel.gb1 == gbOne || rel.gb2 == gbOne))
                {
                    matchingRels.Add(rel);
                }
            }
            return matchingRels;
        }

        //records the new location of a groupbox that has been moved
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

    }
}
