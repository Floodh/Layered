

//      note:   The Visual.cs and UserInput.cs are powered by SDL2
//              You could replace those classes with verisions that
//              are powered by something else and everything will
//              will still work


namespace Layered
{
    //  Currently in the Visual(SDL2) folder
    namespace Internal
    {
        //  Internal methods that are primarily used by DrawObjects and UiObjects
        //  This noteably includes the Visual class and its static functions (in the Visual folder)
        //      Sidenote:   The Visual class is powered by SDL2 but could be powered by anything
        //                  UserInput.cs is also powered by SDL2 tho, but it is not in Internal



    }

    namespace DrawObject
    {
        //  Custom made draw objects that can be reused across diffrent projects
        //  All draw objects makes use of the methods in the Visual class

    }

    namespace UIObject
    {
        //  code for User input related feutures
        //  does not include the camera
    }

    namespace Basic
    {
        //  Contains the most essential methods and the methods 
    }
    
}