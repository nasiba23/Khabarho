using System;
using Khabarho.Utilities;

namespace Khabarho.Extensions
{
    public static class ObjectExtension
    {
        public static void NullCheck(this object obj, string errorMessage)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(null, errorMessage);
            }
        }
    }
}