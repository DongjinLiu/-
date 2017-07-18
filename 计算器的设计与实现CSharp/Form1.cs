/*
 * 作者：刘东锦
 * 日期：2017/1/3
 * 功能：接收数字键和功能键输入，完成能判断运算优先级的四则运算
 * 需要完善的功能：
 * 1.表达式能直接使用键盘输入
 * 2.加上（）运算的优先级判定
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 计算器的设计与实现CSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string input="";
        /// <summary>
        /// 监听输入函数
        /// </summary>
        /// <param name="c">输入的字符</param>
        void Input(char c)
        {
            input += c;
        }
        /// <summary>
        /// 显示函数
        /// 更新文本框中的信息
        /// </summary>
        void ShowMessage()
        {
            txtBox.Text = input;
        }

        //使用region表示代码块，对代码逻辑无影响
        #region 监听输入
        private void btn1_Click(object sender, EventArgs e)
        {
            Input('1');
            ShowMessage();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            Input('2');
            ShowMessage();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            Input('3');
            ShowMessage();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            Input('4');
            ShowMessage();
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            Input('5');
            ShowMessage();
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            Input('6');
            ShowMessage();
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            Input('7');
            ShowMessage();
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            Input('8');
            ShowMessage();
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            Input('9');
            ShowMessage();
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            Input('0');
            ShowMessage();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Input('+');
            ShowMessage();
        }

        private void btnSubtruct_Click(object sender, EventArgs e)
        {
            Input('-');
            ShowMessage();
        }

        private void btnRide_Click(object sender, EventArgs e)
        {
            Input('x');
            ShowMessage();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Input('÷');
            ShowMessage();
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            Input('.');
            ShowMessage();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            input = "";
            txtBox.Text = "";
            ShowMessage();
            txtBox.ReadOnly = false;
            btnEqual.Enabled = true;
        }
        #endregion

        /// <summary>
        /// 判断运算优先级
        /// </summary>
        /// <param name="sign"></param>
        /// <returns></returns>
        private int GetSignYouXianJi(char sign)  // get sign priority
        {
            switch (sign)
            {
                case '+':
                case '-':
                    return 1;//加减运算的优先级为1
                case 'x':
                case '÷':
                    return 2;//乘除运算的优先级为2
            }
            return -1;
        }
        
        /// <summary>
        /// 分析后缀表达式
        /// </summary>
        /// <param name="obj">用户输入的信息</param>
        /// <returns></returns>
        private char HouZhuiBiaoDaShi(string obj)
        {
            if (obj == "+")
            {
                return '+';
            }
            else if (obj == "-")
            {
                return '-';
            }
            else if (obj == "x")
            {
                return 'x';
            }
            else if (obj == "÷")
            {
                return '÷';
            }
            else                
            {
                return '0';
            }
        }


        /// <summary>
        /// 计算表达式的结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEqual_Click(object sender, EventArgs e)
        {
            btnEqual.Enabled = false;//避免多次输入"="带来的异常

            Stack<double> tempStack = new Stack<double>();                  // 运算栈
            Queue<string> houZhuiBiaoDaShiQueue = new Queue<string>();     // 保存后缀表达式的队列
            Stack<char> signStack = new Stack<char>();                      // 运算符栈

            string tempStr = "";                                            // 临时记录输入的数字或小数点
            int objType;
            double tempDouble;

            try
            {
                #region 中缀表达式转后缀表达式
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] <= '9' && input[i] >= '0' || input[i] == '.')  //找出运算数
                    {
                        tempStr += input[i];
                    }
                    else
                    {
                        if (tempStr.Length > 0)             //如果符号前存在数字，则将数字添加到houZhuiBiaoDaShiQueue队尾
                        {
                            houZhuiBiaoDaShiQueue.Enqueue(tempStr);
                            tempStr = "";
                        }
                        if (signStack.Count == 0)           //运算符栈为空，该运算符直接入栈
                        {
                            signStack.Push(input[i]);
                        }
                        else                                //运算符栈不为空，需要判断运算符的优先级
                        {
                            #region 判断运算优先级
                            if (GetSignYouXianJi(input[i]) > GetSignYouXianJi(signStack.Peek()))
                            {
                                signStack.Push(input[i]);
                            }
                            else
                            {
                                while (true)
                                {
                                    houZhuiBiaoDaShiQueue.Enqueue(Convert.ToString(signStack.Pop()));
                                    if (signStack.Count == 0 || GetSignYouXianJi(input[i]) > GetSignYouXianJi(signStack.Peek()))
                                        break;
                                }
                                signStack.Push(input[i]);
                            }
                            #endregion
                        }
                    }
                }   // end for

                if (tempStr.Length > 0)   //将最后一个运算数添加到houZhuiBiaoDaShiQueue队尾
                {
                    houZhuiBiaoDaShiQueue.Enqueue(tempStr);
                    tempStr = "";
                }
                while (signStack.Count > 0)//将所有运算符依次添加到队尾，形成后缀表达式
                {
                    houZhuiBiaoDaShiQueue.Enqueue(Convert.ToString(signStack.Pop()));
                }
                #endregion

                signStack.Clear();
                tempStr = "";

                #region 计算后缀表达式

                while (houZhuiBiaoDaShiQueue.Count > 0)
                {
                    objType = HouZhuiBiaoDaShi(houZhuiBiaoDaShiQueue.Peek());
                    switch (objType)
                    {
                        case '0':                 // 如果是运算数，则直接入栈（并在后缀表达式队列中移除该运算数）
                            tempStack.Push(Convert.ToDouble(houZhuiBiaoDaShiQueue.Dequeue()));
                            break;
                        case '+':
                            houZhuiBiaoDaShiQueue.Dequeue();//在后缀表达式队列中移除该运算符
                            tempStack.Push(tempStack.Pop() + tempStack.Pop());
                            break;
                        case '-':
                            houZhuiBiaoDaShiQueue.Dequeue();
                            tempDouble = tempStack.Pop();//提取出减数
                            tempStack.Push(tempStack.Pop() - tempDouble);
                            break;
                        case 'x':
                            houZhuiBiaoDaShiQueue.Dequeue();
                            tempStack.Push(tempStack.Pop() * tempStack.Pop());
                            break;
                        case '÷':
                            houZhuiBiaoDaShiQueue.Dequeue();
                            tempDouble = tempStack.Pop();//提取出除数
                            if (tempDouble != 0)
                            {
                                tempStack.Push(tempStack.Pop() / tempDouble);
                            }
                            else
                            {
                                MessageBox.Show("Error: 0是被除数！");
                            }
                            break;
                        default:
                            MessageBox.Show("未知错误！");
                            break;
                    }
                }
                #endregion

                input += "=" + Convert.ToString(tempStack.Pop());

                ShowMessage();
                txtBox.ReadOnly = true;
            }
            catch (Exception)
            {
                MessageBox.Show("请输入正确的运算式！");
                throw;
            }
        }
    }
}
