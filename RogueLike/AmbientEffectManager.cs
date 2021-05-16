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
    static class AmbientEffectManager
    {
        static Random rnd = new Random();
        static double timeSinceLastEffect;

        static List<AmbientEffect> ambientEffectsOnScreen = new List<AmbientEffect>();

        public static void NewCircle()
        {
            timeSinceLastEffect = 0;
            ambientEffectsOnScreen.Clear();
        }

        public static void UpdateAmbientEffects(GameTime gameTime, int currentCircle, Vector2 playerPos)
        {
            switch (currentCircle)
            {
                case 1:



                    break;
                case 2:

                    timeSinceLastEffect += gameTime.ElapsedGameTime.TotalSeconds;
                    if (timeSinceLastEffect >= 0.2)
                    {
                        timeSinceLastEffect = 0;
                        ambientEffectsOnScreen.Add(windBreezze(playerPos));
                    }

                    for (int a = 0; a < ambientEffectsOnScreen.Count; a++)
                    {
                        ambientEffectsOnScreen[a].Update(gameTime);
                        if (ambientEffectsOnScreen[a].destroy)
                        {
                            ambientEffectsOnScreen.RemoveAt(a);
                            a--;
                        }
                    }


                    break;

                case 3:

                    ambientEffectsOnScreen.Add(rainDrop(playerPos));

                    for (int a = 0; a < ambientEffectsOnScreen.Count; a++)
                    {
                        ambientEffectsOnScreen[a].Update(gameTime);
                        if (ambientEffectsOnScreen[a].destroy)
                        {
                            ambientEffectsOnScreen.RemoveAt(a);
                            a--;
                        }
                    }

                    break;

                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    ambientEffectsOnScreen.Add(fireDrop(playerPos));

                    for (int a = 0; a < ambientEffectsOnScreen.Count; a++)
                    {
                        ambientEffectsOnScreen[a].Update(gameTime);
                        if (ambientEffectsOnScreen[a].destroy)
                        {
                            ambientEffectsOnScreen.RemoveAt(a);
                            a--;
                        }
                    }
                    break;
                case 8:
                    break;
                case 9:
                    break;
            }


        }

        public static void Draw(SpriteBatch sb)
        {
            foreach (AmbientEffect a in ambientEffectsOnScreen)
            {
                a.Draw(sb);
            }
        }


        static AmbientEffect windBreezze(Vector2 playerPos)
        {
            return new AmbientEffect(SpriteSheetManager.windBreeze, 0.2, new Vector2(-3, 1), RandomLocation(playerPos), 1);
        }

        static AmbientEffect rainDrop(Vector2 playerPos)
        {
            return new AmbientEffect(SpriteSheetManager.raindrops, 0.3, new Vector2(0, 1), RandomLocation(playerPos), 3);
        }

        static AmbientEffect fireDrop(Vector2 playerPos)
        {
            return new AmbientEffect(SpriteSheetManager.fireDrops, 0.2, new Vector2(-0.3f, 1), RandomLocation(playerPos), 4);
        }

        static Vector2 RandomLocation(Vector2 playerPos)
        {
            int vectorX = rnd.Next((int)playerPos.X - Constants.windowWidth * (3 / 2), (int)playerPos.X + Constants.windowWidth * (3 / 2));
            int vectorY = rnd.Next((int)playerPos.Y - Constants.windowHeight * (3 / 2), (int)playerPos.Y + Constants.windowHeight * (3 / 2));

            return new Vector2(vectorX, vectorY);
        }

    }
}
