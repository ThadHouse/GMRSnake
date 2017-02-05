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
using CoolerMasterSharp.Native;

namespace ThadSnake
{


    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameLinkedList : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D snakeHeadTexture;
        Texture2D snakeBodyTexture;
        Texture2D snakeTailTexture;
        Texture2D pelletTexture;

        //SnakeHead snakeHead;

        Pellet pellet;

        //List<SnakeSprite> snakeList;

        LinkedList<SnakeSprite> snakeList;

        public GameLinkedList()
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
            snakeList = new LinkedList<SnakeSprite>();

            Random random = new Random();


            NativeMethods.SetControlDevice(CoolerMasterSharp.DeviceIndex.MasterKeysLarge);
            NativeMethods.EnableLedControl(true);
            NativeMethods.RefreshLeds(true);
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

            snakeHeadTexture = Content.Load<Texture2D>("SnakeBody");
            snakeBodyTexture = Content.Load<Texture2D>("SnakeBody");
            snakeTailTexture = Content.Load<Texture2D>("SnakeTail");
            pelletTexture = Content.Load<Texture2D>("Pellet");

            Random random = new Random();
            Direction direction = (Direction)random.Next(0, 4);


            // Create our snake head
            SnakeBody snakeHead = new SnakeBody(snakeHeadTexture, null, GraphicsDevice.Viewport);
            snakeHead.RandomizeSprite(direction);

            // Add it to list
            snakeList.AddFirst(snakeHead);

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


            Direction? direction = null;//this.direction;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                direction = Direction.Up;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                direction = Direction.Right;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                direction = Direction.Left;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                direction = Direction.Down;
            }

            //this.direction = snakeHead.MoveAndCheckForWall(direction, 8);
            // Update head direction first
            /*
            if (direction != null)
            {
                snakeList[0].Direction = direction.Value;
            }
            */

            foreach (var snake in snakeList)
            {
                //snake.Update();
            }



            // Update all but first piec
            /*
            for (int i = snakeList.Count - 1; i > 0; i--)
            {
                snakeList[i].Direction = snakeList[i - 1].Direction;
            }
            */

            // Remove back piece
            SnakeSprite last = snakeList.Last.Value;
            snakeList.RemoveLast();
            // Move to front
            if (direction != null)
            {
                last.Direction = direction.Value;
            }
            else if (snakeList.Count == 0)
            {
                // Don't change direction at all
            }
            else
            {
                last.Direction = snakeList.First.Value.Direction;
            }

            if (snakeList.Count != 0)
            {
                last.PlaceAtHead(snakeList.First.Value);
            }
            else
            {
                last.Update();
            }


            snakeList.AddFirst(last);

            if (snakeList.First.Value.CheckColision(pellet))
            {
                // Add a new snake body. Relocate pallet
                snakeList.AddLast(new SnakeBody(snakeBodyTexture, snakeList.Last.Value, GraphicsDevice.Viewport));
                pellet.RandomizeLocation(snakeList);
            }

            // Check for head colision
            LinkedListNode<SnakeSprite> next = snakeList.First.Next;
            while(next != null)
            {
                if (snakeList.First.Value.CheckColision(next.Value))
                {
                    // COLLISION

                    snakeList.Clear();
                    // Create our snake head
                    SnakeBody snakeHead = new SnakeBody(snakeHeadTexture, null, GraphicsDevice.Viewport);
                    snakeHead.RandomizeSprite((Direction)new Random().Next(0, 4));

                    // Add it to list
                    snakeList.AddFirst(snakeHead);

                    //Create our pellet
                    pellet = new Pellet(pelletTexture, new Point(0, 0), GraphicsDevice.Viewport);

                    pellet.RandomizeLocation(snakeList);
                } 
                next = next.Next;
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
            if (NativeMethods.IsDevicePluggedIn())
                pellet.Draw(spriteBatch);

            foreach (var sprite in snakeList)
            {
                sprite.Draw(spriteBatch);
            }

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
