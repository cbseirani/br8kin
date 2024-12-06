/******************************************
 * 06/21/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  IntroScreen.cs 
 * 
 *****************************************/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Br8kIn_
{
    class IntroScreen
    {
        //instance variables
        StateSystem stateSystem;
        Video video;
        VideoPlayer vidPlayer;
        Texture2D videoTexture;

        //flags
        bool isIntro;
        short c3;

        /*****************************************/

        //constructor
        public IntroScreen(StateSystem stSystem)
        {
            stateSystem = stSystem;
            isIntro = true;

            c3 = 2775;
        }

        /*****************************************/

        //load
        public void LoadContent(Engine engine, Rectangle clientBounds)
        {
            video = engine.Content.Load<Video>(@"Video\intro8");
            vidPlayer = new VideoPlayer();
        }

        /*****************************************/

        //update
        public void Update(GameTime gameTime, Rectangle clientBounds, GamePadState g)
        {
            if (c3 > 0)
            {
                vidPlayer.IsLooped = false;
                vidPlayer.Play(video);
                c3--;
            }
            else
            {
                vidPlayer.Stop();
                stateSystem.endIntro();
            }
        }

        /*****************************************/


        //draw
        public void Draw(SpriteBatch spriteBatch, Rectangle clientBounds)
        {
            videoTexture = vidPlayer.GetTexture();

            Rectangle screen = new Rectangle(0, 0, 1280, 720);

            spriteBatch.Draw(videoTexture, screen, Color.White);
        }

        /*****************************************/

        //method to retrieve isIntro flag
        public bool getIsIntro()
        {
            return isIntro;
        }

        //method to set isIntro flag
        public void setIsIntro(bool x)
        {
            isIntro = x;
        }

        public void stopVid()
        {
            vidPlayer.Stop();//Dispose();
        }
    }
}
