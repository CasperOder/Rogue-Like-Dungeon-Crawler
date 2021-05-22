using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RogueLike
{
    static class SavefileReader
    {
        static Weapon savedWeapon;
        static int savedCircle, savedCurrency;
        static float savedHealth, savedMaxHealth, savedDamageMultiplier, savedAttackSpeedMultiplier, savedSpeedMultiplier;
        

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


        public static void LoadFromFile()
        {
            Level.currentCircle = savedCircle;
            Level.currency = savedCurrency;
            Level.player.SetStatsFromSaveFile(savedWeapon, savedHealth, savedMaxHealth, savedAttackSpeedMultiplier, savedDamageMultiplier, savedSpeedMultiplier);
        }

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

    }
}
