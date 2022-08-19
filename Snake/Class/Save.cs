using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{

    public static class Save
    {
        public static void AddSaveMap(String Name, int MapSizeY, int MapSizeX, int SnakeLocationY, int SnakeLocationX, int PixelHeight, int PixelWidth, int[,] map, int[] MapGenerateApple)
        {
            String Stats = "";
            Stats = Convert.ToString(Name) + "-" + "MS" + Convert.ToString(MapSizeY) + "," + Convert.ToString(MapSizeX) + "-" + "L" + Convert.ToString(SnakeLocationY) + "," + Convert.ToString(SnakeLocationX) + "-";
            Stats = Stats + "PS" + Convert.ToString(PixelHeight) + "," + Convert.ToString(PixelWidth) + "-";
            StringBuilder l = new StringBuilder();
            l.Length = MapSizeY * MapSizeX;

            for (int q = 0, ind = 0; q < MapSizeY; q++)
            {
                for (int w = 0; w < MapSizeX; w++)
                {
                    l[ind] = (char)(map[q, w] + '0');
                    ind++;
                }
            }
            Stats = Stats + l.ToString() + "-" + "GS" + Convert.ToString(MapGenerateApple.Length) + "-";
            l.Clear();

            for (int q = 0; q < MapGenerateApple.Length; q++)
            {
                l.Insert(l.Length, MapGenerateApple[q] + ",");
            }
            Stats = Stats + l.ToString();
            SaveData.Default.Map.Add(Stats);
        }

        public static String MapToString(String Name, int MapSizeY, int MapSizeX, int SnakeLocationY, int SnakeLocationX, int PixelHeight, int PixelWidth, int[,] map, int[] MapGenerateApple)
        {
            String Stats = "";
            Stats = Convert.ToString(Name) + "-" + "MS" + Convert.ToString(MapSizeY) + "," + Convert.ToString(MapSizeX) + "-" + "L" + Convert.ToString(SnakeLocationY) + "," + Convert.ToString(SnakeLocationX) + "-";
            Stats = Stats + "PS" + Convert.ToString(PixelHeight) + "," + Convert.ToString(PixelWidth) + "-";
            StringBuilder l = new StringBuilder();
            l.Length = MapSizeY * MapSizeX;

            for (int q = 0, ind = 0; q < MapSizeY; q++)
            {
                for (int w = 0; w < MapSizeX; w++)
                {
                    l[ind] = (char)(map[q, w] + '0');
                    ind++;
                }
            }
            Stats = Stats + l.ToString() + "-" + "GS" + Convert.ToString(MapGenerateApple.Length) + "-";
            l.Clear();

            for (int q = 0; q < MapGenerateApple.Length; q++)
            {
                l.Insert(l.Length, MapGenerateApple[q] + ",");
            }
            Stats = Stats + l.ToString();
            return Stats;
        }

        public static String GetDefaultName(int ind)
        {
            String FindName = "";

            for (int q = 0; q < SaveData.Default.DefaultMap[ind].Length; q++)
            {
                if (SaveData.Default.DefaultMap[ind][q] == '-')
                {
                    FindName = SaveData.Default.DefaultMap[ind].Substring(0, q);
                    return FindName;
                }
            }
            return FindName;

        }

        public static String GetName(int ind)
        {
            String FindName = "";

            for (int q = 0; q < SaveData.Default.Map[ind].Length; q++)
            {
                if (SaveData.Default.Map[ind][q] == '-')
                {
                    FindName = SaveData.Default.Map[ind].Substring(0, q);
                    return FindName;
                }
            }
            return FindName;

        }

        public static int FindDefaultMap(String Name)
        {
            for (int q = 0; q < SaveData.Default.DefaultMap.Count; q++)
            {
                if (Name == GetDefaultName(q))
                {
                    return q;
                }
            }
            return -1;
            
        }

        public static int FindMap(String Name)
        {
            String FindName="";
            for (int w = 0; w < SaveData.Default.Map.Count; w++)
            {
                for (int q = 0; q < SaveData.Default.Map[w].Length; q++)
                {
                    if (SaveData.Default.Map[w][q] == '-')
                    {
                        FindName = SaveData.Default.Map[w].Substring(0, q);
                        break;
                    }
                }
                if (FindName == Name)
                {
                    return w;
                }
            }
            return -1;
        }

        public static ParametrsMap GetDefaultMap(int ind)
        {
            return GetMap(SaveData.Default.DefaultMap[ind]);
        }

        public static ParametrsMap GetMap(int ind)
        {
            return GetMap(SaveData.Default.Map[ind]);
        }

        public static ParametrsMap GetMap(String Stats)
        {
            ParametrsMap result = new ParametrsMap();
            //name
            int ind = 0;
            for (int q = 0; q < Stats.Length; q++)
            {
                if (Stats[q] == '-')
                {
                    ind = q;
                    result.Name = Stats.Substring(0, q);
                    break;
                }
            }
            //mapsize
            ind += 3;
            result.SizeMapY = 0;
            for (int q = ind; q < Stats.Length; q++)
            {
                if (Stats[q] >= '0' && Stats[q] <= '9')
                {
                    result.SizeMapY = result.SizeMapY * 10 + Stats[q] - '0';
                }
                else
                {
                    ind = q;
                    break;
                }
            }
            ind++;
            result.SizeMapX = 0;
            for (int q = ind; q < Stats.Length; q++)
            {
                if (Stats[q] >= '0' && Stats[q] <= '9')
                {
                    result.SizeMapX = result.SizeMapX * 10 + Stats[q] - '0';
                }
                else
                {
                    ind = q;
                    break;
                }
            }
            //snakelocation
            ind += 2;
            result.LocationSnakeY = 0;
            for (int q = ind; q < Stats.Length; q++)
            {
                if (Stats[q] >= '0' && Stats[q] <= '9')
                {
                    result.LocationSnakeY = result.LocationSnakeY * 10 + Stats[q] - '0';
                }
                else
                {
                    ind = q;
                    break;
                }
            }
            ind++;
            result.LocationSnakeX = 0;
            for (int q = ind; q < Stats.Length; q++)
            {
                if (Stats[q] >= '0' && Stats[q] <= '9')
                {
                    result.LocationSnakeX = result.LocationSnakeX * 10 + Stats[q] - '0';
                }
                else
                {
                    ind = q;
                    break;
                }
            }
            //pixelsize
            ind += 3;
            result.HeightPixel = 0;
            for (int q = ind; q < Stats.Length; q++)
            {
                if (Stats[q] >= '0' && Stats[q] <= '9')
                {
                    result.HeightPixel = result.HeightPixel * 10 + Stats[q] - '0';
                }
                else
                {
                    ind = q;
                    break;
                }
            }
            ind++;
            result.WidthPixel = 0;
            for (int q = ind; q < Stats.Length; q++)
            {
                if (Stats[q] >= '0' && Stats[q] <= '9')
                {
                    result.WidthPixel = result.WidthPixel * 10 + Stats[q] - '0';
                }
                else
                {
                    ind = q;
                    break;
                }
            }
            //map
            ind++;
            result.map = new int[result.SizeMapY, result.SizeMapX];
            for (int q = 0; q < result.SizeMapY; q++)
            {
                for (int w = 0; w < result.SizeMapX; w++)
                {
                    if (Stats[ind] == '-')
                    {
                        ind++;
                        break;
                    }
                    else
                    {
                        result.map[q, w] = Stats[ind] - '0';
                        ind++;
                    }
                }
            }
            ind += 3;
            //generatemapsize
            int length = 0;
            for (int q = ind; q < Stats.Length; q++)
            {
                if (Stats[q] >= '0' && Stats[q] <= '9')
                {
                    length = length * 10 + Stats[q] - '0';
                }
                else
                {
                    ind = q;
                    break;
                }
            }
            ind++;
            //generatemap
            result.MapGenerateApple = new int[length];
            for (int q = ind, w = 0; q < Stats.Length; q++)
            {
                if (Stats[q] >= '0' && Stats[q] <= '9')
                {
                    result.MapGenerateApple[w] = result.MapGenerateApple[w] * 10 + Stats[q] - '0';

                }
                else
                {
                    w++;
                }
            }
            return result;
        }
    }
}
