/******************************************
 * 06/21/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  BlockManager.cs 
 * 
 *****************************************/
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Br8kIn_
{
    class BlockManager
    {
        //instance variables
        List<Blocks> blocks;
        List<int> type;
        List<Vector2> pos, pos2;
        bool x;
        int lvl;

        /*****************************************/

        //constructor
        public BlockManager()
        {
            blocks = new List<Blocks>();
            type = new List<int>();
            pos = new List<Vector2>();
            pos2 = new List<Vector2>();
            x = false;
        }

        /*****************************************/

        //load
        public void Load(int lvlNum, Engine engine, SoundManager sManager)
        {
            blocks = new List<Blocks>();
            type = new List<int>();
            pos = new List<Vector2>();
            pos2 = new List<Vector2>();
            lvl = lvlNum;

            switch (lvl)
            {
                case 0:
                    break;

                //lvl 1
                case 1:

                    //load plates
                    blocks.Add(new Plates(5));
                    blocks[0].Load(engine, type);

                    //set plates positions
                    pos.Add(new Vector2(225, 200));
                    //pos.Add(new Vector2(450, 300));
                    pos.Add(new Vector2(650, 200));
                    //pos.Add(new Vector2(850, 300));
                    pos.Add(new Vector2(1050, 200));

                    //pos.Add(new Vector2(250, 150));
                    pos.Add(new Vector2(450, 100));
                    //pos.Add(new Vector2(650, 150));
                    pos.Add(new Vector2(825, 100));
                    //pos.Add(new Vector2(1050, 150));
                    blocks[0].setPositions(pos);
                    break;

                //lvl 2
                case 2:

                    //load pots
                    blocks.Add(new Pots(5));
                    type.Add(1);//red
                    type.Add(2);//blue
                    type.Add(3);//green
                    type.Add(4);//yellow
                    type.Add(1);//red
                    blocks[0].Load(engine, type);

                    //set pots positions
                    pos.Add(new Vector2(400, 100));
                    pos.Add(new Vector2(900, 100));

                    pos.Add(new Vector2(200, 200));

                    pos.Add(new Vector2(650, 200));
                    pos.Add(new Vector2(1050, 200));


                    blocks[0].setPositions(pos);
                    break;

                case 3:

                    //load pots
                    blocks.Add(new Pots(3));
                    type.Add(1);//red
                    type.Add(2);//blue
                    type.Add(3);//green
                    blocks[0].Load(engine, type);

                    //load plates
                    blocks.Add(new Plates(2));
                    blocks[1].Load(engine, type);

                    //set pots positions
                    pos.Add(new Vector2(400, 100));
                    pos.Add(new Vector2(650, 200));
                    pos.Add(new Vector2(900, 100));
                    blocks[0].setPositions(pos);

                    //set plates positions
                    pos = new List<Vector2>();
                    pos.Add(new Vector2(230, 300));
                    pos.Add(new Vector2(1100, 300));
                    blocks[1].setPositions(pos);
                    break;

                case 4:
                    sManager.setAmbientSound("backgroundWind");
                    sManager.playAmbientSound();
                    //load clouds row 1
                    blocks.Add(new Clouds(3, 2, new Vector2(-.6f, 0), .24f, 240));
                    blocks[0].Load(engine);

                    //set big clouds pos row 1
                    pos.Add(new Vector2(430, 200));
                    pos.Add(new Vector2(30, 205));
                    pos.Add(new Vector2(780, 215));

                    //set small clouds pos row 1
                    pos2.Add(new Vector2(970, 230));
                    pos2.Add(new Vector2(330, 210));
                    blocks[0].setPositions(pos, pos2);

                    //load clouds row 2
                    blocks.Add(new Clouds(3, 2, new Vector2(.6f, 0), .26f, 120));
                    blocks[1].Load(engine);

                    //set big clouds pos row 2
                    pos = new List<Vector2>();
                    pos2 = new List<Vector2>();
                    pos.Add(new Vector2(230, 230));
                    pos.Add(new Vector2(70, 225));
                    pos.Add(new Vector2(980, 215));

                    //set small clouds pos row 2
                    pos2.Add(new Vector2(830, 210));
                    pos2.Add(new Vector2(335, 200));
                    blocks[1].setPositions(pos, pos2);

                    //load clouds row 3
                    blocks.Add(new Clouds(3, 2, new Vector2(-.6f, 0), .28f, 440));
                    blocks[2].Load(engine);

                    //set big clouds pos row 3
                    pos = new List<Vector2>();
                    pos2 = new List<Vector2>();
                    pos.Add(new Vector2(160, 300));
                    pos.Add(new Vector2(270, 305));
                    pos.Add(new Vector2(920, 305));

                    //set small clouds pos row 3
                    pos2.Add(new Vector2(780, 290));
                    pos2.Add(new Vector2(365, 275));
                    blocks[2].setPositions(pos, pos2);
                    break;

                case 5:

                    //load clouds row 1
                    blocks.Add(new Clouds(3, 2, new Vector2(-.6f, 0), .24f, 240));
                    blocks[0].Load(engine);

                    //set big clouds pos row 1
                    pos.Add(new Vector2(430, 200));
                    pos.Add(new Vector2(30, 205));
                    pos.Add(new Vector2(780, 215));

                    //set small clouds pos row 1
                    pos2.Add(new Vector2(970, 230));
                    pos2.Add(new Vector2(330, 210));
                    blocks[0].setPositions(pos, pos2);

                    //load clouds row 2
                    blocks.Add(new Clouds(3, 2, new Vector2(.6f, 0), .26f, 120));
                    blocks[1].Load(engine);

                    //set big clouds pos row 2
                    pos = new List<Vector2>();
                    pos2 = new List<Vector2>();
                    pos.Add(new Vector2(230, 230));
                    pos.Add(new Vector2(70, 225));
                    pos.Add(new Vector2(980, 215));

                    //set small clouds pos row 2
                    pos2.Add(new Vector2(830, 210));
                    pos2.Add(new Vector2(335, 200));
                    blocks[1].setPositions(pos, pos2);

                    //load clouds row 3
                    blocks.Add(new Clouds(3, 2, new Vector2(-.6f, 0), .28f, 440));
                    blocks[2].Load(engine);

                    //set big clouds pos row 3
                    pos = new List<Vector2>();
                    pos2 = new List<Vector2>();
                    pos.Add(new Vector2(160, 300));
                    pos.Add(new Vector2(270, 305));
                    pos.Add(new Vector2(920, 305));

                    //set small clouds pos row 3
                    pos2.Add(new Vector2(780, 290));
                    pos2.Add(new Vector2(365, 275));
                    blocks[2].setPositions(pos, pos2);
                    break;

                case 6:

                    //load clouds row 1
                    blocks.Add(new Clouds(3, 2, new Vector2(-.6f, 0), .24f, 240));
                    blocks[0].Load(engine);

                    //set big clouds pos row 1
                    pos.Add(new Vector2(430, 200));
                    pos.Add(new Vector2(30, 205));
                    pos.Add(new Vector2(780, 215));

                    //set small clouds pos row 1
                    pos2.Add(new Vector2(970, 230));
                    pos2.Add(new Vector2(330, 210));
                    blocks[0].setPositions(pos, pos2);

                    //load clouds row 2
                    blocks.Add(new Clouds(3, 2, new Vector2(.6f, 0), .26f, 120));
                    blocks[1].Load(engine);

                    //set big clouds pos row 2
                    pos = new List<Vector2>();
                    pos2 = new List<Vector2>();
                    pos.Add(new Vector2(230, 230));
                    pos.Add(new Vector2(70, 225));
                    pos.Add(new Vector2(980, 215));

                    //set small clouds pos row 2
                    pos2.Add(new Vector2(830, 210));
                    pos2.Add(new Vector2(335, 200));
                    blocks[1].setPositions(pos, pos2);

                    //load clouds row 3
                    blocks.Add(new Clouds(3, 2, new Vector2(-.6f, 0), .28f, 440));
                    blocks[2].Load(engine);

                    //set big clouds pos row 3
                    pos = new List<Vector2>();
                    pos2 = new List<Vector2>();
                    pos.Add(new Vector2(160, 300));
                    pos.Add(new Vector2(270, 305));
                    pos.Add(new Vector2(920, 305));

                    //set small clouds pos row 3
                    pos2.Add(new Vector2(780, 290));
                    pos2.Add(new Vector2(365, 275));
                    blocks[2].setPositions(pos, pos2);
                    break;

                case 7:
                    sManager.stopAmbientSound();
                    //load aliens row 1
                    blocks.Add(new Aliens(5, 1, new Vector2(1.5f, 0), 60, false));
                    type.Add(4);
                    type.Add(2);
                    type.Add(4);
                    type.Add(1);
                    type.Add(4);
                    blocks[0].Load(engine, type);
                    pos.Add(new Vector2(250, 130));
                    pos.Add(new Vector2(400, 130));
                    pos.Add(new Vector2(550, 130));
                    pos.Add(new Vector2(700, 130));
                    pos.Add(new Vector2(850, 130));
                    blocks[0].setPositions(pos);

                    //load aliens row 2
                    pos = new List<Vector2>();
                    type = new List<int>();
                    blocks.Add(new Aliens(5, -1, new Vector2(2, 0), 120, false));
                    type.Add(3);
                    type.Add(2);
                    type.Add(3);
                    type.Add(1);
                    type.Add(2);
                    blocks[1].Load(engine, type);
                    pos.Add(new Vector2(250, 200));
                    pos.Add(new Vector2(400, 200));
                    pos.Add(new Vector2(550, 200));
                    pos.Add(new Vector2(700, 200));
                    pos.Add(new Vector2(850, 200));
                    blocks[1].setPositions(pos);

                    //load aliens row 3
                    pos = new List<Vector2>();
                    type = new List<int>();
                    blocks.Add(new Aliens(5, 1, new Vector2(1.7f, 0), 220, false));
                    type.Add(1);
                    type.Add(1);
                    type.Add(3);
                    type.Add(2);
                    type.Add(1);
                    blocks[2].Load(engine, type);
                    pos.Add(new Vector2(250, 270));
                    pos.Add(new Vector2(400, 270));
                    pos.Add(new Vector2(550, 270));
                    pos.Add(new Vector2(700, 270));
                    pos.Add(new Vector2(850, 270));
                    blocks[2].setPositions(pos);

                    //load aliens row 4
                    pos = new List<Vector2>();
                    type = new List<int>();
                    blocks.Add(new Aliens(5, -1, new Vector2(1.3f, 0), 320, false));
                    type.Add(2);
                    type.Add(3);
                    type.Add(1);
                    type.Add(2);
                    type.Add(3);
                    blocks[3].Load(engine, type);
                    pos.Add(new Vector2(250, 340));
                    pos.Add(new Vector2(400, 340));
                    pos.Add(new Vector2(550, 340));
                    pos.Add(new Vector2(700, 340));
                    pos.Add(new Vector2(850, 340));
                    blocks[3].setPositions(pos);
                    break;

                case 8:

                    sManager.setAmbientSound("planetRumble");
                    sManager.playAmbientSound();

                    //load planet
                    blocks.Add(new Planets(2));
                    blocks[0].Load(engine);

                    //load aliens row 1
                    blocks.Add(new Aliens(5, 1, new Vector2(1.5f, 0), 120, true));
                    type.Add(4);
                    type.Add(2);
                    type.Add(4);
                    type.Add(1);
                    type.Add(4);
                    blocks[1].Load(engine, type);
                    pos.Add(new Vector2(250, 300));
                    pos.Add(new Vector2(400, 300));
                    pos.Add(new Vector2(550, 300));
                    pos.Add(new Vector2(700, 300));
                    pos.Add(new Vector2(850, 300));
                    blocks[1].setPositions(pos);

                    //load aliens row 2
                    pos = new List<Vector2>();
                    type = new List<int>();
                    blocks.Add(new Aliens(5, -1, new Vector2(2, 0), 220, true));
                    type.Add(3);
                    type.Add(2);
                    type.Add(3);
                    type.Add(1);
                    type.Add(2);
                    blocks[2].Load(engine, type);
                    pos.Add(new Vector2(250, 370));
                    pos.Add(new Vector2(400, 370));
                    pos.Add(new Vector2(550, 370));
                    pos.Add(new Vector2(700, 370));
                    pos.Add(new Vector2(850, 370));
                    blocks[2].setPositions(pos);
                    break;

                case 9:

                    //load planet
                    blocks.Add(new Planets(1));
                    blocks[0].Load(engine);
                    break;


                case 10:
                    sManager.stopAmbientSound();
                    //load teeth
                    blocks.Add(new Teeth());
                    blocks[0].Load(engine);
                    break;

                case 11:

                    //load plates
                    blocks.Add(new Plates(11));
                    blocks[0].Load(engine, type);

                    //set plates positions
                    pos.Add(new Vector2(250, 200));
                    pos.Add(new Vector2(450, 300));
                    pos.Add(new Vector2(650, 200));
                    pos.Add(new Vector2(850, 300));
                    pos.Add(new Vector2(1050, 200));

                    pos.Add(new Vector2(250, 400));
                    pos.Add(new Vector2(450, 100));
                    pos.Add(new Vector2(450, 500));

                    pos.Add(new Vector2(850, 500));
                    pos.Add(new Vector2(850, 100));
                    pos.Add(new Vector2(1050, 400));
                    blocks[0].setPositions(pos);
                    break;

                case 12:

                    //load pots
                    blocks.Add(new Pots(10));
                    type.Add(1);//red
                    type.Add(2);//blue
                    type.Add(3);//green
                    type.Add(4);//yellow
                    type.Add(1);//red
                    type.Add(1);//red
                    type.Add(2);//blue
                    type.Add(3);//green
                    type.Add(4);//yellow
                    type.Add(1);//red
                    blocks[0].Load(engine, type);

                    //set pots positions
                    pos.Add(new Vector2(400, 100));
                    pos.Add(new Vector2(900, 100));
                    pos.Add(new Vector2(200, 200));
                    pos.Add(new Vector2(600, 200));
                    pos.Add(new Vector2(1050, 200));
                    //
                    pos.Add(new Vector2(400, 500));
                    pos.Add(new Vector2(900, 500));
                    pos.Add(new Vector2(200, 400));
                    pos.Add(new Vector2(780, 400));
                    pos.Add(new Vector2(1050, 400));

                    blocks[0].setPositions(pos);
                    break;

                case 13:

                    //load pots
                    blocks.Add(new Pots(7));
                    type.Add(1);//red
                    type.Add(2);//blue
                    type.Add(3);//green
                    type.Add(1);//red
                    type.Add(2);//blue
                    type.Add(3);//green
                    type.Add(4);//yellow
                    blocks[0].Load(engine, type);

                    //load plates
                    blocks.Add(new Plates(4));
                    blocks[1].Load(engine, type);

                    //set pots positions
                    pos.Add(new Vector2(400, 100));
                    pos.Add(new Vector2(780, 350));
                    pos.Add(new Vector2(900, 100));
                    pos.Add(new Vector2(400, 300));
                    pos.Add(new Vector2(560, 350));
                    pos.Add(new Vector2(900, 300));
                    pos.Add(new Vector2(680, 200));
                    blocks[0].setPositions(pos);

                    //set plates positions
                    pos = new List<Vector2>();
                    pos.Add(new Vector2(250, 300));
                    pos.Add(new Vector2(1100, 300));
                    pos.Add(new Vector2(500, 500));
                    pos.Add(new Vector2(800, 500));
                    blocks[1].setPositions(pos);
                    break;

                case 14:
                    sManager.setAmbientSound("backgroundWind");
                    sManager.playAmbientSound();
                    //load clouds row 1
                    blocks.Add(new Clouds(3, 2, new Vector2(-.6f, 0), .25f, 240));
                    blocks[0].Load(engine);

                    //set big clouds pos row 1
                    pos.Add(new Vector2(530, 235));
                    pos.Add(new Vector2(30, 245));
                    pos.Add(new Vector2(80, 190));

                    //set small clouds pos row 1
                    pos2.Add(new Vector2(930, 235));
                    pos2.Add(new Vector2(330, 200));
                    blocks[0].setPositions(pos, pos2);


                    //load clouds row 2
                    blocks.Add(new Clouds(3, 2, new Vector2(.6f, 0), .26f, 120));
                    blocks[1].Load(engine);

                    //set big clouds pos row 2
                    pos = new List<Vector2>();
                    pos2 = new List<Vector2>();
                    pos.Add(new Vector2(230, 240));
                    pos.Add(new Vector2(70, 215));
                    pos.Add(new Vector2(980, 180));

                    //set small clouds pos row 2
                    pos2.Add(new Vector2(930, 220));
                    pos2.Add(new Vector2(335, 200));
                    blocks[1].setPositions(pos, pos2);


                    //load clouds row 3
                    blocks.Add(new Clouds(1, 2, new Vector2(.4f, 0), .24f, 60));
                    blocks[2].Load(engine);

                    //set big clouds pos row 3
                    pos = new List<Vector2>();
                    pos2 = new List<Vector2>();
                    pos.Add(new Vector2(310, 300));
                    pos.Add(new Vector2(900, 300));

                    //set small clouds pos row 3
                    pos2.Add(new Vector2(950, 300));
                    pos2.Add(new Vector2(300, 310));
                    blocks[2].setPositions(pos, pos2);


                    //load clouds row 4
                    blocks.Add(new Clouds(1, 2, new Vector2(-.8f, 0), .24f, 60));
                    blocks[3].Load(engine);

                    //set big clouds pos row 4
                    pos = new List<Vector2>();
                    pos2 = new List<Vector2>();
                    pos.Add(new Vector2(460, 80));

                    //set small clouds pos row 4
                    pos2.Add(new Vector2(970, 95));
                    pos2.Add(new Vector2(310, 99));
                    blocks[3].setPositions(pos, pos2);

                    //load clouds row 5
                    blocks.Add(new Clouds(1, 2, new Vector2(-.4f, 0), .22f, 50));
                    blocks[4].Load(engine);

                    //set big clouds pos row 5
                    pos = new List<Vector2>();
                    pos2 = new List<Vector2>();
                    pos.Add(new Vector2(560, 40));

                    //set small clouds pos row 5
                    pos2.Add(new Vector2(960, 50));
                    pos2.Add(new Vector2(320, 50));
                    blocks[4].setPositions(pos, pos2);

                    break;

                case 15:


                    //load clouds row 1
                    blocks.Add(new Clouds(3, 2, new Vector2(-.6f, 0), .25f, 240));
                    blocks[0].Load(engine);

                    //set big clouds pos row 1
                    pos.Add(new Vector2(530, 235));
                    pos.Add(new Vector2(30, 245));
                    pos.Add(new Vector2(80, 190));

                    //set small clouds pos row 1
                    pos2.Add(new Vector2(930, 235));
                    pos2.Add(new Vector2(330, 200));
                    blocks[0].setPositions(pos, pos2);


                    //load clouds row 2
                    blocks.Add(new Clouds(3, 2, new Vector2(.6f, 0), .26f, 120));
                    blocks[1].Load(engine);

                    //set big clouds pos row 2
                    pos = new List<Vector2>();
                    pos2 = new List<Vector2>();
                    pos.Add(new Vector2(230, 240));
                    pos.Add(new Vector2(70, 215));
                    pos.Add(new Vector2(980, 180));

                    //set small clouds pos row 2
                    pos2.Add(new Vector2(930, 220));
                    pos2.Add(new Vector2(335, 200));
                    blocks[1].setPositions(pos, pos2);


                    //load clouds row 3
                    blocks.Add(new Clouds(1, 2, new Vector2(.4f, 0), .24f, 60));
                    blocks[2].Load(engine);

                    //set big clouds pos row 3
                    pos = new List<Vector2>();
                    pos2 = new List<Vector2>();
                    pos.Add(new Vector2(310, 300));
                    pos.Add(new Vector2(900, 300));

                    //set small clouds pos row 3
                    pos2.Add(new Vector2(950, 300));
                    pos2.Add(new Vector2(300, 310));
                    blocks[2].setPositions(pos, pos2);


                    //load clouds row 4
                    blocks.Add(new Clouds(1, 2, new Vector2(-.8f, 0), .24f, 60));
                    blocks[3].Load(engine);

                    //set big clouds pos row 4
                    pos = new List<Vector2>();
                    pos2 = new List<Vector2>();
                    pos.Add(new Vector2(460, 80));

                    //set small clouds pos row 4
                    pos2.Add(new Vector2(970, 95));
                    pos2.Add(new Vector2(310, 99));
                    blocks[3].setPositions(pos, pos2);

                    //load clouds row 5
                    blocks.Add(new Clouds(1, 2, new Vector2(-.4f, 0), .22f, 50));
                    blocks[4].Load(engine);

                    //set big clouds pos row 5
                    pos = new List<Vector2>();
                    pos2 = new List<Vector2>();
                    pos.Add(new Vector2(560, 40));

                    //set small clouds pos row 5
                    pos2.Add(new Vector2(960, 50));
                    pos2.Add(new Vector2(320, 50));
                    blocks[4].setPositions(pos, pos2);

                    break;

                case 16:

                    //load clouds row 1
                    blocks.Add(new Clouds(3, 2, new Vector2(-.6f, 0), .25f, 240));
                    blocks[0].Load(engine);

                    //set big clouds pos row 1
                    pos.Add(new Vector2(530, 235));
                    pos.Add(new Vector2(30, 245));
                    pos.Add(new Vector2(80, 190));

                    //set small clouds pos row 1
                    pos2.Add(new Vector2(930, 235));
                    pos2.Add(new Vector2(330, 200));
                    blocks[0].setPositions(pos, pos2);


                    //load clouds row 2
                    blocks.Add(new Clouds(3, 2, new Vector2(.6f, 0), .26f, 120));
                    blocks[1].Load(engine);

                    //set big clouds pos row 2
                    pos = new List<Vector2>();
                    pos2 = new List<Vector2>();
                    pos.Add(new Vector2(230, 240));
                    pos.Add(new Vector2(70, 215));
                    pos.Add(new Vector2(980, 180));

                    //set small clouds pos row 2
                    pos2.Add(new Vector2(930, 220));
                    pos2.Add(new Vector2(335, 200));
                    blocks[1].setPositions(pos, pos2);


                    //load clouds row 3
                    blocks.Add(new Clouds(1, 2, new Vector2(.4f, 0), .24f, 60));
                    blocks[2].Load(engine);

                    //set big clouds pos row 3
                    pos = new List<Vector2>();
                    pos2 = new List<Vector2>();
                    pos.Add(new Vector2(310, 300));
                    pos.Add(new Vector2(900, 300));

                    //set small clouds pos row 3
                    pos2.Add(new Vector2(950, 300));
                    pos2.Add(new Vector2(300, 310));
                    blocks[2].setPositions(pos, pos2);


                    //load clouds row 4
                    blocks.Add(new Clouds(1, 2, new Vector2(-.8f, 0), .24f, 60));
                    blocks[3].Load(engine);

                    //set big clouds pos row 4
                    pos = new List<Vector2>();
                    pos2 = new List<Vector2>();
                    pos.Add(new Vector2(460, 80));

                    //set small clouds pos row 4
                    pos2.Add(new Vector2(970, 95));
                    pos2.Add(new Vector2(310, 99));
                    blocks[3].setPositions(pos, pos2);

                    //load clouds row 5
                    blocks.Add(new Clouds(1, 2, new Vector2(-.4f, 0), .22f, 50));
                    blocks[4].Load(engine);

                    //set big clouds pos row 5
                    pos = new List<Vector2>();
                    pos2 = new List<Vector2>();
                    pos.Add(new Vector2(560, 40));

                    //set small clouds pos row 5
                    pos2.Add(new Vector2(960, 50));
                    pos2.Add(new Vector2(320, 50));
                    blocks[4].setPositions(pos, pos2);

                    break;

                case 17:
                    sManager.stopAmbientSound();
                    sManager.setAmbientSound("planetRumble");
                    sManager.playAmbientSound();
                    //load aliens row 1
                    blocks.Add(new Aliens(5, 1, new Vector2(1.5f, 0), 60, false));
                    type.Add(4);
                    type.Add(2);
                    type.Add(4);
                    type.Add(1);
                    type.Add(4);
                    blocks[0].Load(engine, type);
                    pos.Add(new Vector2(250, 130));
                    pos.Add(new Vector2(400, 130));
                    pos.Add(new Vector2(550, 130));
                    pos.Add(new Vector2(700, 130));
                    pos.Add(new Vector2(850, 130));
                    blocks[0].setPositions(pos);

                    //load aliens row 2
                    pos = new List<Vector2>();
                    type = new List<int>();
                    blocks.Add(new Aliens(5, -1, new Vector2(2, 0), 120, false));
                    type.Add(3);
                    type.Add(2);
                    type.Add(3);
                    type.Add(1);
                    type.Add(2);
                    blocks[1].Load(engine, type);
                    pos.Add(new Vector2(250, 200));
                    pos.Add(new Vector2(400, 200));
                    pos.Add(new Vector2(550, 200));
                    pos.Add(new Vector2(700, 200));
                    pos.Add(new Vector2(850, 200));
                    blocks[1].setPositions(pos);

                    //load aliens row 3
                    pos = new List<Vector2>();
                    type = new List<int>();
                    blocks.Add(new Aliens(5, 1, new Vector2(1.7f, 0), 220, false));
                    type.Add(1);
                    type.Add(1);
                    type.Add(3);
                    type.Add(2);
                    type.Add(1);
                    blocks[2].Load(engine, type);
                    pos.Add(new Vector2(250, 270));
                    pos.Add(new Vector2(400, 270));
                    pos.Add(new Vector2(550, 270));
                    pos.Add(new Vector2(700, 270));
                    pos.Add(new Vector2(850, 270));
                    blocks[2].setPositions(pos);

                    //load aliens row 4
                    pos = new List<Vector2>();
                    type = new List<int>();
                    blocks.Add(new Aliens(5, -1, new Vector2(1.3f, 0), 320, false));
                    type.Add(2);
                    type.Add(3);
                    type.Add(1);
                    type.Add(2);
                    type.Add(3);
                    blocks[3].Load(engine, type);
                    pos.Add(new Vector2(250, 340));
                    pos.Add(new Vector2(400, 340));
                    pos.Add(new Vector2(550, 340));
                    pos.Add(new Vector2(700, 340));
                    pos.Add(new Vector2(850, 340));
                    blocks[3].setPositions(pos);

                    //load aliens row 5
                    pos = new List<Vector2>();
                    type = new List<int>();
                    blocks.Add(new Aliens(5, 1, new Vector2(1, 0), 420, false));
                    type.Add(1);
                    type.Add(1);
                    type.Add(3);
                    type.Add(2);
                    type.Add(1);
                    blocks[4].Load(engine, type);
                    pos.Add(new Vector2(250, 440));
                    pos.Add(new Vector2(400, 440));
                    pos.Add(new Vector2(550, 440));
                    pos.Add(new Vector2(700, 440));
                    pos.Add(new Vector2(850, 440));
                    blocks[4].setPositions(pos);

                    //load aliens row 6
                    pos = new List<Vector2>();
                    type = new List<int>();
                    blocks.Add(new Aliens(5, -1, new Vector2(.7f, 0), 520, false));
                    type.Add(2);
                    type.Add(3);
                    type.Add(1);
                    type.Add(2);
                    type.Add(3);
                    blocks[5].Load(engine, type);
                    pos.Add(new Vector2(250, 510));
                    pos.Add(new Vector2(400, 510));
                    pos.Add(new Vector2(550, 510));
                    pos.Add(new Vector2(700, 510));
                    pos.Add(new Vector2(850, 510));
                    blocks[5].setPositions(pos);
                    break;

                case 18:


                    //load planet
                    blocks.Add(new Planets(2));
                    blocks[0].Load(engine);

                    //load aliens row 1
                    blocks.Add(new Aliens(5, 1, new Vector2(1.5f, 0), 120, true));
                    type.Add(4);
                    type.Add(2);
                    type.Add(4);
                    type.Add(1);
                    type.Add(4);
                    blocks[1].Load(engine, type);
                    pos.Add(new Vector2(250, 300));
                    pos.Add(new Vector2(400, 300));
                    pos.Add(new Vector2(550, 300));
                    pos.Add(new Vector2(700, 300));
                    pos.Add(new Vector2(850, 300));
                    blocks[1].setPositions(pos);

                    //load aliens row 2
                    pos = new List<Vector2>();
                    type = new List<int>();
                    blocks.Add(new Aliens(5, -1, new Vector2(2, 0), 220, true));
                    type.Add(3);
                    type.Add(2);
                    type.Add(3);
                    type.Add(1);
                    type.Add(2);
                    blocks[2].Load(engine, type);
                    pos.Add(new Vector2(250, 370));
                    pos.Add(new Vector2(400, 370));
                    pos.Add(new Vector2(550, 370));
                    pos.Add(new Vector2(700, 370));
                    pos.Add(new Vector2(850, 370));
                    blocks[2].setPositions(pos);

                    //load aliens row 3
                    pos = new List<Vector2>();
                    type = new List<int>();
                    blocks.Add(new Aliens(5, 1, new Vector2(2, 0), 320, true));
                    type.Add(1);
                    type.Add(2);
                    type.Add(3);
                    type.Add(1);
                    type.Add(3);
                    blocks[3].Load(engine, type);
                    pos.Add(new Vector2(250, 440));
                    pos.Add(new Vector2(400, 440));
                    pos.Add(new Vector2(550, 440));
                    pos.Add(new Vector2(700, 440));
                    pos.Add(new Vector2(850, 440));
                    blocks[3].setPositions(pos);
                    break;

                case 19:


                    //load planet
                    blocks.Add(new Planets(1));
                    blocks[0].Load(engine);

                    //load aliens
                    blocks.Add(new Aliens(5, 0, new Vector2(1.5f, 0), 60, true));
                    type.Add(4);
                    type.Add(3);
                    type.Add(1);
                    type.Add(3);
                    type.Add(4);
                    blocks[1].Load(engine, type);
                    pos.Add(new Vector2(425, 255));
                    pos.Add(new Vector2(520, 237));
                    pos.Add(new Vector2(620, 233));
                    pos.Add(new Vector2(720, 237));
                    pos.Add(new Vector2(825, 255));
                    blocks[1].setPositions(pos);

                    //load aliens
                    pos = new List<Vector2>();
                    type = new List<int>();
                    blocks.Add(new Aliens(5, 0, new Vector2(2, 0), 120, true));
                    type.Add(4);
                    type.Add(2);
                    type.Add(3);
                    type.Add(2);
                    type.Add(4);
                    blocks[2].Load(engine, type);
                    pos.Add(new Vector2(100, 50));
                    pos.Add(new Vector2(485, 320));
                    pos.Add(new Vector2(620, 340));
                    pos.Add(new Vector2(770, 320));
                    pos.Add(new Vector2(1180, 50));
                    blocks[2].setPositions(pos);
                    break;


                case 20:
                    sManager.stopAmbientSound();
                    //load teeth
                    blocks.Add(new Teeth());
                    blocks[0].Load(engine);
                    break;
            }
        }

        /*****************************************/

        //update 
        public void Update(GameTime gameT, BallManager bManager,
            StatsManager stat, PlayerManager pM, SoundManager sManager,
            ExtraBallManager xBall, GameObjects gO)
        {
            //check for remaining blocks if not title
            if (lvl >= 1)
            {
                x = false;

                if (lvl == 8 || lvl == 18 || lvl == 19)
                {
                    if (blocks[0].isOn())
                    {
                        x = true;
                    }
                    if (blocks[1].isOn())
                    {

                    }
                    if (blocks[2].isOn())
                    {

                    }
                }
                else
                {
                    for (int a = 0; a < blocks.Count; a++)
                    {
                        if (blocks[a].isOn())
                        {
                            x = true;
                        }
                    }
                }

                if (!x)
                {
                    if (lvl == 20)
                    {
                        gO.setBeatGame(true);
                    }
                    pM.setBeatLevel(true);
                }
            }

            //update blocks
            for (int a = 0; a < blocks.Count; a++)
            {
                //IF lvl 1-6, 7, 10-16, 17, 20
                if (((lvl > 0) && (lvl <= 7)) || ((lvl != 9 && lvl != 8) && (lvl <= 16)) || (lvl == 17) || (lvl == 20))
                {
                    blocks[a].Update(bManager, stat, gameT, pM, sManager, xBall);
                }
                else if (lvl == 9)
                {
                    blocks[a].Update(bManager, stat, gameT, sManager, xBall);
                }
            }
            if (lvl == 8 || lvl == 18)
            {
                blocks[0].Update(bManager, stat, gameT, sManager, xBall);
                blocks[1].Update(bManager, stat, gameT, pM, sManager, xBall);
                blocks[2].Update(bManager, stat, gameT, pM, sManager, xBall);
                if (lvl == 18)
                {
                    blocks[3].Update(bManager, stat, gameT, pM, sManager, xBall);
                }
            }
            else if (lvl == 19)
            {
                blocks[0].Update(bManager, stat, gameT, sManager, xBall);
                blocks[1].Update(bManager, stat, gameT, pM, sManager, xBall);
                blocks[2].Update(bManager, stat, gameT, pM, sManager, xBall);
            }
        }

        /*****************************************/

        //draw
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int a = 0; a < blocks.Count; a++)
            {
                blocks[a].Draw(spriteBatch);
            }
        }
    }
}
