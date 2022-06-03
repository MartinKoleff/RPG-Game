using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame.Champions.Enemies
{
    public class Skeleton : Enemy
    {
        public Skeleton()
        {
            hp = 110;
            damage = 15;
            damageMultiplier = 1;
            defense = 15;
            luck = 20;
            type = Type.SKELETON;

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
