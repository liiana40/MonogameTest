using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using System.Threading.Tasks;

namespace MonogameTest
{
    public class Square
    {
        public static Texture2D Tex; //Texture that we are going to draw

        Rectangle SourceRect = new Rectangle(0, 0, 128, 128); //Source rectangle, or what exactly is being drawn
        public Rectangle Position = new Rectangle(0, 0, 32, 32); //Position and size
                                                                 //  public int Value = 0; the drawing value
        public int HiddenValue = 3;
        public int DisplayValue = 0; //if rightclick, DisplayValue = 0
        private int CheckEmpty = 0; //can not acces it fron the outside of this class
        int i; //index
        const int SIZE = Game1.Width; //our grid

        public static int Flags;
        public static int Cleared;
        public static bool Dead = false;

        List<Square> Neighbors = new List<Square>(8); //maintain a list of neighbors, or we would have had 600 pointers

        public Square(int i) //constructor
        {
            this.i = i; //pass index
            Position.X = 16 + i % SIZE * Position.Width; //grid formula
            Position.Y = 32 + i / SIZE * Position.Height;
        }

        public void GetNeighbors(List<Square> Squares)
        {
            bool isLeft = i % SIZE == 0;
            bool isTop = i / SIZE == 0;
            bool isBottom = i / SIZE == Game1.Height - 1;
            bool isRight = i % SIZE == SIZE - 1;
            if (!isLeft) Neighbors.Add(Squares[i - 1]); //since it's a list , we use the array notaion []
            if (!isRight) Neighbors.Add(Squares[i + 1]);
            if (!isTop) Neighbors.Add(Squares[i - SIZE]);
            if (!isBottom) Neighbors.Add(Squares[i + SIZE]);

            if (!isTop && !isLeft) Neighbors.Add(Squares[i - 1 - SIZE]); //test those permutations for the 8 neighbors around
            if (!isTop && !isRight) Neighbors.Add(Squares[i + 1 - SIZE]);
            if (!isRight && !isBottom) Neighbors.Add(Squares[i + 1 + SIZE]);
            if (!isLeft && !isBottom) Neighbors.Add(Squares[i - 1 + SIZE]);

            //count bombs
            if (HiddenValue == 3) //if we don't have a bomb, then go to the neighbor
                foreach (var item in Neighbors)
                {

                    if (item.HiddenValue == 2)
                    {
                        HiddenValue++; //Count them
                    }

                }
        }




        public void Update()
        {
            //alreadyt cleared
            if (CheckEmpty > 1 || Dead) return;

            if (InputManager.isDown(Position))
            {

                SourceRect.X = 768;
                SourceRect.Y = 768;

            }
            else
            {
                SourceRect.X = DisplayValue % 4 * 128;
                SourceRect.Y = DisplayValue / 4 * 128;
            }
            if (CheckEmpty == 1 || (DisplayValue == 0 && InputManager.hasClicked(Position)))
            {
                // Value = HidenValue;
                SourceRect.X = HiddenValue % 4 * 128; //expoze the one we are standing on so we don't click on it again
                SourceRect.Y = HiddenValue / 4 * 128;
                if (DisplayValue != HiddenValue)
                    Cleared--;
                DisplayValue = HiddenValue;

                if (HiddenValue == 2)
                {
                    Dead = true;
                    Face.SetFace(3);
                }
                CheckEmpty = 3; //so we never hit it again


                if (HiddenValue == 3)
                    foreach (var item in Neighbors) //expose neighbors
                    {
                        //  item.HidenValue = item.HidenValue;
                        item.SourceRect.X = item.HiddenValue % 4 * 128;
                        item.SourceRect.Y = item.HiddenValue / 4 * 128;

                        if (item.DisplayValue != item.HiddenValue)
                        {
                            Cleared--;
                        }
                        item.DisplayValue = item.HiddenValue;

                        if (item.HiddenValue == 3)
                            item.CheckEmpty = 1;
                        else
                            item.CheckEmpty = 3;
                    }
            }
            if (InputManager.hasClickedRight(Position))
                if (DisplayValue == 0)
                    DisplayValue = 1;
                else
                if (DisplayValue == 1)
                    DisplayValue = 0;
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Tex, Position, SourceRect, Color.White);
        }
    }
}

