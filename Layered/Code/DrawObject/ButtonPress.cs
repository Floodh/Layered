using System;
using System.Drawing;

using Layered.Internal;
using Layered.Basic;

namespace Layered.DrawObject
{


    //  simple button, one texture default texture, and one for being pressed
    public class ButtonPress : DrawObject
    {

        private static readonly Size textureSize = new Size(512,512);

        public Rectangle drawArea {get; private set;}

        private SimpleTexture playTexture;
        private SimpleTexture pauseTexture;
        private Color backgroundColor;


        public bool toggled;
        private UIObject.ButtonPressable button;

        public delegate void UserDefinedAction();
        private UserDefinedAction pressedAction;

        public ButtonPress(int z, Rectangle drawArea, Color backgroundColor, UserDefinedAction pressedAction, string texturePath_Default, string texturePath_Pressed, string folderPath = Settings.defaultTextureFolderPath,  bool startAsPressed = false)
        {
            this.Z = z; 
            this.drawArea = drawArea;
            this.toggled = startAsPressed;
            this.playTexture = new SimpleTexture(-1, drawArea, new Rectangle(Point.Empty, textureSize), texturePath_Default, folderPath);
            this.pauseTexture = new SimpleTexture(-1, drawArea, new Rectangle(Point.Empty, textureSize), texturePath_Pressed, folderPath);
            this.button = new UIObject.ButtonPressable(z, drawArea);
            this.pressedAction = pressedAction;
            this.backgroundColor = backgroundColor;

            if (Settings.autoAddUIObjects == false)
                LayeredLayout.Add(this.button);
        }

        public override void Draw()
        {


            Visual.DrawRect(this.drawArea, backgroundColor);
            if (this.button.state == UIObject.ButtonPressable.State.BeingPressed)
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
                button.state = UIObject.ButtonPressable.State.Enabled;      //  this set it such that the button can be pressed again
                this.pressedAction.Invoke();
            }

        }

    }    
}