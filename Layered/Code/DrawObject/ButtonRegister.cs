using System;
using System.Drawing;

using Layered.Internal;
using Layered.Basic;

namespace Layered.DrawObject
{

    

    public class ButtonRegister : DrawObject
    {
        private const int animationPhases = 6;
        private const int animationDurationFrames = 5;

        public Color color;
        private Rectangle drawArea;
        private Rectangle drawAreaInner;


        public bool animation;
        private int animationPhase = 0;
        private int animationFramesCount = 0;

        private UIObject.ButtonPressable button;

        public delegate void OnButtonPress();
        public OnButtonPress action;
        

        



        

        public ButtonRegister(int z, Rectangle drawArea, Color drawColor, OnButtonPress action)
        {
            this.Z = z;
            this.action = action;

            this.drawArea = drawArea;
            int shrinkPx = 2;
            this.drawAreaInner = new Rectangle(drawArea.X + shrinkPx, drawArea.Y + shrinkPx, drawArea.Width - shrinkPx * 2, drawArea.Height - shrinkPx * 2);

            

            this.button = new UIObject.ButtonPressable(this.Z, this.drawArea, Update);

        }


        public override void Draw()
        {
            Visual.DrawRect(this.drawArea, this.color);
            Visual.DrawRectShell(this.drawArea, Color.Azure);

            if (this.animation)
            {
                if (this.animationFramesCount++ >= animationDurationFrames)
                {
                    this.animationFramesCount = 0;
                    if (this.animationPhase++ >= animationPhases)
                    {
                        this.animation = false;
                        this.animationPhase = 0;
                    }
                }

                int reduceBy = this.animationPhase;
                Rectangle drawRect = new Rectangle(
                    this.drawAreaInner.X + reduceBy, this.drawAreaInner.Y + reduceBy,
                    this.drawAreaInner.Width - reduceBy * 2, this.drawAreaInner.Height - reduceBy * 2);

                Visual.DrawRect(drawRect, this.color);

            }

            //Visual.DrawRectShell(this.drawAreaInner, Color.Azure);

        }

        public override void Delete()
        {}

        private UIObject.ButtonPressable.State Update(UIObject.ButtonPressable.State state)
        {

            if (state == UIObject.ButtonPressable.State.Pressed)
            {
                state = UIObject.ButtonPressable.State.None;
                this.animation = true;
                this.animationFramesCount = 0;
                this.animationPhase = 0;

            }

            
            return state;

        }

    }

}