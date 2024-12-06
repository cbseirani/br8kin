/******************************************
 * 04/10/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  PotParticles.cs 
 * 
 *****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Br8kIn_
{
    class PotParticles
    {
        private Random random;
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> particles;
        int ani;
        private List<Texture2D> textures;
        Color color;
        float num1, num2;


        public PotParticles()
        {
            textures = new List<Texture2D>();
            particles = new List<Particle>();
            random = new Random();
            ani = 3;
            num1 = num2 = 0;
        }

        public void Load(Texture2D a, Texture2D b, Texture2D c,
            Texture2D d, Texture2D e, Texture2D f)
        {
            textures.Add(a);
            textures.Add(b);
            textures.Add(c);
            textures.Add(d);
            textures.Add(e);
            textures.Add(f);
        }

        public void Update(bool x, GameTime gT)
        {
            ani--;

            if (x)
            {
                if (ani <= 0)
                {
                    for (int i = 0; i < 30; i++)
                    {
                        particles.Add(GenerateNewParticle(gT));
                    }
                    ani = 3;
                }
            }

            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if (particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        private Particle GenerateNewParticle(GameTime gT)
        {
            Texture2D texture = textures[((gT.ElapsedGameTime.Milliseconds + random.Next() + (int)EmitterLocation.Y) % textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                                    -1f * (float)(random.NextDouble() * 2 - num1),
                                    -1f * (float)(random.NextDouble() * 2 - num2));
            float angle = (float)(random.NextDouble() * 2 - 1);
            float angularVelocity = .2f * (float)(random.NextDouble() * 2 - 1);
            float size = (float)random.NextDouble() % 1.3f;
            int ttl = 70;
            color = Color.White;
            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
        }

        public void setPos(Vector2 pos)
        {
            EmitterLocation = pos;
        }
        public void setNum(Vector2 x)
        {
            num1 = x.X;
            num2 = x.Y;
        }
        public void setColor(Color c)
        {
            color = c;
        }

    }
}
