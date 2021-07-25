using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace FYProj
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    class relationshipPoint
    {
        public Point start, end;
        public GroupBox gb1, gb2;
        public string relType = "";

        public relationshipPoint(Point s, Point e, GroupBox g1, GroupBox g2, String type)
        {
            start = s;
            end = e;
            gb1 = g1;
            gb2 = g2;
            relType = type;
        }

        public Point[][] getGBPoints() {
            /*
            Point GB1_ULCorner = gb1.Location;
            Point GB1_URCorner = new Point(gb1.Location.X + gb1.Width, gb1.Location.Y);
            Point GB1_LLCorner = new Point(gb1.Location.X, gb1.Location.Y + gb1.Height);
            Point GB1_LRCorner = new Point(gb1.Location.X + gb1.Width, gb1.Location.Y + gb1.Height);

            Point GB2_ULCorner = gb2.Location;
            Point GB2_URCorner = new Point(gb2.Location.X + gb2.Width, gb2.Location.Y);
            Point GB2_LLCorner = new Point(gb2.Location.X, gb2.Location.Y + gb2.Height);
            Point GB2_LRCorner = new Point(gb2.Location.X + gb2.Width, gb2.Location.Y + gb2.Height);
            */

            Point[][] GroupBoxDimensions = new Point[2][];

            GroupBoxDimensions[0] = new Point[] {
                gb1.Location, //Top-Left Corner [0]
                new Point(gb1.Location.X + (gb1.Width / 2), gb1.Location.Y), //Top-Mid [1]

                new Point(gb1.Location.X + gb1.Width, gb1.Location.Y), //Top-Right Corner [2]
                new Point(gb1.Location.X + gb1.Width, gb1.Location.Y + (gb1.Height / 2)),//Right-Mid [3]

                new Point(gb1.Location.X + gb1.Width, gb1.Location.Y + gb1.Height), //Bottom-Right Corner [4]
                new Point(gb1.Location.X + (gb1.Width/2), gb1.Location.Y + gb1.Height), //Bottom-Mid [5]

                new Point(gb1.Location.X, gb1.Location.Y + gb1.Height), //Bottom-Left Corner [6]
                new Point(gb1.Location.X, gb1.Location.Y + (gb1.Height/2)) //Left-Mid [7]
            };

            GroupBoxDimensions[1] = new Point[] {
                gb2.Location, //Top-Left Corner
                new Point(gb2.Location.X + (gb2.Width / 2), gb2.Location.Y), //Top-Mid

                new Point(gb2.Location.X + gb2.Width, gb2.Location.Y), //Top-Right Corner
                new Point(gb2.Location.X + gb2.Width, gb2.Location.Y + (gb2.Height / 2)),//Right-Mid

                new Point(gb2.Location.X + gb2.Width, gb2.Location.Y + gb2.Height), //Bottom-Right Corner
                new Point(gb2.Location.X + (gb2.Width/2), gb2.Location.Y + gb2.Height), //Bottom-Mid

                new Point(gb2.Location.X, gb2.Location.Y + gb2.Height), //Bottom-Left Corner
                new Point(gb2.Location.X, gb2.Location.Y + (gb2.Height/2)) //Left-Mid
            };

            return GroupBoxDimensions;
        }
    }
}
