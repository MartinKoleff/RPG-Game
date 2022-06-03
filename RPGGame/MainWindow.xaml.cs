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
    public partial class MainWindow : Window
    {
        private static GameWindow gameWindow = new GameWindow();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void openGame()
        {
            gameWindow.GenerateEnemy();

            //Setting starting values for hp
            gameWindow.enemyHealthBar.Value = GameWindow.enemy.GetHp();
            gameWindow.enemyHealthBar.Maximum = GameWindow.enemy.GetHp();

            gameWindow.heroHealthBar.Value = GameWindow.hero.GetHp();
            gameWindow.heroHealthBar.Maximum = GameWindow.hero.GetHp();

            gameWindow.UpdateStatLabels();
            gameWindow.Show();
            this.Close();
        }

        private void ChooseWarrior(object sender, MouseButtonEventArgs e)
        {
            GameWindow.hero = new Warrior();

            string imagePath = "C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\warrior.png";
            gameWindow.ImageHero.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));

            openGame();
        }

        private void ChooseMage(object sender, MouseButtonEventArgs e)
        {
            GameWindow.hero = new Mage();

            string imagePath = "C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\mage.png"; 
            gameWindow.ImageHero.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));

            openGame();
        }

        private void ChooseRogue(object sender, MouseButtonEventArgs e)
        {
            GameWindow.hero = new Rogue();

            string imagePath = "C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\rogue.png";
            gameWindow.ImageHero.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));

            openGame();
        }

        private void ChooseSniper(object sender, MouseButtonEventArgs e)
        {
            GameWindow.hero = new Sniper();

            string imagePath = "C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\sniper.png"; 
            gameWindow.ImageHero.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));

            openGame();
        }

        public static GameWindow GetGameWindow()
        {
            return gameWindow;
        }
    }
}

