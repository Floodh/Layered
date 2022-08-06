using System;
using System.Drawing;

using Layered.Internal;
using Layered.Basic;

//  maybe remove?
//      the plan was to make use of delegates to update the instance
//      before it is drawn

namespace Layered.DrawObject
{


    // public class BarAuto : IDrawObject
    // {

    //     private static readonly Size textureSize = new Size(512,512);
    //     private const string playButtonTextureName  = "PlayButton.png";
    //     private const string pauseButtonTextureName = "PauseButton.png";
        

    //     public uint     Z {get; private set;} 

    //     public Rectangle drawArea {get; private set;}

    //     private SimpleTexture playTexture;
    //     private SimpleTexture pauseTexture;


    //     public bool toggled;
    //     private UIObject.ButtonPressable button;

    //     public delegate void userDefinedAction(bool toggled);
    //     private userDefinedAction toggleAction;

    //     //private UIObject.SimpleButton.userDefinedAction toggleAction;




    //     public PlayButton(uint z, Rectangle drawArea, userDefinedAction toggleAction, bool startAsToggled = false)
    //     {
    //         this.Z = z; 
    //         this.drawArea = drawArea;
    //         this.toggled = startAsToggled;
    //         this.playTexture = new SimpleTexture(this.Z, drawArea, new Rectangle(Point.Empty, textureSize), playButtonTextureName);
    //         this.pauseTexture = new SimpleTexture(this.Z, drawArea, new Rectangle(Point.Empty, textureSize), pauseButtonTextureName);
    //         this.button = new UIObject.ButtonPressable(z, drawArea);
    //         this.toggleAction = toggleAction;

    //         LayerWeb.Add(this.button);
    //     }

    //     public void Draw()
    //     {


    //         Visual.DrawRect(this.drawArea, Color.Aqua);
    //         if (this.toggled)
    //         {
    //             this.pauseTexture.Draw();
    //         }
    //         else 
    //         {
    //             this.playTexture.Draw();
    //         }
            
    //     }

    //     public void Delete()
    //     {
    //         this.button.Delete();
    //         LayerWeb.Remove(this.button);

    //     }

    //     public void Update()
    //     {
    //         if (this.button.state == UIObject.ButtonPressable.State.Pressed)
    //         {
    //             button.state = UIObject.ButtonPressable.State.Enabled;
    //             this.toggled = !this.toggled;
    //             this.toggleAction.Invoke(this.toggled);
    //         }

    //     }

    // }    
}