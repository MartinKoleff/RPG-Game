using RPGGame.Champions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RPGGame
{
    public class Sniper : Player
    {
        public Sniper() 
        {
            hp = 80;
            damage = 35;
            damageMultiplier = 1.5;
            luck = 35;
            defense = 25;
            type = Type.SNIPER;
            weapon = Weapon.SNIPER_RIFLE;
        }
        public override void Attack()
        {
            base.Attack();
        }

        //One shots enemy but lowers defence...
        public override void SpecialAttack()
        {
            int damageTemp = this.damage;
            int totalDamage = int.MaxValue;
            this.damage = totalDamage;

            int luckTemp = this.luck; 
            this.luck = 0;
            this.Attack();
            MessageBox.Show("You have killed the enemy in one shot!");

            damage = damageTemp;
            luck = luckTemp;

            this.defense -= 5;
        }
    }
}