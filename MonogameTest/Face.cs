using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameTest
{
    public class Face
    {
        public static Texture2D Tex; //Texture that we are going to draw
        public Rectangle Position = new Rectangle(0, 0, 32, 32); //Position and size
        public static Rectangle SourceRect = new Rectangle(0, 0, 80, 80); //Source rectangle, or what exactly is being drawn
        
        public Face(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }
        public static void SetFace( int v)
        {
            SourceRect.X = v%2 * 80;
            SourceRect.Y = v / 2 * 80;
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Tex, Position, SourceRect, Color.White);
        }
    }
}
