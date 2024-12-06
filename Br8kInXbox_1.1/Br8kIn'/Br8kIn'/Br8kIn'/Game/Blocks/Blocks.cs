/******************************************
 * 03/31/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  Blocks.cs 
 * 
 *****************************************/
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Br8kIn_
{
    interface Blocks
    {
        void Load(Engine engine, List<int> y);
        void Load(Engine engine);
        void Update(BallManager bManager, StatsManager stat, GameTime gT, 
            SoundManager sManager, ExtraBallManager xBall);
        void Update(BallManager bManager, StatsManager stat, GameTime gT, 
            PlayerManager pM, SoundManager sManager, ExtraBallManager xBall);
        void Draw(SpriteBatch spriteBatch);
        void setPositions(List<Vector2> x);
        void setPositions(List<Vector2> x, List<Vector2> y);
        bool isOn();
    }
}
