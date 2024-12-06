/******************************************
 * 03/20/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  GameObjects.cs 
 * 
 *****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Xml.Serialization;

namespace Br8kIn_
{
    class GameObjects
    {
        //instance variables
        StateSystem stateSystem;

        //managers
        LevelManager level;
        BackgroundManager bManager;
        PlayerManager pManager;
        BallManager mainBall;
        ExtraBallManager xBall;
        BlockManager blockM;
        StatsManager statsM;
        SoundManager sManager;
        bool lvlSkip, beat, cont, unlimited, enter, enter2;
        bool key, noP, bOn, levelTr, wait, gameOver, trialEnd;
        int soundTrax, time, waitT;

        /*****************************************/


        //constructor
        public GameObjects(StateSystem stSystem)
        {
            //store location of current state system
            stateSystem = stSystem;
            lvlSkip = false;
            key = false;
            noP = false;
            bOn = false;
            levelTr = false;
            time = 100;
            beat = false;
            wait = false;
            waitT = 30;
            cont = false;
            unlimited = false;
            enter = false;
            enter2 = false;
            gameOver = false;
            trialEnd = false;

            //instantiate managers
            level = new LevelManager();
            bManager = new BackgroundManager();
            pManager = new PlayerManager();
            statsM = new StatsManager(stateSystem);
            blockM = new BlockManager();
            sManager = new SoundManager();
            mainBall = new BallManager(true);
            xBall = new ExtraBallManager();
        }


        /*****************************************/


        //load
        public void LoadContent(Engine engine, Rectangle clientBounds)
        {
            //load level
            level.LoadLevel(this, engine);
        }


        /*****************************************/


        //update
        public void Update(GameTime gameTime, Rectangle clientBounds, GamePadState g, DisplayPane dP)
        {
            //IF game is in trial mode
            if (Guide.IsTrialMode)
            {
                //if user reaches level 4 and level is over
                if (level.getLevelNum() >= 4 && trialEnd)
                {
                    //show thanks and go to title screen
                    stateSystem.setThanksT();
                    stateSystem.setIsAboutTitle();
                    return;
                }
            }

            //IF game is over
            if (statsM.getGameOver())
            {
                //update blocks
                bOn = false;
                blockM.Update(gameTime, mainBall, statsM,
                    pManager, sManager, xBall, this);

                //sound manager 
                sManager.Update(level.getTrans(), dP);

                //update player
                pManager.Update(gameTime, clientBounds, g, dP);

                //update ball
                mainBall.Update(gameTime, stateSystem.getIsTitle(),
                    clientBounds, pManager, sManager, bOn, statsM, g, dP.getPaddle(), dP.getBall());

                if (stateSystem.getStorageSelection())
                {
                    if (!enter)
                    {
                        //check if player needs to enter highscore
                        enter2 = statsM.checkHighEnter();
                        enter = true;
                    }

                    //IF player has a highscore to be entered
                    if (enter2)
                    {
                        //let player enter name for high score
                        if (!dP.getEntry())
                        {
                            dP.setEntry(true);
                        }
                        dP.UpdateE(g, gameTime, this);
                    }
                    else
                    {
                        endEntry();
                    }
                }
                else
                {
                    gameOver = true;
                }

                if (gameOver)
                {
                    //display the top 10
                    if (!dP.getGameOver())
                    {
                        dP.setGameOver(true);
                    }
                    dP.UpdateG(g, gameTime, this);
                }

                //IF ready to continue back to title screen
                if (cont)
                {
                    //IF player beat all 20 levels
                    if (beat)
                    {
                        //show congratz and go to title screen
                        stateSystem.setCongratz();
                        stateSystem.setIsAboutTitle();
                    }
                    else
                    {
                        //show thanks and go to title screen
                        stateSystem.setThanks();
                        stateSystem.setIsAboutTitle();
                    }
                }
            }
            //ELSE IF player did not finish level
            else if (!pManager.getBeatLevel() && !lvlSkip)
            {
                if (wait)
                {
                    if (waitT > 0)
                    {
                        waitT--;
                    }
                    else
                    {
                        wait = false;
                        waitT = 30;
                    }
                }
                else
                {
                    //update player
                    pManager.Update(gameTime, clientBounds, g, dP);

                    //update ball
                    mainBall.Update(gameTime, stateSystem.getIsTitle(), clientBounds,
                        pManager, sManager, bOn, statsM, g, dP.getPaddle(), dP.getBall());
                    xBall.Update(mainBall, gameTime, stateSystem.getIsTitle(),
                        clientBounds, pManager, sManager, bOn, statsM, g);

                    //update blocks
                    blockM.Update(gameTime, mainBall, statsM,
                        pManager, sManager, xBall, this);

                    //sound manager 
                    sManager.Update(level.getTrans(), dP);

                    //update stats
                    statsM.Update(gameTime, this, pManager, level.getLevelNum());
                }
            }
            //ELSE IF load new level
            else if (levelTr)
            {
                //level transitioning starts
                if (!level.getTrans())
                {
                    //cover screen
                    level.setTrans(true);
                    level.setCover(true);
                    level.incLevelIndex();
                    pManager.vibeOff();
                    noP = true;
                }
                //when screen is fully covered, load level
                else if (level.getSet())
                {
                    //load new level
                    level.newLevel();
                    level.LoadLevel(this, stateSystem.getEngine());
                    level.setSet(false);
                    level.setCover(false);
                    level.setUCover(true);

                    //stop Background
                    sManager.stopMusic(level.getLevelNum(), level.getSkip(), soundTrax);

                    //play background
                    sManager.volumeDown();
                    soundTrax = 0;
                    sManager.playMusic(level.getLevelNum(), soundTrax);
                }
                //when screen is being covered or uncovered
                else if (level.getCover() || level.getUCover())
                {
                    level.Update(gameTime, clientBounds);
                }
                //end of level transition
                else
                {
                    level.setTrans(false);
                    levelTr = false;
                    level.reset();
                    pManager.setBeatLevel(false);
                    lvlSkip = false;
                    noP = false;
                    wait = true;
                }
            }
            //ELSE finish block/player particles, tally up points and display
            else
            {
                //update blocks
                blockM.Update(gameTime, mainBall, statsM,
                    pManager, sManager, xBall, this);

                //sound manager 
                sManager.Update(level.getTrans(), dP);

                //update player
                pManager.Update(gameTime, clientBounds, g, dP);

                //update ball
                mainBall.Update(gameTime, stateSystem.getIsTitle(), clientBounds,
                    pManager, sManager, bOn, statsM, g, dP.getPaddle(), dP.getBall());
                xBall.Update(mainBall, gameTime, stateSystem.getIsTitle(),
                    clientBounds, pManager, sManager, bOn, statsM, g);

                //display level beaten screen
                if (level.getLevelNum() > 0)
                {
                    bOn = false;
                    if (!dP.getLvlEnd())
                    {
                        if (time > 0)
                        {
                            time -= 2;
                        }
                        else
                        {
                            dP.setLvlEnd(true);
                            bOn = false;
                            time = 100;
                        }
                    }
                    else
                    {
                        dP.UpdateR(g, gameTime, this);
                    }
                }
                else
                {
                    //set level transition on
                    levelTr = true;
                    bOn = true;
                }
            }

            /*if (!stateSystem.getIsTitle())
            {
                if (g.IsButtonDown(Buttons.LeftShoulder))
                {
                    key = true;
                }
                else if (g.IsButtonUp(Buttons.LeftShoulder) && key || Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    lvlSkip = true;
                    key = false;
                }
            }*/
        }


        /*****************************************/


        //draw
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch,
            Rectangle clientBounds, DisplayPane dP)
        {
            //draw background
            bManager.Draw(spriteBatch, clientBounds);
            level.Draw(spriteBatch, clientBounds);

            //draw player
            pManager.Draw(spriteBatch);

            //draw ball
            mainBall.Draw(spriteBatch);
            xBall.Draw(spriteBatch);

            //draw blocks
            blockM.Draw(spriteBatch);

            //draw stats
            statsM.Draw(spriteBatch);

            if (dP.getLvlEnd() || dP.getGameOver() || dP.getEntry())
            {
                dP.Draw(spriteBatch);
            }
        }


        /*****************************************/


        //method to load new level
        public void LoadNewLevel(int wal, int bac, short playerType)
        {
            //load background
            bManager.LoadBackground(stateSystem.getIsTitle(), stateSystem.getEngine(),
                wal, level.getLevelNum());

            //load player
            pManager.Load(stateSystem.getEngine(), playerType, stateSystem.getContNum());

            //load ball
            mainBall.Load(stateSystem.getEngine(), level.getLevelNum());
            xBall.Load(stateSystem.getEngine(), level.getLevelNum());

            //load blocks
            blockM = new BlockManager();
            blockM.Load(level.getLevelNum(), stateSystem.getEngine(), sManager);

            //load stats
            statsM.Load(this, stateSystem.getEngine(), level.getLevelNum());

            //load sound
            sManager.LoadSound(level.getLevelNum());
        }

        //method to start a new game
        public void startNewGame()
        {
            pManager.setBeatLevel(true);
        }

        //method to get play position
        public Vector2 getPlayerPos()
        {
            return pManager.getPos();
        }

        //method to load title screen
        public void LoadTitle()
        {
            level.setLevelNum(0);
            stateSystem.setIsTitle(true);
            level.LoadLevel(this, stateSystem.getEngine());
        }

        public bool getNoP()
        {
            return noP;
        }

        public void Unload()
        {
            sManager.Unload();
        }

        public void endResults()
        {
            if (level.getLevelNum() != 20)
            {
                if (level.getLevelNum() >= 4 && Guide.IsTrialMode)
                {
                    trialEnd = true;
                }
                else
                {
                    bOn = true;
                    levelTr = true;
                }
            }
            else
            {
                statsM.setGameOver();
            }
        }
        public void setBeatGame(bool x)
        {
            beat = x;
        }

        public StatsManager getStats()
        {
            return statsM;
        }

        public void saveHighScore(StorageDevice device)
        {
            statsM.writeHS(device);
        }
        public void loadHighScore(StorageDevice device)
        {
            statsM.readHS(device);
        }
        public PlayerManager getPlayerM()
        {
            return pManager;
        }
        public bool getUnlmtd()
        {
            return unlimited;
        }
        public void endEntry()
        {
            enter2 = false;
            gameOver = true;
        }
        public LevelManager lvlManager()
        {
            return level;
        }
        public void endGameOver(DisplayPaneF dP)
        {
            gameOver = false;
            dP.setGameOver(false);
            cont = true;
        }
        public void pushData(string name)
        {
            statsM.enterName(name);
            statsM.pushDataAndSave(stateSystem.getDeviceSel());
        }
        public StateSystem getState()
        {
            return stateSystem;
        }
        public SoundManager getSmanager()
        {
            return sManager;
        }
    }
}
