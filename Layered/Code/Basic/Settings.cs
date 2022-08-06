using System.Drawing;

namespace Layered.Basic   {


    public static class Settings{

        public static string windowName = "window";



        public static int   windowWidth = 1440;
        public static int   windowHeight = 1080;
        public static Size  windowSize {
            get {return new Size(windowWidth, windowHeight);}
            set {windowWidth = value.Width; windowHeight = value.Height;}
        }


        public static int frameRate = 60;



        
        public static int drawLayerDepth = 8;
        public static int uiLayerDepth = 8;

        public static bool autoAssignDrawObjects {
            get {return autoAddDrawObjects && autoRemoveDrawObjects;} 
            set {autoAddDrawObjects = value; autoRemoveDrawObjects = value;}}
        public static bool autoAddDrawObjects = true;
        public static bool autoRemoveDrawObjects = true;


        public static bool autoAssignUIObjects{
            get {return autoAddUIObjects && autoRemoveUIObjects;} 
            set {autoAddUIObjects = value; autoRemoveUIObjects = value;}}
        public static bool autoAddUIObjects = true;
        public static bool autoRemoveUIObjects = true;



        public const int debugSpacing = 5;


        
        public  const   string  resourcesFolderPath         =   "Layered\\Resources";
        public  const   string  defaultTextureFolderPath    =   resourcesFolderPath + "\\" + "Textures";
        public  const   string  fontFolderPath              =   resourcesFolderPath + "\\" + "Fonts";

        public  const   string  settingsFolderPath          =   "Layered\\Settings";
        public  const   string  controlsBindingsFilePath    =   settingsFolderPath + "\\" + "Controls.txt";
        //public  const   string  graphicFilePath             =   settingsFolderPath + "\\" + "Controls.txt";



        // //  UserInput
        // public const uint      mouse_buttons       = 3;
        // public const uint      keyboard_keys       = 10;



    }

}