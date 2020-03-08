using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameTest
{
    public static class InputManager
    {
        public static MouseState ms = new MouseState();
        public static MouseState oms;
        public static bool Cheat = false;

        public static void Update()
        {
            oms = ms;
            ms = Mouse.GetState();
            Cheat = (ms.MiddleButton == ButtonState.Pressed); //press and hold left button
        }

        public static bool isDown(Rectangle r) //is the button pressed
        {
            return ms.LeftButton == ButtonState.Pressed &&
                r.Contains(ms.X, ms.Y); //are we in the middle of that rectangle
        }
        public static bool isDown() //is the button pressed
        {
            return ms.LeftButton == ButtonState.Pressed;
        }
        public static bool hasClicked(Rectangle r)
        {
            return ms.LeftButton == ButtonState.Released &&
                oms.LeftButton == ButtonState.Pressed &&
                r.Contains(ms.X, ms.Y);
        }
        public static bool hasClicked()
        {
            return ms.LeftButton == ButtonState.Released &&
                oms.LeftButton == ButtonState.Pressed;
        }
        public static bool hasClickedRight(Rectangle r)
        {
            return ms.RightButton == ButtonState.Released &&
                oms.RightButton == ButtonState.Pressed &&
                r.Contains(ms.X, ms.Y);
        }
    }

}
