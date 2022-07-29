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



    public enum EnumActor
    {
        wall=1,
        apple=2,
        grass=0

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

        public void Restart(int WidthPixel, int HeightPixel, Brush ColorPixel, AddApple addApple)
        {
           
            this.HeightPixel = HeightPixel;
            this.WidthPixel = WidthPixel;
            this.ColorPixel = ColorPixel;
            this.addApple = addApple;
            for (int i = 0; i < SizeY; i++)
            {
                for (int j = 0; j < SizeX; j++)
                {
                    
                    pole[i, j].rectangle.Width = WidthPixel;
                    pole[i, j].rectangle.Height = HeightPixel;
                    pole[i, j].rectangle.Fill = ColorPixel;
                    pole[i, j].type = EnumActor.grass;
                    pole[i, j].rectangle.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    pole[i, j].rectangle.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    pole[i, j].rectangle.Margin = new System.Windows.Thickness(j * HeightPixel, i * WidthPixel, 0, 0);
                }

            }



        }

        public Map(int SizeMapX,int SizeMapY,int WidthPixel,int HeightPixel,Brush ColorPixel,AddApple addApple)
        {
            
            SizeX = SizeMapX;
            SizeY= SizeMapY;
            this.HeightPixel=HeightPixel;
            this.WidthPixel=WidthPixel;
            this.ColorPixel=ColorPixel;
            this.addApple = addApple;

            pole = new Pixel[SizeMapY,SizeMapX];
            for (int i = 0; i < SizeMapY; i++)
            {
                for (int j = 0; j < SizeMapX; j++)
                {
                    pole[i, j] = new Pixel();
                    pole[i, j].rectangle.Focusable = false;
                    pole[i, j].rectangle.Width = WidthPixel;
                    pole[i, j].rectangle.Height = HeightPixel;
                    pole[i, j].rectangle.Fill = ColorPixel;
                    pole[i, j].rectangle.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    pole[i, j].rectangle.VerticalAlignment = System.Windows.VerticalAlignment.Top;
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

        public void CreateWall(Brush BrushesWall)
        {
            for (int i = 0; i < SizeY; i++)
            {
                pole[i, 0].rectangle.Fill = BrushesWall;
                pole[i, 0].type = EnumActor.wall;
            }
            for (int i = 0; i < SizeY; i++)
            {
                pole[i, SizeX-1].rectangle.Fill = BrushesWall;
                pole[i, SizeX - 1].type = EnumActor.wall;
            }
            for (int i = 1; i < SizeX-1; i++)
            {
                pole[0, i].rectangle.Fill = BrushesWall;
                pole[0, i].type = EnumActor.wall;
            }
            for (int i = 1; i < SizeX - 1; i++)
            {
                pole[SizeY-1, i].rectangle.Fill = BrushesWall;
                pole[SizeY - 1, i].type = EnumActor.wall;

            }
        }

        public void NewApple(int y,int x)
        {
            pole[y,x].type = EnumActor.apple;
            pole[y, x].rectangle.Fill = Brushes.Red;
        }

    }
}
