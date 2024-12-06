/******************************************
 * 03/31/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  Plates.cs 
 * 
 *****************************************/
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Br8kIn_
{
    class Plates : Blocks
    {
        //instance variables
        List<Plate> plate;
        short num;

        /*****************************************/

        //constructor
        public Plates(short numb)
        {
            plate = new List<Plate>();
            num = numb;
        }

        /*****************************************/

        //load
        public void Load(Engine engine, List<int> y)
        {
            for (short a = 0; a < num; a++)
            {
                plate.Add(new Plate(new Vector2(), new Vector2(), new Point(130, 139), engine));
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
                plate[a].Update(bManager, stats, gT, pM, sManager, xBall);
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
                plate[a].Draw(spriteBatch);
            }
        }

        /*****************************************/

        //set positions
        public void setPositions(List<Vector2> x)
        {
            for (int a = 0; a < num; a++)
            {
                plate[a].setPos(x[a]);
            }
        }
        public void setPositions(List<Vector2> x, List<Vector2> y) { }

        /*****************************************/

        public bool isOn()
        {
            bool x = false;

            for (int a = 0; a < num; a++)
            {
                if (plate[a].isOn())
                {
                    x = true;
                }
            }

            return x;
        }

        /*****************************************/
        /*****************************************/

        private class Plate : Sprite
        {
            //instance variables
            Texture2D[] imgs;
            Texture2D[] shadow;
            PlateParticles pG, pY, pP;
            Vector2 origin, origin2;
            short count, num, num2;
            bool on, part;

            /**********************/

            //constructor
            public Plate(Vector2 pos, Vector2 spe, Point fSize, Engine engine)
                : base(pos, spe, fSize)
            {
                imgs = new Texture2D[5];
                shadow = new Texture2D[3];
                origin = new Vector2(fSize.X / 2, frameSize.Y / 2);
                origin2 = new Vector2(139 / 2, 28 / 2); num = 10;
                count = 0;
                on = true;
                num2 = 4;
                part = false;
                pG = new PlateParticles();
                pY = new PlateParticles();
                pP = new PlateParticles();

                imgs[0] = engine.Content.Load<Texture2D>(@"Blocks/Plates/plates01");
                imgs[1] = engine.Content.Load<Texture2D>(@"Blocks/Plates/plates02");
                imgs[2] = engine.Content.Load<Texture2D>(@"Blocks/Plates/plates03");
                imgs[3] = engine.Content.Load<Texture2D>(@"Blocks/Plates/plates04");
                imgs[4] = engine.Content.Load<Texture2D>(@"Blocks/Plates/plates05");
                shadow[0] = engine.Content.Load<Texture2D>(@"Blocks/Plates/shadow01");
                pG.Load(engine.Content.Load<Texture2D>(@"Particles/pg01"),
                    engine.Content.Load<Texture2D>(@"Particles/pg02"),
                    engine.Content.Load<Texture2D>(@"Particles/pg03"),
                    engine.Content.Load<Texture2D>(@"Particles/pg04"),
                    engine.Content.Load<Texture2D>(@"Particles/pg05"),
                    engine.Content.Load<Texture2D>(@"Particles/pg06"));
                pY.Load(engine.Content.Load<Texture2D>(@"Particles/py01"),
                    engine.Content.Load<Texture2D>(@"Particles/py02"),
                    engine.Content.Load<Texture2D>(@"Particles/py03"),
                    engine.Content.Load<Texture2D>(@"Particles/py04"),
                    engine.Content.Load<Texture2D>(@"Particles/py05"),
                    engine.Content.Load<Texture2D>(@"Particles/py06"));
                pP.Load(engine.Content.Load<Texture2D>(@"Particles/pp01"),
                    engine.Content.Load<Texture2D>(@"Particles/pp02"),
                    engine.Content.Load<Texture2D>(@"Particles/pp03"),
                    engine.Content.Load<Texture2D>(@"Particles/pp04"),
                    engine.Content.Load<Texture2D>(@"Particles/pp05"),
                    engine.Content.Load<Texture2D>(@"Particles/pp06"));
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

                    //LEFT
                    if (bManager.getCollRect1().Intersects(getLColl()) ||
                        bManager.getCollRect2().Intersects(getLColl()) ||
                        bManager.getCollRect3().Intersects(getLColl()) ||
                        bManager.getCollRect4().Intersects(getLColl()))
                    {
                        if (bManager.getBall().getDir().X > 0)
                        {
                            sManager.playSound("break");
                            bManager.setColl(false);
                            bManager.setPos(new Vector2(getLColl().X - 10, bManager.getPos().Y));
                            bManager.bounce(new Vector2(-1, 1));
                            xBall.incCombo();

                            if (count <= 3)
                            {
                                count++;
                                stats.addPoint(1);
                                bManager.setPart(true, new Vector2(-.5f, 1), 0);
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
                    //RIGHT
                    else if (bManager.getCollRect1().Intersects(getRColl()) ||
                        bManager.getCollRect2().Intersects(getRColl()) ||
                        bManager.getCollRect3().Intersects(getRColl()) ||
                        bManager.getCollRect4().Intersects(getRColl()))
                    {
                        if (bManager.getBall().getDir().X < 0)
                        {
                            sManager.playSound("break");
                            bManager.setColl(false);
                            bManager.setPos(new Vector2(getRColl().X + 22, bManager.getPos().Y));
                            bManager.bounce(new Vector2(-1, 1));
                            xBall.incCombo();

                            if (count <= 3)
                            {
                                count++;
                                stats.addPoint(1);
                                bManager.setPart(true, new Vector2(2, 1), 0);
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
                    //TOP
                    else if (bManager.getCollRect1().Intersects(getTColl()) ||
                        bManager.getCollRect2().Intersects(getTColl()) ||
                        bManager.getCollRect3().Intersects(getTColl()) ||
                        bManager.getCollRect4().Intersects(getTColl()))
                    {
                        if (bManager.getBall().getDir().Y > 0)
                        {
                            sManager.playSound("break");
                            bManager.setColl(false);
                            bManager.setPos(new Vector2(bManager.getPos().X, getTColl().Y - 10));
                            bManager.bounce(new Vector2(1, -1));
                            xBall.incCombo();

                            if (count <= 3)
                            {
                                count++;
                                stats.addPoint(1);
                                bManager.setPart(true, new Vector2(1, 0), 0);
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
                    //BOTTOM
                    else if (bManager.getCollRect1().Intersects(getBColl()) ||
                        bManager.getCollRect2().Intersects(getBColl()) ||
                        bManager.getCollRect3().Intersects(getBColl()) ||
                        bManager.getCollRect4().Intersects(getBColl()))
                    {
                        if (bManager.getBall().getDir().Y < 0)
                        {
                            sManager.playSound("break");
                            bManager.setColl(false);
                            bManager.setPos(new Vector2(bManager.getPos().X, getBColl().Y + 40));
                            bManager.bounce(new Vector2(1, -1));
                            xBall.incCombo();

                            if (count <= 3)
                            {
                                count++;
                                stats.addPoint(1);
                                bManager.setPart(true, new Vector2(1, 2), 1);
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
                    pG.Update(part, gT);
                    pY.Update(part, gT);
                    pP.Update(part, gT);
                }
            }

            /**********************/

            //draw
            public void Draw(SpriteBatch spriteBatch)
            {
                if (on)
                {
                    //draw plates
                    spriteBatch.Draw(imgs[count], position, null,
                        Color.White, 0, origin, 1f, SpriteEffects.None, .25f);

                    //draw shadow
                    spriteBatch.Draw(shadow[0], new Vector2(position.X, position.Y + frameSize.Y / 2 - num), null,
                        Color.White, 0, origin2, 1f, SpriteEffects.None, .24f);
                }
                else
                {
                    //draw particles
                    pG.Draw(spriteBatch);
                    pY.Draw(spriteBatch);
                    pP.Draw(spriteBatch);
                }
            }

            /**********************/

            //collision rectangle
            public Rectangle getLColl()
            {
                return new Rectangle((int)(position.X - frameSize.X / 2) + 20,
                    (int)(position.Y - frameSize.Y / 2) + 25, 20, (int)frameSize.Y - 50);
            }
            public Rectangle getRColl()
            {
                return new Rectangle((int)(position.X + (frameSize.X / 2) - 31),
                    (int)(position.Y - frameSize.Y / 2) + 25, 12, (int)frameSize.Y - 50);
            }
            public Rectangle getTColl()
            {
                return new Rectangle((int)(position.X - frameSize.X / 2) + 20,
                    (int)(position.Y - frameSize.Y / 2) + 15, (int)frameSize.X - 40, 30);
            }
            public Rectangle getBColl()
            {
                return new Rectangle((int)(position.X - frameSize.X / 2) + 35,
                    (int)(position.Y + (frameSize.Y / 2) - 41), (int)frameSize.X - 70, 30);
            }

            /**********************/

            public void setPart()
            {
                part = true;
                pG.setPos(new Vector2(position.X, position.Y - 40));
                pG.setNum(new Vector2(1, .2f));
                pY.setPos(new Vector2(position.X, position.Y));
                pY.setNum(new Vector2(0, 0));
                pP.setPos(new Vector2(position.X, position.Y + 40));
                pP.setNum(new Vector2(1, 0));
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
                        if (xBall.getBalls()[a].getCollRect1().Intersects(getLColl()) ||
                            xBall.getBalls()[a].getCollRect2().Intersects(getLColl()) ||
                            xBall.getBalls()[a].getCollRect3().Intersects(getLColl()) ||
                            xBall.getBalls()[a].getCollRect4().Intersects(getLColl()))
                        {
                            if (xBall.getBalls()[a].getBall().getDir().X > 0)
                            {
                                sManager.playSound("break");
                                xBall.getBalls()[a].setColl(false);
                                xBall.getBalls()[a].setPos(new Vector2(xBall.getBalls()[a].getPos().X - 10, xBall.getBalls()[a].getPos().Y));
                                xBall.getBalls()[a].bounce(new Vector2(-1, 1));

                                if (count <= 3)
                                {
                                    count++;
                                    stats.addPoint(1);
                                    xBall.getBalls()[a].setPart(true, new Vector2(-.5f, 1), 0);
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
                        else if (xBall.getBalls()[a].getCollRect1().Intersects(getRColl()) ||
                            xBall.getBalls()[a].getCollRect2().Intersects(getRColl()) ||
                            xBall.getBalls()[a].getCollRect3().Intersects(getRColl()) ||
                            xBall.getBalls()[a].getCollRect4().Intersects(getRColl()))
                        {
                            if (xBall.getBalls()[a].getBall().getDir().X < 0)
                            {
                                sManager.playSound("break");
                                xBall.getBalls()[a].setColl(false);
                                xBall.getBalls()[a].setPos(new Vector2(xBall.getBalls()[a].getPos().X + 10,
                                    xBall.getBalls()[a].getPos().Y));
                                xBall.getBalls()[a].bounce(new Vector2(-1, 1));

                                if (count <= 3)
                                {
                                    count++;
                                    stats.addPoint(1);
                                    xBall.getBalls()[a].setPart(true, new Vector2(2, 1), 0);
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
                        else if (xBall.getBalls()[a].getCollRect1().Intersects(getTColl()) ||
                            xBall.getBalls()[a].getCollRect2().Intersects(getTColl()) ||
                            xBall.getBalls()[a].getCollRect3().Intersects(getTColl()) ||
                            xBall.getBalls()[a].getCollRect4().Intersects(getTColl()))
                        {
                            if (xBall.getBalls()[a].getBall().getDir().Y > 0)
                            {
                                sManager.playSound("break");
                                xBall.getBalls()[a].setColl(false);
                                xBall.getBalls()[a].setPos(new Vector2(xBall.getBalls()[a].getPos().X,
                                    xBall.getBalls()[a].getPos().Y - 10));
                                xBall.getBalls()[a].bounce(new Vector2(1, -1));

                                if (count <= 3)
                                {
                                    count++;
                                    stats.addPoint(1);
                                    xBall.getBalls()[a].setPart(true, new Vector2(1, 0), 0);
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
                        else if (xBall.getBalls()[a].getCollRect1().Intersects(getBColl()) ||
                            xBall.getBalls()[a].getCollRect2().Intersects(getBColl()) ||
                            xBall.getBalls()[a].getCollRect3().Intersects(getBColl()) ||
                            xBall.getBalls()[a].getCollRect4().Intersects(getBColl()))
                        {
                            if (xBall.getBalls()[a].getBall().getDir().Y < 0)
                            {
                                sManager.playSound("break");
                                xBall.getBalls()[a].setColl(false);
                                xBall.getBalls()[a].setPos(new Vector2(xBall.getBalls()[a].getPos().X,
                                    xBall.getBalls()[a].getPos().Y + 10));
                                xBall.getBalls()[a].bounce(new Vector2(1, -1));

                                if (count <= 3)
                                {
                                    count++;
                                    stats.addPoint(1);
                                    xBall.getBalls()[a].setPart(true, new Vector2(1, 2), 1);
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
}
