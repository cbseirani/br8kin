/******************************************
 * 05/22/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  AlienParticles.cs 
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
    class AlienParticles
    {
        private Random random;
        private List<Particle> particles;
        private List<Texture2D> textures;
        Color color;
        float num1, num2;
        bool on, load;

        public AlienParticles()
        {
            textures = new List<Texture2D>();
            particles = new List<Particle>();
            random = new Random();
            num1 = num2 = 0;
            load = true;
        }

        public void Load(Engine eng, short num)
        {
            switch (num)
            {
                case 1:
                    textures.Add(eng.Content.Load<Texture2D>(@"Blocks/Aliens/a11"));
                    textures.Add(eng.Content.Load<Texture2D>(@"Blocks/Aliens/a12"));
                    textures.Add(eng.Content.Load<Texture2D>(@"Blocks/Aliens/a13"));
                    textures.Add(eng.Content.Load<Texture2D>(@"Blocks/Aliens/a14"));
                    textures.Add(eng.Content.Load<Texture2D>(@"Blocks/Aliens/a15"));
                    break;
                case 2:
                    textures.Add(eng.Content.Load<Texture2D>(@"Blocks/Aliens/a21"));
                    textures.Add(eng.Content.Load<Texture2D>(@"Blocks/Aliens/a22"));
                    textures.Add(eng.Content.Load<Texture2D>(@"Blocks/Aliens/a23"));
                    textures.Add(eng.Content.Load<Texture2D>(@"Blocks/Aliens/a24"));
                    textures.Add(eng.Content.Load<Texture2D>(@"Blocks/Aliens/a25"));
                    break;
                case 3:
                    textures.Add(eng.Content.Load<Texture2D>(@"Blocks/Aliens/a31"));
                    textures.Add(eng.Content.Load<Texture2D>(@"Blocks/Aliens/a32"));
                    textures.Add(eng.Content.Load<Texture2D>(@"Blocks/Aliens/a33"));
                    textures.Add(eng.Content.Load<Texture2D>(@"Blocks/Aliens/a34"));
                    textures.Add(eng.Content.Load<Texture2D>(@"Blocks/Aliens/a35"));
                    break;
                case 4:
                    textures.Add(eng.Content.Load<Texture2D>(@"Blocks/Aliens/a41"));
                    textures.Add(eng.Content.Load<Texture2D>(@"Blocks/Aliens/a42"));
                    textures.Add(eng.Content.Load<Texture2D>(@"Blocks/Aliens/a43"));
                    textures.Add(eng.Content.Load<Texture2D>(@"Blocks/Aliens/a44"));
                    textures.Add(eng.Content.Load<Texture2D>(@"Blocks/Aliens/a45"));
                    break;
            }
        }

        public void Update(bool x, GameTime gT, Vector2 posi)
        {
            on = x;

            if (on)
            {
                if (load)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        particles.Add(GenerateNewParticle(i, posi));
                    }
                    load = false;
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
        }

        private Particle GenerateNewParticle(int x, Vector2 y)
        {
            Vector2 position = y;
            Vector2 velocity = new Vector2(
                                    -1f * (float)(random.NextDouble() * 2 - num1),
                                    -1f * (float)(random.NextDouble() * 2 - num2));
            float angle = (float)(random.NextDouble() * 2 - 1);
            float angularVelocity = .2f * (float)(random.NextDouble() * 2 - 1);
            float size = 1f;
            int ttl = 80;
            color = Color.White;
            return new Particle(textures[x], position, velocity, angle, angularVelocity, color, size, ttl);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (true)
            {
                for (int index = 0; index < particles.Count; index++)
                {
                    particles[index].Draw(spriteBatch, .25f);
                }
            }
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
