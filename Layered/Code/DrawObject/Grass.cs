using System;
using System.Drawing;

using Layered.Internal;
using Layered.Basic;

namespace Layered.DrawObject
{


    public class Grass : DrawObject
    {


        public const string textureName = "Grass";
        public const string texturePath = Settings.defaultTextureFolderPath + "\\" + "Grass.png";

        // 16 x 16  * 4
        public static IntPtr texture; 
        public Rectangle texture_area = new Rectangle(16, 0, 16, 16);

        public Grass(int z)
        {
            this.Z = z;
            if (((long)texture) == 0) texture = Visual.LoadTexture(texturePath);
        }

        public override void Draw()
        {
            Visual.DrawTexture(texture, texture_area, Camera.ModifyRect( new Rectangle(16, 16, 64, 64) ));
        }

        public override void Delete()
        {

        }

    }    
}