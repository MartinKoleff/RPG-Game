using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RPGGame
{
    public class Item
    {
        private int price;
        private int quantity;
        private int position;

        private string imagePath;
        private string title;

        public Item(string imagePath)
        {
            this.imagePath = imagePath;
            title = imagePath.Split('/')
                .Last();

            switch (title)
            {
                case "healing_potion.png":
                    break;
                case "lucky_talisman.png":
                    break;
                case "crystal_heart.png":
                    break;
            }
        }
        public override string ToString()
        {
            return title;
        }
        public int GetPosition()
        {
            return position;
        }
        public void SetPosition(int position)
        {
            this.position = position;
        }
        public void Use()
        {
            switch (title)
            {
                case "healing_potion.png":
                    MainWindow.GetGameWindow().heroHealthBar.Value += 50 +  (12.5 * (int)(MainWindow.GetGameWindow().wave / 10)); //scaling for potions...
                    MainWindow.GetGameWindow().UpdateStatLabels();
                    MainWindow.GetGameWindow().inventoryItemsImagesList.ElementAt(position - 1).Source = null;
                    break;
                case "lucky_talisman.png":
                    if (GameWindow.hero.GetLuck() + 10 >= 100)
                    {
                        MessageBox.Show("Max luck reached! All attacks from now on will deal crit damage!");
                        GameWindow.hero.SetLuck(100);
                    }
                    else
                    {
                        GameWindow.hero.SetLuck(GameWindow.hero.GetLuck() + 10);
                    }
                    MainWindow.GetGameWindow().UpdateStatLabels();
                    break;
                case "crystal_heart.png":
                    GameWindow.hero.SetHp(GameWindow.hero.GetHp() + 25);
                    MainWindow.GetGameWindow().heroHealthBar.Maximum += 25;
                    MainWindow.GetGameWindow().heroHealthBar.Value += 25;
                    MainWindow.GetGameWindow().UpdateStatLabels();
                    break;
            }
        }
    }
}
//MainWindow.GetGameWindow().inventoryItemsImagesList.ElementAt(position - 1).Source = null;
