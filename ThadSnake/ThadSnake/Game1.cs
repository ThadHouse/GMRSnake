using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ThadSnake.Sprite;

namespace ThadSnake
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D snakeHeadTexture;
        Texture2D snakeBodyTexture;
        Texture2D snakeTailTexture;
        Texture2D pelletTexture;

        SnakeHead snakeHead;

        Pellet pellet;

        List<SnakeSprite> snakeList;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            snakeList = new List<SnakeSprite>();

            Random random = new Random();



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

            snakeHeadTexture = Content.Load<Texture2D>("SnakeHead");
            snakeBodyTexture = Content.Load<Texture2D>("SnakeBody");
            snakeTailTexture = Content.Load<Texture2D>("SnakeTail");
            pelletTexture = Content.Load<Texture2D>("Pellet");

            Random random = new Random();
            Direction direction = (Direction)random.Next(0, 4);


            // Create our snake head
            snakeHead = new SnakeHead(snakeHeadTexture, new Point(0, 0), GraphicsDevice.Viewport);
            snakeHead.RandomizeSprite(direction);

            // Add it to list
            snakeList.Add(snakeHead);

            //Create our pellet
            pellet = new Pellet(pelletTexture, new Point(0, 0), GraphicsDevice.Viewport);

            pellet.RandomizeLocation(snakeList);



            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        //Direction direction;

        int frameCount = 0;

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (frameCount < 2)
            {
                frameCount++;
                return;
            }
            else
            {
                frameCount = 0;
            }

            SnakeSprite first = snakeList[0];
            Direction? direction = null;//this.direction;
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && first.Direction != Direction.Down)
            {
                direction = Direction.Up;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && first.Direction != Direction.Left)
            {
                direction = Direction.Right;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && first.Direction != Direction.Right)
            {
                direction = Direction.Left;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) && first.Direction != Direction.Up)
            {
                direction = Direction.Down;
            }

            //this.direction = snakeHead.MoveAndCheckForWall(direction, 8);
            // Update head direction first
            if (direction != null)
            {
                snakeList[0].Direction = direction.Value;
            }



            // Update all pieces
            foreach(var snake in snakeList)
            {
                snake.Update();
            }

            for (int i = snakeList.Count - 1; i > 0; i--)
            {
                snakeList[i].Direction = snakeList[i - 1].Direction;
            }

            if (snakeHead.CheckColision(pellet))
            {
                // Add a new snake body. Relocate pallet
                snakeList.Add(new SnakeBody(snakeBodyTexture, snakeList[snakeList.Count - 1], GraphicsDevice.Viewport));
                pellet.RandomizeLocation(snakeList);
            }

            // Check for head colision
            for (int i = 1; i < snakeList.Count; i++)
            {
                if (snakeList[0].CheckColision(snakeList[i]))
                {
                    // COLLISION
                    // Reset game
                    snakeList.Clear();
                    // Create our snake head
                    snakeHead = new SnakeHead(snakeHeadTexture, new Point(0, 0), GraphicsDevice.Viewport);
                    snakeHead.RandomizeSprite((Direction)new Random().Next(0, 4));

                    // Add it to list
                    snakeList.Add(snakeHead);

                    //Create our pellet
                    pellet = new Pellet(pelletTexture, new Point(0, 0), GraphicsDevice.Viewport);

                    pellet.RandomizeLocation(snakeList);
                }
            }
            
            // TODO: Add your update logic here

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

            pellet.Draw(spriteBatch);

            foreach(var sprite in snakeList)
            {
                sprite.Draw(spriteBatch);
            }

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
