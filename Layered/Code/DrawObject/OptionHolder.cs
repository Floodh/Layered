using System;
using System.Drawing;

using Layered.Internal;
using Layered.Basic;

namespace Layered.DrawObject
{


    //  TODO: finnish this
    public class OptionHolder : OptionBasic
    {


        public new const string defualtOptionTexture = "Option_DefualtTexture.png";
        public new static readonly Rectangle defualtOptionTextureArea = new Rectangle(0,0, 980, 980);

        private const int containedOptionsShiftPx = 32;
        private const int containedOptionsSpacing = 2;


        public bool displayOptions {
            get {return this.onOff;} 
            set {this.onOff = value;}}
        public OptionBase[] options;

        public const string defualtFont = "OpenSans-Bold.ttf";
        public string fontName = defualtFont;
        public Color textColor = Color.Crimson;
        private Text numberText;



        public new Point drawPoint {
            get {return drawArea.Location;} 
            set {
                base.drawPoint = value;
                this.EditContainedOptions();
            }
        }

        public new Rectangle drawArea {
            get {return base.drawArea;} 
            set {
                base.drawArea = value;
                this.EditContainedOptions();
            }

        }

        

        

        //  Implement when there is a suitable defualt texture
        public OptionHolder(int z, Rectangle drawArea, Color drawColor, Size iconSize, OptionBase[] options)
            :   this(z, drawArea, drawColor, iconSize, options, defualtOptionTextureArea, defualtOptionTexture)
        {}

        public OptionHolder(int z, Rectangle drawArea, Color drawColor, Size iconSize, OptionBase[] options, Rectangle textureArea, string textureName, string textureFolderPath = Settings.defaultTextureFolderPath)
            : this(z, drawArea, drawColor, iconSize, options, 
                new SimpleTexture(-1, new Rectangle(Point.Empty, iconSize), textureArea, textureName, textureFolderPath))
        {}

        public OptionHolder(int z, Rectangle drawArea, Color drawColor, Size iconSize, SimpleTexture textureSymbol)
            :this(z, drawArea, drawColor, iconSize, new OptionBasic[0],textureSymbol)
        {}

        public OptionHolder(int z, Rectangle drawArea, Color drawColor, Size iconSize, OptionBase[] options, SimpleTexture textureSymbol)
            : base(z, drawArea, drawColor, iconSize, null, textureSymbol)
        {
            this.displayOptions = false;
            this.options = options;
            this.drawArea = drawArea;

            Point numberPoint = drawPoint;
            numberPoint.Y -= this.drawArea.Height / 2 - 4;
            this.numberText = new Text(-1, fontName, this.drawArea.Height - 4, textColor, $"{this.options.Length}", numberPoint);

            base.action = ToggleDisplayOptions;
            this.EditContainedOptions();

        }

        private void EditContainedOptions()
        {

            int y = this.drawPoint.Y + this.drawArea.Height + containedOptionsSpacing;


            for (int i = 0; i < options.Length; i++)
            {
                options[i].drawArea = new Rectangle(this.drawPoint.X + containedOptionsShiftPx, y, options[i].drawArea.Width - containedOptionsShiftPx / 2, options[i].drawArea.Height);
                y += options[i].drawArea.Height + containedOptionsSpacing;
            }

        }



        public override void Draw()
        {
            base.Draw();
            //this.numberText.Draw();
            if (displayOptions)
                for (int i = 0; i < options.Length; i++)
                    options[i].Draw();
                
        }

        public new void Delete()
        {
            base.Delete();
        }

        public bool ToggleDisplayOptions(bool onOff)
        {
            for (int i = 0; i < options.Length; i++)
                this.options[i].enabled = onOff;
            return this.displayOptions = onOff;
        }

    } 

}  