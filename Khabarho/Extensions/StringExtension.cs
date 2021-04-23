using System;
using Khabarho.Utilities;

namespace Khabarho.Extensions
{
    public static class StringExtension
    {
        public static void NullCheck(this string str, string errorMessage)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException(null, errorMessage);
            }
        }
    }
}