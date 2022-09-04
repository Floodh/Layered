using System;
using System.Drawing;

using Layered.Internal;


namespace Layered.DrawObject
{


    public class Canvas : DrawObject
    {


        private         Rectangle           textureArea;
        private         Rectangle           drawArea; 
        private         IntPtr              internalTexture; 
        private         List<IDrawObject>[] drawLayers;

        public          bool                clearCanvas = false;

        public Canvas(int z, Rectangle drawArea, int layers)
        {
            this.Z = z;
            this.textureArea = new Rectangle( new Point(0,0), drawArea.Size);
            this.drawArea = drawArea;
            this.internalTexture = Visual.LoadTexture(drawArea.Size, Color.Black);
            this.drawLayers = new List<IDrawObject>[layers];
            for (int i = 0; i < layers; i++)
            {
                this.drawLayers[i] = new List<IDrawObject>();
            }
        }

        //  properties
        public Size Size 
        {
            get { return this.drawArea.Size;}
            private set { this.drawArea.Size = value; } 
        }

        public Point drawPoint
        {
            get { return this.drawArea.Location;}
            set { this.drawArea.Location = value;}
        }

        public Rectangle screenDrawArea
        {
            get {return this.drawArea;}
        }
        //


        //  functions
        public override void Draw()
        {
            Visual.SetRenderTarget(this.internalTexture);
            if (clearCanvas)
                Visual.ClearScreen();
            foreach (List<IDrawObject> layer in this.drawLayers)
            {
                foreach (IDrawObject obj in layer)
                {
                    obj.Draw();
                }
            }
            Visual.ResetRenderTarget();
            Visual.DrawTexture(this.internalTexture, this.textureArea, this.drawArea);
        }

        public override void Delete()
        {
            //  Visual.DeleteTexture(textures[texture_index].texture); since textures are stored as static variables we cannot Delete it
        }


        public void Add(IDrawObject drawObject)
        {
            this.drawLayers[drawObject.Z].Add(drawObject);
        }
        public bool Remove(IDrawObject drawObject)
        {
            return this.drawLayers[drawObject.Z].Remove(drawObject);
        }

    }    
}