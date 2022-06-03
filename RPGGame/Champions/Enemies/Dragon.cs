using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame.Champions.Enemies
{
    public class Dragon : Enemy
    {
        public Dragon()
        {
            hp = 90;
            damage = 25;
            damageMultiplier = 1;
            defense = 20;
            luck = 10;
            type = Type.DRAGON;


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
