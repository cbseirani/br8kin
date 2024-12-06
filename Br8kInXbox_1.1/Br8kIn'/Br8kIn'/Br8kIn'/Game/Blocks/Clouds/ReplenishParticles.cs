/******************************************
 * 07/17/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  ReplenishParticles.cs 
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
    class ReplenishParticles
    {
        private Random random;
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> particles;
        int ani;
        private List<Texture2D> textures;
        Color color;
        float num1, num2;

        public ReplenishParticles()
        {
            textures = new List<Texture2D>();
            particles = new List<Particle>();
            random = new Random();
            ani = 3;
            num1 = num2 = 0;
            color = Color.White;
        }

        public void Load(Engine eng)
        {
            textures.Add(eng.Content.Load<Texture2D>(@"Particles/snowball00_snow_21x22"));
            textures.Add(eng.Content.Load<Texture2D>(@"Particles/snowball02_snow_21x22"));
            textures.Add(eng.Content.Load<Texture2D>(@"Particles/snowball04_snow_21x22"));
            textures.Add(eng.Content.Load<Texture2D>(@"Particles/snowball06_snow_21x22"));
        }

        public void Update(bool x, GameTime gT)
        {
            ani--;

            if (x)
            {
                if (ani <= 0)
                {
                    for (int i = 0; i < 15; i++)
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
            Texture2D texture = textures[3];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                                    -1f * (float)(random.NextDouble() * 2 - 1),
                                    -1f * (float)(random.NextDouble() * 2 - 1));
            float angle = (float)(random.NextDouble() * 2 - 1);
            float angularVelocity = .2f * (float)(random.NextDouble() * 2 - 1);
            float size = (float)random.NextDouble() % 1.2f;
            int ttl = 30;
            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch, .4f);
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
