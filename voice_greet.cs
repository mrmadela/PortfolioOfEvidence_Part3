using System;
using System.Media;

namespace PortfolioOfEvidence_Part3
{// start of namespace
    public class voice_greet
    {// start of voice_greet class
        public voice_greet()
        {// start of the constructor

            // get the path of thegreet.wav autometicaly
            string full_path = AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\", @"\greet.wav");

            // creating an instance for the soundplayer class with an object name greet
            SoundPlayer greet = new SoundPlayer(full_path);

            // play the sound
            greet.Play();

        }// end of constructor

    }// end of voice_greet class

}// end of namespace