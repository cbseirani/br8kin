/******************************************
 * 04/10/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  Clouds.cs 
 * 
 *****************************************/
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Br8kIn_
{
    class Clouds : Blocks
    {
        //instance variables
        List<bCloud> bclouds;
        List<sCloud> sclouds;
        List<RainDrop> rainDrops;
        Texture2D rainD;
        Engine eng;
        Random randomNum;
        Vector2 speed;
        short num1, num2, rainF, rF;
        int[] b, s;
        float layer, layAdd;
        int countb, counts, countr;

        /*****************************************/

        //constructor
        public Clouds(short numb1, short numb2, Vector2 spe, float lay, short rainFr)
        {
            randomNum = new Random();
            bclouds = new List<bCloud>();
            sclouds = new List<sCloud>();
            rainDrops = new List<RainDrop>();
            num1 = numb1;
            num2 = numb2;
            b = new int[20];
            s = new int[20];
            countb = 0;
            counts = 0;
            countr = 0;
            speed = spe;
            layer = lay;
            rF = rainFr;
            rainF = rF;
            layAdd = .005f;
        }

        /*****************************************/

        //load
        public void Load(Engine engine)
        {
            eng = engine;
            for (int a = 0; a < num1; a++)
            {
                bclouds.Add(new bCloud(new Vector2(), speed, new Point(243, 73), engine,
                    randomNum, layer, this));
            }
            for (int b = 0; b < num2; b++)
            {
                sclouds.Add(new sCloud(new Vector2(), speed, new Point(100, 60), engine,
                    randomNum, layer));
            }
            rainD = engine.Content.Load<Texture2D>(@"Particles/rain01");
        }
        public void Load(Engine engine, List<int> y) { }

        /*****************************************/

        //update
        public void Update(BallManager bManager, StatsManager stats, GameTime gT,
            PlayerManager pM, SoundManager sManager, ExtraBallManager xBall)
        {
            for (int a = 0; a < num1; a++)
            {
                bclouds[a].Update(bManager, stats, gT, sManager, xBall);
            }
            for (int b = 0; b < num2; b++)
            {
                sclouds[b].Update(bManager, stats, gT, sManager, xBall);
            }
            rain(bManager, pM, gT, sManager, xBall);
        }
        public void Update(BallManager bManager, StatsManager stats, GameTime gT, 
            SoundManager sManager, ExtraBallManager xBall) { }

        /*****************************************/

        //draw
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int a = 0; a < num1; a++)
            {
                bclouds[a].Draw(spriteBatch);
            }
            for (int b = 0; b < num2; b++)
            {
                sclouds[b].Draw(spriteBatch);
            }
            for (int i = 0; i < countr; i++)
            {
                rainDrops[i].Draw(spriteBatch);
            }
        }

        /*****************************************/

        public void setPositions(List<Vector2> x, List<Vector2> y)
        {
            for (int a = 0; a < num1; a++)
            {
                bclouds[a].setPos(x[a]);
            }
            for (int b = 0; b < num2; b++)
            {
                sclouds[b].setPos(y[b]);
            }
        }
        public void setPositions(List<Vector2> x) { }

        /*****************************************/

        public bool isOn()
        {
            bool x = false;
            countb = 0;
            counts = 0;

            for (int a = 0; a < num1; a++)
            {
                if (bclouds[a].isOn())
                {
                    if (bclouds[a].getPos().X > 50 && bclouds[a].getPos().X < 1230)
                    {
                        b[countb] = a;
                        countb++;
                    }
                    x = true;
                }
            }
            for (int j = 0; j < num2; j++)
            {
                if (sclouds[j].isOn())
                {
                    if (sclouds[j].getPos().X > 50 && sclouds[j].getPos().X < 1230)
                    {
                        s[counts] = j;
                        counts++;
                    }
                    x = true;
                }
            }

            return x;
        }

        /*****************************************/

        private void rain(BallManager bM, PlayerManager pM, GameTime gT, 
            SoundManager sManager, ExtraBallManager xBall)
        {
            //at end of rain frequency period
            if (rainF <= 0)
            {
                rainF = rF;

                //if big and small clouds exist on screen
                if (countb > 0 && counts > 0)
                {
                    //randomly pick either big or small
                    if (randomNum.Next() % 10 >= 5)
                    {
                        //drop rain drop
                        rainDrops.Add(new RainDrop(bclouds[b[randomNum.Next() % countb]].getPos(),
                            new Vector2(0, 3), new Point(25, 32), rainD, randomNum, layer - .1f, eng));
                        countr++;
                        sManager.playSound("waterDrop");
                    }
                    else
                    {
                        rainDrops.Add(new RainDrop(sclouds[s[randomNum.Next() % counts]].getPos(),
                            new Vector2(0, 3), new Point(25, 32), rainD, randomNum, layer - .1f, eng));
                        countr++;
                    }
                }
                //if only big clouds exist on screen
                else if (countb > 0 && counts <= 0)
                {
                    //randomly drop from big
                    rainDrops.Add(new RainDrop(bclouds[b[randomNum.Next() % countb]].getPos(),
                        new Vector2(0, 3), new Point(25, 32), rainD, randomNum, layer - .1f, eng));
                    countr++;
                    sManager.playSound("waterDrop");
                }
                //if only small clouds exist on screen
                else if (countb <= 0 && counts > 0)
                {
                    //randomly drop from small
                    rainDrops.Add(new RainDrop(sclouds[s[randomNum.Next() % counts]].getPos(),
                        new Vector2(0, 3), new Point(25, 32), rainD, randomNum, layer - .1f, eng));
                    countr++;
                    sManager.playSound("waterDrop");
                }
            }
            else
            {
                rainF--;
            }

            //update rain drops
            for (int a = 0; a < countr; a++)
            {
                rainDrops[a].Update(bM, pM, gT, xBall);
            }
        }

        /*****************************************/

        public void splitUp(Vector2 pos)
        {
            sclouds.Add(new sCloud(new Vector2(-500, -500), speed,
                new Point(100, 60), eng, randomNum, layer + layAdd));
            num2++;
            sclouds.Add(new sCloud(new Vector2(-500, -500), speed,
                new Point(100, 60), eng, randomNum, layer + layAdd));
            num2++;
            layAdd += .001f;

            sclouds[num2 - 1].splitOn(new Vector2(pos.X - 50, pos.Y), new Vector2(-1, 0), 15);
            sclouds[num2 - 2].splitOn(new Vector2(pos.X + 50, pos.Y), new Vector2(1.5f, 0), 15);
        }

        /*****************************************/
        /*****************************************/
        /*****************************************/
        /*****************************************/

        private class bCloud : Sprite
        {
            //instance variables
            Texture2D img;
            Random rand;
            Vector2 origin;
            Clouds c;
            CloudParticles cP, aP;
            bool on, part, flip;
            short num2;
            float layer;

            /**********************/

            //constructor
            public bCloud(Vector2 pos, Vector2 spe, Point fSize,
                Engine engine, Random r, float lay, Clouds cl)
                : base(pos, spe, fSize)
            {
                rand = r;
                c = cl;
                cP = new CloudParticles();
                aP = new CloudParticles();
                on = true;
                layer = lay;
                part = false;
                flip = false;
                num2 = 7;
                origin = new Vector2(fSize.X / 2, frameSize.Y / 2);
                getBig(rand.Next(), engine);

                cP.Load(engine.Content.Load<Texture2D>(@"Particles/c01"),
                    engine.Content.Load<Texture2D>(@"Particles/c02"),
                    engine.Content.Load<Texture2D>(@"Particles/c03"),
                    engine.Content.Load<Texture2D>(@"Particles/c04"));
                aP.Load(engine.Content.Load<Texture2D>(@"Particles/c01"),
                    engine.Content.Load<Texture2D>(@"Particles/c02"),
                    engine.Content.Load<Texture2D>(@"Particles/c03"),
                    engine.Content.Load<Texture2D>(@"Particles/c04"));
            }

            /**********************/

            //update
            public void Update(BallManager bManager, StatsManager stats, GameTime gT, 
                SoundManager sManager, ExtraBallManager xBall)
            {
                if (on)
                {
                    //move cloud
                    position.X += speed.X;
                    if (speed.X > 0)
                    {
                        if (position.X - frameSize.X > 1280)
                        {
                            position.X = 0 - frameSize.X / 2;
                        }
                    }
                    else if (speed.X < 0)
                    {
                        if (position.X + frameSize.X < 0)
                        {
                            position.X = 1280 + frameSize.X / 2;
                        }
                    }

                    //check collision
                    extraColl(xBall, stats, sManager);
                    if (bManager.getCollRect1().Intersects(getColl()) ||
                        bManager.getCollRect2().Intersects(getColl()) ||
                        bManager.getCollRect3().Intersects(getColl()) ||
                        bManager.getCollRect4().Intersects(getColl()))
                    {
                        bManager.setColl(false);
                        setPart();
                        xBall.incCombo();
                        stats.addPoint(3);
                        c.splitUp(position);
                        on = false;
                        sManager.playSound("cloudBurst");
                    }
                }
                else
                {
                    //update particles
                    if (num2 <= 0)
                    {
                        num2 = 4;
                        part = false;
                    }
                    else if (part)
                    {
                        num2--;
                    }
                    cP.Update(part, gT);
                    aP.Update(part, gT);
                }
            }

            /**********************/

            //draw 
            public void Draw(SpriteBatch spriteBatch)
            {
                if (on)
                {
                    if (flip)
                    {
                        //draw big cloud
                        spriteBatch.Draw(img, position, null, Color.White,
                             0, origin, 1f, SpriteEffects.None, layer);
                    }
                    else
                    {
                        spriteBatch.Draw(img, position, null, Color.White,
                             0, origin, 1f, SpriteEffects.FlipHorizontally, layer);
                    }
                }
                else
                {
                    //draw particles
                    cP.Draw(spriteBatch);
                    aP.Draw(spriteBatch);
                }
            }

            /**********************/

            private void getBig(int x, Engine engine)
            {
                if ((x % 10) >= 5)
                {
                    flip = true;
                }
                switch ((x % 4) + 1)
                {
                    case 1:
                        img = engine.Content.Load<Texture2D>(@"Blocks/Clouds/big1");
                        break;
                    case 2:
                        img = engine.Content.Load<Texture2D>(@"Blocks/Clouds/big2");
                        break;
                    case 3:
                        img = engine.Content.Load<Texture2D>(@"Blocks/Clouds/big3");
                        break;
                    case 4:
                        img = engine.Content.Load<Texture2D>(@"Blocks/Clouds/big4");
                        break;
                }
            }

            /**********************/

            //collision rectangles
            public Rectangle getColl()
            {
                return new Rectangle((int)(position.X - frameSize.X / 2) - 5,
                    (int)(position.Y + frameSize.Y / 2) - 20, frameSize.X - 10, 15);
            }

            /**********************/

            private void setPart()
            {
                part = true;
                cP.setPos(new Vector2(position.X, position.Y - 40));
                cP.setNum(new Vector2(1, .2f));
                aP.setPos(new Vector2(position.X, position.Y));
                aP.setNum(new Vector2(0, 0));
            }

            /**********************/

            public bool isOn()
            {
                return on;
            }

            /**********************/

            public void extraColl(ExtraBallManager xBall, StatsManager stats,
                SoundManager sManager)
            {
                //check collision
                for (int a = 0; a < 2; a++)
                {
                    if (xBall.getBalls()[a].getOn())
                    {
                        if (xBall.getBalls()[a].getCollRect1().Intersects(getColl()) ||
                            xBall.getBalls()[a].getCollRect2().Intersects(getColl()) ||
                            xBall.getBalls()[a].getCollRect3().Intersects(getColl()) ||
                            xBall.getBalls()[a].getCollRect4().Intersects(getColl()))
                        {
                            xBall.getBalls()[a].setColl(false);
                            setPart();
                            stats.addPoint(3);
                            c.splitUp(position);
                            on = false;
                            sManager.playSound("cloudBurst");
                        }
                    }
                }
            }
        }

        /*****************************************/
        /*****************************************/
        /*****************************************/
        /*****************************************/

        private class sCloud : Sprite
        {
            //instance variables
            Texture2D img;
            Vector2 origin, tempS;
            Random rand;
            CloudParticles cP, aP;
            bool on, part, split, flip;
            short num2, aniF;
            float layer;

            /**********************/

            //constructor
            public sCloud(Vector2 pos, Vector2 spe, Point fSize, Engine engine,
                Random r, float lay)
                : base(pos, spe, fSize)
            {
                rand = r;
                cP = new CloudParticles();
                aP = new CloudParticles();
                layer = lay;
                on = true;
                num2 = 4;
                part = false;
                flip = false;
                split = false;
                origin = new Vector2(fSize.X / 2, fSize.Y / 2);
                getSmall(rand.Next(), engine);

                cP.Load(engine.Content.Load<Texture2D>(@"Particles/c01"),
                    engine.Content.Load<Texture2D>(@"Particles/c02"),
                    engine.Content.Load<Texture2D>(@"Particles/c03"),
                    engine.Content.Load<Texture2D>(@"Particles/c04"));
                aP.Load(engine.Content.Load<Texture2D>(@"Particles/c01"),
                    engine.Content.Load<Texture2D>(@"Particles/c02"),
                    engine.Content.Load<Texture2D>(@"Particles/c03"),
                    engine.Content.Load<Texture2D>(@"Particles/c04"));
            }

            /**********************/

            //update
            public void Update(BallManager bManager, StatsManager stats, GameTime gT, 
                SoundManager sManager, ExtraBallManager xBall)
            {
                if (on)
                {
                    //move clouds
                    if (!split)
                    {
                        position.X += speed.X;
                        if (speed.X > 0)
                        {
                            if (position.X - 243 > 1280)
                            {
                                position.X = 0 - 243 / 2;
                            }
                        }
                        else if (speed.X < 0)
                        {
                            if (position.X + 243 < 0)
                            {
                                position.X = 1280 + 243 / 2;
                            }
                        }
                    }
                    else
                    {
                        splitUp();
                    }

                    //check collision
                    extraColl(xBall, stats, sManager);
                    if (bManager.getCollRect1().Intersects(getColl()) ||
                        bManager.getCollRect2().Intersects(getColl()) ||
                        bManager.getCollRect3().Intersects(getColl()) ||
                        bManager.getCollRect4().Intersects(getColl()))
                    {
                        bManager.setColl(false);
                        setPart();
                        xBall.incCombo();
                        stats.addPoint(4);
                        on = false;
                        sManager.playSound("cloudBurst");
                    }
                }
                else
                {
                    //update particles
                    if (num2 <= 0)
                    {
                        num2 = 4;
                        part = false;
                    }
                    else if (part)
                    {
                        num2--;
                    }
                    cP.Update(part, gT);
                    aP.Update(part, gT);
                }
            }

            /**********************/

            //draw 
            public void Draw(SpriteBatch spriteBatch)
            {
                if (on)
                {
                    if (flip)
                    {
                        //draw cloud
                        spriteBatch.Draw(img, position, null, Color.White,
                            0, origin, 1f, SpriteEffects.None, layer);
                    }
                    else
                    {
                        //draw cloud
                        spriteBatch.Draw(img, position, null, Color.White,
                            0, origin, 1f, SpriteEffects.FlipHorizontally, layer);
                    }
                }
                else
                {
                    //draw particles
                    cP.Draw(spriteBatch);
                    aP.Draw(spriteBatch);
                }
            }

            /**********************/

            private void getSmall(int x, Engine engine)
            {
                if ((x % 10) >= 5)
                {
                    flip = true;
                }

                switch ((x % 4) + 1)
                {
                    case 1:
                        img = engine.Content.Load<Texture2D>(@"Blocks/Clouds/small1");
                        break;
                    case 2:
                        img = engine.Content.Load<Texture2D>(@"Blocks/Clouds/small2");
                        break;
                    case 3:
                        img = engine.Content.Load<Texture2D>(@"Blocks/Clouds/small3");
                        break;
                    case 4:
                        img = engine.Content.Load<Texture2D>(@"Blocks/Clouds/small4");
                        break;
                }
            }

            /**********************/

            //collision rectangles
            public Rectangle getColl()
            {
                return new Rectangle((int)(position.X - frameSize.X / 2) - 5, (int)(position.Y + frameSize.Y / 2) - 20,
                    frameSize.X - 10, 15);
            }

            /**********************/

            private void setPart()
            {
                part = true;
                cP.setPos(new Vector2(position.X, position.Y - 40));
                cP.setNum(new Vector2(1, .2f));
                aP.setPos(new Vector2(position.X, position.Y));
                aP.setNum(new Vector2(0, 0));
            }

            /**********************/

            public bool isOn()
            {
                return on;
            }

            /**********************/

            public void splitOn(Vector2 pos, Vector2 spe, short ani)
            {
                position = pos;
                tempS = spe;
                aniF = ani;
                split = true;
            }
            private void splitUp()
            {
                if (aniF > 0)
                {
                    position.X += tempS.X;
                    aniF--;
                }
                else
                {
                    split = false;
                }
            }

            /**********************/

            public void extraColl(ExtraBallManager xBall, StatsManager stats,
                SoundManager sManager)
            {
                //check collision
                for (int a = 0; a < 2; a++)
                {
                    if (xBall.getBalls()[a].getOn())
                    {
                        if (xBall.getBalls()[a].getCollRect1().Intersects(getColl()) ||
                            xBall.getBalls()[a].getCollRect2().Intersects(getColl()) ||
                            xBall.getBalls()[a].getCollRect3().Intersects(getColl()) ||
                            xBall.getBalls()[a].getCollRect4().Intersects(getColl()))
                        {
                            xBall.getBalls()[a].setColl(false);
                            setPart();
                            stats.addPoint(4);
                            on = false;
                            sManager.playSound("cloudBurst");
                        }
                    }
                }
            }
        }

        /*****************************************/
        /*****************************************/
        /*****************************************/
        /*****************************************/

        private class RainDrop : Sprite
        {
            //instance variables
            Texture2D drop;
            RainParticles rP;
            Vector2 origin;
            bool flip, on, part, first;
            float layer;
            short num2, c2;

            /**********************/

            //constructor
            public RainDrop(Vector2 pos, Vector2 spe, Point fSize, Texture2D x,
                Random r, float l, Engine eng)
                : base(pos, spe, fSize)
            {
                on = true;
                part = false;
                drop = x;
                layer = l;
                origin = new Vector2(fSize.X / 2, fSize.Y / 2);
                flip = false;
                if ((r.Next() % 10) >= 5)
                {
                    flip = true;
                }
                rP = new RainParticles();
                rP.Load(eng);
                num2 = 4;
                c2 = 0;
                first = false;
            }

            /**********************/

            //update
            public void Update(BallManager bM, PlayerManager pM, GameTime gT, 
                ExtraBallManager xBall)
            {
                if (on)
                {
                    //move rain drop
                    moveDrop();

                    //check for collisions
                    if (position.Y >= 720)
                    {
                        on = false;
                        setPart();
                    }
                    extraColl(xBall);
                    if (bM.getCollRect().Intersects(getColl()))
                    {
                        on = false;
                        setPart();
                    }
                    if (!pM.getGrace())
                    {
                        if (pM.getOn())
                        {
                            if (pM.getCollRect().Intersects(getColl2()) ||
                                pM.getCollRectL().Intersects(getColl2()) ||
                                pM.getCollRectR().Intersects(getColl2()))
                            {
                                on = false;
                                pM.gotHit();
                                setPart();
                            }
                        }
                    }
                }
                else
                {
                    //update particles
                    if (num2 <= 0)
                    {
                        num2 = 4;
                        part = false;
                    }
                    else if (part)
                    {
                        num2--;
                    }

                    //update particles
                    rP.Update(part, gT);
                }
            }

            /**********************/

            //draw
            public void Draw(SpriteBatch spriteBatch)
            {
                if (on)
                {
                    if (flip)
                    {
                        spriteBatch.Draw(drop, position, null, Color.White,
                            0, origin, 1f, SpriteEffects.FlipHorizontally, .23f);
                    }
                    else
                    {
                        spriteBatch.Draw(drop, position, null, Color.White,
                            0, origin, 1f, SpriteEffects.None, .23f);
                    }
                }
                else
                {
                    //draw particles
                    rP.Draw(spriteBatch);
                }
            }

            /**********************/

            private void setPart()
            {
                part = true;
                rP.setPos(new Vector2(position.X, position.Y));
                rP.setNum(new Vector2(1, 1));
            }

            /**********************/

            private Rectangle getColl()
            {
                return new Rectangle((int)(position.X - frameSize.X / 2),
                    (int)(position.Y - frameSize.Y / 2), frameSize.X, 32);
            }
            private Rectangle getColl2()
            {
                return new Rectangle((int)(position.X - frameSize.X / 2),
                    (int)position.Y, frameSize.X, (int)frameSize.Y / 2 - 10);
            }

            /**********************/

            public bool isOn()
            {
                return on;
            }

            /**********************/

            private void moveDrop()
            {
                if (!first)
                {
                    first = true;
                    speed = new Vector2(0, 1.4f);
                }

                if (c2 >= 4)
                {
                    c2 = 0;
                    speed.Y += .5f;
                }
                else
                {
                    c2++;
                }

                position += speed;
            }

            /**********************/

            public void extraColl(ExtraBallManager xBall)
            {
                //check collision
                for (int a = 0; a < 2; a++)
                {
                    if (xBall.getBalls()[a].getOn())
                    {
                        if (xBall.getBalls()[a].getCollRect().Intersects(getColl()))
                        {
                            on = false;
                            setPart();
                        }
                    }
                }
            }
        }
    }
}
