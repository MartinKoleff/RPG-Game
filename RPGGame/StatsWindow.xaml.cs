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
using System.Windows.Shapes;

namespace RPGGame
{
    /// <summary>
    /// Interaction logic for StatsWindow.xaml
    /// </summary>
    public partial class StatsWindow : Window
    {
        public StatsWindow()
        {
            InitializeComponent();
            UpdateStatsLabels();
        }

        private void UpdateStatsLabels()
        {
            bossesKilledLabel.Content = "Bosses killed: " + MainWindow.GetGameWindow().bossesKilled;
            miniBossesKilledLabel.Content = "Mini bosses killed: " + MainWindow.GetGameWindow().miniBossesKilled;
            enemiesKilledLabel.Content = "Enemies killed: " + MainWindow.GetGameWindow().enemiesKilled;
            moneySpentLabel.Content = "Money spent: " + MainWindow.GetGameWindow().moneySpent;
            itemsBoughtLabel.Content = "Items bought: " + MainWindow.GetGameWindow().itemsBought;
            specialAttacksUsedLabel.Content = "Special attacks used: " + MainWindow.GetGameWindow().specialAttacksUsed;
            critsDealtLabel.Content = "Crits damage dealt: " + MainWindow.GetGameWindow().critsDealt;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
