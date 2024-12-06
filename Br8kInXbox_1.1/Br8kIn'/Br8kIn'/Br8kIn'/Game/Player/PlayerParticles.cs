/******************************************
 * 06/25/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  PlayerParticles.cs 
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
    class PlayerParticles
    {
        Random random;
        Vector2 location;
        List<Particle> particles;
        List<Texture2D> textures;
        Color color;
        int ani, type;
        float num1, num2, lay;
        short num3, ttl;

        public PlayerParticles()
        {
            textures = new List<Texture2D>();
            particles = new List<Particle>();
            random = new Random();
            ani = 3;
            num1 = num2 = 0;
        }

        /*****************************************/

        public void Load(Engine engine, int t)
        {
            textures = new List<Texture2D>();
            type = t;

            switch (t)
            {
                case 1:
                    textures.Add(engine.Content.Load<Texture2D>(@"Particles/coin02"));
                    color = Color.White;
                    lay = .4f;
                    num3 = 30;
                    ttl = 55;
                    break;
                case 2:
                    textures.Add(engine.Content.Load<Texture2D>(@"Particles/snowball00_snow_21x22"));
                    textures.Add(engine.Content.Load<Texture2D>(@"Particles/snowball02_snow_21x22"));
                    textures.Add(engine.Content.Load<Texture2D>(@"Particles/snowball04_snow_21x22"));
                    textures.Add(engine.Content.Load<Texture2D>(@"Particles/snowball06_snow_21x22"));
                    color = Color.CornflowerBlue;
                    lay = .2f;
                    num3 = 3;
                    ttl = 50;
                    break;
                case 3:
                    textures.Add(engine.Content.Load<Texture2D>(@"Particles/snowball00_snow_21x22"));
                    textures.Add(engine.Content.Load<Texture2D>(@"Particles/snowball02_snow_21x22"));
                    textures.Add(engine.Content.Load<Texture2D>(@"Particles/snowball04_snow_21x22"));
                    textures.Add(engine.Content.Load<Texture2D>(@"Particles/snowball06_snow_21x22"));
                    color = Color.BurlyWood;
                    lay = .4f;
                    num3 = 30;
                    ttl = 55;
                    break;
                case 4:
                    textures.Add(engine.Content.Load<Texture2D>(@"Particles/snowball00_snow_21x22"));
                    textures.Add(engine.Content.Load<Texture2D>(@"Particles/snowball02_snow_21x22"));
                    textures.Add(engine.Content.Load<Texture2D>(@"Particles/snowball04_snow_21x22"));
                    textures.Add(engine.Content.Load<Texture2D>(@"Particles/snowball06_snow_21x22"));
                    color = Color.CornflowerBlue;
                    lay = .4f;
                    num3 = 30;
                    ttl = 55;
                    break;
                case 5:
                    textures.Add(engine.Content.Load<Texture2D>(@"Particles/snowball00_snow_21x22"));
                    textures.Add(engine.Content.Load<Texture2D>(@"Particles/snowball02_snow_21x22"));
                    textures.Add(engine.Content.Load<Texture2D>(@"Particles/snowball04_snow_21x22"));
                    textures.Add(engine.Content.Load<Texture2D>(@"Particles/snowball06_snow_21x22"));
                    color = Color.WhiteSmoke;
                    lay = .4f;
                    num3 = 5;
                    ttl = 15;
                    break;
                case 6:
                    textures.Add(engine.Content.Load<Texture2D>(@"Particles/shards10"));
                    color = Color.CornflowerBlue;
                    lay = .4f;
                    num3 = 30;
                    ttl = 55;
                    break;
            }
        }

        /*****************************************/

        public void Update(bool x, GameTime gT)
        {
            ani--;

            if (x)
            {
                if (ani <= 0)
                {
                    for (int i = 0; i < num3; i++)
                    {
                        particles.Add(generateParticle(gT));
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

        /*****************************************/

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch, lay);
            }
        }

        /*****************************************/

        private Particle generateParticle(GameTime gT)
        {
            Texture2D texture = textures[((gT.ElapsedGameTime.Milliseconds + random.Next() +
                (int)location.Y) % textures.Count)];
            Vector2 position = location;
            Vector2 velocity = new Vector2(
                                    -1f * (float)(random.NextDouble() * 2 - num1),
                                    -1f * (float)(random.NextDouble() * 2 - num2));
            float angle = (float)(random.NextDouble() * 2 - 1);
            float angularVelocity = .2f * (float)(random.NextDouble() * 2 - 1);
            float size = (float)random.NextDouble() % .8f;

            return new Particle(texture, position, velocity, angle,
                angularVelocity, color, size, ttl);
        }
        public void setPos(Vector2 pos)
        {
            location = pos;
        }
        public void setNum(Vector2 x)
        {
            num1 = x.X;
            num2 = x.Y;
        }
        public void endParts()
        {
            particles = new List<Particle>();
            switch (type)
            {
                case 1:
                    lay = .4f;
                    num3 = 30;
                    ttl = 55;
                    break;
                case 2:
                    lay = .2f;
                    num3 = 3;
                    ttl = 20;
                    break;
                case 3:
                    lay = .4f;
                    num3 = 30;
                    ttl = 55;
                    break;
                case 4:
                    lay = .4f;
                    num3 = 30;
                    ttl = 55;
                    break;
                case 5:
                    lay = .4f;
                    num3 = 5;
                    ttl = 15;
                    break;
                case 6:
                    lay = .4f;
                    num3 = 30;
                    ttl = 55;
                    break;
            }
        }
        public int getType()
        {
            return type;
        }
        public void setAm()
        {
            num3 *= 2;
            ttl += 10;
        }
        public void setDes()
        {
            ttl = 55;
            num3 = 30;
        }
        public void setDesM()
        {
            ttl = 55;
            num3 = 30;
            color = Color.Gray;
        }
        public void setColor()
        {
            color = Color.BurlyWood;
        }
    }
}
