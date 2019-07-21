using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x415

namespace kolkoikrzyzyk
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Game2 game = new Game2();
        char mark = 'X';
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void ClickButton(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;

            var x = Grid.GetRow(button);
            var y = Grid.GetColumn(button);

            if (game.AddToTabb(x, y, mark)) //niech gra doda na pozycji x/y znak(mark), który użytkownik ma. Zwraca boola. jeżeli udało się udać, to wykonaj if.
            {
                button.Content = mark;

                if (mark == 'X')
                {
                    mark = 'O';
                }
                else
                {
                    mark = 'X';
                }
            }

            if (game.CheckGameStatus())
            {
                var a = game.WinnerMark;

                var dialog = new MessageDialog($"Wygrał: {game.WinnerMark}");
                await dialog.ShowAsync();
                ClearGame();
            }
        }

        private void ClearGame()    //czyszczenie gry
        {
            game = new Game2();

            B1.Content = "";
            B2.Content = "";
            B3.Content = "";

            B4.Content = "";
            B5.Content = "";
            B6.Content = "";

            B7.Content = "";
            B8.Content = "";
            B9.Content = "";
        }

        private void B1_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class Game2
    {
        private char[,] gameTab;
        public char WinnerMark;
        public Game2()
        {
            gameTab = new char[3, 3]; 
        }
        public bool AddToTabb(int x, int y, char mark)
        {
            if (!Char.IsLetter(gameTab[x,y])) //sprawdza, czy w danym polu znajduję się juz jakiś znak
            {
                gameTab[x, y] = mark;
                return true;
            }
            return false;   //jeśli tak, zwraca false
        }

        private bool CheckRows()    //sprawdza poziomo
        {
            char mark = 'X';
            for (int k = 0; k < 2; k++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (gameTab[i,0] == mark && gameTab[i,1] == mark && gameTab[i,2] == mark)
                    {
                        WinnerMark = mark;
                        return true;
                    }
                }
                mark = 'O';
            }
            return false;
        }

        private bool CheckCols()    //sprawdza pionowo
        {
            char mark = 'X';
            for (int k = 0; k < 2; k++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (gameTab[0, i] == mark && gameTab[1, i] == mark && gameTab[2, i] == mark)
                    {
                        WinnerMark = mark;
                        return true;
                    }
                }
                mark = 'O';
            }
            return false;
        }

        public bool CheckGameStatus()   //sprawdza kto wygrał (metoda publiczna)
        {
            if (CheckRows() || CheckCols() ||CheckCross() || CheckFull())
            {
                return true;
            }
            return false;
        }

        private bool CheckCross()   //sprawdza skosy
        {
            char mark = 'X';
            for (int i = 0; i < 2; i++)
            {
                if (gameTab[0, 0] == mark && gameTab[1, 1] == mark && gameTab[2, 2] == mark)
                {
                   WinnerMark = mark;
                   return true;
                }else if (gameTab[0, 2] == mark && gameTab[1, 1] == mark && gameTab[2, 0] == mark)
                {
                   WinnerMark = mark;
                   return true;
                }
                    mark = 'O';
            }
            return false;

        }
        private bool CheckFull()    //sprawdzenie remisu
        {
            foreach (char x in gameTab)
            {
                if (!Char.IsLetter(x))
                {
                    return false;
                }
            }
            WinnerMark = 'D';
            return true;
        }
    }
}
