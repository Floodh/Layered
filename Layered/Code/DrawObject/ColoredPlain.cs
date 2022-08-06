using System;
using System.Drawing;

using Layered.Internal;

namespace Layered.DrawObject
{


    public class ColoredPlain : DrawObject
    {

    
        private IntPtr      texture;
        private Rectangle   screenRect;
        private Rectangle   textureRect;

        public  Point       Location    {   get {return screenRect.Location;}           set {this.screenRect.Location   = value;} }
        public  Size        Size        {   get {return screenRect.Size;    }   private set {this.screenRect.Size       = value;} }




        public ColoredPlain(int z, Rectangle rect, Color color)
        {
            this.Z = z;
            this.texture = Visual.LoadTexture(rect.Size, color);
            this.screenRect = rect;
            this.textureRect = new Rectangle(new Point(0,0), rect.Size);

        }

        public override void Draw()
        {
            Visual.DrawTexture(texture, textureRect, screenRect);
        }

        public override void Delete()
        {
            Visual.DeleteTexture(texture);
        }
        public void Edit(Point point, Color colors)
        {
            Visual.EditTexture(texture, point, colors);
        }

        public void Edit(Point[] points, Color colors)
        {
            Visual.EditTexture(texture, points, colors);
        }
        public void Edit(List<Point> points, Color colors)
        {
            Visual.EditTexture(texture, points, colors);
        }     

        public void Edit(Rectangle rect, Color colors)
        {
            Visual.EditTexture(texture, rect, colors);
        }

        public void Edit(Rectangle[] rects, Color colors)
        {
            Visual.EditTexture(texture, rects, colors);
        }
        public void Edit(List<Rectangle> rects, Color colors)
        {
            Visual.EditTexture(texture, rects, colors);
        }    

    }    
}