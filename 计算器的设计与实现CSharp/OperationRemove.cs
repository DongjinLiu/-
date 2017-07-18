using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 计算器的设计与实现CSharp
{
   public class OperationRemove:Operation//继承
    {
        /// <summary>
        /// 重写父类中的虚方法
        /// </summary>
        /// <returns></returns>
        public override double GetResult()
        {
            if (NumberB!=0.0)
            {
                return NumberA / NumberB;
            }
            else
            {
                return 0;
            }
        }

    }
}
