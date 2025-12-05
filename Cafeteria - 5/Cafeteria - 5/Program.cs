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
            }).ToArray();

            int i = Array.IndexOf(file, "");

            string[] spoiled = new string[file.Length - rangeslist.Count() - 1];
            Array.ConstrainedCopy(file, i + 1, spoiled, 0, spoiled.Length);

            // Part 1

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
                    total++;
            }

            Console.WriteLine(total);

            // Part 2
            total = 0;

            for (int k = 0; k < rangeslist.Count(); k++) 
            {
                int n = 0;
                foreach (var r2 in rangeslist)
                {
                    if (r2 == (0, 0) || n == k)
                    {
                        n++;
                        continue;
                    } 
                    if (rangeslist.ElementAt(k).Item1 >= r2.Item1 && rangeslist.ElementAt(k).Item1 <= r2.Item2)
                        rangeslist[k].Item1 = r2.Item2 + 1;
                    if (rangeslist.ElementAt(k).Item2 <= r2.Item2 && rangeslist.ElementAt(k).Item2 >= r2.Item1)
                        rangeslist[k].Item2 = r2.Item1 - 1;
                    if (rangeslist.ElementAt(k).Item2 < rangeslist.ElementAt(k).Item1)
                    {
                        rangeslist[k] = (0, 0);
                        break;
                    }

                    n++;
                }

                if (rangeslist.ElementAt(k) != (0,0))
                    total += rangeslist[k].Item2 + 1 - rangeslist[k].Item1;
            }
            
            Console.WriteLine(total);
        }
    }
}

