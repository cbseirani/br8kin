/******************************************
 * 07/28/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  DisplayPane.cs 
 * 
 *****************************************/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Br8kIn_
{
    class DisplayPane
    {
        //instance variables
        StateSystem stateS;
        DisplayPaneF displayF;
        Timer selectTimer;

        /*****************************************/

        //constructor
        public DisplayPane(StateSystem sS)
        {
            stateS = sS;
            displayF = new DisplayPaneF(this);
            selectTimer = new Timer(90f, 0, 1);
        }

        /*****************************************/

        //load
        public void Load(Engine engine)
        {
            displayF.Load(engine);
        }

        /*****************************************/

        //update
        public void Update(GamePadState g, GameTime gameTime)
        {
            //check user input
            displayF.Update(checkInput(g, gameTime), g);
        }
        public void UpdateR(GamePadState g, GameTime gameTime, GameObjects gO)
        {
            //check user input
            displayF.UpdateR(checkInput(g, gameTime), g, gO);
        }
        public void UpdateG(GamePadState g, GameTime gameTime, GameObjects gO)
        {
            //check user input
            displayF.UpdateG(checkInput(g, gameTime), g, gO);
        }
        public void UpdateE(GamePadState g, GameTime gameTime, GameObjects gO)
        {
            //check user input
            displayF.UpdateE(checkInput(g, gameTime), g, gO, gameTime);
        }
        public void UpdateM(GamePadState g, GameTime gameTime, GameObjects gO)
        {
            //check user input
            displayF.UpdateM(checkInput(g, gameTime), g, gO, gameTime);
        }

        /*****************************************/

        //draw
        public void Draw(SpriteBatch sB)
        {
            displayF.Draw(sB);
        }

        /*****************************************/

        //method to check user input
        private short checkInput(GamePadState g, GameTime gameTime)
        {
            short upOrDown = 0;

            //IF user pressed down
            if (Keyboard.GetState().IsKeyDown(Keys.Down) || g.IsButtonDown(Buttons.DPadDown) ||
                 g.ThumbSticks.Left.Y < -.3 || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                //IF timer is not enabled
                if (!selectTimer.isEnabled())
                {
                    upOrDown = 1;
                    selectTimer.setEnable(true);
                }
                //ELSE timer is enabled
                else
                {
                    //IF timer is less than a decent time to slow down selection    
                    if (selectTimer.returnSeconds() < 1)
                    {
                        selectTimer.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                    }
                    //ELSE 
                    else
                    {
                        selectTimer = new Timer(90f, 0, 1);
                    }
                }
            }

            //IF user pressed up
            if (Keyboard.GetState().IsKeyDown(Keys.Up) || g.IsButtonDown(Buttons.DPadUp) ||
                    g.ThumbSticks.Left.Y > .3 || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                //IF timer is not enabled
                if (!selectTimer.isEnabled())
                {
                    upOrDown = -1;
                    selectTimer.setEnable(true);
                }
                //ELSE timer is enabled
                else
                {
                    //IF timer is less than a decent time to slow down selection    
                    if (selectTimer.returnSeconds() < 1)
                    {
                        selectTimer.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                    }
                    //ELSE 
                    else
                    {
                        selectTimer = new Timer(90f, 0, 1);
                    }
                }
            }

            return upOrDown;
        }

        /*****************************************/

        public void setPause(bool x)
        {
            displayF.setPause(x);
        }
        public void setStorage(bool x)
        {
            displayF.setHighScore(x);
        }
        public void setCredits(bool x)
        {
            displayF.setCredits(x);
        }
        public void setHs(bool x)
        {
            displayF.setHighScore(x);
        }
        public void setOptions(bool x)
        {
            displayF.setOptions(x);
        }
        public bool getInMenu()
        {
            return displayF.getInMenu();
        }
        public void setInMenu(bool x)
        {
            displayF.setInMenu(x);
        }
        public void setLvlEnd(bool x)
        {
            displayF.setNextLevel(x);
        }
        public bool getLvlEnd()
        {
            return displayF.getNextLevel();
        }

        public bool getVibration()
        {
            return displayF.getVibration();
        }
        public bool getSound()
        {
            return displayF.getSound();
        }
        public void getToTitle()
        {
            stateS.setThanks();
            stateS.setIsAboutTitle();
        }
        public void quitGame()
        {
            stateS.uQuit();
        }
        public StateSystem getStSys()
        {
            return stateS;
        }
        public void setGameOver(bool x)
        {
            displayF.setGameOver(x);
        }
        public bool getGameOver()
        {
            return displayF.getGameOver();
        }
        public void setEntry(bool x)
        {
            displayF.setEntry(x);
        }
        public bool getEntry()
        {
            return displayF.getEntry();
        }
        public void setMainMenu(bool x)
        {
            displayF.setMainMenu(x);
        }
        public bool getMainMenu()
        {
            return displayF.getMainMenuO();
        }
        public bool getPaddle()
        {
            return displayF.getPaddle();
        }
        public bool getBall()
        {
            return displayF.getBall();
        }
    }




    /*****************************************/
}
