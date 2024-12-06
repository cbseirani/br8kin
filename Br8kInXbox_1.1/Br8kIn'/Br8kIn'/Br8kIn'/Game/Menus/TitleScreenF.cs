/******************************************
 * 06/21/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  TitleScreenF.cs 
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
    class TitleScreenF
    {
        //instance variables
        TitleScreen tS;
        Tutorial tutorial;
        Texture2D title, buyNow, menu;
        Texture2D[] buttons;
        short userSelection;
        short currentButton, x, cheatSeq, y, c2, buyO;
        bool first, endCheat, wait;

        /*****************************************/


        //constructor
        public TitleScreenF(TitleScreen titleS, StateSystem stSystem)
        {
            tS = titleS;
            tutorial = new Tutorial(stSystem);
            buttons = new Texture2D[2];
            reset();
            first = true;
            x = 10;
            cheatSeq = 0;
            y = 10;
            wait = false;
           
            c2 = 10;
            buyO = 1;
            
        }


        /*****************************************/


        //load
        public void LoadContent(Engine engine, Rectangle clientBounds)
        {
            //load buttons
            title = engine.Content.Load<Texture2D>(@"Menus\Title\title");
            tutorial.LoadContent(engine, engine.getWindowSize());
            buttons[0] = engine.Content.Load<Texture2D>(@"Menus\Title\start1");
            buttons[1] = engine.Content.Load<Texture2D>(@"Menus\Title\quit1");
            buyNow = engine.Content.Load<Texture2D>(@"Menus\Title\buynow");
            menu = engine.Content.Load<Texture2D>(@"Menus\Title\menuOp");
        }


        /*****************************************/


        //update
        public void Update(GamePadState g, GameTime gameTime, GameObjects gameObjects,
            Rectangle clientBounds)
        {
            if (first)
            {
                if (Keyboard.GetState().IsKeyUp(Keys.Enter) && g.IsButtonUp(Buttons.A) && g.IsButtonUp(Buttons.B) && g.IsButtonUp(Buttons.Back))
                {
                    if (x > 0)
                    {
                        x--;
                    }
                    else
                    {
                        first = false;
                        x = 10;
                    }
                }
            }

            if (!first)
            {
                if (!getTut())
                {
                    //check for cheat code sequence
                    if (!endCheat)
                    {
                        checkCheat(g, gameObjects);
                    }
                    else
                    {
                        //set lives to 99
                        tS.getState().getGame().getStats().setLives();
                    }

                    //IF player if on left side of screen
                    if (gameObjects.getPlayerPos().X < (1280 / 2 + 5))
                    {
                        currentButton = 1;
                    }
                    //ELSE IF player if on right side of screen
                    else if (gameObjects.getPlayerPos().X > 1280 / 2 - 75)
                    {
                        currentButton = 2;
                    }

                    //IF user has selected start game
                    if ((currentButton == 1) && (Keyboard.GetState().IsKeyDown(Keys.Enter) || g.IsButtonDown(Buttons.A)))
                    {
                        userSelection = 1;
                    }
                    //ELSE IF user has selected quit
                    else if ((currentButton == 2) && (Keyboard.GetState().IsKeyDown(Keys.Enter) || g.IsButtonDown(Buttons.A)))
                    {
                        userSelection = 2;
                    }
                    //ELSE IF user has selected tutorial
                    if (g.IsButtonDown(Buttons.Back) && !tS.getState().getGame().lvlManager().getTrans())
                    {
                        setTut(true);
                        gameObjects.getPlayerM().vibeOff();
                        tS.getState().setIsTut(true);
                        if (getTut())
                        {
                            tutorial.Update(gameTime, clientBounds, g);
                            gameObjects.getSmanager().volumeDown();
                        }
                    }
                }
                else
                {
                    /*if (g.IsButtonDown(Buttons.Start))
                    {
                        setTut(false);
                        tS.getState().setIsTut(false);
                    }*/
                }
            }

            //IF trial mode, check if person select buy now
            if (Guide.IsTrialMode)
            {
                //flash buy now
                if (c2 > 0)
                {
                    c2--;
                }
                else
                {
                    c2 = 10;
                    buyO *= -1;
                }

                if (!first)
                {
                    //IF user presses b button to buy now
                    if (g.IsButtonDown(Buttons.B) && !getTut())
                    {
                        first = true;
                        SignedInGamer gamer = null;
                        switch (tS.getState().getEngine().getPlayer())
                        {
                            case 1:
                                gamer = Gamer.SignedInGamers[PlayerIndex.One];
                                break;
                            case 2:
                                gamer = Gamer.SignedInGamers[PlayerIndex.Two];
                                break;
                            case 3:
                                gamer = Gamer.SignedInGamers[PlayerIndex.Three];
                                break;
                            case 4:
                                gamer = Gamer.SignedInGamers[PlayerIndex.Four];
                                break;
                        }

                        if (gamer != null && gamer.Privileges.AllowPurchaseContent)
                        {
                            switch (tS.getState().getEngine().getPlayer())
                            {
                                case 1:
                                    Guide.ShowMarketplace(PlayerIndex.One);
                                    break;
                                case 2:
                                    Guide.ShowMarketplace(PlayerIndex.Two);
                                    break;
                                case 3:
                                    Guide.ShowMarketplace(PlayerIndex.Three);
                                    break;
                                case 4:
                                    Guide.ShowMarketplace(PlayerIndex.Four);
                                    break;
                            }
                        }
                    }
                }
            }
        }


        /*****************************************/


        //draw
        public void Draw(SpriteBatch spriteBatch, Rectangle cB)
        {
            //draw title
            spriteBatch.Draw(title, new Vector2(0, 25), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .3f);

            spriteBatch.Draw(menu, new Vector2(0, 25), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .3f);

            //draw START button
            if (currentButton <= 1)
                spriteBatch.Draw(buttons[0], new Vector2(0, 0), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .05f);
            else
                spriteBatch.Draw(buttons[1], new Vector2(0, 0), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .05f);

            //IF trial mode, draw buy now button
            if (Guide.IsTrialMode)
            {
                if (buyO > 0)
                {
                    spriteBatch.Draw(buyNow, new Vector2((1280 / 2) - (361 / 2), (720 / 2) - (52 / 2) + 100), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .6f);
                }
            }
            if (getTut())
            {
                tutorial.Draw(spriteBatch, cB);
            }

        }


        /*****************************************/


        //method to return user selection
        public short getSelection()
        {
            return userSelection;
        }

        //method to reset title screen buttons
        public void reset()
        {
            userSelection = -1;
            currentButton = 0;
            cheatSeq = 0;
            x = 10;
            y = 10;
        }

        /*****************************************/

        private void checkCheat(GamePadState g, GameObjects gameObjects)
        {
            //up
            if (cheatSeq == 0)
            {
                if (g.IsButtonDown(Buttons.DPadUp))
                {
                    cheatSeq++;
                }
            }
            //down
            if (cheatSeq == 1)
            {
                if (g.IsButtonDown(Buttons.DPadDown))
                {
                    cheatSeq++;
                }
            }
            //left
            if (cheatSeq == 2)
            {
                if (g.IsButtonDown(Buttons.DPadLeft))
                {
                    cheatSeq++;
                }
            }
            //right
            if (cheatSeq == 3)
            {
                if (g.IsButtonDown(Buttons.DPadRight))
                {
                    cheatSeq++;
                }
            }
            //rb
            if (cheatSeq == 4)
            {
                if (g.IsButtonDown(Buttons.RightShoulder))
                {
                    cheatSeq++;
                }
            }
            //lb
            if (cheatSeq == 5)
            {
                if (g.IsButtonDown(Buttons.LeftShoulder))
                {
                    cheatSeq++;
                }
            }
            //rt
            if (cheatSeq == 6)
            {
                if (g.IsButtonDown(Buttons.RightTrigger))
                {
                    cheatSeq++;
                }
            }
            //lt
            if (cheatSeq == 7)
            {
                if (g.IsButtonDown(Buttons.LeftTrigger))
                {
                    cheatSeq++;
                }
            }
            //start
            if (cheatSeq == 8)
            {
                if (g.IsButtonDown(Buttons.Start))
                {
                    endCheat = true;
                    gameObjects.getSmanager().playSound("Bark");
                }
            }
        }

        public void setFirst(bool x)
        {
            first = x;
        }
        /**********************/
        public void setTut(bool x)
        {
            tutorial.setIsTut(x);
        }
        /**********************/
        public bool getTut()
        {
            return tutorial.getIsTut();
        }

    }
}
