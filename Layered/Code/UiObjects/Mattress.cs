using System.Drawing;

using Layered.Basic;

namespace Layered.UIObject{



    //  base class for all mattresses
    //  having bounds here might be a bit redundant
    public abstract class Mattress : UIObject
    {

        public Rectangle bounds;
        public List<IUIArea> areas = new List<IUIArea>();

        public Mattress(int z, Rectangle bounds)
        {
            this.Z = z;
            this.bounds = bounds;
        }

    }

}