/******************************************
 * 04/06/2012
 * 
 * Br8kIn
 *      
 * By : 
 * 
 *  Particle.cs 
 * 
 *****************************************/
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Br8kIn_
{
    class Particle
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Angle { get; set; }
        public float AngularVelocity { get; set; }
        public Color Color { get; set; }
        public float Size { get; set; }
        public int TTL { get; set; }

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity,
            float angle, float angularVelocity, Color color, float size, int ttl)
        {
            Texture = texture;
            Position = position;
            Velocity = velocity;
            Angle = angle;
            AngularVelocity = angularVelocity;
            Color = color;
            Size = size;
            TTL = ttl;
        }

        public void Update()
        {
            TTL--;
            Position += Velocity;
            Angle += AngularVelocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            spriteBatch.Draw(Texture, Position, sourceRectangle, Color,
                Angle, origin, Size, SpriteEffects.None, 0.29f);
        }
        public void Draw(SpriteBatch spriteBatch, float lay)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            spriteBatch.Draw(Texture, Position, sourceRectangle, Color,
                Angle, origin, Size, SpriteEffects.None, lay);
        }
        public void Draw(SpriteBatch spriteBatch, float lay, bool x)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            if (x)
            {
                spriteBatch.Draw(Texture, Position, sourceRectangle, Color,
                    Angle, origin, Size, SpriteEffects.None, lay);
            }
            else
            {
                spriteBatch.Draw(Texture, Position, sourceRectangle, Color,
                    Angle, origin, Size, SpriteEffects.None, .29f);
            }
        }
    }
}
