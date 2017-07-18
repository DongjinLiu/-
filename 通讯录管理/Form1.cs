using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 通讯录管理
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Person> person = new List<Person>();

        #region 文件读写模块
        /// <summary>
        /// 读取txt中的文件
        /// </summary>
        void Read()
        {
            person.Clear();
            string[] sReadPerson = File.ReadAllLines(@"通讯录.txt", Encoding.Default);//使用相对路径
            int location1 = 0;
            int location2 = 0;
            string item = "";
            string[] str = new string[8];
            for (int j = 0; j < sReadPerson.Length; j++)
            {
                Person p = new Person();
                location1 = 0;
                location2 = 0;
                item = sReadPerson[j];
                for (int i = 0; i < 8; i++)
                {
                    location1 = item.IndexOf("*", location2);
                    location2 = item.IndexOf("*", location1 + 1);
                    str[i] = item.Substring(location1 + 1, location2 - location1 - 1);
                    // MessageBox.Show("j:"+j.ToString()+"i:"+i.ToString());
                }
                p.Number = Convert.ToInt32(str[0]);
                p.Name = str[1];
                p.Sex = str[2];
                p.WorkPlace = str[3];
                p.Tel = str[4];
                p.Email = str[5];
                p.Address = str[6];
                p.IsDelete = Convert.ToInt32(str[7]);
                person.Add(p);
            }

        }
        /// <summary>
        /// 向txt中写入文件
        /// </summary>
        void Write()
        {
            using (FileStream fsWrite = new FileStream(@"通讯录.txt", FileMode.Create, FileAccess.Write))
            {
                foreach (var p in person)
                {
                    string personMessage = "*" + p.Number.ToString() + "*" + p.Name + "*" + p.Sex + "*" + p.WorkPlace + "*" + p.Tel + "*" + p.Email + "*" + p.Address + "*" + p.IsDelete + "*" + "\r\n";
                    byte[] buffer = Encoding.Default.GetBytes(personMessage);
                    fsWrite.Position = fsWrite.Length;
                    fsWrite.Write(buffer, 0, buffer.Length);
                }
            }
        }
        #endregion

        #region 添加模块

        /// <summary>
        /// 保存记录
        /// </summary>
        /// <param name="p">要保存的对象</param>
        /// <param name="i">要保存对象的编号</param>
        void Save(Person p)
        {
            //MessageBox.Show(i.ToString());
            //person[i] = new Person(Convert.ToInt32(p.Number), p.Name, p.Sex, p.WorkPlace, p.Tel, p.Email, p.Address);
            person.Add(p);
            Write();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Person person1 = new Person(Convert.ToInt32(txtNumber2.Text), txtName2.Text, txtSex2.Text, txtWorkPlace2.Text, txtTel2.Text, txtEmail2.Text, txtAddress2.Text);
            foreach (var p in person)
            {
                if (p.Number==person1.Number&&p.IsDelete==0)
                {
                    MessageBox.Show("已存在学号为" + p.Number.ToString() + "的记录");
                    ClearBox();
                    return;
                }
            }
            Save(person1);
            MessageBox.Show("保存成功！");
            ClearBox();
        }
        #endregion

        Person findPerson = new Person();
        #region 搜索模块
        /// <summary>
        /// 学号搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            findPerson = FindByNumber(Convert.ToInt32(txtNumber0.Text));
            XianShiFind();
        }

        /// <summary>
        /// 姓名搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            findPerson = FindByName(txtName0.Text);
            XianShiFind();
        }

        /// <summary>
        /// 电话搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            findPerson = FindByTel(txtTel0.Text);
            XianShiFind();
        }

        Person FindByNumber(int number)
        {
            Person findPerson = new Person();
            foreach (var item in person)
            {
                if (item.Number == number && item.IsDelete == 0)
                {
                    findPerson = item;
                }
            }
            return findPerson;
        }

        Person FindByName(string name)
        {
            Person findPerson = new Person();
            foreach (var item in person)
            {
                if (item.Name.Contains(name)&& item.IsDelete == 0)
                {
                    findPerson = item;
                }
            }
            return findPerson;
        }

        Person FindByTel(string tel)
        {
            Person findPerson = new Person();
            foreach (var item in person)
            {
                if (item.Tel == tel && item.IsDelete == 0)
                {
                    findPerson = item;
                }
            }
            return findPerson;
        }

        /// <summary>
        /// 显示搜索结果
        /// </summary>
        void XianShiFind()
        {
            if (findPerson.Number != 0)
            {
                txtNumber1.Text = findPerson.Number.ToString();
                txtName1.Text = findPerson.Name;
                txtSex1.Text = findPerson.Sex;
                txtWorkPlace1.Text = findPerson.WorkPlace;
                txtTel1.Text = findPerson.Tel;
                txtEmail1.Text = findPerson.Email;
                txtAddress1.Text = findPerson.Address;
            }
            else
            {
                txtNumber1.Text = "";
                txtName1.Text = "";
                txtSex1.Text = "";
                txtWorkPlace1.Text = "";
                txtTel1.Text = "";
                txtEmail1.Text = "";
                txtAddress1.Text = "";
                MessageBox.Show("未找到与该关键字匹配的记录！");
            }
        }

        #endregion

        #region 修改模块
        private void btnSaveChange_Click(object sender, EventArgs e)
        {
            findPerson.Number = Convert.ToInt32(txtNumber1.Text);
            findPerson.Name = txtName1.Text;
            findPerson.Sex = txtSex1.Text;
            findPerson.WorkPlace = txtWorkPlace1.Text;
            findPerson.Tel = txtTel1.Text;
            findPerson.Email = txtEmail1.Text;
            findPerson.Address = txtAddress1.Text;
            findPerson.IsDelete = 0;

            Write();

            MessageBox.Show("保存成功！");
            ClearBox();
        }
        #endregion

        #region 删除模块
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确实要删除该记录？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                findPerson.IsDelete = 1;
                Write();
                ClearBox();
            }
        }
        #endregion

        #region 显示所有记录
        void View()
        {
            Read();
            listBox1.Items.Clear();
            int allCount = 0;
            int boyCount = 0;
            int girlCount = 0;
            GetCount(ref allCount, ref boyCount,ref girlCount);
            listBox1.Items.Add("男生：" + boyCount + "个\r\t女生：" + girlCount + "个\r\t总计：" + allCount);
            foreach (var p in person)
            {
                if (p.IsDelete == 0)
                {
                    listBox1.Items.Add("学号："+p.Number.ToString() + "\r\t" + "姓名："+p.Name + "\r\t" +"性别："+ p.Sex + "\r\t" +"工作单位："+ p.WorkPlace + "\r\t" +"电话号码："+ p.Tel + "\r\t" +"电子邮箱："+ p.Email + "\r\t" +"通讯地址："+ p.Address);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            View();
        }
        private void tabControl1_Click(object sender, EventArgs e)
        {
            View();
        }
        #endregion

        /// <summary>
        /// 清空文本框
        /// </summary>
        void ClearBox()
        {
            txtNumber1.Text = "";
            txtName1.Text = "";
            txtSex1.Text = "";
            txtWorkPlace1.Text = "";
            txtTel1.Text = "";
            txtEmail1.Text = "";
            txtAddress1.Text = "";

            txtNumber2.Text = "";
            txtName2.Text = "";
            txtSex2.Text = "";
            txtWorkPlace2.Text = "";
            txtTel2.Text = "";
            txtEmail2.Text = "";
            txtAddress2.Text = "";
        }
        /// <summary>
        /// 统计记录
        /// </summary>
        /// <param name="allCount">总人数</param>
        /// <param name="boyCount">男生数</param>
        /// <param name="girlCount">女生数</param>
        void GetCount(ref int allCount,ref int boyCount,ref int girlCount)
        {
            foreach (var item in person)
            {
                if (item.IsDelete==0)
                {
                    allCount++;
                    if (item.Sex == "男")
                    {
                        boyCount++;
                    }
                    else if(item.Sex=="女")
                    {
                        girlCount++;
                    }
                    else
                    {
                        MessageBox.Show("发现未知性别！");
                        break;
                    }
                }
            }
        }
    }
}
