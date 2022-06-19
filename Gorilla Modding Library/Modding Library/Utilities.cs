using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modding_Library
{
    public class Utilities
    {
        public static bool ContainsNumber(string input)
        {
            if (input.Any(char.IsDigit))
                return true;
            else
                return false;
        }

        public static bool ContainsLetter(string input)
        {
            if (input.Any(char.IsLetter))
                return true;
            else
                return false;
        }

        public static string GenerateRandomString(int length)
        {
            System.Random random = new System.Random();
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(letters, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
