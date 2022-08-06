using SDL2;


namespace Layered.Internal{

    public static partial class Visual{

        public static void ClearScreen(){

            SDL.SDL_SetRenderDrawColor(renderer, 0, 0, 0, 0);
            SDL.SDL_RenderClear(renderer);  

        }

        public static void ShowFrame(){

            SDL.SDL_RenderPresent(renderer);
            Visual.frame++;

        }

    }



}