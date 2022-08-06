using System;
using System.Drawing;

namespace Layered.Basic{

    //  for a obj that wished to be able to be attached to the camera
    public interface ICameraAttachable
    {
        Point cameraPosition {get;}
    }


    public static class Camera
    {
        public static Point position = new Point();
        public static int zoomLevel = 0;

        public static bool attached         {get {return attached_obj != null;} }
        public static bool in_transition    {get; private set;}

        public static bool attachedToKeys   {get; private set;}


        private static ICameraAttachable? attached_obj = null;


        // //  transition realated variables
        // private static Point old_position = new Point();
        // private static Point attachedobj_old_position = new Point();

        // private static int transition_farmes;
        // private static int transition_frames_spent;

        // private static double transition_x;
        // private static double transition_y;
        // private static double transition_distance;

        private static string[] keyNames = new string[4];

        public static void AttachToKeys(string up = "__up__", string down = "__down__", string right = "__right__", string left = "__left__")
        {
            keyNames = new string[4] {up, down, right, left};
            Camera.UnAttach();
            Camera.attachedToKeys = true;
        }

        //  Lock the camera in place
        public static void UnAttach()
        {
            attached_obj = null;
            in_transition = false;
        }

        //  Attach the camera to a obj
        public static void Attach(ICameraAttachable obj)
        {
            attached_obj = obj;
            in_transition = false;
        }

        // //  Transition to new Attached object
        // public static void Transition(ICameraAttachable obj, int frames = 120)
        // {
        //     if (frames < 0)
        //     {
        //         Attach(obj);
        //         return;
        //     }
            
        //     Update();                   //  may remove this

        //     in_transition = true;
        //     transition_farmes = frames;
        //     transition_frames_spent = 0;
        //     transition_x = position.X;
        //     transition_y = position.Y;
        //     transition_distance = Misc.Distance(position, obj.camera_position);

        //     old_position = obj.camera_position;
        //     //old_position = obj.camera_position;
        //     attached_obj = obj;

        // }

        public static void Update()
        {
            //if (attached_obj == null) return;
            // if (in_transition)
            // {
            //     double percent_time_left = (Convert.ToDouble(transition_frames_spent) / Convert.ToDouble(transition_farmes));
            //     double move_percentage = Math.Asin(percent_time_left);

            //     int diff_x = attached_obj.camera_position.X - old_position.X;
            //     int diff_y = attached_obj.camera_position.Y - old_position.Y;

            //     transition_frames_spent++;
            //     return;
            // }
            if (attached_obj != null)
                position = attached_obj.cameraPosition;
            else if (Camera.attachedToKeys)
            {
                if (UserInput.GetKeyboardKey(keyNames[0]).pressed)  //  upp
                    Camera.position.Y++;
                if (UserInput.GetKeyboardKey(keyNames[1]).pressed)  //  down
                    Camera.position.Y--;            
                if (UserInput.GetKeyboardKey(keyNames[2]).pressed)  //  right
                    Camera.position.X--;
                if (UserInput.GetKeyboardKey(keyNames[3]).pressed)  //  left
                    Camera.position.X++;
            }
        }

    


        //  add camera effect into type
            public static Point ModifyPoint(Point pos)
            {
                pos.Offset(-Camera.position.X, -Camera.position.Y);
                return pos;
            }

            public static Rectangle ModifyRect(Rectangle rect)
            {
                rect.Location = ModifyPoint(rect.Location);
                return rect;
            }



    }
}