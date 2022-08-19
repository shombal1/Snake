using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    //[Serializable]
    public class NameMap
    {

        
        public String[] name=new String[0];
        public ParametrsMap[] maps=new ParametrsMap[0];
    }

    

    public class ParametrsMap
    {
        public string Name;
        public int SizeMapX;
        public int SizeMapY;
        public int WidthPixel;
        public int HeightPixel;
        public int[,] map=new int[0,0];
        public int[] MapGenerateApple=new int[0];
        public int LocationSnakeX;
        public int LocationSnakeY;

    }
}
