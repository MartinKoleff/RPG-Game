using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RPGGame.Champions
{
    public abstract class Player
    {
        protected int hp;
        protected int damage;
        protected double damageMultiplier;
        protected int defense;
        protected int luck;
        protected Type type;
        protected Weapon weapon;

        public bool hasUpgraded = false;
        private int money;
        public bool isImmortal = false;
        public Player()
        {

        }
        public Player(int hp, int damage, int damageMultiplier, int defense, int luck, Type type, Weapon weapon)
        {
            this.hp = hp;
            this.damage = damage;
            this.damageMultiplier = damageMultiplier;
            this.defense = defense;
            this.luck = luck;
            this.type = type;
            this.weapon = weapon;
        }
        public int GetHp()
        {
            return hp;
        }
        public void SetHp(int hp)
        {
            this.hp = hp;
        }
      
        public int GetMoney()
        {
            return money;
        }
        public void SetMoney(int money)
        {
            this.money = money;
        }

        public int GetDamage()
        {
            return damage;
        }
        public void SetDamage(int damage)
        {
            this.damage = damage;
        }

        public int GetLuck()
        {
            return luck;
        }

        public void SetLuck(int luck)
        {
            this.luck = luck;
        }
        public int GetDefence()
        {
            return defense;
        }
        public void SetDefence(int defence)
        {
            this.defense = defence;
        }

        public Type GetType()
        {
            return type;
        }

        public Weapon GetWeapon()
        {
            return weapon;
        }

        public abstract void SpecialAttack();

        public virtual void Attack()
        {
            Random luckRandom = new Random();
            double totalDamage = damage;
            if (luckRandom.Next(100) <= luck)
            {
                totalDamage *= damageMultiplier;
                MainWindow.GetGameWindow().critsDealt += totalDamage - damage;
                MessageBox.Show($"You have dealt a critical strike -> {totalDamage} damage dealt to the enemy {GameWindow.enemy.GetType()}!");
            }

            Random defenseRandom = new Random();
            if (defenseRandom.Next(100) <= GameWindow.enemy.GetDefence())
            {
                totalDamage -= (double)GameWindow.enemy.GetDefence() / 2;
                if(totalDamage < 0)
                {
                    totalDamage = 0;
                }
                MessageBox.Show($"The enemy {GameWindow.enemy.GetType()} has blocked {(double)GameWindow.enemy.GetDefence() / 2} damage!");
            }

            MainWindow.GetGameWindow().enemyHealthBar.Value -= totalDamage;
            GameWindow.enemy.SetHp(GameWindow.enemy.GetHp() - totalDamage); 
        }

        public void Upgrade()
        {
            damage += 10;
            defense += 10;
            luck += 5;
            hasUpgraded = true;
        }
        public enum Type
        {
        WARRIOR,
        MAGE,
        ROGUE,
        SNIPER
        }

        public enum Weapon
        {
            SWORD,
            LIGHT_SABER,
            DAGGER,
            WAND,
            SNIPER_RIFLE
        }
    }
}
