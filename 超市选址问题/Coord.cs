using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 超市选址问题
{
    public class Coord
    {
        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// 构造函数1
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Coord(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// 构造函数的重载
        /// </summary>
        public Coord()
        {

        }

        /// <summary>
        /// 获取两点之间的距离
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int GetDistance(Coord a, Coord b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        /// <summary>
        /// 输出显示函数
        /// </summary>
        public void Show()
        {
            Console.WriteLine(this.X.ToString() + "," + this.Y.ToString());
        }
    }
}
