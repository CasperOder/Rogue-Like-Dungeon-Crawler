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
        public static MeleeWeapon testMelee, sweepMelee, knifeMelee, spear;
        public static List<Weapon> AllWeaponList = new List<Weapon>();

        public static void LoadAllWeaponsAndItems()
        {
            testMelee = new MeleeWeapon(120, 40, 20, 2, SpriteSheetManager.arrow, SpriteSheetManager.arrowItem, 0.9f, 1, "testweapon");
            sweepMelee = new MeleeWeapon(60, 180, 75, 1, SpriteSheetManager.sweep, SpriteSheetManager.sweepItem, 0.69f, 2, "battleaxe");
            knifeMelee = new MeleeWeapon(30, 25, 5, 10, SpriteSheetManager.knife, SpriteSheetManager.knifeItem, 1.5f, 1, "knife");
            AllWeaponList.Add(testMelee);
            AllWeaponList.Add(sweepMelee);
            AllWeaponList.Add(knifeMelee);
            spear = new MeleeWeapon(200, 16, 30, 1.5f, SpriteSheetManager.spear, SpriteSheetManager.spearItem, 0.75f, 3, "spear");
            AllWeaponList.Add(spear);
        }

        public static Item coin (Vector2 pos)
        {
            return new Item(10, true, SpriteSheetManager.coin, pos, Item.ItemType.coin, "+10 coins");
        }

        public static Item newStatUpgrade(Vector2 pos, bool stronger, Random rnd)
        {
            int whichBoost= rnd.Next(0,4);
            int coinGain=0;
            float multiplier=1;
            string itemTypeName=null; //får värde längre ner.
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
                        itemTypeName = "Attack Speed: +10%";
                    }
                    else
                    {
                        coinGain = -20;
                        itemType = Item.ItemType.attackSpeedBoost;
                        multiplier = 1.05f;
                        spriteSheet = SpriteSheetManager.attackSpeedBoost;
                        itemTypeName = "Attack Speed: +5%";
                    }
                    
                    break;
                case 1:
                    if (stronger)
                    {
                        coinGain = -40;
                        itemType = Item.ItemType.damageBoost;
                        multiplier = 1.1f;
                        spriteSheet = SpriteSheetManager.damageBoost;
                        itemTypeName = "Damage: +10%";
                    }
                    else
                    {
                        coinGain = -20;
                        itemType = Item.ItemType.damageBoost;
                        multiplier = 1.05f;
                        spriteSheet = SpriteSheetManager.damageBoost;
                        itemTypeName = "Damage: +5%";
                    }

                    break;
                case 2:
                    if (stronger)
                    {
                        coinGain = -20;
                        itemType = Item.ItemType.speedBoost;
                        multiplier = 1.1f;
                        spriteSheet = SpriteSheetManager.speedBoost;
                        itemTypeName = "Speed: +10%";
                    }
                    else
                    {
                        coinGain = -10;
                        itemType = Item.ItemType.speedBoost;
                        multiplier = 1.05f;
                        spriteSheet = SpriteSheetManager.speedBoost;
                        itemTypeName = "Speed: +5%";
                    }

                    break;
                case 3:
                    if (stronger)
                    {
                        coinGain = -50;
                        itemType = Item.ItemType.healthBoost;
                        multiplier = 50;
                        spriteSheet = SpriteSheetManager.healthBoost;
                        itemTypeName = "Health: +50";
                    }
                    else
                    {
                        coinGain = -25;
                        itemType = Item.ItemType.healthBoost;
                        multiplier = 25;
                        spriteSheet = SpriteSheetManager.healthBoost;
                        itemTypeName = "Health: +25%";
                    }

                    break;
            }
            Item newItem = new Item(coinGain, false, spriteSheet, pos, itemType, itemTypeName);
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
                    coinGain = -100;
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

            return new WeaponItem(AllWeaponList[weaponNum], coinGain, false, AllWeaponList[weaponNum].itemSpriteSheet, pos, Item.ItemType.weaponType, AllWeaponList[weaponNum].itemName);

        }

    }
}