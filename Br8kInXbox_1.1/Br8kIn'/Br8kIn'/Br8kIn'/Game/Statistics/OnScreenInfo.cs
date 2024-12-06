/******************************************
 * 06/16/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  OnScreenInfo.cs 
 * 
 *****************************************/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Br8kIn_
{
    class OnScreenInfo : Sprite
    {
        //instance variables
        SpriteFont font;
        Texture2D overlay;
        OneUp one;
        int points;
        int numOfLives;
        short min, sec;
        bool unlimited, oneB;

        //temp variables
        int levelNum;
        int elapsedTimeSec;

        /*****************************************/

        //constructor
        public OnScreenInfo(Texture2D ima, Vector2 pos, Vector2 spe,
            Point fSize, SpriteFont fT)
            : base(pos, spe, fSize)
        {
            overlay = ima;
            font = fT;
            points = 0;
            levelNum = 0;
            min = 0;
            sec = 0;
            unlimited = false;
            oneB = false;
        }

        /*****************************************/

        //update
        public void Update(GameTime gameTime, PlayerManager player, StatsManager gameStats,
            int lvlNum)
        {
            levelNum = lvlNum;

            sec = (short)(elapsedTimeSec % 60);
            min = (short)(elapsedTimeSec / 60);

            //update points
            points = gameStats.getNumOfPoints();
            numOfLives = gameStats.getNumOfLives();
            elapsedTimeSec = gameStats.getElapsedTime();

            if (oneB)
            {
                one.Update();
            }
        }

        /*****************************************/

        //draw
        public void Draw(SpriteBatch spriteBatch)
        {
            if (levelNum != 0)
            {
                spriteBatch.Draw(overlay, position,
                    new Rectangle(0, 0, frameSize.X, frameSize.Y),
                    Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, .9f);

                if (oneB)
                {
                    one.Draw(spriteBatch);
                }

                if (levelNum == 8 || levelNum == 18)
                {
                    spriteBatch.DrawString(font, "Points : " + points.ToString("0000000"), new Vector2(position.X + 22, position.Y + 15), Color.Black,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .91f);

                    if (!unlimited)
                    {
                        spriteBatch.DrawString(font, "Lives : " + numOfLives.ToString("000"), new Vector2(position.X + 22, position.Y + 40), Color.Black,
                            0f, new Vector2(0, 0), .9f, SpriteEffects.None, .91f);
                    }
                    else
                    {
                        spriteBatch.DrawString(font, "Lives : " + "---", new Vector2(position.X + 22, position.Y + 40), Color.Black,
                            0f, new Vector2(0, 0), .9f, SpriteEffects.None, .91f);
                    }

                    spriteBatch.DrawString(font, min.ToString("0") + ":" + sec.ToString("00"), new Vector2(position.X + 165, position.Y + 40), Color.Black,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .91f);
                }
                else
                {
                    spriteBatch.DrawString(font, "Points : " + points.ToString("0000000"), new Vector2(position.X + 22, position.Y + 15), Color.White,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .91f);

                    if (!unlimited)
                    {
                        spriteBatch.DrawString(font, "Lives : " + numOfLives.ToString("000"), new Vector2(position.X + 22, position.Y + 40), Color.White,
                            0f, new Vector2(0, 0), .9f, SpriteEffects.None, .91f);
                    }
                    else
                    {
                        spriteBatch.DrawString(font, "Lives : " + "---", new Vector2(position.X + 22, position.Y + 40), Color.White,
                            0f, new Vector2(0, 0), .9f, SpriteEffects.None, .91f);
                    }

                    spriteBatch.DrawString(font, min.ToString("0") + ":" + sec.ToString("00"), new Vector2(position.X + 165, position.Y + 40), Color.White,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .91f);
                }
            }
        }

        public void setUnlimited(bool x)
        {
            unlimited = x;
        }

        public short getMin()
        {
            return min;
        }
        public short getSec()
        {
            return sec;
        }

        /*****************************************/

        public void setOneUp(Engine engine)
        {
            oneB = true;
            one = new OneUp(new Vector2((1280 / 2)-110, 60), new Vector2(0, .5f), 
                new Point(0,0), engine, this);
        }
        public void oneOff()
        {
            oneB = false;
        }

        /*****************************************/

        private class OneUp : Sprite
        {
            //instance variables
            Texture2D one;
            OnScreenInfo oSi;

            /**********************/

            //constructor
            public OneUp(Vector2 pos, Vector2 spe, Point fSize, Engine engine, OnScreenInfo oS)
                : base(pos, spe, fSize)
            {
                speed = spe;
                position = pos;
                oSi = oS;
                one = engine.Content.Load<Texture2D>(@"Overlays/oneup");
            }

            /**********************/

            //update
            public void Update()
            {                
                if (position.Y > -70)
                {
                    //move up
                    position.Y -= speed.Y;
                }
                else
                {
                    //turn off
                    oSi.oneOff();
                }
            }

            /**********************/

            //draw
            public void Draw(SpriteBatch sB)
            {
                sB.Draw(one, position, null, Color.White, 0,
                        Vector2.Zero, 1f, SpriteEffects.None, .92f);
            }
        }
    }
}
