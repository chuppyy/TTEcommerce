using System;
using System.Linq;

namespace TTEcommerce.Domain.Core
{
    public static class StrHelper
    {
        private static readonly Random _rnd = new Random();

        public static string GenRndStr(int len = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, len)
                .Select(s => s[_rnd.Next(s.Length)]).ToArray());
        }
    }
}
