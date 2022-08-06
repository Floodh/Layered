using System.Drawing;

using Layered.Basic;

namespace Layered.UIObject{


    //  When placed in LayerWeb
    //  Will check UserInput and update variables accordingly.
    public class Hitbox : UIObject
    {


        public bool underMouse {get; private set;} = false;

        private readonly Rectangle bounds;

        public Hitbox(int z, Rectangle bounds)
        {
            this.Z = z;
            this.bounds = bounds;
        }

        //  returns if the mouse should be considered blocked
        public override UIInternalData Check(UIInternalData data) //  maybe pass bool that says if mouse is blocked by blocker
        {
            this.underMouse = this.bounds.Contains(UserInput.Mouse.position) & !data.mouseBlocked;
            return data;
        }
        public override void Delete()
        {

        }

    }

}