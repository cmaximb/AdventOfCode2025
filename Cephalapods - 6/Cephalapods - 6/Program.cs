using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cephalapods___6
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            string[] file = File.ReadAllLines("input.txt");
            string[] separators = new string[] { " ", "  ", "   " };
            double[] line_split = file[0].Split(separators, StringSplitOptions.RemoveEmptyEntries).Select(x => double.Parse(x)).ToArray();
            string[] operations = file[file.Length - 1].Split(separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in file.Skip(1)) { 
                if ("*+".Contains(line[0])) break;
                int[] adders = line.Split(separators, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

                line_split = line_split.Select((x, c) => {
                    if (operations[c] == "*")
                        x *= adders[c];
                    else if (operations[c] == "+")
                        x += adders[c];
                    return x;
                } ).ToArray();
            }

            double total = 0;
            foreach (var count in line_split)
                total += count;

            Console.WriteLine(total);
        }
    }
}
