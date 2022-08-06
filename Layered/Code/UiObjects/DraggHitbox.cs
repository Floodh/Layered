using System.Drawing;

using Layered.Basic;

namespace Layered.UIObject{


    //  When placed in LayerWeb
    //  Will check UserInput and update variables accordingly.
    //  Will also remeber
    public class DraggHitbox : UIObject
    {

        
        


        public              bool                        dragging    {get; private set;}

        private readonly    Rectangle                   bounds;
        public              Point                       startPoint;
        public              Point                       dragPoint;
        public              Point                       endPoint;
        public              Queue<Tuple<Point, Point>>  draggedEvents;
        

        public DraggHitbox(int z, Rectangle bounds)
        {
            this.Z              =   z;
            this.bounds         =   bounds;
            this.dragging       =   false;
            this.startPoint     =   Point.Empty;
            this.dragPoint      =   Point.Empty;
            this.endPoint       =   Point.Empty;
            //  from start to end
            this.draggedEvents  =   new Queue<Tuple<Point, Point>>(8);
        }

        //  returns if the mouse should be considered blocked
        public override UIInternalData Check(UIInternalData data)
        {

            //  press and drag logic (within bounds)
            Point mousePos = UserInput.Mouse.position;
            if (this.bounds.Contains(UserInput.Mouse.left.down) && !data.mouseBlocked && !data.mouseDownBlocked && UserInput.Mouse.left.pressed && !dragging)
            {
                this.startPoint = UserInput.Mouse.left.down;
                dragging = true;
            }
            if (dragging && !data.mouseBlocked)
            {
                this.dragPoint = mousePos;
                if (UserInput.Mouse.left.pressed == false)
                {
                    dragging = false;
                    this.endPoint = UserInput.Mouse.left.up;
                    this.startPoint = UserInput.Mouse.left.down;
                    draggedEvents.Enqueue(new Tuple<Point, Point>(this.startPoint, this.endPoint));
                }
                
            }

            return data;
        }
        public override void Delete()
        {

        }

    }

}