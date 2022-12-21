using System.Drawing;

using Layered.Basic;

namespace Layered.UIObject{

    public interface IUIArea
    {
        public Rectangle bounds{get; set;}
        public uint uiStatusEnum{set;}
    }
}