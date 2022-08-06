using System;
using System.Drawing;

using Layered.Internal;
using Layered.Basic;

namespace Layered.DrawObject
{

    //  todo implement a proper base Option class to support more option structures


    public class OptionBasic : DrawObject
    {

        public const string defualtOptionTexture = "Option_DefualtTexture.png";
        public static readonly Rectangle defualtOptionTextureArea = new Rectangle(0,0, 256, 256);


        public Point drawPoint {
            get {return drawArea.Location;} 
            set {
                drawArea = new Rectangle(value, drawArea_org.Size);
            }
        }

        private     Rectangle       drawArea_org;
        public      Rectangle       drawArea {
            get {return this.drawArea_org;} 
            set {
                this.drawArea_org = value;

                Point iconPoint = value.Location;
                iconPoint.Offset(0, (value.Height - this.iconSize.Height) / 2);
                this.texture.screenDrawArea = new Rectangle(iconPoint, this.iconSize);
                this.buttonArea = new Rectangle(drawArea.X + drawArea.Width - drawArea.Height, drawArea.Y, drawArea.Height, drawArea.Height);
                this.blocker.bounds = value;

                if (this.text != null)
                    this.AddText(text); //  to update the text position
            }

        }
        

        private     Size            iconSize_org;
        public      Size            iconSize {
            get {return iconSize_org;} 
            set {
                iconSize_org = value;
                this.texture.screenDrawArea = new Rectangle(this.texture.screenDrawArea.Location, value);
            }
        }
        


        public      Color           drawColor;
        protected   SimpleTexture   texture;


        //  todo:   properties to acces these publicly
        protected   bool    onOff;


        public      bool    enabled;

        private Rectangle buttonArea {
            get {return this.button.bounds;}
            set {this.button.bounds = value;}
        }
        
        private UIObject.ButtonPressable button;
        private UIObject.Blocker blocker;


        public delegate bool UserDefinedAction(bool onOff);
        protected UserDefinedAction? action;

        private Text? text = null;


        


        //  Implement when there is a suitable defualt texture
        public OptionBasic(int z, Rectangle drawArea, Color drawColor, Size iconSize, UserDefinedAction? action, bool onOff = false)
            :   this(z, drawArea, drawColor, iconSize, action, defualtOptionTextureArea, defualtOptionTexture, Settings.defaultTextureFolderPath, onOff)
        {}

        public OptionBasic(int z, Rectangle drawArea, Color drawColor, Size iconSize, UserDefinedAction? action, Rectangle textureArea, string textureName, string textureFolderPath = Settings.defaultTextureFolderPath, bool onOff = false)
            : this(z, drawArea, drawColor, iconSize, action,
                new SimpleTexture(-1, new Rectangle(Point.Empty, iconSize), textureArea, textureName, textureFolderPath),
                onOff)
        {}


        public OptionBasic(int z, Rectangle drawArea, Color drawColor, Size iconSize, UserDefinedAction? action, SimpleTexture textureSymbol, bool onOff = false)
        {
            this.Z = z; 

            this.onOff = onOff;
            this.enabled = true;

            this.texture = textureSymbol;

            this.blocker = new UIObject.Blocker(this.Z, drawArea);
            this.button = new UIObject.ButtonPressable(this.Z + 1, new Rectangle(drawArea.X + drawArea.Width - drawArea.Height, drawArea.Y, drawArea.Height, drawArea.Height), DoAction);

            this.iconSize = iconSize;
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
            
            texture.Draw();
            Visual.DrawRectShell(drawArea, Color.Azure);

            Rectangle drawRect = buttonArea;
            Visual.DrawRectShell(buttonArea, Color.Azure); 

            if (this.onOff)
            {

                int reduceBy = this.buttonArea.Width / 4;
                Visual.DrawRect(
                    new Rectangle(buttonArea.X + reduceBy, buttonArea.Y + reduceBy, buttonArea.Width - reduceBy * 2, buttonArea.Height - reduceBy * 2), 
                    Color.Azure);
            }

            if (this.text != null)
                this.text.Draw();


        }

        public override void Delete()
        {
            LayeredLayout.Remove(blocker);
            LayeredLayout.Remove(button);            
        }

 
        public void AddText(string fontName, Color color, string text)
        {
            AddText(new Text(-1, fontName, this.drawArea.Height - 4, color, text, Point.Empty) ); 
        }     


        public void AddText(Text text)
        {
            if (this.text != null && this.text != text)
                this.text.Delete();

            this.text = text;
            this.text.drawPoint = new Point(this.drawPoint.X + this.iconSize.Width, this.drawPoint.Y);

        }

        private UIObject.ButtonPressable.State DoAction(UIObject.ButtonPressable.State state)
        {
           // Console.WriteLine($"enabled = {this.action != null}");


            if (enabled && this.action != null)
                if (state == UIObject.ButtonPressable.State.Pressed)
                    this.onOff = this.action.Invoke(!this.onOff);

            if (state == UIObject.ButtonPressable.State.Pressed)
                state = UIObject.ButtonPressable.State.Enabled;

            //Console.WriteLine(onOff);
            

            return state;
        }

    } 

}  