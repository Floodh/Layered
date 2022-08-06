namespace Layered.UIObject{



    public interface IUIObject
    {
        int Z {get;}

        //  Draw function
        UIInternalData Check(UIInternalData data);

        //  remove all associated DrawObjects connected to this object from the hidden layer
        void Delete();   

    }



}