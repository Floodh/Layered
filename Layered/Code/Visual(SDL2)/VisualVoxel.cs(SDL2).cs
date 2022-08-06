using SDL2;
using System;
using System.Runtime.InteropServices;   //  for Marshall
using System.Drawing;

namespace Layered.Internal{

    //  this clas
    public static partial class Visual{

        private static SDL.SDL_Rect rect_sample = new SDL.SDL_Rect();
        private static IntPtr rect_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(rect_sample));
        

        public static void DrawLine(Point p1, Point p2, Color rgb){

            SDL.SDL_SetRenderDrawColor(renderer, rgb.R, rgb.G, rgb.B, rgb.A);
            SDL.SDL_RenderDrawLine(renderer, p1.X, p1.Y, p2.X, p2.Y);

        }

        public static void DrawLines(Point[] connecting_points, Color rgb){

            SDL.SDL_SetRenderDrawColor(renderer, rgb.R, rgb.G, rgb.B, rgb.A);
            int counter = 0;
            while (counter < connecting_points.Length){
                SDL.SDL_RenderDrawLine(
                    renderer, connecting_points[counter].X, 
                    connecting_points[counter].Y, 
                    connecting_points[counter].X, 
                    connecting_points[counter].Y);
                counter++;
            }

        }

        public static void DrawRect(Rectangle rect, Color rgb){

            rect_sample.x = rect.X;
            rect_sample.y = rect.Y;
            rect_sample.w = rect.Width;
            rect_sample.h = rect.Height;

            Marshal.StructureToPtr<SDL2.SDL.SDL_Rect>(rect_sample, rect_ptr, false);
            SDL.SDL_SetRenderDrawColor(renderer, rgb.R, rgb.G, rgb.B, rgb.A);
            SDL.SDL_RenderFillRect(renderer, rect_ptr);

        }        

        public static void DrawRect(Point point, Size area, Color rgb){

            DrawRect(new Rectangle(point, area), rgb);

        }

        public static void DrawRect(int x, int y, int width, int height, Color rgb){

            DrawRect(new Rectangle(x, y, width, height), rgb);

        }

        public static void DrawRectShell(Rectangle rect, Color rgb){

            rect_sample.x = rect.X;
            rect_sample.y = rect.Y;
            rect_sample.w = rect.Width;
            rect_sample.h = rect.Height;

            Marshal.StructureToPtr<SDL2.SDL.SDL_Rect>(rect_sample, rect_ptr, false);
            SDL.SDL_SetRenderDrawColor(renderer, rgb.R, rgb.G, rgb.B, rgb.A);
            SDL.SDL_RenderDrawRect(renderer, rect_ptr);

        }        


        public static void DrawRectShell(Point point, Size area, Color rgb){

            DrawRectShell(new Rectangle(point, area), rgb);

        }

        public static void DrawRectShell(int x, int y, int width, int height, Color rgb){
            DrawRectShell(new Rectangle(x, y, width, height), rgb);
        }


    }

}
