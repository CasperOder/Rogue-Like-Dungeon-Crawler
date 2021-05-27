using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RogueLike
{
    static class LoadWeaponsAndItems
    {
        public static MeleeWeapon testMelee, sweepMelee, knifeMelee, spear, smashRings, boxingGlove;

        public static RangeWeapon bow, throwing, fireRod, iceRod;
        public static Projectile arrow, shuriken, fireBall, iceBall;

        public static List<Weapon> AllWeaponList = new List<Weapon>();

        /// <summary>
        /// Loads all the weapons and items.
        /// </summary>
        public static void LoadAllWeaponsAndItems()
        {
            testMelee = new MeleeWeapon(110, 40, 20, 2, SpriteSheetManager.swordSwing, SpriteSheetManager.arrowItem, 0.9f, 2, "testweapon", 1);
            sweepMelee = new MeleeWeapon(60, 180, 75, 1, SpriteSheetManager.sweep, SpriteSheetManager.sweepItem, 0.69f, 2, "battleaxe", 1);
            knifeMelee = new MeleeWeapon(30, 25, 5, 10, SpriteSheetManager.knife, SpriteSheetManager.knifeItem, 1.5f, 7, "knife", 1);
            AllWeaponList.Add(testMelee);
            AllWeaponList.Add(sweepMelee);
            AllWeaponList.Add(knifeMelee);

            spear = new MeleeWeapon(200, 16, 30, 1.5f, SpriteSheetManager.spear, SpriteSheetManager.spearItem, 0.75f, 8, "spear", 1);
            AllWeaponList.Add(spear);

            smashRings = new MeleeWeapon(320, 320, 150, 0.8f, SpriteSheetManager.smash, SpriteSheetManager.smashItem, 0.01f, 9, "Smash Rings", 1);
            AllWeaponList.Add(smashRings);

            boxingGlove = new MeleeWeapon(100, 40, 14, 5, SpriteSheetManager.punch, SpriteSheetManager.punchItem, 0.6f, 8, "Speedy Punch Glove", 1);
            AllWeaponList.Add(boxingGlove);

            arrow = new Projectile(32, 8, SpriteSheetManager.arrowBow, 1, 10);
            bow = new RangeWeapon(40, 80, 20, 1000, 2, SpriteSheetManager.bow, SpriteSheetManager.bowItem, 0.3f, 3, "Bow", arrow, 1);
            AllWeaponList.Add(bow);

            shuriken = new Projectile(25, 25, SpriteSheetManager.shuriken, 1, 20);
            throwing = new RangeWeapon(60, 40, 15, 500, 5, SpriteSheetManager.throwing, SpriteSheetManager.throwItem, 0.90f, 10, "Shurikens", shuriken, 1);
            AllWeaponList.Add(throwing);

            fireBall = new Projectile(42, 34, SpriteSheetManager.fireBall, 0.2, 12);
            fireRod = new RangeWeapon(146, 42, 80, 600, 1, SpriteSheetManager.fireRod, SpriteSheetManager.fireRodItem, 0.85f, 2, "Rod of Bursting Flames and Generic Names", fireBall, 1);
            AllWeaponList.Add(fireRod);

            iceBall = new Projectile(34, 34, SpriteSheetManager.iceBall, 0.3, 8);
            iceRod = new RangeWeapon(146, 41, 55, 400, 2, SpriteSheetManager.iceRod, SpriteSheetManager.iceRodItem, 0.7f, 1, "Rod of Chilling Ice and Everything Nice", iceBall, 1);
            AllWeaponList.Add(iceRod);
        }

        /// <summary>
        /// Creates a new instance of a coin.
        /// </summary>
        /// <param name="pos">Position of the coin.</param>
        /// <returns></returns>
        public static Item Coin (Vector2 pos)
        {
            return new Item(10, true, SpriteSheetManager.coin, pos, Item.ItemType.coin, "+10 coins");
        }

        /// <summary>
        /// Returns a random stat upgrade.
        /// </summary>
        /// <param name="pos">Position of the upgrade.</param>
        /// <param name="stronger">Decides if it is a stronger upgrade or not.</param>
        /// <param name="rnd"></param>
        /// <returns></returns>
        public static Item NewStatUpgrade(Vector2 pos, bool stronger, Random rnd)
        {
            int whichBoost= rnd.Next(0,4);
            int coinGain=0;
            float itemMultiplier=1;
            string itemTypeName=null; //får värde längre ner.
            Item.ItemType itemType= Item.ItemType.coin; //Måste ha ett värde. Ändras senare.
            SpriteSheet spriteSheet = SpriteSheetManager.fire;

            switch(whichBoost)
            {
                case 0:
                    if(stronger)
                    {
                        coinGain = -40;
                        itemType = Item.ItemType.attackSpeedBoost;
                        itemMultiplier = 1.1f;
                        spriteSheet = SpriteSheetManager.attackSpeedBoost;
                        itemTypeName = "Attack Speed: +10%";
                    }
                    else
                    {
                        coinGain = -20;
                        itemType = Item.ItemType.attackSpeedBoost;
                        itemMultiplier = 1.05f;
                        spriteSheet = SpriteSheetManager.attackSpeedBoost;
                        itemTypeName = "Attack Speed: +5%";
                    }
                    
                    break;
                case 1:
                    if (stronger)
                    {
                        coinGain = -40;
                        itemType = Item.ItemType.damageBoost;
                        itemMultiplier = 1.1f;
                        spriteSheet = SpriteSheetManager.damageBoost;
                        itemTypeName = "Damage: +10%";
                    }
                    else
                    {
                        coinGain = -20;
                        itemType = Item.ItemType.damageBoost;
                        itemMultiplier = 1.05f;
                        spriteSheet = SpriteSheetManager.damageBoost;
                        itemTypeName = "Damage: +5%";
                    }

                    break;
                case 2:
                    if (stronger)
                    {
                        coinGain = -20;
                        itemType = Item.ItemType.speedBoost;
                        itemMultiplier = 1.1f;
                        spriteSheet = SpriteSheetManager.speedBoost;
                        itemTypeName = "Speed: +10%";
                    }
                    else
                    {
                        coinGain = -10;
                        itemType = Item.ItemType.speedBoost;
                        itemMultiplier = 1.05f;
                        spriteSheet = SpriteSheetManager.speedBoost;
                        itemTypeName = "Speed: +5%";
                    }

                    break;
                case 3:
                    if (stronger)
                    {
                        coinGain = -50;
                        itemType = Item.ItemType.healthBoost;
                        itemMultiplier = 50;
                        spriteSheet = SpriteSheetManager.healthBoost;
                        itemTypeName = "Health: +50";
                    }
                    else
                    {
                        coinGain = -25;
                        itemType = Item.ItemType.healthBoost;
                        itemMultiplier = 25;
                        spriteSheet = SpriteSheetManager.healthBoost;
                        itemTypeName = "Health: +25%";
                    }

                    break;
            }
            Item newItem = new Item(coinGain, false, spriteSheet, pos, itemType, itemTypeName)
            {
                multiplier = itemMultiplier
            };

            return newItem;

        }

        /// <summary>
        /// Returns a healing potion.
        /// </summary>
        /// <param name="pos">Position of the healing potion.</param>
        /// <param name="healAmount">Amount healed by the potion.</param>
        /// <returns></returns>
        public static Item HealPotion(Vector2 pos, int healAmount)
        {
            return new Item(0, false, SpriteSheetManager.healPotion, pos, Item.ItemType.healAndSave, "Heal and Save!",32, 64) { multiplier = healAmount };
        }

        /// <summary>
        /// Returns a random weapon item.
        /// </summary>
        /// <param name="pos">Position of the Item.</param>
        /// <param name="currCircle">Current circle of the level.</param>
        /// <param name="betterWeapon">Decides if it is a better weapon or not.</param>
        /// <param name="rnd"></param>
        /// <returns></returns>
        public static WeaponItem NewWeaponItem(Vector2 pos, int currCircle, bool betterWeapon, Random rnd)
        {
            //bool weaponChosen = false;
            int weightChoser = currCircle;
            int coinGain = 0;

            if (betterWeapon)
            {
                weightChoser++;
            }

            switch (weightChoser)
            {
                case 1:
                    coinGain = -25;
                    break;
                case 2:
                    coinGain = -50;
                    break;

                case 3:
                    coinGain = -100;
                    break;
                case 4:
                    coinGain = -150;
                    break;
                case 5:
                    coinGain = -200;
                    break;
                case 6:

                    break;
                case 7:
                    coinGain = -50;
                    break;
                case 8:
                    break;
                case 9:
                    coinGain = -100;
                    break;
                case 10:
                    break;
            }

            int weaponNum;

            do
            {
                weaponNum = rnd.Next(0, AllWeaponList.Count());
            }
            while (AllWeaponList[weaponNum].weight != weightChoser);

            return new WeaponItem(AllWeaponList[weaponNum], coinGain, false, AllWeaponList[weaponNum].itemSpriteSheet, pos, Item.ItemType.weaponType, AllWeaponList[weaponNum].itemName);
        }

        /// <summary>
        /// Returns the saved weapon.
        /// </summary>
        /// <param name="weaponName">Name of the saved weapon.</param>
        /// <returns></returns>
        public static Weapon SavedWeaponFromList(string weaponName)
        {
            Weapon returnWeapon=testMelee; //måste anges ett värde för att kunna returna. Värdet ändras i loopen nedan

            foreach(Weapon weapon in AllWeaponList)
            {
                if(weapon.itemName==weaponName)
                {
                    returnWeapon = weapon;
                    break;
                }
            }
            return returnWeapon;
        }

    }
}