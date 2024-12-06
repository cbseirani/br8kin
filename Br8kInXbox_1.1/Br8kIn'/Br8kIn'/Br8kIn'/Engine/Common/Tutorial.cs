/******************************************
 * 10/09/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  Tutorial.cs 
 * 
 *****************************************/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Br8kIn_
{
    class Tutorial
    {
        //instance variables
        StateSystem stateSystem;
        Video video;
        VideoPlayer vidPlayer;
        Texture2D videoTexture;
        Vector2 vec;
        Texture2D button;

        //flags
        bool isTut;
        short c3;

        /*****************************************/
        
        //constructor
        public Tutorial(StateSystem stSystem)
        {
            stateSystem = stSystem;
            isTut = false;

            c3 = 2775;
        }

        /*****************************************/

        //load
        public void LoadContent(Engine engine, Rectangle clientBounds)
        {
            video = engine.Content.Load<Video>(@"Video\tut6");
            vidPlayer = new VideoPlayer();
            button = engine.Content.Load<Texture2D>(@"Menus\Title\exitTutBut");
        }

        /*****************************************/

        //update
        public void Update(GameTime gameTime, Rectangle clientBounds, GamePadState g)
        {
            /*if (c3 > 0)
            {
                vidPlayer.IsLooped = false;
                vidPlayer.Play(video);
                c3--;
            }
            else
            {
                vidPlayer.Stop();
                isTut = false;
            }*/

            vidPlayer.IsLooped = false;
            vidPlayer.Play(video);
            

            /*if (g.IsButtonDown(Buttons.Start))
            {
                vidPlayer.Stop();
            }*/
        }

        /*****************************************/

        //draw
        public void Draw(SpriteBatch spriteBatch, Rectangle clientBounds)
        {
            videoTexture = vidPlayer.GetTexture();

            Rectangle screen = new Rectangle(0, 0, 1280, 720);
            Vector2 vec = new Vector2(0, 0);

            spriteBatch.Draw(videoTexture, screen, null, Color.White, 0.0f,vec, SpriteEffects.None, 0.7f);

            spriteBatch.Draw(button, new Vector2(920, 40), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, .8f);
        }

        /*****************************************/

        //method to retrieve isIntro flag
        public bool getIsTut()
        {
            return isTut;
        }

        //method to set isIntro flag
        public void setIsTut(bool x)
        {
            isTut = x;
            if (!x)
            {
                stopVid();
            }
        }

        public void stopVid()
        {
            vidPlayer.Stop();
        }

    }
}
