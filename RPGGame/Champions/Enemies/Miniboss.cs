using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame.Champions.Enemies
{
    public class Miniboss : Enemy
    {
            public Miniboss()
            {
                hp = 200;
                damage = 25;
                damageMultiplier = 1.2;
                defense = 20;
                luck = 10;
                type = Type.MINIBOSS;


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
