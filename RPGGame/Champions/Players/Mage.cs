using RPGGame.Champions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RPGGame
{
    public class Mage : Player
    {
        public Mage()
        {
            hp = 100;
            damageMultiplier = 1.2;
            damage = 35;
            defense = 20;
            luck = 15;
            type = Type.MAGE;
            weapon = Weapon.WAND;
        }
        public override void Attack()
        {
            base.Attack();
        }

        //Debuff with tick damage till the death of the enemy (works with the boss) //debuff = 50% attack damage 
        public override void SpecialAttack()
        {
            double totalDamage = this.damage * 2.5;
            int damageTemp = this.damage;
            this.damage = (int)totalDamage;

            GameWindow.enemy.isTakingDebuff = true;
            this.Attack();

            this.damage = damageTemp;
            this.Attack();

            MessageBox.Show("Your enemy will take debuff for 5 rounds!");
        }
    }
}
//GameWindow.enemy.hp -= totalDamage; //TEST
