/******************************************
 * 03/20/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  LevelManager.cs 
 * 
 *****************************************/
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Br8kIn_
{
    class LevelManager
    {
        //instance variables
        int levelNum;
        int wall;
        int back;
        short playerType;

        Texture2D[] levelTw;
        Texture2D[] levelTip;
        Texture2D[] titles;
        Texture2D loadT;
        Vector2 pos1;
        short index1, index2, c1, c2, c3, time, loadO, s1;
        bool fade, load, title, fall1, fall2, wait, end,
            fall3, fall4;

        bool first;
        bool transOn;
        bool cover;
        bool uncover;
        bool set;
        int skip;
        

        /*****************************************/

        //constructor
        public LevelManager()
        {
      
            levelNum = 0;
            first = true;
            levelTw = new Texture2D[5];
            levelTip = new Texture2D[20];
            titles = new Texture2D[20];
            set = false;
            transOn = false;

            index1 = 0;
            index2 = -1;
            c1 = 2;
            c2 = 10;
            c3 = 2;
            s1 = 0;
            time = 200;
            fade = true;
            load = false;
            loadO = 1;
            title = false;
            fall1 = true;
            fall2 = false;
            fall3 = false;
            fall4 = false;
            wait = true;
            end = false;
            pos1 = new Vector2(1280 / 2, -50);
            
        }

        /*****************************************/

        //load
        public void LoadLevel(GameObjects gameObjects, Engine engine)
        {
            //determine which level to load
            switch (levelNum)
            {
                //title screen
                case 0:
                    wall = 1;
                    back = 1;
                    playerType = 0;
                    first = true;
                    break;
                case 1:
                    wall = 1;
                    back = 1;
                    playerType = 1;
                    break;
                case 2:
                    wall = 1;
                    back = 1;
                    playerType = 1;
                    break;
                case 3:
                    wall = 1;
                    back = 1;
                    playerType = 1;
                    break;
                case 4:
                    wall = 2;
                    back = 2;
                    playerType = 2;
                    break;
                case 5:
                    wall = 2;
                    back = 2;
                    playerType = 3;
                    break;
                case 6:
                    wall = 2;
                    back = 2;
                    playerType = 4;
                    break;
                case 7:
                    wall = 3;
                    back = 3;
                    playerType = 5;
                    break;
                case 8:
                    wall = 3;
                    back = 3;
                    playerType = 5;
                    break;
                case 9:
                    wall = 3;
                    back = 3;
                    playerType = 5;
                    break;
                case 10:
                    wall = 4;
                    back = 4;
                    playerType = 6;
                    break;
                case 11:
                    wall = 1;
                    back = 1;
                    playerType = 1;
                    break;
                case 12:
                    wall = 1;
                    back = 1;
                    playerType = 1;
                    break;
                case 13:
                    wall = 1;
                    back = 1;
                    playerType = 1;
                    break;
                case 14:
                    wall = 2;
                    back = 2;
                    playerType = 2;
                    break;
                case 15:
                    wall = 2;
                    back = 2;
                    playerType = 3;
                    break;
                case 16:
                    wall = 2;
                    back = 2;
                    playerType = 4;
                    break;
                case 17:
                    wall = 3;
                    back = 3;
                    playerType = 5;
                    break;
                case 18:
                    wall = 3;
                    back = 3;
                    playerType = 5;
                    break;
                case 19:
                    wall = 3;
                    back = 3;
                    playerType = 5;
                    break;
                case 20:
                    wall = 4;
                    back = 4;
                    playerType = 6;
                    end = true;
                    break;

                default:
                    break;
            }

            if (first)
            {
                levelTw[0] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranz01");
                levelTw[1] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranz02");
                levelTw[2] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranz03");
                levelTw[3] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranz04");
                levelTw[4] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranz05");
                
                levelTip[0] = engine.Content.Load<Texture2D>(@"LevelTrans\Tip1");
                levelTip[1] = engine.Content.Load<Texture2D>(@"LevelTrans\Tip2");
                levelTip[2] = engine.Content.Load<Texture2D>(@"LevelTrans\Tip3");
                levelTip[3] = engine.Content.Load<Texture2D>(@"LevelTrans\Tip4");
                levelTip[4] = engine.Content.Load<Texture2D>(@"LevelTrans\Tip5");
                levelTip[5] = engine.Content.Load<Texture2D>(@"LevelTrans\Tip6");
                levelTip[6] = engine.Content.Load<Texture2D>(@"LevelTrans\Tip7");
                levelTip[7] = engine.Content.Load<Texture2D>(@"LevelTrans\Tip8");
                levelTip[8] = engine.Content.Load<Texture2D>(@"LevelTrans\Tip9");
                levelTip[9] = engine.Content.Load<Texture2D>(@"LevelTrans\Tip10");
                levelTip[10] = engine.Content.Load<Texture2D>(@"LevelTrans\Tip11");
                levelTip[11] = engine.Content.Load<Texture2D>(@"LevelTrans\Tip12");
                levelTip[12] = engine.Content.Load<Texture2D>(@"LevelTrans\Tip13");
                levelTip[13] = engine.Content.Load<Texture2D>(@"LevelTrans\Tip14");
                levelTip[14] = engine.Content.Load<Texture2D>(@"LevelTrans\Tip15");
                levelTip[15] = engine.Content.Load<Texture2D>(@"LevelTrans\Tip16");
                levelTip[16] = engine.Content.Load<Texture2D>(@"LevelTrans\Tip17");
                levelTip[17] = engine.Content.Load<Texture2D>(@"LevelTrans\Tip18");
                levelTip[18] = engine.Content.Load<Texture2D>(@"LevelTrans\Tip19");
                levelTip[19] = engine.Content.Load<Texture2D>(@"LevelTrans\Tip20");

                titles[0] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranzT01");
                titles[1] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranzT02");
                titles[2] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranzT03");
                titles[3] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranzT04");
                titles[4] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranzT05");
                titles[5] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranzT06");
                titles[6] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranzT07");
                titles[7] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranzT08");
                titles[8] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranzT09");
                titles[9] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranzT10");
                titles[10] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranzT11");
                titles[11] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranzT12");
                titles[12] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranzT13");
                titles[13] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranzT14");
                titles[14] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranzT15");
                titles[15] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranzT16");
                titles[16] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranzT17");
                titles[17] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranzT18");
                titles[18] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranzT19");
                titles[19] = engine.Content.Load<Texture2D>(@"LevelTrans\levelTranzT20");

                loadT = engine.Content.Load<Texture2D>(@"LevelTrans\loading");

                first = false;
            }

            //load game objects by level
            gameObjects.LoadNewLevel(wall, back, playerType);
        }

        /*****************************************/

        //method to return level number
        public int getLevelNum()
        {
            return levelNum;
        }
        public void incLevelIndex()
        {
            index2++;
        }

        //method to increment level num
        public void newLevel()
        {
            skip = 1;
            levelNum += skip;
        }
        //used to get level skipped to
        public int getSkip()
        {
            return skip;
        }
        //method to set level number
        public void setLevelNum(int x)
        {
            levelNum = x;
        }

        /*****************************************/

        public void setTrans(bool x)
        {
            transOn = x;
        }
        public bool getTrans()
        {
            return transOn;
        }

        public void setCover(bool x)
        {
            cover = x;
        }
        public bool getCover()
        {
            return cover;
        }

        public bool getUCover()
        {
            return uncover;
        }
        public void setUCover(bool x)
        {
            uncover = x;
        }

        public void setSet(bool x)
        {
            set = x;
        }
        public bool getSet()
        {
            return set;
        }

        public void reset()
        {
            first = false;
            set = false;
            index1 = 0;
            c1 = 2;
            c2 = 10;
            c3 = 2;
            time = 200;
            fade = true;
            load = false;
            loadO = 1;
            title = false;
            fall1 = true;
            fall2 = false;
            fall3 = false;
            fall4 = false;
            wait = true;
            pos1 = new Vector2(1280 / 2, -50);
        }

        /*****************************************/

        //update
        public void Update(GameTime gameTime, Rectangle cb)
        {
            if (transOn)
            {
                if (cover)
                {
                    //fade in white screen
                    if (fade)
                    {
                        if (c1 > 0)
                        {
                            c1--;
                        }
                        else
                        {
                            c1 = 2;
                            if (index1 <= 3)
                            {
                                index1++;
                            }
                            else
                            {
                                fade = false;
                            }
                        }
                    }
                    //ELSE fade in is finished
                    else
                    {
                        //display loading/flash loading
                        if (!load)
                        {
                            load = true;
                            loadO = 1;
                        }

                        if (c2 > 0)
                        {
                            c2--;
                        }
                        else
                        {
                            c2 = 10;
                            loadO *= -1;
                        }

                        //display title/bounce title
                        if (!title)
                        {
                            title = true;
                        }
                        else
                        {
                            //start falling momentum
                            if (fall1)
                            {
                                if (pos1.Y < ((720 / 2)))
                                {
                                    if (s1 == 0)
                                    {
                                        pos1.Y += 1.5f * 2;
                                        if (pos1.Y > 10)
                                        {
                                            s1 = 1;
                                        }
                                    }
                                    else if (s1 == 1)
                                    {
                                        pos1.Y += 1.5f * 3;
                                        if (pos1.Y > 30)
                                        {
                                            s1 = 2;
                                        }
                                    }
                                    else if (s1 == 2)
                                    {
                                        pos1.Y += 1.5f * 4;
                                        if (pos1.Y > 80)
                                        {
                                            s1 = 3;
                                        }
                                    }
                                    else if (s1 == 3)
                                    {
                                        pos1.Y += 1.5f * 5;
                                        if (pos1.Y > 120)
                                        {
                                            s1 = 4;
                                        }
                                    }
                                    else if (s1 == 4)
                                    {
                                        pos1.Y += 1.5f * 6;
                                        if (pos1.Y > 200)
                                        {
                                            s1 = 5;
                                        }
                                    }
                                    else if (s1 == 5)
                                    {
                                        pos1.Y += 1.5f * 7;
                                        if (pos1.Y > 360)
                                        {
                                            s1 = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    fall1 = false;
                                    fall2 = true;
                                }
                            }
                            //once it reaches Y1, bounce back up
                            else if (fall2)
                            {
                                if (pos1.Y > 250)
                                {
                                    if (s1 == 0)
                                    {
                                        pos1.Y -= 1.5f * 5;
                                        if (pos1.Y < 340)
                                        {
                                            s1 = 1;
                                        }
                                    }
                                    else if (s1 == 1)
                                    {
                                        pos1.Y -= 1.5f * 4;
                                        if (pos1.Y < 300)
                                        {
                                            s1 = 2;
                                        }
                                    }
                                    else if (s1 == 2)
                                    {
                                        pos1.Y -= 1.5f * 3;
                                        if (pos1.Y < 280)
                                        {
                                            s1 = 3;
                                        }
                                    }
                                    else if (s1 == 3)
                                    {
                                        pos1.Y -= 1.5f * 2;
                                        if (pos1.Y < 270)
                                        {
                                            s1 = 4;
                                        }
                                    }
                                    else if (s1 == 4)
                                    {
                                        pos1.Y -= 1.5f * 1;
                                        if (pos1.Y < 258)
                                        {
                                            s1 = 5;
                                        }
                                    }
                                    else if (s1 == 5)
                                    {
                                        pos1.Y -= 1.5f * .9f;
                                        if (pos1.Y < 250)
                                        {
                                            s1 = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    fall2 = false;
                                    fall3 = true;
                                }
                            }
                            //once it reaches Y2, fall back to Y1 
                            else if (fall3)
                            {
                                if (pos1.Y < ((720 / 2)))
                                {
                                    if (s1 == 0)
                                    {
                                        pos1.Y += 1.5f * 1;
                                        if (pos1.Y > 260)
                                        {
                                            s1 = 1;
                                        }
                                    }
                                    else if (s1 == 1)
                                    {
                                        pos1.Y += 1.5f * 2;
                                        if (pos1.Y > 280)
                                        {
                                            s1 = 2;
                                        }
                                    }
                                    else if (s1 == 2)
                                    {
                                        pos1.Y += 1.5f * 3;
                                        if (pos1.Y > 320)
                                        {
                                            s1 = 3;
                                        }
                                    }
                                    else if (s1 == 3)
                                    {
                                        pos1.Y += 1.5f * 4;
                                        if (pos1.Y > 360)
                                        {
                                            s1 = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    fall3 = false;
                                    fall4 = true;
                                }
                            }
                            //bounce back up
                            else if (fall4)
                            {
                                if (pos1.Y > 320)
                                {
                                    if (s1 == 0)
                                    {
                                        pos1.Y -= 1.5f * 2;
                                        if (pos1.Y < 340)
                                        {
                                            s1 = 1;
                                        }
                                    }
                                    else if (s1 == 1)
                                    {
                                        pos1.Y -= 1.5f * 1;
                                        if (pos1.Y < 320)
                                        {
                                            s1 = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    fall4 = false;
                                }
                            }
                            //fall back to y1
                            else
                            {
                                if (pos1.Y < ((720 / 2)))
                                {
                                    if (s1 == 0)
                                    {
                                        pos1.Y += 1.5f * 1;
                                        if (pos1.Y > 260)
                                        {
                                            s1 = 1;
                                        }
                                    }
                                    else if (s1 == 1)
                                    {
                                        pos1.Y += 1.5f * 2;
                                        if (pos1.Y > 360)
                                        {
                                            s1 = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    set = true;
                                }
                            }
                        }
                    }
                }
                else if (uncover)
                {
                    //take off ball/title/loading
                    if (wait)
                    {
                        if (time > 0)
                        {
                            time -= 2;
                        }
                        else
                        {
                            wait = false;
                        }
                        if (c2 > 0)
                        {
                            c2--;
                        }
                        else
                        {
                            c2 = 10;
                            loadO *= -1;
                        }
                    }
                    else
                    {
                        load = false;
                        title = false;

                        //fade out white background
                        if (c3 > 0)
                        {
                            c3--;
                        }
                        else
                        {
                            c3 = 2;
                            if (index1 > 0)
                            {
                                index1--;
                            }
                            else
                            {
                                uncover = false;
                            }
                        }
                    }
                }
            }
        }

        /*****************************************/

        //draw
        public void Draw(SpriteBatch spriteBatch, Rectangle clientBounds)
        {
            if (transOn)
            {
                //draw white background
                spriteBatch.Draw(levelTw[index1], new Vector2(0, 0), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .97f);


                //draw loading
                if (load)
                {
                    if (loadO > 0)
                    {
                        spriteBatch.Draw(loadT, new Vector2(1280 / 2 + 450, 720 / 2 + 265), null, Color.White, 0,
                            new Vector2(loadT.Width / 2, loadT.Height / 2), 1f, SpriteEffects.None, .99f);
                    }
                }

                //draw title
                if (title)
                {
                    spriteBatch.Draw(titles[index2], pos1, null, Color.White, 0,
                        new Vector2(titles[index2].Width / 2, titles[index2].Height / 2), 1f,
                        SpriteEffects.None, .99f);

                    //draw tip            
                    spriteBatch.Draw(levelTip[index2], new Vector2(50,50), null, Color.White, 0,
                        new Vector2(5,5), 1f,
                        SpriteEffects.None, .999f);
                }
            }
        }
    }
}