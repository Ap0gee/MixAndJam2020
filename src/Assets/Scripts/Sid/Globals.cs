using System.IO;

namespace GameJam
{
    public class Globals
    {
        public static int resolutionX = 0;
        public static int resolutionY = 0;

        //this line must remain at bottom of class in order to auto-update based on build scenes.
        public static string[] gameScenes = new string[] {  };
    }
}