using   System;
using   System.Drawing;

using   Layered.Internal;
using   Layered.Basic;

namespace Layered.DrawObject
{


    public class RecursiveTexture : IDrawObject
    {


        public  int        Z {get; private set;} 

        private IntPtr      internalTexture; 
        private IntPtr      recurringTexture;

        private Rectangle   internalTextureArea;
        private Rectangle   recurringTextureArea;
        private Rectangle   drawArea;  

        public RecursiveTexture(int z, Rectangle drawArea, Rectangle textureArea, string textureName, string texturePath = Settings.defaultTextureFolderPath)
        {
            this.Z = z;
            this.recurringTexture       =   Visual.LoadTexture(texturePath + "\\" + textureName);
            this.recurringTextureArea   =   textureArea;
            this.drawArea               =   drawArea;
            this.NewInternalTexture();

        }

        public Size textureSize 
        {
            get { return this.drawArea.Size;} 
            set { ReSize(value);}

        }

        public Point drawPoint
        {
            get { return this.drawArea.Location;}
            set { this.drawArea.Location = value;}
        }

        public Rectangle screenDrawArea
        {
            get {return this.drawArea;}
            set {this.drawArea = value; ReSize(value.Size);}
        }

        public void Draw()
        {
            Visual.DrawTexture(internalTexture, internalTextureArea, drawArea);
        }

        public void Delete()
        {
            Visual.DeleteTexture(this.internalTexture);
            Visual.DeleteTexture(this.recurringTexture);
        }

        private void ReSize(Size newSize)
        {
            this.drawArea.Size = newSize;  
            //  if the size fits inside the old texture we can just resize the parts of its that is drawn
            if (newSize.Width < internalTextureArea.Width && newSize.Height < internalTextureArea.Height)
                return; 
            //  we need to re Size the internal texture
            this.NewInternalTexture();
            
        }

        //  creates a new internal texture based on the draw area and reccuring texture
        //  edits internalTexture and internalTextureArea
        private void NewInternalTexture()
        {

            
            this.internalTextureArea    =   new Rectangle( new Point(0,0), drawArea.Size);
            //  increase the size of the internalTexture
            //  the draw area remains the same
            this.internalTextureArea.Width  += this.internalTextureArea.Width   % this.recurringTextureArea.Width;
            this.internalTextureArea.Height += this.internalTextureArea.Height  % this.recurringTextureArea.Height;
            this.internalTexture        =   Visual.LoadTexture(internalTextureArea.Size, Color.White);

            for (int x = 0; x < internalTextureArea.Width;  x += recurringTextureArea.Width )
            for (int y = 0; y < internalTextureArea.Height; y += recurringTextureArea.Height)
            {
                Console.WriteLine($"x, y = {x}, {y}");
                Visual.EditTexture(
                    internalTexture, 
                    recurringTexture, 
                    new Rectangle(x, y, recurringTextureArea.Width, recurringTextureArea.Height),
                    recurringTextureArea);
            }           
        }

    }    
}