/******************************************
 * 03/20/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  BackgroundManager.cs 
 * 
 *****************************************/
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Br8kIn_
{
    class BackgroundManager
    {
        //instance variables
        Texture2D background;
        Texture2D wall;
        int level;
        Random randomNum;
        bool x;

        /*****************************************/

        //constructor
        public BackgroundManager()
        {
            randomNum = new Random();
            x = false;
        }

        /*****************************************/

        //load
        public void LoadBackground(bool isTitle, Engine engine, int bac, int lvl)
        {
            //IF user is at title screen
            level = lvl;
            x = isTitle;
            if (x)
            {
                randomNum = new Random();
                chooseBackground((getRandomNum(376) % 2) + 1, engine);
            }
            //ELSE user is in game
            else
            {
                chooseBackground(bac, engine);
            }
        }

        /*****************************************/

        //update 
        public void Update(Rectangle clientBounds)
        {
            //check for wall/ball collisions
        }

        /*****************************************/

        //draw
        public void Draw(SpriteBatch spriteBatch, Rectangle clientBounds)
        {
            spriteBatch.Draw(background, new Vector2(0, 0), null, Color.White,
                0, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            if (level == 10 || level == 20)
            {
                spriteBatch.Draw(wall, new Vector2(0, 0), null, Color.White, 
                    0, Vector2.Zero, 1f, SpriteEffects.None, .29f);
            }
        }

        /*****************************************/

        //method to return random number
        private int getRandomNum(int max)
        {
            return randomNum.Next(1, max);
        }

        /*****************************************/

        //method to choose background texture for title
        private void chooseBackground(int x, Engine engine)
        {
            switch (x)
            {
                case 1:
                    background = engine.Content.Load<Texture2D>(@"Backgrounds\background1");
                    break;
                case 2:
                    background = engine.Content.Load<Texture2D>(@"Backgrounds\background1");
                    break;
                case 3:
                    background = engine.Content.Load<Texture2D>(@"Backgrounds\background1");
                    break;
                case 4:
                    background = engine.Content.Load<Texture2D>(@"Backgrounds\background4");
                    wall = engine.Content.Load<Texture2D>(@"Backgrounds\wall4");
                    break;
            }
        }

    }
}
