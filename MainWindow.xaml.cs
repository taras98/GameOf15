using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path = @"1.txt";
        string path2 = @"2.txt";
        string path3 = @"3.txt";

        Game15 game;
        public MainWindow()
        {
            InitializeComponent();
            game = new Game15(4);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int position = Convert.ToInt32(((Button)sender).Tag);
            game.shift(position);
            //if (game.check_numbers())
            //{
            //    MessageBox.Show("Win", "Win");
            //    start_game();
            //}
            refresh();
        }
        private Button button(int position)
        {
            switch (position)
            {
                case 0: return button0;
                case 1: return button1;
                case 2: return button2;
                case 3: return button3;
                case 4: return button4;
                case 5: return button5;
                case 6: return button6;
                case 7: return button7;
                case 8: return button8;
                case 9: return button9;
                case 10: return button10;
                case 11: return button11;
                case 12: return button12;
                case 13: return button13;
                case 14: return button14;
                case 15: return button15;
                default: return null;


            }
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            start_game();
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {

                for (int i = 0; i < 5; i++)
                {

                    sw.WriteLine(game.shuffler());
                }
                sw.Close();
            }
            refresh();
            using (StreamWriter sw = new StreamWriter(path2, false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        sw.Write(button(i * 4 + j).Content + "|");
                    }
                    sw.WriteLine();

                }
                sw.Close();
            }

        }
        private void start_game()
        {
            game.start();
            refresh();
        }
        private void refresh()
        {
            for (int position = 0; position < 16; position++)
            {
                button(position).Content = game.getNumber(position);
                if (game.getNumber(position) > 0) button(position).Visibility = Visibility.Visible;
                else button(position).Visibility = Visibility.Hidden;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            start_game();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string[] lines = File.ReadAllLines(path);

            IEnumerable<string> revLines = lines.Reverse();

            
                foreach (string line in revLines)
                {
                    string[] divided = line.Split(',');
                    if (Convert.ToInt32(divided[1]) == 1)
                    {
                        game.shift((Convert.ToInt32(divided[0]) - 1));
                    }
                    if (Convert.ToInt32(divided[1]) == 2)
                    {
                        game.shift((Convert.ToInt32(divided[0]) + 1));
                    }
                    if (Convert.ToInt32(divided[1]) == 3)
                    {
                        game.shift((Convert.ToInt32(divided[0]) - 4));
                    }
                    if (Convert.ToInt32(divided[1]) == 4)
                    {
                        game.shift((Convert.ToInt32(divided[0]) + 4));
                    }

                    refresh();
                    Thread.Sleep(5000);
                }
                if (game.check_numbers())
                {
                    MessageBox.Show("Win", "Win");
                    start_game();
                    return;

                }

            

        }
    }
}

