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
            Application.Run(new Form1());
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
    }
}
