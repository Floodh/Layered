using System;
using System.Drawing;

using Layered.Internal;
using Layered.Basic;

namespace Layered.DrawObject
{

    
    public class ButtonToggle : DrawObject
    {

        private static readonly Size textureSize = new Size(512,512);

        public Rectangle drawArea {get; private set;}

        private SimpleTexture onTexture;
        private SimpleTexture offTexture;

        public bool toggled;
        public bool on      {get {return    toggled;}}
        public bool off     {get {return   !toggled;}}

        private UIObject.ButtonPressable button;

        public delegate void userDefinedAction(bool toggled);
        private userDefinedAction toggleAction;

        //  it is the user responsibility to ensure that the texture area matches the Size of the draw area
        public ButtonToggle(int z, Rectangle drawArea, userDefinedAction toggleAction, SimpleTexture toggledTexture, SimpleTexture untoggledTexture, bool startAsToggled = false)
        {
            this.Z = z;
            this.drawArea = drawArea;
            this.toggled = startAsToggled;
            this.onTexture = toggledTexture;
            this.offTexture = untoggledTexture;
            this.button = new UIObject.ButtonPressable(z, drawArea);
            this.toggleAction = toggleAction;

            this.onTexture.screenDrawArea = this.drawArea;
            this.offTexture.screenDrawArea = this.drawArea;

            if (Settings.autoAddUIObjects == false)
                LayeredLayout.Add(this.button);
        }

        public override void Draw()
        {


            //Visual.DrawRect(this.drawArea, Color.Aqua);
            if (this.toggled)
            {
                this.onTexture.Draw();
            }
            else 
            {
                this.offTexture.Draw();
            }
            
        }

        public override void Delete()
        {
            this.button.Delete();
            LayeredLayout.Remove(this.button);

        }

        public void Update()
        {
            //Console.WriteLine($"State = {this.button.state}");
            if (this.button.state == UIObject.ButtonPressable.State.Pressed)
            {
                button.state = UIObject.ButtonPressable.State.Enabled;
                this.toggled = !this.toggled;
                this.toggleAction.Invoke(this.toggled);
            }

        }

    }    
}
