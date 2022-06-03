using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame.Champions.Enemies
{
    internal class Orc : Enemy
    {
        public Orc()
        {
            hp = 100;
            damage = 15;
            damageMultiplier = 1;
            defense = 30;
            luck = 10;
            type = Type.ORC;

            if (MainWindow.GetGameWindow().enemyIsScaling)
            {
                this.Scale();
            }
        }
        public override void Attack()
        {
            base.Attack();
        }
        public override void Scale()
        {
            base.Scale();
        }
    }
}
