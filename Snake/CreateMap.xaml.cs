using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Snake
{
    /// <summary>
    /// Логика взаимодействия для CreateMap.xaml
    /// </summary>
    public partial class CreateMap : Window
    {
        int sizeMapX = 50;
        int sizeMapY = 30;
        int widthPixel = 18;
        int heightPixel = 18;
        bool draw = false;
        int[] MapGenerateApple = new int[0];
        int lengthMapGenerateApple = 0;
        String name = "";
        public Menu menu;
        Brush color;
        ActorType actor;
        Pixel[,] map;
        DispatcherTimer timer = new DispatcherTimer();
        bool snakeMove=false;
        List<Pixel> lastPixel=new List<Pixel>(3);
        TaskMap task;

        public CreateMap(string name,TaskMap task)
        {
            InitializeComponent();
            this.name = name;
            this.task = task;
        }

        void BuildMapGenerateApple(int y, int x)
        {
            
            
            if (map[y, x].type == ActorType.grass)
            {
                map[y, x].type = ActorType.none;
                MapGenerateApple[lengthMapGenerateApple] = y * sizeMapX + x;
                lengthMapGenerateApple++;

                if (x > 0)
                {
                    BuildMapGenerateApple(y, x - 1);
                }
                if (x + 1 < sizeMapX)
                {
                    BuildMapGenerateApple(y, x + 1);
                }
                if (y > 0)
                {
                    BuildMapGenerateApple(y - 1, x);
                }
                if (y + 1 < sizeMapY)
                {
                    BuildMapGenerateApple(y + 1, x);
                }
            }
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            color = ColorActor.wall;
            actor = ActorType.wall;
            B_Grass.Background = ColorActor.grass;
            B_Wall.Background = ColorActor.wall;
            timer.Interval = TimeSpan.Zero;
            timer.Tick += Timer_Tick;
            ParametrsMap parametrs;
            if (TaskMap.Save == task)
            {
                parametrs = Save.GetDefaultMap(0);
                sizeMapY = parametrs.SizeMapY;
                sizeMapX = parametrs.SizeMapX;
                widthPixel = parametrs.WidthPixel;
                heightPixel = parametrs.HeightPixel;
                MapGenerateApple = parametrs.MapGenerateApple;
                map = new Pixel[sizeMapY, sizeMapX];
            }
            else
            {
                parametrs = Save.GetMap(Save.FindMap(name));
                sizeMapY = parametrs.SizeMapY;
                sizeMapX = parametrs.SizeMapX;
                widthPixel = parametrs.WidthPixel;
                heightPixel = parametrs.HeightPixel;
                map = new Pixel[sizeMapY, sizeMapX];
            }
            MapGenerateApple = new int[sizeMapY * sizeMapX];
            


            
            for (int i = 0; i < sizeMapY; i++)
            {
                for (int j = 0; j < sizeMapX; j++)
                {

                    map[i, j] = new Pixel();
                    map[i, j].rectangle.Focusable = false;
                    map[i, j].rectangle.Width = widthPixel;
                    map[i, j].rectangle.Height = heightPixel;
                    map[i, j].rectangle.HorizontalAlignment = HorizontalAlignment.Left;
                    map[i, j].rectangle.VerticalAlignment = VerticalAlignment.Top;
                    switch (parametrs.map[i, j])
                    {
                        case 0:
                            map[i, j].type = ActorType.grass;
                            map[i, j].rectangle.Fill = ColorActor.grass;
                            break;
                        case 1:
                            map[i, j].type = ActorType.wall;
                            map[i, j].rectangle.Fill = ColorActor.wall;
                            break;
                        case 2:
                            map[i, j].type = ActorType.apple;
                            map[i, j].rectangle.Fill = ColorActor.apple;
                            break;
                    }
                    map[i, j].rectangle.Margin = new System.Windows.Thickness(j * heightPixel, i * widthPixel, 0, 0);
                }

            }

            for (int i = 1; i < sizeMapY - 1; i++)
            {
                for (int j = 1; j < sizeMapX - 1; j++)
                {
                    map[i, j].rectangle.MouseMove += Rectangle_MouseMove;
                }
            }

            for (int i = 0; i < sizeMapY; i++)
            {
                for (int j = 0; j < sizeMapX; j++)
                {
                    Grid.Children.Add(map[i, j].rectangle);
                }
            }

           
            SnakeSpawn.Margin=new Thickness(parametrs.LocationSnakeX*widthPixel, parametrs.LocationSnakeY*heightPixel, 0,0);
            for (int q = 0; q < 3; q++)
            {
                lastPixel.Add(map[parametrs.LocationSnakeY + q, parametrs.LocationSnakeX]);               
                lastPixel[q].rectangle.Fill = ColorActor.apple;
                lastPixel[q].rectangle.MouseMove-= Rectangle_MouseMove;
            }
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (draw && !snakeMove)
            {
                ((Rectangle)sender).Fill = color;
                map[((int)Mouse.GetPosition(Grid).Y) / heightPixel, ((int)Mouse.GetPosition(Grid).X) / widthPixel].type = actor;
                label.Content = "y = " + Convert.ToString(((int)Mouse.GetPosition(Grid).Y) / heightPixel) + " x = " + Convert.ToString(((int)Mouse.GetPosition(Grid).X) / widthPixel);
            }
        }

        private void Grid_DrawMouseDown(object sender, MouseButtonEventArgs e)
        {
            draw = true;
            int y = ((int)e.GetPosition(Grid).Y) / heightPixel;
            int x = ((int)e.GetPosition(Grid).X) / widthPixel;
            Rectangle_MouseMove(map[y,x].rectangle, null);
        }

        private void Grid_DrawMouseUp(object sender, MouseButtonEventArgs e)
        {
            draw = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int[,] v = new int[sizeMapY, sizeMapX];
            for (int q = 0; q < sizeMapY; q++)
            {
                for (int w = 0; w < sizeMapX; w++)
                {
                    v[q, w] = (int)(map[q, w].type);
                }
            }
            BuildMapGenerateApple((int)SnakeSpawn.Margin.Top / heightPixel, (int)SnakeSpawn.Margin.Left / widthPixel);
    
            int[] v2 = new int[lengthMapGenerateApple];
            for (int q = 0; q < lengthMapGenerateApple; q++)
            {
                v2[q] = MapGenerateApple[q];
            }
            MapGenerateApple = v2;
            if (task == TaskMap.Save)
            {
                Save.AddSaveMap(name, sizeMapY, sizeMapX, (int)SnakeSpawn.Margin.Top / heightPixel, (int)SnakeSpawn.Margin.Left / widthPixel, heightPixel, widthPixel, v, v2);
                SaveData.Default.Save();
                menu.Update();
                Close();
            }
            else
            {
                SaveData.Default.Map[Save.FindMap(name)] = Save.MapToString(name, sizeMapY, sizeMapX, (int)SnakeSpawn.Margin.Top / heightPixel, (int)SnakeSpawn.Margin.Left / widthPixel, heightPixel, widthPixel, v, v2);
                SaveData.Default.Save();
                Close();
            }
        }


        private void Wall_Click(object sender, RoutedEventArgs e)
        {
            color = ColorActor.wall;
            actor = ActorType.wall;
        }

        private void Grass_Click(object sender, RoutedEventArgs e)
        {
            color = ColorActor.grass;
            actor = ActorType.grass;
        }

        private void SnakeSpawn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            timer.Start();
            snakeMove = true;
        }

        private bool TestSpawn()
        {
            for (int i = 0; i < 3; i++)
            {
                if (!(map[(int)SnakeSpawn.Margin.Top/heightPixel-i+2, (int)SnakeSpawn.Margin.Left / widthPixel].type == ActorType.grass))
                {
                    return false;
                }
            }
            
            return true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                SnakeSpawn.Margin = new Thickness((int)(Mouse.GetPosition(Grid).X/widthPixel)* widthPixel, (int)(Mouse.GetPosition(Grid).Y/heightPixel)* heightPixel, 0, 0);
            }
            else
            {
                if ((int)SnakeSpawn.Margin.Top > 0 && (int)SnakeSpawn.Margin.Top/heightPixel < sizeMapY - 3 && (int)SnakeSpawn.Margin.Left > 0 && (int)SnakeSpawn.Margin.Left/widthPixel < sizeMapX - 1 && TestSpawn())
                {

                    foreach (var a in lastPixel)
                    {
                        a.rectangle.MouseMove += Rectangle_MouseMove;
                        a.rectangle.Fill = ColorActor.grass;
                    }
                    for (int i = 0; i < lastPixel.Count; i++)
                    {
                        lastPixel[i] = map[(int)SnakeSpawn.Margin.Top / heightPixel + i, (int)SnakeSpawn.Margin.Left / widthPixel];
                        lastPixel[i].rectangle.Fill = ColorActor.apple;
                        lastPixel[i].rectangle.MouseMove -= Rectangle_MouseMove;
                    }
                }
                else
                {
                    SnakeSpawn.Margin = new Thickness(lastPixel[0].rectangle.Margin.Left, lastPixel[0].rectangle.Margin.Top, 0, 0);
                }

                snakeMove = false;
                timer.Stop();
            }
        }
    }
}
