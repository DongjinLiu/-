using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 超市选址问题
{
    class Program
    {
        public static int count = 0;//居民点数
        public static List<Coord> coord = new List<Coord>();//居民坐标列表
        public static Coord supermarketCoord = new Coord();//超市坐标
        public static int minDistance = 0;//最小距离和
        public static int maxX = 0;//地图横坐标最大值
        public static int maxY = 0;//地图纵坐标最大值
        /// <summary>
        /// Main函数
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("请输入区域横坐标的最大值：");
            maxX = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("请输入区域横坐标的最大值：");
            maxY = Convert.ToInt32(Console.ReadLine());
            Console.Write("请输入居民点数：");
            count =Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                Input();
            }

            GetSupermarketCoord();

            GetMinDistance();

            Console.Write("超市的坐标为：");
            supermarketCoord.Show();

            Console.WriteLine("最小距离之和为："+minDistance.ToString());

            ShowMap(); 

            Console.ReadKey();
        }

        /// <summary>
        /// 获取用户输入
        /// </summary>
        public static void Input()
        {
            Coord c = new Coord();
            string coordString = Console.ReadLine();
            int cut = coordString.IndexOf(",");
            if (cut==-1)
            {
                return;
            }
            c.X = Convert.ToInt32(coordString.Substring(0, cut));
            c.Y = Convert.ToInt32(coordString.Substring(cut + 1));
            coord.Add(c);
        }

        /// <summary>
        /// 获取超市位置坐标
        /// </summary>
        public static void GetSupermarketCoord()
        {
            int[] x = new int[count];
            int[] y = new int[count];
            for (int i = 0; i < count; i++)
            {
                x[i] = coord[i].X;
                y[i] = coord[i].Y;
            }
            supermarketCoord.X = GetMidValue(x);
            supermarketCoord.Y = GetMidValue(y);
        }

        /// <summary>
        /// 求数组的中值
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static int GetMidValue(int[] a)
        {
            int midValue = 0;
            int temp;
            for (int i = 0; i <a.Length-1; i++)
            {
                for (int j = 0; j < a.Length-i-1; j++)
                {
                    if (a[j] > a[j + 1])
                    {
                        temp = a[j];
                        a[j] = a[j + 1];
                        a[j + 1] = temp;
                    }
                }
            }
            if (a.Length % 2 == 0)
            {
                midValue = (a[a.Length / 2 - 1] + a[a.Length / 2])/2;
            }
            else
            {
                midValue = a[a.Length / 2];
            }
            //Console.WriteLine(a[0].ToString()+a[1].ToString()+a[2].ToString()+a[3].ToString()+midValue.ToString());
            return midValue;
        }

        /// <summary>
        /// 获取居民点到超市最短距离之和
        /// </summary>
        /// <returns></returns>
        public static void GetMinDistance()
        {
            for (int i = 0; i < count; i++)
            {
                minDistance += Coord.GetDistance(coord[i], supermarketCoord);
            }
        }

        /// <summary>
        /// 绘制示意地图
        /// </summary>
        public static void ShowMap()
        {
            string[,] map = new string[maxX, maxY];
            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    map[i, j] = "--";
                }
            }
            foreach (var item in coord)
            {
                map[item.X, item.Y] = "０";
            }
            map[supermarketCoord.X, supermarketCoord.Y] = "＠";
            for (int i = 0; i < maxX; i++)
            { 
                for (int j = 0; j < maxY; j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
