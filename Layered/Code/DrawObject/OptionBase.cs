using System;
using System.Drawing;

using Layered.Internal;
using Layered.Basic;

namespace Layered.DrawObject
{

    public class OptionBase : DrawObject
    {


        public Point drawPoint {
            get {return drawArea.Location;} 
            set {
                drawArea = new Rectangle(value, drawArea_org.Size);
            }
        }

        private     Rectangle       drawArea_org;
        public      Rectangle       drawArea {      //  todo:   make this virtual and remove reduce code sprawl
            get {return this.drawArea_org;} 
            set {
                this.drawArea_org = value;
                this.buttonArea = new Rectangle(drawArea.X + drawArea.Width - drawArea.Height, drawArea.Y, drawArea.Height, drawArea.Height);
                this.blocker.bounds = value;

                if (this.text != null)
                    this.AddText(text); //  to update the text position
            }

        }
        
        public      Color           drawColor;

        //  todo:   properties to acces these publicly
        protected   bool    onOff;


        public      bool    enabled;

        protected Rectangle buttonArea {
            get {return this.button.bounds;}
            set {this.button.bounds = value;}
        }
        
        protected UIObject.ButtonPressable button;
        protected UIObject.Blocker blocker;


        public delegate bool UserDefinedAction(bool onOff);
        protected UserDefinedAction? action;

        protected Text? text = null;

        protected int textHeight {get{ return this.drawArea.Height - 4;}}


        

        public OptionBase(int z, Rectangle drawArea, Color drawColor, UserDefinedAction? action, bool onOff = false)
        {
            this.Z = z; 

            this.onOff = onOff;
            this.enabled = true;

            this.blocker = new UIObject.Blocker(this.Z, drawArea);
            this.button = new UIObject.ButtonPressable(this.Z + 1, new Rectangle(drawArea.X + drawArea.Width - drawArea.Height, drawArea.Y, drawArea.Height, drawArea.Height), DoAction);

            //this.iconSize = iconSize;
            this.drawArea = drawArea;
            this.drawColor = drawColor; 

            this.action = action;

            
            if (Settings.autoAddUIObjects == false && z > -1)
            {
                LayeredLayout.Add(blocker);
                LayeredLayout.Add(button);
            }

        }

        public override void Draw()
        {
            Visual.DrawRect(drawArea, drawColor);
            Visual.DrawRectShell(drawArea, Color.Azure);


            if (this.text != null)
                this.text.Draw();
        }

        public override void Delete()
        {
            LayeredLayout.Remove(blocker);
            LayeredLayout.Remove(button);
            if (this.text != null)
                this.text.Delete();            
        }

 
        public void AddText(string fontName, Color color, string text)
        {
            AddText(new Text(-1, fontName, textHeight, color, text, Point.Empty) ); 
        }     


        public void AddText(Text text)
        {
            if (this.text != null && this.text != text)
                this.text.Delete();

            this.text = text;
            this.text.drawPoint = new Point(this.drawPoint.X + textHeight, this.drawPoint.Y);

        }

        private UIObject.ButtonPressable.State DoAction(UIObject.ButtonPressable.State state)
        {

            if (enabled && this.action != null)
                if (state == UIObject.ButtonPressable.State.Pressed)
                    this.onOff = this.action.Invoke(!this.onOff);

            if (state == UIObject.ButtonPressable.State.Pressed)
                state = UIObject.ButtonPressable.State.Enabled;

            return state;
        }

    } 

}