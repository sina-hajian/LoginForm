using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm.Model
{
    public static class StaticString
    {
        public static readonly string userPlaceHolderText = "نام کاربری";
        public static readonly string passwordPlaceHolderText = "کلمه عبور";
        public static readonly string confirmPassPlaceHolderText = "تکرار کلمه عبور";
        public static readonly string namePlaceHolderText = "نام";
        public static readonly string familyPlaceHolderText = "نام خانوادگی";
        public static readonly string emailPlaceHolderText = "ایمیل";
        public static readonly string phonePlaceHolderText = "شماره تلفن";


        //error

        public static readonly string err_length = "تعداد کمتر از میزان تعیین شده است";
        public static readonly string err_Email = "لطفا یک ایمیل معتبر وارد نمایید";
    }
}
