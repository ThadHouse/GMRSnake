using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThadSnake.Sprite
{
    public class SnakeBody : SnakeSprite
    {
        public SnakeBody(Texture2D texture, SnakeSprite previousSnake, Viewport viewport) : base (texture, new Point(0,0), viewport)
        {
            if (previousSnake == null) return;

            // Make sure snake gets put in the right point.
            this.Direction = previousSnake.Direction;
            switch (previousSnake.Direction)
            {
                case Direction.Down:
                    // Put new piece above old piece
                    this.Position = new Vector2(previousSnake.Position.X, previousSnake.Position.Y - texture.Height);
                    break;
                case Direction.Up:
                    this.Position = new Vector2(previousSnake.Position.X, previousSnake.Position.Y + previousSnake.Texture.Height);
                    break;
                case Direction.Left:
                    this.Position = new Vector2(previousSnake.Position.X + previousSnake.Texture.Width, previousSnake.Position.Y);
                    break;
                case Direction.Right:
                    this.Position = new Vector2(previousSnake.Position.X - texture.Width, previousSnake.Position.Y);
                    break;
            }
        }

       

    }
}
