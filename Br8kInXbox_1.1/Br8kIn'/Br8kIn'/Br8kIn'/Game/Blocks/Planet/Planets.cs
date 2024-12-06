/******************************************
 * 04/19/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  Planets.cs 
 * 
 *****************************************/
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Br8kIn_
{
    class Planets : Blocks
    {
        //instance variables
        List<Planet> plan;
        short num;

        /*****************************************/

        //constructor
        public Planets(short numb)
        {
            plan = new List<Planet>();
            num = numb;
        }

        /*****************************************/

        //load
        public void Load(Engine engine)
        {
            if (num == 1)
            {
                plan.Add(new Planet(new Vector2(200, 250), new Vector2(1, 1),
                        new Point(225, 225), engine, num));
            }
            else
            {
                plan.Add(new Planet(new Vector2(1280 / 2, -80), new Vector2(0, 0),
                    new Point(590, 590), engine, num));
            }
        }
        public void Load(Engine engine, List<int> y) { }

        /*****************************************/

        //update 
        public void Update(BallManager bM, StatsManager stats, GameTime gT,
            SoundManager sManager, ExtraBallManager xBall)
        {
            plan[0].Update(bM, stats, gT, sManager, xBall);
        }
        public void Update(BallManager bManager, StatsManager stats, GameTime gT,
            PlayerManager pM, SoundManager sManager, ExtraBallManager xBall) { }

        /*****************************************/

        //draw
        public void Draw(SpriteBatch spriteBatch)
        {
            plan[0].Draw(spriteBatch);
        }

        /*****************************************/

        public bool isOn()
        {
            return plan[0].isOn();
        }

        /*****************************************/

        public void setPositions(List<Vector2> x)
        {
            plan[0].setPos(x[0]);
        }
        public void setPositions(List<Vector2> x, List<Vector2> y) { }

        /*****************************************/
        /*****************************************/
        /*****************************************/
        /*****************************************/

        private class Planet : Sprite
        {
            //instance variables
            Texture2D plan, core;
            List<Texture2D> cores, pieces, piecesh, destruct;
            Vector2 origin;
            Vector2[] desPiece;
            List<bool> hit, desHit;
            Random rand;
            PlanetParticles dirt, water, grass, lava;
            short ani, count1, count2, count3, c3, c4,
                c5, c6, type, count4, count5, count6,
                count7, count8, c7, c8, c9;
            float angle, scale;
            bool on, onF, flip, flip2, partw, partg,
                partd, partl, desH, noCore, b1, sideHit;

            /**********************/

            //constructor
            public Planet(Vector2 pos, Vector2 spe, Point fSize, Engine engine, short t)
                : base(pos, spe, fSize)
            {
                pieces = new List<Texture2D>();
                cores = new List<Texture2D>();
                piecesh = new List<Texture2D>();
                destruct = new List<Texture2D>();
                hit = new List<bool>();
                desHit = new List<bool>();
                rand = new Random();
                angle = 0;
                type = t;
                desH = false;
                scale = 1f;
                noCore = false;
                c7 = 0;
                c8 = 0;
                c9 = 30;
                sideHit = false;
                b1 = false;

                if (type == 1)
                {
                    plan = engine.Content.Load<Texture2D>(@"Blocks/Planet/p00");
                    core = engine.Content.Load<Texture2D>(@"Blocks/Planet/c00");
                    origin = new Vector2(225 / 2, 225 / 2);

                    pieces.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p01"));
                    pieces.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p02"));
                    pieces.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p03"));
                    pieces.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p04"));
                    pieces.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p05"));
                    pieces.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p06"));
                    pieces.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p07"));
                    pieces.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p08"));
                    pieces.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p09"));
                    pieces.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p10"));
                    pieces.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p11"));
                    pieces.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p12"));
                    pieces.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p13"));
                    pieces.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p14"));

                    piecesh.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p01h"));
                    piecesh.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p02h"));
                    piecesh.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p03h"));
                    piecesh.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p04h"));
                    piecesh.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p05h"));
                    piecesh.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p06h"));
                    piecesh.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p07h"));
                    piecesh.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p08h"));
                    piecesh.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p09h"));
                    piecesh.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p10h"));
                    piecesh.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p11h"));
                    piecesh.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p12h"));
                    piecesh.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p13h"));
                    piecesh.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/p14h"));

                    cores.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/core01"));
                    cores.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/core02"));
                    cores.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/core03"));
                    cores.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/core04"));
                    cores.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/core05"));
                    cores.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/core06"));
                    cores.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/core07"));
                    cores.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/core08"));
                    cores.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/core09"));
                    cores.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/core10"));

                    destruct.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/piece01"));
                    destruct.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/piece02"));
                    destruct.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/piece03"));
                    destruct.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/piece04"));
                    destruct.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/piece05"));
                    destruct.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/piece06"));
                    destruct.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/piece07"));
                    destruct.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/piece08"));
                    destruct.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet/piece09"));

                    desPiece = new Vector2[9];
                }
                else
                {
                    plan = engine.Content.Load<Texture2D>(@"Blocks/Planet2/planet2_m");
                    core = engine.Content.Load<Texture2D>(@"Blocks/Planet2/c0");
                    origin = new Vector2(590 / 2, 590 / 2);

                    pieces.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planet2_01"));
                    pieces.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planet2_02"));
                    pieces.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planet2_03"));
                    pieces.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planet2_04"));
                    pieces.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planet2_05"));
                    pieces.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planet2_06"));
                    pieces.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planet2_07"));

                    piecesh.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planet2_01h"));
                    piecesh.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planet2_02h"));
                    piecesh.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planet2_03h"));
                    piecesh.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planet2_04h"));
                    piecesh.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planet2_05h"));
                    piecesh.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planet2_06h"));
                    piecesh.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planet2_07h"));

                    cores.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/c1"));
                    cores.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/c2"));
                    cores.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/c3"));
                    cores.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/c4"));
                    cores.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/c5"));
                    cores.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/c6"));
                    cores.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/c7"));
                    cores.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/c8"));
                    cores.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/c9"));
                    cores.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/c10"));

                    destruct.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planetP1"));
                    destruct.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planetP2"));
                    destruct.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planetP3"));
                    destruct.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planetP4"));
                    destruct.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planetP5"));
                    destruct.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planetP6"));
                    destruct.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planetP7"));
                    destruct.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planetP8"));
                    destruct.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planetP9"));
                    destruct.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planetP10"));
                    destruct.Add(engine.Content.Load<Texture2D>(@"Blocks/Planet2/planetP11"));

                    desPiece = new Vector2[11];
                }

                count1 = 0;
                count2 = 0;
                count3 = 0;
                c3 = 0;
                c4 = 0;
                c5 = 0;
                c6 = 0;
                ani = 0;
                on = true;
                onF = true;
                flip = false;
                flip2 = false;
                partw = false;
                partg = false;
                partd = false;
                partl = false;
                count4 = 2;
                count5 = 2;
                count6 = 2;
                count7 = 2;
                count8 = 2;

                for (short a = 0; a < pieces.Count; a++)
                {
                    hit.Add(false);
                }
                for (short b = 0; b < destruct.Count; b++)
                {
                    desHit.Add(false);
                }

                dirt = new PlanetParticles();
                lava = new PlanetParticles();
                water = new PlanetParticles();
                grass = new PlanetParticles();

                dirt.Load(engine.Content.Load<Texture2D>(@"Particles/c01"),
                    engine.Content.Load<Texture2D>(@"Particles/c02"),
                    engine.Content.Load<Texture2D>(@"Particles/c03"),
                    engine.Content.Load<Texture2D>(@"Particles/c04"), 1,
                    Color.Khaki, false);
                water.Load(engine.Content.Load<Texture2D>(@"Particles/snowball00_snow_21x22"),
                    engine.Content.Load<Texture2D>(@"Particles/snowball00_snow_21x22"),
                    engine.Content.Load<Texture2D>(@"Particles/snowball00_snow_21x22"),
                    engine.Content.Load<Texture2D>(@"Particles/snowball00_snow_21x22"), 1,
                    Color.CornflowerBlue, false);
                lava.Load(engine.Content.Load<Texture2D>(@"Particles/corep01"),
                    engine.Content.Load<Texture2D>(@"Particles/corep02"),
                    engine.Content.Load<Texture2D>(@"Particles/corep03"),
                    engine.Content.Load<Texture2D>(@"Particles/corep04"), 1,
                    Color.White, true);
                grass.Load(engine.Content.Load<Texture2D>(@"Particles/shards07"),
                    engine.Content.Load<Texture2D>(@"Particles/snowball00_snow_21x22"),
                    engine.Content.Load<Texture2D>(@"Particles/snowball00_snow_21x22"),
                    engine.Content.Load<Texture2D>(@"Particles/snowball00_snow_21x22"), 2,
                    Color.White, false);

                if (type == 1)
                {
                    movePlanet();
                }

            }

            /**********************/

            //update
            public void Update(BallManager bM, StatsManager stats, GameTime gT,
                SoundManager sManager, ExtraBallManager xBall)
            {
                if (onF)
                {
                    if (type == 1)
                    {
                        //move planet
                        movePlanet();
                    }

                    //animate core
                    animateCore();

                    //update collision particles
                    updateParticles(gT);

                    //choose lava particles
                    chooseLava(gT);

                    if (sideHit)
                    {
                        //wait 20 frames
                        if (c9 <= 0)
                        {
                            sideHit = false;
                            c9 = 30;
                        }
                        else
                        {
                            c9--;
                        }
                    }

                    //check collision for each piece
                    extraColl(xBall, stats, sManager);
                    if (!bM.getFirst())
                    {
                        for (int a = 0; a < pieces.Count; a++)
                        {
                            if (type == 1)
                            {
                                if (getColl()[a].Intersects(bM.getCollRect()))
                                {
                                    //top
                                    if (a == 0 || a == 13 || a == 12 || a == 1)
                                    {
                                        if (bM.getBall().getDir().Y > 0)
                                        {
                                            bM.bounce(new Vector2(1, -1));
                                            bM.setPos(new Vector2(bM.getPos().X, bM.getPos().Y - 10));
                                            bM.setColl(false);
                                            xBall.incCombo();

                                            if (count2 >= 14)
                                            {
                                                onF = false;
                                                stats.addPoint(6);
                                            }
                                            if (!hit[a])
                                            {
                                                hit[a] = true;
                                                count2++;
                                                stats.addPoint(5);
                                                ejectParts(a + 1, bM);
                                                sManager.playSound("Explode short2");
                                            }
                                        }
                                    }
                                    //bottom
                                    else if (a == 8 || a == 7 || a == 6)
                                    {
                                        if (bM.getBall().getDir().Y < 0)
                                        {
                                            bM.bounce(new Vector2(1, -1));
                                            bM.setPos(new Vector2(bM.getPos().X, bM.getPos().Y + 10));
                                            bM.setColl(false);
                                            xBall.incCombo();

                                            if (count2 >= 14)
                                            {
                                                onF = false;
                                                stats.addPoint(6);

                                            }
                                            if (!hit[a])
                                            {
                                                hit[a] = true;
                                                count2++;
                                                stats.addPoint(5);
                                                ejectParts(a + 1, bM);
                                                sManager.playSound("Explode short2");
                                            }
                                        }
                                    }
                                    //left
                                    else if (a == 9 || a == 10 || a == 11)
                                    {
                                        if (bM.getBall().getDir().X >= -5)
                                        {
                                            if (bM.getBall().getDir().X <= 0)
                                            {
                                                if (!sideHit)
                                                {
                                                    bM.bounce(new Vector2(1, -1));
                                                    sideHit = true;
                                                    if (bM.getBall().getDir().Y > 0)
                                                    {
                                                        bM.setPos(new Vector2(bM.getPos().X - 10, bM.getPos().Y - 10));
                                                    }
                                                    else
                                                    {
                                                        bM.setPos(new Vector2(bM.getPos().X - 10, bM.getPos().Y + 10));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bM.bounce(new Vector2(-1, 1));
                                                bM.setPos(new Vector2(bM.getPos().X - 10, bM.getPos().Y));
                                            }
                                            bM.setColl(false);
                                            xBall.incCombo();

                                            if (count2 >= 14)
                                            {
                                                onF = false;
                                                stats.addPoint(6);

                                            }
                                            if (!hit[a])
                                            {
                                                hit[a] = true;
                                                count2++;
                                                stats.addPoint(5);
                                                ejectParts(a + 1, bM);
                                                sManager.playSound("Explode short2");
                                            }
                                        }
                                    }
                                    //right
                                    else if (a == 2 || a == 3 || a == 4 || a == 5)
                                    {
                                        if (bM.getBall().getDir().X <= 5)
                                        {
                                            if (bM.getBall().getDir().X >= 0)
                                            {
                                                if (!sideHit)
                                                {
                                                    bM.bounce(new Vector2(1, -1));
                                                    sideHit = true;
                                                    if (bM.getBall().getDir().Y > 0)
                                                    {
                                                        bM.setPos(new Vector2(bM.getPos().X + 10, bM.getPos().Y - 10));
                                                    }
                                                    else
                                                    {
                                                        bM.setPos(new Vector2(bM.getPos().X + 10, bM.getPos().Y + 10));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bM.bounce(new Vector2(-1, 1));
                                                bM.setPos(new Vector2(bM.getPos().X + 10, bM.getPos().Y));
                                            }
                                            bM.setColl(false);
                                            xBall.incCombo();

                                            if (count2 >= 14)
                                            {
                                                onF = false;
                                                stats.addPoint(6);

                                            }
                                            if (!hit[a])
                                            {
                                                hit[a] = true;
                                                count2++;
                                                stats.addPoint(5);
                                                ejectParts(a + 1, bM);
                                                sManager.playSound("Explode short2");
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (getColl2()[a].Intersects(bM.getCollRect()))
                                {
                                    //left piece
                                    if (a == 1)
                                    {
                                        if (bM.getBall().getDir().X >= -5)
                                        {
                                            if (bM.getBall().getDir().X <= 0)
                                            {
                                                if (!sideHit)
                                                {
                                                    bM.bounce(new Vector2(1, -1));
                                                    sideHit = true;
                                                    if (bM.getBall().getDir().Y > 0)
                                                    {
                                                        bM.setPos(new Vector2(bM.getPos().X - 10, bM.getPos().Y - 10));
                                                    }
                                                    else
                                                    {
                                                        bM.setPos(new Vector2(bM.getPos().X - 10, bM.getPos().Y + 10));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bM.bounce(new Vector2(-1, 1));
                                                bM.setPos(new Vector2(bM.getPos().X - 10, bM.getPos().Y));
                                            }
                                            bM.setColl(false);
                                            xBall.incCombo();

                                            if (count2 >= 5)
                                            {
                                                onF = false;
                                                stats.addPoint(6);

                                            }
                                            if (!hit[a])
                                            {
                                                if (bM.getLevelNum() > 10)
                                                {
                                                    if (count4 <= 1)
                                                    {
                                                        hit[a] = true;
                                                        count2++;
                                                        ejectParts(a, bM);
                                                        sManager.playSound("Explode short2");
                                                    }
                                                    else
                                                    {
                                                        count4--;
                                                    }
                                                }
                                                else
                                                {
                                                    hit[a] = true;
                                                    count2++;
                                                    ejectParts(a, bM);
                                                    sManager.playSound("Explode short2");
                                                }
                                                stats.addPoint(5);
                                            }
                                        }
                                    }
                                    //right piece
                                    else if (a == 5)
                                    {
                                        if (bM.getBall().getDir().X <= 5)
                                        {
                                            if (bM.getBall().getDir().X >= 0)
                                            {
                                                if (!sideHit)
                                                {
                                                    bM.bounce(new Vector2(1, -1));
                                                    sideHit = true;
                                                    if (bM.getBall().getDir().Y > 0)
                                                    {
                                                        bM.setPos(new Vector2(bM.getPos().X + 10, bM.getPos().Y - 10));
                                                    }
                                                    else
                                                    {
                                                        bM.setPos(new Vector2(bM.getPos().X + 10, bM.getPos().Y + 10));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bM.bounce(new Vector2(-1, 1));
                                                bM.setPos(new Vector2(bM.getPos().X + 10, bM.getPos().Y));
                                            }
                                            bM.setColl(false);
                                            xBall.incCombo();

                                            if (count2 >= 5)
                                            {
                                                onF = false;
                                                stats.addPoint(6);

                                            }
                                            if (!hit[a])
                                            {
                                                if (bM.getLevelNum() > 10)
                                                {
                                                    if (count5 <= 1)
                                                    {
                                                        hit[a] = true;
                                                        count2++;
                                                        ejectParts(a, bM);
                                                        sManager.playSound("Explode short2");
                                                    }
                                                    else
                                                    {
                                                        count5--;
                                                    }
                                                }
                                                else
                                                {
                                                    hit[a] = true;
                                                    count2++;
                                                    ejectParts(a, bM);
                                                    sManager.playSound("Explode short2");
                                                }
                                                stats.addPoint(5);
                                            }
                                        }
                                    }
                                    //bottom pieces
                                    else if (a == 2 || a == 3 || a == 4)
                                    {
                                        if (bM.getBall().getDir().Y < 0)
                                        {
                                            bM.bounce(new Vector2(1, -1));
                                            bM.setPos(new Vector2(bM.getPos().X, bM.getPos().Y + 10));
                                            bM.setColl(false);
                                            xBall.incCombo();

                                            if (count2 >= 5)
                                            {
                                                onF = false;
                                                stats.addPoint(6);

                                            }
                                            if (!hit[a])
                                            {
                                                if (a == 2)
                                                {
                                                    if (bM.getLevelNum() > 10)
                                                    {
                                                        if (count6 <= 1)
                                                        {
                                                            hit[a] = true;
                                                            count2++;
                                                            ejectParts(a, bM);
                                                            sManager.playSound("Explode short2");
                                                        }
                                                        else
                                                        {
                                                            count6--;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        hit[a] = true;
                                                        count2++;
                                                        ejectParts(a, bM);
                                                        sManager.playSound("Explode short2");
                                                    }
                                                }
                                                else if (a == 3)
                                                {
                                                    if (bM.getLevelNum() > 10)
                                                    {
                                                        if (count7 <= 1)
                                                        {
                                                            hit[a] = true;
                                                            count2++;
                                                            ejectParts(a, bM);
                                                            sManager.playSound("Explode short2");
                                                        }
                                                        else
                                                        {
                                                            count7--;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        hit[a] = true;
                                                        count2++;
                                                        ejectParts(a, bM);
                                                        sManager.playSound("Explode short2");
                                                    }
                                                }
                                                else if (a == 4)
                                                {
                                                    if (bM.getLevelNum() > 10)
                                                    {
                                                        if (count8 <= 1)
                                                        {
                                                            hit[a] = true;
                                                            count2++;
                                                            ejectParts(a, bM);
                                                            sManager.playSound("Explode short2");
                                                        }
                                                        else
                                                        {
                                                            count8--;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        hit[a] = true;
                                                        count2++;
                                                        ejectParts(a, bM);
                                                        sManager.playSound("Explode short2");
                                                    }
                                                }
                                                stats.addPoint(5);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    //destruction handler
                    destroy(gT, bM, xBall);
                }
            }

            /**********************/

            //draw
            public void Draw(SpriteBatch spriteBatch)
            {
                if (onF)
                {
                    //draw base
                    spriteBatch.Draw(plan, position, null,
                        Color.White, 0, origin, 1f, SpriteEffects.None, .245f);

                    //draw core
                    spriteBatch.Draw(core, position, null,
                        Color.White, 0, origin, 1f, SpriteEffects.None, .24f);
                    spriteBatch.Draw(cores[count1], position, null,
                        Color.White, 0, origin, 1f, SpriteEffects.None, .242f);

                    //draw particles
                    water.Draw(spriteBatch);
                    grass.Draw(spriteBatch);
                    dirt.Draw(spriteBatch);
                    lava.Draw(spriteBatch);

                    for (int a = 0; a < pieces.Count; a++)
                    {
                        if (!hit[a])
                        {
                            spriteBatch.Draw(pieces[a], position, null,
                                Color.White, 0, origin, 1f, SpriteEffects.None, .25f);
                        }
                        else
                        {
                            spriteBatch.Draw(piecesh[a], position, null,
                                Color.White, 0, origin, 1f, SpriteEffects.None, .25f);
                        }
                    }
                }
                else
                {
                    if (!desH)
                    {
                        //draw core
                        spriteBatch.Draw(core, position, null,
                            Color.White, 0, origin, 1f, SpriteEffects.None, .24f);
                        spriteBatch.Draw(cores[count1], position, null,
                            Color.White, 0, origin, 1f, SpriteEffects.None, .242f);
                        for (int a = 0; a < destruct.Count; a++)
                        {
                            if (!desHit[a])
                            {
                                spriteBatch.Draw(destruct[a], position, null,
                                    Color.White, 0, origin, 1f, SpriteEffects.None, .25f);
                            }
                        }
                    }
                    else
                    {
                        for (int a = 0; a < destruct.Count; a++)
                        {
                            if (!desHit[a])
                            {
                                spriteBatch.Draw(destruct[a], desPiece[a], null,
                                    Color.White, 0, origin, 1f, SpriteEffects.None, .25f);
                            }
                        }

                        if (!noCore)
                        {
                            //draw core
                            spriteBatch.Draw(core, position, null,
                                Color.White, 0, origin, scale, SpriteEffects.None, .24f);
                            spriteBatch.Draw(cores[count1], position, null,
                                Color.White, 0, origin, scale, SpriteEffects.None, .242f);
                        }
                    }

                    //draw particles
                    water.Draw(spriteBatch);
                    grass.Draw(spriteBatch);
                    dirt.Draw(spriteBatch);
                    lava.Draw(spriteBatch);
                }
            }

            /**********************/

            public bool isOn()
            {
                return on;
            }

            /**********************/

            private List<Rectangle> getColl()
            {
                List<Rectangle> collision = new List<Rectangle>();

                collision.Add(new Rectangle((int)(position.X - frameSize.X / 2) + 105, (int)(position.Y - frameSize.Y / 2), 58, 23));
                collision.Add(new Rectangle((int)(position.X - frameSize.X / 2) + 156, (int)(position.Y - frameSize.Y / 2) + 12, 38, 28));
                collision.Add(new Rectangle((int)(position.X - frameSize.X / 2) + 188, (int)(position.Y - frameSize.Y / 2) + 34, 28, 38));
                collision.Add(new Rectangle((int)(position.X - frameSize.X / 2) + 210, (int)(position.Y - frameSize.Y / 2) + 69, 18, 53));
                collision.Add(new Rectangle((int)(position.X - frameSize.X / 2) + 203, (int)(position.Y - frameSize.Y / 2) + 114, 18, 48));
                collision.Add(new Rectangle((int)(position.X - frameSize.X / 2) + 177, (int)(position.Y - frameSize.Y / 2) + 160, 33, 38));
                collision.Add(new Rectangle((int)(position.X - frameSize.X / 2) + 145, (int)(position.Y - frameSize.Y / 2) + 192, 43, 28));
                collision.Add(new Rectangle((int)(position.X - frameSize.X / 2) + 92, (int)(position.Y - frameSize.Y / 2) + 215, 58, 13));
                collision.Add(new Rectangle((int)(position.X - frameSize.X / 2) + 44, (int)(position.Y - frameSize.Y / 2) + 197, 53, 28));
                collision.Add(new Rectangle((int)(position.X - frameSize.X / 2) + 11, (int)(position.Y - frameSize.Y / 2) + 155, 38, 48));
                collision.Add(new Rectangle((int)(position.X - frameSize.X / 2), (int)(position.Y - frameSize.Y / 2) + 107, 18, 53));
                collision.Add(new Rectangle((int)(position.X - frameSize.X / 2), (int)(position.Y - frameSize.Y / 2) + 61, 18, 48));
                collision.Add(new Rectangle((int)(position.X - frameSize.X / 2) + 14, (int)(position.Y - frameSize.Y / 2) + 19, 43, 48));
                collision.Add(new Rectangle((int)(position.X - frameSize.X / 2) + 52, (int)(position.Y - frameSize.Y / 2), 58, 23));

                return collision;
            }
            private List<Rectangle> getColl2()
            {
                List<Rectangle> collision = new List<Rectangle>();

                collision.Add(new Rectangle());
                collision.Add(new Rectangle((int)(position.X - frameSize.X / 2) + 53, (int)(position.Y - frameSize.Y / 2) + 399, 83, 122));
                collision.Add(new Rectangle((int)(position.X - frameSize.X / 2) + 136, (int)(position.Y - frameSize.Y / 2) + 484, 122, 80));
                collision.Add(new Rectangle((int)(position.X - frameSize.X / 2) + 246, (int)(position.Y - frameSize.Y / 2) + 552, 143, 28));
                collision.Add(new Rectangle((int)(position.X - frameSize.X / 2) + 361, (int)(position.Y - frameSize.Y / 2) + 487, 110, 69));
                collision.Add(new Rectangle((int)(position.X - frameSize.X / 2) + 478, (int)(position.Y - frameSize.Y / 2) + 377, 62, 120));
                collision.Add(new Rectangle());

                return collision;
            }

            /**********************/

            private void ejectParts(int a, BallManager bM)
            {
                if (type == 1)
                {
                    switch (a)
                    {
                        //g/d
                        case 1:
                            grass.setPos(new Vector2(bM.getPos().X, bM.getPos().Y + 10));
                            grass.setNum(new Vector2(1, 0));
                            partg = true;
                            dirt.setPos(new Vector2(bM.getPos().X, bM.getPos().Y + 30));
                            dirt.setNum(new Vector2(1, 0));
                            partd = true;
                            break;
                        //g/d
                        case 2:
                            grass.setPos(new Vector2(bM.getPos().X, bM.getPos().Y + 10));
                            grass.setNum(new Vector2(1, 0));
                            partg = true;
                            dirt.setPos(new Vector2(bM.getPos().X, bM.getPos().Y + 30));
                            dirt.setNum(new Vector2(1, 0));
                            partd = true;
                            break;
                        //g/d
                        case 3:
                            grass.setPos(new Vector2(bM.getPos().X - 5, bM.getPos().Y + 5));
                            grass.setNum(new Vector2(2, 1));
                            partg = true;
                            dirt.setPos(new Vector2(bM.getPos().X - 10, bM.getPos().Y + 10));
                            dirt.setNum(new Vector2(2, 1));
                            partd = true;
                            break;
                        //w/d
                        case 4:
                            water.setPos(new Vector2(bM.getPos().X - 10, bM.getPos().Y));
                            water.setNum(new Vector2(2, 1));
                            partw = true;
                            dirt.setPos(new Vector2(bM.getPos().X - 30, bM.getPos().Y));
                            dirt.setNum(new Vector2(2, 1));
                            partd = true;
                            break;
                        //w/d
                        case 5:
                            water.setPos(new Vector2(bM.getPos().X - 10, bM.getPos().Y - 5));
                            water.setNum(new Vector2(2, 1));
                            partw = true;
                            dirt.setPos(new Vector2(bM.getPos().X - 30, bM.getPos().Y - 5));
                            dirt.setNum(new Vector2(2, 1));
                            partd = true;
                            break;
                        //g/d
                        case 6:
                            grass.setPos(new Vector2(bM.getPos().X - 10, bM.getPos().Y - 10));
                            grass.setNum(new Vector2(2, 1));
                            partg = true;
                            dirt.setPos(new Vector2(bM.getPos().X - 30, bM.getPos().Y - 10));
                            dirt.setNum(new Vector2(2, 1));
                            partd = true;
                            break;
                        //g/d
                        case 7:
                            grass.setPos(new Vector2(bM.getPos().X - 10, bM.getPos().Y - 5));
                            grass.setNum(new Vector2(1, 2));
                            partg = true;
                            dirt.setPos(new Vector2(bM.getPos().X - 10, bM.getPos().Y - 15));
                            dirt.setNum(new Vector2(1, 2));
                            partd = true;
                            break;
                        //g/d
                        case 8:
                            grass.setPos(new Vector2(bM.getPos().X, bM.getPos().Y - 5));
                            grass.setNum(new Vector2(1, 2));
                            partg = true;
                            dirt.setPos(new Vector2(bM.getPos().X, bM.getPos().Y - 20));
                            dirt.setNum(new Vector2(1, 2));
                            partd = true;
                            break;
                        //w/d
                        case 9:
                            water.setPos(new Vector2(bM.getPos().X + 5, bM.getPos().Y - 10));
                            water.setNum(new Vector2(1, 2));
                            partw = true;
                            dirt.setPos(new Vector2(bM.getPos().X + 5, bM.getPos().Y - 25));
                            dirt.setNum(new Vector2(1, 2));
                            partd = true;
                            break;
                        //w/d
                        case 10:
                            water.setPos(new Vector2(bM.getPos().X + 5, bM.getPos().Y - 5));
                            water.setNum(new Vector2(-.5f, 1));
                            partw = true;
                            dirt.setPos(new Vector2(bM.getPos().X + 10, bM.getPos().Y - 10));
                            dirt.setNum(new Vector2(-.5f, 1));
                            partd = true;
                            break;
                        //g/d
                        case 11:
                            grass.setPos(new Vector2(bM.getPos().X + 5, bM.getPos().Y));
                            grass.setNum(new Vector2(-.5f, 1));
                            partg = true;
                            dirt.setPos(new Vector2(bM.getPos().X + 20, bM.getPos().Y));
                            dirt.setNum(new Vector2(-.5f, 1));
                            partd = true;
                            break;
                        //g/d
                        case 12:
                            grass.setPos(new Vector2(bM.getPos().X + 5, bM.getPos().Y));
                            grass.setNum(new Vector2(-.5f, 1));
                            partg = true;
                            dirt.setPos(new Vector2(bM.getPos().X + 20, bM.getPos().Y));
                            dirt.setNum(new Vector2(-.5f, 1));
                            partd = true;
                            break;
                        //g/d
                        case 13:
                            grass.setPos(new Vector2(bM.getPos().X + 5, bM.getPos().Y + 5));
                            grass.setNum(new Vector2(1, 0));
                            partg = true;
                            dirt.setPos(new Vector2(bM.getPos().X + 10, bM.getPos().Y + 10));
                            dirt.setNum(new Vector2(1, 0));
                            partd = true;
                            break;
                        //w/d
                        case 14:
                            water.setPos(new Vector2(bM.getPos().X, bM.getPos().Y + 5));
                            water.setNum(new Vector2(1, 0));
                            partw = true;
                            dirt.setPos(new Vector2(bM.getPos().X, bM.getPos().Y + 20));
                            dirt.setNum(new Vector2(1, 0));
                            partd = true;
                            break;
                    }
                }
                else
                {
                    switch (a)
                    {
                        case 1:
                            water.setPos(new Vector2(bM.getPos().X + 5, bM.getPos().Y - 5));
                            water.setNum(new Vector2(-.5f, 1));
                            partw = true;
                            dirt.setPos(new Vector2(bM.getPos().X + 10, bM.getPos().Y - 10));
                            dirt.setNum(new Vector2(-.5f, 1));
                            partd = true;
                            break;

                        case 2:
                            water.setPos(new Vector2(bM.getPos().X + 5, bM.getPos().Y - 10));
                            water.setNum(new Vector2(1, 2));
                            partw = true;
                            dirt.setPos(new Vector2(bM.getPos().X + 5, bM.getPos().Y - 25));
                            dirt.setNum(new Vector2(1, 2));
                            partd = true;
                            break;

                        case 3:
                            grass.setPos(new Vector2(bM.getPos().X, bM.getPos().Y - 5));
                            grass.setNum(new Vector2(1, 2));
                            partg = true;
                            dirt.setPos(new Vector2(bM.getPos().X, bM.getPos().Y - 20));
                            dirt.setNum(new Vector2(1, 2));
                            partd = true;
                            break;

                        case 4:
                            grass.setPos(new Vector2(bM.getPos().X - 10, bM.getPos().Y - 5));
                            grass.setNum(new Vector2(1, 2));
                            partg = true;
                            dirt.setPos(new Vector2(bM.getPos().X - 10, bM.getPos().Y - 15));
                            dirt.setNum(new Vector2(1, 2));
                            partd = true;
                            break;

                        case 5:
                            water.setPos(new Vector2(bM.getPos().X - 10, bM.getPos().Y - 5));
                            water.setNum(new Vector2(2, 1));
                            partw = true;
                            dirt.setPos(new Vector2(bM.getPos().X - 30, bM.getPos().Y - 5));
                            dirt.setNum(new Vector2(2, 1));
                            partd = true;
                            break;
                    }
                }
            }

            /**********************/

            private void chooseLava(GameTime gT)
            {
                if (count3 >= 90)
                {
                    count3 = 0;

                    if (type == 1)
                    {
                        switch (rand.Next(23456) % 7)
                        {
                            case 0:
                                if (hit[1])
                                {
                                    lava.setPos(new Vector2(getColl()[1].Center.X - 33, getColl()[1].Center.Y + 45));
                                    lava.setNum(new Vector2(1, 0));
                                    partl = true;
                                }
                                break;
                            case 1:
                                if (hit[3])
                                {
                                    lava.setPos(new Vector2(getColl()[3].Center.X - 55, getColl()[3].Center.Y + 9));
                                    lava.setNum(new Vector2(3, 1));
                                    partl = true;
                                }
                                break;
                            case 2:
                                if (hit[5])
                                {
                                    lava.setPos(new Vector2(getColl()[5].Center.X - 49, getColl()[5].Center.Y - 43));
                                    lava.setNum(new Vector2(2, 1));
                                    partl = true;
                                }
                                break;
                            case 3:
                                if (hit[7])
                                {
                                    lava.setPos(new Vector2(getColl()[7].Center.X - 4, getColl()[7].Center.Y - 62));
                                    lava.setNum(new Vector2(1, 2));
                                    partl = true;
                                }
                                break;
                            case 4:
                                if (hit[9])
                                {
                                    lava.setPos(new Vector2(getColl()[9].Center.X + 61, getColl()[9].Center.Y - 47));
                                    lava.setNum(new Vector2(-.5f, 1));
                                    partl = true;
                                }
                                break;
                            case 5:
                                if (hit[11])
                                {
                                    lava.setPos(new Vector2(getColl()[11].Center.X + 62, getColl()[11].Center.Y + 17));
                                    lava.setNum(new Vector2(-.5f, 1));
                                    partl = true;
                                }
                                break;
                            case 6:
                                if (hit[13])
                                {
                                    lava.setPos(new Vector2(getColl()[13].Center.X + 23, getColl()[13].Center.Y + 50));
                                    lava.setNum(new Vector2(1, 0));
                                    partl = true;
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (rand.Next(23456) % 5)
                        {
                            case 0:
                                if (hit[1])
                                {
                                    lava.setPos(new Vector2(getColl2()[1].Center.X + 10, getColl2()[1].Center.Y));
                                    lava.setNum(new Vector2(0, 1));
                                    partl = true;
                                }
                                break;

                            case 1:
                                if (hit[2])
                                {
                                    lava.setPos(new Vector2(getColl2()[2].Center.X + 10, getColl2()[2].Center.Y - 10));
                                    lava.setNum(new Vector2(0, 2));
                                    partl = true;
                                }
                                break;

                            case 2:
                                if (hit[3])
                                {
                                    lava.setPos(new Vector2(getColl2()[3].Center.X + 5, getColl2()[3].Center.Y - 20));
                                    lava.setNum(new Vector2(1, 2));
                                    partl = true;
                                }
                                break;

                            case 3:
                                if (hit[4])
                                {
                                    lava.setPos(new Vector2(getColl2()[4].Center.X, getColl2()[4].Center.Y));
                                    lava.setNum(new Vector2(1.5f, 1.5f));
                                    partl = true;
                                }
                                break;

                            case 4:
                                if (hit[5])
                                {
                                    lava.setPos(new Vector2(getColl2()[5].Center.X, getColl2()[5].Center.Y));
                                    lava.setNum(new Vector2(1.5f, 2.2f));
                                    partl = true;
                                }
                                break;
                        }
                    }
                }
                else
                {
                    count3++;
                }
            }

            /**********************/

            private void movePlanet()
            {
                angle += .5f;

                position.X = (float)(500 * Math.Cos(angle * 2 * Math.PI / 360)) + 630;
                position.Y = (float)(150 * Math.Sin(angle * 2 * Math.PI / 360)) + 250;

                if (angle > 360)
                {
                    angle = 0;
                }
            }
            private void animateCore()
            {

                //animate core
                if (ani < 5)
                {
                    ani++;
                }
                else
                {
                    ani = 0;
                    if (count1 < 9 && !flip)
                    {
                        count1++;
                    }
                    else if (flip)
                    {
                        if (count1 <= 0)
                        {
                            flip = false;
                            count1 = 0;
                        }
                        else
                        {
                            count1--;
                        }
                    }
                    else
                    {
                        flip = true;
                        count1--;
                    }
                }
            }
            private void updateParticles(GameTime gT)
            {
                if (partw && c4 < 3)
                {
                    c4++;
                }
                else
                {
                    c4 = 0;
                    partw = false;
                }
                if (partd && c5 < 3)
                {
                    c5++;
                }
                else
                {
                    c5 = 0;
                    partd = false;
                }
                if (partg && c3 < 3)
                {
                    c3++;
                }
                else
                {
                    c3 = 0;
                    partg = false;
                }
                if (partl && c6 < 2)
                {
                    c6++;
                }
                else
                {
                    c6 = 0;
                    partl = false;
                }
                water.Update(partw, gT);
                grass.Update(partg, gT);
                dirt.Update(partd, gT);
                lava.Update(partl, gT);
            }

            /**********************/

            public void extraColl(ExtraBallManager xBall, StatsManager stats, SoundManager sManager)
            {
                //check collision
                for (int c = 0; c < 2; c++)
                {
                    if (xBall.getBalls()[c].getOn())
                    {
                        for (int a = 0; a < pieces.Count; a++)
                        {
                            if (type == 1)
                            {
                                if (getColl()[a].Intersects(xBall.getBalls()[c].getCollRect()))
                                {
                                    //top
                                    if (a == 0 || a == 13 || a == 12 || a == 1)
                                    {
                                        if (xBall.getBalls()[c].getBall().getDir().Y > 0)
                                        {
                                            xBall.getBalls()[c].bounce(new Vector2(1, -1));
                                            xBall.getBalls()[c].setPos(new Vector2(xBall.getBalls()[c].getPos().X,
                                                xBall.getBalls()[c].getPos().Y - 10));
                                            xBall.getBalls()[c].setColl(false);
                                            if (count2 >= 14)
                                            {
                                                onF = false;
                                                stats.addPoint(6);
                                            }
                                            if (!hit[a])
                                            {
                                                hit[a] = true;
                                                count2++;
                                                stats.addPoint(5);
                                                ejectParts(a + 1, xBall.getBalls()[c]);
                                            }
                                        }
                                    }
                                    //bottom
                                    else if (a == 8 || a == 7 || a == 6)
                                    {
                                        if (xBall.getBalls()[c].getBall().getDir().Y < 0)
                                        {
                                            xBall.getBalls()[c].bounce(new Vector2(1, -1));
                                            xBall.getBalls()[c].setPos(new Vector2(xBall.getBalls()[c].getPos().X,
                                                xBall.getBalls()[c].getPos().Y + 10));
                                            xBall.getBalls()[c].setColl(false);
                                            if (count2 >= 14)
                                            {
                                                onF = false;
                                                stats.addPoint(6);
                                            }
                                            if (!hit[a])
                                            {
                                                hit[a] = true;
                                                count2++;
                                                stats.addPoint(5);
                                                ejectParts(a + 1, xBall.getBalls()[c]);
                                            }
                                        }
                                    }
                                    //left
                                    else if (a == 9 || a == 10 || a == 11)
                                    {
                                        if (xBall.getBalls()[c].getBall().getDir().X > 0)
                                        {
                                            xBall.getBalls()[c].bounce(new Vector2(-1, 1));
                                            xBall.getBalls()[c].setPos(new Vector2(xBall.getBalls()[c].getPos().X - 10,
                                                xBall.getBalls()[c].getPos().Y));
                                            xBall.getBalls()[c].setColl(false);
                                            if (count2 >= 14)
                                            {
                                                onF = false;
                                                stats.addPoint(6);
                                            }
                                            if (!hit[a])
                                            {
                                                hit[a] = true;
                                                count2++;
                                                stats.addPoint(5);
                                                ejectParts(a + 1, xBall.getBalls()[c]);
                                            }
                                        }
                                    }
                                    //right
                                    else if (a == 2 || a == 3 || a == 4 || a == 5)
                                    {
                                        if (xBall.getBalls()[c].getBall().getDir().X < 0)
                                        {
                                            xBall.getBalls()[c].bounce(new Vector2(-1, 1));
                                            xBall.getBalls()[c].setPos(new Vector2(xBall.getBalls()[c].getPos().X - 10,
                                                xBall.getBalls()[c].getPos().Y));
                                            xBall.getBalls()[c].setColl(false);
                                            if (count2 >= 14)
                                            {
                                                onF = false;
                                                stats.addPoint(6);
                                            }
                                            if (!hit[a])
                                            {
                                                hit[a] = true;
                                                count2++;
                                                stats.addPoint(5);
                                                ejectParts(a + 1, xBall.getBalls()[c]);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (getColl2()[a].Intersects(xBall.getBalls()[c].getCollRect()))
                                {
                                    //left piece
                                    if (a == 1)
                                    {
                                        if (xBall.getBalls()[c].getBall().getDir().X > 0)
                                        {
                                            xBall.getBalls()[c].bounce(new Vector2(-1, 1));
                                            xBall.getBalls()[c].setPos(new Vector2(xBall.getBalls()[c].getPos().X - 10,
                                                xBall.getBalls()[c].getPos().Y));
                                            xBall.getBalls()[c].setColl(false);
                                            if (count2 >= 5)
                                            {
                                                onF = false;
                                                stats.addPoint(6);
                                            }
                                            if (!hit[a])
                                            {
                                                hit[a] = true;
                                                count2++;
                                                stats.addPoint(5);
                                                ejectParts(a, xBall.getBalls()[c]);
                                            }
                                        }
                                    }
                                    //right piece
                                    else if (a == 5)
                                    {
                                        if (xBall.getBalls()[c].getBall().getDir().X < 0)
                                        {
                                            xBall.getBalls()[c].bounce(new Vector2(-1, 1));
                                            xBall.getBalls()[c].setPos(new Vector2(xBall.getBalls()[c].getPos().X - 10,
                                                xBall.getBalls()[c].getPos().Y));
                                            xBall.getBalls()[c].setColl(false);
                                            if (count2 >= 5)
                                            {
                                                onF = false;
                                                stats.addPoint(6);
                                            }
                                            if (!hit[a])
                                            {
                                                hit[a] = true;
                                                count2++;
                                                stats.addPoint(5);
                                                ejectParts(a, xBall.getBalls()[c]);
                                            }
                                        }
                                    }
                                    //bottom pieces
                                    else if (a == 2 || a == 3 || a == 4)
                                    {
                                        if (xBall.getBalls()[c].getBall().getDir().Y < 0)
                                        {
                                            xBall.getBalls()[c].bounce(new Vector2(1, -1));
                                            xBall.getBalls()[c].setPos(new Vector2(xBall.getBalls()[c].getPos().X,
                                                xBall.getBalls()[c].getPos().Y + 10));
                                            xBall.getBalls()[c].setColl(false);
                                            if (count2 >= 5)
                                            {
                                                onF = false;
                                                stats.addPoint(6);
                                            }
                                            if (!hit[a])
                                            {
                                                hit[a] = true;
                                                count2++;
                                                stats.addPoint(5);
                                                ejectParts(a, xBall.getBalls()[c]);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            /**********************/

            private void destroy(GameTime gT, BallManager bM, ExtraBallManager xBall)
            {
                if (type == 1)
                {
                    if (!desH)
                    {
                        movePlanet();
                        chooseLava(gT);
                        animateCore();
                    }
                    else
                    {
                        if (!noCore)
                        {
                            blowUpCore();
                        }
                        moveParts();
                    }
                    updateParticles(gT);

                    //check collisions
                    for (int f = 0; f < 9; f++)
                    {
                        if (!noCore)
                        {
                            if (!desHit[f])
                            {
                                if (bM.getCollRect().Intersects(desColl()[f]))
                                {
                                    if (!desH)
                                    {
                                        desH = true;
                                        bM.setBottomOn(true);
                                        desPiece[0] = position;
                                        desPiece[1] = position;
                                        desPiece[2] = position;
                                        desPiece[3] = position;
                                        desPiece[4] = position;
                                        desPiece[5] = position;
                                        desPiece[6] = position;
                                        desPiece[7] = position;
                                        desPiece[8] = position;
                                    }

                                    setPart(f);
                                    desHit[f] = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (!desH)
                    {
                        chooseLava(gT);
                        animateCore();
                    }
                    else
                    {
                        if (!noCore)
                        {
                            blowUpCore();
                        }
                        moveParts1();
                    }
                    updateParticles(gT);

                    //check collisions
                    for (int f = 0; f < 11; f++)
                    {
                        if (!noCore)
                        {
                            if (!desHit[f])
                            {
                                if (bM.getCollRect().Intersects(desColl1()[f]))
                                {
                                    if (!desH)
                                    {
                                        desH = true;
                                        bM.setBottomOn(true);
                                        desPiece[0] = position;
                                        desPiece[1] = position;
                                        desPiece[2] = position;
                                        desPiece[3] = position;
                                        desPiece[4] = position;
                                        desPiece[5] = position;
                                        desPiece[6] = position;
                                        desPiece[7] = position;
                                        desPiece[8] = position;
                                        desPiece[9] = position;
                                        desPiece[10] = position;
                                    }

                                    setPart(f);
                                    desHit[f] = true;
                                }
                            }
                        }
                    }

                }
            }
            private Rectangle[] desColl()
            {
                Rectangle[] buttz = new Rectangle[9];

                buttz[0] = new Rectangle(((int)(position.X - (225 / 2)) + 133), ((int)(position.Y - (225 / 2)) + 28), 44, 55);
                buttz[1] = new Rectangle(((int)(position.X - (225 / 2)) + 162), ((int)(position.Y - (225 / 2)) + 76), 51, 82);
                buttz[2] = new Rectangle(((int)(position.X - (225 / 2)) + 128), ((int)(position.Y - (225 / 2)) + 144), 41, 63);
                buttz[3] = new Rectangle(((int)(position.X - (225 / 2)) + 80), ((int)(position.Y - (225 / 2)) + 146), 40, 69);
                buttz[4] = new Rectangle(((int)(position.X - (225 / 2)) + 32), ((int)(position.Y - (225 / 2)) + 120), 52, 64);
                buttz[5] = new Rectangle(((int)(position.X - (225 / 2)) + 18), ((int)(position.Y - (225 / 2)) + 58), 57, 77);
                buttz[6] = new Rectangle(((int)(position.X - (225 / 2)) + 67), ((int)(position.Y - (225 / 2)) + 22), 59, 67);
                buttz[7] = new Rectangle(((int)(position.X - (225 / 2)) + 124), ((int)(position.Y - (225 / 2)) + 91), 34, 49);
                buttz[8] = new Rectangle(((int)(position.X - (225 / 2)) + 90), ((int)(position.Y - (225 / 2)) + 106), 39, 40);

                return buttz;
            }
            private Rectangle[] desColl1()
            {
                Rectangle[] buttz = new Rectangle[11];

                buttz[0] = new Rectangle(((int)(position.X - (590 / 2)) + 34), ((int)(position.Y - (590 / 2)) + 301), 123, 103);
                buttz[1] = new Rectangle(((int)(position.X - (590 / 2)) + 186), ((int)(position.Y - (590 / 2)) + 296), 84, 58);
                buttz[2] = new Rectangle(((int)(position.X - (590 / 2)) + 192), ((int)(position.Y - (590 / 2)) + 379), 77, 98);
                buttz[3] = new Rectangle(((int)(position.X - (590 / 2)) + 112), ((int)(position.Y - (590 / 2)) + 437), 58, 92);
                buttz[4] = new Rectangle(((int)(position.X - (590 / 2)) + 194), ((int)(position.Y - (590 / 2)) + 486), 111, 86);
                buttz[5] = new Rectangle(((int)(position.X - (590 / 2)) + 324), ((int)(position.Y - (590 / 2)) + 422), 86, 137);
                buttz[6] = new Rectangle(((int)(position.X - (590 / 2)) + 260), ((int)(position.Y - (590 / 2)) + 308), 85, 98);
                buttz[7] = new Rectangle(((int)(position.X - (590 / 2)) + 350), ((int)(position.Y - (590 / 2)) + 297), 71, 64);
                buttz[8] = new Rectangle(((int)(position.X - (590 / 2)) + 339), ((int)(position.Y - (590 / 2)) + 368), 72, 57);
                buttz[9] = new Rectangle(((int)(position.X - (590 / 2)) + 436), ((int)(position.Y - (590 / 2)) + 304), 109, 123);
                buttz[10] = new Rectangle(((int)(position.X - (590 / 2)) + 414), ((int)(position.Y - (590 / 2)) + 421), 57, 93);

                return buttz;
            }
            private void moveParts()
            {
                //move piece 1
                if (!desHit[0])
                {
                    desPiece[0].X += 0.8f;
                    desPiece[0].Y += -0.8f;
                }
                //move piece 2
                if (!desHit[1])
                {
                    desPiece[1].X += 0.8f;
                    desPiece[1].Y += -0.5f;
                }
                //move piece 3
                if (!desHit[2])
                {
                    desPiece[2].X += 0.5f;
                    desPiece[2].Y += 0.5f;
                }
                //move piece 4
                if (!desHit[3])
                {
                    desPiece[3].X += -0.5f;
                    desPiece[3].Y += 0.8f;
                }
                //move piece 5
                if (!desHit[4])
                {
                    desPiece[4].X += -0.8f;
                    desPiece[4].Y += 0.5f;
                }
                //move piece 6
                if (!desHit[5])
                {
                    desPiece[5].X += -0.8f;
                    desPiece[5].Y += -0.5f;
                }
                //move piece 7
                if (!desHit[6])
                {
                    desPiece[6].X += -0.5f;
                    desPiece[6].Y += -0.8f;
                }
                //move piece 8
                if (!desHit[7])
                {
                    desPiece[7].X += 0.3f;
                    desPiece[7].Y += 0.3f;
                }
                //move piece 9
                if (!desHit[8])
                {
                    desPiece[8].X += -0.3f;
                    desPiece[8].Y += -0.3f;
                }
            }
            private void moveParts1()
            {
                //1
                if (!desHit[0])
                {
                    desPiece[0].X -= 0.8f;
                    desPiece[0].Y += 0.5f;
                }
                //2
                if (!desHit[1])
                {
                    desPiece[1].X -= 0.2f;
                    desPiece[1].Y += 0.3f;
                }
                //3
                if (!desHit[2])
                {
                    desPiece[2].X -= 0.3f;
                    desPiece[2].Y += 0.4f;
                }
                //4
                if (!desHit[3])
                {
                    desPiece[3].X -= 0.2f;
                    desPiece[3].Y += 0.5f;
                }
                //5
                if (!desHit[4])
                {
                    desPiece[4].X -= 0.1f;
                    desPiece[4].Y += 0.6f;
                }
                //6
                if (!desHit[5])
                {
                    desPiece[5].X += 0.2f;
                    desPiece[5].Y += 0.6f;
                }
                //7
                if (!desHit[6])
                {
                    desPiece[6].X += 0.1f;
                    desPiece[6].Y += 0.3f;
                }
                //8
                if (!desHit[7])
                {
                    desPiece[7].X += 0.2f;
                    desPiece[7].Y += 0.4f;
                }
                //9
                if (!desHit[8])
                {
                    desPiece[8].X += 0.2f;
                    desPiece[8].Y += 0.3f;
                }
                //10
                if (!desHit[9])
                {
                    desPiece[9].X += 0.5f;
                    desPiece[9].Y += 0.2f;
                }
                //11
                if (!desHit[10])
                {
                    desPiece[10].X += 0.5f;
                    desPiece[10].Y += 0.5f;
                }
            }
            private void blowUpCore()
            {
                //get larger for 20 frames
                if (c7 <= 0 && c8 < 15 && !b1)
                {
                    c7 = 1;
                    c8++;
                    scale += 0.03f;
                    if (c8 >= 15)
                    {
                        b1 = true;
                    }
                }
                else if (!b1)
                {
                    c7--;
                }

                //get smaller for 20 frames
                if (c7 <= 0 && c8 > 0 && b1)
                {
                    c7 = 1;
                    c8--;
                    scale -= 0.09f;
                    if (scale <= 0)
                    {
                        noCore = true;
                        on = false;
                    }
                }
                else if (b1)
                {
                    c7--;
                    if (c8 <= 0)
                    {
                        noCore = true;
                        on = false;
                    }
                }


            }
            private void setPart(int x)
            {
                if (type == 1)
                {
                    switch (x)
                    {
                        //g/w/d
                        case 0:
                            grass.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y + 5));
                            grass.setNum(new Vector2(1, 1.2f));
                            partg = true;
                            dirt.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y));
                            dirt.setNum(new Vector2(1.2f, 1));
                            partd = true;
                            water.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y - 05));
                            water.setNum(new Vector2(1, 1));
                            partw = true;
                            break;
                        //g/w/d
                        case 1:
                            grass.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y + 5));
                            grass.setNum(new Vector2(1, 1.2f));
                            partg = true;
                            dirt.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y));
                            dirt.setNum(new Vector2(1.2f, 1));
                            partd = true;
                            water.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y - 05));
                            water.setNum(new Vector2(1, 1));
                            partw = true;
                            break;
                        //g/w/d
                        case 2:
                            grass.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y + 5));
                            grass.setNum(new Vector2(1, 1.2f));
                            partg = true;
                            dirt.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y));
                            dirt.setNum(new Vector2(1.2f, 1));
                            partd = true;
                            water.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y - 05));
                            water.setNum(new Vector2(1, 1));
                            partw = true;
                            break;
                        //g/w/d
                        case 3:
                            grass.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y + 5));
                            grass.setNum(new Vector2(1, 1.2f));
                            partg = true;
                            dirt.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y));
                            dirt.setNum(new Vector2(1.2f, 1));
                            partd = true;
                            water.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y - 05));
                            water.setNum(new Vector2(1, 1));
                            partw = true;
                            break;
                        //g/w/d
                        case 4:
                            grass.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y + 5));
                            grass.setNum(new Vector2(1, 1.2f));
                            partg = true;
                            dirt.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y));
                            dirt.setNum(new Vector2(1.2f, 1));
                            partd = true;
                            water.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y - 05));
                            water.setNum(new Vector2(1, 1));
                            partw = true;
                            break;
                        //g/w/d
                        case 5:
                            grass.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y + 5));
                            grass.setNum(new Vector2(1, 1.2f));
                            partg = true;
                            dirt.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y));
                            dirt.setNum(new Vector2(1.2f, 1));
                            partd = true;
                            water.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y - 05));
                            water.setNum(new Vector2(1, 1));
                            partw = true;
                            break;
                        //g/w/d
                        case 6:
                            grass.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y + 5));
                            grass.setNum(new Vector2(1, 1.2f));
                            partg = true;
                            dirt.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y));
                            dirt.setNum(new Vector2(1.2f, 1));
                            partd = true;
                            water.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y - 05));
                            water.setNum(new Vector2(1, 1));
                            partw = true;
                            break;
                        //w/d
                        case 7:
                            dirt.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y));
                            dirt.setNum(new Vector2(1.2f, 1));
                            partd = true;
                            water.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y - 05));
                            water.setNum(new Vector2(1, 1));
                            partw = true;
                            break;
                        //g/w/d
                        case 8:
                            grass.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y + 5));
                            grass.setNum(new Vector2(1, 1.2f));
                            partg = true;
                            dirt.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y));
                            dirt.setNum(new Vector2(1.2f, 1));
                            partd = true;
                            water.setPos(new Vector2(desColl()[x].Center.X, desColl()[x].Center.Y - 05));
                            water.setNum(new Vector2(1, 1));
                            partw = true;
                            break;
                    }
                }
                else
                {
                    switch (x)
                    {
                        //g/w/d
                        case 0:
                            grass.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y + 5));
                            grass.setNum(new Vector2(1, 1.2f));
                            partg = true;
                            dirt.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y));
                            dirt.setNum(new Vector2(1.2f, 1));
                            partd = true;
                            water.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y - 05));
                            water.setNum(new Vector2(1, 1));
                            partw = true;
                            break;
                        //g/w/d
                        case 1:
                            grass.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y + 5));
                            grass.setNum(new Vector2(1, 1.2f));
                            partg = true;
                            dirt.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y));
                            dirt.setNum(new Vector2(1.2f, 1));
                            partd = true;
                            water.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y - 05));
                            water.setNum(new Vector2(1, 1));
                            partw = true;
                            break;
                        //g/w/d
                        case 2:
                            grass.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y + 5));
                            grass.setNum(new Vector2(1, 1.2f));
                            partg = true;
                            dirt.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y));
                            dirt.setNum(new Vector2(1.2f, 1));
                            partd = true;
                            water.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y - 05));
                            water.setNum(new Vector2(1, 1));
                            partw = true;
                            break;
                        //g/w/d
                        case 3:
                            grass.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y + 5));
                            grass.setNum(new Vector2(1, 1.2f));
                            partg = true;
                            dirt.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y));
                            dirt.setNum(new Vector2(1.2f, 1));
                            partd = true;
                            water.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y - 05));
                            water.setNum(new Vector2(1, 1));
                            partw = true;
                            break;
                        //g/w/d
                        case 4:
                            grass.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y + 5));
                            grass.setNum(new Vector2(1, 1.2f));
                            partg = true;
                            dirt.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y));
                            dirt.setNum(new Vector2(1.2f, 1));
                            partd = true;
                            water.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y - 05));
                            water.setNum(new Vector2(1, 1));
                            partw = true;
                            break;
                        //g/w/d
                        case 5:
                            grass.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y + 5));
                            grass.setNum(new Vector2(1, 1.2f));
                            partg = true;
                            dirt.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y));
                            dirt.setNum(new Vector2(1.2f, 1));
                            partd = true;
                            water.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y - 05));
                            water.setNum(new Vector2(1, 1));
                            partw = true;
                            break;
                        //g/w/d
                        case 6:
                            grass.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y + 5));
                            grass.setNum(new Vector2(1, 1.2f));
                            partg = true;
                            dirt.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y));
                            dirt.setNum(new Vector2(1.2f, 1));
                            partd = true;
                            water.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y - 05));
                            water.setNum(new Vector2(1, 1));
                            partw = true;
                            break;
                        //w/d
                        case 7:
                            dirt.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y));
                            dirt.setNum(new Vector2(1.2f, 1));
                            partd = true;
                            water.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y - 05));
                            water.setNum(new Vector2(1, 1));
                            partw = true;
                            break;
                        //g/w/d
                        case 8:
                            grass.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y + 5));
                            grass.setNum(new Vector2(1, 1.2f));
                            partg = true;
                            dirt.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y));
                            dirt.setNum(new Vector2(1.2f, 1));
                            partd = true;
                            water.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y - 05));
                            water.setNum(new Vector2(1, 1));
                            partw = true;
                            break;
                        case 9:
                            grass.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y + 5));
                            grass.setNum(new Vector2(1, 1.2f));
                            partg = true;
                            dirt.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y));
                            dirt.setNum(new Vector2(1.2f, 1));
                            partd = true;
                            water.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y - 05));
                            water.setNum(new Vector2(1, 1));
                            partw = true;
                            break;
                        case 10:
                            grass.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y + 5));
                            grass.setNum(new Vector2(1, 1.2f));
                            partg = true;
                            dirt.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y));
                            dirt.setNum(new Vector2(1.2f, 1));
                            partd = true;
                            water.setPos(new Vector2(desColl1()[x].Center.X, desColl1()[x].Center.Y - 05));
                            water.setNum(new Vector2(1, 1));
                            partw = true;
                            break;
                    }
                }
            }
        }
    }
}
