using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria___5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double total;
            string[] file = File.ReadAllLines("input.txt");
            HashSet<double> invalidIngredients = new HashSet<double>();

            var rangeslist = file.TakeWhile((l) => l != "").Select(x =>
            {
                var separated = x.Split('-');
                return (double.Parse(separated[0]), double.Parse(separated[1]));
            });

            int i = Array.IndexOf(file, "");

            string[] spoiled = new string[file.Length - rangeslist.Count() - 1];
            Array.ConstrainedCopy(file, i + 1, spoiled, 0, spoiled.Length);

            total = 0;

            foreach (string s in spoiled)
            {
                bool fresh = false;
                foreach (var r in rangeslist)
                {
                    if (double.Parse(s) >= r.Item1 && double.Parse(s) <= r.Item2)
                    {
                        fresh = true;
                    }
                }
                if (fresh)
                    total ++;
            }

            Console.WriteLine(total);
        }
    }
}
