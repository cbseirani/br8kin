using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Br8kIn_
{
    class Controller
    {
        //instance variables
        Engine engine;

        /*****************************************/

        public Controller(Engine eng)
        {
            engine = eng;
        }

        /*****************************************/

        public void getController(StateSystem state)
        {
            if (!state.getIsIntro())
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                {
                    engine.setControl(1);
                    engine.setStorage(true);
                }
                else if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed)
                {
                    engine.setControl(1);
                    engine.setStorage(false);
                }
                else if (GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed)
                {
                    engine.setControl(2);
                    engine.setStorage(true);
                }
                else if (GamePad.GetState(PlayerIndex.Two).Buttons.B == ButtonState.Pressed)
                {
                    engine.setControl(2);
                    engine.setStorage(false);
                }
                else if (GamePad.GetState(PlayerIndex.Three).Buttons.A == ButtonState.Pressed)
                {
                    engine.setControl(3);
                    engine.setStorage(true);
                }
                else if (GamePad.GetState(PlayerIndex.Three).Buttons.B == ButtonState.Pressed)
                {
                    engine.setControl(3);
                    engine.setStorage(false);
                }
                else if (GamePad.GetState(PlayerIndex.Four).Buttons.A == ButtonState.Pressed)
                {
                    engine.setControl(4);
                    engine.setStorage(true);
                }
                else if (GamePad.GetState(PlayerIndex.Four).Buttons.B == ButtonState.Pressed)
                {
                    engine.setControl(4);
                    engine.setStorage(false);
                }
            }
            else
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
                {
                    state.endIntro();
                }
                else if (GamePad.GetState(PlayerIndex.Two).Buttons.Start == ButtonState.Pressed)
                {
                    state.endIntro();
                }
                else if (GamePad.GetState(PlayerIndex.Three).Buttons.Start == ButtonState.Pressed)
                {
                    state.endIntro();
                }
                else if (GamePad.GetState(PlayerIndex.Four).Buttons.Start == ButtonState.Pressed)
                {
                    state.endIntro();
                }
            }
        }
    }
}
