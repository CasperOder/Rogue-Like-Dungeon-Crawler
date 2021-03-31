using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RogueLike
{
    public static class Level
    {
        static Player player;
        static Vector2 playerStartPos;
        
        public static void Load_Level()
        {
            playerStartPos = new Vector2(100, 100);
            player = new Player(Spriteclass.ballTex, playerStartPos);
        }


        public static void Update()
        {
            player.Movement();
        }

        public static void Draw(SpriteBatch sb)
        {
            player.Draw(sb);
        }

    }
}
