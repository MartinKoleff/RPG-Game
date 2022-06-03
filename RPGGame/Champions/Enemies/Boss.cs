using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame.Champions.Enemies
{
    public class Boss : Enemy
    {
        public Boss()
        {
            hp = 300;
            damage = 25;
            damageMultiplier = 1.2;
            defense = 25;
            luck = 15;
            type = Type.BOSS;

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
