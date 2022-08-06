#if Test

using   System;

using   Layered.Basic;
using   Layered.Internal;

using   System.Drawing;


namespace DieKurve{

    class MainClass
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");


            Visual.Setup();
            UserInput.LoadBindings();
            LayerWeb.Init();


            while ( UserInput.quit == false)
            {
                UserInput.UpdateAll();
                
                Visual.ClearScreen();
                LayerWeb.DrawAll();
                Visual.ShowFrame();


                SDL2.SDL.SDL_Delay(10);
            }

            Console.WriteLine("End");
        }

    }

}


#endif