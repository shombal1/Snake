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
        public String name;

        public MainWindow(string name)
        {

            InitializeComponent();
            this.name = name;
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
                int c = map.MapGenerateApple[random.Next(1, map.MapGenerateApple.GetLength(0))];
                y =c/map.SizeX;
                x=c%map.SizeX;
                if (map.pole[y, x].type == ActorType.grass)
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
            if (Save.FindMap(name) == -1)
            {
                map = new Map(Save.GetDefaultMap(Save.FindDefaultMap(name)), Apple);
            }
            else
            {
                map = new Map(Save.GetMap(Save.FindMap(name)), Apple);
            }


            snake = new Snakes(map, map.parametrsMap.LocationSnakeY, map.parametrsMap.LocationSnakeX, 3, Brushes.LightGreen,Brushes.Green,hitWall);
            


            map.AddToGrid(Grid);

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
                Thread.Sleep(58);
            }
        }


        private void keyDown(object sender, KeyEventArgs e)
        {
            try
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
            catch
            { }
        }

        private void wa_Click(object sender, RoutedEventArgs e)
        {
            map.Restart();

            snake.Restart(map, map.parametrsMap.LocationSnakeY, map.parametrsMap.LocationSnakeX, 3, Brushes.LightGreen, Brushes.Green, hitWall);





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
