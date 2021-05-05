using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    public static class SoundManager
    {
        public static Song shopTheme;

        //public static List<SoundEffect> playerSounds = new List<SoundEffect>();


        public static void LoadAudio(ContentManager Content)
        {
            shopTheme = Content.Load<Song>(@"Pandora's Box");
            //playerSounds.Add(Content.Load<SoundEffect>(@""));
            //playerSounds.Add(Content.Load<SoundEffect>(@""));
            //playerSounds.Add(Content.Load<SoundEffect>(@""));
        }
    }
}
