using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThadSnake.Sprite
{
    public class SnakeHead : SnakeSprite
    {
        
        public SnakeHead(Texture2D texture, Point startingPoint, Viewport viewport) : base (texture, startingPoint, viewport)
        {
            
        }

        public override void Update()
        {

            if (Viewport.Width == Position.X + Texture.Width)
            {
                // We are against right wall
                // If being told to go right, randomly select either up or down
                if (Direction == Direction.Right)
                {
                    int rand = random.Next(0, 2);
                    if (rand == 0)
                    {
                        Direction = Direction.Up;
                    }
                    else
                    {
                        Direction = Direction.Down;
                    }
                }
            }
            else if (Position.X == 0)
            {
                // We are against left wall
                // If being told to go left, randomly select either up or down
                if (Direction == Direction.Left)
                {
                    int rand = random.Next(0, 2);
                    if (rand == 0)
                    {
                        Direction = Direction.Up;
                    }
                    else
                    {
                        Direction = Direction.Down;
                    }
                }
            }

            else if (Viewport.Height == Position.Y + Texture.Height)
            {
                // We are against bottom wall
                // If being told to go down, randomly select either left or right
                if (Direction == Direction.Down)
                {
                    int rand = random.Next(0, 2);
                    if (rand == 0)
                    {
                        Direction = Direction.Right;
                    }
                    else
                    {
                        Direction = Direction.Left;
                    }
                }
            }
            else if (Position.Y == 0)
            {
                // We are against top wall
                // If being told to go right, randomly select either right or left
                if (Direction == Direction.Up)
                {
                    int rand = random.Next(0, 2);
                    if (rand == 0)
                    {
                        Direction = Direction.Left;
                    }
                    else
                    {
                        Direction = Direction.Right;
                    }
                }
            }
            Vector2 newPos = new Vector2();

            // Move our sprite
            switch (Direction)
            {
                case Direction.Up:
                    newPos = new Vector2(Position.X, Position.Y - Texture.Height);
                    // If new rect would go past upper wall, force it into wall
                    if (newPos.Y < 0)
                    {
                        newPos = new Vector2(Position.X, 0);
                    }
                    break;
                case Direction.Right:
                    newPos = new Vector2(Position.X + Texture.Width, Position.Y);
                    if (newPos.X + Texture.Width > Viewport.Width)
                    {
                        newPos = new Vector2(Viewport.Width - Texture.Width, Position.Y);
                    }
                    break;
                case Direction.Down:
                    newPos = new Vector2(Position.X, Position.Y + Texture.Height);
                    // If new rect would go past upper wall, force it into wall
                    if (newPos.Y + Texture.Height > Viewport.Height)
                    {
                        newPos = new Vector2(Position.X, Viewport.Height - Texture.Height);
                    }
                    break;
                case Direction.Left:
                    newPos = new Vector2(Position.X - Texture.Width, Position.Y);
                    if (newPos.X < 0)
                    {
                        newPos = new Vector2(0, Position.Y);
                    }
                    break;
            }

            Position = newPos;



            // Update first
            //base.Update();
        }

        /*
        public Direction MoveAndCheckForWall(Direction direction, int distanceToMove)
        {
            bool changedDirection = false;

            if (Viewport.Width == Hitbox.X + Hitbox.Width)
            {
                // We are against right wall
                // If being told to go right, randomly select either up or down
                if (direction == Direction.Right)
                {
                    int rand = random.Next(0, 2);
                    if (rand == 0)
                    {
                        direction = Direction.Up;
                    }
                    else
                    {
                        direction = Direction.Down;
                    }
                    changedDirection = true;
                }
            }
            else if(Hitbox.X == 0)
            {
                // We are against left wall
                // If being told to go left, randomly select either up or down
                if (direction == Direction.Left)
                {
                    int rand = random.Next(0, 2);
                    if (rand == 0)
                    {
                        direction = Direction.Up;
                    }
                    else
                    {
                        direction = Direction.Down;
                    }
                    changedDirection = true;
                }
            }

            else if(Viewport.Height == Hitbox.Y + Hitbox.Height)
            {
                // We are against bottom wall
                // If being told to go down, randomly select either left or right
                if (direction == Direction.Down)
                {
                    int rand = random.Next(0, 2);
                    if (rand == 0)
                    {
                        direction = Direction.Right;
                    }
                    else
                    {
                        direction = Direction.Left;
                    }
                    changedDirection = true;
                }
            }
            else if(Hitbox.Y == 0)
            {
                // We are against top wall
                // If being told to go right, randomly select either right or left
                if (direction == Direction.Up)
                {
                    int rand = random.Next(0, 2);
                    if (rand == 0)
                    {
                        direction = Direction.Left;
                    }
                    else
                    {
                        direction = Direction.Right;
                    }
                    changedDirection = true;
                }
            }
            Rectangle newRect = new Rectangle();

            // Move our sprite
            switch (direction)
            {
                case Direction.Up:
                    newRect = new Rectangle(Hitbox.X, Hitbox.Y - distanceToMove, Hitbox.Width, Hitbox.Height);
                    // If new rect would go past upper wall, force it into wall
                    if (newRect.Y < 0)
                    {
                        newRect = new Rectangle(Hitbox.X, 0, Hitbox.Width, Hitbox.Height);
                    }
                    break;
                case Direction.Right:
                    newRect = new Rectangle(Hitbox.X + distanceToMove, Hitbox.Y, Hitbox.Width, Hitbox.Height);
                    if (newRect.X + newRect.Width > Viewport.Width)
                    {
                        newRect = new Rectangle(Viewport.Width - Hitbox.Width, Hitbox.Y, Hitbox.Width, Hitbox.Height);
                    }
                    break;
                case Direction.Down:
                    newRect = new Rectangle(Hitbox.X, Hitbox.Y + distanceToMove, Hitbox.Width, Hitbox.Height);
                    // If new rect would go past upper wall, force it into wall
                    if (newRect.Y + newRect.Height > Viewport.Height)
                    {
                        newRect = new Rectangle(Hitbox.X, Viewport.Height - Hitbox.Height, Hitbox.Width, Hitbox.Height);
                    }
                    break;
                case Direction.Left:
                    newRect = new Rectangle(Hitbox.X - distanceToMove, Hitbox.Y, Hitbox.Width, Hitbox.Height);
                    if (newRect.X < 0)
                    {
                        newRect = new Rectangle(0, Hitbox.Y, Hitbox.Width, Hitbox.Height);
                    }
                    break;
            }

            Hitbox = newRect;
            Position = new Vector2(Hitbox.X, Hitbox.Y);

            this.Direction = direction;

            return direction;
        }
        */

        

    }
}
