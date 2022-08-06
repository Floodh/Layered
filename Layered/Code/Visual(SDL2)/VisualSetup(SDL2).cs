using   SDL2;
using   System.Runtime.InteropServices;   //  for Marshall
using   System; //for IntPtr

using   Layered.Basic;

namespace Layered.Internal{

    //  this clas
    public static partial class Visual{


        //
        public static void Setup(bool silent = false, int debugLevel = 1){


            //  init function for SDL2
            if (silent == false){Console.WriteLine(new string(' ', debugLevel * Settings.debugSpacing) + "Intilizing SDL2");}
            SDL.SDL_Init(SDL.SDL_INIT_VIDEO);
            if (silent == false){Console.WriteLine(new string(' ', debugLevel * Settings.debugSpacing) + "Intilizing SDL2 Audio");}
            SDL.SDL_Init(SDL.SDL_INIT_AUDIO);


            //  init font
            if (silent == false){Console.WriteLine(new string(' ', debugLevel * Settings.debugSpacing) + "Intilizing SDL2 TTF");}
            if  (SDL_ttf.TTF_Init() == -1) 
                throw new InvalidOperationException("Failed to intiate TTF");
            

            //  inti sdl image
            if (silent == false){Console.WriteLine(new string(' ', debugLevel * Settings.debugSpacing) + "Intilizing SDL2 image");}
            SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG);
            //  create a window and attach it to an IntPtr
            if (silent == false){Console.WriteLine(new string(' ', debugLevel * Settings.debugSpacing) + "Creating Window");}
            window = SDL.SDL_CreateWindow(
                Settings.windowName, 100, 50,
                Settings.windowWidth, 
                Settings.windowHeight, 
                SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL | SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN
            );
            //  create a renderer and attach it to an IntPtr
            if (silent == false){Console.WriteLine(new string(' ', debugLevel * Settings.debugSpacing) + "Creating renderer");}
            renderer = SDL.SDL_CreateRenderer(window, 0, 0);

            //  set blend mofe
            if (silent == false){Console.WriteLine(new string(' ', debugLevel * Settings.debugSpacing) + "Set blend mode");}
            SDL.SDL_SetRenderDrawBlendMode(renderer, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND);

            // //  load textures
            // if (silent == false){Console.WriteLine("               Loading textures");}
            // LoadAllTextures();  // found in VisualTextures
            Visual.frame = 0;


        }

        


    }



}
