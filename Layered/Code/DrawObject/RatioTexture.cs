using System;
using System.Drawing;

using   Layered.Internal;
using   Layered.Basic;

namespace Layered.DrawObject
{

    //  respects the ratio of the texture area
    public class RatioTexture : SimpleTexture
    {

        public readonly Size ratio;


        public RatioTexture(int z, Rectangle drawArea, Rectangle textureArea, string texturenName, string textureFolderPath = Settings.defaultTextureFolderPath, bool prioWidth = true)
            : base(z, drawArea, textureArea, texturenName, textureFolderPath)
        {
            //  automaticly adjust the draw area to respect the ratio provided by the texture area

            int gcd = GCD(textureArea.Width, textureArea.Height);
            this.ratio = new Size(textureArea.Width / gcd, textureArea.Height / gcd);


            if (prioWidth)
            {
                this.drawArea.Width -=  this.drawArea.Width % this.ratio.Width;
                this.drawArea.Height = (this.drawArea.Width / this.ratio.Width) * this.ratio.Height;
            }
            else
            {
                this.drawArea.Height-=  this.drawArea.Height % this.ratio.Height;
                this.drawArea.Width  = (this.drawArea.Height / this.ratio.Height) * this.ratio.Width;
            }

            
        }


        private static int GCD(int a, int b)
        {
            int Remainder;

            while( b != 0 )
            {
                Remainder = a % b;
                a = b;
                b = Remainder;
            }

            return a;
        }


    }    
}