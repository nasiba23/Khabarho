using System;
using System.Collections.Generic;
using System.Linq;
using Khabarho.Utilities;

namespace Khabarho.Extensions
{
    public static class ListExtension
    {
        public static void NullCheck<T>(this List<T> list, string errorMessage)
        {
            if (!list.Any())
            {
                throw new ArgumentNullException(null, errorMessage);
            }
        }
    }
}