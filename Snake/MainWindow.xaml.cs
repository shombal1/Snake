using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace Snake
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        delegate void MOVE();
        MOVE move;
        HitWall hitWall;
        AddApple Apple;

        Map map; 
        Snakes snake;
        DispatcherTimer timer = new DispatcherTimer();
        Random random = new Random();

        bool EndGame = false;
        int Score = -2;

        public MainWindow()
        {
            
            InitializeComponent();
        }


        public void GameOver()
        {
            EndGame = true;
            
        }

        public void AddApple()
        {
            Score++;
            L_Score.Content = "Score " + Convert.ToString(Score);
            int y = 0;
            int x = 0;
            while (true)
            {
                y = random.Next(1, map.SizeY);
                x = random.Next(1, map.SizeX);
                if (map.pole[y, x].type == EnumActor.grass)
                {
                    map.NewApple(y, x);
                    break;
                }
            }
        }

        private void BeginPlay(object sender, RoutedEventArgs e)
        {
            hitWall = GameOver;
            Apple = AddApple;

            map = new Map(50, 30, 18, 18, Brushes.MediumSeaGreen, Apple);

            snake = new Snakes(map, map.SizeY/2, map.SizeX/2, 3, Brushes.LightGreen,Brushes.Green,hitWall);
            


            map.AddToGrid(Grid);
            map.CreateWall(Brushes.Gray);

            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            move = snake.moveUp;



            Grid.Focusable = true;
            Grid.Focus();

            AddApple();
            AddApple();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!EndGame)
            {
                move();
                Thread.Sleep(47);
            }
        }


        private void keyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    if (!snake.ReturnMoveUP())
                    {
                        move = snake.moveUp;
                    }
                    break;
                case Key.S:
                    if (!snake.ReturnMoveDown())
                    {
                        move = snake.moveDown;
                    }                    
                    break;
                case Key.A:
                    if (!snake.ReturnMoveLeft())
                    {
                        move = snake.moveLeft;
                    }                   
                    break;
                case Key.D:
                    if (!snake.ReturnMoveRigts())
                    {
                        move = snake.moveRigts;
                    }                   
                    break;
            }             
        }

        private void wa_Click(object sender, RoutedEventArgs e)
        {
            map.Restart(18, 18, Brushes.MediumSeaGreen, Apple);

            snake.Restart(map, map.SizeY / 2, map.SizeX / 2, 3, Brushes.LightGreen, Brushes.Green, hitWall);



            map.CreateWall(Brushes.Gray);

            move = snake.moveUp;

            EndGame = false;

            Grid.Focusable = true;
            Grid.Focus();

            Score = -2;

            AddApple();
            AddApple();

        }

        private void ll(object sender, RoutedEventArgs e)
        {
            wa.Content = 1;
        }
    }
}
