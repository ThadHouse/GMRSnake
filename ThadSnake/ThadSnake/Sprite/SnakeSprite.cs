using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThadSnake.Sprite
{
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    public abstract class SnakeSprite : Sprite
    {
        protected Random random;
        public SnakeSprite(Texture2D texture, Point startingPoint, Viewport viewport) : base (texture, startingPoint, viewport)
        {
            random = new Random();
        }

        public void RandomizeSprite(Direction direction)
        {
            // Used to randomize the head's location and direction
            Direction = direction;
            int xPos = 0;
            int yPos = 0;
            while (true)
            {
                xPos = random.Next(0, Viewport.Width - Texture.Width + 1);
                if (xPos % Texture.Width == 0) break;
            }

            while (true)
            {
                yPos = random.Next(0, Viewport.Height - Texture.Height + 1);
                if (yPos % Texture.Height == 0) break;
            }
            //Hitbox = new Rectangle(random.Next(0, Viewport.Width - Hitbox.Width + 1), random.Next(0, Viewport.Height - Hitbox.Height + 1), Hitbox.Width, Hitbox.Height);
            Position = new Vector2(xPos, yPos);
        }

        public virtual void Update()
        {
            switch (Direction)
            {
                case Direction.Down:
                    this.Position = new Vector2(this.Position.X, this.Position.Y + this.Texture.Height);
                    break;
                case Direction.Up:
                    this.Position = new Vector2(this.Position.X, this.Position.Y - this.Texture.Height);
                    break;
                case Direction.Left:
                    this.Position = new Vector2(this.Position.X - this.Texture.Width, this.Position.Y);
                    break;
                case Direction.Right:
                    this.Position = new Vector2(this.Position.X + this.Texture.Width, this.Position.Y);
                    break;
            }
        }

        public void PlaceAtHead(SnakeSprite currentHead)
        {
            //SnakeSprite first 
            switch (Direction)
            {
                case Direction.Up:
                    // Put new piece above old piece
                    this.Position = new Vector2(currentHead.Position.X, currentHead.Position.Y - Texture.Height);
                    break;
                case Direction.Down:
                    this.Position = new Vector2(currentHead.Position.X, currentHead.Position.Y + currentHead.Texture.Height);
                    break;
                case Direction.Right:
                    this.Position = new Vector2(currentHead.Position.X + currentHead.Texture.Width, currentHead.Position.Y);
                    break;
                case Direction.Left:
                    this.Position = new Vector2(currentHead.Position.X - Texture.Width, currentHead.Position.Y);
                    break;
            }
        }

    }
}
