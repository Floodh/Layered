using SDL2;
using System;
using System.Runtime.InteropServices;   //  for Marshall
using System.Drawing;

namespace Layered.Internal{

    //  this clas
    public static partial class Visual
    {
        //  set a texture as render target
        public static void SetRenderTarget(IntPtr texture)
        {
            SDL2.SDL.SDL_SetRenderTarget(renderer, texture);
        }
        //  set the window as render target
        public static void ResetRenderTarget()
        {
            SDL2.SDL.SDL_SetRenderTarget(renderer, IntPtr.Zero);
        }



    }
}
