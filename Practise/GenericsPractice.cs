using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationTracker.Practise
{
    public class Box<T>
    {
        public T Value { get; set; }
        public Box(T value) { Value = value; }
    }
    public static class GenericsPractice
    {
       
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

      
        public static T Max<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) > 0 ? a : b;
        }

        public static void Run()
        {
            Console.WriteLine("--- Generics practice ---");

            int x = 1, y = 2;
            Swap(ref x, ref y);
            Console.WriteLine($"Swap ints: {x}, {y}");

            string s1 = "Ali", s2 = "Omar";
            Swap(ref s1, ref s2);
            Console.WriteLine($"Swap strings: {s1}, {s2}");

            Console.WriteLine("Max(5, 9) = " + Max(5, 9));
            Console.WriteLine("Max(apple, zebra) = " + Max("apple", "zebra"));

            Box<int> b = new Box<int>(10);
            Console.WriteLine("Box<int>: " + b.Value);

            Console.WriteLine("Constraints: 'where T : class, IEntity' on DataStore<T> means T must be a");
            Console.WriteLine("reference type with an Id, so the store can search by Id safely.");
        }
    }
}
