/******************************************
 * 04/07/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  Pots.cs 
 * 
 *****************************************/
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Br8kIn_
{
    class Pots : Blocks
    {
        //instance variables
        List<Pot> pots;
        short num;

        /*****************************************/

        //constructor
        public Pots(short numb)
        {
            pots = new List<Pot>();
            num = numb;
        }

        /*****************************************/

        //load
        public void Load(Engine engine, List<int> y)
        {
            for (int a = 0; a < num; a++)
            {
                if (y[a] == 1)
                {
                    pots.Add(new Pot(new Vector2(), new Vector2(), new Point(75, 117), engine,
                        engine.Content.Load<Texture2D>(@"Blocks/Pots/pot1"), 5));
                }
                else if (y[a] == 2)
                {
                    pots.Add(new Pot(new Vector2(), new Vector2(), new Point(75, 117), engine,
                        engine.Content.Load<Texture2D>(@"Blocks/Pots/pot2"), 4));
                }
                else if (y[a] == 3)
                {
                    pots.Add(new Pot(new Vector2(), new Vector2(), new Point(75, 117), engine,
                        engine.Content.Load<Texture2D>(@"Blocks/Pots/pot3"), 3));
                }
                else
                {
                    pots.Add(new Pot(new Vector2(), new Vector2(), new Point(75, 117), engine,
                        engine.Content.Load<Texture2D>(@"Blocks/Pots/pot4"), 2));
                }
            }
        }
        public void Load(Engine engine) { }

        /*****************************************/

        //update
        public void Update(BallManager bManager, StatsManager stats, GameTime gT, 
            PlayerManager pM, SoundManager sManager, ExtraBallManager xBall)
        {
            for (short a = 0; a < num; a++)
            {
                pots[a].Update(bManager, stats, gT, pM, sManager, xBall);
            }
        }
        public void Update(BallManager bManager, StatsManager stats, GameTime gT, 
            SoundManager sManager, ExtraBallManager xBall) { }

        /*****************************************/

        //draw 
        public void Draw(SpriteBatch spriteBatch)
        {
            for (short a = 0; a < num; a++)
            {
                pots[a].Draw(spriteBatch);
            }
        }

        /*****************************************/

        //set positions
        public void setPositions(List<Vector2> x)
        {
            for (int a = 0; a < num; a++)
            {
                pots[a].setPos(x[a]);
            }
        }
        public void setPositions(List<Vector2> x, List<Vector2> y) { }

        /*****************************************/

        public bool isOn()
        {
            bool x = false;

            for (int a = 0; a < num; a++)
            {
                if (pots[a].isOn())
                {
                    x = true;
                }
            }

            return x;
        }

        /*****************************************/
        /*****************************************/

        private class Pot : Sprite
        {
            //instance variables
            Texture2D[] imgs, cracks;
            Texture2D[] shadow;
            PotParticles p1, p2, p3;
            Vector2 origin, origin2;
            short count, num, num2;
            bool on, part;
            int shard;

            /**********************/

            //constructor
            public Pot(Vector2 pos, Vector2 spe, Point fSize, Engine engine, Texture2D p, int s)
                : base(pos, spe, fSize)
            {
                imgs = new Texture2D[1];
                cracks = new Texture2D[4];
                shadow = new Texture2D[3];
                origin = new Vector2(fSize.X / 2, frameSize.Y / 2);
                origin2 = new Vector2(104 / 2, 23 / 2); num = 2;
                count = -1;
                on = true;
                num2 = 4;
                part = false;
                shard = s;
                p1 = new PotParticles();
                p2 = new PotParticles();
                p3 = new PotParticles();

                imgs[0] = p;
                shadow[0] = engine.Content.Load<Texture2D>(@"Blocks/Pots/shadow02");
                cracks[0] = engine.Content.Load<Texture2D>(@"Blocks/Pots/cracks01");
                cracks[1] = engine.Content.Load<Texture2D>(@"Blocks/Pots/cracks02");
                cracks[2] = engine.Content.Load<Texture2D>(@"Blocks/Pots/cracks03");
                cracks[3] = engine.Content.Load<Texture2D>(@"Blocks/Pots/cracks04");
                loadParticles(engine);
            }

            /**********************/

            //update
            public void Update(BallManager bManager, StatsManager stats, GameTime gT, 
                PlayerManager pM, SoundManager sManager, ExtraBallManager xBall)
            {
                if (on)
                {
                    //check collision
                    extraColl(xBall, stats, pM, sManager);

                    //TOP
                    if (bManager.getCollRect1().Intersects(getTColl()) ||
                        bManager.getCollRect2().Intersects(getTColl()) ||
                        bManager.getCollRect3().Intersects(getTColl()) ||
                        bManager.getCollRect4().Intersects(getTColl()))
                    {
                        sManager.playSound("breakPot");
                        bManager.setColl(false);

                        if (count <= 2)
                        {
                            count++;
                            stats.addPoint(1);
                            if (bManager.getBall().getDir().Y < 0)
                            {
                                sManager.playSound("breakPot");
                                bManager.setPos(new Vector2(bManager.getPos().X, getTColl().Y + 10));
                                bManager.setPart(true, new Vector2(1, 2), shard);
                                bManager.bounce(new Vector2(-1, -1));
                                xBall.incCombo();
                            }
                            else
                            {
                                sManager.playSound("breakPot");
                                bManager.setPos(new Vector2(bManager.getPos().X, getTColl().Y - 10));
                                bManager.setPart(true, new Vector2(1, 0), shard);
                                bManager.bounce(new Vector2(1, -1));
                                xBall.incCombo();
                            }
                        }
                        else
                        {
                            if (bManager.getBall().getDir().Y < 0)
                            {
                                sManager.playSound("breakPot");
                                bManager.setPos(new Vector2(bManager.getPos().X, getTColl().Y + 10));
                                bManager.bounce(new Vector2(-1, -1));
                                xBall.incCombo();
                            }
                            else
                            {
                                sManager.playSound("breakPot");
                                bManager.setPos(new Vector2(bManager.getPos().X, getTColl().Y - 10));
                                bManager.bounce(new Vector2(1, -1));
                                xBall.incCombo();
                            }
                            pM.gotHit();
                            stats.addPoint(2);
                            on = false;
                            setPart();
                        }
                    }
                    //BOTTOM
                    else if (bManager.getCollRect1().Intersects(getBColl()) ||
                        bManager.getCollRect2().Intersects(getBColl()) ||
                        bManager.getCollRect3().Intersects(getBColl()) ||
                        bManager.getCollRect4().Intersects(getBColl()))
                    {
                        if (bManager.getBall().getDir().Y < 0)
                        {
                            sManager.playSound("breakPot");
                            bManager.setColl(false);
                            bManager.setPos(new Vector2(bManager.getPos().X, getBColl().Y + 20));
                            bManager.bounce(new Vector2(1, -1));
                            xBall.incCombo();

                            if (count <= 2)
                            {
                                count++;
                                stats.addPoint(1);
                                bManager.setPart(true, new Vector2(1, 2), shard);
                            }
                            else
                            {
                                on = false;
                                stats.addPoint(2);
                                pM.gotHit();
                                setPart();
                            }
                        }
                    }
                    if (bManager.getCollRect1().Intersects(getm1Coll()) ||
                        bManager.getCollRect2().Intersects(getm1Coll()) ||
                        bManager.getCollRect3().Intersects(getm1Coll()) ||
                        bManager.getCollRect4().Intersects(getm1Coll()))
                    {
                        sManager.playSound("breakPot");
                        bManager.setColl(false);
                        bManager.bounce(new Vector2(-1, 1));
                        xBall.incCombo();

                        if (count <= 2)
                        {
                            count++;
                            stats.addPoint(1);
                            if (bManager.getBall().getDir().X < 0)
                            {
                                sManager.playSound("breakPot");
                                bManager.setPart(true, new Vector2(-.5f, 1), shard);
                                bManager.setPos(new Vector2(bManager.getPos().X + 10, bManager.getPos().Y));
                            }
                            else
                            {
                                sManager.playSound("breakPot");
                                bManager.setPart(true, new Vector2(2, 1), shard);
                                bManager.setPos(new Vector2(bManager.getPos().X - 10, bManager.getPos().Y));
                            }
                        }
                        else
                        {
                            on = false;
                            stats.addPoint(2);
                            pM.gotHit();
                            setPart();
                        }
                    }
                    else if (bManager.getCollRect1().Intersects(getm2Coll()) ||
                        bManager.getCollRect2().Intersects(getm2Coll()) ||
                        bManager.getCollRect3().Intersects(getm2Coll()) ||
                        bManager.getCollRect4().Intersects(getm2Coll()))
                    {
                        sManager.playSound("breakPot");
                        bManager.setColl(false);
                        bManager.bounce(new Vector2(-1, 1));
                        xBall.incCombo();

                        if (count <= 2)
                        {
                            count++;
                            stats.addPoint(1);
                            sManager.playSound("breakPot");
                            if (bManager.getBall().getDir().X < 0)
                            {
                                bManager.setPart(true, new Vector2(-.5f, 1), shard);
                                bManager.setPos(new Vector2(bManager.getPos().X + 10, bManager.getPos().Y));
                            }
                            else
                            {

                                bManager.setPart(true, new Vector2(2, 1), shard);
                                bManager.setPos(new Vector2(bManager.getPos().X - 10, bManager.getPos().Y));
                            }
                        }
                        else
                        {
                            on = false;
                            stats.addPoint(2);
                            pM.gotHit();
                            setPart();
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
                    p1.Update(part, gT);
                    p2.Update(part, gT);
                    p3.Update(part, gT);
                }
            }

            /**********************/

            //draw
            public void Draw(SpriteBatch spriteBatch)
            {
                if (on)
                {
                    //draw pot
                    spriteBatch.Draw(imgs[0], position, null,
                        Color.White, 0, origin, 1f, SpriteEffects.None, .25f);

                    if (count >= 0)
                    {
                        //draw cracks
                        spriteBatch.Draw(cracks[count], position, null,
                            Color.White, 0, origin, 1f, SpriteEffects.None, .255f);
                    }

                    //draw shadow
                    spriteBatch.Draw(shadow[0], new Vector2(position.X, position.Y + frameSize.Y / 2 - num), null,
                        Color.White, 0, origin2, 1f, SpriteEffects.None, .24f);
                }
                else
                {
                    //draw particles
                    p1.Draw(spriteBatch);
                    p2.Draw(spriteBatch);
                    p3.Draw(spriteBatch);
                }
            }

            /**********************/

            //collision rectangles
            public Rectangle getm1Coll()
            {
                return new Rectangle((int)(position.X - frameSize.X / 2) + 26,
                    (int)(position.Y - frameSize.Y / 2) + 1, 25, 54);
            }
            public Rectangle getm2Coll()
            {
                return new Rectangle((int)(position.X - frameSize.X / 2) + 5,
                    (int)(position.Y - frameSize.Y / 2) + 49, 64, 59);
            }
            public Rectangle getTColl()
            {
                return new Rectangle((int)(position.X - frameSize.X / 2) + 13,
                    (int)(position.Y - frameSize.Y / 2) + 1, 50, 10);
            }
            public Rectangle getBColl()
            {
                return new Rectangle((int)(position.X - frameSize.X / 2) + 14,
                    (int)(position.Y + (frameSize.Y / 2) - 12), 48, 10);
            }

            /**********************/

            public void setPart()
            {
                part = true;
                p1.setPos(new Vector2(position.X, position.Y - 40));
                p1.setNum(new Vector2(1, .2f));
                p2.setPos(new Vector2(position.X, position.Y));
                p2.setNum(new Vector2(0, 0));
                p3.setPos(new Vector2(position.X, position.Y + 40));
                p3.setNum(new Vector2(1, 0));
            }
            private void loadParticles(Engine engine)
            {
                //yellow
                if (shard == 2)
                {
                    p1.Load(engine.Content.Load<Texture2D>(@"Particles/p00"),
                        engine.Content.Load<Texture2D>(@"Particles/pYY01"),
                        engine.Content.Load<Texture2D>(@"Particles/p00"),
                        engine.Content.Load<Texture2D>(@"Particles/pYY03"),
                        engine.Content.Load<Texture2D>(@"Particles/p00"),
                        engine.Content.Load<Texture2D>(@"Particles/pYY04"));
                    p2.Load(engine.Content.Load<Texture2D>(@"Particles/pYY03"),
                        engine.Content.Load<Texture2D>(@"Particles/pYY06"),
                        engine.Content.Load<Texture2D>(@"Particles/pYY02"),
                        engine.Content.Load<Texture2D>(@"Particles/pYY06"),
                        engine.Content.Load<Texture2D>(@"Particles/pYY05"),
                        engine.Content.Load<Texture2D>(@"Particles/pYY06"));
                    p3.Load(engine.Content.Load<Texture2D>(@"Particles/pYY01"),
                        engine.Content.Load<Texture2D>(@"Particles/pYY02"),
                        engine.Content.Load<Texture2D>(@"Particles/pYY03"),
                        engine.Content.Load<Texture2D>(@"Particles/pYY04"),
                        engine.Content.Load<Texture2D>(@"Particles/pYY05"),
                        engine.Content.Load<Texture2D>(@"Particles/pYY07"));
                }
                //green
                else if (shard == 3)
                {
                    p1.Load(engine.Content.Load<Texture2D>(@"Particles/p00"),
                        engine.Content.Load<Texture2D>(@"Particles/pGG01"),
                        engine.Content.Load<Texture2D>(@"Particles/p00"),
                        engine.Content.Load<Texture2D>(@"Particles/pGG03"),
                        engine.Content.Load<Texture2D>(@"Particles/p00"),
                        engine.Content.Load<Texture2D>(@"Particles/pGG04"));
                    p2.Load(engine.Content.Load<Texture2D>(@"Particles/pGG03"),
                        engine.Content.Load<Texture2D>(@"Particles/pGG06"),
                        engine.Content.Load<Texture2D>(@"Particles/pGG02"),
                        engine.Content.Load<Texture2D>(@"Particles/pGG06"),
                        engine.Content.Load<Texture2D>(@"Particles/pGG05"),
                        engine.Content.Load<Texture2D>(@"Particles/pGG06"));
                    p3.Load(engine.Content.Load<Texture2D>(@"Particles/pGG01"),
                        engine.Content.Load<Texture2D>(@"Particles/pGG02"),
                        engine.Content.Load<Texture2D>(@"Particles/pGG03"),
                        engine.Content.Load<Texture2D>(@"Particles/pGG04"),
                        engine.Content.Load<Texture2D>(@"Particles/pGG05"),
                        engine.Content.Load<Texture2D>(@"Particles/pGG07"));
                }
                //blue
                else if (shard == 4)
                {
                    p1.Load(engine.Content.Load<Texture2D>(@"Particles/p00"),
                        engine.Content.Load<Texture2D>(@"Particles/pB01"),
                        engine.Content.Load<Texture2D>(@"Particles/p00"),
                        engine.Content.Load<Texture2D>(@"Particles/pB03"),
                        engine.Content.Load<Texture2D>(@"Particles/p00"),
                        engine.Content.Load<Texture2D>(@"Particles/pB04"));
                    p2.Load(engine.Content.Load<Texture2D>(@"Particles/pB03"),
                        engine.Content.Load<Texture2D>(@"Particles/pB06"),
                        engine.Content.Load<Texture2D>(@"Particles/pB02"),
                        engine.Content.Load<Texture2D>(@"Particles/pB06"),
                        engine.Content.Load<Texture2D>(@"Particles/pB05"),
                        engine.Content.Load<Texture2D>(@"Particles/pB06"));
                    p3.Load(engine.Content.Load<Texture2D>(@"Particles/pB01"),
                        engine.Content.Load<Texture2D>(@"Particles/pB02"),
                        engine.Content.Load<Texture2D>(@"Particles/pB03"),
                        engine.Content.Load<Texture2D>(@"Particles/pB04"),
                        engine.Content.Load<Texture2D>(@"Particles/pB05"),
                        engine.Content.Load<Texture2D>(@"Particles/pB07"));
                }
                //red
                else
                {
                    p1.Load(engine.Content.Load<Texture2D>(@"Particles/p00"),
                        engine.Content.Load<Texture2D>(@"Particles/pR01"),
                        engine.Content.Load<Texture2D>(@"Particles/p00"),
                        engine.Content.Load<Texture2D>(@"Particles/pR03"),
                        engine.Content.Load<Texture2D>(@"Particles/p00"),
                        engine.Content.Load<Texture2D>(@"Particles/pR04"));
                    p2.Load(engine.Content.Load<Texture2D>(@"Particles/pR03"),
                        engine.Content.Load<Texture2D>(@"Particles/pR06"),
                        engine.Content.Load<Texture2D>(@"Particles/pR02"),
                        engine.Content.Load<Texture2D>(@"Particles/pR06"),
                        engine.Content.Load<Texture2D>(@"Particles/pR05"),
                        engine.Content.Load<Texture2D>(@"Particles/pR06"));
                    p3.Load(engine.Content.Load<Texture2D>(@"Particles/pR01"),
                        engine.Content.Load<Texture2D>(@"Particles/pR02"),
                        engine.Content.Load<Texture2D>(@"Particles/pR03"),
                        engine.Content.Load<Texture2D>(@"Particles/pR04"),
                        engine.Content.Load<Texture2D>(@"Particles/pR05"),
                        engine.Content.Load<Texture2D>(@"Particles/pR07"));
                }
            }

            /**********************/

            public bool isOn()
            {
                return on;
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
                        //check collision
                        if (xBall.getBalls()[a].getCollRect1().Intersects(getTColl()) ||
                            xBall.getBalls()[a].getCollRect2().Intersects(getTColl()) ||
                            xBall.getBalls()[a].getCollRect3().Intersects(getTColl()) ||
                            xBall.getBalls()[a].getCollRect4().Intersects(getTColl()))
                        {
                            sManager.playSound("breakPot");
                            xBall.getBalls()[a].setColl(false);

                            if (count <= 2)
                            {
                                count++;
                                stats.addPoint(1);
                                if (xBall.getBalls()[a].getBall().getDir().Y < 0)
                                {
                                    sManager.playSound("breakPot");
                                    xBall.getBalls()[a].setPos(new Vector2(xBall.getBalls()[a].getPos().X,
                                        xBall.getBalls()[a].getPos().Y + 10));
                                    xBall.getBalls()[a].setPart(true, new Vector2(1, 2), shard);
                                    xBall.getBalls()[a].bounce(new Vector2(-1, -1));
                                }
                                else
                                {
                                    sManager.playSound("breakPot");
                                    xBall.getBalls()[a].setPos(new Vector2(xBall.getBalls()[a].getPos().X,
                                        xBall.getBalls()[a].getPos().Y - 10));
                                    xBall.getBalls()[a].setPart(true, new Vector2(1, 0), shard);
                                    xBall.getBalls()[a].bounce(new Vector2(1, -1));
                                }
                            }
                            else
                            {
                                if (xBall.getBalls()[a].getBall().getDir().Y < 0)
                                {
                                    sManager.playSound("breakPot");
                                    xBall.getBalls()[a].setPos(new Vector2(xBall.getBalls()[a].getPos().X,
                                        xBall.getBalls()[a].getPos().Y + 10));
                                    xBall.getBalls()[a].bounce(new Vector2(-1, -1));
                                }
                                else
                                {
                                    sManager.playSound("breakPot");
                                    xBall.getBalls()[a].setPos(new Vector2(xBall.getBalls()[a].getPos().X,
                                        xBall.getBalls()[a].getPos().Y - 10));
                                    xBall.getBalls()[a].bounce(new Vector2(1, -1));
                                }
                                pM.gotHit();
                                stats.addPoint(2);
                                on = false;
                                setPart();
                            }
                        }
                        else if (xBall.getBalls()[a].getCollRect1().Intersects(getBColl()) ||
                            xBall.getBalls()[a].getCollRect2().Intersects(getBColl()) ||
                            xBall.getBalls()[a].getCollRect3().Intersects(getBColl()) ||
                            xBall.getBalls()[a].getCollRect4().Intersects(getBColl()))
                        {
                            if (xBall.getBalls()[a].getBall().getDir().Y < 0)
                            {
                                sManager.playSound("breakPot");
                                xBall.getBalls()[a].setColl(false);
                                xBall.getBalls()[a].setPos(new Vector2(xBall.getBalls()[a].getPos().X,
                                    xBall.getBalls()[a].getPos().Y + 10));
                                xBall.getBalls()[a].bounce(new Vector2(1, -1));

                                if (count <= 2)
                                {
                                    count++;
                                    stats.addPoint(1);
                                    xBall.getBalls()[a].setPart(true, new Vector2(1, 2), shard);
                                }
                                else
                                {
                                    on = false;
                                    stats.addPoint(2);
                                    pM.gotHit();
                                    setPart();
                                }
                            }
                        }
                        if (xBall.getBalls()[a].getCollRect1().Intersects(getm1Coll()) ||
                            xBall.getBalls()[a].getCollRect2().Intersects(getm1Coll()) ||
                            xBall.getBalls()[a].getCollRect3().Intersects(getm1Coll()) ||
                            xBall.getBalls()[a].getCollRect4().Intersects(getm1Coll()))
                        {
                            sManager.playSound("breakPot");
                            xBall.getBalls()[a].setColl(false);
                            xBall.getBalls()[a].bounce(new Vector2(-1, 1));

                            if (count <= 2)
                            {
                                count++;
                                stats.addPoint(1);
                                if (xBall.getBalls()[a].getBall().getDir().X < 0)
                                {
                                    sManager.playSound("breakPot");
                                    xBall.getBalls()[a].setPart(true, new Vector2(-.5f, 1), shard);
                                    xBall.getBalls()[a].setPos(new Vector2(xBall.getBalls()[a].getPos().X + 10,
                                        xBall.getBalls()[a].getPos().Y));
                                }
                                else
                                {
                                    sManager.playSound("breakPot");
                                    xBall.getBalls()[a].setPart(true, new Vector2(2, 1), shard);
                                    xBall.getBalls()[a].setPos(new Vector2(xBall.getBalls()[a].getPos().X - 10,
                                        xBall.getBalls()[a].getPos().Y));
                                }
                            }
                            else
                            {
                                on = false;
                                stats.addPoint(2);
                                pM.gotHit();
                                setPart();
                            }
                        }
                        else if (xBall.getBalls()[a].getCollRect1().Intersects(getm2Coll()) ||
                            xBall.getBalls()[a].getCollRect2().Intersects(getm2Coll()) ||
                            xBall.getBalls()[a].getCollRect3().Intersects(getm2Coll()) ||
                            xBall.getBalls()[a].getCollRect4().Intersects(getm2Coll()))
                        {
                            sManager.playSound("breakPot");
                            xBall.getBalls()[a].setColl(false);
                            xBall.getBalls()[a].bounce(new Vector2(-1, 1));

                            if (count <= 2)
                            {
                                count++;
                                stats.addPoint(1);
                                sManager.playSound("breakPot");
                                if (xBall.getBalls()[a].getBall().getDir().X < 0)
                                {
                                    xBall.getBalls()[a].setPart(true, new Vector2(-.5f, 1), shard);
                                    xBall.getBalls()[a].setPos(new Vector2(xBall.getBalls()[a].getPos().X + 10,
                                        xBall.getBalls()[a].getPos().Y));
                                }
                                else
                                {

                                    xBall.getBalls()[a].setPart(true, new Vector2(2, 1), shard);
                                    xBall.getBalls()[a].setPos(new Vector2(xBall.getBalls()[a].getPos().X - 10,
                                        xBall.getBalls()[a].getPos().Y));
                                }
                            }
                            else
                            {
                                on = false;
                                stats.addPoint(2);
                                pM.gotHit();
                                setPart();
                            }
                        }
                    }
                }
            }
        }
    }
}
