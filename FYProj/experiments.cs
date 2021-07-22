using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYProj
{
    class experiments
    {
    }
}

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


/*
private void reDrawForm(int offsetOne, int offsetTwo)
{
    if (surface != null)
    {
        surface.Clear(this.BackColor);
    }
    surface = CreateGraphics();

    foreach (relationshipPoint coord in RelPoints)
    {
        surface.DrawLine(blackPen, coord.start.X, coord.start.Y, coord.end.X, coord.end.Y);
        surface.DrawLine(blackPen, coord.start.X + 50, coord.start.Y + 50, 200, 200);
    }
}
*/





/*
private void testGraphics() {

    Graphics foot = CreateGraphics();
    Pen bluePen = new Pen(Color.Blue, 2);
    Pen redPen = new Pen(Color.Red, 2);
    foot.DrawLine(bluePen, 10, 100, 40, 100);
    foot.DrawLine(bluePen, 10, 100, 40, 80);
    foot.DrawLine(bluePen, 10, 100, 40, 120);

    double t;
    int pointAx, pointAy, pointBx, pointBy;
    t = 0.7;
    pointAx = 300;
    pointAy = 50;
    pointBx = 500;
    pointBy = 300;

    //https://www.wikihow.com/Use-Distance-Formula-to-Find-the-Length-of-a-Line
    foot.DrawLine(redPen, pointAx, pointAy, pointBx, pointBy);
    int footLen = (int) Math.Sqrt((Math.Pow((pointBx - pointAx), 2) + Math.Pow((pointBy - pointAy), 2)));
    //MessageBox.Show(footLen.ToString());

    //(Ax+t(Bx−Ax),Ay+t(By−Ay)) - shortens line by the factor t and maintians angle
    //https://math.stackexchange.com/questions/3058210/how-to-shorten-a-line-but-maintain-its-angle
    int newX = Convert.ToInt32(pointAx + t * (pointBx - pointAx));
    int newY = Convert.ToInt32(pointAy + t * (pointBy - pointAy));

    //foot.DrawLine(bluePen, newX, newY, pointBx - 50, pointBy - 50);
    //foot.DrawLine(bluePen, newX, newY, pointBx + 50, pointBy + 50);

    foot.Transform = rotateLine(new Point(newX, newY), 90);
    foot.DrawLine(bluePen, newX, newY, pointBx, pointBy);
}




//surface.DrawLine(redPen, coord.start.X + 50, coord.start.Y + 50, coord.end.X, coord.end.Y);
        //surface.DrawLine(redPen, coord.start.X - 50, coord.start.Y - 50, coord.end.X, coord.end.Y);

        //double angle = Math.Abs(((Math.Atan2((coord.end.X - coord.start.X), (coord.end.Y - coord.start.Y)) * 180 / Math.PI) + 180) - 360);
        //MessageBox.Show(angle.ToString());











private void drawRelationshipLines(relationshipPoint GBCoords) {
    double lineAngle = Math.Abs(((Math.Atan2((GBCoords.start.X - GBCoords.end.X), (GBCoords.start.Y - GBCoords.end.Y)) * 180 / Math.PI) + 180) - 360);
    int lineLength = (int)Math.Sqrt((Math.Pow((GBCoords.start.X - GBCoords.end.X), 2) + Math.Pow((GBCoords.start.Y - GBCoords.end.Y), 2)));

    int hypotenuse_AKAIncrement = 10;
    double opposite_YInc = Math.Sin(lineAngle) * hypotenuse_AKAIncrement;
    double adjacent_XInc = Math.Cos(lineAngle) * hypotenuse_AKAIncrement;

    Point newEndPoint = GBCoords.start;
    Point newStartPoint = GBCoords.end;

    int incSoFar = 0;

    switch (lineAngle)
    {
        case 0:
            break;
        case 90:
            break;
        case 180:
            break;
        case 270:
            break;
        default:
            if (lineAngle > 0 && lineAngle < 90) {
                while (incSoFar < lineLength && newEndPoint.Y > (GBCoords.end.Y + (GBCoords.gb2.Height / 2)) && newEndPoint.X < (GBCoords.end.X - (GBCoords.gb2.Width / 2))) {
                    incSoFar += hypotenuse_AKAIncrement;
                    newEndPoint.Y -= (int) opposite_YInc;
                    newEndPoint.X += (int) adjacent_XInc;
                }
                incSoFar = 0;
                while (incSoFar < lineLength && newStartPoint.Y < (GBCoords.start.Y - (GBCoords.gb2.Height / 2)) && newStartPoint.X > (GBCoords.start.X + (GBCoords.gb2.Width / 2)))
                {
                    incSoFar += hypotenuse_AKAIncrement;
                    newEndPoint.Y += (int)opposite_YInc;
                    newEndPoint.X -= (int)adjacent_XInc;
                }
            }

            else if (lineAngle > 90 && lineAngle < 180) {
                while (incSoFar < lineLength && newEndPoint.Y < (GBCoords.end.Y - (GBCoords.gb2.Height / 2)) && newEndPoint.X < (GBCoords.end.X - (GBCoords.gb2.Width / 2)))
                {
                    incSoFar += hypotenuse_AKAIncrement;
                    newEndPoint.Y += (int)opposite_YInc;
                    newEndPoint.X += (int)adjacent_XInc;
                }
                incSoFar = 0;
                while (incSoFar < lineLength && newStartPoint.Y > (GBCoords.start.Y + (GBCoords.gb2.Height / 2)) && newStartPoint.X > (GBCoords.start.X + (GBCoords.gb2.Width / 2)))
                {
                    incSoFar += hypotenuse_AKAIncrement;
                    newEndPoint.Y -= (int)opposite_YInc;
                    newEndPoint.X -= (int)adjacent_XInc;
                }
            }

            else if (lineAngle > 180 && lineAngle < 270) {

            }

            else if (lineAngle > 270 && lineAngle < 360) {

            }
            break;

    }
*/
