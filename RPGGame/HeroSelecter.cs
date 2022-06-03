using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RPGGame
{
    public class HeroSelecter<T> where T : Champion // : Initializable //: Window
    {
        private T hero;
        private Image heroImage;
        public HeroSelecter()
        {
            SetHero();
            //Game logic...
        }

        private void SetHero()
        {
            switch (typeof(T).Name) 
            {
                case "Warrior":
                    hero = new Warrior() as T;
                    setImage("C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\warrior.png");
                    break;
                case "Mage":
                    hero = new Mage() as T;
                    setImage("C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\mage.png");
                    break;
                case "Rogue":
                    hero = new Rogue() as T;
                    setImage("C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\rogue.png");
                    break;
                case "Sniper":
                    hero = new Sniper() as T;
                    setImage("C:\\Users\\Martin\\source\\repos\\RPGGame\\RPGGame\\Images\\sniper.png");
                    break;
                default:
                    throw new Exception();
            }
        }

        public T getHero()
        {
            return hero;
        }

        public void setImage(String imagePath)
        {
            Image image = new Image();
            image.Width = 200;
            image.Height = 200;

            image.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));

            heroImage = image;
        }

        public Image getImage()
        {
            return heroImage;
        }
    }
}
//T.GetType().Name
//hero = (T)Activator.CreateInstance(typeof(T));
//public GameWindowController(T hero)
//{
//    this.hero = hero;
//}
//public ImageSource getImage()
//{
//    return heroImage;
//}
//public void setImage(String imagePath)
//{
//    //heroImage = new Image(imagePath);
//    ImageSource image = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
//    heroImage = image;
//}
//public void setImage(String imagePath)
//{
//    //heroImage = new Image(imagePath);
//    ImageSource image = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
//    heroImage = image;
//}


//public void setImage(Image image)
//{
//    heroImage = image;
//}
//private ImageSource heroImage;
