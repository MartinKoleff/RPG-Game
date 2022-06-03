using RPGGame.Champions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RPGGame
{
    public class Warrior : Player
    {
        public Warrior()
        {
            hp = 120;
            damageMultiplier = 1;
            damage = 25;
            luck = 15;
            defense = 50;
            type = Type.WARRIOR;
            weapon = Weapon.LIGHT_SABER;
        }

        public override void Attack()
        {
            base.Attack();
        }

        //Does 3 Attacks and stuns enemy (100% dodge chance)...
        public override void SpecialAttack()
        {
            int luckTemp = this.luck;
            luck = 0;

            this.Attack();
            this.Attack();
            this.Attack();
            luck = luckTemp; //No crits

            isImmortal = true;

            MessageBox.Show("You have attacked 3 times!");
        }
    }
}

