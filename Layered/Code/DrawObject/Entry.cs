using System;
using System.Drawing;

using Layered.Internal;
using Layered.Basic;

namespace Layered.DrawObject
{

    

    public class TextEntry : DrawObject
    {
        public Color color;

        public Queue<string> queue = new Queue<string>(8);
        private Text text;
        private string str = "";

        private Rectangle drawArea;
        private Rectangle drawAreaInner;

        //private UIObject.ButtonPressable button;

        public bool isSelected {
            get {return 
                drawArea.Contains( UserInput.Mouse.left.down) && 
                drawArea.Contains(UserInput.Mouse.left.up) &&
                !UserInput.Mouse.left.pressed;}}
        public bool isHighlighted { 
            get {return 
                drawArea.Contains(UserInput.Mouse.left.down) && 
                UserInput.Mouse.left.pressed;}}
        

        public TextEntry(int z, Rectangle drawArea, Color drawColor, int fontSize, string fontName, string startStr = "")
        {
            this.Z = z;

            this.drawArea = drawArea;
            int shrinkPx = 2;
            this.drawAreaInner = new Rectangle(drawArea.X + shrinkPx, drawArea.Y + shrinkPx, drawArea.Width - shrinkPx * 2, drawArea.Height - shrinkPx * 2);

            this.str = startStr;
            this.text = new Text(this.Z, fontName, fontSize, drawColor, this.str, drawArea.Location);

            UserInput.keyDownLiseners.Add(this.HandleInput);


            //this.button = new UIObject.ButtonPressable(this.Z, this.d);

        }


        private void HandleInput(string text)
        {
            this.str += text;
        }

        public override void Draw()
        {
            Visual.DrawRect(this.drawArea, this.color);
            Visual.DrawRect(this.drawAreaInner, Color.Azure);
            this.text.Draw();

            if (isSelected)
                Visual.DrawRectShell(this.drawAreaInner, Color.Azure);
            
            if (isHighlighted)
                Visual.DrawRectShell(this.drawArea, Color.Azure);
            
        }

        public override void Delete()
        {}

    }

}