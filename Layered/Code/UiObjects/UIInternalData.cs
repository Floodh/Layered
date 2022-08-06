using System.Drawing;
using System.Collections;

namespace Layered.UIObject{


    //  When placed in LayerWeb
    //  Will check UserInput and update variables accordingly.
    public readonly struct UIInternalData
    {

        public readonly bool keyboardBlocked;
        public readonly bool mouseBlocked;
        public readonly bool mouseDownBlocked;
        public readonly bool mouseUpBlocked;

        public UIInternalData(
            bool mouseBlocked = false, 
            bool keyboardBlocked = false, 
            bool mouseDownBlocked = false,
            bool mouseUpBlocked = false
        )
        {
            this.mouseBlocked = mouseBlocked;
            this.keyboardBlocked = keyboardBlocked;
            this.mouseDownBlocked = mouseDownBlocked;
            this.mouseUpBlocked = mouseUpBlocked;
        }


    }

}