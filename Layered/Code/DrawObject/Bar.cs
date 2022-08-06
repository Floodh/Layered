using System;
using System.Drawing;

using Layered.Internal;
using Layered.Basic;

namespace Layered.DrawObject
{
    public class Bar : DrawObject
    {

        public Rectangle drawArea {get; private set;}


        private const int maxFill = 100 * 100;

        private int currentFill;
        public int fillPercent  {get {return currentFill / 100;} set {currentFill = (value * 100) % maxFill;}}
        public int fillRaw      {get {return currentFill;} set {currentFill = value % maxFill;}}

        Color backgroundColor;
        Color fillColor;

        Direction direction;




        public Bar(int z, Rectangle drawArea, Color backgroundColor, Color fillColor, Direction fillDirection = Direction.east)
        {
            this.Z = z; 
            this.drawArea = drawArea;
            this.currentFill = 0;
            this.backgroundColor = backgroundColor;
            this.fillColor = fillColor;
            this.direction = fillDirection;
        }

        public override void Draw()
        {
            Visual.DrawRect(this.drawArea, backgroundColor);
            Rectangle fillRect = this.drawArea;
            if (this.direction == Direction.east)
            {
                fillRect.Width = (this.drawArea.Width * currentFill) / maxFill;
            }
            else if (this.direction == Direction.west)
            {
                fillRect.Width = (this.drawArea.Width * currentFill) / maxFill;
                Point pos = this.drawArea.Location;
                pos.X += this.drawArea.Width - fillRect.Width;
                fillRect.Location = pos;
            }
            else if (this.direction == Direction.south)
            {
                fillRect.Height = (this.drawArea.Height * currentFill) / maxFill;
            }
            else if (this.direction == Direction.north)
            {
                fillRect.Height = (this.drawArea.Height * currentFill) / maxFill;
                Point pos = this.drawArea.Location;
                pos.Y += this.drawArea.Height - fillRect.Height;
                fillRect.Location = pos;
            }
            Visual.DrawRect(fillRect, fillColor);
        }

        public override void Delete()
        {
        }

    } 

}  