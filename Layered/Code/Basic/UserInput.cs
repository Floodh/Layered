
using SDL2;
using System.Drawing;
using System.Collections.Generic;


// //  this file defines the implementation of the mouse and keyboard settings

//  notes:
//
//      - The update functions keyup and keydown will check all keys incase some keys share the same keycode, which means that there is no break command after finding a matching key

namespace Layered.Basic
{

    public static class UserInput {


        //  Predefined variables
        public static bool              quit            = false;

        //  mouse
        //      is implemented as a static class
 

        //  Keyboard
        private static Dictionary<string, SDL.SDL_Keycode> keycodeDict = new Dictionary<string, SDL.SDL_Keycode>();
        private static Dictionary<SDL.SDL_Keycode, KeyboardKey> keyboardDict = new Dictionary<SDL.SDL_Keycode, KeyboardKey>();
        //private static KeyboardKey[]     keyboard = new KeyboardKey[0];

        public delegate void LisenForKeyDown(string keyNameFromValue);
        public static List<LisenForKeyDown> keyDownLiseners = new List<LisenForKeyDown>();

        private static void SendToKeyDownLiseners(SDL.SDL_Keycode keycode)
        {
            string keyStr = SDL.SDL_GetKeyName(keycode);
            foreach (LisenForKeyDown action in keyDownLiseners)
                action.Invoke(keyStr);
            
        }

        public static KeyboardKey GetKeyboardKey(string actionName)
        {
            return keyboardDict[keycodeDict[actionName]];
        }


        private static void UpdateMouseDown(SDL.SDL_MouseButtonEvent click)
        {
            //Console.WriteLine($"Mouse {click.button}");
            Mouse.mousekey[click.button] = new Mouse.MouseKey(new Point(click.x, click.y), Mouse.mousekey[click.button].up, true);
        }

        private static void UpdateMouseUp(SDL.SDL_MouseButtonEvent click)
        {
            Mouse.mousekey[click.button] = new Mouse.MouseKey(Mouse.mousekey[click.button].down, new Point(click.x, click.y), false);
        }

        private static void UpdateMousePosition(SDL.SDL_MouseMotionEvent motion)
        {
            Mouse.position = new Point(motion.x, motion.y);
            Mouse.moved = true;
        }

        private static void UpdateKeyDown(SDL.SDL_Keycode keycode){


            keyboardDict[keycode] = new KeyboardKey(true, Time.sec);

        }

        private static void UpdateKeyUp(SDL.SDL_Keycode keycode){

            keyboardDict[keycode] = new KeyboardKey(false, keyboardDict[keycode].last_pressed);

        }

        public static void UpdateAll(){

            Mouse.moved = false;    //  set the bool to false, then if the mouse has moved it will become true since one event will be of motion type
            SDL.SDL_Event    event_variable;
            while ( SDL.SDL_PollEvent(out event_variable) != 0 ){

                switch (event_variable.type){
                    // quiting via x buttom
                    case SDL.SDL_EventType.SDL_QUIT:
                        quit = true;
                        break;       
                    // if a key is pressed
                    case SDL.SDL_EventType.SDL_KEYDOWN:
                        UpdateKeyDown(event_variable.key.keysym.sym);
                        SendToKeyDownLiseners(event_variable.key.keysym.sym);
                        break;
                    // if a key is released
                    case SDL.SDL_EventType.SDL_KEYUP:
                        UpdateKeyUp(event_variable.key.keysym.sym);
                        break;
                    // if mouse button is pressed
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                        UpdateMouseDown(event_variable.button);
                        break;
                    // if mouse button is released
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                        UpdateMouseUp(event_variable.button);
                        break;
                    // if mouse moves
                    case SDL.SDL_EventType.SDL_MOUSEMOTION:
                        UpdateMousePosition(event_variable.motion);
                        break;

                }


            }

        }

        public static bool LoadBindings(string path = Settings.controlsBindingsFilePath, bool silent = false, int debugLevel = 1)
        {
            try 
            {

                foreach (string line in System.IO.File.ReadLines(path))
                {
                    Add(line, silent, debugLevel);
                }

            }
            catch
            {
                if (!silent)
                    Console.WriteLine(new string(' ', debugLevel * Settings.debugSpacing) + $"Something went wrong trying to load bindings");                        
                return false;
            }
            return true;
        }


        private static void Add(string line, bool silent = false, int debugLevel = 1)
        {

            
            string[] text = line.Split('=',2);
            if (text.Length > 1)
            {
                text[0] = text[0].Trim();
                text[1] = text[1].Trim();
                //if (text[0].Split(' ', 2)[0].Trim().ToLower() == "mousebuttons =")
                
                if (text[0].ToLower() == "mousebuttons")
                {
                    //  set mouse buttons
                    int result = 0;
                    if (Int32.TryParse(text[1], out result))
                    {

                        //          THIS IS BAD PRACTICE!!, consider reworking
                        Mouse.mousekey = new Mouse.MouseKey[result];
                        for (int i = 0; i < result; i++)
                            Mouse.mousekey[i] = new Mouse.MouseKey();
                        //


                        if (!silent)
                            Console.WriteLine(new string(' ', debugLevel * Settings.debugSpacing) + $"MouseButtons set to: <{result}>");                        
                        
                    }
                    else if (!silent)
                        Console.WriteLine(new string(' ', debugLevel * Settings.debugSpacing) + $"Could not parse to int for mouse buttons: <{text[1]}>");                        
                }
                // else if ()
                // {

                // }
                else
                {
                    // add key
                    SDL.SDL_Keycode keycode = SDL.SDL_GetKeyFromName(text[1]);
                    
                    keycodeDict.Add(text[0], keycode);
                    keyboardDict.Add(keycode, new KeyboardKey(false, 0));
                    

                    if (!silent)
                        Console.WriteLine(new string(' ', debugLevel * Settings.debugSpacing) + $"Loaded: {text[0]} = {keycode}");

                }

            }
            
        }

        public static class Mouse 
        {

            //  Mouse
            public static bool      moved         = false;

            public static Point     position      = new Point(10,10);
            
            public static MouseKey  left          { get {return mousekey[1];} }
            public static MouseKey  button        { get {return mousekey[2];} }
            public static MouseKey  right         { get {return mousekey[3];} }

            public static MouseKey[] mousekey     = new MouseKey[0];


            public readonly struct MouseKey
            {
                public readonly Point down;
                public readonly Point up;
                public readonly bool  pressed;
                public MouseKey(Point down, Point up, bool pressed)
                {
                    this.down = down;
                    this.up = up;
                    this.pressed = pressed;
                }
                public MouseKey()
                {
                    this.down = new Point();
                    this.up = new Point();
                    this.pressed = false;                    
                }
            }

        }
    }


    



    public readonly struct KeyboardKey
    {

        public readonly bool                pressed         = false;
        public readonly int                 last_pressed    = 0;

        public KeyboardKey(bool pressed, int last_pressed)
        {
            this.pressed = pressed;
            this.last_pressed = last_pressed;
        }

       
    }





}

