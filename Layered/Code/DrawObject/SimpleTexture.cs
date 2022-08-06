using System;
using System.Drawing;

using   Layered.Internal;
using   Layered.Basic;

namespace Layered.DrawObject
{


    public class SimpleTexture : DrawObject
    {

        private static  List<InternalTexture>   textures = new List<InternalTexture>(); 
        private         int                     texture_index;
        private         Rectangle               textureArea;
        protected       Rectangle               drawArea;  


        public SimpleTexture(int z, Rectangle drawArea, Rectangle textureArea, string texturenName, string textureFolderPath = Settings.defaultTextureFolderPath)
        {
            string texturePath = textureFolderPath;
            if (texturePath != "")
                texturePath += "\\";
            texturePath += texturenName;

            this.Z = z;
            
            bool add_new_texture = true;
            for (int i = 0; i < textures.Count; i++)
            {
                if (textures[i].path == texturePath)
                {
                    add_new_texture = false;
                    this.texture_index = i;
                    break;
                }
            }
            

            if (add_new_texture)
            {   
                texture_index = textures.Count;
                textures.Add(new InternalTexture(texturePath));
                if (textures.Last().texture == IntPtr.Zero)
                {
                    throw new InvalidOperationException($"Could not load new texture at {texturePath}");
                }
            }



            this.textureArea   =   textureArea;
            this.drawArea      =   drawArea;
        }

        //  properties
        public Size textureSize 
        {
            get { return this.textureArea.Size;} 
        }

        public Point drawPoint
        {
            get { return this.drawArea.Location;}
            set { this.drawArea.Location = value;}
        }

        public int X 
        {
            get { return this.drawArea.X;}
            set { this.drawArea.X = value;}
        }
        public int Y 
        {
            get { return this.drawArea.Y;}
            set { this.drawArea.Y = value;}
        }
        public Rectangle screenDrawArea
        {
            get {return this.drawArea;}
            set {this.drawArea = value;}
            
        }
        //


        //  functions
        public override void Draw()
        {
            Visual.DrawTexture(textures[texture_index].texture, textureArea, drawArea);
        }

        public override void Delete()
        {
            //  Visual.DeleteTexture(textures[texture_index].texture); since textures are stored as static variables we cannot Delete it
        }


        //  nested datatypes
        private readonly struct InternalTexture
        {
            public readonly string path;
            public readonly IntPtr texture;
            
            public InternalTexture(string texturePath)
            {
                this.path       =   texturePath;
                this.texture    =   Visual.LoadTexture(this.path);
            }
            public InternalTexture(string texturenName, string textureFolderPath = Settings.defaultTextureFolderPath)
                : this(textureFolderPath + "\\" + texturenName)
            {}

        }


    }    
}