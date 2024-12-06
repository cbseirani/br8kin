/******************************************
 * 03/20/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  Player.cs 
 * 
 *****************************************/
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Br8kIn_
{
    class Player : Sprite
    {
        //instance variables
        List<Texture2D> player;
        short c1, c2, graceP, c3, time, playerDmg;
        Vector2 maxSpeed, minSpeed;
        Vector2 origin;
        bool hLeft, hRight, sLeft, f1, on, grace, hookR, hookL;

        //flags
        short directionP;

        /*****************************************/

        //constructor
        public Player(Vector2 pos, Vector2 spe, Point fSize)
            : base(pos, spe, fSize)
        {
            directionP = 0;
            player = new List<Texture2D>();
            speed = new Vector2(15, 0);
            frameSize = new Point(150, 25);
            c1 = 0;
            c2 = 3;
            c3 = 4;
            time = 100;
            sLeft = true;
            f1 = true;
            grace = false;
            graceP = 1;
            maxSpeed = new Vector2(0, 0);
            minSpeed = new Vector2(0, 0);
            hLeft = false;
            hRight = false;
            origin = new Vector2();
            hookR = false;
            hookL = false;
        }

        /*****************************************/

        //load
        public void Load(List<Texture2D> images, Point[] frame, Vector2 pos, short pT)
        {
            player = images;
            position = pos;
            c1 = 10;
            c2 = 3;
            c3 = 4;
            time = 100;
            f1 = true;
            on = true;
        }

        /*****************************************/

        //update
        public void Update(GameTime gameTime, Rectangle clientBounds, GamePadState g)
        {
            if (on)
            {
                //move sprite based on user input
                position += direction(g, gameTime);

                // If sprite is off the screen, move it back within the game window
                if ((position.X - frameSize.X / 2) < 10)
                {
                    position.X = 10 + frameSize.X / 2;
                }
                if ((position.X + frameSize.X / 2) > 1280 - 10)
                {
                    position.X = 1280 - 10 - frameSize.X / 2;
                }

                if (grace)
                {
                    if (time > 0)
                    {
                        time--;
                        if (c3 > 0)
                        {
                            c3--;
                        }
                        else
                        {
                            c3 = 4;
                            graceP *= -1;
                        }
                    }
                    else
                    {
                        grace = false;
                        graceP = 1;
                    }
                }
            }
        }

        /*****************************************/

        //draw
        public void Draw(SpriteBatch spriteBatch, short plyDmg)
        {
            playerDmg = plyDmg;
            if (on)
            {
                if (graceP > 0)
                {
                    spriteBatch.Draw(player[plyDmg], position, null,
                        Color.White, 0, origin, 1f, SpriteEffects.None, .28f);
                }
            }
        }

        /*****************************************/

        //method to set direction
        private void setDir(short dir)
        {
            directionP = dir;
        }

        //method to return direction of player
        public short getDir()
        {
            return directionP;
        }
        public void setMaxSpeed(Vector2 x)
        {
            maxSpeed = x;
            minSpeed = new Vector2(x.X - 9, x.Y);
        }

        /*****************************************/

        //method that returns player's collision rectangle
        public Rectangle getCollRect()
        {
            return new Rectangle((int)(position.X - (frameSize.X / 2) + 5), (int)position.Y - (frameSize.Y / 2),
                frameSize.X - 10, frameSize.Y);
        }
        public Rectangle getCollRectL()
        {
            return new Rectangle((int)(position.X - (frameSize.X / 2) + 2), (int)position.Y - (frameSize.Y / 2),
                5, frameSize.Y);
        }
        public Rectangle getCollRectR()
        {
            return new Rectangle((int)(position.X + (frameSize.X / 2) - 7), (int)position.Y - (frameSize.Y / 2),
                5, frameSize.Y);
        }
        public Rectangle[] getMainColl()
        {
            Rectangle[] main = new Rectangle[5];

            //left collision box
            main[0] = new Rectangle((int)(position.X - player[playerDmg].Width / 2),
                (int)(position.Y - player[playerDmg].Height / 2),
                player[playerDmg].Width / 3, player[playerDmg].Height);

            //middle collision box
            main[1] = new Rectangle((int)(position.X - (player[playerDmg].Width / 2)) + (player[playerDmg].Width / 3),
                (int)(position.Y - player[playerDmg].Height / 2),
                player[playerDmg].Width / 3, player[playerDmg].Height);

            //right collision box
            main[2] = new Rectangle((int)(position.X - (player[playerDmg].Width / 2)) + (player[playerDmg].Width / 3) + (player[playerDmg].Width / 3),
                (int)(position.Y - player[playerDmg].Height / 2),
                player[playerDmg].Width / 3, player[playerDmg].Height);

            //left corner
            main[3] = new Rectangle((int)(position.X - player[playerDmg].Width / 2),
                (int)(position.Y - player[playerDmg].Height / 2),
                3, player[playerDmg].Height);

            //right corner
            main[4] = new Rectangle((int)(position.X - player[playerDmg].Width / 2) + (player[playerDmg].Width - 3),
                (int)(position.Y - player[playerDmg].Height / 2),
                3, player[playerDmg].Height);

            return main;
        }

        /*****************************************/

        //method to determine position based on user input
        private Vector2 direction(GamePadState gps, GameTime gameTime)
        {
            //create blank input
            Vector2 inputDir = Vector2.Zero;

            //LEFT
            if (Keyboard.GetState().IsKeyDown(Keys.Left) || gps.IsButtonDown(Buttons.DPadLeft) ||
                gps.ThumbSticks.Left.X < -.3 || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                //IF user has been holding left
                if (hLeft)
                {
                    //increase speed until max speed is reached
                    if (speed.X < maxSpeed.X)
                    {
                        speed.X++;
                    }
                    inputDir.X -= 1.2f;
                }
                else
                {
                    hLeft = true;
                    sLeft = true;
                    speed.X = minSpeed.X;
                    c1 = 0;
                    c2 = 1;
                    f1 = false;
                }
            }
            else
            {
                hLeft = false;
            }

            //RIGHT
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || gps.IsButtonDown(Buttons.DPadRight) ||
                gps.ThumbSticks.Left.X > .3 || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (hRight)
                {
                    if (speed.X < maxSpeed.X)
                    {
                        speed.X++;
                    }
                    inputDir.X += 1.2f;
                }
                else
                {
                    hRight = true;
                    sLeft = false;
                    speed.X = minSpeed.X;
                    c1 = 0;
                    c2 = 1;
                    f1 = false;
                }
            }
            else
            {
                hRight = false;
            }

            //RIGHT triger
            if (Keyboard.GetState().IsKeyDown(Keys.E) || gps.IsButtonDown(Buttons.RightTrigger))
            {
                hookR = true;
            }
            else
                hookR  = false;
                //Left Trigger
                if (Keyboard.GetState().IsKeyDown(Keys.Q) || gps.IsButtonDown(Buttons.LeftTrigger))
                {
                    hookL = true;
                }
                else
                    hookL = false;

            if (!f1)
            {
                //if user is not pressing direction
                if (!hLeft && !hRight)
                {
                    //if player is still in motion
                    if (c1 < 13)
                    {
                        if (c2 <= 0)
                        {
                            c2 = 1;
                            c1++;

                            //slow down
                            if (speed.X > 0)
                            {
                                speed.X -= 2;
                                if (sLeft)
                                {
                                    inputDir.X -= .8f;
                                }
                                else
                                {
                                    inputDir.X += .8f;
                                }
                            }
                        }
                        else
                        {
                            c2--;
                        }
                    }
                }
            }

            //return user input direction
            return inputDir * speed;
        }

        /*****************************************/

        public void setOrigin(Vector2 x)
        {
            origin = x;
        }
        public void setOn(bool x)
        {
            on = x;
        }
        public bool getOn()
        {
            return on;
        }
        public bool getGrace()
        {
            return grace;
        }
        public void setGrace(bool x)
        {
            grace = x;
        }
        public bool getHookR()
        {
            return hookR;
        }
        public bool getHookL()
        {
            return hookL;
        }
    }
}
