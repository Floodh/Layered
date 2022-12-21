using System.Drawing;

using Layered.Basic;

namespace Layered.UIObject{



    //  if left down is being pressed within bounds
    //  dragg all associated areas
    public class MattressSelect : Mattress
    {
        public enum UiStatusEnum {none, beingSelected, selected}

        private bool    startedSelecting = false;
        private Point   prevMousePosition;
        private Point   prevMouseLeftDown;

        private IUIArea? potentialSelection = null;

        public Queue<IUIArea> selected = new Queue<IUIArea>();


        public MattressSelect(int z, Rectangle bounds)
            : base(z, bounds)
        {}

        public override UIInternalData Check(UIInternalData data)
        {
            Point mousePos = UserInput.Mouse.position;
            Point mouseDown = UserInput.Mouse.left.down;
            Point mouseUp = UserInput.Mouse.left.up;


            if (this.startedSelecting)
            {
                if (!UserInput.Mouse.left.pressed)
                {
                    this.startedSelecting = false;

                    if (this.potentialSelection != null)
                    {
                        this.potentialSelection.uiStatusEnum = (int)UiStatusEnum.none;
                        if (this.bounds.Contains(mouseUp))
                        if (potentialSelection.bounds.Contains(mouseUp))
                        {
                            this.selected.Enqueue(potentialSelection);   //  maybe change this to a delegate system?
                            this.potentialSelection.uiStatusEnum = (int)UiStatusEnum.selected;
                            this.potentialSelection = null;
                        }
                    }

                    

                }
            }
            else
            {
                //  this will not work if the user presses the exact same pixel position twice in a row.
                //  the mouse struct needs to provide a way to do a more primitive key down check
                if (prevMouseLeftDown != mouseDown)
                if (this.bounds.Contains(mouseDown))
                {
                    this.prevMouseLeftDown = mouseDown;     //  to diffrentiate between new and old mouse clicks, is flawed, fix this
                    foreach (IUIArea area in this.areas)
                    {
                        //  were just searching for one element that can be selected
                        if (area.bounds.Contains(mouseDown))
                        {
                            this.potentialSelection = area;
                            this.potentialSelection.uiStatusEnum = (int)UiStatusEnum.beingSelected;
                            this.startedSelecting = true;
                            

                            data = new UIInternalData(
                                true, 
                                data.keyboardBlocked, 
                                true, 
                                data.mouseUpBlocked);
                            break;
                        }
                    }  
                    
                    
                }
            }

            this.prevMousePosition = UserInput.Mouse.position;

            return data;

            
        }

        public override void Delete()
        {}



    }

}