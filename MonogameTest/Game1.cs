using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MonogameTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public const int Width = 15;
        public const int Height = 16;
        public const int Bombs = 30;
        public const int NumberOfSquares = Width*Height;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public List<Square> Squares = new List<Square>(NumberOfSquares);
        Face face;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true; //set mouse to visible
            graphics.PreferredBackBufferHeight = 40 + Height * 32;
            graphics.PreferredBackBufferWidth = 32 + Height * 32;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Square.Tex = Content.Load<Texture2D>("MineSweeper");
            Face.Tex = Content.Load<Texture2D>("SmileyFace");
            face = new Face(Window.ClientBounds.Width/2 - 16, 0);
            Reset();
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            /* if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                 Exit();*/

            InputManager.Update();

            //win
            if (Square.Flags == 0 || Square.Cleared == 0)
            {
                Face.SetFace(2);
                if (InputManager.hasClicked()) Reset();
            }
            else
            //lose
            if (Square.Dead)
            {
                if (InputManager.hasClicked()) Reset();
                Face.SetFace(3);
            }
            else
            if (InputManager.isDown())
                Face.SetFace(1);
            else
                Face.SetFace(0);

            foreach (var item in Squares)
            {
                item.Update();
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            face.Draw(spriteBatch);
            foreach (var item in Squares)
            {
                item.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void Reset()
        {
            InputManager.ms = new MouseState();
            InputManager.oms = new MouseState();

            Squares.Clear();
            for (int i = 0; i < NumberOfSquares; i++)
            {
                Squares.Add(new Square(i));
            }
            Random rnd = new Random();
            int numBombs = 10;
            Square.Flags = numBombs;
            Square.Cleared = Squares.Count - numBombs;
            Square.Dead = false;
            while (numBombs > 0)
            {
                int sq = rnd.Next(NumberOfSquares-2) + 1;
                if (Squares[sq].HiddenValue == 2) continue; //if you want to sit a bomb where is a bomb it wouldn't work

                Squares[sq].HiddenValue = 2;
                numBombs--;
            }
            foreach (var item in Squares)
            {
                item.GetNeighbors(Squares); //get neighbors and count
            }
        }
    }
}
