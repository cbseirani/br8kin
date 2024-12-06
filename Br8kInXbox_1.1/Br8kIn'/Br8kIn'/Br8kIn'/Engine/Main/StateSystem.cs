/******************************************
 * 03/20/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  StateSystem.cs 
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
    class StateSystem
    {
        //instance variables
        Engine engine;

        //main objects
        IntroScreen introScreen;
        TitleScreen titleScreen;
        GameObjects gameObjects;
        DisplayPane displayPane;
        HighScore highScore;
        Texture2D warn, thanks, congratz, trial;

        //xbox-specific objects
        SignedInGamerCollection sIgC;
        Object stateobj;
        StorageDevice device;
        IAsyncResult result;
        bool first = true;
        short wait, time, countTut;


        //flags
        bool keyRelease, controllerSelected,
            storageDeviceSelected, cont, thanksB,
            aboutTitle, congratzB, signedIn, contDis,
            trialB, active, startTime, isPlaying;

        /*****************************************/

        //constructor
        public StateSystem(Engine engine)
        {
            //instantiate new game objects 
            this.engine = engine;
            introScreen = new IntroScreen(this);
            titleScreen = new TitleScreen(this);
            gameObjects = new GameObjects(this);
            displayPane = new DisplayPane(this);
            highScore = new HighScore(this);

            //initialize flags
            keyRelease = true;
            controllerSelected = false;
            storageDeviceSelected = false;
            cont = false;
            thanksB = false;
            aboutTitle = false;
            congratzB = false;
            signedIn = true;
            wait = 20;
            time = 0;
            contDis = false;
            trialB = false;
            active = false;
            countTut = 10;
            startTime = false;
            isPlaying = false;

        }

        /*****************************************/

        //load
        public void LoadContent()
        {
            //load game objects
            introScreen.LoadContent(engine, engine.getWindowSize());
            titleScreen.LoadContent(engine, engine.getWindowSize());
            gameObjects.LoadContent(engine, engine.getWindowSize());
            displayPane.Load(engine);
            warn = engine.Content.Load<Texture2D>(@"Menus\storageWarning");
            thanks = engine.Content.Load<Texture2D>(@"Menus\thanks");
            congratz = engine.Content.Load<Texture2D>(@"Menus\congratz");
            trial = engine.Content.Load<Texture2D>(@"Menus\trial");
        }

        /*****************************************/

        //update
        public void Update(GameTime gameTime, Rectangle clientBounds,
            GamePadState g, Engine engine)
        {
            if (!gameObjects.lvlManager().getTrans())
            {
                //check if controller disconnected
                if (g.IsConnected)
                {
                    contDis = false;
                }
                else
                {
                    contDis = true;
                }
            }

            //IF user is at intro video
            if (introScreen.getIsIntro())
            {
                //update intro video
                introScreen.Update(gameTime, clientBounds, g);
                gameObjects.getSmanager().volumeDown();

            }
            //ELSE user is in game
            else
            {
                if (signedIn)
                {
                    if (controllerSelected)
                    {
                        if (cont)
                        {
                            if (Guide.IsVisible)
                            {
                                gameObjects.getPlayerM().vibeOff();
                            }
                            else if (aboutTitle)
                            {
                                if (wait > 0)
                                {
                                    wait--;
                                }
                                else
                                {
                                    wait = 20;
                                    uTitle();
                                }
                            }
                            //IF user is in the menu system
                            else if (displayPane.getInMenu())
                            {
                                if (displayPane.getMainMenu())
                                {
                                    titleScreen.setFirst(true);
                                    displayPane.UpdateM(g, gameTime, gameObjects);
                                }
                                else
                                {
                                    //update menu system
                                    displayPane.Update(g, gameTime);
                                }
                            }
                            //ELSE user is playing
                            else
                            {
                                //IF user is at title screen
                                if (titleScreen.getIsTitle())
                                {
                                    titleScreen.Update(gameTime, clientBounds, g, gameObjects);
                                    if (!isPlaying && displayPane.getSound())
                                    {
                                        gameObjects.getSmanager().playMusic(0, 0);
                                        isPlaying = true;
                                    }

                                    if (startTime)
                                    {
                                        if (countTut > 0)
                                        {
                                            countTut--;
                                        }
                                        else
                                        {
                                            startTime = false;
                                            countTut = 10;
                                        }
                                    }
                                }

                                if (!displayPane.getLvlEnd())
                                {
                                    //IF user is in game and not in level transition
                                    if (!titleScreen.getIsTitle())
                                    {
                                        //Pause
                                        if (contDis || Keyboard.GetState().IsKeyDown(Keys.Escape) || g.IsButtonDown(Buttons.Start))
                                        {
                                            //IF key release flag is true
                                            if (contDis || (keyRelease && !gameObjects.getNoP()))
                                            {
                                                displayPane.setInMenu(true);
                                                displayPane.setPause(true);
                                                gameObjects.getPlayerM().vibeOff();
                                                keyRelease = false;
                                            }
                                        }
                                        else if (Keyboard.GetState().IsKeyUp(Keys.A))
                                        {
                                            keyRelease = true;
                                        }
                                    }
                                    else
                                    {
                                        //MainMenu
                                        if ((Keyboard.GetState().IsKeyDown(Keys.Escape) || g.IsButtonDown(Buttons.Start)))
                                        {
                                            //IF key release flag is true
                                            if (keyRelease && titleScreen.getTut())
                                            {
                                                setIsTut(false);
                                                titleScreen.setTut(false);
                                                startTime = true;
                                            }
                                            else if (keyRelease && !gameObjects.getNoP())
                                            {
                                                if (!startTime)
                                                {
                                                    displayPane.setInMenu(true);
                                                    displayPane.setMainMenu(true);
                                                }
                                                gameObjects.getPlayerM().vibeOff();

                                            }
                                            keyRelease = false;
                                        }
                                        if (g.IsButtonUp(Buttons.Start))
                                        {
                                            keyRelease = true;
                                        }

                                    }
                                }

                                //update game objects
                                if (!titleScreen.getTut())
                                {
                                    gameObjects.Update(gameTime, clientBounds, g, displayPane);
                                }
                            }
                        }
                        //ELSE user is selecting storage device
                        else
                        {
                            //IF user is not signed in on controller
                            sIgC = Gamer.SignedInGamers;
                            signedIn = false;
                            for (int a = 0; a < sIgC.Count; a++)
                            {
                                switch (engine.getPlayer())
                                {
                                    case 1:
                                        if (Gamer.SignedInGamers[a].PlayerIndex == PlayerIndex.One)
                                        {
                                            signedIn = true;
                                        }
                                        break;
                                    case 2:
                                        if (Gamer.SignedInGamers[a].PlayerIndex == PlayerIndex.Two)
                                        {
                                            signedIn = true;
                                        }
                                        break;
                                    case 3:
                                        if (Gamer.SignedInGamers[a].PlayerIndex == PlayerIndex.Three)
                                        {
                                            signedIn = true;
                                        }
                                        break;
                                    case 4:
                                        if (Gamer.SignedInGamers[a].PlayerIndex == PlayerIndex.Four)
                                        {
                                            signedIn = true;
                                        }
                                        break;
                                }
                            }
                            if (!signedIn)
                            {
                                setControl(false);
                                setCont(false);
                                if (!Guide.IsVisible)
                                {
                                    Guide.ShowSignIn(1, false);
                                    signedIn = true;
                                }
                                return;
                            }


                            if (!Guide.IsVisible && first)
                            {
                                // Reset the device
                                first = false;
                                device = null;
                                stateobj = (Object)"GetDevice";
                                switch (engine.getPlayer())
                                {
                                    case 1:
                                        StorageDevice.BeginShowSelector(PlayerIndex.One,
                                            this.GetDevice, stateobj);
                                        break;

                                    case 2:
                                        StorageDevice.BeginShowSelector(PlayerIndex.Two,
                                            this.GetDevice, stateobj);
                                        break;

                                    case 3:
                                        StorageDevice.BeginShowSelector(PlayerIndex.Three,
                                            this.GetDevice, stateobj);
                                        break;

                                    case 4:
                                        StorageDevice.BeginShowSelector(PlayerIndex.Four,
                                            this.GetDevice, stateobj);
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }

        /*****************************************/

        //draw
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch,
            Rectangle clientBounds, GamePadState g)
        {
            //IF user is at intro video
            if (introScreen.getIsIntro())
            {
                //draw intro video
                introScreen.Draw(spriteBatch, clientBounds);
            }
            //ELSE user is playing
            else
            {
                if (!thanksB && !congratzB && !trialB)
                {
                    if (!controllerSelected)
                    {
                        spriteBatch.Draw(warn, new Vector2(0, 0), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .9f);
                    }

                    //IF user is in the menu system
                    if (displayPane.getInMenu())
                    {
                        //draw menu system
                        displayPane.Draw(spriteBatch);
                    }

                    //IF user is at title screen
                    if (titleScreen.getIsTitle())
                    {
                        //draw main menu
                        titleScreen.Draw(spriteBatch, clientBounds);
                    }

                    //draw game objects
                    gameObjects.Draw(gameTime, spriteBatch, clientBounds, displayPane);
                }
                else if (congratzB)
                {
                    spriteBatch.Draw(congratz, new Vector2(0, 0), null, Color.White, 0,
                        Vector2.Zero, 1f, SpriteEffects.None, .9f);
                }
                else if (trialB)
                {
                    spriteBatch.Draw(trial, new Vector2(0, 0), null, Color.White, 0,
                        Vector2.Zero, 1f, SpriteEffects.None, .9f);
                }
                else
                {
                    spriteBatch.Draw(thanks, new Vector2(0, 0), null, Color.White, 0,
                        Vector2.Zero, 1f, SpriteEffects.None, .9f);
                }
            }
        }


        /*****************************************/

        //method to start game
        public void uStart()
        {
            //turn off title screen
            titleScreen.setIsTitle(false);

            //start new game
            gameObjects.startNewGame();
        }

        //method to quit
        public void uQuit()
        {
            //exit program
            engine.Exit();
        }

        //method to return to title screen
        public void uTitle()
        {
            titleScreen = new TitleScreen(this);
            titleScreen.LoadContent(engine, engine.getWindowSize());
            titleScreen.setIsTitle(true);

            highScore = gameObjects.getStats().getHighScore();
            gameObjects.Unload();
            gameObjects = new GameObjects(this);
            gameObjects.LoadContent(engine, engine.getWindowSize());
            gameObjects.LoadTitle();
            gameObjects.getStats().setHighScore(highScore);

            displayPane.setInMenu(false);
            displayPane.setPause(false);
            aboutTitle = false;
            thanksB = false;
            trialB = false;
            congratzB = false;

            isPlaying = false;

        }

        /*****************************************/

        //method to return current engine
        public Engine getEngine()
        {
            return engine;
        }
        public GameObjects getGame()
        {
            return gameObjects;
        }

        //method to set if title screen is on or not
        public void setIsTitle(bool x)
        {
            titleScreen.setIsTitle(x);
        }
        public bool getIsTitle()
        {
            return titleScreen.getIsTitle();
        }

        public void saveHigh(StorageDevice device)
        {
            gameObjects.saveHighScore(device);
        }
        public void loadHigh(StorageDevice device)
        {
            gameObjects.loadHighScore(device);
        }

        /*****************************************/

        public void yesStorage()
        {
            storageDeviceSelected = true;
        }
        public void noStorage()
        {
            storageDeviceSelected = false;
            gameObjects.getStats().loadDefault();
        }
        public void setControl(bool x)
        {
            controllerSelected = x;
        }
        public bool getControl()
        {
            return controllerSelected;
        }
        public void setSignedIn(bool x)
        {
            signedIn = x;
        }
        public void setCont(bool x)
        {
            cont = x;
            if (!x)
            {
                first = true;
            }
        }
        public short getContNum()
        {
            return engine.getPlayer();
        }
        public void setThanks()
        {
            thanksB = true;
        }
        public void setCongratz()
        {
            congratzB = true;
        }
        public void setThanksT()
        {
            trialB = true;
        }
        public void setIsAboutTitle()
        {
            aboutTitle = true;
        }
        public bool getStorageSelection()
        {
            return storageDeviceSelected;
        }
        public StorageDevice getDeviceSel()
        {
            return device;
        }
        public void endIntro()
        {
            introScreen.stopVid();
            introScreen.setIsIntro(false);
            titleScreen.setIsTitle(true);
            gameObjects.LoadContent(engine, engine.getWindowSize());
            gameObjects.getSmanager().volumeUp();
        }
        public void GetDevice(IAsyncResult result)
        {
            device = StorageDevice.EndShowSelector(result);
            cont = true;
            if (device != null && device.IsConnected)
            {
                gameObjects.getStats().LoadData(device);
            }
            else
            {
                gameObjects.getStats().loadDefault();
            }
        }
        public bool getIsIntro()
        {
            return introScreen.getIsIntro();
        }
        /************************/
        public void setIsTut(bool x)
        {
            active = x;
        }
        /************************/
        public bool getIsTut()
        {
            return titleScreen.getTut();
        }
        /************************/
        public void setIsActive(bool x)
        {
            active = x;
        }
        /************************/
        public void setTimer(short x)
        {
            time = x;
        }
    }
}
