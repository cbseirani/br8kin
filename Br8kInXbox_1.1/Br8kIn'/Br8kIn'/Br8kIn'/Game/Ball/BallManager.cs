/******************************************
 * 03/20/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  BallManager.cs 
 * 
 *****************************************/
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Br8kIn_
{
    class BallManager
    {
        //instance variables
        Ball ball;
        Engine eng;
        BallParticles ballP, trail;
        short count, c2, c3, c4;
        float speChange;
        int minSpeed, maxSpeed, levelNum;
        Random random;
        bool part, type, extra, first, collOn, on, bottomOn;

        /*****************************************/

        //constructor
        public BallManager(bool x)
        {
            ball = new Ball(new Vector2(0, 0), new Vector2(0, 0), new Point(0, 0), x);
            ballP = new BallParticles();
            trail = new BallParticles();
            collOn = true;
            count = 5;
            on = true;
            first = true;
            random = new Random();
            speChange = 1f;
            minSpeed = 1;
            maxSpeed = 4;
            part = false;
            c2 = 4;
            c3 = 4;
            c4 = 160;
            type = x;
            extra = false;
            bottomOn = false;
        }

        /*****************************************/

        //load
        public void Load(Engine engine, int level)
        {
            eng = engine;
            ball.Load(engine, level);
            ballP.Load(engine, level, type);
            trail.Load(engine, level, type);
            setTrail(true, new Vector2(1, 1), false);
            trail.trailEnd();
            ballP.trailEnd();
            collOn = true;
            bottomOn = false;
            on = true;
            c3 = 4;
            c4 = 160;
            if (levelNum == 4 || levelNum == 5 || levelNum == 6 ||
                levelNum == 14 || levelNum == 15 || levelNum == 16)
            {
                count = 10;
            }
            else
            {
                count = 5;
            }
            levelNum = level;
            first = true;
            part = false;
            minSpeed = 1;
        }

        /*****************************************/

        //update
        public void Update(GameTime gameTime, bool title, Rectangle clientBounds,
            PlayerManager player, SoundManager sManager, bool bottomOn, StatsManager stats,
            GamePadState g, bool paddleControl, bool ballControl)
        {
            if (on)
            {
                setTrail(true, new Vector2(1, 1), true);

                if (count == 0 && !collOn)
                {
                    collOn = true;
                    if (levelNum == 4 || levelNum == 5 || levelNum == 6 ||
                        levelNum == 14 || levelNum == 15 || levelNum == 16)
                    {
                        count = 10;
                    }
                    else
                    {
                        count = 5;
                    }
                }
                else if (!collOn)
                {
                    count--;
                }

                if (part)
                {
                    c2--;
                    if (c2 <= 0)
                    {
                        c2 = 4;
                        part = false;
                    }
                }

                //update ball
                ball.Update(gameTime, g, extra);

                //check collisions with walls
                collWall(clientBounds);

                if (player.getOn())
                {
                    if (!paddleControl || extra)
                    {
                        //check collisions with player
                        collPlayer(player, sManager);
                    }
                    else
                    {
                        collPlayer2(player, sManager);
                    }

                    if (!extra && ballControl)
                    {
                        if (player.getHookR() == true)
                        {
                            ball.setDir(new Vector2((ball.getDir().X + .005f), ball.getDir().Y));
                        }
                        else
                            ball.setDir(new Vector2((ball.getDir().X), ball.getDir().Y));

                        if (player.getHookL() == true)
                        {
                            ball.setDir(new Vector2((ball.getDir().X - .005f), ball.getDir().Y));
                        }
                        else
                            ball.setDir(new Vector2((ball.getDir().X), ball.getDir().Y));
                    }
                }

                //check collisions with pit
                collPit(title, clientBounds, stats, bottomOn, player);

                //update ball particles
                ballP.Update(part);
                trail.Update(true);
            }
            else
            {
                if (c4 > 0)
                {
                    //update particles
                    if (c3 <= 0)
                    {
                        c3 = 4;
                        part = false;
                    }
                    else if (part)
                    {
                        c3--;
                    }
                    c4--;
                    ballP.Update(part);
                }
                else
                {
                    if (!extra)
                    {
                        player.playerReset();
                        newBall();
                        trail.trailEnd();
                    }
                }
                setTrail(false, new Vector2(1, 1), false);
            }
        }

        /*****************************************/

        //draw
        public void Draw(SpriteBatch spriteBatch)
        {
            if (on)
            {
                ball.Draw(spriteBatch);
                if (!extra)
                {
                    trail.Draw(spriteBatch);
                }
            }
            ballP.Draw(spriteBatch);
        }

        /*****************************************/

        //method to check collisions with walls
        private void collWall(Rectangle clientBounds)
        {
            //IF ball hits left wall
            if (ball.getPos().X <= 10)
            {
                //repel ball in different x direciton, same y direction
                ball.setPos(new Vector2(0 + 10, ball.getPos().Y));
                ball.setDir(new Vector2(ball.getDir().X * -1f, ball.getDir().Y));
                ball.toggRotDir();
            }
            //IF ball hits right wall
            if (ball.getPos().X >= 1280 - 10)
            {
                //repel ball in different x direciton, same y direction
                ball.setPos(new Vector2(1280 - 10, ball.getPos().Y));
                ball.setDir(new Vector2(ball.getDir().X * -1f, ball.getDir().Y));
                ball.toggRotDir();
            }
            //IF ball hits top wall
            if (ball.getPos().Y <= 10)
            {
                //repel ball in same x direciton, different y direction
                ball.setPos(new Vector2(ball.getPos().X, 0 + 10));
                ball.setDir(new Vector2(ball.getDir().X, ball.getDir().Y * -1f));
                ball.toggRotDir();
            }
        }

        //method to check collisions with player
        private void collPlayer(PlayerManager player, SoundManager sManager)
        {
            if (ball.getPos().Y < 705)
            {
                //IF ball hits left side of player
                if (getCollRect().Intersects(player.getCollRectL()))
                {
                    player.vibeOnL();
                    if (player.getBallDes())
                    {
                        //player.iniBallDes();
                    }
                    else
                    {
                        if (minSpeed < maxSpeed)
                        {
                            speChange = 1.2f;
                            minSpeed++;
                        }
                        else
                        {
                            speChange = 1f;
                        }
                        ball.setDir(new Vector2(ball.getDir().X * -1, ball.getDir().Y * -speChange));
                        setPos(new Vector2(getPos().X, player.getMainColl()[0].Y - 5));
                        ball.toggRotDir();
                        setColl(false);
                        sManager.playSound("pop");
                        checkFirst();
                        if (player.getBallDesM())
                        {
                            player.iniBallDes();
                        }
                    }
                }
                //ELSE IF ball hits right side of player
                if (getCollRect().Intersects(player.getCollRectR()))
                {
                    player.vibeOnR();
                    if (player.getBallDes())
                    {
                        //player.iniBallDes();
                    }
                    else
                    {
                        if (minSpeed < maxSpeed)
                        {
                            speChange = 1.2f;
                            minSpeed++;
                        }
                        else
                        {
                            speChange = 1f;
                        }
                        ball.setDir(new Vector2(ball.getDir().X * -1, ball.getDir().Y * -speChange));
                        setPos(new Vector2(getPos().X, player.getMainColl()[0].Y - 5));
                        ball.toggRotDir();
                        setColl(false);
                        sManager.playSound("pop");
                        checkFirst();
                        if (player.getBallDesM())
                        {
                            player.iniBallDes();
                        }
                    }
                }
                //ELSE IF ball hits center of player
                if (getCollRect().Intersects(player.getCollRect()))
                {
                    player.vibeOnC();
                    if (player.getBallDes())
                    {
                        //player.iniBallDes();
                    }
                    else
                    {
                        //repel ball in same x direciton, different y direction
                        ball.setDir(new Vector2(ball.getDir().X, ball.getDir().Y * -1f));
                        setPos(new Vector2(getPos().X, player.getMainColl()[1].Y - 5));
                        ball.toggRotDir();
                        setColl(false);
                        sManager.playSound("pop");
                        checkFirst();
                        if (player.getBallDesM())
                        {
                            player.iniBallDes();
                        }
                    }
                }
            }
        }
        private void collPlayer2(PlayerManager player, SoundManager sManager)
        {
            //IF main ball collides with left corner
            if (getCollRect().Intersects(player.getMainColl()[3]))
            {
                player.vibeOnL();
                sManager.playSound("pop");
                setColl(false);
                ball.toggRotDir();
                checkFirst2();
                if (player.getBallDesM())
                {
                    player.iniBallDes();
                }

                //change x direction
                ball.setDir(new Vector2(ball.getDir().X * -1, ball.getDir().Y * -1));
                setPos(new Vector2(getPos().X, player.getMainColl()[0].Y - 5));
            }
            //ELSE IF main ball collides with right corner
            else if (getCollRect().Intersects(player.getMainColl()[4]))
            {
                player.vibeOnR();
                sManager.playSound("pop");
                setColl(false);
                ball.toggRotDir();
                checkFirst2();
                if (player.getBallDesM())
                {
                    player.iniBallDes();
                }

                //change x direction
                ball.setDir(new Vector2(ball.getDir().X * -1, ball.getDir().Y * -1));
                setPos(new Vector2(getPos().X, player.getMainColl()[0].Y - 5));
            }
            //ELSE IF main ball collides with left side of player
            else if (getCollRect().Intersects(player.getMainColl()[0]))
            {
                player.vibeOnL();
                sManager.playSound("pop");
                setColl(false);
                ball.toggRotDir();
                checkFirst2();
                if (player.getBallDesM())
                {
                    player.iniBallDes();
                }

                //set direction more left
                if (ball.getDir().X > -1.6f)
                {
                    ball.setDir(new Vector2(ball.getDir().X - 0.3f, ball.getDir().Y * -1));
                }
                else
                {
                    ball.setDir(new Vector2(ball.getDir().X, ball.getDir().Y * -1));
                }
                setPos(new Vector2(getPos().X, player.getMainColl()[0].Y - 5));
            }
            //ELSE IF main ball collides with the middle of player
            else if (getCollRect().Intersects(player.getMainColl()[1]))
            {
                player.vibeOnC();
                sManager.playSound("pop");
                setColl(false);
                ball.toggRotDir();
                checkFirst2();
                if (player.getBallDesM())
                {
                    player.iniBallDes();
                }

                //set direction more opposite of current direction
                if ((ball.getDir().X > -1.6f) && (ball.getDir().X < 1.6f))
                {
                    if (ball.getDir().X < 0)
                    {
                        ball.setDir(new Vector2(ball.getDir().X + 0.3f, ball.getDir().Y * -1));
                    }
                    else
                    {
                        ball.setDir(new Vector2(ball.getDir().X - 0.3f, ball.getDir().Y * -1));
                    }
                }
                else
                {
                    ball.setDir(new Vector2(ball.getDir().X, ball.getDir().Y * -1));
                }
                setPos(new Vector2(getPos().X, player.getMainColl()[1].Y - 5));
            }
            //ELSE IF main ball collides with the right side of player
            else if (getCollRect().Intersects(player.getMainColl()[2]))
            {
                player.vibeOnR();
                sManager.playSound("pop");
                setColl(false);
                ball.toggRotDir();
                checkFirst2();
                if (player.getBallDesM())
                {
                    player.iniBallDes();
                }

                //set direction more right
                if (ball.getDir().X < 1.6f)
                {
                    ball.setDir(new Vector2(ball.getDir().X + 0.3f, ball.getDir().Y * -1));
                }
                else
                {
                    ball.setDir(new Vector2(ball.getDir().X, ball.getDir().Y * -1));
                }
                setPos(new Vector2(getPos().X, player.getMainColl()[2].Y - 5));
            }
        }

        //method to check collisions with pit
        private void collPit(bool title, Rectangle clientBounds, StatsManager stats,
            bool bottomOnE, PlayerManager pM)
        {
            //IF ball hits bottom of the screen
            if (ball.getPos().Y >= 710)
            {
                //IF user is at title screen
                if (title || extra)
                {
                    //repel ball in same x direciton, different y direction
                    ball.setDir(new Vector2(ball.getDir().X, ball.getDir().Y * -1f));
                    ball.toggRotDir();
                    checkFirst();
                }
                //ELSE user is in game
                else
                {
                    //player loses a ball and continues with a new ball
                    if (bottomOnE && !bottomOn)
                    {
                        if (!extra)
                        {
                            stats.delLife();
                            pM.freeze();
                        }
                        on = false;
                        setPart(true, new Vector2(1, 1), true);
                    }
                    else
                    {
                        ball.setDir(new Vector2(ball.getDir().X, ball.getDir().Y * -1f));
                        ball.toggRotDir();
                        checkFirst();
                    }
                }
            }
        }

        /*****************************************/

        //method to return ball's collision rectangle
        public Rectangle getCollRect()
        {
            return ball.getCollRect(collOn);
        }
        public Rectangle getCollRect1()
        {
            return ball.getCollRect1(collOn);
        }
        public Rectangle getCollRect2()
        {
            return ball.getCollRect2(collOn);
        }
        public Rectangle getCollRect3()
        {
            return ball.getCollRect3(collOn);
        }
        public Rectangle getCollRect4()
        {
            return ball.getCollRect4(collOn);
        }
        public Rectangle getCollRect5()
        {
            return ball.getCollRect5(collOn);
        }

        /*****************************************/

        public Ball getBall()
        {
            return ball;
        }
        public void setColl(bool x)
        {
            collOn = x;
        }
        public void setPart(bool x, Vector2 y, int z)
        {
            part = x;
            ballP.setPos(ball.getPos());
            ballP.setNum(y, z);
        }
        public void setPart(bool x, Vector2 y, bool a)
        {
            part = x;
            ballP.setPos(ball.getPos());
            ballP.setNum(y, a);
        }
        public void setTrail(bool x, Vector2 y, bool a)
        {
            trail.setPos(ball.getPos());
            trail.setTrail(y, a);
        }

        /*****************************************/

        private void checkFirst()
        {
            if (first)
            {
                //set random x direction
                switch (random.Next() % 2)
                {
                    case 0:
                        ball.setDir(new Vector2(1, -1));
                        break;
                    case 1:
                        ball.setDir(new Vector2(-1, -1));
                        break;
                }

                ball.setRotSpe();
                if (!extra)
                {
                    ball.setSpeed(new Vector2(6.5f, 6.5f));
                }
                else
                {
                    ball.setSpeed(new Vector2(4, 4));
                }
                first = false;
            }
        }
        private void checkFirst2()
        {
            if (first)
            {
                //set random x direction
                switch (random.Next() % 2)
                {
                    case 0:
                        ball.setDir(new Vector2(1, 1));
                        break;
                    case 1:
                        ball.setDir(new Vector2(-1, 1));
                        break;
                }

                ball.setRotSpe();
                if (!extra)
                {
                    ball.setSpeed(new Vector2(6.5f, 6.5f));
                }
                else
                {
                    ball.setSpeed(new Vector2(4, 4));
                }
                first = false;
            }
        }

        /*****************************************/

        public void bounce(Vector2 b)
        {
            ball.bounce(b);
        }
        public void setPos(Vector2 x)
        {
            ball.setPos(x);
        }
        public Vector2 getPos()
        {
            return ball.getPos();
        }

        /*****************************************/

        public void newBall()
        {
            Load(eng, levelNum);
        }
        public bool getFirst()
        {
            return first;
        }
        public bool getOn()
        {
            return on;
        }
        public void setOn(bool x)
        {
            extra = true;
            on = x;

            ball = new Ball(new Vector2(0, 0), new Vector2(0, 0), new Point(0, 0), true);
            ballP = new BallParticles();
            trail = new BallParticles();
            ball.Load(eng, levelNum);
            ballP.Load(eng, levelNum, type);
            trail.Load(eng, levelNum, type);
            setTrail(true, new Vector2(1, 1), false);
            trail.trailEnd();
            ballP.trailEnd();
            collOn = true;
            c3 = 4;
            c4 = 160;
            if (levelNum == 4 || levelNum == 5 || levelNum == 6 ||
                levelNum == 14 || levelNum == 15 || levelNum == 16)
            {
                count = 10;
            }
            else
            {
                count = 5;
            }
            first = true;
            part = false;
            minSpeed = 1;
        }
        public void setOff()
        {
            on = false;
        }
        public int getLevelNum()
        {
            return levelNum;
        }
        public bool getBottomOn()
        {
            return bottomOn;
        }
        public void setBottomOn(bool x)
        {
            bottomOn = x;
        }
    }
}
