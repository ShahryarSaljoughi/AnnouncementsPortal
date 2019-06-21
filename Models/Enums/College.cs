using System;

namespace Models.Enums
{
    public enum College
    {
        Engineering = 1, // مهندسی
        Agriculture = 2, // کشاورزی
        Humanities = 4, // علوم انسانی
        Science = 3, // علوم
        PardisSohrevardi=5, // پردیس سهروردی
    }

    public static partial class EnumUtils
    {
        public static string GetPersianTranslation(this College college)
        {
            switch (college)
            {
                case College.Agriculture:
                    return "دانشکده کشاورزی";
                case College.Engineering:
                    return "دانشکده مهندسی";
                case College.Humanities:
                    return "دانشکده علوم انسانی";
                case College.PardisSohrevardi:
                    return "پردیس سهروردی";
                case College.Science:
                    return "دانشکده علوم";
                default:
                    return "";
            }
        }
    }
}