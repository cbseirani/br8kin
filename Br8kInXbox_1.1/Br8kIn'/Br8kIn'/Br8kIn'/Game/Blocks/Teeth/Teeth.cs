/******************************************
 * 05/01/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  Teeth.cs 
 * 
 *****************************************/
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Br8kIn_
{
    class Teeth : Blocks
    {
        //instance variables
        List<Tooth> th;

        /*****************************************/

        //constructor
        public Teeth()
        {
            th = new List<Tooth>();
        }

        /*****************************************/

        //load
        public void Load(Engine engine)
        {
            th.Add(new Tooth(new Vector2(183, 112), new Vector2(0, 0), new Point(170, 200),
                engine, false, 2));
            th.Add(new Tooth(new Vector2(356, 119), new Vector2(0, 0), new Point(170, 200),
                engine, true, 2));
            th.Add(new Tooth(new Vector2(542, 112), new Vector2(0, 0), new Point(205, 230),
                engine, true, 1));
            th.Add(new Tooth(new Vector2(740, 112), new Vector2(0, 0), new Point(205, 230),
                engine, false, 1));
            th.Add(new Tooth(new Vector2(926, 119), new Vector2(0, 0), new Point(170, 200),
                engine, false, 2));
            th.Add(new Tooth(new Vector2(1096, 112), new Vector2(0, 0), new Point(170, 200),
                engine, true, 2));
        }
        public void Load(Engine engine, List<int> y) { }

        /*****************************************/

        //update
        public void Update(BallManager bM, StatsManager stats, GameTime gT,
            PlayerManager pM, SoundManager sManager, ExtraBallManager xBall)
        {
            for (short a = 0; a < 6; a++)
            {
                th[a].Update(bM, stats, gT, pM, sManager, xBall);
            }
        }
        public void Update(BallManager bManager, StatsManager stats, GameTime gT,
            SoundManager sManager, ExtraBallManager xBall) { }

        /*****************************************/

        //draw
        public void Draw(SpriteBatch spriteBatch)
        {
            for (short a = 0; a < 6; a++)
            {
                th[a].Draw(spriteBatch);
            }
        }

        /*****************************************/

        public bool isOn()
        {
            bool x = false;

            for (int c = 0; c < th.Count; c++)
            {
                if (th[c].isOn())
                {
                    x = true;
                }
                else if (th[c].shardOn())
                {
                    x = true;
                }
            }

            return x;
        }

        /*****************************************/

        public void setPositions(List<Vector2> x) { }
        public void setPositions(List<Vector2> x, List<Vector2> y) { }

        /*****************************************/
        /*****************************************/
        /*****************************************/
        /*****************************************/

        private class Tooth : Sprite
        {
            //instance variables
            Texture2D[] img;
            ToothShards tS;
            Vector2 origin;
            bool on, flip;
            short c1;

            /**********************/

            //constructor
            public Tooth(Vector2 pos, Vector2 spe, Point fSize,
                Engine eng, bool f, short t)
                : base(pos, spe, fSize)
            {
                on = true;
                flip = f;
                origin = new Vector2(fSize.X / 2, fSize.Y / 2);
                tS = new ToothShards(pos, spe, fSize, eng, f, t, origin);
                img = new Texture2D[4];
                c1 = 0;

                switch (t)
                {
                    case 1:
                        img[0] = eng.Content.Load<Texture2D>(@"Blocks/Teeth/tb01");
                        img[1] = eng.Content.Load<Texture2D>(@"Blocks/Teeth/tb02");
                        img[2] = eng.Content.Load<Texture2D>(@"Blocks/Teeth/tb03");
                        img[3] = eng.Content.Load<Texture2D>(@"Blocks/Teeth/tb04");
                        break;
                    case 2:
                        img[0] = eng.Content.Load<Texture2D>(@"Blocks/Teeth/t01");
                        img[1] = eng.Content.Load<Texture2D>(@"Blocks/Teeth/t02");
                        img[2] = eng.Content.Load<Texture2D>(@"Blocks/Teeth/t03");
                        img[3] = eng.Content.Load<Texture2D>(@"Blocks/Teeth/t04");
                        break;
                }
            }

            /**********************/

            //update
            public void Update(BallManager bM, StatsManager stats, GameTime gT,
                PlayerManager pM, SoundManager sManager, ExtraBallManager xBall)
            {
                if (on)
                {
                    //check collisions
                    extraColl(xBall, stats, pM, sManager);
                    if (bM.getCollRect().Intersects(getColl1()))
                    {
                        if (bM.getBall().getDir().Y < 0)
                        {
                            bM.setColl(false);
                            bM.setPos(new Vector2(bM.getPos().X, bM.getPos().Y + 10));
                            bM.bounce(new Vector2(1, -1));
                            xBall.incCombo();

                            if (c1 < 3)
                            {
                                c1++;
                                stats.addPoint(8);
                                bM.setPart(true, new Vector2(1, 2), 0);
                                sManager.playRandom();
                                if (c1 >= 3)
                                {
                                    on = false;
                                }
                            }
                        }
                    }
                    else if (bM.getCollRect().Intersects(getColl2()))
                    {
                        if (bM.getBall().getDir().X > 0)
                        {
                            bM.setColl(false);
                            bM.setPos(new Vector2(bM.getPos().X - 10, bM.getPos().Y));
                            bM.bounce(new Vector2(-1, 1));
                            xBall.incCombo();

                            if (c1 < 3)
                            {
                                c1++;
                                stats.addPoint(8);
                                bM.setPart(true, new Vector2(-.5f, 1), 0);
                                sManager.playRandom();
                                if (c1 >= 3)
                                {
                                    on = false;
                                }
                            }
                        }
                    }
                    else if (bM.getCollRect().Intersects(getColl3()))
                    {
                        if (bM.getBall().getDir().X < 0)
                        {
                            bM.setColl(false);
                            bM.setPos(new Vector2(bM.getPos().X + 10, bM.getPos().Y));
                            bM.bounce(new Vector2(-1, 1));
                            xBall.incCombo();

                            if (c1 < 3)
                            {
                                c1++;
                                stats.addPoint(8);
                                bM.setPart(true, new Vector2(2, 1), 0);
                                sManager.playRandom();
                                if (c1 >= 3)
                                {
                                    on = false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    //update tooth shards
                    tS.Update(bM, stats, gT, pM, xBall, sManager);
                }
            }

            /**********************/

            //draw
            public void Draw(SpriteBatch sB)
            {
                if (on)
                {
                    if (flip)
                    {
                        sB.Draw(img[c1], position, null, Color.White,
                            0, origin, 1f, SpriteEffects.FlipHorizontally, .25f);
                    }
                    else
                    {
                        sB.Draw(img[c1], position, null, Color.White,
                            0, origin, 1f, SpriteEffects.None, .25f);
                    }
                }
                else
                {
                    //draw tooth shards
                    tS.Draw(sB);
                }
            }

            /**********************/

            public Rectangle getColl1()
            {
                return new Rectangle((int)(position.X - frameSize.X / 2),
                    (int)(position.Y + (frameSize.Y / 2) - 30), frameSize.X, 20);
            }
            public Rectangle getColl2()
            {
                return new Rectangle((int)(position.X - frameSize.X / 2),
                    (int)(position.Y - frameSize.Y / 2), 20, frameSize.Y - 25);
            }
            public Rectangle getColl3()
            {
                return new Rectangle((int)(position.X + (frameSize.X / 2) - 20),
                    (int)(position.Y - frameSize.Y / 2), 20, frameSize.Y - 25);
            }

            /**********************/

            public bool isOn()
            {
                return on;
            }
            public bool shardOn()
            {
                return tS.isOn();
            }

            /**********************/

            public void extraColl(ExtraBallManager xBall, StatsManager stats,
                PlayerManager pM, SoundManager sManager)
            {
                //check collision
                for (int a = 0; a < 2; a++)
                {
                    if (xBall.getBalls()[a].getOn())
                    {
                        if (xBall.getBalls()[a].getCollRect().Intersects(getColl1()))
                        {
                            if (xBall.getBalls()[a].getBall().getDir().Y < 0)
                            {
                                xBall.getBalls()[a].setColl(false);
                                xBall.getBalls()[a].setPos(new Vector2(xBall.getBalls()[a].getPos().X,
                                    xBall.getBalls()[a].getPos().Y + 10));
                                xBall.getBalls()[a].bounce(new Vector2(1, -1));

                                if (c1 < 3)
                                {
                                    c1++;
                                    stats.addPoint(8);
                                    xBall.getBalls()[a].setPart(true, new Vector2(1, 2), 0);
                                    if (c1 >= 3)
                                    {
                                        on = false;
                                    }
                                }
                            }
                        }
                        else if (xBall.getBalls()[a].getCollRect().Intersects(getColl2()))
                        {
                            if (xBall.getBalls()[a].getBall().getDir().X > 0)
                            {
                                xBall.getBalls()[a].setColl(false);
                                xBall.getBalls()[a].setPos(new Vector2(xBall.getBalls()[a].getPos().X - 10,
                                    xBall.getBalls()[a].getPos().Y));
                                xBall.getBalls()[a].bounce(new Vector2(-1, 1));

                                if (c1 < 3)
                                {
                                    c1++;
                                    stats.addPoint(8);
                                    xBall.getBalls()[a].setPart(true, new Vector2(-.5f, 1), 0);
                                    if (c1 >= 3)
                                    {
                                        on = false;
                                    }
                                }
                            }
                        }
                        else if (xBall.getBalls()[a].getCollRect().Intersects(getColl3()))
                        {
                            if (xBall.getBalls()[a].getBall().getDir().X < 0)
                            {
                                xBall.getBalls()[a].setColl(false);
                                xBall.getBalls()[a].setPos(new Vector2(xBall.getBalls()[a].getPos().X + 10,
                                    xBall.getBalls()[a].getPos().Y));
                                xBall.getBalls()[a].bounce(new Vector2(-1, 1));

                                if (c1 < 3)
                                {
                                    c1++;
                                    stats.addPoint(8);
                                    xBall.getBalls()[a].setPart(true, new Vector2(2, 1), 0);

                                    if (c1 >= 3)
                                    {
                                        on = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /*****************************************/
        /*****************************************/
        /*****************************************/
        /*****************************************/

        private class ToothShards : Sprite
        {
            //instance variables
            Texture2D img;
            List<Shard> shards;
            Rectangle pColl;
            Vector2 origin;
            bool flip, inItFall, first;
            short type, c1, c2;

            /**********************/

            //constructor
            public ToothShards(Vector2 pos, Vector2 spe, Point fSize,
                Engine eng, bool f, short t, Vector2 ori)
                : base(pos, spe, fSize)
            {
                origin = ori;
                flip = f;
                type = t;
                inItFall = false;
                first = false;
                shards = new List<Shard>();
                fillShards(eng, t);
                c1 = 0;
                c2 = 0;
            }

            /**********************/

            //update
            public void Update(BallManager bM, StatsManager stats, GameTime gT,
                PlayerManager pM, ExtraBallManager xBall, SoundManager sManager)
            {
                //check for collision with piece
                extraColl(xBall, stats, pM);
                if (bM.getCollRect().Intersects(pColl))
                {
                    if (bM.getBall().getDir().Y < 0)
                    {
                        sManager.playRandom();
                        bM.setPos(new Vector2(bM.getPos().X, bM.getPos().Y + 10));
                        bM.bounce(new Vector2(1, -1));
                        bM.setPart(true, new Vector2(1, 2), 0);
                    }
                }

                //if ball hit for the last time
                if (inItFall)
                {


                    if (c2 >= 3)
                    {
                        c2 = 0;

                        switch (type)
                        {
                            //big
                            case 1:
                                //set fall starting from bottom to top
                                //0,2,3,4,6 - c1 = 0
                                if (c1 < 1)
                                {
                                    shards[0].setFall(1, .2599f);
                                    shards[2].setFall(2, .2598f);
                                    shards[3].setFall(3, .2597f);
                                    shards[4].setFall(4, .2596f);
                                    shards[6].setFall(5, .2595f);
                                }
                                //1,5 - c1 = 1
                                else if (c1 < 2)
                                {
                                    shards[1].setFall(3, .2594f);
                                    shards[5].setFall(4, .2593f);
                                }
                                //7,8,9,10 - c1 = 2
                                else if (c1 < 3)
                                {
                                    shards[7].setFall(1, .2592f);
                                    shards[8].setFall(2, .2591f);
                                    shards[9].setFall(3, .2590f);
                                    shards[10].setFall(4, .2589f);
                                }
                                //11,13,15,16 - c1 = 3
                                else if (c1 < 4)
                                {
                                    shards[11].setFall(1, .2588f);
                                    shards[13].setFall(2, .2587f);
                                    shards[15].setFall(3, .2586f);
                                    shards[16].setFall(4, .2585f);
                                }
                                //12,14 - c1 = 4
                                else if (c1 < 5)
                                {
                                    shards[12].setFall(3, .2584f);
                                    shards[14].setFall(4, .2583f);
                                }
                                //17,21 - c1 = 5
                                else if (c1 < 6)
                                {
                                    shards[17].setFall(3, .2582f);
                                    shards[21].setFall(4, .2581f);
                                }
                                //18,19,20,22,23 - c1 = 6
                                else if (c1 < 7)
                                {
                                    shards[18].setFall(1, .2580f);
                                    shards[19].setFall(2, .2579f);
                                    shards[20].setFall(3, .2578f);
                                    shards[22].setFall(4, .2577f);
                                    shards[23].setFall(5, .2576f);
                                }
                                c1++;
                                break;

                            //small
                            case 2:
                                //set fall starting from bottom to top
                                //0,1,2,3 - c1 = 0
                                if (c1 < 1)
                                {
                                    shards[0].setFall(1, .2599f);
                                    shards[1].setFall(2, .2598f);
                                    shards[2].setFall(3, .2597f);
                                    shards[3].setFall(4, .2596f);
                                }
                                //4,5,6,7 - c1 = 1
                                else if (c1 < 2)
                                {
                                    shards[4].setFall(1, .2595f);
                                    shards[5].setFall(2, .2594f);
                                    shards[6].setFall(3, .2593f);
                                    shards[7].setFall(4, .2592f);
                                }
                                //8,9,10,11 - c1 = 2
                                else if (c1 < 3)
                                {
                                    shards[8].setFall(1, .2591f);
                                    shards[9].setFall(2, .2590f);
                                    shards[10].setFall(3, .2589f);
                                    shards[11].setFall(4, .2588f);
                                }
                                //12,13,14,15,16 - c1 = 3
                                else if (c1 < 4)
                                {
                                    shards[12].setFall(1, .2587f);
                                    shards[13].setFall(2, .2586f);
                                    shards[14].setFall(3, .2585f);
                                    shards[15].setFall(4, .2584f);
                                    shards[16].setFall(5, .2583f);
                                }
                                c1++;
                                break;
                        }
                    }
                    else
                    {
                        c2++;
                    }
                }

                //update shards
                for (int a = 0; a < shards.Count; a++)
                {
                    shards[a].Update(bM, stats, gT, pM, this, xBall, sManager);
                }
            }

            /**********************/

            //draw
            public void Draw(SpriteBatch sB)
            {
                if (flip)
                {
                    sB.Draw(img, position, null, Color.White,
                        0, origin, 1f, SpriteEffects.FlipHorizontally, .25f);
                }
                else
                {
                    sB.Draw(img, position, null, Color.White,
                        0, origin, 1f, SpriteEffects.None, .25f);
                }

                //draw shards
                for (int a = 0; a < shards.Count; a++)
                {
                    shards[a].Draw(sB);
                }
            }

            /**********************/

            public bool isOn()
            {
                bool x = false;

                for (int c = 0; c < shards.Count; c++)
                {
                    if (shards[c].isOn())
                    {
                        x = true;
                    }
                }

                return x;
            }
            public void setFall(bool x, SoundManager sManager)
            {
                inItFall = x;
                if (!first)
                {
                    sManager.playRandom();
                    first = true;
                }
            }
            public bool getFall()
            {
                return inItFall;
            }

            /**********************/

            private void fillShards(Engine eng, short t)
            {
                switch (t)
                {
                    case 1:

                        img = eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p1");
                        pColl = new Rectangle((int)(position.X - frameSize.X / 2),
                            (int)(position.Y - frameSize.Y / 2), (int)(frameSize.X * 1.5), 73);

                        if (!flip)
                        {
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p2"),
                                origin, new Rectangle(144, 177, 58, 54), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p3"),
                                origin, new Rectangle(136, 176, 24, 35), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p4"),
                                origin, new Rectangle(111, 175, 43, 55), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p5"),
                                origin, new Rectangle(84, 182, 46, 50), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p6"),
                                origin, new Rectangle(34, 192, 70, 39), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p7"),
                                origin, new Rectangle(32, 176, 38, 38), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p8"),
                                origin, new Rectangle(0, 170, 46, 59), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p9"),
                                origin, new Rectangle(16, 152, 66, 41), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p10"),
                                origin, new Rectangle(63, 153, 57, 46), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p11"),
                                origin, new Rectangle(106, 144, 50, 68), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p12"),
                                origin, new Rectangle(145, 145, 55, 51), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p13"),
                                origin, new Rectangle(166, 135, 41, 51), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p14"),
                                origin, new Rectangle(128, 114, 58, 44), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p15"),
                                origin, new Rectangle(113, 136, 40, 25), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p16"),
                                origin, new Rectangle(66, 122, 59, 37), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p17"),
                                origin, new Rectangle(53, 137, 53, 41), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p18"),
                                origin, new Rectangle(0, 131, 59, 47), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p19"),
                                origin, new Rectangle(28, 99, 61, 51), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p20"),
                                origin, new Rectangle(0, 85, 49, 56), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p21"),
                                origin, new Rectangle(50, 90, 66, 42), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p22"),
                                origin, new Rectangle(99, 93, 53, 34), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p23"),
                                origin, new Rectangle(108, 108, 51, 32), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p24"),
                                origin, new Rectangle(128, 84, 63, 45), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p25"),
                                origin, new Rectangle(170, 77, 39, 67), flip, eng));
                        }
                        else
                        {
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p2"),
                                origin, new Rectangle(0, 177, 62, 54), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p3"),
                                origin, new Rectangle(42, 176, 24, 35), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p4"),
                                origin, new Rectangle(51, 175, 43, 55), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p5"),
                                origin, new Rectangle(82, 182, 46, 50), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p6"),
                                origin, new Rectangle(101, 192, 70, 39), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p7"),
                                origin, new Rectangle(137, 176, 38, 38), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p8"),
                                origin, new Rectangle(156, 170, 46, 59), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p9"),
                                origin, new Rectangle(123, 152, 66, 41), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p10"),
                                origin, new Rectangle(83, 153, 57, 46), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p11"),
                                origin, new Rectangle(54, 144, 50, 68), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p12"),
                                origin, new Rectangle(7, 145, 55, 51), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p13"),
                                origin, new Rectangle(0, 135, 41, 51), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p14"),
                                origin, new Rectangle(19, 114, 58, 44), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p15"),
                                origin, new Rectangle(53, 136, 40, 25), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p16"),
                                origin, new Rectangle(82, 122, 59, 37), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p17"),
                                origin, new Rectangle(99, 137, 53, 41), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p18"),
                                origin, new Rectangle(142, 131, 59, 47), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p19"),
                                origin, new Rectangle(119, 99, 61, 51), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p20"),
                                origin, new Rectangle(150, 85, 49, 56), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p21"),
                                origin, new Rectangle(90, 90, 66, 42), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p22"),
                                origin, new Rectangle(59, 93, 53, 34), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p23"),
                                origin, new Rectangle(51, 108, 51, 32), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p24"),
                                origin, new Rectangle(15, 84, 63, 45), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t0p25"),
                                origin, new Rectangle(0, 77, 39, 67), flip, eng));
                        }
                        break;

                    case 2:

                        img = eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p1");
                        pColl = new Rectangle((int)(position.X - frameSize.X / 2),
                            (int)(position.Y - frameSize.Y / 2), frameSize.X * 2, 75);

                        if (!flip)
                        {
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p2"),
                                origin, new Rectangle(104, 154, 66, 42), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p3"),
                                origin, new Rectangle(63, 159, 58, 39), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p4"),
                                origin, new Rectangle(24, 164, 50, 33), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p5"),
                                origin, new Rectangle(0, 141, 41, 59), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p6"),
                                origin, new Rectangle(22, 136, 48, 39), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p7"),
                                origin, new Rectangle(52, 140, 48, 42), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p8"),
                                origin, new Rectangle(63, 128, 59, 42), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p9"),
                                origin, new Rectangle(112, 116, 55, 53), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p10"),
                                origin, new Rectangle(99, 111, 62, 42), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p11"),
                                origin, new Rectangle(62, 115, 49, 31), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p12"),
                                origin, new Rectangle(31, 114, 49, 37), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p13"),
                                origin, new Rectangle(0, 118, 48, 44), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p14"),
                                origin, new Rectangle(0, 89, 45, 38), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p15"),
                                origin, new Rectangle(26, 91, 47, 43), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p16"),
                                origin, new Rectangle(59, 90, 55, 44), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p17"),
                                origin, new Rectangle(81, 82, 62, 40), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p18"),
                                origin, new Rectangle(130, 77, 38, 50), flip, eng));
                        }
                        else
                        {
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p2"),
                                origin, new Rectangle(0, 154, 66, 42), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p3"),
                                origin, new Rectangle(47, 159, 58, 39), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p4"),
                                origin, new Rectangle(96, 164, 50, 33), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p5"),
                                origin, new Rectangle(129, 141, 41, 59), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p6"),
                                origin, new Rectangle(100, 136, 48, 39), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p7"),
                                origin, new Rectangle(71, 140, 48, 42), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p8"),
                                origin, new Rectangle(47, 128, 59, 42), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p9"),
                                origin, new Rectangle(0, 116, 55, 53), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p10"),
                                origin, new Rectangle(11, 111, 62, 42), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p11"),
                                origin, new Rectangle(62, 115, 49, 31), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p12"),
                                origin, new Rectangle(91, 114, 49, 37), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p13"),
                                origin, new Rectangle(120, 118, 48, 44), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p14"),
                                origin, new Rectangle(125, 89, 45, 38), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p15"),
                                origin, new Rectangle(101, 91, 47, 43), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p16"),
                                origin, new Rectangle(57, 90, 55, 44), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p17"),
                                origin, new Rectangle(25, 82, 62, 40), flip, eng));
                            shards.Add(new Shard(position, speed, frameSize,
                                eng.Content.Load<Texture2D>(@"Blocks/Teeth/t1p18"),
                                origin, new Rectangle(0, 77, 38, 50), flip, eng));
                        }
                        break;
                }
            }

            /**********************/

            public void extraColl(ExtraBallManager xBall, StatsManager stats,
                PlayerManager pM)
            {
                //check collision
                for (int a = 0; a < 2; a++)
                {
                    if (xBall.getBalls()[a].getOn())
                    {
                        if (xBall.getBalls()[a].getCollRect().Intersects(pColl))
                        {
                            if (xBall.getBalls()[a].getBall().getDir().Y < 0)
                            {
                                stats.addPoint(9);
                                xBall.getBalls()[a].setPos(new Vector2(xBall.getBalls()[a].getPos().X,
                                    xBall.getBalls()[a].getPos().Y + 10));
                                xBall.getBalls()[a].bounce(new Vector2(1, -1));
                                xBall.getBalls()[a].setPart(true, new Vector2(1, 2), 0);
                            }
                        }
                    }
                }
            }
        }

        /*****************************************/
        /*****************************************/
        /*****************************************/
        /*****************************************/

        private class Shard : Sprite
        {
            //instance variables
            Texture2D img;
            ToothParticles tP;
            Rectangle collBox;
            Vector2 origin;
            bool on, flip, part, fall, first, sec;
            short c1, c2, rotDir;
            float lay, angle, rotSpe;

            /**********************/

            //constructor
            public Shard(Vector2 pos, Vector2 spe, Point fSize,
                Texture2D imge, Vector2 ori, Rectangle coll, bool f,
                Engine eng)
                : base(pos, spe, fSize)
            {
                on = true;
                origin = ori;
                flip = f;
                img = imge;
                collBox = coll;
                c1 = 4;
                c2 = 0;
                fall = false;
                first = false;
                tP = new ToothParticles();
                tP.Load(eng);
                angle = 0;
                sec = false;
                rotSpe = .1f;
                rotDir = -1;
                speed = new Vector2(0, 4);
                lay = .25f;

            }

            /**********************/

            //update
            public void Update(BallManager bM, StatsManager stats, GameTime gT,
                PlayerManager pM, ToothShards tS, ExtraBallManager xBall, SoundManager sManager)
            {
                if (on)
                {
                    //check collisions with ball
                    extraColl(xBall, tS, sManager);
                    if (bM.getCollRect().Intersects(getColl()))
                    {
                        tS.setFall(true, sManager);
                        setPart();
                        on = false;
                    }

                    //check collisions with player
                    if (pM.getCollRect().Intersects(getColl()) ||
                        pM.getCollRectL().Intersects(getColl()) ||
                        pM.getCollRectR().Intersects(getColl()))
                    {
                        setPart();
                        pM.gotHit();
                        on = false;
                    }

                    //check collisions with bottom of screen
                    if (getColl().Y + collBox.Height / 2 >= 720)
                    {
                        setPart();
                        on = false;
                    }

                    //if initial collision hit, update falling
                    if (fall)
                    {
                        falling(gT);
                    }
                }
                else
                {
                    //update particles
                    if (c1 <= 0)
                    {
                        c1 = 4;
                        part = false;
                    }
                    else if (part)
                    {
                        c1--;
                    }

                    //update particles
                    tP.Update(part, gT);
                }
            }

            /**********************/

            //draw
            public void Draw(SpriteBatch sB)
            {
                if (on)
                {
                    if (flip)
                    {
                        sB.Draw(img, position, null, Color.White,
                            angle, origin, 1f, SpriteEffects.FlipHorizontally, lay);
                    }
                    else
                    {
                        sB.Draw(img, position, null, Color.White,
                            angle, origin, 1f, SpriteEffects.None, lay);
                    }
                }
                else
                {
                    //draw particles
                    tP.Draw(sB);
                }
            }

            /**********************/

            public bool isOn()
            {
                return on;
            }
            public void setFall(short dir, float layer)
            {
                fall = true;
                lay = layer;

                //set initial speed
                switch (dir)
                {
                    case 1:
                        speed = new Vector2(-1.4f, -1);
                        rotDir = 1;
                        break;
                    case 2:
                        speed = new Vector2(-.8f, -1);
                        rotDir = 1;
                        break;
                    case 3:
                        speed = new Vector2(0, -1);
                        rotDir = 0;
                        break;
                    case 4:
                        speed = new Vector2(.8f, -1);
                        rotDir = -1;
                        break;
                    case 5:
                        speed = new Vector2(1.4f, -1);
                        rotDir = -1;
                        break;
                }
            }

            /**********************/

            private void falling(GameTime gT)
            {
                //pop up for 10 frames in a direction
                if (c2 >= 10)
                {
                    c2 = 0;

                    if (!first)
                    {
                        first = true;
                        speed = new Vector2(0, 1);
                    }
                    else
                    {
                        speed.Y += 1.4f;
                    }
                }
                else
                {
                    c2++;
                }

                if (!sec)
                {
                    //rotate
                    angle += (float)gT.ElapsedGameTime.TotalSeconds +
                            rotSpe * rotDir;
                    angle = (angle % (MathHelper.Pi * 2));
                    sec = true;
                }

                //update shard position
                position += speed;
            }

            /**********************/

            private void setPart()
            {
                part = true;
                tP.setPos(new Vector2(getColl().X + collBox.Width / 2, getColl().Y + collBox.Height / 2));
                tP.setNum(new Vector2(1, 1));
            }

            /**********************/

            private Rectangle getColl()
            {
                return new Rectangle((int)(position.X - (frameSize.X / 2) + collBox.X),
                    (int)(position.Y - (frameSize.Y / 2) + collBox.Y),
                    collBox.Width, collBox.Height);
            }

            /**********************/

            public void extraColl(ExtraBallManager xBall, ToothShards tS, SoundManager sManager)
            {
                //check collision
                for (int a = 0; a < 2; a++)
                {
                    if (xBall.getBalls()[a].getOn())
                    {
                        if (xBall.getBalls()[a].getCollRect().Intersects(getColl()))
                        {
                            tS.setFall(true, sManager);
                            setPart();
                            on = false;
                        }
                    }
                }
            }
        }
    }
}
