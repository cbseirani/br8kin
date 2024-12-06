/******************************************
 * 03/25/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  StatsManager.cs 
 * 
 *****************************************/
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Content;

namespace Br8kIn_
{
    class StatsManager
    {
        //instance variables
        OnScreenInfo oSi;
        PointManager points;
        HighScore hS;
        GameObjects gameO;
        bool gameOver, hasHighScore, first, unlimited;
        int[] highScoreTemp;
        string[] names;
        int index;

        /*****************************************/

        //constructor
        public StatsManager(StateSystem stSys)
        {
            gameOver = false;
            hasHighScore = false;
            points = new PointManager();
            hS = new HighScore(stSys);
            names = new string[10];
            first = true;
            unlimited = false;
            highScoreTemp = new int[10];
            index = -1;
        }

        /*****************************************/

        //load
        public void Load(GameObjects gameObjects, Engine engine, int lvl)
        {
            //load points
            gameO = gameObjects;
            points.Load(lvl, this);

            //load on screen info
            oSi = new OnScreenInfo(engine.Content.Load<Texture2D>(@"Overlays/pOverlay"),
                    new Vector2((1280 / 2) - (250 / 2), 13), new Vector2(0, 0), new Point(250, 90),
                    engine.Content.Load<SpriteFont>("ScreenInfo"));
        }

        /*****************************************/

        //update
        public void Update(GameTime gT, GameObjects gO, PlayerManager pM, int lvl)
        {
            //update points
            points.Update(gT);

            //update onscreen info
            oSi.Update(gT, pM, this, lvl);
        }

        /*****************************************/

        //draw
        public void Draw(SpriteBatch spriteBatch)
        {
            //draw onscreen info
            oSi.Draw(spriteBatch);
        }

        /*****************************************/

        public void addLife()
        {
            points.addLife();
        }
        public void setLives()
        {
            points.setLives(99);
        }
        public void delLife()
        {
            if (!unlimited)
            {
                if (points.getNumOfLives() == 0)
                {
                    gameOver = true;
                }
                else
                {
                    points.delLife();
                }
            }
        }
        public void addPoint(short type)
        {
            points.addPoint(type);
        }
        public int getNumOfPoints()
        {
            return points.getNumOfPoints();
        }
        public int getNumOfLives()
        {
            return points.getNumOfLives();
        }
        public int getElapsedTime()
        {
            return points.getElapsedTime();
        }
        public bool getGameOver()
        {
            return gameOver;
        }
        public void setHasHighScore(bool x)
        {
            hasHighScore = x;
        }
        public bool getHasHighScore()
        {
            return hasHighScore;
        }

        /*****************************************/

        public bool checkHighEnter()
        {
            //get highscores in array
            int[] x = hS.getPoints();
            highScoreTemp = new int[10];

            //compare current score with high scores
            for (int a = 0; a < 10; a++)
            {
                //IF current score fits in top 10
                if (points.getNumOfPoints() > x[a])
                {
                    highScoreTemp = insertion(a, x, points.getNumOfPoints());
                    return true;
                }
            }
            
            return false;
        }

        /*****************************************/

        public void readHS(StorageDevice device)
        {
            hS.loadGame(device);
        }
        public void writeHS(StorageDevice device)
        {
            hS.saveGame(device);
        }

        public string[] getHS()
        {
            string[] HSdisplay = new string[10];
            string[] n = hS.getNames();
            int[] p = hS.getPoints();

            HSdisplay[0] = "1.  " + n[0] + "..." + p[0];
            HSdisplay[1] = "2.  " + n[1] + "..." + p[1];
            HSdisplay[2] = "3.  " + n[2] + "..." + p[2];
            HSdisplay[3] = "4.  " + n[3] + "..." + p[3];
            HSdisplay[4] = "5.  " + n[4] + "..." + p[4];
            HSdisplay[5] = "6.  " + n[5] + "..." + p[5];
            HSdisplay[6] = "7.  " + n[6] + "..." + p[6];
            HSdisplay[7] = "8.  " + n[7] + "..." + p[7];
            HSdisplay[8] = "9.  " + n[8] + "..." + p[8];
            HSdisplay[9] = "10. " + n[9] + "..." + p[9];

            return HSdisplay;
        }
        public string[] getPointString()
        {
            string[] display = new string[8];
            int x = points.getTimeBonus();

            display[0] = "Level Points";
            display[1] = points.getPointCounter().ToString();
            display[2] = "Time Bonus";
            display[3] = points.getTimeBonusSt();
            display[4] = "Level Total";
            display[5] = x.ToString();
            display[6] = "Bonus Lives";
            display[7] = points.getBonusLives().ToString();

            return display;
        }

        /*****************************************/

        public void newLevel(int lvl)
        {
            //load points
            points.Load(lvl, this);
        }

        public void setUnlimited(bool x)
        {
            unlimited = x;
            oSi.setUnlimited(x);
        }

        public void setGameOver()
        {
            gameOver = true;
        }

        public int[] insertion(int a, int[] x, int pts)
        {
            int[] z = new int[10];
            index = a;

            if (a == 0)
            {
                z[0] = pts;
                z[1] = x[0];
                z[2] = x[1];
                z[3] = x[2];
                z[4] = x[3];
                z[5] = x[4];
                z[6] = x[5];
                z[7] = x[6];
                z[8] = x[7];
                z[9] = x[8];
            }
            else if (a == 1)
            {
                z[0] = x[0];
                z[1] = pts;
                z[2] = x[1];
                z[3] = x[2];
                z[4] = x[3];
                z[5] = x[4];
                z[6] = x[5];
                z[7] = x[6];
                z[8] = x[7];
                z[9] = x[8];
            }
            else if (a == 2)
            {
                z[0] = x[0];
                z[1] = x[1];
                z[2] = pts;
                z[3] = x[2];
                z[4] = x[3];
                z[5] = x[4];
                z[6] = x[5];
                z[7] = x[6];
                z[8] = x[7];
                z[9] = x[8];
            }
            else if (a == 3)
            {
                z[0] = x[0];
                z[1] = x[1];
                z[2] = x[2];
                z[3] = pts;
                z[4] = x[3];
                z[5] = x[4];
                z[6] = x[5];
                z[7] = x[6];
                z[8] = x[7];
                z[9] = x[8];
            }
            else if (a == 4)
            {
                z[0] = x[0];
                z[1] = x[1];
                z[2] = x[2];
                z[3] = x[3];
                z[4] = pts;
                z[5] = x[4];
                z[6] = x[5];
                z[7] = x[6];
                z[8] = x[7];
                z[9] = x[8];
            }
            else if (a == 5)
            {
                z[0] = x[0];
                z[1] = x[1];
                z[2] = x[2];
                z[3] = x[3];
                z[4] = x[4];
                z[5] = pts;
                z[6] = x[5];
                z[7] = x[6];
                z[8] = x[7];
                z[9] = x[8];
            }
            else if (a == 6)
            {
                z[0] = x[0];
                z[1] = x[1];
                z[2] = x[2];
                z[3] = x[3];
                z[4] = x[4];
                z[5] = x[5];
                z[6] = pts;
                z[7] = x[6];
                z[8] = x[7];
                z[9] = x[8];
            }
            else if (a == 7)
            {
                z[0] = x[0];
                z[1] = x[1];
                z[2] = x[2];
                z[3] = x[3];
                z[4] = x[4];
                z[5] = x[5];
                z[6] = x[6];
                z[7] = pts;
                z[8] = x[7];
                z[9] = x[8];
            }
            else if (a == 8)
            {
                z[0] = x[0];
                z[1] = x[1];
                z[2] = x[2];
                z[3] = x[3];
                z[4] = x[4];
                z[5] = x[5];
                z[6] = x[6];
                z[7] = x[7];
                z[8] = pts;
                z[9] = x[8];
            }
            else if (a == 9)
            {
                z[0] = x[0];
                z[1] = x[1];
                z[2] = x[2];
                z[3] = x[3];
                z[4] = x[4];
                z[5] = x[5];
                z[6] = x[6];
                z[7] = x[7];
                z[8] = x[8];
                z[9] = pts;
            }

            return z;
        }

        public void enterName(string y)
        {
            string[] x = hS.getNames();
            names = new string[10];

            if (index == 0)
            {
                names[0] = y;
                names[1] = x[0];
                names[2] = x[1];
                names[3] = x[2];
                names[4] = x[3];
                names[5] = x[4];
                names[6] = x[5];
                names[7] = x[6];
                names[8] = x[7];
                names[9] = x[8];
            }
            else if (index == 1)
            {
                names[0] = x[0];
                names[1] = y;
                names[2] = x[1];
                names[3] = x[2];
                names[4] = x[3];
                names[5] = x[4];
                names[6] = x[5];
                names[7] = x[6];
                names[8] = x[7];
                names[9] = x[8];
            }
            else if (index == 2)
            {
                names[0] = x[0];
                names[1] = x[1];
                names[2] = y;
                names[3] = x[2];
                names[4] = x[3];
                names[5] = x[4];
                names[6] = x[5];
                names[7] = x[6];
                names[8] = x[7];
                names[9] = x[8];
            }
            else if (index == 3)
            {
                names[0] = x[0];
                names[1] = x[1];
                names[2] = x[2];
                names[3] = y;
                names[4] = x[3];
                names[5] = x[4];
                names[6] = x[5];
                names[7] = x[6];
                names[8] = x[7];
                names[9] = x[8];
            }
            else if (index == 4)
            {
                names[0] = x[0];
                names[1] = x[1];
                names[2] = x[2];
                names[3] = x[3];
                names[4] = y;
                names[5] = x[4];
                names[6] = x[5];
                names[7] = x[6];
                names[8] = x[7];
                names[9] = x[8];
            }
            else if (index == 5)
            {
                names[0] = x[0];
                names[1] = x[1];
                names[2] = x[2];
                names[3] = x[3];
                names[4] = x[4];
                names[5] = y;
                names[6] = x[5];
                names[7] = x[6];
                names[8] = x[7];
                names[9] = x[8];
            }
            else if (index == 6)
            {
                names[0] = x[0];
                names[1] = x[1];
                names[2] = x[2];
                names[3] = x[3];
                names[4] = x[4];
                names[5] = x[5];
                names[6] = y;
                names[7] = x[6];
                names[8] = x[7];
                names[9] = x[8];
            }
            else if (index == 7)
            {
                names[0] = x[0];
                names[1] = x[1];
                names[2] = x[2];
                names[3] = x[3];
                names[3] = x[3];
                names[4] = x[4];
                names[5] = x[5];
                names[6] = x[6];
                names[7] = y;
                names[8] = x[7];
                names[9] = x[8];
            }
            else if (index == 8)
            {
                names[0] = x[0];
                names[1] = x[1];
                names[2] = x[2];
                names[3] = x[3];
                names[4] = x[4];
                names[5] = x[5];
                names[6] = x[6];
                names[7] = x[7];
                names[8] = y;
                names[9] = x[8];
            }
            else if (index == 9)
            {
                names[0] = x[0];
                names[1] = x[1];
                names[2] = x[2];
                names[3] = x[3];
                names[4] = x[4];
                names[5] = x[5];
                names[6] = x[6];
                names[7] = x[7];
                names[8] = x[8];
                names[9] = y;
            }
        }

        public void pushDataAndSave(StorageDevice device)
        {
            hS.setPoints(highScoreTemp);
            hS.setNames(names);
            hS.saveGame(device);
        }

        public void LoadData(StorageDevice device)
        {
            hS.loadGame(device);
        }

        public void loadDefault()
        {
            hS.loadDefault();
        }

        public HighScore getHighScore()
        {
            return hS;
        }

        public void setHighScore(HighScore high)
        {
            hS = high;
        }

        public OnScreenInfo getOSi()
        {
            return oSi;
        }

        public GameObjects getGameO()
        {
            return gameO;
        }
        public void putOne()
        {
            oSi.setOneUp(getGameO().getState().getEngine());
        }
    }
}
