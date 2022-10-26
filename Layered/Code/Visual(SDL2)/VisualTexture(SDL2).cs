using SDL2;
using System;
using System.Drawing;
using System.Runtime.InteropServices;   //  for Marshall

namespace Layered.Internal{

    //  
    public static partial class Visual
    {
        //RGBA8888 is the RGB pattern
        //private static IntPtr texture =
        private static SDL.SDL_Rect textureArea = new SDL.SDL_Rect(){x = 0, y = 0};    //  the size of the texture
        private static SDL.SDL_Rect screenArea = new SDL.SDL_Rect(){x = 0, y = 0};     //  the area of the screen the texture will be drawn to
        private static IntPtr textureArea_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(textureArea));
        private static IntPtr screenArea_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(screenArea));

        

        public static IntPtr LoadTexture(string path)
        {
            IntPtr texture = SDL_image.IMG_LoadTexture(renderer, path);
            if (((long)texture) != 0) return texture;
                texture = SDL_image.IMG_LoadTexture(renderer, path);
            if (((long)texture) != 0) return texture;          
                Console.WriteLine("Failed to load texture: " + path);   
            return texture;
        }

        public static IntPtr LoadTexture(Bitmap bitmap)
        {
            IntPtr texture = SDL.SDL_CreateTexture(
                renderer, 
                SDL.SDL_PIXELFORMAT_RGB888, 
                2,              //  SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET
                bitmap.Width, bitmap.Height);

            SDL.SDL_SetRenderTarget(renderer, texture);

            for (int y = 0; y < bitmap.Height; y++)
            for (int x = 0; x < bitmap.Width; x++)
            {
                //  y is not inverted in .bmp formats
                Color color = bitmap.GetPixel(x,bitmap.Height - y);
                SDL.SDL_SetRenderDrawColor(renderer, color.R, color.G, color.B, color.A);
                SDL.SDL_RenderDrawPoint(renderer, x, y);


            }

            //  so that we can render the window again
            SDL.SDL_SetRenderTarget(renderer, IntPtr.Zero);

            return texture;

        }

        public static IntPtr LoadTexture(Size size, Color color)
        {
            IntPtr texture = SDL.SDL_CreateTexture(
                renderer, 
                SDL.SDL_PIXELFORMAT_RGB888, 
                2,              //  SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET
            size.Width, size.Height);

            SDL.SDL_SetRenderTarget(renderer, texture);
            rect_sample.x = 0;
            rect_sample.y = 0;
            rect_sample.w = size.Width;
            rect_sample.h = size.Height;

            Marshal.StructureToPtr<SDL2.SDL.SDL_Rect>(rect_sample, rect_ptr, false);
            SDL.SDL_SetRenderDrawColor(renderer, color.R, color.G, color.B, color.A);
            SDL.SDL_RenderFillRect(renderer, rect_ptr);
            SDL.SDL_SetRenderTarget(renderer, IntPtr.Zero);

            return texture;
        }




        public static void DrawTexture(IntPtr texture, Rectangle textureArea, Rectangle screenArea)
        {
            Visual.textureArea = new SDL.SDL_Rect(){x = textureArea.X, y = textureArea.Y, w = textureArea.Width, h = textureArea.Height};
            Visual.screenArea = new SDL.SDL_Rect(){x = screenArea.X, y = screenArea.Y, w = screenArea.Width, h = screenArea.Height};
            Marshal.StructureToPtr<SDL2.SDL.SDL_Rect>(Visual.textureArea, textureArea_ptr, false);
            Marshal.StructureToPtr<SDL2.SDL.SDL_Rect>(Visual.screenArea, screenArea_ptr, false);
            SDL.SDL_RenderCopy(renderer, texture, textureArea_ptr, screenArea_ptr);  //texture_ptr pointer to the actual texture, will be held by the draw object
        }

        // public static void DrawTexture(IntPtr renderTargetTexture, IntPtr texture, Rectangle textureArea, Rectangle screenArea)
        // {

        // }

        public static void DeleteTexture(IntPtr texture)
        {
            if (texture != null)
                SDL.SDL_DestroyTexture(texture);
        }

        public static Color AvgColorOfTexture(IntPtr texture)
        {
            byte r,g,b,a;
            SDL.SDL_GetTextureColorMod(texture, out r,out g, out b);
            SDL.SDL_GetTextureAlphaMod(texture, out a);
            return Color.FromArgb(a,r,g,b);

        }











        //  Edit points

        //  Edit a single point
        public static void EditTexture(IntPtr texture, Point point, Color color)
        {
            SDL.SDL_SetRenderTarget(renderer, texture);
            SDL.SDL_SetRenderDrawColor(renderer, color.R, color.G, color.B, color.A);
            SDL.SDL_RenderDrawPoint(renderer, point.X, point.Y);
            SDL.SDL_SetRenderTarget(renderer, IntPtr.Zero);

        }

        //  Edit several point with one color
        public static void EditTexture(IntPtr texture, Point[] points, Color color)
        {
            SDL.SDL_SetRenderTarget(renderer, texture);
            SDL.SDL_SetRenderDrawColor(renderer, color.R, color.G, color.B, color.A);
            foreach (Point p in points)
            {
                SDL.SDL_RenderDrawPoint(renderer, p.X, p.Y);
            }
            SDL.SDL_SetRenderTarget(renderer, IntPtr.Zero);

        }

        //  Edit several point with one color
        public static void EditTexture(IntPtr texture, List<Point> points, Color color)
        {
            SDL.SDL_SetRenderTarget(renderer, texture);
            SDL.SDL_SetRenderDrawColor(renderer, color.R, color.G, color.B, color.A);
            foreach (Point p in points)
            {
                SDL.SDL_RenderDrawPoint(renderer, p.X, p.Y);
            }
            SDL.SDL_SetRenderTarget(renderer, IntPtr.Zero);

        }

        //  Edit several point with respektive colors
        public static void EditTexture(IntPtr texture, Point[] points, Color[] color)
        {
            SDL.SDL_SetRenderTarget(renderer, texture);
            for (int i = 0; i < points.Length; i++)
            {
                SDL.SDL_SetRenderDrawColor(renderer, color[i].R, color[i].G, color[i].B, color[i].A);
                SDL.SDL_RenderDrawPoint(renderer, points[i].X, points[i].Y);
            }
            SDL.SDL_SetRenderTarget(renderer, IntPtr.Zero);

        }

        //  Edit several point with respektive colors
        public static void EditTexture(IntPtr texture, List<Point> points, Color[] color)
        {
            SDL.SDL_SetRenderTarget(renderer, texture);
            for (int i = 0; i < points.Count; i++)
            {
                SDL.SDL_SetRenderDrawColor(renderer, color[i].R, color[i].G, color[i].B, color[i].A);
                SDL.SDL_RenderDrawPoint(renderer, points[i].X, points[i].Y);
            }
            SDL.SDL_SetRenderTarget(renderer, IntPtr.Zero);

        }


        //  Edit areas with one color
        public static void EditTexture(IntPtr texture, Rectangle rect, Color color)
        {
            SDL.SDL_SetRenderTarget(renderer, texture);
            SDL.SDL_SetRenderDrawColor(renderer, color.R, color.G, color.B, color.A);
            rect_sample.x = rect.X;
            rect_sample.y = rect.Y;
            rect_sample.w = rect.Width;
            rect_sample.h = rect.Height;
            Marshal.StructureToPtr<SDL2.SDL.SDL_Rect>(rect_sample, rect_ptr, false);
            SDL.SDL_RenderFillRect(renderer, rect_ptr);
            SDL.SDL_SetRenderTarget(renderer, IntPtr.Zero);

        }


        //  Edit areas with one color
        public static void EditTexture(IntPtr texture, Rectangle[] rects, Color color)
        {
            SDL.SDL_SetRenderTarget(renderer, texture);
            SDL.SDL_SetRenderDrawColor(renderer, color.R, color.G, color.B, color.A);
            foreach (Rectangle rect in rects)
            {

                rect_sample.x = rect.X;
                rect_sample.y = rect.Y;
                rect_sample.w = rect.Width;
                rect_sample.h = rect.Height;
                Marshal.StructureToPtr<SDL2.SDL.SDL_Rect>(rect_sample, rect_ptr, false);
                SDL.SDL_RenderFillRect(renderer, rect_ptr);

            }
            SDL.SDL_SetRenderTarget(renderer, IntPtr.Zero);

        }
        //  Edit areas with respektive colors
        public static void EditTexture(IntPtr texture, Rectangle[] rects, Color[] colors)
        {
            SDL.SDL_SetRenderTarget(renderer, texture);
            for (int i = 0; i < rects.Length; i++)
            {
                
                rect_sample.x = rects[i].X;
                rect_sample.y = rects[i].Y;
                rect_sample.w = rects[i].Width;
                rect_sample.h = rects[i].Height;
                Marshal.StructureToPtr<SDL2.SDL.SDL_Rect>(rect_sample, rect_ptr, false);

                SDL.SDL_SetRenderDrawColor(renderer, colors[i].R, colors[i].G, colors[i].B, colors[i].A);
                SDL.SDL_RenderFillRect(renderer, rect_ptr);
            }
            SDL.SDL_SetRenderTarget(renderer, IntPtr.Zero);

        }

        //  Edit areas with one color
        public static void EditTexture(IntPtr texture, List<Rectangle> rects, Color color)
        {
            SDL.SDL_SetRenderTarget(renderer, texture);
            SDL.SDL_SetRenderDrawColor(renderer, color.R, color.G, color.B, color.A);
            foreach (Rectangle rect in rects)
            {

                rect_sample.x = rect.X;
                rect_sample.y = rect.Y;
                rect_sample.w = rect.Width;
                rect_sample.h = rect.Height;
                Marshal.StructureToPtr<SDL2.SDL.SDL_Rect>(rect_sample, rect_ptr, false);
                SDL.SDL_RenderFillRect(renderer, rect_ptr);

            }
            SDL.SDL_SetRenderTarget(renderer, IntPtr.Zero);

        }
        //  Edit areas with respektive colors
        public static void EditTexture(IntPtr texture, List<Rectangle> rects, Color[] colors)
        {
            SDL.SDL_SetRenderTarget(renderer, texture);
            for (int i = 0; i < rects.Count; i++)
            {
                
                rect_sample.x = rects[i].X;
                rect_sample.y = rects[i].Y;
                rect_sample.w = rects[i].Width;
                rect_sample.h = rects[i].Height;
                Marshal.StructureToPtr<SDL2.SDL.SDL_Rect>(rect_sample, rect_ptr, false);

                SDL.SDL_SetRenderDrawColor(renderer, colors[i].R, colors[i].G, colors[i].B, colors[i].A);
                SDL.SDL_RenderFillRect(renderer, rect_ptr);
            }
            SDL.SDL_SetRenderTarget(renderer, IntPtr.Zero);

        }


        public static void EditTexture(IntPtr targetedTexture, IntPtr texture, Rectangle editArea, Rectangle textureArea)
        {
            SDL.SDL_SetRenderTarget(renderer, targetedTexture);

            Visual.textureArea  = new SDL.SDL_Rect(){x = textureArea.X, y = textureArea.Y, w = textureArea.Width, h = textureArea.Height};
            Visual.screenArea   = new SDL.SDL_Rect(){x = editArea.X, y = editArea.Y, w = editArea.Width, h = editArea.Height};
            Marshal.StructureToPtr<SDL2.SDL.SDL_Rect>(Visual.textureArea, textureArea_ptr, false);
            Marshal.StructureToPtr<SDL2.SDL.SDL_Rect>(Visual.screenArea, screenArea_ptr, false);


            SDL.SDL_RenderCopy(renderer, texture, textureArea_ptr, screenArea_ptr);  //texture_ptr pointer to the actual texture, will be held by the draw object
            SDL.SDL_SetRenderTarget(renderer, IntPtr.Zero);
        }

    }



}
