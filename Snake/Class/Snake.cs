using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Snake
{

    public delegate void HitWall();
    public class Snakes
    {
        private Map map;
        public int length;
        private int StartX;
        private int StartY;
        public Point Head=new Point();
        public List<Pixel> snake = new List<Pixel>(0);
        public Brush SnakeHeadBrushes;
        public Brush SnakeBrushes;
        HitWall hitWall;

        public void Restart(Map map, int StartY, int StartX, int length, Brush SnakeBrushes, Brush SnakeHeadBrushes, HitWall hitWall)
        {
            snake.Clear();
            this.map = map;
            this.length = length;
            this.StartX = StartX;
            this.StartY = StartY;
            Head.X = StartX;
            Head.Y = StartY;
            this.SnakeHeadBrushes = SnakeHeadBrushes;
            this.SnakeBrushes = SnakeBrushes;
            this.hitWall = hitWall;


            for (int i = 0; i < length; i++)
            {
                snake.Add(map.pole[StartY + i, StartX]);
            }
            foreach (var a in snake)
            {
                a.rectangle.Fill = SnakeBrushes;
                a.type = ActorType.wall;
            }
            snake[0].rectangle.Fill = SnakeHeadBrushes;
        }

        public Snakes(Map map,int StartY, int StartX, int length,Brush SnakeBrushes, Brush SnakeHeadBrushes,HitWall hitWall)
        {
            this.map = map;
            this.length = length;
            this.StartX = StartX;
            this.StartY = StartY;
            Head.X=StartX;
            Head.Y=StartY;
            this.SnakeHeadBrushes=SnakeHeadBrushes;
            this.SnakeBrushes=SnakeBrushes;
            this.hitWall=hitWall;


            for (int i = 0; i < length; i++)
            {
                snake.Add(map.pole[StartY+i, StartX]);
            }
            foreach (var a in snake)
            {
                a.rectangle.Fill = SnakeBrushes;
                a.type = ActorType.wall;
            }
            snake[0].rectangle.Fill = SnakeHeadBrushes;
        }

        public bool ReturnMoveUP()
        {
            if (map.pole[(int)Head.Y-1,(int)Head.X] == snake[1])
                return true;
            else
                return false;
   
        }
        public bool ReturnMoveDown()
        {
            if (map.pole[(int)Head.Y + 1, (int)Head.X] == snake[1])
                return true;
            else
                return false;

        }
        public bool ReturnMoveLeft()
        {
            if (map.pole[(int)Head.Y, (int)Head.X-1] == snake[1])
                return true;
            else
                return false;

        }
        public bool ReturnMoveRigts()
        {
            if (map.pole[(int)Head.Y, (int)Head.X + 1] == snake[1])
                return true;
            else
                return false;

        }



        public void moveUp()
        { 
            Head.Y =Head.Y- 1;

            switch (map.pole[(int)Head.Y, (int)Head.X].type)
            {
                case ActorType.grass:
                    snake.Insert(0, map.pole[(int)Head.Y, (int)Head.X]);
                    snake[0].rectangle.Fill = SnakeHeadBrushes;
                    snake[0].type=ActorType.wall;
                    snake[1].rectangle.Fill = SnakeBrushes;
                    snake[snake.Count - 1].rectangle.Fill = ColorActor.grass;
                    snake[snake.Count - 1].type =ActorType.grass ;
                    snake.RemoveAt(snake.Count - 1);
                    break;
                case ActorType.wall:
                    hitWall();
                    break;
                case ActorType.apple:
                    snake.Insert(0, map.pole[(int)Head.Y, (int)Head.X]);
                    snake[0].rectangle.Fill = SnakeHeadBrushes;
                    snake[0].type = ActorType.wall;
                    snake[1].rectangle.Fill = SnakeBrushes;
                    length++;
                    map.addApple();
                    break;
            }

 
        }
        public void moveDown()
        {
            Head.Y = Head.Y + 1;

            switch (map.pole[(int)Head.Y, (int)Head.X].type)
            {
                case ActorType.grass:
                    snake.Insert(0, map.pole[(int)Head.Y, (int)Head.X]);
                    snake[0].rectangle.Fill = SnakeHeadBrushes;
                    snake[0].type = ActorType.wall;
                    snake[1].rectangle.Fill = SnakeBrushes;
                    snake[snake.Count - 1].rectangle.Fill = ColorActor.grass;
                    snake[snake.Count - 1].type = ActorType.grass;
                    snake.RemoveAt(snake.Count - 1);
                    break;
                case ActorType.wall:
                    hitWall();
                    break;
                case ActorType.apple:
                    snake.Insert(0, map.pole[(int)Head.Y, (int)Head.X]);
                    snake[0].rectangle.Fill = SnakeHeadBrushes;
                    snake[0].type = ActorType.wall;
                    snake[1].rectangle.Fill = SnakeBrushes;
                    length++;
                    map.addApple();
                    break;
            }
        }
        public void moveLeft()
        {
            Head.X = Head.X - 1;

            switch (map.pole[(int)Head.Y, (int)Head.X].type)
            {
                case ActorType.grass:
                    snake.Insert(0, map.pole[(int)Head.Y, (int)Head.X]);
                    snake[0].rectangle.Fill = SnakeHeadBrushes;
                    snake[0].type = ActorType.wall;
                    snake[1].rectangle.Fill = SnakeBrushes;
                    snake[snake.Count - 1].rectangle.Fill = ColorActor.grass;
                    snake[snake.Count - 1].type = ActorType.grass;
                    snake.RemoveAt(snake.Count - 1);
                    break;
                case ActorType.wall:
                    hitWall();
                    break;
                case ActorType.apple:
                    snake.Insert(0, map.pole[(int)Head.Y, (int)Head.X]);
                    snake[0].rectangle.Fill = SnakeHeadBrushes;
                    snake[0].type = ActorType.wall;
                    snake[1].rectangle.Fill = SnakeBrushes;
                    length++;
                    map.addApple();
                    break;
            }
        }
        public void moveRigts()
        {
            Head.X = Head.X + 1;

            switch (map.pole[(int)Head.Y, (int)Head.X].type)
            {
                case ActorType.grass:
                    snake.Insert(0, map.pole[(int)Head.Y, (int)Head.X]);
                    snake[0].rectangle.Fill = SnakeHeadBrushes;
                    snake[0].type = ActorType.wall;
                    snake[1].rectangle.Fill = SnakeBrushes;
                    snake[snake.Count - 1].rectangle.Fill = ColorActor.grass;
                    snake[snake.Count - 1].type = ActorType.grass;
                    snake.RemoveAt(snake.Count - 1);
                    break;
                case ActorType.wall:
                    hitWall();
                    break;
                case ActorType.apple:
                    snake.Insert(0, map.pole[(int)Head.Y, (int)Head.X]);
                    snake[0].rectangle.Fill = SnakeHeadBrushes;
                    snake[0].type = ActorType.wall;
                    snake[1].rectangle.Fill = SnakeBrushes;
                    length++;
                    map.addApple();
                    break;
            }

        }


    }
}
