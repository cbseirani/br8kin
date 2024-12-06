/******************************************
 * 03/20/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  Ball.cs 
 * 
 *****************************************/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Br8kIn_
{
    class Ball : Sprite
    {
        //instance variables
        Texture2D ball;
        Vector2 origin;
        Vector2 direction;
        float angle;
        float rotSpe;
        short rotDir, c1;
        short mult;
        bool type;

        /*****************************************/

        //constructor
        public Ball(Vector2 pos, Vector2 spe, Point fSize, bool x)
            : base(pos, spe, fSize)
        {
            mult = 0;
            type = x;
            rotSpe = .3f;
            rotDir = 1;
            direction = new Vector2(1, -1);
            speed = new Vector2(6.5f, 6.5f);
            position = new Vector2(500, 500);
            c1 = 3;
        }

        /*****************************************/

        //load
        public void Load(Engine engine, int lvl)
        {
            if (type)
            {
                ball = engine.Content.Load<Texture2D>(@"Ball/ball1");
            }
            else
            {
                ball = engine.Content.Load<Texture2D>(@"Ball/ball");
            }
            origin.X = ball.Width / 2;
            origin.Y = ball.Height / 2;
            position = new Vector2(1280 / 2, (720 / 2) + 40);
            speed = new Vector2(0, -5);
            rotSpe = 0;
            rotDir *= -1;
            angle = 0;
            direction = new Vector2(1, -1);
            frameSize = new Point(20, 20);
            c1 = 3;
        }

        /*****************************************/

        //update
        public void Update(GameTime gameTime, GamePadState g, bool extra)
        {
            //move ball in direction
            position += (direction * speed);

            //update ball animation
            ballAni(gameTime);
        }

        /*****************************************/

        //draw
        public void Draw(SpriteBatch spriteBatch)
        {
            if (type)
            {
                spriteBatch.Draw(ball, position, null,
                    Color.White, angle, origin, 1f, SpriteEffects.None, .15f);
            }
            else
            {
                spriteBatch.Draw(ball, position, null,
                    Color.White, angle, origin, 1f, SpriteEffects.None, .98f);
            }
        }

        /*****************************************/

        //method that returns ball's collision rectangle
        public Rectangle getCollRect(bool x)
        {
            if (!x || !type)
            {
                return new Rectangle();
            }
            else
            {
                return new Rectangle((int)position.X - 10, (int)position.Y - 10,
                    16, 16);
            }
        }
        public Rectangle getCollRect1(bool x)
        {
            if (!x || !type)
            {
                return new Rectangle();
            }
            else
            {
                return new Rectangle((int)position.X - 10, (int)position.Y - 10,
                        5, 5);
            }
        }
        public Rectangle getCollRect2(bool x)
        {
            if (!x || !type)
            {
                return new Rectangle();
            }
            else
            {
                return new Rectangle((int)position.X + 5, (int)position.Y - 10,
                        5, 5);
            }
        }
        public Rectangle getCollRect3(bool x)
        {
            if (!x || !type)
            {
                return new Rectangle();
            }
            else
            {
                return new Rectangle((int)position.X - 10, (int)position.Y + 5,
                        5, 5);
            }
        }
        public Rectangle getCollRect4(bool x)
        {
            if (!x || !type)
            {
                return new Rectangle();
            }
            else
            {
                return new Rectangle((int)position.X + 5, (int)position.Y + 5,
                        5, 5);
            }
        }
        public Rectangle getCollRect5(bool x)
        {
            if (!x || !type)
            {
                return new Rectangle();
            }
            else
            {
                return new Rectangle((int)position.X - 20, (int)position.Y - 20,
                        20, 20);
            }
        }

        /*****************************************/

        //method to set ball's direction
        public void setDir(Vector2 dir)
        {
            direction = dir;
        }

        //method to get ball's direction
        public Vector2 getDir()
        {
            return direction;
        }

        public void toggRotDir()
        {
            rotDir *= -1;
        }
        public void setRotSpe()
        {
            rotSpe = .3f;
        }

        //method to set ball's speed
        public void setSpeed(Vector2 spe)
        {
            speed = spe;
        }

        /*****************************************/

        //method to process ball animation
        private void ballAni(GameTime gameTime)
        {
            angle += (float)gameTime.ElapsedGameTime.TotalSeconds +
                rotSpe * rotDir;
            angle = (angle % (MathHelper.Pi * 2));
        }

        /*****************************************/

        public void bounce(Vector2 b)
        {
            direction *= b;
        }
    }
}
