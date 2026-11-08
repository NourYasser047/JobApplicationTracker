using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationTracker.Services
{
    public static class Input
    {
        public static string Text(string prompt)
        {
            Console.Write(prompt);
            string s = Console.ReadLine();
            return s == null ? "" : s.Trim();
        }

        public static bool TryInt(string prompt, out int value)
        {
            if (int.TryParse(Text(prompt), out value)) return true;
            Console.WriteLine("Invalid input: please type a whole number.");
            return false;
        }

        public static bool TryDecimal(string prompt, out decimal value)
        {
            if (decimal.TryParse(Text(prompt), out value)) return true;
            Console.WriteLine("Invalid input: please type a number.");
            return false;
        }
    }
}
