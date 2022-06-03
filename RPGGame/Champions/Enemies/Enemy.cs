using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RPGGame.Champions.Enemies
{
    public class Enemy
    {
        protected double hp;
        protected double damage;
        protected double damageMultiplier;
        protected double defense;
        protected int luck;
        //public double scalingMultiplayer = 1.5;
        //public bool isScaling = false;
        protected Type type;

        public bool isTakingDebuff = false;

        public virtual void Attack()
        {
            if (GameWindow.hero.isImmortal)
            {
                MessageBox.Show($"Hero is immortal! 0 Damage dealt by the enemy {this.type}!");
                GameWindow.hero.isImmortal = false;
                return;
            }
            Random luckRandom = new Random();
            double totalDamage = damage;
            if (luckRandom.Next(100) <= luck)
            {
                totalDamage *= damageMultiplier;
                MessageBox.Show($"Critical strike done by the enemy {this.type} -> {totalDamage} damage taken!");
            }

            Random defenseRandom = new Random();
            if (defenseRandom.Next(100) <= GameWindow.hero.GetDefence())
            {
                totalDamage -= (double)GameWindow.hero.GetDefence() / 2;
                if (totalDamage < 0)
                {
                    totalDamage = 0;
                }
                MessageBox.Show($"You have blocked {(double)GameWindow.hero.GetDefence() / 2} damage from the enemy {GameWindow.enemy.type}!");
            }
            MainWindow.GetGameWindow().heroHealthBar.Value -= totalDamage;
        }

        public double GetHp()
        {
            return hp;
        }

        public void SetHp(double hp)
        {
            this.hp = hp;
        }

        public double GetDamage()
        {
            return damage;
        }

        public int GetLuck()
        {
            return luck;
        }

        public double GetDefence()
        {
            return defense;
        }

        public Type GetType()
        {
            return type;
        }

        public virtual void Scale()
        {
            hp *= MainWindow.GetGameWindow().scalingMultiplayer;
            damage *= MainWindow.GetGameWindow().scalingMultiplayer;
            defense *= MainWindow.GetGameWindow().scalingMultiplayer;
            //MessageBox.Show("The enemy " + this.type + " has scaled in difficulty!");
            
        }

        public enum Type
        {
        ORC,
        DRAGON,
        WEREWOLF,
        SKELETON,
        BOSS,
        MINIBOSS
        }

    
    }
}

