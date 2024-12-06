/******************************************
 * 05/01/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  Aliens.cs 
 * 
 *****************************************/
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Br8kIn_
{
    class Aliens : Blocks
    {
        //instance variables
        List<Alien> enemies;
        List<Bullet> bullets;
        Random randomNum;
        Engine eng;
        short num, num2, bullF, bF, direction, sC, oC;
        Vector2 speed;
        bool slowD, slowI, replenish, repP;
        int[] s;

        /*****************************************/

        //constructor
        public Aliens(short numb, short dir, Vector2 spe, short buF, bool rep)
        {
            randomNum = new Random();
            num = numb;
            num2 = 0;
            bullF = buF;
            bF = buF;
            slowD = false;
            slowI = false;
            repP = rep;
            direction = dir;
            replenish = rep;
            speed = spe;
            bullets = new List<Bullet>();
            enemies = new List<Alien>();
            s = new int[5];
        }

        /*****************************************/

        //load
        public void Load(Engine engine, List<int> y)
        {
            eng = engine;
            for (int a = 0; a < num; a++)
            {
                if (y[a] == 1)
                {
                    enemies.Add(new Alien(new Vector2(), speed, new Point(40, 30), engine,
                        engine.Content.Load<Texture2D>(@"Blocks/Aliens/a01"), 1));
                }
                else if (y[a] == 2)
                {
                    enemies.Add(new Alien(new Vector2(), speed, new Point(40, 30), engine,
                        engine.Content.Load<Texture2D>(@"Blocks/Aliens/a02"), 2));
                }
                else if (y[a] == 3)
                {
                    enemies.Add(new Alien(new Vector2(), speed, new Point(40, 30), engine,
                        engine.Content.Load<Texture2D>(@"Blocks/Aliens/a03"), 3));
                }
                else
                {
                    enemies.Add(new Alien(new Vector2(), speed, new Point(50, 40), engine,
                        engine.Content.Load<Texture2D>(@"Blocks/Aliens/a04"), 4));
                }
            }
        }
        public void Load(Engine engine) { }

        /*****************************************/

        //update
        public void Update(BallManager bM, StatsManager stats, GameTime gT, 
            PlayerManager pM, SoundManager sManager, ExtraBallManager xBall)
        {
            for (short a = 0; a < num; a++)
            {
                enemies[a].Update(bM, stats, gT, sManager, direction, 
                    slowD, slowI, this, repP, xBall);
            }
            shoot(bM, pM, gT, xBall, sManager);
            changeDir();
            if (replenish)
            {
                replenishEnemies(sManager);
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
                enemies[a].Draw(spriteBatch);
            }
            for (int i = 0; i < num2; i++)
            {
                bullets[i].Draw(spriteBatch);
            }
        }

        /*****************************************/

        public bool isOn()
        {
            bool x = false;
            sC = 0;

            for (int c = 0; c < num; c++)
            {
                if (enemies[c].isOn())
                {
                    s[sC] = c;
                    sC++;
                    x = true;
                }
            }

            return x;
        }

        /*****************************************/

        public void setPositions(List<Vector2> x)
        {
            for (int a = 0; a < num; a++)
            {
                enemies[a].setPos(x[a]);
            }
        }
        public void setPositions(List<Vector2> x, List<Vector2> y) { }

        /*****************************************/

        private void shoot(BallManager bM, PlayerManager pM, GameTime gT,
            ExtraBallManager xBall, SoundManager sManager)
        {
            //at end of bullet frequency
            if (bullF <= 0)
            {
                //reset frequency
                bullF = bF;

                //if aliens still exist in row
                if (sC > 0)
                {
                    //pick an alien
                    int p = randomNum.Next() % sC;
                    bullets.Add(new Bullet(enemies[s[p]].getPos(), new Vector2(0, 3),
                        new Point(20, 20), eng, enemies[s[p]]));
                    num2 += 1;
                    sManager.playSound("zap2");
                }
            }
            else
            {
                bullF--;
            }

            //update bullets
            for (int a = 0; a < num2; a++)
            {
                bullets[a].Update(bM, pM, gT, xBall);
            }
        }

        /*****************************************/

        public void changeDir()
        {
            //if row is moving to the left
            if (direction < 0)
            {
                //if first alien reaches left side of the screen
                if (enemies[0].getPos().X < 80)
                {
                    //trigger slow down
                    slowD = true;

                    if (enemies[0].getPos().X < 23)
                    {
                        //reverse direction
                        slowD = false;
                        slowI = true;
                        direction *= -1;
                    }
                }
            }
            //else row is moving to the right
            else
            {
                //if last alien reaches right side of screen
                if (enemies[4].getPos().X > 1200)
                {
                    //trigger slow down
                    slowD = true;

                    if (enemies[4].getPos().X > 1257)
                    {
                        //reverse direction
                        slowD = false;
                        slowI = true;
                        direction *= -1;
                    }
                }
            }
        }

        public void setSlowI()
        {
            slowI = false;
        }

        private void replenishEnemies(SoundManager sManager)
        {
            //check each alien to see if they are off
            for (int c = 0; c < num; c++)
            {
                //if off, check if ready to replenish
                if (!enemies[c].isOn())
                {
                    if (enemies[c].getRdy())
                    {
                        enemies[c].reset();
                        sManager.playSound("BugUp");
                    }
                }
            }
        }

        /*****************************************/
        /*****************************************/
        /*****************************************/
        /*****************************************/
        /*****************************************/
        /*****************************************/
        /*****************************************/
        /*****************************************/

        private class Alien : Sprite
        {
            //instance variables
            Texture2D img;
            Vector2 origin;
            AlienParticles aP;
            MoveParticles mP;
            ReplenishParticles rP;
            bool on, part, rep, rdy, rPart, repP, coll;
            short slowC, count1, particles, ani, hit;
            Vector2 speT;
            Engine eng;

            /**********************/

            //constructor
            public Alien(Vector2 pos, Vector2 spe, Point fSize, Engine engine,
                Texture2D p, short parts)
                : base(pos, spe, fSize)
            {
                img = p;
                on = true;
                part = false;
                rep = false;
                rdy = false;
                rPart = false;
                slowC = 5;
                ani = 5;
                eng = engine;
                particles = parts;
                count1 = 0;
                speT = spe;
                origin = new Vector2(frameSize.X / 2, frameSize.Y / 2);
                aP = new AlienParticles();
                aP.Load(engine, parts);
                mP = new MoveParticles();
                mP.Load(engine);
                rP = new ReplenishParticles();
                rP.Load(eng);
                coll = false;
                hit = 1;

                if (parts == 4)
                {
                    coll = true;
                }
            }

            /**********************/

            //update
            public void Update(BallManager bManager, StatsManager stats, GameTime gT,
                SoundManager sManager, short dir, bool sD, bool sI, Aliens x, bool repX, 
                ExtraBallManager xBall)
            {
                //move alien
                moveAlien(dir, sD, sI, x);
                repP = repX;

                if (repP)
                {
                    rP.setPos(position);
                    rP.Update(rPart, gT);
                }

                if (rPart)
                {
                    if (ani > 0)
                    {
                        ani--;
                    }
                    else
                    {
                        rPart = false;
                    }
                }

                //if replenish is on
                if (rep)
                {
                    //update replenish timer
                    replenishTimer();
                }

                if (on)
                {
                    mP.setPos(position);
                    mP.Update();

                    //check collisions
                    extraColl(xBall, stats, sManager);
                    if (bManager.getCollRect().Intersects(getTColl()))
                    {
                        if (bManager.getBall().getDir().Y > 0)
                        {
                            bManager.setColl(false);
                            stats.addPoint(7);
                            sManager.playSound("BugDown");
                            xBall.incCombo();

                            if (coll)
                            {
                                bManager.setPos(new Vector2(bManager.getPos().X,
                                    getTColl().Y - 10));
                                bManager.bounce(new Vector2(1, -1));
                                if (hit > 0)
                                {
                                    hit--;
                                }
                                else
                                {
                                    hit = 1;
                                    on = false;
                                    rep = true;
                                    repReset();
                                    setPart();
                                }
                            }
                            else
                            {
                                on = false;
                                rep = true;
                                repReset();
                                setPart();
                            }
                        }
                    }
                    else if (bManager.getCollRect().Intersects(getBColl()))
                    {
                        if (bManager.getBall().getDir().Y < 0)
                        {
                            bManager.setColl(false);
                            stats.addPoint(7);
                            sManager.playSound("BugDown");
                            xBall.incCombo();

                            if (coll)
                            {
                                bManager.setPos(new Vector2(bManager.getPos().X,
                                    getBColl().Y + 40));
                                bManager.bounce(new Vector2(1, -1));
                                if (hit > 0)
                                {
                                    hit--;
                                }
                                else
                                {
                                    hit = 1;
                                    on = false;
                                    rep = true;
                                    repReset();
                                    setPart();
                                    sManager.playSound("BugDown");
                                }
                            }
                            else
                            {
                                on = false;
                                rep = true;
                                repReset();
                                setPart();
                            }
                        }
                    }
                    if (bManager.getCollRect().Intersects(getLColl()))
                    {
                        if (bManager.getBall().getDir().X > 0)
                        {
                            bManager.setColl(false);
                            stats.addPoint(7);
                            sManager.playSound("BugDown");
                            xBall.incCombo();

                            if (coll)
                            {

                                bManager.setPos(new Vector2(getLColl().X - 10,
                                    bManager.getPos().Y));
                                bManager.bounce(new Vector2(-1, 1));
                                if (hit > 0)
                                {
                                    hit--;
                                }
                                else
                                {
                                    hit = 1;
                                    on = false;
                                    rep = true;
                                    repReset();
                                    setPart();
                                    sManager.playSound("BugDown");
                                }
                            }
                            else
                            {
                                on = false;
                                rep = true;
                                repReset();
                                setPart();
                            }
                        }
                    }
                    else if (bManager.getCollRect().Intersects(getRColl()))
                    {
                        if (bManager.getBall().getDir().X < 0)
                        {
                            bManager.setColl(false);
                            stats.addPoint(7);
                            sManager.playSound("BugDown");
                            xBall.incCombo();

                            if (coll)
                            {

                                bManager.setPos(new Vector2(getRColl().X + 30,
                                    bManager.getPos().Y));
                                bManager.bounce(new Vector2(-1, 1));
                                if (hit > 0)
                                {
                                    hit--;
                                }
                                else
                                {
                                    hit = 1;
                                    on = false;
                                    rep = true;
                                    repReset();
                                    setPart();
                                    sManager.playSound("BugDown");
                                }
                            }
                            else
                            {
                                on = false;
                                rep = true;
                                repReset();
                                setPart();
                            }
                        }
                    }
                }
                else
                {
                    //update particles
                    aP.Update(part, gT, position);
                }
            }

            /**********************/

            //draw
            public void Draw(SpriteBatch sB)
            {
                if (on)
                {
                    sB.Draw(img, position, null, Color.White,
                        0, origin, 1f, SpriteEffects.None, .22f);
                    mP.Draw(sB);
                }
                else
                {
                    aP.Draw(sB);
                }

                if (repP)
                {
                    rP.Draw(sB);
                }
            }

            /**********************/

            public Rectangle getLColl()
            {
                return new Rectangle((int)(position.X - frameSize.X / 2),
                    (int)(position.Y - frameSize.Y / 2) + 20, 20, (int)frameSize.Y - 20);
            }
            public Rectangle getRColl()
            {
                return new Rectangle((int)(position.X + (frameSize.X / 2) - 20),
                    (int)(position.Y - frameSize.Y / 2) + 20, 20, (int)frameSize.Y - 20);
            }
            public Rectangle getTColl()
            {
                return new Rectangle((int)(position.X - frameSize.X / 2),
                    (int)(position.Y - frameSize.Y / 2), (int)frameSize.X, 20);
            }
            public Rectangle getBColl()
            {
                return new Rectangle((int)(position.X - frameSize.X / 2),
                    (int)(position.Y + (frameSize.Y / 2) - 20), (int)frameSize.X, 30);
            }

            /**********************/

            private void moveAlien(short dir, bool sD, bool sI, Aliens x)
            {
                //if row is moving left
                if (dir < 0)
                {
                    //if row is slowing down
                    if (sD)
                    {
                        if (slowC <= 0)
                        {
                            slowC = 5;
                            if (speT.X > .18f)
                            {
                                speT.X -= .15f;
                            }
                        }
                        else
                        {
                            slowC--;
                        }
                    }
                    //if row is speeding up
                    else if (sI)
                    {
                        if (slowC <= 0)
                        {
                            slowC = 5;
                            if (speT.X < speed.X)
                            {
                                speT.X += .1f;
                            }
                            else
                            {
                                x.setSlowI();
                            }
                        }
                        else
                        {
                            slowC--;
                        }
                    }
                    else
                    {
                        speT.X = speed.X;
                    }
                }
                //if row is moving right
                else
                {
                    //if row is slowing down
                    if (sD)
                    {
                        if (slowC <= 0)
                        {
                            slowC = 5;
                            if (speT.X > .18f)
                            {
                                speT.X -= .15f;
                            }
                        }
                        else
                        {
                            slowC--;
                        }
                    }
                    //if row is speeding up
                    else if (sI)
                    {
                        if (slowC <= 0)
                        {
                            slowC = 5;
                            if (speT.X < speed.X)
                            {
                                speT.X += .1f;
                            }
                            else
                            {
                                x.setSlowI();
                            }
                        }
                        else
                        {
                            slowC--;
                        }
                    }
                    else
                    {
                        speT.X = speed.X;
                    }
                }

                position.X += speT.X * dir;
            }

            /**********************/

            private void setPart()
            {
                part = true;
                aP.setNum(new Vector2(1, 1));
            }

            /**********************/

            public bool isOn()
            {
                return on;
            }
            public void setSlow()
            {
                slowC = 10;
            }
            public void reset()
            {
                on = true;
                part = false;
                rep = false;
                rdy = false;
                slowC = 5;
                count1 = 0;
                aP = new AlienParticles();
                aP.Load(eng, particles);
                mP = new MoveParticles();
                mP.Load(eng);
                ani = 5;
            }
            public void replenishTimer()
            {
                //if counter is 300
                if (count1 >= 300)
                {
                    rep = false;
                    rdy = true;
                    setRepPart();
                }
                else
                {
                    count1++;
                }
            }
            public void repReset()
            {
                count1 = 0;
                rdy = false;
            }
            public bool getRdy()
            {
                return rdy;
            }
            public void setRepPart()
            {
                rPart = true;
                switch (particles)
                {
                    case 1:
                        rP.setColor(Color.Yellow);
                        break;
                    case 2:
                        rP.setColor(Color.CornflowerBlue);
                        break;
                    case 3:
                        rP.setColor(Color.Red);
                        break;
                    case 4:
                        rP.setColor(Color.Violet);
                        break;
                }
            }

            /**********************/

            public void extraColl(ExtraBallManager xBall, StatsManager stats, SoundManager sManager)
            {
                //check collision
                for (int a = 0; a < 2; a++)
                {
                    if (xBall.getBalls()[a].getOn())
                    {
                        if (xBall.getBalls()[a].getCollRect().Intersects(getTColl()))
                        {
                            if (xBall.getBalls()[a].getBall().getDir().Y > 0)
                            {
                                xBall.getBalls()[a].setColl(false);
                                sManager.playSound("BugDown");
                                stats.addPoint(7);

                                if (coll)
                                {
                                    xBall.getBalls()[a].setPos(new Vector2(xBall.getBalls()[a].getPos().X,
                                        xBall.getBalls()[a].getPos().Y - 10));
                                    xBall.getBalls()[a].bounce(new Vector2(1, -1));
                                    if (hit > 0)
                                    {
                                        hit--;
                                    }
                                    else
                                    {
                                        hit = 1;
                                        on = false;
                                        rep = true;
                                        repReset();
                                        setPart();
                                        sManager.playSound("BugDown");
                                    }
                                }
                                else
                                {
                                    on = false;
                                    rep = true;
                                    repReset();
                                    setPart();
                                }
                            }
                        }
                        else if (xBall.getBalls()[a].getCollRect().Intersects(getBColl()))
                        {
                            if (xBall.getBalls()[a].getBall().getDir().Y < 0)
                            {
                                xBall.getBalls()[a].setColl(false);
                                sManager.playSound("BugDown");
                                stats.addPoint(7);

                                if (coll)
                                {
                                    xBall.getBalls()[a].setPos(new Vector2(xBall.getBalls()[a].getPos().X,
                                        xBall.getBalls()[a].getPos().Y + 10));
                                    xBall.getBalls()[a].bounce(new Vector2(1, -1));
                                    if (hit > 0)
                                    {
                                        hit--;
                                    }
                                    else
                                    {
                                        hit = 1;
                                        on = false;
                                        rep = true;
                                        repReset();
                                        setPart();
                                        sManager.playSound("BugDown");
                                    }
                                }
                                else
                                {
                                    on = false;
                                    rep = true;
                                    repReset();
                                    setPart();
                                }
                            }
                        }
                        if (xBall.getBalls()[a].getCollRect().Intersects(getLColl()))
                        {
                            if (xBall.getBalls()[a].getBall().getDir().X > 0)
                            {
                                xBall.getBalls()[a].setColl(false);
                                sManager.playSound("BugDown");
                                stats.addPoint(7);

                                if (coll)
                                {

                                    xBall.getBalls()[a].setPos(new Vector2(xBall.getBalls()[a].getPos().X - 10,
                                        xBall.getBalls()[a].getPos().Y));
                                    xBall.getBalls()[a].bounce(new Vector2(-1, 1));
                                    if (hit > 0)
                                    {
                                        hit--;
                                    }
                                    else
                                    {
                                        hit = 1;
                                        on = false;
                                        rep = true;
                                        repReset();
                                        setPart();
                                        sManager.playSound("BugDown");
                                    }
                                }
                                else
                                {
                                    on = false;
                                    rep = true;
                                    repReset();
                                    setPart();
                                }
                            }
                        }
                        else if (xBall.getBalls()[a].getCollRect().Intersects(getRColl()))
                        {
                            if (xBall.getBalls()[a].getBall().getDir().X < 0)
                            {
                                xBall.getBalls()[a].setColl(false);
                                sManager.playSound("BugDown");
                                stats.addPoint(7);

                                if (coll)
                                {

                                    xBall.getBalls()[a].setPos(new Vector2(xBall.getBalls()[a].getPos().X + 10,
                                        xBall.getBalls()[a].getPos().Y));
                                    xBall.getBalls()[a].bounce(new Vector2(-1, 1));
                                    if (hit > 0)
                                    {
                                        hit--;
                                    }
                                    else
                                    {
                                        hit = 1;
                                        on = false;
                                        rep = true;
                                        repReset();
                                        setPart();
                                        sManager.playSound("BugDown");
                                    }
                                }
                                else
                                {
                                    on = false;
                                    rep = true;
                                    repReset();
                                    setPart();
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

        private class Bullet : Sprite
        {
            //instance variables
            List<Texture2D> blast;
            Texture2D b;
            Alien alien;
            BulletParticles bP1;
            Vector2 origin1, origin2, pos1, pos2;
            bool on, part1, chargeup, chargedown, shoot;
            short ani, c1, c2;

            /**********************/

            //constructor
            public Bullet(Vector2 pos, Vector2 spe, Point fSize,
                Engine engine, Alien x)
                : base(pos, spe, fSize)
            {
                b = engine.Content.Load<Texture2D>(@"Blocks/Aliens/bullet");
                blast = new List<Texture2D>();
                blast.Add(engine.Content.Load<Texture2D>(@"Blocks/Aliens/b01"));
                blast.Add(engine.Content.Load<Texture2D>(@"Blocks/Aliens/b02"));
                blast.Add(engine.Content.Load<Texture2D>(@"Blocks/Aliens/b03"));
                blast.Add(engine.Content.Load<Texture2D>(@"Blocks/Aliens/b04"));
                blast.Add(engine.Content.Load<Texture2D>(@"Blocks/Aliens/b05"));
                blast.Add(engine.Content.Load<Texture2D>(@"Blocks/Aliens/b06"));
                blast.Add(engine.Content.Load<Texture2D>(@"Blocks/Aliens/b07"));
                blast.Add(engine.Content.Load<Texture2D>(@"Blocks/Aliens/b08"));
                blast.Add(engine.Content.Load<Texture2D>(@"Blocks/Aliens/b09"));
                blast.Add(engine.Content.Load<Texture2D>(@"Blocks/Aliens/b10"));
                bP1 = new BulletParticles();
                alien = x;
                bP1.Load(engine, .3f);
                origin1 = new Vector2(fSize.X / 2, fSize.Y / 2);
                origin2 = new Vector2(b.Width / 2, b.Height / 2);
                on = true;
                chargeup = true;
                chargedown = false;
                shoot = false;
                part1 = false;
                pos1 = new Vector2(pos.X, pos.Y + 20);
                pos2 = pos1;
                ani = 5;
                c1 = 0;
                c2 = 5;
            }

            /**********************/

            //update
            public void Update(BallManager bM, PlayerManager pM, GameTime gT, 
                ExtraBallManager xBall)
            {
                if (on)
                {
                    if (chargeup)
                    {
                        //move charge
                        moveCharge();

                        //update charge
                        if (ani <= 0)
                        {
                            ani = 5;
                            if (c1 > 8)
                            {
                                chargeup = false;
                                chargedown = true;
                                shoot = true;
                                pos2.X = alien.getPos().X;
                                ani = 2;
                            }
                            else
                            {
                                c1++;
                            }
                        }
                        else
                        {
                            ani--;
                        }
                    }
                    else if (chargedown)
                    {
                        //move charge
                        moveCharge();

                        //update charge
                        if (ani <= 0)
                        {
                            ani = 2;
                            if (c1 <= 0)
                            {
                                chargedown = false;
                            }
                            else
                            {
                                c1--;
                            }
                        }
                        else
                        {
                            ani--;
                        }
                    }

                    if (shoot)
                    {
                        //move bullet
                        pos2 += speed;

                        //check for collisions
                        if (pos2.Y >= 720)
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
                                if (pM.getCollRect().Intersects(getColl()) ||
                                    pM.getCollRectL().Intersects(getColl()) ||
                                    pM.getCollRectR().Intersects(getColl()))
                                {
                                    on = false;
                                    pM.gotHit();
                                    setPart();
                                }
                            }
                        }
                    }
                }
                else
                {
                    //update particles
                    if (c2 <= 0)
                    {
                        part1 = false;
                    }
                    else if (part1)
                    {
                        c2--;
                    }

                    //update particles
                    bP1.Update(part1, gT);
                }
            }

            /**********************/

            //draw
            public void Draw(SpriteBatch sB)
            {
                if (on)
                {
                    if (chargeup || chargedown)
                    {
                        sB.Draw(blast[c1], pos1, null, Color.White,
                            0, origin1, 1f, SpriteEffects.None, .23f);
                    }

                    if (shoot)
                    {
                        sB.Draw(b, pos2, null, Color.White,
                            0, origin2, 1f, SpriteEffects.None, .22f);
                    }
                }
                else
                {
                    //draw particles
                    bP1.Draw(sB);
                }
            }

            /**********************/

            private void setPart()
            {
                part1 = true;
                bP1.setPos(new Vector2(pos2.X, pos2.Y));
                bP1.setNum(new Vector2(1, 1));
            }

            /**********************/

            private Rectangle getColl()
            {
                return new Rectangle((int)(pos2.X - b.Width / 2), (int)(pos2.Y - b.Height / 2),
                    b.Width, b.Height);
            }

            /**********************/

            public bool isOn()
            {
                return on;
            }
            public void moveCharge()
            {
                pos1.X = alien.getPos().X;
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
