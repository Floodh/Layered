using System;
using System.Drawing;

namespace Layered.Basic   {

    public static class Misc{

        public static readonly Random rng = new Random();

        public static readonly Color[] defaultColors = {Color.Red, Color.Teal, Color.Purple, Color.Green, Color.Yellow, Color.Magenta, Color.Brown};

        public static double Distance(Point p1, Point p2)
        {
            return Math.Sqrt( ((p1.X - p2.X) * (p1.Y - p2.Y)) + ((p1.X - p2.Y) * (p1.Y - p2.Y)) );
        }

        public static double Atan2(double x, double y)
        {
            return Math.Atan2(y, x);
        }



    }

}
