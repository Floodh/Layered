using System.Drawing;

using Layered.Basic;

namespace Layered.UIObject{



    //  if left down is being pressed within bounds
    //  dragg all associated areas
    public class MattressDragg : Mattress
    {


        public int draggSpeed, draggSpeedMax;

        private bool    startedDragging = false;
        private Point   prevMousePosition;
        private Point   prevMouseLeftDown;


        public MattressDragg(int z, Rectangle bounds, int draggSpeed, int draggSpeedMax)
            : base(z, bounds)
        {
            this.prevMousePosition = UserInput.Mouse.position;
            this.prevMouseLeftDown = UserInput.Mouse.left.down;
        }

        public override UIInternalData Check(UIInternalData data)
        {
            Point mousePos = UserInput.Mouse.position;
            Point mouseDown = UserInput.Mouse.left.down;

            if (this.startedDragging)
            {
                if (!UserInput.Mouse.left.pressed)
                    this.startedDragging = false;
            }
            else
            {
                //  this will not work if the user presses the exact same pixel position twice in a row.
                //  the mouse struct needs to provide a way to do a more premitive key down check
                if (prevMouseLeftDown != mouseDown)
                {
                    
                    this.prevMouseLeftDown = mouseDown;
                    if (this.bounds.Contains(mouseDown))
                    if (!data.mouseDownBlocked)
                    {
                        //this.prevMousePosition = mousePos;
                        this.startedDragging = true;
                    }
                }
            }

            if (this.startedDragging)
            {
                //  do stuff here
                foreach (IUIArea stuckedArea in this.areas)
                {
                    Point p = stuckedArea.bounds.Location;
                    p.Offset(
                        UserInput.Mouse.position.X - this.prevMousePosition.X,
                        UserInput.Mouse.position.Y - this.prevMousePosition.Y
                    );
                    stuckedArea.bounds = new Rectangle(p, stuckedArea.bounds.Size);
                }

            }
            this.prevMousePosition = UserInput.Mouse.position;

            return new UIInternalData(
                data.mouseBlocked | this.bounds.Contains(mousePos), 
                data.keyboardBlocked, 
                data.mouseDownBlocked, 
                data.mouseUpBlocked);
            
        }

        public override void Delete()
        {}



    }

}