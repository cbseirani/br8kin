/******************************************
 * 07/28/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  DisplayPaneF.cs 
 * 
 *****************************************/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Br8kIn_
{
    class DisplayPaneF
    {
        //instance variables
        DisplayPane displayP;
        Texture2D continueB, backB, back,
            selectB, backgroundPaneL, backgroundPaneS, pauseI, warnI,
            highScoreI, resultsI, creditsI, optionsI, shadow, quitMsg,
            gameO, enterT, selector, menuI;
        Texture2D[] pauseMenu, optionsMenu, onB, offB, chars, mainMenu;
        SpriteFont font;
        bool options, highScore, credits, nextLevel, pause,
            warn, pauseT, nextT, warnT, hT, storageWarn,
            quitWarn, creditsT, soundOn, vibrationOn,
            titleScreen, quit, paused, inMenu, ok2Chek, gameOver,
            entry, mainMen, mainMen2, paddleOn, ballOn;
        short cP, cO, cOnOff, count1, count2, count3, count4, c2, selectO, wait, cM;
        string[] hS, charList;
        Vector2[] posis;
        Timer selectTimer;

        /*****************************************/

        //constructor
        public DisplayPaneF(DisplayPane dP)
        {
            displayP = dP;
            soundOn = true;
            vibrationOn = true;
            titleScreen = false;
            paddleOn = true;
            ballOn = true;
            paused = false;
            quit = false;
            gameOver = false;
            mainMen = false;
            inMenu = false;
            entry = false;
            reset();
            ok2Chek = false;
            hS = new string[10];
            count1 = 0;
            count2 = 0;
            count3 = 0;
            count4 = 0;
            c2 = 10;
            cM = 0;
            wait = 20;
            selectO = 1;
            selectTimer = new Timer(90f, 0, 1);
        }

        /*****************************************/

        //load
        public void Load(Engine engine)
        {
            font = engine.Content.Load<SpriteFont>("SpriteFont1");
            back = engine.Content.Load<Texture2D>(@"Menus\back");

            //load button images
            continueB = engine.Content.Load<Texture2D>(@"Menus\Buttons\contB");
            backB = engine.Content.Load<Texture2D>(@"Menus\Buttons\backB");
            selectB = engine.Content.Load<Texture2D>(@"Menus\Buttons\selectB");

            //load title images
            pauseI = engine.Content.Load<Texture2D>(@"Menus\Titles\pauseT");
            warnI = engine.Content.Load<Texture2D>(@"Menus\Titles\warnT");
            highScoreI = engine.Content.Load<Texture2D>(@"Menus\Titles\highScoreT");
            resultsI = engine.Content.Load<Texture2D>(@"Menus\Titles\resultsT");
            creditsI = engine.Content.Load<Texture2D>(@"Menus\credits");
            optionsI = engine.Content.Load<Texture2D>(@"Menus\Titles\optionsT");
            shadow = engine.Content.Load<Texture2D>(@"Menus\shadow");
            quitMsg = engine.Content.Load<Texture2D>(@"Menus\quitMsg");
            gameO = engine.Content.Load<Texture2D>(@"Menus\Titles\gameOver");
            enterT = engine.Content.Load<Texture2D>(@"Menus\Titles\enterTitle");
            selector = engine.Content.Load<Texture2D>(@"Menus\Chars\selector");

            //load background pane
            backgroundPaneL = engine.Content.Load<Texture2D>(@"Menus\displayPane");
            backgroundPaneS = engine.Content.Load<Texture2D>(@"Menus\smallPane");

            //load pause menu choices
            pauseMenu = new Texture2D[5];
            pauseMenu[0] = engine.Content.Load<Texture2D>(@"Menus\Pause\pause01");
            pauseMenu[1] = engine.Content.Load<Texture2D>(@"Menus\Pause\pause02");
            pauseMenu[2] = engine.Content.Load<Texture2D>(@"Menus\Pause\pause03");
            pauseMenu[3] = engine.Content.Load<Texture2D>(@"Menus\Pause\pause04");
            pauseMenu[4] = engine.Content.Load<Texture2D>(@"Menus\Pause\pause05");

            //load option menu choices
            optionsMenu = new Texture2D[4];
            optionsMenu[0] = engine.Content.Load<Texture2D>(@"Menus\Options\options01");
            optionsMenu[1] = engine.Content.Load<Texture2D>(@"Menus\Options\options02");
            optionsMenu[2] = engine.Content.Load<Texture2D>(@"Menus\Options\options03");
            optionsMenu[3] = engine.Content.Load<Texture2D>(@"Menus\Options\options04");
            onB = new Texture2D[2];
            onB[0] = engine.Content.Load<Texture2D>(@"Menus\Options\on01");
            onB[1] = engine.Content.Load<Texture2D>(@"Menus\Options\on02");
            offB = new Texture2D[2];
            offB[0] = engine.Content.Load<Texture2D>(@"Menus\Options\off01");
            offB[1] = engine.Content.Load<Texture2D>(@"Menus\Options\off02");

            //load main menu
            mainMenu = new Texture2D[4];
            menuI = engine.Content.Load<Texture2D>(@"Menus\menuT");
            mainMenu[0] = engine.Content.Load<Texture2D>(@"Menus\menu01");
            mainMenu[1] = engine.Content.Load<Texture2D>(@"Menus\menu02");
            mainMenu[2] = engine.Content.Load<Texture2D>(@"Menus\menu03");
            mainMenu[3] = engine.Content.Load<Texture2D>(@"Menus\menu04");

            //load chars
            chars = new Texture2D[26];
            chars[0] = engine.Content.Load<Texture2D>(@"Menus\Chars\a");
            chars[1] = engine.Content.Load<Texture2D>(@"Menus\Chars\b");
            chars[2] = engine.Content.Load<Texture2D>(@"Menus\Chars\c");
            chars[3] = engine.Content.Load<Texture2D>(@"Menus\Chars\d");
            chars[4] = engine.Content.Load<Texture2D>(@"Menus\Chars\e");
            chars[5] = engine.Content.Load<Texture2D>(@"Menus\Chars\f");
            chars[6] = engine.Content.Load<Texture2D>(@"Menus\Chars\g");
            chars[7] = engine.Content.Load<Texture2D>(@"Menus\Chars\h");
            chars[8] = engine.Content.Load<Texture2D>(@"Menus\Chars\i");
            chars[9] = engine.Content.Load<Texture2D>(@"Menus\Chars\j");
            chars[10] = engine.Content.Load<Texture2D>(@"Menus\Chars\k");
            chars[11] = engine.Content.Load<Texture2D>(@"Menus\Chars\l");
            chars[12] = engine.Content.Load<Texture2D>(@"Menus\Chars\m");
            chars[13] = engine.Content.Load<Texture2D>(@"Menus\Chars\n");
            chars[14] = engine.Content.Load<Texture2D>(@"Menus\Chars\o");
            chars[15] = engine.Content.Load<Texture2D>(@"Menus\Chars\p");
            chars[16] = engine.Content.Load<Texture2D>(@"Menus\Chars\q");
            chars[17] = engine.Content.Load<Texture2D>(@"Menus\Chars\r");
            chars[18] = engine.Content.Load<Texture2D>(@"Menus\Chars\s");
            chars[19] = engine.Content.Load<Texture2D>(@"Menus\Chars\t");
            chars[20] = engine.Content.Load<Texture2D>(@"Menus\Chars\u");
            chars[21] = engine.Content.Load<Texture2D>(@"Menus\Chars\v");
            chars[22] = engine.Content.Load<Texture2D>(@"Menus\Chars\w");
            chars[23] = engine.Content.Load<Texture2D>(@"Menus\Chars\x");
            chars[24] = engine.Content.Load<Texture2D>(@"Menus\Chars\y");
            chars[25] = engine.Content.Load<Texture2D>(@"Menus\Chars\z");
            posis = new Vector2[3];
            posis[0] = new Vector2((1280 / 2) - (95 / 2) - 100, (720 / 2) - (105 / 2) + 10);
            posis[1] = new Vector2((1280 / 2) - (95 / 2), (720 / 2) - (105 / 2) + 10);
            posis[2] = new Vector2((1280 / 2) - (95 / 2) + 100, (720 / 2) - (105 / 2) + 10);
            charList = new string[26];
            charList[0] = "A";
            charList[1] = "B";
            charList[2] = "C";
            charList[3] = "D";
            charList[4] = "E";
            charList[5] = "F";
            charList[6] = "G";
            charList[7] = "H";
            charList[8] = "I";
            charList[9] = "J";
            charList[10] = "K";
            charList[11] = "L";
            charList[12] = "M";
            charList[13] = "N";
            charList[14] = "O";
            charList[15] = "P";
            charList[16] = "Q";
            charList[17] = "R";
            charList[18] = "S";
            charList[19] = "T";
            charList[20] = "U";
            charList[21] = "V";
            charList[22] = "W";
            charList[23] = "X";
            charList[24] = "Y";
            charList[25] = "Z";
        }

        /*****************************************/

        //update 
        public void Update(short upOrDown, GamePadState g)
        {
            if (ok2Chek)
            {
                checkInput(upOrDown, g);
            }

            if (!g.IsButtonDown(Buttons.A) && !g.IsButtonDown(Buttons.B) &&
                !g.IsButtonDown(Buttons.Start) && !g.IsButtonDown(Buttons.Back))
            {
                ok2Chek = true;
            }
            else
            {
                ok2Chek = false;
            }

        }
        public void UpdateR(short upOrDown, GamePadState g, GameObjects gO)
        {
            checkInputR(upOrDown, g, gO);
        }
        public void UpdateG(short upOrDown, GamePadState g, GameObjects gO)
        {
            if (wait > 0)
            {
                wait--;
            }
            else
            {
                checkInputG(upOrDown, g, gO);
            }
        }
        public void UpdateE(short upOrDown, GamePadState g, GameObjects gO, GameTime gT)
        {
            if (wait > 0)
            {
                wait--;
            }
            else
            {
                checkInputE(upOrDown, g, gO, gT);
            }
        }
        public void UpdateM(short upOrDown, GamePadState g, GameObjects gO, GameTime gT)
        {
            if (ok2Chek)
            {
                checkInputM(upOrDown, g, gO, gT);
            }

            if (!g.IsButtonDown(Buttons.A) && !g.IsButtonDown(Buttons.B) &&
                !g.IsButtonDown(Buttons.Start) && !g.IsButtonDown(Buttons.Back))
            {
                ok2Chek = true;
            }
            else
            {
                ok2Chek = false;
            }
        }

        /*****************************************/

        //draw
        public void Draw(SpriteBatch sB)
        {
            if (pause)
            {
                sB.Draw(back, new Vector2(0, 0), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .69f);

                //draw pause menu title
                sB.Draw(pauseI, new Vector2((1280 / 2) - (235 / 2), (720 / 2) - (75 / 2) - (170)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);

                //draw pause menu selection
                sB.Draw(pauseMenu[cP], new Vector2((1280 / 2) - (659 / 2) + 5, (720 / 2) - (410 / 2)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);

                //draw select/back buttons
                sB.Draw(selectB, new Vector2((1280 / 2) - (177 / 2) - (110), (720 / 2) - (55 / 2) + (170)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);
                sB.Draw(backB, new Vector2((1280 / 2) - (157 / 2) + (120), (720 / 2) - (55 / 2) + (170)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);

                //draw large displayPane
                sB.Draw(backgroundPaneL, new Vector2((1280 / 2) - (489 / 2), (720 / 2) - (484 / 2)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .7f);

                //display pane shadow
                sB.Draw(shadow, new Vector2((1280 / 2) - (992 / 2), (720 / 2) - (365 / 2) - (130)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .71f);
            }
            else if (highScore)
            {
                sB.Draw(back, new Vector2(0, 0), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .69f);

                //draw highscore title
                sB.Draw(highScoreI, new Vector2((1280 / 2) - (235 / 2) - 60, (720 / 2) - (75 / 2) - (170)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);

                //draw highscores
                sB.DrawString(font, hS[0], new Vector2((1280 / 2) - 125, (720 / 2) - 145), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);
                sB.DrawString(font, hS[1], new Vector2((1280 / 2) - 125, (720 / 2) - 115), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);
                sB.DrawString(font, hS[2], new Vector2((1280 / 2) - 125, (720 / 2) - 85), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);
                sB.DrawString(font, hS[3], new Vector2((1280 / 2) - 125, (720 / 2) - 55), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);
                sB.DrawString(font, hS[4], new Vector2((1280 / 2) - 125, (720 / 2) - 25), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);
                sB.DrawString(font, hS[5], new Vector2((1280 / 2) - 125, (720 / 2) + 5), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);
                sB.DrawString(font, hS[6], new Vector2((1280 / 2) - 125, (720 / 2) + 35), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);
                sB.DrawString(font, hS[7], new Vector2((1280 / 2) - 125, (720 / 2) + 65), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);
                sB.DrawString(font, hS[8], new Vector2((1280 / 2) - 125, (720 / 2) + 95), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);
                sB.DrawString(font, hS[9], new Vector2((1280 / 2) - 125, (720 / 2) + 125), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);

                //draw back button
                sB.Draw(backB, new Vector2((1280 / 2) - (157 / 2), (720 / 2) - (55 / 2) + (187)), null, Color.White, 0,
                        Vector2.Zero, 1f, SpriteEffects.None, .72f);

                //draw large displayPane
                sB.Draw(backgroundPaneL, new Vector2((1280 / 2) - (489 / 2), (720 / 2) - (484 / 2)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .7f);

                //display pane shadow
                sB.Draw(shadow, new Vector2((1280 / 2) - (992 / 2), (720 / 2) - (365 / 2) - (130)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .71f);
            }
            else if (nextLevel)
            {
                sB.Draw(back, new Vector2(0, 0), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .69f);

                //draw results title
                sB.Draw(resultsI, new Vector2((1280 / 2) - (235 / 2) - 5, (720 / 2) - (75 / 2) - (170)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);

                //draw total points + bonus
                sB.DrawString(font, hS[0], new Vector2((1280 / 2) - 125, (720 / 2) - 130), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);
                sB.DrawString(font, hS[1], new Vector2((1280 / 2) - 125, (720 / 2) - 100), Color.AntiqueWhite,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);

                sB.DrawString(font, hS[2], new Vector2((1280 / 2) - 125, (720 / 2) - 60), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);
                sB.DrawString(font, hS[3], new Vector2((1280 / 2) - 125, (720 / 2) - 30), Color.AntiqueWhite,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);

                sB.DrawString(font, hS[4], new Vector2((1280 / 2) - 125, (720 / 2) + 10), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);
                sB.DrawString(font, hS[5], new Vector2((1280 / 2) - 125, (720 / 2) + 40), Color.AntiqueWhite,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);

                sB.DrawString(font, hS[6], new Vector2((1280 / 2) - 125, (720 / 2) + 80), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);
                sB.DrawString(font, hS[7], new Vector2((1280 / 2) - 125, (720 / 2) + 110), Color.AntiqueWhite,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);

                //draw continue button
                sB.Draw(continueB, new Vector2((1280 / 2) - (157 / 2) - 40, (720 / 2) - (55 / 2) + (180)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);

                //draw large displayPane
                sB.Draw(backgroundPaneL, new Vector2((1280 / 2) - (489 / 2), (720 / 2) - (484 / 2)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .7f);

                //display pane shadow
                sB.Draw(shadow, new Vector2((1280 / 2) - (992 / 2), (720 / 2) - (365 / 2) - (130)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .71f);
            }
            else if (credits)
            {
                sB.Draw(back, new Vector2(0, 0), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .69f);

                //draw credits title
                sB.Draw(creditsI, new Vector2((1280 / 2) - (1280 / 2), (720 / 2) - (720 / 2)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);

                //draw back button
                sB.Draw(backB, new Vector2((1280 / 2) - (157 / 2), (720 / 2) - (55 / 2) + (180)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);

                //draw large displayPane
                sB.Draw(backgroundPaneL, new Vector2((1280 / 2) - (489 / 2), (720 / 2) - (484 / 2)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .7f);

                //display pane shadow
                sB.Draw(shadow, new Vector2((1280 / 2) - (992 / 2), (720 / 2) - (365 / 2) - (130)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .71f);
            }
            else if (options)
            {
                sB.Draw(back, new Vector2(0, 0), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .69f);

                //draw options title
                sB.Draw(optionsI, new Vector2((1280 / 2) - (235 / 2), (720 / 2) - (75 / 2) - (170)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);

                //draw options menu selection
                sB.Draw(optionsMenu[cO], new Vector2((1280 / 2) - (659 / 2), (720 / 2) - (410 / 2) + 30), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);

                if (cO == 0)
                {
                    //if paddle is on
                    if (paddleOn)
                    {
                        sB.Draw(onB[0], new Vector2((1280 / 2) - (70 / 2) + 153, (720 / 2) - (44 / 2) - 67), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }
                    else
                    {
                        sB.Draw(offB[0], new Vector2((1280 / 2) - (78 / 2) + 153, (720 / 2) - (46 / 2) - 67), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }

                    //if ball is on
                    if (ballOn)
                    {
                        sB.Draw(onB[1], new Vector2((1280 / 2) - (70 / 2) + 153, (720 / 2) - (44 / 2) - 16), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }
                    else
                    {
                        sB.Draw(offB[1], new Vector2((1280 / 2) - (78 / 2) + 153, (720 / 2) - (46 / 2) - 16), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }

                    //if sound is on
                    if (vibrationOn)
                    {
                        sB.Draw(onB[1], new Vector2((1280 / 2) - (70 / 2) + 153, (720 / 2) - (44 / 2) + 38), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }
                    else
                    {
                        sB.Draw(offB[1], new Vector2((1280 / 2) - (78 / 2) + 153, (720 / 2) - (46 / 2) + 38), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }

                    //if vibration is on
                    if (soundOn)
                    {
                        sB.Draw(onB[1], new Vector2((1280 / 2) - (70 / 2) + 153, (720 / 2) - (44 / 2) + 90), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }
                    else
                    {
                        sB.Draw(offB[1], new Vector2((1280 / 2) - (78 / 2) + 153, (720 / 2) - (46 / 2) + 90), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }
                }
                else if (cO == 1)
                {
                    //if paddle is on
                    if (paddleOn)
                    {
                        sB.Draw(onB[1], new Vector2((1280 / 2) - (70 / 2) + 153, (720 / 2) - (44 / 2) - 67), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }
                    else
                    {
                        sB.Draw(offB[1], new Vector2((1280 / 2) - (78 / 2) + 153, (720 / 2) - (46 / 2) - 67), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }

                    //if ball is on
                    if (ballOn)
                    {
                        sB.Draw(onB[0], new Vector2((1280 / 2) - (70 / 2) + 153, (720 / 2) - (44 / 2) - 16), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }
                    else
                    {
                        sB.Draw(offB[0], new Vector2((1280 / 2) - (78 / 2) + 153, (720 / 2) - (46 / 2) - 16), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }

                    //if sound is on
                    if (vibrationOn)
                    {
                        sB.Draw(onB[1], new Vector2((1280 / 2) - (70 / 2) + 153, (720 / 2) - (44 / 2) + 38), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }
                    else
                    {
                        sB.Draw(offB[1], new Vector2((1280 / 2) - (78 / 2) + 153, (720 / 2) - (46 / 2) + 38), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }

                    //if vibration is on
                    if (soundOn)
                    {
                        sB.Draw(onB[1], new Vector2((1280 / 2) - (70 / 2) + 153, (720 / 2) - (44 / 2) + 90), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }
                    else
                    {
                        sB.Draw(offB[1], new Vector2((1280 / 2) - (78 / 2) + 153, (720 / 2) - (46 / 2) + 90), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }
                }
                else if (cO == 2)
                {
                    //if paddle is on
                    if (paddleOn)
                    {
                        sB.Draw(onB[1], new Vector2((1280 / 2) - (70 / 2) + 153, (720 / 2) - (44 / 2) - 67), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }
                    else
                    {
                        sB.Draw(offB[1], new Vector2((1280 / 2) - (78 / 2) + 153, (720 / 2) - (46 / 2) - 67), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }

                    //if ball is on
                    if (ballOn)
                    {
                        sB.Draw(onB[1], new Vector2((1280 / 2) - (70 / 2) + 153, (720 / 2) - (44 / 2) - 16), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }
                    else
                    {
                        sB.Draw(offB[1], new Vector2((1280 / 2) - (78 / 2) + 153, (720 / 2) - (46 / 2) - 16), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }

                    //if sound is on
                    if (vibrationOn)
                    {
                        sB.Draw(onB[0], new Vector2((1280 / 2) - (70 / 2) + 153, (720 / 2) - (44 / 2) + 38), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }
                    else
                    {
                        sB.Draw(offB[0], new Vector2((1280 / 2) - (78 / 2) + 153, (720 / 2) - (46 / 2) + 38), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }

                    //if vibration is on
                    if (soundOn)
                    {
                        sB.Draw(onB[1], new Vector2((1280 / 2) - (70 / 2) + 153, (720 / 2) - (44 / 2) + 90), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }
                    else
                    {
                        sB.Draw(offB[1], new Vector2((1280 / 2) - (78 / 2) + 153, (720 / 2) - (46 / 2) + 90), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }
                }
                else if (cO == 3)
                {
                    //if paddle is on
                    if (paddleOn)
                    {
                        sB.Draw(onB[1], new Vector2((1280 / 2) - (70 / 2) + 153, (720 / 2) - (44 / 2) - 67), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }
                    else
                    {
                        sB.Draw(offB[1], new Vector2((1280 / 2) - (78 / 2) + 153, (720 / 2) - (46 / 2) - 67), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }

                    //if ball is on
                    if (ballOn)
                    {
                        sB.Draw(onB[1], new Vector2((1280 / 2) - (70 / 2) + 153, (720 / 2) - (44 / 2) - 16), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }
                    else
                    {
                        sB.Draw(offB[1], new Vector2((1280 / 2) - (78 / 2) + 153, (720 / 2) - (46 / 2) - 16), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }

                    //if sound is on
                    if (vibrationOn)
                    {
                        sB.Draw(onB[1], new Vector2((1280 / 2) - (70 / 2) + 153, (720 / 2) - (44 / 2) + 38), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }
                    else
                    {
                        sB.Draw(offB[1], new Vector2((1280 / 2) - (78 / 2) + 153, (720 / 2) - (46 / 2) + 38), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }

                    //if vibration is on
                    if (soundOn)
                    {
                        sB.Draw(onB[0], new Vector2((1280 / 2) - (70 / 2) + 153, (720 / 2) - (44 / 2) + 90), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }
                    else
                    {
                        sB.Draw(offB[0], new Vector2((1280 / 2) - (78 / 2) + 153, (720 / 2) - (46 / 2) + 90), null, Color.White, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, .73f);
                    }
                }

                //draw select/back buttons
                sB.Draw(selectB, new Vector2((1280 / 2) - (177 / 2) - (110), (720 / 2) - (55 / 2) + (170)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);
                sB.Draw(backB, new Vector2((1280 / 2) - (157 / 2) + (120), (720 / 2) - (55 / 2) + (170)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);

                //draw large displayPane
                sB.Draw(backgroundPaneL, new Vector2((1280 / 2) - (489 / 2), (720 / 2) - (484 / 2)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .7f);

                //display pane shadow
                sB.Draw(shadow, new Vector2((1280 / 2) - (992 / 2), (720 / 2) - (365 / 2) - (130)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .71f);
            }
            else if (storageWarn)
            {
                sB.Draw(back, new Vector2(0, 0), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .69f);

                //draw warn title
                sB.Draw(warnI, new Vector2(), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .6f);

                //draw storage warning message

                //draw continue/back buttons
                sB.Draw(continueB, new Vector2(), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .6f);
                sB.Draw(backB, new Vector2(), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .6f);

                //draw small displayPane
                sB.Draw(backgroundPaneS, new Vector2(1280 / 2, 720 / 2), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .6f);

                //display pane shadow
                sB.Draw(shadow, new Vector2(), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .6f);
            }
            else if (quitWarn)
            {
                sB.Draw(back, new Vector2(0, 0), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .69f);

                //draw warn title
                sB.Draw(warnI, new Vector2((1280 / 2) - (45 / 2), (720 / 2) - (80 / 2) - 80), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);

                //draw quit warning message
                sB.Draw(quitMsg, new Vector2((1280 / 2) - (429 / 2), (720 / 2) - (263 / 2)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);

                //draw continue/back buttons
                sB.Draw(continueB, new Vector2((1280 / 2) - (177 / 2) - (110), (720 / 2) - (55 / 2) + (90)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);
                sB.Draw(backB, new Vector2((1280 / 2) - (157 / 2) + (120), (720 / 2) - (55 / 2) + (90)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);

                //draw small displayPane
                sB.Draw(backgroundPaneS, new Vector2((1280 / 2) - (429 / 2), (720 / 2) - (263 / 2)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .7f);

                //display pane shadow
                sB.Draw(shadow, new Vector2((1280 / 2) - (992 / 2), (720 / 2) - (365 / 2) - 50), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .71f);
            }
            else if (gameOver)
            {
                sB.Draw(back, new Vector2(0, 0), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .69f);

                //draw gameover title
                sB.Draw(gameO, new Vector2((1280 / 2) - (330 / 2), (720 / 2) - (55 / 2) - 180), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);

                //draw highscores
                sB.DrawString(font, hS[0], new Vector2((1280 / 2) - 125, (720 / 2) - 145), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);
                sB.DrawString(font, hS[1], new Vector2((1280 / 2) - 125, (720 / 2) - 115), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);
                sB.DrawString(font, hS[2], new Vector2((1280 / 2) - 125, (720 / 2) - 85), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);
                sB.DrawString(font, hS[3], new Vector2((1280 / 2) - 125, (720 / 2) - 55), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);
                sB.DrawString(font, hS[4], new Vector2((1280 / 2) - 125, (720 / 2) - 25), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);
                sB.DrawString(font, hS[5], new Vector2((1280 / 2) - 125, (720 / 2) + 5), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);
                sB.DrawString(font, hS[6], new Vector2((1280 / 2) - 125, (720 / 2) + 35), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);
                sB.DrawString(font, hS[7], new Vector2((1280 / 2) - 125, (720 / 2) + 65), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);
                sB.DrawString(font, hS[8], new Vector2((1280 / 2) - 125, (720 / 2) + 95), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);
                sB.DrawString(font, hS[9], new Vector2((1280 / 2) - 125, (720 / 2) + 125), Color.Gold,
                        0f, new Vector2(0, 0), .9f, SpriteEffects.None, .73f);

                //draw continue button
                sB.Draw(continueB, new Vector2((1280 / 2) - (157 / 2) - 40, (720 / 2) - (55 / 2) + (190)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);

                //draw large displayPane
                sB.Draw(backgroundPaneL, new Vector2((1280 / 2) - (489 / 2), (720 / 2) - (484 / 2)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .7f);

                //display pane shadow
                sB.Draw(shadow, new Vector2((1280 / 2) - (992 / 2), (720 / 2) - (365 / 2) - (130)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .71f);
            }
            else if (entry)
            {
                sB.Draw(back, new Vector2(0, 0), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .69f);

                //display small panel
                sB.Draw(backgroundPaneS, new Vector2((1280 / 2) - (429 / 2), (720 / 2) - (263 / 2)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .7f);

                //display title
                sB.Draw(enterT, new Vector2((1280 / 2) - (292 / 2), (720 / 2) - (69 / 2) - 85), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);

                //display selection shadow
                if (selectO > 0)
                {
                    sB.Draw(selector, posis[count4], null, Color.White, 0,
                        Vector2.Zero, 1f, SpriteEffects.None, .72f);
                }

                //display 3 characters
                sB.Draw(chars[count1], posis[0], null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);
                sB.Draw(chars[count2], posis[1], null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);
                sB.Draw(chars[count3], posis[2], null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);

                //display continue button
                sB.Draw(continueB, new Vector2((1280 / 2) - (157 / 2) - 40, (720 / 2) - (55 / 2) + (90)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);
            }
            else if (mainMen)
            {
                sB.Draw(back, new Vector2(0, 0), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .69f);

                //draw large displayPane
                sB.Draw(backgroundPaneL, new Vector2((1280 / 2) - (489 / 2), (720 / 2) - (484 / 2)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .7f);

                //display pane shadow
                sB.Draw(shadow, new Vector2((1280 / 2) - (992 / 2), (720 / 2) - (365 / 2) - (130)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .71f);

                sB.Draw(menuI, new Vector2((1280 / 2) - (235 / 2) - 3, (720 / 2) - (75 / 2) - (170)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);

                sB.Draw(mainMenu[cM], new Vector2((1280 / 2) - (659 / 2), (720 / 2) - (410 / 2)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);

                //draw select/back buttons
                sB.Draw(selectB, new Vector2((1280 / 2) - (177 / 2) - (110), (720 / 2) - (55 / 2) + (170)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);
                sB.Draw(backB, new Vector2((1280 / 2) - (157 / 2) + (120), (720 / 2) - (55 / 2) + (170)), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .72f);

            }
        }

        /*****************************************/

        public void setHighScore(bool x)
        {
            highScore = x;
            if (x)
            {
                //get highscores to display
                hS = displayP.getStSys().getGame().getStats().getHS();
            }
        }
        public bool getHighScore(bool x)
        {
            return highScore;
        }
        public void setGameOver(bool x)
        {
            gameOver = x;
            if (x)
            {
                //get highscores to display
                hS = displayP.getStSys().getGame().getStats().getHS();
            }
        }
        public bool getGameOver()
        {
            return gameOver;
        }
        public void setPause(bool x)
        {
            pause = x;
        }
        public bool getPause(bool x)
        {
            return pause;
        }
        public void setNextLevel(bool x)
        {
            nextLevel = x;
            if (x)
            {
                //get highscores to display
                hS = displayP.getStSys().getGame().getStats().getPointString();
            }
        }
        public bool getNextLevel(bool x)
        {
            return nextLevel;
        }
        public void setCredits(bool x)
        {
            credits = x;
        }
        public bool getCredits(bool x)
        {
            return credits;
        }
        public void setOptions(bool x)
        {
            options = x;
        }
        public bool getOptions(bool x)
        {
            return options;
        }
        public void setStorage(bool x)
        {
            storageWarn = x;
        }
        public void setQuit(bool x)
        {
            quitWarn = x;
        }
        public void setInMenu(bool x)
        {
            inMenu = x;
        }
        public bool getNextLevel()
        {
            return nextLevel;
        }
        public bool getInMenu()
        {
            return inMenu;
        }
        public void setEntry(bool x)
        {
            entry = x;
        }
        public bool getEntry()
        {
            return entry;
        }
        public void setMainMenu(bool x)
        {
            mainMen = x;
        }
        public bool getMainMenuO()
        {
            bool too = false;

            if (mainMen)
            {
                too = true;
            }
            else if (mainMen2)
            {
                too = true;
            }

            return too;
        }
        private void reset()
        {
            //pane flags
            options = false;
            highScore = false;
            credits = false;
            nextLevel = false;
            pause = false;
            paused = false;
            entry = false;
            gameOver = false;
            quitWarn = false;
            storageWarn = false;
            titleScreen = false;
            mainMen = false;
            mainMen2 = false;
            quit = false;
            cP = 0;
            cO = 0;
            cM = 0;
            count1 = 0;
            count2 = 0;
            count3 = 0;
            count4 = 0;
            selectO = 1;
            c2 = 10;
            wait = 20;
            selectTimer = new Timer(90f, 0, 1);
        }

        /*****************************************/

        private void checkInput(short upOrDown, GamePadState g)
        {
            if (pause)
            {
                checkPause(upOrDown, g);
            }
            else if (highScore)
            {
                checkHs(upOrDown, g);
            }
            else if (credits)
            {
                checkCredits(upOrDown, g);
            }
            else if (options)
            {
                checkOp(upOrDown, g);
            }
            else if (storageWarn)
            {
                checkSw(upOrDown, g);
            }
            else if (quitWarn)
            {
                checkQw(upOrDown, g);
            }
        }
        private void checkInputR(short upOrDown, GamePadState g, GameObjects gO)
        {
            if (nextLevel)
            {
                checkResults(upOrDown, g, gO);
            }
        }
        private void checkInputG(short upOrDown, GamePadState g, GameObjects gO)
        {
            if (gameOver)
            {
                checkGameOver(upOrDown, g, gO);
            }
        }
        private void checkInputE(short upOrDown, GamePadState g, GameObjects gO, GameTime gT)
        {
            if (entry)
            {
                checkEntry(upOrDown, g, gO, gT);
            }
        }
        private void checkInputM(short upOrDown, GamePadState g, GameObjects gO, GameTime gT)
        {
            if (mainMen)
            {
                checkMainMenu(upOrDown, g, gO, gT);
            }
            else if (highScore)
            {
                checkHs(upOrDown, g);
            }
            else if (credits)
            {
                checkCredits(upOrDown, g);
            }
            else if (options)
            {
                checkOp(upOrDown, g);
            }
        }

        private void checkPause(short upOrDown, GamePadState g)
        {
            //if user moved up
            if (upOrDown > 0)
            {
                if (cP < 4)
                {
                    cP++;
                }
            }
            else if (upOrDown < 0)
            {
                if (cP > 0)
                {
                    cP--;
                }
            }

            //if user selects High Scores
            if ((cP == 0) && (Keyboard.GetState().IsKeyDown(Keys.Enter) || g.IsButtonDown(Buttons.A)))
            {
                pause = false;
                paused = true;
                highScore = true;
                hS = displayP.getStSys().getGame().getStats().getHS();
            }
            //if user selects Options
            if ((cP == 1) && (Keyboard.GetState().IsKeyDown(Keys.Enter) || g.IsButtonDown(Buttons.A)))
            {
                pause = false;
                paused = true;
                options = true;
            }
            //if user selects Credits
            if ((cP == 2) && (Keyboard.GetState().IsKeyDown(Keys.Enter) || g.IsButtonDown(Buttons.A)))
            {
                pause = false;
                paused = true;
                credits = true;
            }
            //if user selects Title Screen
            if ((cP == 3) && (Keyboard.GetState().IsKeyDown(Keys.Enter) || g.IsButtonDown(Buttons.A)))
            {
                pause = false;
                paused = true;
                quitWarn = true;
                titleScreen = true;
            }
            //if user selects Quit
            if ((cP == 4) && (Keyboard.GetState().IsKeyDown(Keys.Enter) || g.IsButtonDown(Buttons.A)))
            {
                pause = false;
                paused = true;
                quitWarn = true;
                quit = true;
            }

            //if user backs
            if ((Keyboard.GetState().IsKeyDown(Keys.Escape) || g.IsButtonDown(Buttons.B) ||
                g.IsButtonDown(Buttons.Back)))
            {
                reset();
                pause = false;
                paused = false;
                titleScreen = false;
                inMenu = false;
                quit = false;
            }
        }
        private void checkHs(short upOrDown, GamePadState g)
        {
            //if user backs
            if ((Keyboard.GetState().IsKeyDown(Keys.Escape) || g.IsButtonDown(Buttons.B) || g.IsButtonDown(Buttons.Back)))
            {
                if (paused)
                {
                    highScore = false;
                    pause = true;
                }
                else if (mainMen2)
                {
                    highScore = false;
                    mainMen = true;
                }
                else
                {
                    reset();
                }
            }
        }
        private void checkResults(short upOrDown, GamePadState g, GameObjects gO)
        {
            //if user continues
            if ((Keyboard.GetState().IsKeyDown(Keys.Enter) || g.IsButtonDown(Buttons.A)))
            {
                reset();
                gO.endResults();
            }
        }
        private void checkCredits(short upOrDown, GamePadState g)
        {
            //if user backs
            if ((Keyboard.GetState().IsKeyDown(Keys.Escape) || g.IsButtonDown(Buttons.B) || g.IsButtonDown(Buttons.Back)))
            {
                if (paused)
                {
                    credits = false;
                    pause = true;
                }
                else if (mainMen2)
                {
                    credits = false;
                    mainMen = true;
                }
                else
                {
                    reset();
                }
            }
        }
        private void checkSw(short upOrDown, GamePadState g)
        {
            //if user continues
            if ((Keyboard.GetState().IsKeyDown(Keys.Enter) || g.IsButtonDown(Buttons.A)))
            {
                reset();
            }
            else if ((Keyboard.GetState().IsKeyDown(Keys.Escape) || g.IsButtonDown(Buttons.B) || g.IsButtonDown(Buttons.Back)))
            {
                storageWarn = false;
            }
        }
        private void checkQw(short upOrDown, GamePadState g)
        {
            //if user continues
            if ((Keyboard.GetState().IsKeyDown(Keys.Enter) || g.IsButtonDown(Buttons.A)))
            {
                if (quit)
                {
                    //quit game
                    reset();
                    displayP.quitGame();
                }
                else if (titleScreen)
                {
                    //go back to title screen
                    reset();
                    displayP.getToTitle();
                }
            }
            else if ((Keyboard.GetState().IsKeyDown(Keys.Escape) || g.IsButtonDown(Buttons.B) || g.IsButtonDown(Buttons.Back)))
            {
                reset();
                ok2Chek = false;
                pause = true;
            }
        }
        private void checkOp(short upOrDown, GamePadState g)
        {
            //if user moved up
            if (upOrDown > 0)
            {
                if (cO < 3)
                {
                    cO++;
                }
            }
            else if (upOrDown < 0)
            {
                if (cO >= 1)
                {
                    cO--;
                }
            }


            //if user toggles paddle
            if ((cO == 0) && (Keyboard.GetState().IsKeyDown(Keys.Enter) || g.IsButtonDown(Buttons.A)))
            {
                if (paddleOn)
                {
                    paddleOn = false;
                }
                else
                {
                    paddleOn = true;
                }
            }

            //if user toggles ball
            if ((cO == 1) && (Keyboard.GetState().IsKeyDown(Keys.Enter) || g.IsButtonDown(Buttons.A)))
            {
                if (ballOn)
                {
                    ballOn = false;
                }
                else
                {
                    ballOn = true;
                }
            }

            //if user toggles vibration
            if ((cO == 2) && (Keyboard.GetState().IsKeyDown(Keys.Enter) || g.IsButtonDown(Buttons.A)))
            {
                if (vibrationOn)
                {
                    vibrationOn = false;
                }
                else
                {
                    vibrationOn = true;
                }
            }
            //if user toggles sound
            if ((cO == 3) && (Keyboard.GetState().IsKeyDown(Keys.Enter) || g.IsButtonDown(Buttons.A)))
            {
                if (soundOn)
                {
                    soundOn = false;
                }
                else
                {
                    soundOn = true;
                }
            }

            //if user backs
            if ((Keyboard.GetState().IsKeyDown(Keys.Escape) || g.IsButtonDown(Buttons.B) || g.IsButtonDown(Buttons.Back)))
            {
                if (paused)
                {
                    options = false;
                    pause = true;
                }
                else if (mainMen2)
                {
                    options = false;
                    paused = false;
                    pause = false;
                    mainMen = true;
                }
                else
                {
                    reset();
                }
            }
        }
        private void checkGameOver(short upOrDown, GamePadState g, GameObjects gO)
        {
            //if user continues
            if ((Keyboard.GetState().IsKeyDown(Keys.Enter) || g.IsButtonDown(Buttons.A)))
            {
                reset();
                gO.endGameOver(this);
            }
        }
        private void checkEntry(short upOrDown, GamePadState g, GameObjects gO, GameTime gameTime)
        {
            //IF user moved left
            if (Keyboard.GetState().IsKeyDown(Keys.Left) || g.IsButtonDown(Buttons.DPadLeft) ||
                g.ThumbSticks.Left.X < -.3 || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                //IF timer is not enabled
                if (!selectTimer.isEnabled())
                {
                    //decrement count4 until 0
                    if (count4 > 0)
                    {
                        count4--;
                    }
                    selectTimer.setEnable(true);
                }
                //ELSE timer is enabled
                else
                {
                    //IF timer is less than a decent time to slow down selection    
                    if (selectTimer.returnSeconds() < 2)
                    {
                        selectTimer.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                    }
                    //ELSE 
                    else
                    {
                        selectTimer = new Timer(100f, 0, 1);
                    }
                }
            }

            //IF user moved right
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || g.IsButtonDown(Buttons.DPadRight) ||
                g.ThumbSticks.Left.X > .3 || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                //IF timer is not enabled
                if (!selectTimer.isEnabled())
                {
                    //increment count4 until 2
                    if (count4 < 2)
                    {
                        count4++;
                    }
                    selectTimer.setEnable(true);
                }
                //ELSE timer is enabled
                else
                {
                    //IF timer is less than a decent time to slow down selection    
                    if (selectTimer.returnSeconds() < 2)
                    {
                        selectTimer.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                    }
                    //ELSE 
                    else
                    {
                        selectTimer = new Timer(100f, 0, 1);
                    }
                }
            }

            //IF user is on posi 1
            if (count4 == 0)
            {
                //IF user pressed down
                if (upOrDown > 0)
                {
                    //increment count1 until 25
                    if (count1 < 25)
                    {
                        count1++;
                    }
                    else
                    {
                        count1 = 0;
                    }
                }
                //IF user pressed up
                else if (upOrDown < 0)
                {
                    //decrement count1 until 25
                    if (count1 > 0)
                    {
                        count1--;
                    }
                    else
                    {
                        count1 = 25;
                    }
                }
            }
            //IF user is on posi 2
            else if (count4 == 1)
            {
                //IF user pressed down
                if (upOrDown > 0)
                {
                    //increment count1 until 25
                    if (count2 < 25)
                    {
                        count2++;
                    }
                    else
                    {
                        count2 = 0;
                    }
                }
                //IF user pressed up
                else if (upOrDown < 0)
                {
                    //decrement count1 until 25
                    if (count2 > 0)
                    {
                        count2--;
                    }
                    else
                    {
                        count2 = 25;
                    }
                }
            }
            //IF user is on posi 3
            else if (count4 == 2)
            {
                //IF user pressed down
                if (upOrDown > 0)
                {
                    //increment count1 until 25
                    if (count3 < 25)
                    {
                        count3++;
                    }
                    else
                    {
                        count3 = 0;
                    }
                }
                //IF user pressed up
                else if (upOrDown < 0)
                {
                    //decrement count1 until 25
                    if (count3 > 0)
                    {
                        count3--;
                    }
                    else
                    {
                        count3 = 25;
                    }
                }
            }

            //flash selector
            if (c2 > 0)
            {
                c2--;
            }
            else
            {
                c2 = 10;
                selectO *= -1;
            }


            //IF user pressed continue
            if ((Keyboard.GetState().IsKeyDown(Keys.Enter) || g.IsButtonDown(Buttons.A)))
            {
                //create string for high score
                string name = charList[count1] + charList[count2] + charList[count3];
                reset();
                gO.pushData(name);
                gO.endEntry();
            }
        }
        private void checkMainMenu(short upOrDown, GamePadState g, GameObjects gO, GameTime gameTime)
        {
            //if user moved up
            if (upOrDown > 0)
            {
                if (cM < 3)
                {
                    cM++;
                }
            }
            else if (upOrDown < 0)
            {
                if (cM >= 1)
                {
                    cM--;
                }
            }

            //if user selects High Scores
            if ((cM == 0) && (Keyboard.GetState().IsKeyDown(Keys.Enter) || g.IsButtonDown(Buttons.A)))
            {
                mainMen = false;
                mainMen2 = true;
                highScore = true;
                hS = displayP.getStSys().getGame().getStats().getHS();
            }
            //if user selects Options
            if ((cM == 1) && (Keyboard.GetState().IsKeyDown(Keys.Enter) || g.IsButtonDown(Buttons.A)))
            {
                mainMen = false;
                mainMen2 = true;
                options = true;
            }
            //if user selects Credits
            if ((cM == 2) && (Keyboard.GetState().IsKeyDown(Keys.Enter) || g.IsButtonDown(Buttons.A)))
            {
                mainMen = false;
                mainMen2 = true;
                credits = true;
            }
            //if user selects Quit
            if ((cM == 3) && (Keyboard.GetState().IsKeyDown(Keys.Enter) || g.IsButtonDown(Buttons.A)))
            {
                reset();
                displayP.quitGame();
            }

            //if user backs
            if ((Keyboard.GetState().IsKeyDown(Keys.Escape) || g.IsButtonDown(Buttons.B) ||
                g.IsButtonDown(Buttons.Back)))
            {
                reset();
                mainMen2 = false;
                pause = false;
                paused = false;
                inMenu = false;
                quit = false;
            }
        }

        /*****************************************/

        public bool getVibration()
        {
            return vibrationOn;
        }
        public bool getSound()
        {
            return soundOn;
        }
        public bool getPaddle()
        {
            return paddleOn;
        }
        public bool getBall()
        {
            return ballOn;
        }
    }
}
