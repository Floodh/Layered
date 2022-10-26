using System;
using System.Drawing;

using Layered.Internal;
using Layered.Basic;

//  note:
//      this class acutllay does acces UserInput even though that responsability would normaly fall on the UiObjects

namespace Layered.DrawObject
{

    //  todo implement a proper base Option class to support more option structures


    public class OptionEntry : OptionBase
    {

        public const int entryPading = 0;


        
        public new Point drawPoint {
            get {return base.drawPoint;} 
            set {
                base.drawPoint = value;
                this.drawArea = this.drawArea;
            }
        }

        private Rectangle draggHitboxBasedDrawArea;
        public new Rectangle drawArea {
            get {return base.drawArea;} 
            set {
                base.drawArea = value;
                draggHitboxBasedDrawArea = value;
                this.hitbox.bounds = value;
                if (text== null)
                    throw new Exception("text is somehow null");
                this.text.drawPoint = new Point(this.drawPoint.X + 4, this.drawPoint.Y);
            }
        }

        public bool isSelected {get; private set;} = false;  
        private UIObject.DraggHitbox hitbox;

        public string content = "";

        //  on buttong press recive content as argument and sends out new content
        public delegate string OnButtonPressAction(string content);
        protected OnButtonPressAction? onButtonPressAction;
        
        public OptionEntry(int z, Rectangle drawArea, Color drawColor, OnButtonPressAction? onButtonPress, bool onOff = true)
            : base(z, drawArea, drawColor, null, onOff)
        {
            if (this.button == null || this.blocker == null)
                throw new Exception("OptionBasic Base construtor did not initilize button and blocker (atleast one is null)");

            hitbox = new UIObject.DraggHitbox(Math.Abs(z), this.drawArea);
            draggHitboxBasedDrawArea = this.drawArea;   //  optimaztion

            UserInput.keyDownLiseners.Add(UpdateWhenChecked);
            AddText("KosugiMaru-Regular.ttf", Color.Azure, this.content);

 
            base.action = this.OnButtonPress;   //  can't send this into constructor because this refrence is not initialized
            this.onButtonPressAction = onButtonPress;

        }

        public void UpdateWhenChecked(string keyName)
        {
            //  if the bounds are based on a diffrent drawArea
            if (this.draggHitboxBasedDrawArea != this.drawArea)
                this.drawArea = this.drawArea;
        
            CheckSelected();

            if (isSelected)
                if (keyName.Length == 1)
                    this.content += keyName;

        }

        private bool OnButtonPress(bool onOff)
        {
            if (this.onButtonPressAction != null)
                this.content = this.onButtonPressAction.Invoke(this.content);
            else 
                this.content = "";
            return !onOff;
        }


        //  in this case we wan't to override the default draw function
        public override void Draw()
        {
            if (this.draggHitboxBasedDrawArea != this.drawArea)
                this.drawArea = this.drawArea;
            CheckSelected();


            if (text == null)
                throw new Exception("Text DrawObject in OptionEntry is null when it is supposed to never be null");
            //  we don't need to update the dragghitboxArea here
            if (this.content != this.text.textStr)
                this.text.textStr = this.content;

            //  actual draw part
            base.Draw();
            if (isSelected)
                Visual.DrawRectShell(new Rectangle(this.drawArea.X + 1, this.drawArea.Y + 1, this.drawArea.Width - 2, this.drawArea.Height -2), Color.Azure);

            this.text.Draw();

            Rectangle drawRect = buttonArea;
            Visual.DrawRectShell(buttonArea, Color.Azure); 

            if (this.onOff)
            {
                int reduceBy = this.buttonArea.Width / 4;
                Visual.DrawRect(
                    new Rectangle(buttonArea.X + reduceBy, buttonArea.Y + reduceBy, buttonArea.Width - reduceBy * 2, buttonArea.Height - reduceBy * 2), 
                    Color.Azure);
            }

            if (this.button.state == UIObject.ButtonPressable.State.BeingPressed)
            {
                int reduceBy = 1;
                Visual.DrawRectShell(
                    new Rectangle(buttonArea.X + reduceBy, buttonArea.Y + reduceBy, buttonArea.Width - reduceBy * 2, buttonArea.Height - reduceBy * 2), 
                    Color.Azure);
                reduceBy = 2;
                Visual.DrawRectShell(
                    new Rectangle(buttonArea.X + reduceBy, buttonArea.Y + reduceBy, buttonArea.Width - reduceBy * 2, buttonArea.Height - reduceBy * 2), 
                    Color.Azure);
                reduceBy = 3;
                Visual.DrawRectShell(
                    new Rectangle(buttonArea.X + reduceBy, buttonArea.Y + reduceBy, buttonArea.Width - reduceBy * 2, buttonArea.Height - reduceBy * 2), 
                    Color.Azure);      
            }
            
        }

        public override void Delete()
        {
            base.Delete();           
        }

        private void CheckSelected()
        {
            this.isSelected = 
                this.enabled && 
                (this.drawArea.Contains(UserInput.Mouse.left.down) && this.drawArea.Contains(UserInput.Mouse.left.up) && (hitbox.startPoint != Point.Empty || hitbox.endPoint != Point.Empty))  //  this solution does not account for UiData, meaning it can't be blocked by other Ui Elements
                //(hitbox.startPoint == UserInput.Mouse.left.down && hitbox.endPoint == UserInput.Mouse.right.up)
            ;
        }


    } 

}  