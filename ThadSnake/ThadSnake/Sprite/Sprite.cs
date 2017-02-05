using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThadSnake.Sprite
{
    public abstract class Sprite
    {
        public Texture2D Texture { get; }

        public Vector2 Position { get; protected set; }

        public Viewport Viewport { get; protected set; }

        public Vector2 Origin { get; private set; }

        public Direction Direction { get; set; } = Direction.Up;

        protected Sprite(Texture2D image, Point startingPoint, Viewport viewport)
        {
            Texture = image;
            Viewport = viewport;
            Position = new Vector2(startingPoint.X, startingPoint.Y);
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        public bool CheckColision(Sprite sprite)
        {
            return GetHitbox().Intersects(sprite.GetHitbox());
        }

        public bool CheckColision(Rectangle rectangle)
        {
            return GetHitbox().Intersects(rectangle);
        }

        private float GetRadiansForDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return 0;
                case Direction.Right:
                    return (float)(Math.PI / 2);
                case Direction.Down:
                    return (float)(Math.PI);
                case Direction.Left:
                    return (float)(3 * Math.PI / 2);
                default:
                    return 0;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position + Origin, null, Color.White, GetRadiansForDirection(Direction), Origin, 1, SpriteEffects.None, 0);
            //spriteBatch.Draw(Texture, Position, null, Color.White, GetRadiansForDirection(Direction), new Vector2(Hitbox.Width / 2, Hitbox.Height / 2), SpriteEffects.None, 0);
        }

    }
}
