using System;
using System.Drawing;

using Layered.Internal;
using Layered.Basic;

namespace Layered.DrawObject
{

    //  todo implement a proper base Option class to support more option structures


    public class OptionBasic : OptionBase
    {

        public const string defualtOptionTexture = "Option_DefualtTexture.png";
        public static readonly Rectangle defualtOptionTextureArea = new Rectangle(0,0, 256, 256);


        private Rectangle iconBasedDrawArea;
        public new Rectangle drawArea {
            get {return base.drawArea;} 
            set {
                base.drawArea = value;
                iconBasedDrawArea = value;
                Point iconPoint = value.Location;
                iconPoint.Offset(0, (value.Height - this.iconSize.Height) / 2);
                this.texture.screenDrawArea = new Rectangle(iconPoint, this.iconSize);
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
        

        protected   SimpleTexture   texture;


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
            : base(z, drawArea, drawColor, action, onOff)
        {
            this.texture = textureSymbol;
            this.iconSize = iconSize;
            if (this.button == null || this.blocker == null)
                throw new Exception("OptionBasic Base construtor did not initilize button and blocker (atleast one is null)");
        }

        //  in this case we wan't to override the default draw function
        public override void Draw()
        {
            //  if the icon is not based on our current drawArea(maybe because it got updated via the base class property)
            if (this.drawArea != iconBasedDrawArea)
                this.drawArea = drawArea;

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
            base.Delete();           
        }

 
    } 

}  