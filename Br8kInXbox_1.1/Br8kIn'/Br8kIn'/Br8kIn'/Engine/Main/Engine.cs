/******************************************
 * 03/20/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  Engine.cs 
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
    class Engine : Microsoft.Xna.Framework.Game
    {
        //instance variables
        GraphicsDeviceManager graphics;
        GamePadState gps;
        StateSystem state;
        SpriteBatch spriteBatch;
        Controller controllerSelect;
        short x;

        /*****************************************/

        //constructor
        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.SynchronizeWithVerticalRetrace = true;
            IsFixedTimeStep = false;
            controllerSelect = new Controller(this);
            x = 0;
            this.Components.Add(new GamerServicesComponent(this));
        }

        /*****************************************/

        protected override void Initialize()
        {
            //initialize manager
            state = new StateSystem(this);

            base.Initialize();
        }

        /*****************************************/

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //load state system
            state.LoadContent();
        }

        /*****************************************/

        protected override void Update(GameTime gameTime)
        {
            //get gamepad state for user input
            if (!state.getControl())
            {
                controllerSelect.getController(state);
            }
            else
            {
                switch (x)
                {
                    case 1:
                        gps = GamePad.GetState(PlayerIndex.One);
                        break;

                    case 2:
                        gps = GamePad.GetState(PlayerIndex.Two);
                        break;

                    case 3:
                        gps = GamePad.GetState(PlayerIndex.Three);
                        break;

                    case 4:
                        gps = GamePad.GetState(PlayerIndex.Four);
                        break;
                }
            }

            //update state
            state.Update(gameTime, Window.ClientBounds, gps, this);
            
            base.Update(gameTime);            
        }

        /*****************************************/

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //begin spriteBatch draw
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            //draw state information
            state.Draw(gameTime, spriteBatch, Window.ClientBounds, GamePad.GetState(PlayerIndex.One));

            //end spriteBatch draw
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /*****************************************/

        //method to return game window size
        public Rectangle getWindowSize()
        {
            return Window.ClientBounds;
        }

        /*****************************************/

        //method to toggle full screen
        public void toggleFull()
        {
            graphics.ToggleFullScreen();
        }

        /*****************************************/

        public void setControl(short g)
        {
            state.setControl(true);
            x = g;
        }
        public void setStorage(bool f)
        {
            if (f)
            {
                state.yesStorage();
            }
            else
            {
                state.noStorage();
                state.setCont(true);
            }
        }
        public short getPlayer()
        {
            return x;
        }
    }
}
