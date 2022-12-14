using System;
using System.Drawing;

using Layered.DrawObject;
using Layered.UIObject;


namespace Layered.Basic{

    public static class LayeredLayout
    {
        private static List<IDrawObject>[]  drawLayers = new List<IDrawObject>[Settings.drawLayerDepth];
        private static List<IUIObject>[]    uiLayers = new List<IUIObject>[Settings.uiLayerDepth];

        static LayeredLayout()
        {
            Init();
        }

        public static void Init()
        {
            for (int i = 0; i < drawLayers.Length; i++)
            {
                drawLayers[i] = new List<IDrawObject>();
            }
            for (int i = 0; i < uiLayers.Length; i++)
            {
                uiLayers[i] = new List<IUIObject>();
            }
        }


        public static void DrawAll()
        {
            foreach (List<IDrawObject> layer in drawLayers)
            {
                foreach (IDrawObject drawObject in layer)
                {
                    drawObject.Draw();
                }

            }
        }


        public static void CheckAll()
        {
            UIInternalData data = new UIInternalData();
            for (int i = drawLayers.Length - 1; i > -1; i--)
            {
                List<IUIObject> layer = uiLayers[i];
                foreach (IUIObject uiObject in layer)
                {
                    //  normaly the data will remain unchanged
                    data = uiObject.Check(data);
                }

            }            
        }


        public static void Add(IDrawObject drawObject)
        {
            if (drawObject.Z >= drawLayers.Length || drawObject.Z < 0)
                throw new InvalidOperationException($"Z = {drawObject.Z} value exceds draw layer depth or is negative");
            drawLayers[drawObject.Z].Add(drawObject);
        }
        public static void Add(IUIObject uiObject)
        {
            if (uiObject.Z >= uiLayers.Length)
                throw new InvalidOperationException($"Z = {uiObject.Z} value exceds ui layer depth or is negative");
            uiLayers[uiObject.Z].Add(uiObject);
        }


        public static bool Remove(IDrawObject drawObject)
        {
            if (drawObject.Z >= drawLayers.Length)
                throw new InvalidOperationException($"Z = {drawObject.Z} value exceds draw layer depth or is negative");            
            return drawLayers[drawObject.Z].Remove(drawObject);
        }
        public static bool Remove(IUIObject uiObject)
        {
            if (uiObject.Z >= uiLayers.Length)
                throw new InvalidOperationException($"Z = {uiObject.Z} value exceds ui layer depth or is negative");            
            return uiLayers[uiObject.Z].Remove(uiObject);
        }
        
    }

}