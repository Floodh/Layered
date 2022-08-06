
using Layered.Basic;

namespace Layered.DrawObject
{

    public abstract class DrawObject : IDrawObject
    {


        /// <summary>
        /// The Z value determines in which layer the draw object should be drawn inside LayeredLayout.
        /// If autoAssignDrawObjects is set to true than the draw object will automatically be assigned
        //  and unassigned to LayeredLayout if the Z value > -1
        /// </summary>
        public int Z {
            get {return z;}
            set {

                if (Settings.autoRemoveDrawObjects && z > -1)      
                    LayeredLayout.Remove(this);
                z = value;
                if (Settings.autoAddDrawObjects && value > -1)
                    LayeredLayout.Add(this);
            }
        }
        private int z = -1;

        //  Draw function
        public abstract void Draw();

        //  ensure that there is no more leak, such as destroy textures
        public abstract void Delete();
    }

}