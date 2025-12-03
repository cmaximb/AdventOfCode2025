using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace lobby
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] file = File.ReadAllLines("Input.txt");
            Console.WriteLine("Part1: " + Part1(file));
            Console.WriteLine("Part2: " + Part2(file));
        }

        static string Part1(string[] file) 
        { 
            int output = 0;

            foreach (string l in file) {
                int highest = -1;
                int sechighest = -1;
                int i = 0, p1 = 0, p2 = 0;

                foreach (char c in l)
                {
                    int d = int.Parse($"{c}");

                    if (d > highest)
                    {
                        sechighest = highest;
                        highest = d;
                        p2 = p1;
                        p1 = i;
                    }
                    else if (d > sechighest)
                    {
                        sechighest = d;
                        p2 = i;
                    }

                    i += 1;
                }

                if (p2 < p1 && p1 != l.Length - 1)
                {
                    sechighest = 0;
                    for (int j = p1 + 1; j < l.Length; j++)
                    {
                        if (int.Parse($"{l[j]}") > sechighest)
                        {
                            sechighest = int.Parse($"{l[j]}");
                            p2 = j;
                        }
                    }
                }

                if (p2 > p1)
                    output += highest * 10 + sechighest;
                else
                    output += sechighest * 10 + highest;
            }
            
            return output.ToString();
        }

        static string Part2(string[] File)
        {
            double output = 0;

            foreach (string l in File)
            {
                double tot = 0;
                int p = -1;

                for (int j = 12; j >= 0; j--)
                {  
                    var highest = 0;
                    int d = p;
                    p = -1;

                    for (int i = d + 1; i < l.Length - j; i++)
                    {
                        if (int.Parse(l[i].ToString()) > highest)
                        {
                            highest = int.Parse(l[i].ToString());
                            p = i;
                        }
                    }

                    tot *= 10;
                    tot += highest;
                }

                output += tot;
            }

            return output.ToString();
        }
    }
}
