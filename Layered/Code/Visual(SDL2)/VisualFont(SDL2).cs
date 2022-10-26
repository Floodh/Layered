using   SDL2;
using   System.Drawing;

using   Layered.Basic;

namespace Layered.Internal{

    public static partial class Visual{

        //  specialised function for drawing text, use the normal draw texture function
        //  if you wan't more control

        //  The functions used are the UNICODE functions
        //  All functions assumes this is the desired format


        public static void DrawTextTexture(IntPtr textTexture, int x, int y)
        {
            DrawTextTexture(textTexture, new Rectangle(x, y, Settings.windowWidth * 8, Settings.windowHeight * 8));
        }
        public static void DrawTextTexture(IntPtr textTexture, Point pos)
        {
            DrawTextTexture(textTexture, pos.X, pos.Y);
        }
        public static void DrawTextTexture(IntPtr textTexture, Rectangle drawArea)      //  primary function
        {
            Rectangle textureArea = new Rectangle(0,0, drawArea.Width, drawArea.Height);
            Visual.DrawTexture(textTexture, textureArea, drawArea);
        }


        public static IntPtr LoadTextTexture(IntPtr font, string text, Color color)
        {
            if (font == IntPtr.Zero)
            {
                throw new ArgumentException("font is null");
            }
            SDL.SDL_Color fontColor = new SDL.SDL_Color(){a = color.A, r = color.B, g = color.G, b = color.B};
            //IntPtr surface = SDL_ttf.TTF_RenderText_Solid(font, text, fontColor);
            if (text == "")
                text = " ";
            IntPtr surface = SDL_ttf.TTF_RenderUNICODE_Solid(font, text, fontColor);
            //IntPtr surface = SDL_ttf.TTF_RenderUNICODE_Blended_Wrapped(font, text, fontColor, 300);
            if (surface == IntPtr.Zero)
            {
                throw new InvalidOperationException("Failed to procces text into texture");
            }
            IntPtr texture = SDL.SDL_CreateTextureFromSurface(renderer, surface);
            SDL.SDL_FreeSurface(surface);
            return texture;   
        }

        public static int TextureSize(IntPtr font, string text, out int width, out int height)
        {
            return SDL2.SDL_ttf.TTF_SizeUNICODE(font, text, out width, out height);
        }


        public static int TextureWidth(IntPtr font, string text)
        {
            int width, height;
            //SDL2.SDL_ttf.TTF_SizeText(font, text, out width, out height);
            SDL_ttf.TTF_SizeUNICODE(font, text, out width, out height);
            return width;
        }
        public static int TextureHeight(IntPtr font, string text)
        {
            int width, height;
            SDL2.SDL_ttf.TTF_SizeUNICODE(font, text, out width, out height);
            return height;
        }

        public static IntPtr OpenFont(string path, int ptSize)
        {
           return SDL2.SDL_ttf.TTF_OpenFont(path, ptSize);
        }

        public static void CloseFont(IntPtr font)
        {
            SDL_ttf.TTF_CloseFont(font);
        }



    }



}
