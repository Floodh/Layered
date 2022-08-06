using System.Globalization;
using System;

namespace Layered.Basic{

    //  this time module only updates when Update() is called in order to sync everything that renders
    //  intended to be used for visual effects and not game machanics
    /// <summary>
    /// The time is updated every frame in the GraphicHandler
    /// </summary>
    public static class Time{

        private static long     start_time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        private static int      given_time = 0;
        public  static int      sec     { get   {return given_time / 1000;}    private set { given_time = value * 1000;}}
        public  static int      milisec { get   {return given_time;} private set { given_time = value;}}

        public static void Reset(){

            start_time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            given_time = 0;

        }

        public static void Update(){

            long current_time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            given_time = (int) (current_time - start_time);

        }

    }


}