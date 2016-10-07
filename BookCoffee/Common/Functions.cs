using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookCoffee.Common
{
    public static class Functions
    {
        public static string left(string inString, int inInt)
        {
            if (inInt > inString.Length)
                inInt = inString.Length;
            return inString.Substring(0, inInt);
        }
        public static string Right(string str, int length)
        {
            return str.Substring(str.Length - length, length);
        }
        public static string Mid(string param, int startIndex, int length)
        {
            string result = param.Substring(startIndex - 1, length);
            return result;
        }


    }
}