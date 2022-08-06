using System;
using System.Drawing;

using Layered.Internal;
using Layered.Basic;

namespace Layered.DrawObject
{


    public class PlayButton : DrawObject
    {

        private static readonly Size textureSize = new Size(512,512);
        private const string playButtonTextureName  = "PlayButton.png";
        private const string pauseButtonTextureName = "PauseButton.png";

        public Rectangle drawArea {get; private set;}

        private SimpleTexture playTexture;
        private SimpleTexture pauseTexture;
        private Color backgroundColor;


        public bool toggled;
        private UIObject.ButtonPressable button;

        public delegate void UserDefinedAction(bool toggled);
        private UserDefinedAction toggleAction;

        //private UIObject.SimpleButton.userDefinedAction toggleAction;




        public PlayButton(int z, Rectangle drawArea, Color backgroundColor, UserDefinedAction toggleAction, bool startAsToggled = false)
        {
            this.Z = z; 
            this.drawArea = drawArea;
            this.toggled = startAsToggled;
            this.playTexture = new SimpleTexture(this.Z, drawArea, new Rectangle(Point.Empty, textureSize), playButtonTextureName);
            this.pauseTexture = new SimpleTexture(this.Z, drawArea, new Rectangle(Point.Empty, textureSize), pauseButtonTextureName);
            this.button = new UIObject.ButtonPressable(z, drawArea);
            this.toggleAction = toggleAction;
            this.backgroundColor = backgroundColor;

            if (Settings.autoAddUIObjects == false)
                LayeredLayout.Add(this.button);
        }

        public override void Draw()
        {


            Visual.DrawRect(this.drawArea, backgroundColor);
            if (this.toggled)
            {
                this.pauseTexture.Draw();
            }
            else 
            {
                this.playTexture.Draw();
            }
            
        }

        public override void Delete()
        {
            this.button.Delete();
            LayeredLayout.Remove(this.button);

        }

        public void Update()
        {
            if (this.button.state == UIObject.ButtonPressable.State.Pressed)
            {
                button.state = UIObject.ButtonPressable.State.Enabled;
                this.toggled = !this.toggled;
                this.toggleAction.Invoke(this.toggled);
            }

        }

    }    
}
