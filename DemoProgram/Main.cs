#if Demo



using   Layered.Basic;
using   Layered.Internal;
using   Layered.DrawObject;

using   System;
using   System.Drawing;


namespace DieKurve{

    class MainClass
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");


            //  true by defualt
            Settings.autoAddDrawObjects = true;

            //
            Visual.Setup(false, 1);
            UserInput.LoadBindings();       //  by defualt loads a premade file at Layered/Settings/Controls.txt


            //  test draw object
            SimpleTexture texture = new SimpleTexture(0, new Rectangle(600,400,100,100), new Rectangle(0,0,16,16), "Grass.png");


            while ( UserInput.quit == false)
            {
                Time.Update();
                UserInput.UpdateAll();

                if (UserInput.GetKeyboardKey("__right__").pressed)
                    texture.X++;
                if (UserInput.GetKeyboardKey("__left__").pressed)
                    texture.X--;
                if (UserInput.GetKeyboardKey("__up__").pressed)
                    texture.Y--;
                if (UserInput.GetKeyboardKey("__down__").pressed)
                    texture.Y++;

                Visual.ClearScreen();
                LayeredLayout.DrawAll();    //  the draw object is drawn here
                Visual.ShowFrame();

                SDL2.SDL.SDL_Delay(10);
            }

            Console.WriteLine("End");
        }

    }

}


#endif