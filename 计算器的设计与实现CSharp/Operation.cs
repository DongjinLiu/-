using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 计算器的设计与实现CSharp
{
    public class Operation:Form1
    {
        //定义属性
        public double NumberA { get; set; }
        public double NumberB { get; set; }

        /// <summary>
        /// 虚方法，可以被重写
        /// </summary>
        /// <returns></returns>
        public virtual double GetResult()
        {
            double result = 0;
            return result;
        }

    }
}
