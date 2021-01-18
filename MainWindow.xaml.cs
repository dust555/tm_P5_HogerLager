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


namespace HogerLager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Game Game = new Game();

        public MainWindow()
        {

            DataContext = Game;
            Game.MessageBoxEvent += ShowMessageBox;
            InitializeComponent();

            NewGameButton.Click += Game.NewGame;
            HigherButton.Click += Game.Guess;
            LowerButton.Click += Game.Guess;

            SortCardListASC.Click += Game.SortCardList;
            SortCardListDESC.Click += Game.SortCardList;

            UploadScore.Click += Game.UploadScore;

        }

        public void ShowMessageBox(String message){
            MessageBox.Show(message);
        }
    }
}
