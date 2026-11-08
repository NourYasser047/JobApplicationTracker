using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationTracker.Practise
{
    public static class CollectionsPractice
    {
        public static void Run()
        {
            Console.WriteLine("--- Collections practice ---");

            // List: ordered items, access by index. Used inside DataStore<T>.
            List<string> list = new List<string> { "Edita", "Pharco", "Papia" };
            Console.WriteLine("List: " + string.Join(", ", list));

            // Dictionary: fast lookup by key. Used for jobs by Id.
            Dictionary<int, string> dict = new Dictionary<int, string> { { 1, "Chemist" }, { 2, "HSE Engineer" } };
            Console.WriteLine("Dictionary[2]: " + dict[2]);

            // HashSet: no duplicates. Used for unique emails and (JobId, CandidateId).
            HashSet<string> set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            Console.WriteLine("HashSet add a@x.com: " + set.Add("a@x.com"));
            Console.WriteLine("HashSet add A@X.COM: " + set.Add("A@X.COM") + " (duplicate)");

            // Queue: first in, first out. Fits a line of applications waiting for review.
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("Ahmed"); queue.Enqueue("Sara");
            Console.WriteLine("Queue dequeue: " + queue.Dequeue());

            // Stack: last in, first out. Used for Undo of company status.
            Stack<string> stack = new Stack<string>();
            stack.Push("Pending"); stack.Push("Approved");
            Console.WriteLine("Stack pop: " + stack.Pop());
        }
    }
}
