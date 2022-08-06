using System.Drawing;

using Layered.Basic;

namespace Layered.UIObject
{

    public class ButtonPressable : UIObject
    {
        public enum State {None, Disabled, Enabled, BeingPressed, Pressed}
        public State state = State.Enabled;
        private Point startPoint;
        private Point endPoint;


        public Rectangle bounds;

        /// <summary> Is called at the end of the objects Check() method. Recivies the currentState and returns the new state.</summary>
        public delegate State UserDefinedAction(State currentState);
        private UserDefinedAction? action;

        public ButtonPressable(int z, Rectangle bounds, UserDefinedAction? action = null)
        {
            this.Z = z;
            this.bounds = bounds;
            this.action = action;
        }

        //  
        public override UIInternalData Check(UIInternalData data) //  maybe pass bool that says if mouse is blocked by blocker
        {
            

            switch (this.state)
            {
                
                case State.Enabled:
                    if (this.bounds.Contains(UserInput.Mouse.left.down) && !data.mouseBlocked && UserInput.Mouse.left.pressed)
                    {
                        this.startPoint = UserInput.Mouse.left.down;
                        this.state = State.BeingPressed;
                    }
                    break;
                case State.BeingPressed:
                    if (!UserInput.Mouse.left.pressed)
                    {
                        if (this.bounds.Contains(UserInput.Mouse.left.up) && !data.mouseBlocked)
                        {
                            this.endPoint = UserInput.Mouse.left.up;
                            this.state = State.Pressed;
                        }
                        else
                        {
                            this.state = State.Enabled;
                        }

                    }
                    break;
                default:
                    break;
            }

            if (this.action != null)
                this.state = action.Invoke(this.state);
            
            return new UIInternalData(
                bounds.Contains(UserInput.Mouse.position) | data.mouseBlocked,
                data.keyboardBlocked,
                data.mouseDownBlocked,
                data.mouseUpBlocked
            );
        }
        public override void Delete()
        {

        }

}

}