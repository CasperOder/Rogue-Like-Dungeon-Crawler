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
                case 2:

                    timeSinceLastEffect += gameTime.ElapsedGameTime.TotalSeconds;
                    if (timeSinceLastEffect >= 0.1)
                    {
                        timeSinceLastEffect = 0;
                        ambientEffectsOnScreen.Add(windBreeze(playerPos));
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
                case 5:
                    timeSinceLastEffect += gameTime.ElapsedGameTime.TotalSeconds;
                    if (timeSinceLastEffect >= 0.3)
                    {
                        timeSinceLastEffect = 0;
                        ambientEffectsOnScreen.Add(bubbles(playerPos));
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
                case 6:
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
                case 9:
                    timeSinceLastEffect += gameTime.ElapsedGameTime.TotalSeconds;
                    if (timeSinceLastEffect >= 0.5)
                    {
                        timeSinceLastEffect = 0;
                        ambientEffectsOnScreen.Add(strongWind(playerPos));
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
            }


        }

        public static void Draw(SpriteBatch sb)
        {
            foreach (AmbientEffect a in ambientEffectsOnScreen)
            {
                a.Draw(sb);
            }
        }

        static AmbientEffect windBreeze(Vector2 playerPos)
        {
            return new AmbientEffect(SpriteSheetManager.windBreeze, 0.2, new Vector2(-3, 1), RandomLocation(playerPos), 1, 0.75f);
        }

        static AmbientEffect rainDrop(Vector2 playerPos)
        {
            return new AmbientEffect(SpriteSheetManager.raindrops, 0.3, new Vector2(0, 1), RandomLocation(playerPos), 3,1);
        }

        static AmbientEffect bubbles(Vector2 playerPos)
        {
            return new AmbientEffect(SpriteSheetManager.bubbles, 0.2, new Vector2(0, -1), RandomLocation(playerPos), 4, 0.5f);
        }

        static AmbientEffect fireDrop(Vector2 playerPos)
        {
            return new AmbientEffect(SpriteSheetManager.fireDrops, 0.2, new Vector2(-0.3f, 1), RandomLocation(playerPos), 4,1);
        }

        static AmbientEffect strongWind(Vector2 playerPos)
        {
            return new AmbientEffect(SpriteSheetManager.strongWind, 0.3, new Vector2(0, 1), RandomLocation(playerPos-new Vector2(0,Constants.windowHeight)), 10, 0.9f);
        }

        static Vector2 RandomLocation(Vector2 playerPos)
        {
            int vectorX = rnd.Next((int)playerPos.X - Constants.windowWidth * (3 / 2), (int)playerPos.X + Constants.windowWidth * (3 / 2));
            int vectorY = rnd.Next((int)playerPos.Y - Constants.windowHeight * (3 / 2), (int)playerPos.Y + Constants.windowHeight * (3 / 2));

            return new Vector2(vectorX, vectorY);
        }

        public static void AddShatteredVase(Vector2 pos)
        {
            ambientEffectsOnScreen.Add( new AmbientEffect(SpriteSheetManager.shatteredVase, 0.1, new Vector2(0, 0), pos, 10, 0.9f));

        }

    }
}
