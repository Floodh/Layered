using System.Drawing;

using Layered.Basic;

namespace Layered.UIObject{


    //  When placed in LayerWeb
    //  Will check if mouse actions are inside it's infulence and edit UiInternalData acordingly
    public class Blocker : UIObject
    {


        public Rectangle bounds;
        public Blocker(int z, Rectangle bounds)
        {
            this.Z = z;
            this.bounds = bounds;
        }

        //  returns if the mouse should be considered blocked
        public override UIInternalData Check(UIInternalData data) //  maybe pass bool that says if mouse is blocked by blocker
        {
            return new UIInternalData(
                bounds.Contains(UserInput.Mouse.position) | data.mouseBlocked,
                data.keyboardBlocked | bounds.Contains(UserInput.Mouse.position),
                data.mouseDownBlocked | bounds.Contains(UserInput.Mouse.position),
                data.mouseUpBlocked | bounds.Contains(UserInput.Mouse.position)
            );
        }
        public override void Delete()
        {
        }

    }

}