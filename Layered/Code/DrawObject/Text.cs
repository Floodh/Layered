using System;
using System.Drawing;

using   Layered.Internal;
using   Layered.Basic;

namespace Layered.DrawObject
{


    public class Text : DrawObject
    {
        public string textStr 
            {get {return this.textStr_org;} 
            set {this.textStr_org = value; Visual.DeleteTexture(this.texture); Visual.LoadTextTexture(font, value, color); }}
        private string textStr_org;

        private Color color;

        private string      fontName;
        private IntPtr      font = IntPtr.Zero;
        private IntPtr      texture = IntPtr.Zero;
        //
        private int         width;
        private int         height;
        public  Size        Size {
            get         { return new Size(width, height);} 
            private set { width = value.Width; height = value.Height;}}

        private Rectangle   textureArea;
        private Rectangle   drawArea;
        public  Point       drawPoint { 
            get         { return this.drawArea.Location;} 
            set         { this.drawArea.Location = value;}}

        
        public  static string   fontFolderPath = Settings.fontFolderPath;      
        private static Dictionary<string, IntPtr> fontDictionary = new Dictionary<string, IntPtr>();

        public Text(int z, string fontName, int ptSize, Color color, string text, Point drawPoint)
        {
            this.textStr_org = text;
            this.color = color;

            this.Z              = z;   
            this.fontName       = fontName;

            if (fontDictionary.ContainsKey(fontFolderPath + "\\" + fontName))
            {
                this.font = fontDictionary[fontFolderPath + "\\" + fontName];
            }
            else 
            {
                this.font           = Visual.OpenFont(fontFolderPath + "\\" + fontName, ptSize);
                fontDictionary.Add(fontFolderPath + "\\" + fontName, this.font);
            }
            
            
            this.texture        = Visual.LoadTextTexture(font, text, color);

            Visual.TextureSize(this.font, text, out this.width, out this.height);

            this.textureArea    = new Rectangle(Point.Empty, this.Size);
            this.drawArea       = new Rectangle(drawPoint, this.Size);

        }
        

        //  functions
        public override void Draw()
        {
            Visual.DrawTexture(this.texture, this.textureArea, this.drawArea);
        }

        public override void Delete()
        {
            //  Visual.DeleteTexture(textures[texture_index].texture); since textures are stored as static variables we cannot Delete it
        }

    }    
}