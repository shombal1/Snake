using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Snake
{
    public delegate void AddApple();
    public delegate void RemoveRow(int ind);

    public enum TaskMap
    {
        Save=0,
        Upgrade=1
    }

    public enum ActorType
    {
        none=-1,
        wall=1,
        apple=2,
        grass=0

    }
    public static class ColorActor
    {
        public static Brush grass = Brushes.MediumSeaGreen;
        public static Brush wall = Brushes.Gray;
        public static Brush apple = Brushes.Red;
        public static Brush HeadSnake = Brushes.Green;
        public static Brush SnakeBrushes = Brushes.LightGreen;
    }


    public class Map
    {
        public int SizeX=0;
        public int SizeY=0;
        public int WidthPixel = 0;
        public int HeightPixel = 0;
        public Brush ColorPixel;
        public Pixel[,] pole;
        public AddApple addApple;
        public int[] MapGenerateApple = new int[0];
        public ParametrsMap parametrsMap;

        public void Load(ParametrsMap parametrs)
        {
            this.HeightPixel = parametrs.HeightPixel;
            this.WidthPixel = parametrs.WidthPixel;
            for (int q = 0; q < parametrs.map.GetLength(0); q++)
            {
                for (int w = 0; w < parametrs.map.GetLength(1); w++)
                {
                    pole[q,w].rectangle.Width = WidthPixel;
                    pole[q, w].rectangle.Height = HeightPixel;
                    pole[q, w].rectangle.Fill = ColorPixel;
                    switch (parametrs.map[q, w])
                    {
                        case 0:
                            pole[q, w].type = ActorType.grass;
                            break;
                        case 1:
                            pole[q, w].type = ActorType.wall;
                            pole[q, w].rectangle.Fill = Brushes.Gray;
                            break;
                        case 2:
                            pole[q, w].type = ActorType.apple;
                            pole[q, w].rectangle.Fill = Brushes.Red;
                            break;
                    }
                    pole[q, w].rectangle.Margin = new System.Windows.Thickness(w * HeightPixel, q * WidthPixel, 0, 0);
                }
            }
        }

        public void Restart()
        {

            for (int i = 0; i < SizeY; i++)
            {
                for (int j = 0; j < SizeX; j++)
                {
                    switch (parametrsMap.map[i, j])
                    {
                        case 0:
                            pole[i, j].type = ActorType.grass;
                            pole[i, j].rectangle.Fill = ColorActor.grass;
                            break;
                        case 1:
                            pole[i, j].type = ActorType.wall;
                            pole[i, j].rectangle.Fill = ColorActor.wall;
                            break;
                        case 2:
                            pole[i, j].type = ActorType.apple;
                            pole[i, j].rectangle.Fill = ColorActor.apple;
                            break;
                    }
                    pole[i, j].rectangle.Margin = new System.Windows.Thickness(j * HeightPixel, i * WidthPixel, 0, 0);
                }

            }



        }

        public Map(ParametrsMap parametrs,AddApple addApple)
        {
            SizeX = parametrs.map.GetLength(1);
            SizeY= parametrs.map.GetLength(0);
            this.HeightPixel= parametrs.HeightPixel;
            this.WidthPixel= parametrs.WidthPixel;
            this.addApple = addApple;
            this.MapGenerateApple = parametrs.MapGenerateApple;
            this.parametrsMap = parametrs;
            pole = new Pixel[SizeY,SizeX];

            for (int i = 0; i < SizeY; i++)
            {
                for (int j = 0; j < SizeX; j++)
                {

                    pole[i, j] = new Pixel();
                    pole[i, j].rectangle.Focusable = false;
                    pole[i, j].rectangle.Width = WidthPixel;
                    pole[i, j].rectangle.Height = HeightPixel;
                    pole[i, j].rectangle.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    pole[i, j].rectangle.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    switch (parametrs.map[i, j])
                    {
                        case 0:
                            pole[i, j].type = ActorType.grass;
                            pole[i, j].rectangle.Fill = ColorActor.grass;
                            break;
                        case 1:
                            pole[i, j].type = ActorType.wall;
                            pole[i, j].rectangle.Fill = ColorActor.wall;
                            break;
                        case 2:
                            pole[i, j].type = ActorType.apple;
                            pole[i, j].rectangle.Fill = ColorActor.apple;
                            break;
                    }
                    pole[i, j].rectangle.Margin=new System.Windows.Thickness(j*HeightPixel,i*WidthPixel,0,0);
                }

            }
        }
        public void AddToGrid(Grid grid)
        {
            for (int i = 0; i < SizeY; i++)
            {
                for (int j = 0; j < SizeX; j++)
                {
                    grid.Children.Add(pole[i, j].rectangle);
                }
            }
        }

        public void SetBrushPixel(int y, int x,Brush brush)
        {
            pole[y, x].rectangle.Fill = brush;
        }

        public void NewApple(int y,int x)
        {
            pole[y,x].type = ActorType.apple;
            pole[y, x].rectangle.Fill = Brushes.Red;
            
        }

    }
}
