/******************************************
 * 07/06/2012
 * 
 * Br8kIn
 *      
 * By : HuskySoft LLC
 *      - Lead Software Developer : Christopher G. Bseirani
 * 
 *  HighScore.cs 
 * 
 *****************************************/
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Content;

namespace Br8kIn_
{
    class HighScore
    {
        //instance variables
        StateSystem stateSystem;
        int[] points;
        string[] names;

        HighScoreSave realHighScore;
        IAsyncResult result;
        public struct HighScoreSave
        {
            public int[] pointsSave;
            public string[] namesSave;
        }

        /*****************************************/

        //constructor
        public HighScore(StateSystem stSys)
        {
            stateSystem = stSys;
            points = new int[10];
            names = new string[10];
        }

        /*****************************************/

        public void setPoints(int[] p)
        {
            points = p;
        }
        public void setNames(string[] n)
        {
            names = n;
        }
        public int[] getPoints()
        {
            return points;
        }
        public string[] getNames()
        {
            return names;
        }

        /*****************************************/

        public void saveGame(StorageDevice device)
        {
            string filename = "highScore.sav";

            try
            {
                //create data to save
                realHighScore = new HighScoreSave();
                realHighScore.pointsSave = points;
                realHighScore.namesSave = names;

                //open storage container
                result = device.BeginOpenContainer("Br8kIn", null, null);

                // Wait for the WaitHandle to become signaled.
                result.AsyncWaitHandle.WaitOne();

                StorageContainer container = device.EndOpenContainer(result);

                // Close the wait handle.
                result.AsyncWaitHandle.Close();

                // Check to see whether the save exists.
                if (container.FileExists(filename))
                {
                    // Delete it so that we can create one fresh.
                    container.DeleteFile(filename);
                }

                // Create the file.
                Stream stream = container.CreateFile(filename);

                // Convert the object to XML data and put it in the stream.
                XmlSerializer serializer = new XmlSerializer(typeof(HighScoreSave));
                serializer.Serialize(stream, realHighScore);

                // Close the file.
                stream.Close();
                container.Dispose();
            }
            catch (InvalidOperationException iOe)
            {
            }
            catch (ArgumentNullException aNe)
            {
            }
            catch(StorageDeviceNotConnectedException sDe)
            {
            }
            catch (GamerPrivilegeException gPe)
            {
            }
        }
        public void loadGame(StorageDevice device)
        {
            string filename = "highScore.sav";

            try
            {
                // Open a storage container.
                result = device.BeginOpenContainer("Br8kIn", null, null);

                // Wait for the WaitHandle to become signaled.
                result.AsyncWaitHandle.WaitOne();
                StorageContainer container = device.EndOpenContainer(result);

                // Close the wait handle.
                result.AsyncWaitHandle.Close();

                // Check to see whether the save exists.
                if (!container.FileExists(filename))
                {
                    // If not, dispose of the container and return.
                    container.Dispose();
                    loadDefault();
                    return;
                }

                // Open the file.
                Stream stream = container.OpenFile(filename, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(HighScoreSave));
                realHighScore = (HighScoreSave)serializer.Deserialize(stream);

                // Close the file.
                stream.Close();
                container.Dispose();

                //store loaded data to display
                points = realHighScore.pointsSave;
                names = realHighScore.namesSave;
                stateSystem.setSignedIn(true);
            }
            catch (InvalidOperationException iOe)
            {
                loadDefault();
            }
            catch (ArgumentNullException aNe)
            {
                loadDefault();
            }
            catch (StorageDeviceNotConnectedException sDe)
            {
                loadDefault();
            }
            catch(GamerPrivilegeException gPe)
            {
            }
        }
        public void loadDefault()
        {
            //load up default names and points
            points[0] = 1400;
            names[0] = "ERN";
            points[1] = 1300;
            names[1] = "CRS";
            points[2] = 1200;
            names[2] = "STV";
            points[3] = 1100;
            names[3] = "CLN";
            points[4] = 1000;
            names[4] = "MSA";
            points[5] = 900;
            names[5] = "JES";
            points[6] = 800;
            names[6] = "BOB";
            points[7] = 700;
            names[7] = "JNY";
            points[8] = 600;
            names[8] = "ARN";
            points[9] = 500;
            names[9] = "CRF";
        }
    }
}
