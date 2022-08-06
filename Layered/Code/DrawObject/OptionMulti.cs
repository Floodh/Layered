using System;
using System.Drawing;

using Layered.Internal;
using Layered.Basic;

namespace Layered.DrawObject
{

    //  todo implement a proper base Option class to support more option structures
    public class OptionMulti : DrawObject
    {

        public const string defualtOptionTexture = "Option_DefualtTexture.png";
        public static readonly Rectangle defualtOptionTextureArea = new Rectangle(0,0, 980, 980);

        public const int sectionTotalSizePx = 24;
        public const int lineThicknessPx = 4;
        public const byte alphaValue1 = 0xA0;
        public const byte alphaValue2 = 0xB0;
        public const byte alphaValue3 = 0x00;
        //public const int alphaValueBitmask = unchecked((int)0xFF000000);


        public Point drawPoint {
            get {return drawArea.Location;} 
            set {
                drawArea = new Rectangle(value, drawArea_org.Size);
            }
        }

        private     Rectangle       drawArea_org;
        public      Rectangle       drawArea {
            get {return this.drawArea_org;} 
            set {
                this.drawArea_org = value;
            }

        }
        

        public  Color drawColor1;
        public  Color drawColor2;
        public  Color drawColor3;

        public int width;
        private int ptSize; // text pixel size
    


        public delegate bool UserDefinedAction(bool onOff);
        private UserDefinedAction[] allResponses = new UserDefinedAction[0];
        private Text[] allTexts = new Text[0];

        



        


        


        //  Implement when there is a suitable defualt texture
        public OptionMulti(int z, Tuple<string, UserDefinedAction>[] options, 
            int width, Point drawPoint, int ptSize, string fontName, 
            Color drawColor1, Color drawColor2, Color drawColor3)
        {
            this.Z = z; 
            this.drawArea = new Rectangle(drawPoint, new Size(width, ptSize));

            this.drawColor1 = Color.FromArgb( alphaValue1, drawColor1.R, drawColor1.G, drawColor1.B);
            this.drawColor2 = Color.FromArgb( alphaValue2, drawColor2.R, drawColor2.G, drawColor2.B);
            this.drawColor3 = Color.FromArgb( alphaValue3, drawColor3.R, drawColor3.G, drawColor3.B);

            this.width = width;
            this.ptSize = ptSize;

            allResponses = new UserDefinedAction[options.Length];
            allTexts = new Text[options.Length];


            for (int i = 0; i < options.Length; i++)
            {
                allTexts[i] = new Text(-1, fontName, ptSize, this.drawColor3, options[i].Item1, 
                    new Point(this.drawPoint.X, this.drawPoint.Y + i * ptSize));
                allResponses[i] = options[i].Item2;
            }


        }

        public override void Draw()
        {

            for (int optionNumber = 0; optionNumber < this.allTexts.Length; optionNumber++)
            {

                Point drawPoint = this.drawPoint;

                for (int i = 0; i < lineThicknessPx; i++)
                {
                    Visual.DrawRectShell(new Rectangle(drawPoint, new Size(width - i * 2, sectionTotalSizePx - i * 2)), this.drawColor2);
                    drawPoint.Offset(1, 1);
                }

                drawPoint = this.drawPoint;
                drawPoint.Offset(lineThicknessPx, lineThicknessPx);
                Visual.DrawRect(new Rectangle(drawPoint, new Size(width - lineThicknessPx * 2, sectionTotalSizePx - lineThicknessPx * 2)), this.drawColor1);


            }





        }

        public override void Delete()
        {
        }



    } 

}  