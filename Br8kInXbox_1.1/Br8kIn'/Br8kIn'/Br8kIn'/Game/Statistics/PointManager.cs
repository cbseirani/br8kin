/******************************************
 * 06/16/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  PointManager.cs 
 * 
 *****************************************/
using Microsoft.Xna.Framework;

namespace Br8kIn_
{
    class PointManager
    {
        //instance variables
        int numOfPts;
        int levelNum;
        int lifeCounter, pointCounter, timeBonus, bonusLives;
        int numOfLives;
        short multiplier;
        Timer levelTime;
        Timer totalElapsedTime;
        StatsManager stats;
        string timeB;

        const int lifeC1 = 10000;
        const int lifeC2 = 50000;

        /*****************************************/

        //constructor
        public PointManager()
        {
            numOfPts = 0;
            numOfLives = 3;
            lifeCounter = lifeC1;
            levelTime = new Timer(1000f, 0, 1);
            totalElapsedTime = new Timer(1000f, 0, 1);
            pointCounter = 0;
            timeBonus = 0;
            bonusLives = 0;
        }

        /*****************************************/

        //load
        public void Load(int lvl, StatsManager statsM)
        {
            levelNum = lvl;
            levelTime = new Timer(1000f, 0, 1);
            pointCounter = 0;
            stats = statsM;
            timeBonus = 0;
            bonusLives = 0;
        }

        /*****************************************/

        //update
        public void Update(GameTime gT)
        {
            //update timers
            levelTime.Update(gT.ElapsedGameTime.Milliseconds);
            totalElapsedTime.Update(gT.ElapsedGameTime.Milliseconds);

            //add extra life if necessary
            if (lifeCounter <= 0)
            {
                if (levelNum < 11)
                {
                    lifeCounter += lifeC1;
                }
                else
                {
                    lifeCounter += lifeC2;
                }
                addLife();
            }
        }

        /*****************************************/

        public void addPoint(short type)
        {
            if (!stats.getGameOver() || !stats.getGameO().getPlayerM().getBeatLevel())
            {
                switch (type)
                {
                    //crack plates + pots
                    case 1:
                        numOfPts += (10 * levelNum);
                        pointCounter += (10 * levelNum);
                        lifeCounter -= (10 * levelNum);
                        break;

                    //break plates + pots
                    case 2:
                        numOfPts += (100 * levelNum);
                        pointCounter += (100 * levelNum);
                        lifeCounter -= (100 * levelNum);
                        break;

                    //break large clouds
                    case 3:
                        numOfPts += (10 * levelNum);
                        pointCounter += (10 * levelNum);
                        lifeCounter -= (10 * levelNum);
                        break;

                    //break small clouds
                    case 4:
                        numOfPts += (100 * levelNum);
                        pointCounter += (100 * levelNum);
                        lifeCounter -= (100 * levelNum);
                        break;

                    //crack the planet
                    case 5:
                        numOfPts += (10 * levelNum);
                        pointCounter += (10 * levelNum);
                        lifeCounter -= (10 * levelNum);
                        break;

                    //destroy the planet
                    case 6:
                        numOfPts += (100 * levelNum);
                        pointCounter += (100 * levelNum);
                        lifeCounter -= (100 * levelNum);
                        break;

                    //break alien
                    case 7:
                        numOfPts += (50 * levelNum);
                        pointCounter += (50 * levelNum);
                        lifeCounter -= (50 * levelNum);
                        break;

                    //crack teeth
                    case 8:
                        numOfPts += (10 * levelNum);
                        pointCounter += (10 * levelNum);
                        lifeCounter -= (10 * levelNum);
                        break;

                    //break teeth shards
                    case 9:
                        numOfPts += (100 * levelNum);
                        pointCounter += (100 * levelNum);
                        lifeCounter -= (100 * levelNum);
                        break;
                }
            }
        }
        public int getNumOfPoints()
        {
            return numOfPts;
        }
        public int getNumOfLives()
        {
            return numOfLives;
        }
        public void addLife()
        {
            numOfLives++;
            stats.putOne();
        }
        public void setLives(short x)
        {
            numOfLives = x;
        }
        public void delLife()
        {
            numOfLives--;
        }
        public int getElapsedTime()
        {
            return levelTime.returnSeconds();
        }
        public int getPointCounter()
        {
            return pointCounter;
        }

        /*****************************************/

        public int getTimeBonus()
        {
            timeBonus = 0;
            timeB = "";
            bonusLives = 0;

            //IF time is between 0:20-0:30
            if (stats.getOSi().getSec() <= 30 && stats.getOSi().getMin() <= 0)
            {
                timeB = "x4";
                timeBonus = pointCounter * 4;
                numOfPts += pointCounter * 3;

                //calculate bonus lives
                if (levelNum >= 14 && levelNum <= 16)
                {
                    bonusLives = 3;
                    numOfLives += 3;
                }
                else
                {
                    bonusLives = 1;
                    numOfLives += 1;
                }
            }

            //IF time is between 0:31-0:40
            else if (stats.getOSi().getSec() <= 40 && stats.getOSi().getMin() <= 0)
            {
                timeB = "x4";
                timeBonus = pointCounter * 4;
                numOfPts += pointCounter * 3;

                if(levelNum >= 14 && levelNum <= 16)
                {
                    bonusLives = 3;
                    numOfLives += 3;
                }
                else
                {
                    bonusLives = 1;
                    numOfLives += 1;
                }
            }
            //IF time is between 0:41-0:50
            else if (stats.getOSi().getSec() <= 50 && stats.getOSi().getMin() <= 0)
            {
                timeB = "x3";
                timeBonus = pointCounter * 3;
                numOfPts += pointCounter * 2;

                //calculate bonus lives
                if (levelNum <= 6)
                {
                    bonusLives = 0;
                    numOfLives += 0;
                }
                else if (levelNum <= 13 && (levelNum != 10 || levelNum != 20))
                {
                    bonusLives = 1;
                    numOfLives += 1;
                }
                else if (levelNum >= 14 && levelNum <= 16)
                {
                    bonusLives = 2;
                    numOfLives += 2;
                }
                else
                {
                    bonusLives = 1;
                    numOfLives += 1;
                }
            }          
            //IF time is between 0:51-1:00
            else if (stats.getOSi().getMin() <= 1 && stats.getOSi().getSec() <= 0)
            {
                timeB = "x2";
                timeBonus = pointCounter * 2;
                numOfPts += pointCounter * 1;

                //calculate bonus lives
                if(levelNum >= 14 && levelNum <= 16)
                {
                    bonusLives = 1;
                    numOfLives += 1;
                }
                else
                {
                    bonusLives = 0;
                    numOfLives += 0;
                }
            }
            //IF time is anything else
            else
            {
                timeB = "x1";
                timeBonus = pointCounter * 1;

                //calculate bonus lives
                if (levelNum <= 6)
                {
                    bonusLives = 0;
                    numOfLives += 0;
                }
                else if (levelNum <= 10)
                {
                    bonusLives = 0;
                    numOfLives += 0;
                }
                else
                {
                    bonusLives = 0;
                    numOfLives += 0;
                }
            }

            return timeBonus;
        }
        public string getTimeBonusSt()
        {
            return timeB;            
        }
        public int getBonusLives()
        {
            return bonusLives;
        }

        /*****************************************/
        /*****************************************/
        /****************************************
            //IF time is between 0:30-0:50
            else if (stats.getOSi().getSec() <= 50 && stats.getOSi().getMin() <= 0)
            {
                timeB = "x3";
                timeBonus = pointCounter * 3;
                numOfPts += pointCounter * 2;

                //calculate bonus lives//calculate bonus lives
                if (levelNum <= 6)
                {
                    bonusLives = 0;
                    numOfLives += 0;
                }
                else if (levelNum <= 10)
                {
                    bonusLives = 1;
                    numOfLives += 1;
                }
                else
                {
                    bonusLives = 2;
                    numOfLives += 2;
                }
            }
            //IF time is between 0:50-1:00
            else if (stats.getOSi().getMin() <= 1 && stats.getOSi().getSec() <= 0)
            {
                timeB = "x2";
                timeBonus = pointCounter * 2;
                numOfPts += pointCounter * 1;

                //calculate bonus lives//calculate bonus lives
                if (levelNum <= 6)
                {
                    bonusLives = 0;
                    numOfLives += 0;
                }
                else if (levelNum <= 10)
                {
                    bonusLives = 0;
                    numOfLives += 0;
                }
                else
                {
                    bonusLives = 1;
                    numOfLives += 1;
                }
            }
            //IF time is between 0:50-1:00
            else if (stats.getOSi().getMin() <= 1 && stats.getOSi().getSec() <= 0)
            {
                timeB = "x2";
                timeBonus = pointCounter * 2;
                numOfPts += pointCounter * 1;

                //calculate bonus lives//calculate bonus lives
                if (levelNum <= 6)
                {
                    bonusLives = 0;
                    numOfLives += 0;
                }
                else if (levelNum <= 10)
                {
                    bonusLives = 0;
                    numOfLives += 0;
                }
                else
                {
                    bonusLives = 1;
                    numOfLives += 1;
                }
            }*/
        /*****************************************/
    }
}
