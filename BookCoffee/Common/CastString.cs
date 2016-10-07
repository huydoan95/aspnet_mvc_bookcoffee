using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace BookCoffee.Common
{
    public static class CastString
    {
        public static string Cast(string s)
        {
            for (int i = 33; i < 48; i++)
            {
                s = s.Replace(((char)i).ToString(), "");
            }

            for (int i = 58; i < 65; i++)
            {
                s = s.Replace(((char)i).ToString(), "");
            }

            for (int i = 65; i < 91; i++)
            {
                s = s.Replace(((char)i).ToString(), ((char)(i+32)).ToString());
            }

            for (int i = 91; i < 97; i++)
            {
                s = s.Replace(((char)i).ToString(), "");
            }
            for (int i = 123; i < 127; i++)
            {
                s = s.Replace(((char)i).ToString(), "");
            }
            s = s.Replace(" ", "-");
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}