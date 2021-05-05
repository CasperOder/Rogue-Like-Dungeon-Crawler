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
        public static MeleeWeapon testMelee, sweepMelee, knifeMelee;
        public static List<Weapon> AllWeaponList = new List<Weapon>();

        public static void LoadAllWeaponsAndItems()
        {
            testMelee = new MeleeWeapon(120, 40, 20, 2, SpriteSheetManager.arrow, SpriteSheetManager.arrowItem, 0.9f, 1);
            sweepMelee = new MeleeWeapon(60, 180, 40, 1, SpriteSheetManager.sweep, SpriteSheetManager.sweepItem, 0.69f, 2);
            knifeMelee = new MeleeWeapon(30, 25, 5, 10, SpriteSheetManager.knife, SpriteSheetManager.knifeItem, 1.5f, 1);
            AllWeaponList.Add(testMelee);
            AllWeaponList.Add(sweepMelee);
            AllWeaponList.Add(knifeMelee);
        }

        public static Item coin (Vector2 pos)
        {
            return new Item(10, true, SpriteSheetManager.coin, pos, Item.ItemType.coin);
        }

        public static Item newStatUpgrade(Vector2 pos, bool stronger, Random rnd)
        {
            int whichBoost= rnd.Next(0,4);
            int coinGain=0;
            float multiplier=1;
            Item.ItemType itemType= Item.ItemType.coin;
            SpriteSheet spriteSheet = SpriteSheetManager.fire;

            switch(whichBoost)
            {
                case 0:
                    if(stronger)
                    {
                        coinGain = -40;
                        itemType = Item.ItemType.attackSpeedBoost;
                        multiplier = 1.1f;
                        spriteSheet = SpriteSheetManager.attackSpeedBoost;
                    }
                    else
                    {
                        coinGain = -20;
                        itemType = Item.ItemType.attackSpeedBoost;
                        multiplier = 1.05f;
                        spriteSheet = SpriteSheetManager.attackSpeedBoost;
                    }
                    break;
                case 1:
                    if (stronger)
                    {
                        coinGain = -40;
                        itemType = Item.ItemType.damageBoost;
                        multiplier = 1.1f;
                        spriteSheet = SpriteSheetManager.damageBoost;
                    }
                    else
                    {
                        coinGain = -20;
                        itemType = Item.ItemType.damageBoost;
                        multiplier = 1.05f;
                        spriteSheet = SpriteSheetManager.damageBoost;
                    }

                    break;
                case 2:
                    if (stronger)
                    {
                        coinGain = -20;
                        itemType = Item.ItemType.speedBoost;
                        multiplier = 1.1f;
                        spriteSheet = SpriteSheetManager.speedBoost;
                    }
                    else
                    {
                        coinGain = -10;
                        itemType = Item.ItemType.speedBoost;
                        multiplier = 1.05f;
                        spriteSheet = SpriteSheetManager.speedBoost;
                    }

                    break;
                case 3:
                    if (stronger)
                    {
                        coinGain = -50;
                        itemType = Item.ItemType.healthBoost;
                        multiplier = 50;
                        spriteSheet = SpriteSheetManager.healthBoost;
                    }
                    else
                    {
                        coinGain = -25;
                        itemType = Item.ItemType.healthBoost;
                        multiplier = 25;
                        spriteSheet = SpriteSheetManager.healthBoost;
                    }

                    break;
            }
            Item newItem = new Item(coinGain, false, spriteSheet, pos, itemType);
            newItem.multiplier = multiplier;

            return newItem;

        }

        public static WeaponItem newWeaponItem (Vector2 pos, int currCircle, bool betterWeapon, Random rnd)
        {            
            bool weaponChosen = false;
            int weightChoser = currCircle;
            int coinGain=0;

            if(betterWeapon)
            {
                weightChoser++;
            }

            switch(weightChoser)
            {
                case 1:
                    coinGain = -25;
                    break;
                case 2:
                    coinGain = -50;
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
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

            return new WeaponItem(AllWeaponList[weaponNum], coinGain, false, AllWeaponList[weaponNum].itemSpriteSheet, pos, Item.ItemType.weaponType);

        }

    }
}