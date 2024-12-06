/******************************************
 * 06/21/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  TitleScreen.cs 
 * 
 *****************************************/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
namespace Br8kIn_
{
    class TitleScreen
    {
        //instance variables
        StateSystem stateSystem;
        TitleScreenF tSf;

        //flags
        bool isTitle;


        /*****************************************/


        //constructor
        public TitleScreen(StateSystem stSystem)
        {
            stateSystem = stSystem;
            isTitle = false;
            tSf = new TitleScreenF(this, stSystem);
        }


        /*****************************************/


        //load
        public void LoadContent(Engine engine, Rectangle clientBounds)
        {
            tSf.LoadContent(engine, clientBounds);
        }


        /*****************************************/


        //update
        public void Update(GameTime gameTime, Rectangle clientBounds,
            GamePadState g, GameObjects gameObjects)
        {
            //update title screen overlay
            tSf.Update(g, gameTime, gameObjects, clientBounds);

            //check for user selection
            if (tSf.getSelection() > 0)
            {
                //SWITCH for title screen
                switch (tSf.getSelection())
                {
                    //user chose to start new game
                    case 1:
                        stateSystem.uStart();
                        break;

                    //user chose to Quit
                    case 2:
                        stateSystem.getEngine().Exit();
                        break;
                }
            }
        }


        /*****************************************/

        //draw
        public void Draw(SpriteBatch spriteBatch, Rectangle clientBounds)
        {
            //draw title screen overlay
            tSf.Draw(spriteBatch, clientBounds);
        }

        /*****************************************/

        //method to return isTitle flag
        public bool getIsTitle()
        {
            return isTitle;
        }

        //method to set isTitle flag
        public void setIsTitle(bool x)
        {
            isTitle = x;
            if (!x)
            {
                tSf.reset();
            }
        }

        public StateSystem getState()
        {
            return stateSystem;
        }

        public void setFirst(bool x)
        {
            tSf.setFirst(x);
        }
        public bool getTut()
        {
            return tSf.getTut();
        }
        public void setTut(bool x)
        {
            tSf.setTut(x);
        }

    }
}
