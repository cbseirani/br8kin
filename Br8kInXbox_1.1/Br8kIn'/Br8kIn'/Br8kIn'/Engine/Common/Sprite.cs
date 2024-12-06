/******************************************
 * 06/21/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  Sprite.cs 
 * 
 *****************************************/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Br8kIn_
{
    abstract class Sprite
    {
        //instance variables
        protected Vector2 position;
        protected Vector2 speed;
        protected Point frameSize;

        /*****************************************/

        //constructor
        public Sprite(Vector2 pos, Vector2 spe, Point fSize)
        {
            position = pos;
            speed = spe;
            frameSize = fSize;
        }

        /*****************************************/

        //returns sprite's position
        public Vector2 getPos()
        {
            return position;
        }

        /*****************************************/

        //method to set sprite's position
        public void setPos(Vector2 pos)
        {
            position = pos;
        }

        /*****************************************/

        //set Sprite's speed
        public void setSpeed(Vector2 s)
        {
            speed = s;
        }

        /*****************************************/

        public void setPosY(float y)
        {
            position.Y = y;
        }

        /*****************************************/

        public void setFrame(Point f)
        {
            frameSize = f;
        }
        public Point getFrame()
        {
            return frameSize;
        }
    }
}
