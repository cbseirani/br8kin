/******************************************
 * 07/06/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  MoveParticles.cs 
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
    class MoveParticles
    {
        private Random random;
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> particles;
        Texture2D text2;
        Color color;
        int ttl, a;
        float lay;

        public MoveParticles()
        {
            particles = new List<Particle>();
            random = new Random();
            a = 1;
        }

        public void Load(Engine engine)
        {
            text2 = engine.Content.Load<Texture2D>(@"Particles/ballP22");
            ttl = 20;
            lay = .1f;
        }

        public void Update()
        {
            if (a <= 0)
            {
                a = 1;
                for (int i = 0; i < 1; i++)
                {
                    particles.Add(GenerateNewParticle());
                }
            }
            else
            {
                a--;
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
            float size = (float)random.NextDouble() % .3f;
            float angle = (float)(random.NextDouble() * 2 - 5);
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                                    -1f * (float)(random.NextDouble() * 2 - 1),
                                    -1f * (float)(random.NextDouble() * 2 - 1));

            float angularVelocity = .2f * (float)(random.NextDouble() * 2 - 1);
            color = Color.White;
            return new Particle(text2, position, velocity, angle, angularVelocity, color, size, ttl);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch, lay);
            }
        }

        public void setPos(Vector2 pos)
        {
            EmitterLocation = pos;
        }
    }
}
