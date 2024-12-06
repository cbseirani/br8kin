/******************************************
 * 04/06/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  BallParticles.cs 
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
    class BallParticles
    {
        private Random random;
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> particles;
        int ani;
        private List<Texture2D> textures;
        Texture2D text2;
        Color color;
        float num1, num2, lay;
        int num3, level;
        bool des, trail, type;

        public BallParticles()
        {
            textures = new List<Texture2D>();
            particles = new List<Particle>();
            random = new Random();
            ani = 3;
            trail = false;
            des = false;
            num1 = num2 = num3 = 0;
        }

        public void Load(Engine engine, int lvl, bool x)
        {
            textures = new List<Texture2D>();
            level = lvl;
            des = false;
            trail = false;
            type = x;
            if (x)
            {
                text2 = engine.Content.Load<Texture2D>(@"Particles/ballP2");
            }
            else
            {
                text2 = engine.Content.Load<Texture2D>(@"Particles/ballP1");
            }

            if (lvl <= 3 || lvl == 11 || lvl == 12 || lvl == 13)
            {
                textures.Add(engine.Content.Load<Texture2D>(@"Particles/shards01"));
                textures.Add(engine.Content.Load<Texture2D>(@"Particles/shards02"));
                textures.Add(engine.Content.Load<Texture2D>(@"Particles/shards03"));
                textures.Add(engine.Content.Load<Texture2D>(@"Particles/shards04"));
                textures.Add(engine.Content.Load<Texture2D>(@"Particles/shards05"));
                textures.Add(engine.Content.Load<Texture2D>(@"Particles/shards06"));
            }
            else if (lvl == 7 || lvl == 9 || lvl == 17 || lvl == 19)
            {
                textures.Add(engine.Content.Load<Texture2D>(@"Particles/shards08"));
                textures.Add(engine.Content.Load<Texture2D>(@"Particles/shards09"));
            }
            else if (lvl == 10 || lvl == 20)
            {
                textures.Add(engine.Content.Load<Texture2D>(@"Particles/shards10"));
                lay = .28f;
            }
        }

        public void Update(bool x)
        {
            ani--;

            if (x)
            {
                if (!trail)
                {
                    if (ani <= 0)
                    {
                        for (int i = 0; i < 30; i++)
                        {
                            particles.Add(GenerateNewParticle());
                        }
                        ani = 3;
                    }
                }
                else
                {
                    for (int i = 0; i < 1; i++)
                    {
                        particles.Add(GenerateNewParticle());
                    }
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

        private Particle GenerateNewParticle()
        {
            Texture2D texture;
            int ttl;
            float size;
            float angle;

            if (trail)
            {
                texture = text2;
                ttl = 15;
                size = (float)random.NextDouble() % .8f;
                angle = (float)(random.NextDouble() * 2 - 5);
                lay = .1f;
            }
            else if (!des)
            {
                texture = textures[num3];
                ttl = 47;
                size = (float)random.NextDouble() % .5f;
                angle = (float)(random.NextDouble() * 2 - 7);
                lay = .3f;
            }
            else
            {
                texture = text2;
                ttl = 130;
                size = (float)random.NextDouble() % .8f;
                angle = (float)(random.NextDouble() * 2 - 1);
                lay = .1f;
            }

            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                                    -1f * (float)(random.NextDouble() * 2 - num1),
                                    -1f * (float)(random.NextDouble() * 2 - num2));

            float angularVelocity = .2f * (float)(random.NextDouble() * 2 - 1);
            color = Color.White;
            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (type)
            {
                if (level == 10 || level == 20)
                {
                    for (int index = 0; index < particles.Count; index++)
                    {
                        particles[index].Draw(spriteBatch, lay);
                    }
                }
                else
                {
                    for (int index = 0; index < particles.Count; index++)
                    {
                        particles[index].Draw(spriteBatch, lay);
                    }
                }
            }
            else
            {
                for (int index = 0; index < particles.Count; index++)
                {
                    particles[index].Draw(spriteBatch, .98f);
                }
            }
        }

        public void setPos(Vector2 pos)
        {
            EmitterLocation = pos;
        }
        public void setNum(Vector2 x, int y)
        {
            num1 = x.X;
            num2 = x.Y;
            num3 = y;
        }
        public void setNum(Vector2 x, bool y)
        {
            num1 = x.X;
            num2 = x.Y;
            des = y;
        }
        public void setTrail(Vector2 x, bool y)
        {
            num1 = x.X;
            num2 = x.Y;
            trail = y;
        }
        public void setColor(Color c)
        {
            color = c;
        }
        public void trailEnd()
        {
            particles = new List<Particle>();
        }

    }
}
