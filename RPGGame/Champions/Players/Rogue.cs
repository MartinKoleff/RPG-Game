using RPGGame.Champions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RPGGame
{
    public class Rogue : Player
    {
        public Rogue()
        {
            hp = 100;
            damage = 35;
            damageMultiplier = 1.3;
            luck = 35;
            defense = 20;
            type = Type.ROGUE;
            weapon = Weapon.DAGGER;
        }
        public override void Attack()
        {
            base.Attack();
        }

        //5X Damage and steals 250lv
        public override void SpecialAttack()
        {
            int totalDamage = this.damage * 5;
            int damageTemp = this.damage;
            this.damage = totalDamage;
   
            base.Attack();
            this.damage = damageTemp;

            MessageBox.Show("You have dealt 5x attack and you have stolen 250lv!");
            GameWindow.hero.SetMoney(GameWindow.hero.GetMoney() + 250);
        }
    }
}