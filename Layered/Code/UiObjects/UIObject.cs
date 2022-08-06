using Layered.Basic;

namespace Layered.UIObject
{

    public abstract class UIObject : IUIObject
    {


        /// <summary>
        /// The Z value determines in which layer the UI object should check for input in LayeredLayout.
        /// If autoAssignUIObjects is set to true than the draw object will automatically be assigned
        //  and unassigned to LayeredLayout if the Z value > -1
        /// </summary>
        public int Z {
            get {return z;}
            set {

                if (Settings.autoRemoveUIObjects && z > -1)      
                    LayeredLayout.Remove(this);
                z = value;
                if (Settings.autoAddUIObjects && value > -1)
                    LayeredLayout.Add(this);
            }
        }
        private int z = -1;

        //  Draw function
        public abstract UIInternalData Check(UIInternalData data);

        //  ensure that there is no more leak, such as destroy textures
        public abstract void Delete();
    }

}