﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    class Weapon
    {
        public int baseDamage;
        public int baseRange;
        public float baseAttackSpeed;
        public float damageMultiplyier;
        public float rangeMultiplyier;
        public float attackSpeedMultiplyier;


        public Weapon(int baseDamage, float baseAttackSpeed)
        {
            this.baseAttackSpeed = baseAttackSpeed;
            this.baseDamage = baseDamage;
            damageMultiplyier = 1;
            attackSpeedMultiplyier = 1;
        }


    }
}
