/******************************************
 * 06/11/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  PlayerManager.cs 
 * 
 *****************************************/
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Br8kIn_
{
    class PlayerManager
    {
        //instance variables
        Player player;

        PlayerParticles playerP1, playerP2, playerP3;
        List<Texture2D> images;
        bool beatLevel, ballDes, freezeP,
            part1, part2, part3, spon, vibe, isVibe,
            ballDesM, hookR,hookL;
        short playerType, c1, c2;
        short playerDamage, playerImg, contNum;
        Vector2[] speed;
        Point[] frame;

        /*****************************************/

        //constructor
        public PlayerManager()
        {
            player = new Player(new Vector2(0, 0), new Vector2(0, 0), new Point(0, 0));
            playerP1 = new PlayerParticles();
            playerP2 = new PlayerParticles();
            playerP3 = new PlayerParticles();
            images = new List<Texture2D>();
            speed = new Vector2[5];
            frame = new Point[5];
            spon = false;
            freezeP = false;
            beatLevel = false;
            isVibe = true;
            c1 = 5;
            c2 = 0;
            vibe = false;
            hookR = false;
            hookL = false;
        }

        /*****************************************/

        //load
        public void Load(Engine engine, short playerT, short controller)
        {
            playerType = playerT;
            playerImg = 0;
            ballDes = false;
            ballDesM = false;
            contNum = controller;
            freezeP = false;
            spon = false;
            part1 = false;
            part2 = false;
            part3 = false;
            frame = new Point[5];
            speed = new Vector2[5];
            images = new List<Texture2D>();
            playerP1 = new PlayerParticles();
            playerP2 = new PlayerParticles();
            playerP3 = new PlayerParticles();
            c1 = 5;
            c2 = 0;
            vibe = false;

            switch (playerT)
            {
                case 0:
                    images.Add(engine.Content.Load<Texture2D>(@"Player/playerW"));
                    frame[0] = new Point(150, 35);
                    speed[0] = new Vector2(13, 0);
                    break;

                case 1:

                    //load 3 gold bar states
                    images.Add(engine.Content.Load<Texture2D>(@"Player/bar01"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/bar02"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/bar03"));
                    playerP1.Load(engine, 1);
                    playerP2.Load(engine, 1);
                    playerP3.Load(engine, 1);
                    frame[0] = new Point(150, 41);
                    frame[1] = new Point(120, 41);
                    frame[2] = new Point(90, 41);
                    speed[0] = new Vector2(13, 0);
                    playerDamage = 3;
                    break;

                case 2:

                    //load 3 sponge states
                    images.Add(engine.Content.Load<Texture2D>(@"Player/sponge01"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/sponge02"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/sponge03"));
                    playerP1.Load(engine, 2);
                    playerP2.Load(engine, 2);
                    playerP3.Load(engine, 2);
                    frame[0] = new Point(150, 41);
                    speed[0] = new Vector2(13, 0);
                    speed[1] = new Vector2(9, 0);
                    speed[2] = new Vector2(5, 0);
                    playerDamage = 3;
                    break;

                case 3:

                    //load 4 metal states
                    images.Add(engine.Content.Load<Texture2D>(@"Player/player030"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/player031"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/player032"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/player033"));
                    playerP1.Load(engine, 3);
                    playerP2.Load(engine, 3);
                    playerP3.Load(engine, 3);
                    frame[0] = new Point(150, 41);
                    speed[0] = new Vector2(13, 0);
                    playerDamage = 4;
                    break;

                case 4:

                    //load 3 glaciers states
                    images.Add(engine.Content.Load<Texture2D>(@"Player/player040"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/player041"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/player042"));
                    playerP1.Load(engine, 4);
                    playerP2.Load(engine, 4);
                    playerP3.Load(engine, 4);
                    frame[0] = new Point(150, 41);
                    frame[1] = new Point(120, 41);
                    frame[2] = new Point(90, 41);
                    speed[0] = new Vector2(13, 0);
                    playerDamage = 4;
                    break;

                case 5:

                    //load 4 spaceship states
                    images.Add(engine.Content.Load<Texture2D>(@"Player/player050"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/player051"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/player052"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/player053"));
                    playerP1.Load(engine, 5);
                    playerP2.Load(engine, 5);
                    playerP3.Load(engine, 5);
                    frame[0] = new Point(150, 41);
                    speed[0] = new Vector2(13, 0);
                    playerDamage = 4;
                    break;

                case 6:

                    //load 5 candy states
                    images.Add(engine.Content.Load<Texture2D>(@"Player/candy01"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/candy02"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/candy03"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/candy04"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/candy05"));
                    playerP1.Load(engine, 6);
                    playerP2.Load(engine, 6);
                    playerP3.Load(engine, 6);
                    frame[0] = new Point(150, 41);
                    speed[0] = new Vector2(13, 0);
                    playerDamage = 5;
                    break;

                case 7:

                    //load 5 gold bar states
                    images.Add(engine.Content.Load<Texture2D>(@"Player/bar01"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/bar02"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/bar03"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/bar04"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/bar05"));
                    playerP1.Load(engine, 1);
                    playerP2.Load(engine, 1);
                    playerP3.Load(engine, 1);
                    frame[0] = new Point(150, 41);
                    frame[1] = new Point(120, 41);
                    frame[2] = new Point(90, 41);
                    frame[3] = new Point(60, 41);
                    frame[4] = new Point(30, 41);
                    speed[0] = new Vector2(13, 0);
                    playerDamage = 5;
                    break;

                case 8:

                    //load 5 sponge states
                    images.Add(engine.Content.Load<Texture2D>(@"Player/sponge01"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/sponge02"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/sponge03"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/sponge04"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/sponge05"));
                    playerP1.Load(engine, 2);
                    playerP2.Load(engine, 2);
                    playerP3.Load(engine, 2);
                    frame[0] = new Point(150, 41);
                    speed[0] = new Vector2(13, 0);
                    speed[1] = new Vector2(12, 0);
                    speed[2] = new Vector2(10, 0);
                    speed[3] = new Vector2(8.5f, 0);
                    speed[4] = new Vector2(5, 0);
                    playerDamage = 5;
                    break;

                case 9:

                    //load 3 glacier states
                    images.Add(engine.Content.Load<Texture2D>(@"Player/player040"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/player042"));
                    images.Add(engine.Content.Load<Texture2D>(@"Player/player043"));
                    playerP1.Load(engine, 4);
                    playerP2.Load(engine, 4);
                    playerP3.Load(engine, 4);
                    frame[0] = new Point(150, 41);
                    frame[1] = new Point(90, 41);
                    frame[2] = new Point(50, 41);
                    speed[0] = new Vector2(13, 0);
                    playerDamage = 3;
                    break;

            }

            Vector2 pos = new Vector2(1280 / 2, 670);
            player.Load(images, frame, pos, playerType);
            player.setSpeed(speed[playerImg]);
            player.setMaxSpeed(speed[playerImg]);
            player.setFrame(frame[playerImg]);
            player.setOrigin(new Vector2(frame[playerImg].X / 2, frame[playerImg].Y / 2));
        }

        /*****************************************/

        //update
        public void Update(GameTime gameTime, Rectangle clientBounds,
            GamePadState g, DisplayPane dP)
        {
            if (!freezeP)
            {
                //update player
                player.Update(gameTime, clientBounds, g);
            }

            if (dP.getVibration())
            {
                isVibe = true;
            }
            else
            {
                isVibe = false;
            }

            if (!spon)
            {
                if (part1)
                {
                    if (c1 > 0)
                    {
                        c1--;
                        setPart();
                    }
                    else
                    {
                        part1 = false;
                        part2 = false;
                        part3 = false;
                        c1 = 5;
                    }
                }
            }
            else
            {
                setPart();
            }
            playerP1.Update(part1, gameTime);
            playerP2.Update(part2, gameTime);
            playerP3.Update(part3, gameTime);

            //check vibration timer
            if (vibe)
            {
                //count to 10 frames then turn off
                if (c2 < 10)
                {
                    c2++;
                }
                else
                {
                    vibeOff();
                    c2 = 0;
                }

            }
            if (player.getHookR() == true)
            {
                hookR = true;
            }
            else
                hookR = false;

            if (player.getHookL() == true)
            {
                hookL = true;
            }
            else
                hookL = false;
        }

        /*****************************************/

        //draw
        public void Draw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch, playerImg);
            playerP1.Draw(spriteBatch);
            playerP2.Draw(spriteBatch);
            playerP3.Draw(spriteBatch);
        }

        /*****************************************/

        //method to return player's collision rectangle
        public Rectangle getCollRect()
        {
            return player.getCollRect();
        }
        public Rectangle getCollRectL()
        {
            return player.getCollRectL();
        }
        public Rectangle getCollRectR()
        {
            return player.getCollRectR();
        }
        public Rectangle[] getMainColl()
        {
            return player.getMainColl();
        }

        public Vector2 getPos()
        {
            return player.getPos();
        }
        public void vibeOnL()
        {
            if (isVibe)
            {
                switch(contNum)
                {
                    case 1:
                        GamePad.SetVibration(PlayerIndex.One, 0.5f, 0f);
                        vibe = true;
                        break;
                    case 2:
                        GamePad.SetVibration(PlayerIndex.Two, 0.5f, 0f);
                        vibe = true;
                        break;
                    case 3:
                        GamePad.SetVibration(PlayerIndex.Three, 0.5f, 0f);
                        vibe = true;
                        break;
                    case 4:
                        GamePad.SetVibration(PlayerIndex.Four, 0.5f, 0f);
                        vibe = true;
                        break;
                }
            }
        }
        public void vibeOnR()
        {
            if (isVibe)
            {
                switch (contNum)
                {
                    case 1:
                        GamePad.SetVibration(PlayerIndex.One, 0f, 0.5f);
                        vibe = true;
                        break;
                    case 2:
                        GamePad.SetVibration(PlayerIndex.Two, 0f, 0.5f);
                        vibe = true;
                        break;
                    case 3:
                        GamePad.SetVibration(PlayerIndex.Three, 0f, 0.5f);
                        vibe = true;
                        break;
                    case 4:
                        GamePad.SetVibration(PlayerIndex.Four, 0f, 0.5f);
                        vibe = true;
                        break;
                }
            }
        }
        public void vibeOnC()
        {
            if (isVibe)
            {
                switch (contNum)
                {
                    case 1:
                        GamePad.SetVibration(PlayerIndex.One, 0.5f, 0.5f);
                        vibe = true;
                        break;
                    case 2:
                        GamePad.SetVibration(PlayerIndex.Two, 0.5f, 0.5f);
                        vibe = true;
                        break;
                    case 3:
                        GamePad.SetVibration(PlayerIndex.Three, 0.5f, 0.5f);
                        vibe = true;
                        break;
                    case 4:
                        GamePad.SetVibration(PlayerIndex.Four, 0.5f, 0.5f);
                        vibe = true;
                        break;
                }
            }
        }
        public void vibeOnB()
        {
            if (isVibe)
            {
                switch (contNum)
                {
                    case 1:
                        GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
                        vibe = true;
                        break;
                    case 2:
                        GamePad.SetVibration(PlayerIndex.Two, 1f, 1f);
                        vibe = true;
                        break;
                    case 3:
                        GamePad.SetVibration(PlayerIndex.Three, 1f, 1f);
                        vibe = true;
                        break;
                    case 4:
                        GamePad.SetVibration(PlayerIndex.Four, 1f, 1f);
                        vibe = true;
                        break;
                }
            }
        }
        public void vibeOff()
        {
            switch (contNum)
            {
                case 1:
                    GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
                    vibe = false;
                    break;
                case 2:
                    GamePad.SetVibration(PlayerIndex.Two, 0f, 0f);
                    vibe = false;
                    break;
                case 3:
                    GamePad.SetVibration(PlayerIndex.Three, 0f, 0f);
                    vibe = false;
                    break;
                case 4:
                    GamePad.SetVibration(PlayerIndex.Four, 0f, 0f);
                    vibe = false;
                    break;
            }
        }

        /*****************************************/


        //method to return if player beat level
        public bool getBeatLevel()
        {
            return beatLevel;
        }

        /*****************************************/

        //method set player beat level
        public void setBeatLevel(bool x)
        {
            beatLevel = x;
        }

        /*****************************************/

        //method to decrement state
        public void gotHit()
        {
            //based on player type
            switch (playerType)
            {
                case 1:

                    //IF player damage can still occur GOLD
                    if (playerDamage > 1)
                    {
                        playerDamage--;
                        playerImg++;
                        player.setFrame(frame[playerImg]);
                        player.setOrigin(new Vector2(frame[playerImg].X / 2,
                            frame[playerImg].Y / 2));
                        setPart();
                        vibeOnB();

                        //set particles to true
                        part1 = true;
                        part2 = true;
                        part3 = true;
                    }
                    break;

                case 2:

                    //IF player damage can still occur SPONGE
                    if (playerDamage > 1)
                    {
                        playerDamage--;
                        playerImg++;
                        player.setSpeed(speed[playerImg]);
                        player.setMaxSpeed(speed[playerImg]);
                        setPart();
                        vibeOnB();
                        if (spon)
                        {
                            playerP1.setAm();
                            playerP2.setAm();
                            playerP3.setAm();
                        }
                        spon = true;

                        //set particles to true
                        part1 = true;
                        part2 = true;
                        part3 = true;
                    }
                    break;

                case 3:

                    //IF player damage can still occur METAL
                    if (playerDamage > 1)
                    {
                        playerDamage--;
                        playerImg++;
                        vibeOnB();
                        if (playerImg == 3)
                        {
                            ballDesM = true;
                        }
                        setPart();

                        //set particles to true
                        part1 = true;
                        part2 = true;
                        part3 = true;
                    }
                    break;

                case 4:

                    //IF player damage can still occur GLACIER
                    if (playerDamage > 1)
                    {
                        playerDamage--;
                        vibeOnB();
                        if (playerDamage == 1)
                        {
                            player.setOn(false);
                        }
                        else
                        {
                            playerImg++;
                            player.setFrame(frame[playerImg]);
                            player.setOrigin(new Vector2(frame[playerImg].X / 2,
                                frame[playerImg].Y / 2));
                        }
                        setPart();

                        //set particles to true
                        part1 = true;
                        part2 = true;
                        part3 = true;
                    }
                    break;

                case 5:

                    //IF player damage can still occur SPACESHIP
                    if (playerDamage > 1)
                    {
                        playerDamage--;
                        playerImg++;
                        setPart();
                        vibeOnB();
                        spon = true;
                        if (playerDamage == 3)
                        {
                            part1 = true;
                        }
                        else if (playerDamage == 2)
                        {
                            part2 = true;
                        }
                        else
                        {
                            part3 = true;
                        }
                    }
                    else
                    {
                        iniNormDes();
                    }
                    break;

                case 6:

                    //IF player damage can still occur CANDY
                    if (playerDamage > 1)
                    {
                        playerDamage--;
                        vibeOnB();
                        if (playerDamage == 1)
                        {
                            player.setOn(false);
                        }
                        else
                        {
                        }
                        playerImg++;
                        setPart();

                        //set particles to true
                        part1 = true;
                        part2 = true;
                        part3 = true;
                    }
                    break;

                case 7:

                    //IF player damage can still occur GOLD HARD
                    if (playerDamage > 1)
                    {
                        playerDamage--;
                        playerImg++;
                        vibeOnB();
                        player.setFrame(frame[playerImg]);
                        player.setOrigin(new Vector2(frame[playerImg].X / 2,
                            frame[playerImg].Y / 2));
                        //set major change particles
                    }
                    break;

                case 8:

                    //IF player damage can still occur SPONGE HARD
                    if (playerDamage > 1)
                    {
                        playerDamage--;
                        playerImg++;
                        vibeOnB();
                        player.setSpeed(speed[playerImg]);
                        player.setMaxSpeed(speed[playerImg]);
                        //set continuous particles
                    }
                    break;

                case 9:

                    //IF player damage can still occur GLACIER HARD
                    if (playerDamage > 1)
                    {
                        playerDamage--;
                        playerImg++;
                        vibeOnB();
                        player.setFrame(frame[playerImg]);
                        player.setOrigin(new Vector2(frame[playerImg].X / 2,
                            frame[playerImg].Y / 2));
                        //set major change particles
                    }
                    else
                    {
                        iniNormDes();
                    }
                    break;
            }
        }
        public bool getBallDes()
        {
            return ballDes;
        }
        public bool getBallDesM()
        {
            return ballDesM;
        }
        public void iniBallDes()
        {
            setDesPartM();
            player.setOn(false);
        }
        private void iniNormDes()
        {
            setDesPart();
            player.setOn(false);
            spon = false;
        }

        /*****************************************/

        public void playerReset()
        {
            ballDes = false;
            ballDesM = false;
            freezeP = false;
            part1 = false;
            part2 = false;
            part3 = false;
            Vector2 pos = new Vector2(1280 / 2, 670);
            playerImg = 0;
            spon = false;
            playerP1.endParts();
            playerP2.endParts();
            playerP3.endParts();
            c1 = 5;
            player.setGrace(true);

            switch (playerType)
            {
                case 1:
                    playerDamage = 3;
                    break;
                case 2:
                    playerDamage = 3;
                    break;
                case 3:
                    playerDamage = 4;
                    playerP1.setColor();
                    playerP2.setColor();
                    playerP3.setColor();
                    break;
                case 4:
                    playerDamage = 4;
                    break;
                case 5:
                    playerDamage = 4;
                    break;
                case 6:
                    playerDamage = 5;
                    break;
                case 7:
                    playerDamage = 5;
                    break;
                case 8:
                    playerDamage = 5;
                    break;
                case 9:
                    playerDamage = 3;
                    break;
            }
            player.Load(images, frame, pos, playerType);
            player.setSpeed(speed[playerImg]);
            player.setMaxSpeed(speed[playerImg]);
            player.setFrame(frame[playerImg]);
            player.setOrigin(new Vector2(frame[playerImg].X / 2, frame[playerImg].Y / 2));
        }
        public bool getOn()
        {
            return player.getOn();
        }
        public bool getGrace()
        {
            return player.getGrace();
        }
        public void freeze()
        {
            freezeP = true;
        }

        /*****************************************/

        public void setPart()
        {
            //set particle emission position
            playerP1.setPos(new Vector2(player.getPos().X - 37.5f, player.getPos().Y));
            playerP2.setPos(new Vector2(player.getPos().X, player.getPos().Y));
            playerP3.setPos(new Vector2(player.getPos().X + 37.5f, player.getPos().Y));

            //set direction of particle spray
            switch (playerP1.getType())
            {
                case 1:
                    playerP1.setNum(new Vector2(1, 1));
                    playerP2.setNum(new Vector2(1, 1));
                    playerP3.setNum(new Vector2(1, 1));
                    break;
                case 2:
                    playerP1.setNum(new Vector2(1, 2));
                    playerP2.setNum(new Vector2(1, 2));
                    playerP3.setNum(new Vector2(1, 2));
                    break;
                case 3:
                    playerP1.setNum(new Vector2(1, 1));
                    playerP2.setNum(new Vector2(1, 1));
                    playerP3.setNum(new Vector2(1, 1));
                    break;
                case 4:
                    playerP1.setNum(new Vector2(1, 1));
                    playerP2.setNum(new Vector2(1, 1));
                    playerP3.setNum(new Vector2(1, 1));
                    break;
                case 5:
                    playerP1.setPos(new Vector2(player.getPos().X - 40, player.getPos().Y - 10));
                    playerP2.setPos(new Vector2(player.getPos().X - 10, player.getPos().Y));
                    playerP3.setPos(new Vector2(player.getPos().X + 50, player.getPos().Y - 10));
                    playerP1.setNum(new Vector2(1, 0));
                    playerP2.setNum(new Vector2(1, 0));
                    playerP3.setNum(new Vector2(1, 0));
                    break;
                case 6:
                    playerP1.setNum(new Vector2(1, 1));
                    playerP2.setNum(new Vector2(1, 1));
                    playerP3.setNum(new Vector2(1, 1));
                    break;
            }
        }

        public void setDesPart()
        {
            playerP1.setPos(new Vector2(player.getPos().X - 37.5f, player.getPos().Y));
            playerP2.setPos(new Vector2(player.getPos().X, player.getPos().Y));
            playerP3.setPos(new Vector2(player.getPos().X + 37.5f, player.getPos().Y));
            playerP1.setNum(new Vector2(1, 1));
            playerP2.setNum(new Vector2(1, 1));
            playerP3.setNum(new Vector2(1, 1));
            playerP1.setDes();
            playerP2.setDes();
            playerP3.setDes();

        }
        public void setDesPartM()
        {
            playerP1.setPos(new Vector2(player.getPos().X - 37.5f, player.getPos().Y));
            playerP2.setPos(new Vector2(player.getPos().X, player.getPos().Y));
            playerP3.setPos(new Vector2(player.getPos().X + 37.5f, player.getPos().Y));
            playerP1.setNum(new Vector2(1, 1));
            playerP2.setNum(new Vector2(1, 1));
            playerP3.setNum(new Vector2(1, 1));
            playerP1.setDesM();
            playerP2.setDesM();
            playerP3.setDesM();
            part1 = true;
            part2 = true;
            part3 = true;

        }

        public bool getHookR()
        {
            return player.getHookR();
        }
        public bool getHookL()
        {
            return player.getHookL();
        }
    }
}
