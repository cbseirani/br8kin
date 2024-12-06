/******************************************
 * 06/21/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  Timer.cs 
 * 
 *****************************************/

namespace Br8kIn_
{
    class Timer
    {
        //instance variables
        private int seconds;
        private int incrementer;
        private double milliseconds;
        private float interval;
        private bool enabled;

        /********************************/

        //constructor
        public Timer(float inter, int sec, int incre)
        {
            enabled = false;
            seconds = sec;
            milliseconds = 0;
            interval = inter;
            incrementer = incre;
        }

        //method to update timer
        public void Update(double gameTime)
        {
            milliseconds += gameTime;
            if (milliseconds >= interval)
            {
                seconds += incrementer;
                milliseconds = 0;
            }
        }

        //method to enable/disable timer
        public void setEnable(bool set)
        {
            enabled = set;
        }

        //method to return if timer is enabled
        public bool isEnabled()
        {
            return enabled;
        }

        //method to return number of seconds
        public int returnSeconds()
        {
            return seconds;
        }
    }
}
