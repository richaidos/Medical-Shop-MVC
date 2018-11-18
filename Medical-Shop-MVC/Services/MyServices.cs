using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical_Shop_MVC.Services
{
    public class MyServices
    {
        public int GetIntStr(String a) {
            return Int32.Parse(a);
        }
        public String GetStringInt(int a) {
            return a.ToString();
        }
    }
}
