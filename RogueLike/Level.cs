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
            player = new Player(SpriteSheetManager.player, playerStartPos, 0.1d);
        }


        public static void Update(GameTime gameTime)
        {
            player.Movement(gameTime);
        }

        public static void Draw(SpriteBatch sb)
        {
            player.Draw(sb);
        }

    }
}
