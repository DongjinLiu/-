using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 通讯录管理
{
    public class Person
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string WorkPlace { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Person(int number=0,string name=null,string sex=null,string workPlace=null,string tel=null,string email=null,string address=null)
        {
            this.Number = number;
            this.Name = name;
            this.Sex = sex;
            this.WorkPlace = workPlace;
            this.Tel = tel;
            this.Email = email;
            this.Address = address;
            this.IsDelete = 0;
        }
        public Person()
        {
            this.Number = 0;
        }

        public int IsDelete { get; set; }//0表示未删除，1表示已删除
    }
}
