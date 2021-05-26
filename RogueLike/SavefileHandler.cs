using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RogueLike
{
    static class SavefileHandler
    {
        static Weapon savedWeapon;
        static int savedCircle, savedCurrency;
        static float savedHealth, savedMaxHealth, savedDamageMultiplier, savedAttackSpeedMultiplier, savedSpeedMultiplier;
        
        /// <summary>
        /// Reads savefile.
        /// </summary>
        /// <param name="filename">File to read from.</param>
        public static void ReadFile(string filename)
        {
            List<string> lines = File.ReadAllLines(filename).ToList();
            savedCircle = Convert.ToInt32(lines[0]);

            savedWeapon = LoadWeaponsAndItems.SavedWeaponFromList(lines[1]);

            savedHealth = (float)Convert.ToDouble(lines[2]);

            savedMaxHealth = (float)Convert.ToDouble(lines[3]);

            savedAttackSpeedMultiplier = (float)Convert.ToDouble(lines[4]);
            savedDamageMultiplier = (float)Convert.ToDouble(lines[5]);
            savedSpeedMultiplier = (float)Convert.ToDouble(lines[6]);
            savedCurrency = Convert.ToInt32(lines[7]);
        }

        /// <summary>
        /// Load the save from file.
        /// </summary>
        public static void LoadFromFile()
        {
            Level.currentCircle = savedCircle;
            Level.currency = savedCurrency;
            Level.player.SetStatsFromSaveFile(savedWeapon, savedHealth, savedMaxHealth, savedAttackSpeedMultiplier, savedDamageMultiplier, savedSpeedMultiplier);
        }

        /// <summary>
        /// Saves stats to file.
        /// </summary>
        /// <param name="weapon">Weapon to save.</param>
        /// <param name="health">Current health to save.</param>
        /// <param name="maxHealth">Max Health to save.</param>
        /// <param name="attackSpeedMultiplier">Attack Speed multiplier to save.</param>
        /// <param name="damageMultiplier">Damage multiplier to save.</param>
        /// <param name="speedMultiplier">Speed Multiplier to save.</param>
        public static void SaveToFile(Weapon weapon, float health, float maxHealth, float attackSpeedMultiplier, float damageMultiplier, float speedMultiplier)
        {
            List<string> newSavedContent = new List<string>();
            newSavedContent.Add(Level.currentCircle.ToString());
            newSavedContent.Add(weapon.itemName);
            newSavedContent.Add(health.ToString());
            newSavedContent.Add(maxHealth.ToString());
            newSavedContent.Add(attackSpeedMultiplier.ToString());
            newSavedContent.Add(damageMultiplier.ToString());
            newSavedContent.Add(speedMultiplier.ToString());
            newSavedContent.Add(Level.currency.ToString());

            File.WriteAllLines("savefile.txt", newSavedContent.ToArray());

        }

        /// <summary>
        /// Deletes savefile.
        /// </summary>
        public static void DeleteSavefile()
        {
            File.Delete("savefile.txt");
        }
    }
}
