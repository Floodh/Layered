using System;
using System.Drawing;

using Layered.Internal;


namespace Layered.DrawObject
{


    public class PairRect : DrawObject
    {


        public Point    point;
        public int      width;
        public int      length;

        public Color color = Color.FromArgb(255, 255, 50);

        public PairRect(int z, Point point, int width, int length)
        {
            this.Z = z;
            if (width < 1)  width = 1;
            if (length < 1) length = 1;
            this.point = point;
            this.width = width;
            this.length = length;
            
        }

        public override void Draw()
        {

            Visual.DrawRect(new Point(this.point.X - length, this.point.Y - width), new Size(length * 2 + 1, width * 2 + 1), color);
            Visual.DrawRect(new Point(this.point.X - width, this.point.Y - length), new Size(width * 2 + 1, length * 2 + 1), color);
            
        }

        public override void Delete()
        {}

    }    
}