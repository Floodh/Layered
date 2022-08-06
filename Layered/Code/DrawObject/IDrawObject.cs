
namespace Layered.DrawObject
{

    public interface IDrawObject
    {

        int Z {get;}

        //  Draw function
        void Draw();

        //  ensure that there is no more leak, such as destroy textures
        void Delete();
    }

}