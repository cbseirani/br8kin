/******************************************
 * 09/12/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  ExtraBallManager.cs 
 * 
 *****************************************/
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Br8kIn_
{
    class ExtraBallManager
    {
        //instance variables
        BallManager[] helperBalls;
        Timer[] timer;
        short ballTrack, combo;
        bool add;
        int levelNum;

        /*****************************************/

        //constructor
        public ExtraBallManager()
        {
            timer = new Timer[2];
            helperBalls = new BallManager[2];
            helperBalls[0] = new BallManager(true);
            helperBalls[1] = new BallManager(true);
            timer[0] = new Timer(1000f, 0, 1);
            timer[1] = new Timer(1000f, 0, 1);
            ballTrack = -1;
            combo = 0;
            add = false;
            levelNum = 0;
        }

        /*****************************************/

        //load
        public void Load(Engine engine, int level)
        {
            timer = new Timer[2];
            helperBalls = new BallManager[2];
            helperBalls[0] = new BallManager(true);
            helperBalls[1] = new BallManager(true);
            helperBalls[0].Load(engine, level);
            helperBalls[1].Load(engine, level);
            helperBalls[0].setOn(false);
            helperBalls[1].setOn(false);
            timer[0] = new Timer(1000f, 0, 1);
            timer[1] = new Timer(1000f, 0, 1);
            ballTrack = -1;
            combo = 0;
            add = false;
            levelNum = level;
        }

        /*****************************************/

        //update
        public void Update(BallManager mainBall, GameTime gameTime, bool title, Rectangle clientBounds,
            PlayerManager player, SoundManager sManager, bool bottomOn, StatsManager stats, GamePadState g)
        {
            enable();
            checkTime(gameTime);
            determine(mainBall);
            checkBallCollisions(mainBall);

            if (ballTrack == 0)
            {
                helperBalls[0].Update(gameTime, title, clientBounds, player,
                    sManager, bottomOn, stats, g, false, false);
            }
            else if (ballTrack == 1)
            {
                helperBalls[1].Update(gameTime, title, clientBounds, player,
                    sManager, bottomOn, stats, g, false, false);
            }
            else if (ballTrack == 2)
            {
                helperBalls[0].Update(gameTime, title, clientBounds, player,
                    sManager, bottomOn, stats, g, false, false);
                helperBalls[1].Update(gameTime, title, clientBounds, player,
                    sManager, bottomOn, stats, g, false, false);
            }

            if (!helperBalls[0].getOn())
            {
                helperBalls[0].Update(gameTime, title, clientBounds, player,
                    sManager, bottomOn, stats, g, false, false);
            }
            if (!helperBalls[1].getOn())
            {
                helperBalls[1].Update(gameTime, title, clientBounds, player,
                    sManager, bottomOn, stats, g, false, false);
            }
        }

        /*****************************************/

        //draw
        public void Draw(SpriteBatch spriteBatch)
        {
            if (ballTrack == 0)
            {
                helperBalls[0].Draw(spriteBatch);
            }
            else if (ballTrack == 1)
            {
                helperBalls[1].Draw(spriteBatch);
            }
            else if (ballTrack == 2)
            {
                helperBalls[0].Draw(spriteBatch);
                helperBalls[1].Draw(spriteBatch);
            }


            if (!helperBalls[0].getOn())
            {
                helperBalls[0].Draw(spriteBatch);
            }
            if (!helperBalls[1].getOn())
            {
                helperBalls[1].Draw(spriteBatch);
            }
        }

        /*****************************************/

        public void incCombo()
        {
            if (ballTrack != 2)
            {
                if (combo < 4)
                {
                    combo++;
                }
                else
                {
                    add = true;
                    combo = 0;
                }
            }
        }
        public void resetCombo()
        {
            combo = 0;
        }

        /*****************************************/

        private void determine(BallManager main)
        {
            if (helperBalls[0].getOn() && helperBalls[1].getOn())
            {
                ballTrack = 2;
            }
            else if (helperBalls[0].getOn())
            {
                ballTrack = 0;
            }
            else if (helperBalls[1].getOn())
            {
                ballTrack = 1;
            }
            else
            {
                ballTrack = -1;
            }

            //IF player looses life, destroy balls
            if (!main.getOn())
            {
                combo = 0;
                if (ballTrack == 0)
                {
                    timer[0].setEnable(false);
                    helperBalls[0].setOff();
                    helperBalls[0].setPart(true, new Vector2(1, 1), true);
                }
                else if (ballTrack == 1)
                {
                    timer[1].setEnable(false);
                    helperBalls[1].setOff();
                    helperBalls[1].setPart(true, new Vector2(1, 1), true);
                }
                else if (ballTrack == 2)
                {
                    timer[0].setEnable(false);
                    helperBalls[0].setOff();
                    helperBalls[0].setPart(true, new Vector2(1, 1), true);
                    timer[1].setEnable(false);
                    helperBalls[1].setOff();
                    helperBalls[1].setPart(true, new Vector2(1, 1), true);
                }
            }
        }

        /*****************************************/

        private void enable()
        {
            if (add)
            {
                add = false;

                if (levelNum > 6)
                {
                    if (!helperBalls[0].getOn() && !helperBalls[1].getOn())
                    {
                        //set first ball on at last ball location
                        helperBalls[0].setOn(true);
                        timer[0] = new Timer(1000f, 0, 1);
                        timer[0].setEnable(true);
                    }
                    else if (!helperBalls[0].getOn())
                    {
                        //set first ball on
                        helperBalls[0].setOn(true);
                        timer[0] = new Timer(1000f, 0, 1);
                        timer[0].setEnable(true);
                    }
                    else if (!helperBalls[1].getOn())
                    {
                        //set second ball on
                        helperBalls[1].setOn(true);
                        timer[1] = new Timer(1000f, 0, 1);
                        timer[1].setEnable(true);
                    }
                }
                else
                {
                    if (!helperBalls[0].getOn())
                    {
                        //set first ball on
                        helperBalls[0].setOn(true);
                        timer[0] = new Timer(1000f, 0, 1);
                        timer[0].setEnable(true);
                    }
                }
            }
        }

        /*****************************************/

        private void checkBallCollisions(BallManager mainBall)
        {
            if (ballTrack == 0)
            {
                //check main ball with 1st extra ball
                ballCollide(mainBall, helperBalls[0]);
                ballCollide2(mainBall, helperBalls[0]);
            }
            else if (ballTrack == 1)
            {
                //check main ball with 2nd extra ball
                ballCollide(mainBall, helperBalls[1]);
                ballCollide2(mainBall, helperBalls[1]);
            }
            else if (ballTrack == 2)
            {
                //check main ball with 1st extra ball
                ballCollide(mainBall, helperBalls[0]);
                ballCollide2(mainBall, helperBalls[0]);
                //check main ball with 2nd extra ball
                ballCollide(mainBall, helperBalls[1]);
                ballCollide2(mainBall, helperBalls[1]);
                //check 1st extra ball with 2nd extra ball
                ballCollide(helperBalls[0], helperBalls[1]);
            }
        }
        private void ballCollide(BallManager x, BallManager y)
        {
            //check collisions between both balls
            if (x.getCollRect5().Intersects(y.getCollRect5()))
            {
                //IF (+x,+y) & (-x,+y)
                if ((((x.getBall().getDir().X > 0) && (x.getBall().getDir().Y > 0)) &&
                    ((y.getBall().getDir().X < 0) && (y.getBall().getDir().Y > 0))) ||
                    (((y.getBall().getDir().X > 0) && (y.getBall().getDir().Y > 0)) &&
                    ((x.getBall().getDir().X < 0) && (x.getBall().getDir().Y > 0))))
                {
                    x.setColl(false);
                    y.setColl(false);
                    x.bounce(new Vector2(-1, 1));
                    y.bounce(new Vector2(-1, 1));
                }
                //IF (+x,+y) & (+x,-y)
                else if ((((x.getBall().getDir().X > 0) && (x.getBall().getDir().Y > 0)) &&
                    ((y.getBall().getDir().X > 0) && (y.getBall().getDir().Y < 0))) ||
                    (((y.getBall().getDir().X > 0) && (y.getBall().getDir().Y > 0)) &&
                    ((x.getBall().getDir().X > 0) && (x.getBall().getDir().Y < 0))))
                {
                    x.setColl(false);
                    y.setColl(false);
                    x.bounce(new Vector2(1, -1));
                    y.bounce(new Vector2(1, -1));
                }
                //IF (+x,-y) & (-x,-y)
                else if ((((x.getBall().getDir().X > 0) && (x.getBall().getDir().Y < 0)) &&
                    ((y.getBall().getDir().X < 0) && (y.getBall().getDir().Y < 0))) ||
                    (((y.getBall().getDir().X > 0) && (y.getBall().getDir().Y < 0)) &&
                    ((x.getBall().getDir().X < 0) && (x.getBall().getDir().Y < 0))))
                {
                    x.setColl(false);
                    y.setColl(false);
                    x.bounce(new Vector2(-1, 1));
                    y.bounce(new Vector2(-1, 1));
                }
                //IF (-x,+y) & (-x,-y)
                else if ((((x.getBall().getDir().X < 0) && (x.getBall().getDir().Y > 0)) &&
                    ((y.getBall().getDir().X < 0) && (y.getBall().getDir().Y < 0))) ||
                    (((y.getBall().getDir().X < 0) && (y.getBall().getDir().Y > 0)) &&
                    ((x.getBall().getDir().X < 0) && (x.getBall().getDir().Y < 0))))
                {
                    x.setColl(false);
                    y.setColl(false);
                    x.bounce(new Vector2(1, -1));
                    y.bounce(new Vector2(1, -1));
                }
                //IF (+x,+y) & (-x,-y)
                else if ((((x.getBall().getDir().X > 0) && (x.getBall().getDir().Y > 0)) &&
                    ((y.getBall().getDir().X < 0) && (y.getBall().getDir().Y < 0))) ||
                    (((y.getBall().getDir().X > 0) && (y.getBall().getDir().Y > 0)) &&
                    ((x.getBall().getDir().X < 0) && (x.getBall().getDir().Y < 0))))
                {
                    x.setColl(false);
                    y.setColl(false);
                    x.bounce(new Vector2(-1, -1));
                    y.bounce(new Vector2(-1, -1));
                }
                //IF (+x,-y) & (-x,+y)
                else if ((((x.getBall().getDir().X > 0) && (x.getBall().getDir().Y < 0)) &&
                    ((y.getBall().getDir().X < 0) && (y.getBall().getDir().Y > 0))) ||
                    (((y.getBall().getDir().X > 0) && (y.getBall().getDir().Y < 0)) &&
                    ((x.getBall().getDir().X < 0) && (x.getBall().getDir().Y > 0))))
                {
                    x.setColl(false);
                    y.setColl(false);
                    x.bounce(new Vector2(-1, -1));
                    y.bounce(new Vector2(-1, -1));
                }
            }

        }
        private void ballCollide2(BallManager x, BallManager y)
        {
            //check collisions between both balls
            if (x.getCollRect5().Intersects(y.getCollRect5()))
            {
                //IF (+x,+y) & (+x,+y)
                if ((((x.getBall().getDir().X > 0) && (x.getBall().getDir().Y > 0)) &&
                    ((y.getBall().getDir().X > 0) && (y.getBall().getDir().Y > 0))))
                {
                    x.setColl(false);
                    y.setColl(false);
                    x.bounce(new Vector2(-1, -1));
                }
                //IF (-x,-y) & (-x,-y)
                else if ((((x.getBall().getDir().X < 0) && (x.getBall().getDir().Y < 0)) &&
                    ((y.getBall().getDir().X < 0) && (y.getBall().getDir().Y < 0))))
                {
                    x.setColl(false);
                    y.setColl(false);
                    x.bounce(new Vector2(-1, -1));
                }
                //IF (+x,-y) & (+x,-y)
                else if ((((x.getBall().getDir().X > 0) && (x.getBall().getDir().Y < 0)) &&
                    ((y.getBall().getDir().X > 0) && (y.getBall().getDir().Y < 0))))
                {
                    x.setColl(false);
                    y.setColl(false);
                    x.bounce(new Vector2(-1, -1));
                }
                //IF (-x,+y) & (-x,+y)
                else if ((((x.getBall().getDir().X < 0) && (x.getBall().getDir().Y > 0)) &&
                    ((y.getBall().getDir().X < 0) && (y.getBall().getDir().Y > 0))))
                {
                    x.setColl(false);
                    y.setColl(false);
                    x.bounce(new Vector2(-1, -1));
                }
            }
        }

        /*****************************************/

        private void checkTime(GameTime gT)
        {
            if (ballTrack == 0)
            {
                timer[0].Update(gT.ElapsedGameTime.Milliseconds);
            }
            else if (ballTrack == 1)
            {
                timer[1].Update(gT.ElapsedGameTime.Milliseconds);
            }
            else if (ballTrack == 2)
            {
                timer[0].Update(gT.ElapsedGameTime.Milliseconds);
                timer[1].Update(gT.ElapsedGameTime.Milliseconds);
            }

            //IF timer 1 is at the end
            if (timer[0].isEnabled())
            {
                if (timer[0].returnSeconds() > 5)
                {
                    timer[0].setEnable(false);
                    helperBalls[0].setOff();
                    helperBalls[0].setPart(true, new Vector2(1, 1), true);
                }
            }
            //IF timer 2 is at the end
            if (timer[1].isEnabled())
            {
                if (timer[1].returnSeconds() > 5)
                {
                    timer[1].setEnable(false);
                    helperBalls[1].setOff();
                    helperBalls[1].setPart(true, new Vector2(1, 1), true);
                }
            }
        }

        /*****************************************/

        public BallManager[] getBalls()
        {
            return helperBalls;
        }

        /*****************************************/
        /*****************************************/
        /*****************************************/
        /*****************************************/
    }
}
