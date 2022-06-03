using RPGGame.Champions;
using RPGGame.Champions.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RPGGame
{
    public partial class GameWindow : Window 
    {
        public int wave = 1; //9

        public static Player hero;
        public static Enemy enemy;
        public List<Image> inventoryItemsImagesList = new List<Image>();
        private List<Image> shopItemsImagesList = new List<Image>();
        private List<Label> shopPriceLabelsList = new List<Label>();
        private bool hasDoneSpecialAttack = false;
        private int specialAttackCooldownCounter = 5; //0
        public bool enemyIsScaling = false;
        public double scalingMultiplayer = 0;
        private int debuffCounter = 5;

        //Counters for end window (stats)
        public int bossesKilled = 0;
        public int miniBossesKilled = 0;
        public int enemiesKilled = 0;
        public int moneySpent = 0;
        public int itemsBought = 0;
        public int specialAttacksUsed = 0;
        public double critsDealt = 0;

        public GameWindow()
        {
            InitializeComponent();

            FillShopImagesList();
            FillShopPriceLabelsList();
            FillInventoryImagesList();
        }

        private void buttonFight_Click(object sender, RoutedEventArgs e)
        {
            //Demo 1 (set wave to 9 and specialAttackCooldownCounter to 0)
            //heroHealthBar.Maximum = 1000;
            //heroHealthBar.Value = 1000;
            //hero.SetMoney(10000);


            if (!hasDoneSpecialAttack)
            {
                hero.Attack();
            }
            if (enemy.isTakingDebuff)
            {
                MessageBox.Show($"The enemy {enemy.GetType()} is taking {hero.GetDamage() / 2} debuff damage from the special attack!");
                enemy.SetHp(enemy.GetHp() - hero.GetDamage() / 2);
                enemyHealthBar.Value = enemy.GetHp();

                debuffCounter--;
                if(debuffCounter == 0)
                {
                    MessageBox.Show($"The enemy {enemy.GetType()} is no longer taking debuff damage from the special attack!");
                    enemy.isTakingDebuff = false;
                }
                UpdateStatLabels();
            }
            switch (enemy.GetType())
            {
                case Enemy.Type.BOSS:
                    if (enemyHealthBar.Value <= 0)
                    {
                        MessageBox.Show("Congratulations! You have defeated the Boss!");
                        bossesKilled++;
                        specialAttackCooldownCounter--;

                        hero.SetMoney(hero.GetMoney() + 500);
                        moneyLabel.Content = "Money : " + hero.GetMoney() + " lv";
                        ClearShop();

                        if(wave == 10 && !hero.hasUpgraded) //Can unlock the spec upgrade...
                        {
                            shopItem4.Source = new BitmapImage(new Uri("C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\upgrade.png", UriKind.RelativeOrAbsolute));
                            item4PriceLabel.Content = "1000lv";
                        }

                        wave++;
                        UpdateShop();

                        enemyIsScaling = true;
                        scalingMultiplayer += 1.5;
                        GenerateEnemy();

                        //enemy.Scale();
                        enemyHealthBar.Maximum = enemy.GetHp();
                        UpdateStatLabels();

                        MessageBox.Show("The enemy " + enemy.GetType() + " has scaled in difficulty!");
                        break;
                    }

                    UpdateStatLabels();
                    enemy.Attack();

                    //Different boss phases
                    if (enemyHealthBar.Value <= enemyHealthBar.Maximum / 3)
                    {
                        ImageEnemy.Source = new BitmapImage(new Uri("C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\terminator3.png", UriKind.RelativeOrAbsolute));
                    }
                    else if (enemyHealthBar.Value <= enemyHealthBar.Maximum / 2)
                    {
                        ImageEnemy.Source = new BitmapImage(new Uri("C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\terminator2.png", UriKind.RelativeOrAbsolute));
                    }

                    if (heroHealthBar.Value <= 0)
                    {
                        MessageBox.Show("You have been defeated... Better luck next time!");

                        //Show stats window...
                        StatsWindow statsWindow = new StatsWindow();
                        statsWindow.Show();
                        this.Close();
                    }
                    break;
                case Enemy.Type.MINIBOSS:
                    if (enemyHealthBar.Value <= 0)
                    {
                        MessageBox.Show($"You have defeated the enemy miniboss {enemy.GetType()}!");
                        miniBossesKilled++;
                        specialAttackCooldownCounter--;

                        hero.SetMoney(hero.GetMoney() + 250);
                        moneyLabel.Content = "Money : " + hero.GetMoney() + " lv";
                        ClearShop();

                        wave++;

                        GenerateEnemy();
                        enemyHealthBar.Maximum = enemy.GetHp();
                        UpdateShop();
                    }
                    else
                    {
                        enemy.Attack();
                        if (heroHealthBar.Value <= 0)
                        {
                            MessageBox.Show("You have been defeated... Better luck next time!");

                            //Show stats window...
                            StatsWindow statsWindow = new StatsWindow();
                            statsWindow.Show();
                            this.Close();
                        }
                    }
                    UpdateStatLabels();
                    break;
                default:
                    if (enemyHealthBar.Value <= 0)
                    {
                        MessageBox.Show($"You have defeated the enemy {enemy.GetType()}!");
                        enemiesKilled++;
                        specialAttackCooldownCounter--;

                        hero.SetMoney(hero.GetMoney() + 100);
                        moneyLabel.Content = "Money : " + hero.GetMoney() + "lv";
                        ClearShop();
                        wave++;

                        GenerateEnemy();
                        enemyHealthBar.Maximum = enemy.GetHp();
                        UpdateShop();
                    }
                    else
                    {
                        enemy.Attack();
                        if (heroHealthBar.Value <= 0)
                        {
                            MessageBox.Show("You have been defeated... Better luck next time!");

                            //Show stats window...
                            StatsWindow statsWindow = new StatsWindow();
                            statsWindow.Show();
                            this.Close();
                        }
                    }
                    UpdateStatLabels();
                    break;
            }
        }

        private void UpdateShop()
        {
            //Shop Update...
            if (wave % 2 == 0) //Healing potion
            {
                shopItem1.Source = new BitmapImage(new Uri("C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\healing_potion.png", UriKind.RelativeOrAbsolute));
                item1PriceLabel.Content = "50lv";
            }
            if (wave % 5 == 0) //Weapon & Shield
            {
                shopItem3.Source = new BitmapImage(new Uri("C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\shield1.png", UriKind.RelativeOrAbsolute));
                item3PriceLabel.Content = "200lv";

                //Custom items for each hero...
                shopItem2.Source = new BitmapImage(new Uri("C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\" + hero.GetWeapon().ToString().ToLower() + ".png", UriKind.RelativeOrAbsolute));
                item2PriceLabel.Content = "200lv";
            }
            if (wave % 7 == 0) //Lucky talisman
            {
                shopItem5.Source = new BitmapImage(new Uri("C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\lucky_talisman.png", UriKind.RelativeOrAbsolute));
                item5PriceLabel.Content = "200lv";
            }
            if (wave % 10 == 0) //Crystal heart
            {
                shopItem6.Source = new BitmapImage(new Uri("C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\crystal_heart.png", UriKind.RelativeOrAbsolute));
                item6PriceLabel.Content = "200lv";
            }
            if (wave >= 10 && !hero.hasUpgraded) //Spec upgrade (bonus stats)
            {
                shopItem4.Source = new BitmapImage(new Uri("C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\upgrade.png", UriKind.RelativeOrAbsolute));
                item4PriceLabel.Content = "1000lv";
            }
        }

        public void GenerateEnemy()
        {
            Random random = new Random();
            int enemyCount = Enum.GetNames(typeof(Enemy.Type)).Length;
            Enemy.Type enemyGeneratedType = Enemy.Type.BOSS;
            if (wave % 5 == 0) //Boss...
            {
                
                enemy = new Boss();
                ImageEnemy.Source = new BitmapImage(new Uri("C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\terminator1.png", UriKind.RelativeOrAbsolute));
            }
            else if (((wave % 5) % 4 == 0 && wave > 5) || (wave == 4)) //Miniboss...
            {
                enemy = new Miniboss();
                ImageEnemy.Source = new BitmapImage(new Uri("C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\minotaur2.png", UriKind.RelativeOrAbsolute));
            }
            else //Generate enemy...
            {
                do
                {
                    int enemyNumber = random.Next(enemyCount);
                    Enemy.Type[] enemyTypes = (Enemy.Type[])Enum.GetValues(typeof(Enemy.Type));
                    enemyGeneratedType = (Enemy.Type)enemyTypes.GetValue(random.Next(enemyCount));
                } while (enemyGeneratedType == Enemy.Type.BOSS || enemyGeneratedType == Enemy.Type.MINIBOSS); 
            }


            if (enemyGeneratedType.Equals(Enemy.Type.ORC))
            {
                enemy = new Orc();
                ImageEnemy.Source = new BitmapImage(new Uri("C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\orc.png", UriKind.RelativeOrAbsolute));
            }
            else if (enemyGeneratedType.Equals(Enemy.Type.WEREWOLF))
            {
                enemy = new Werewolf();
                ImageEnemy.Source = new BitmapImage(new Uri("C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\werewolf.png", UriKind.RelativeOrAbsolute));
            }
            else if (enemyGeneratedType.Equals(Enemy.Type.SKELETON))
            {
                enemy = new Skeleton(); 
                ImageEnemy.Source = new BitmapImage(new Uri("C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\skeleton.png", UriKind.RelativeOrAbsolute));
            }
            else if (enemyGeneratedType.Equals(Enemy.Type.DRAGON))
            {
                enemy = new Dragon();
                ImageEnemy.Source = new BitmapImage(new Uri("C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\dragon.png", UriKind.RelativeOrAbsolute));
            }
            enemyHealthBar.Value = enemy.GetHp();
        }


        private void FillInventoryImagesList()
        {
            inventoryItemsImagesList.Add(Item1);
            inventoryItemsImagesList.Add(Item2);
            inventoryItemsImagesList.Add(Item3);
            inventoryItemsImagesList.Add(Item4);
            inventoryItemsImagesList.Add(Item5);
            inventoryItemsImagesList.Add(Item6);
        }
        private void FillShopImagesList()
        {
            shopItemsImagesList.Add(shopItem1);
            shopItemsImagesList.Add(shopItem2);
            shopItemsImagesList.Add(shopItem3);
            shopItemsImagesList.Add(shopItem4);
            shopItemsImagesList.Add(shopItem5);
            shopItemsImagesList.Add(shopItem6);
        }
        private void FillShopPriceLabelsList()
        {
            shopPriceLabelsList.Add(item1PriceLabel);
            shopPriceLabelsList.Add(item2PriceLabel);
            shopPriceLabelsList.Add(item3PriceLabel);
            shopPriceLabelsList.Add(item4PriceLabel);
            shopPriceLabelsList.Add(item5PriceLabel);
            shopPriceLabelsList.Add(item6PriceLabel);
        }

        public void UpdateStatLabels()
        {
            heroHealthLabel.Content = "Health: " + heroHealthBar.Value;
            heroDamageLabel.Content = "Damage: " + hero.GetDamage();
            heroDefenceLabel.Content = "Defense: " + hero.GetDefence();
            heroLuckLabel.Content = "Luck: " + hero.GetLuck();
            heroTypeLabel.Content = "Type: " + hero.GetType();
            waveLabel.Content = "Wave: " + wave;
            enemyHealthLabel.Content = "Health: " + enemyHealthBar.Value;
            enemyDamageLabel.Content = "Damage: " + enemy.GetDamage();
            enemyDefenceLabel.Content = "Defense: " + enemy.GetDefence();
            enemyLuckLabel.Content = "Luck: " + enemy.GetLuck();
            enemyTypeLabel.Content = "Type: " + enemy.GetType();
        }

        public void ClearShop()
        {
            foreach (Label label in shopPriceLabelsList)
            {
                label.Content = "";
            }

            foreach (Image image in shopItemsImagesList)
            {
                image.Source = null;
            }
        }
        private void PutItemInBag(String imagePath)
        {
            int avaiableSpace = inventoryItemsImagesList.IndexOf(inventoryItemsImagesList
                .Where(e => e.Source == null)
                .First()); 

            Image image = inventoryItemsImagesList
                .Where(e => e.Source == null)
                .First();

            image.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
            string imageTitle = image.Source.ToString()
                .Split('/')
                .Last();

            //Item item = new Item(image.Source.ToString());
            //item.position = avaiableSpace;

            inventoryItemsImagesList.ElementAt(avaiableSpace).Source = image.Source;
        }

        private bool BagHasSpace()
        {
            return inventoryItemsImagesList
                .Where(e => e.Source == null)
                .Count() > 0;
        }


        //Healing Potion
        private void shopItem1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (hero.GetMoney() < 50)
            {
                MessageBox.Show("You don't have enough money for this item.");
                return;
            }

            if (BagHasSpace())
            {
                MessageBox.Show("You just bought a healing potion for " + (50 + (12.5 * (int)(MainWindow.GetGameWindow().wave / 10))) + " hp for 50lv!");
                hero.SetMoney(hero.GetMoney() - 50);
                moneySpent += 50;
                itemsBought++;

                moneyLabel.Content = "Money : " + hero.GetMoney() + " lv";

                PutItemInBag("C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\healing_potion.png");
                return;
            }
            MessageBox.Show("Your bag is full! Use your items to free up some space!");
        }

        //Weapon
        private void shopItem2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (hero.GetMoney() < 200)
            {
                MessageBox.Show("You don't have enough money for this item.");
                return;
            }

            if (BagHasSpace())
            {
                hero.SetDamage(hero.GetDamage() + 25);
                MessageBox.Show("You just bought a better weapon for 200 lv! Your damage is increased by 25 and is now " + hero.GetDamage());
                hero.SetMoney(hero.GetMoney() - 200);
                moneySpent += 200;
                itemsBought++;

                moneyLabel.Content = "Money : " + hero.GetMoney() + " lv";

                UpdateStatLabels();
                shopItemsImagesList.ElementAt(1).Source = null;
                shopPriceLabelsList.ElementAt(1).Content = "";

                //DONT PUT IN BAG...
                //PutItemInBag("C:\\Users\\Martin\\source\\repos\\RPGGame (ladder prep)\\RPGGame\\Images\\" + hero.weapon.ToString().ToLower() + ".png");
                return;
            }
            MessageBox.Show("Your bag is full! Use your items to free up some space!");
        }

        //Shield
        private void shopItem3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (hero.GetMoney() < 200)
            {
                MessageBox.Show("You don't have enough money for this item.");
                return;
            }

            if (BagHasSpace())
            {
                hero.SetDefence(hero.GetDefence() + 25);

                MessageBox.Show("You just bought better armour for 200 lv! Your defence is increased by 25 and is now " + hero.GetDefence());
                hero.SetMoney(hero.GetMoney() - 200);
                moneySpent += 200;
                itemsBought++;

                moneyLabel.Content = "Money : " + hero.GetMoney() + " lv";

                UpdateStatLabels();
                shopItemsImagesList.ElementAt(2).Source = null;
                shopPriceLabelsList.ElementAt(2).Content = "";

                //DONT PUT IN BAG...
                //PutItemInBag("C:\\Users\\Martin\\source\\repos\\RPGGame (ladder prep)\\RPGGame\\Images\\shield.png");
                return;
            }
            MessageBox.Show("Your bag is full! Use your items to free up some space!");
        }


        //Spec Upgrade
        private void shopItem4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (hero.GetMoney() < 1000)
            {
                MessageBox.Show("You don't have enough money for this item.");
                return;
            }

            MessageBox.Show("You just bought hero specialization upgrade for 1000 lv!");
            hero.SetMoney(hero.GetMoney() - 1000);
            moneySpent += 1000;
            itemsBought++;

            moneyLabel.Content = "Money : " + hero.GetMoney() + " lv";

            hero.Upgrade(); //Spec stats increase...
            heroSpecLabel.Content = "Specialization: ✔️";


            //Activates Button for SpecialAttack();

            UpdateStatLabels();
            shopItemsImagesList.ElementAt(3).Source = null;
            shopPriceLabelsList.ElementAt(3).Content = "";

        }
        //Lucky Talisman
        private void shopItem5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (hero.GetMoney() < 200)
            {
                MessageBox.Show("You don't have enough money for this item.");
                return;
            }

            if (BagHasSpace())
            {
                hero.SetLuck(hero.GetLuck() + 10);

                MessageBox.Show("You just bought a lucky talisman for 200 lv!");
                hero.SetMoney(hero.GetMoney() - 200);
                moneySpent += 200;
                itemsBought++;

                moneyLabel.Content = "Money : " + hero.GetMoney() + " lv";

                UpdateStatLabels();
                shopItemsImagesList.ElementAt(4).Source = null;
                shopPriceLabelsList.ElementAt(4).Content = "";

                //DONT PUT IN BAG...
                //PutItemInBag("C:\\Users\\Martin\\source\\repos\\RPGGame (ladder prep)\\RPGGame\\Images\\shield.png");
                return;
            }
            MessageBox.Show("Your bag is full! Use your items to free up some space!");
        }

        //Heart Crystal
        private void shopItem6_MouseDown(object sender, MouseButtonEventArgs e) //BUG HERE...
        {
            if (hero.GetMoney() < 200)
            {
                MessageBox.Show("You don't have enough money for this item.");
                return;
            }

            if (BagHasSpace())
            {
                MessageBox.Show("You just bought a heart crystal for 200 lv! Your max hp is now " + hero.GetHp());
                hero.SetMoney(hero.GetMoney() - 200);
                moneySpent += 200;
                itemsBought++;

                moneyLabel.Content = "Money : " + hero.GetMoney() + " lv";

                hero.SetDamage(hero.GetDamage() + 25);
                heroHealthBar.Maximum += 25;
                heroHealthBar.Value += 25;

                UpdateStatLabels();
                shopItemsImagesList.ElementAt(5).Source = null;
                shopPriceLabelsList.ElementAt(5).Content = "";

                //DONT PUT IN BAG...
                //PutItemInBag("C:\\Users\\Martin\\source\\repos\\RPGGame (ladder prep)\\RPGGame\\Images\\shield.png");
                return;
            }
            MessageBox.Show("Your bag is full! Use your items to free up some space!");
        }
        //private void showHeroStats_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    MessageBox.Show("Test");
        //}

        private void Item1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Item item = new Item(inventoryItemsImagesList.ElementAt(0).Source.ToString());
            item.SetPosition(1);

            item.Use();
        }

        private void Item2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Item item = new Item(inventoryItemsImagesList.ElementAt(1).Source.ToString());
            item.SetPosition(2);

            item.Use();
        }

        private void Item3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Item item = new Item(inventoryItemsImagesList.ElementAt(2).Source.ToString());
            item.SetPosition(3);

            item.Use();
        }

        private void Item4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Item item = new Item(inventoryItemsImagesList.ElementAt(3).Source.ToString());
            item.SetPosition(4);

            item.Use();
        }

        private void Item5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Item item = new Item(inventoryItemsImagesList.ElementAt(4).Source.ToString());
            item.SetPosition(5);

            item.Use();
        }

        private void Item6_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Item item = new Item(inventoryItemsImagesList.ElementAt(5).Source.ToString());
            item.SetPosition(6);

            item.Use();
        }

        private void buttonSpecialAttack_Click(object sender, RoutedEventArgs e)
        {
            if (hero.hasUpgraded && specialAttackCooldownCounter <= 0)
            {
                hero.SpecialAttack();
                specialAttacksUsed++;

                hasDoneSpecialAttack = true;
                buttonFight_Click(sender, e);
                hasDoneSpecialAttack = false;
                specialAttackCooldownCounter = 5;
                return;
            }else if (specialAttackCooldownCounter > 0 && hero.hasUpgraded)
            {
                MessageBox.Show($"Your special attack is on cooldown! You can use it in {specialAttackCooldownCounter} waves!");
                return;
            }
            MessageBox.Show("You haven't unlocked the specialization needed to use special attacks! Wait for wave 10 and purchase it from the shop!");
        }
    }
}
