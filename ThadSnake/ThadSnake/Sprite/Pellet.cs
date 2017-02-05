using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThadSnake.Sprite
{
    public class Pellet : Sprite
    {
        Random random;
        public Pellet(Texture2D texture, Point startingPoint, Viewport viewport)  : base (texture, startingPoint, viewport)
        {
            random = new Random();
        }

        public void RandomizeLocation(List<SnakeSprite> snakeSprites)
        {
            while (true)
            {
                Rectangle newPoint = new Rectangle(random.Next(0, Viewport.Width - Texture.Width + 1), random.Next(0, Viewport.Height - Texture.Height + 1), Texture.Width, Texture.Height);

                bool foundCollision = false;
                foreach(var sprite in snakeSprites)
                {
                    if (sprite.CheckColision(newPoint))
                    {
                        // Sprites collide, don't place
                        foundCollision = true;
                        // Exit early since we know this spot is not valid
                        break;
                    }
                }

                if (!foundCollision)
                {
                    // If valid point, move point and return
                    Position = new Vector2(newPoint.X, newPoint.Y);
                    return;
                }
                // If found a colision, continue
            }
        }
        public void RandomizeLocation(LinkedList<SnakeSprite> snakeSprites)
        {
            while (true)
            {
                Rectangle newPoint = new Rectangle(random.Next(0, Viewport.Width - Texture.Width + 1), random.Next(0, Viewport.Height - Texture.Height + 1), Texture.Width, Texture.Height);

                bool foundCollision = false;
                foreach (var sprite in snakeSprites)
                {
                    if (sprite.CheckColision(newPoint))
                    {
                        // Sprites collide, don't place
                        foundCollision = true;
                        // Exit early since we know this spot is not valid
                        break;
                    }
                }

                if (!foundCollision)
                {
                    // If valid point, move point and return
                    Position = new Vector2(newPoint.X, newPoint.Y);
                    return;
                }
                // If found a colision, continue
            }
        }
    }
}
