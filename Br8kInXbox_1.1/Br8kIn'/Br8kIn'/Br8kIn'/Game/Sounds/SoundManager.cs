/******************************************
 * 03/25/2012
 * 
 * Br8kIn : A Retro Modernization
 *      
 * By : HuskySoft LLC
 *      - Erin Antonson
 * 
 *  SoundManager.cs 0.5
 * 
 *****************************************/
using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Br8kIn_
{
    class SoundManager
    {
        //local variables
        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;
        AudioCategory musicCategory;
        Cue[,] Sound = new Cue[21, 10];
        Cue trackCue;
        Cue ambient;
        Random random;
        // Music volume.
        float musicVolume = 1.0f;
        /*****************************************/

        //constructor
        public SoundManager()
        {
            audioEngine = new AudioEngine(@"Content\Sounds\GameAudio.xgs");
            waveBank = new WaveBank(audioEngine, @"Content\Sounds\Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, @"Content\Sounds\Sound Bank.xsb");
            random = new Random();
            //needs to change in final
            trackCue = soundBank.GetCue("Title2.0");
            //trackCue.Play();

            // Get the category.
            musicCategory = audioEngine.GetCategory("Music");
        }
        /*****************************************/
        public void Update(bool transOn, DisplayPane dP)
        {
            //LoadSound(level);
            if (transOn)
            {
                fadeOut();
            }
            else
            {
                fadeIn();
            }

            if (dP.getSound())
            {
                musicVolume = 1.0f;
            }
            else
            {
                musicVolume = -1.0f;
            }

            audioEngine.Update();
        }
        /*****************************************/
        public void LoadSound(int levelNum)
        {

            switch (levelNum + 1)
            {
                //title screen
                case 1:
                    Sound[levelNum, 0] = soundBank.GetCue("Title2.0");

                    break;
                //Level 1
                case 2:
                    Sound[levelNum, 0] = soundBank.GetCue("Market3.0");

                    break;
                case 3:
                    Sound[levelNum, 0] = soundBank.GetCue("Market3.0");

                    break;
                case 4:
                    Sound[levelNum, 0] = soundBank.GetCue("Market3.0");

                    break;
                case 5:
                    Sound[levelNum, 0] = soundBank.GetCue("Sky1.0");

                    break;
                case 6:
                    Sound[levelNum, 0] = soundBank.GetCue("Sky1.0");

                    break;
                case 7:
                    Sound[levelNum, 0] = soundBank.GetCue("Sky1.0");

                    break;
                case 8:
                    Sound[levelNum, 0] = soundBank.GetCue("space3.0");

                    break;
                case 9:
                    Sound[levelNum, 0] = soundBank.GetCue("space3.0");

                    break;
                case 10:
                    Sound[levelNum, 0] = soundBank.GetCue("space3.0");

                    break;
                case 11:
                    Sound[levelNum, 0] = soundBank.GetCue("bonus1.0");

                    break;
                //Level 1
                case 12:
                    Sound[levelNum, 0] = soundBank.GetCue("Market3.0");

                    break;
                case 13:
                    Sound[levelNum, 0] = soundBank.GetCue("Market3.0");

                    break;
                case 14:
                    Sound[levelNum, 0] = soundBank.GetCue("Market3.0");

                    break;
                case 15:
                    Sound[levelNum, 0] = soundBank.GetCue("Sky1.0");

                    break;
                case 16:
                    Sound[levelNum, 0] = soundBank.GetCue("Sky1.0");

                    break;
                case 17:
                    Sound[levelNum, 0] = soundBank.GetCue("Sky1.0");

                    break;
                case 18:
                    Sound[levelNum, 0] = soundBank.GetCue("space3.0");

                    break;
                case 19:
                    Sound[levelNum, 0] = soundBank.GetCue("space3.0");

                    break;
                case 20:
                    Sound[levelNum, 0] = soundBank.GetCue("space3.0");

                    break;
                case 21:
                    Sound[levelNum, 0] = soundBank.GetCue("bonus1.0");

                    break;
                default:
                    break;
            }
        }
        /*****************************************/
        public void playMusic(int levelNum, int cueNum)
        {
            Sound[levelNum, cueNum].Play();
        }
        /*****************************************/
        public void stopMusic(int levelNum, int skip, int cueNum)
        {
            Sound[levelNum - skip, cueNum].Dispose();
        }
        /*****************************************/
        public void playSound(string cueName)
        {
            soundBank.PlayCue(cueName);
        }
        /*****************************************/
        public void setAmbientSound(string cueName)
        {
            ambient = soundBank.GetCue(cueName);
        }
        /*****************************************/
        public string getAmbientSound()
        {
            return ambient.ToString();
        }
        /*****************************************/
        public void playAmbientSound()
        {
            ambient.Play();
        }
        /*****************************************/
        public void pauseAmbientSound()
        {
            ambient.Pause();
        }
        /*****************************************/
        public void stopAmbientSound()
        {
            ambient.Stop(0);
        }
        /*****************************************/
        public void clearAmbientSound()
        {
            ambient.Dispose();
        }
        /*****************************************/
        public void fadeOut()
        {
            musicVolume = MathHelper.Clamp(musicVolume - 0.005f, 0.1f, 1.0f);
            // Set the category volume.
            musicCategory.SetVolume(musicVolume);
        }
        /*****************************************/
        public void fadeIn()
        {
            musicVolume = MathHelper.Clamp(musicVolume + 0.005f, 0.0f, 1.0f);
            // Set the category volume.
            musicCategory.SetVolume(musicVolume);
        }
        /*****************************************/
        public void volumeDown()
        {
            //trackCue.Pause();//needs to change in final 
            musicVolume = 0.0f;
            // Set the category volume.
            musicCategory.SetVolume(musicVolume);
        }
        /*****************************************/
        public void volumeUp()
        {

            musicVolume = 1.0f;
            // Set the category volume.
            musicCategory.SetVolume(musicVolume);
        }
        /*****************************************/
        public void clearAllSound()
        {
            soundBank.Dispose();
        }
        /*****************************************/
        public void Unload()
        {
            audioEngine.Dispose();
            waveBank.Dispose();
            soundBank.Dispose();
            trackCue.Dispose();
            //ambient.Dispose();

            /*for (int a = 0; a < 21; a++)
            {
                for (int b = 0; b < 10; b++)
                {
                     //Sound[a, b].Dispose();
                }
            }*/
        }
        /*****************************************/
        public void playRandom()
        {
            switch (random.Next() % 4)
            {
                case 0:
                    playSound("Ouch1");
                    break;
                case 1:
                    playSound("oooh1");
                    break;
                case 2:
                    playSound("Oha1");
                    break;
                case 3:
                    playSound("aaaagh1");
                    break;
            }

        }
        /*****************************************/
    }
}

