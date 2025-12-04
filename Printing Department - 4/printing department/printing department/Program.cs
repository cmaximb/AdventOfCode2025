using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace printing_department
{
    internal class Program
    {
        static bool[,] find_adjacent_loo_roll(string[] file, int CenterY, int CenterX)
        {
            bool[,] win = new bool[3, 3];
            int[] offset = { -1, 0, 1 };

            for (int x = 0; x <= 2; x++)
            {
                for (int y = 0; y <= 2; y++)
                {
                    int x_off = CenterX + offset[x];
                    int y_off = CenterY + offset[y];

                    if (x_off >= 0 && y_off >= 0 && y_off < file.Length && x_off < file[0].Length)
                        win[x, y] = file[y_off][x_off] == '@';
                    else
                        win[x, y] = false;
                }
            }

            return win;
        }

        static int sum_up_each_window(bool[,] window)
        {
            int sum = 0;

            for (int x = 0; x <= 2; x++)
            {
                for (int y = 0; y <= 2; y++)
                {
                    if (window[x, y])
                        sum += 1;
                }
            }

            return sum;
        }

        static void Main(string[] args)
        {
            string[] file = File.ReadAllLines("input.txt");
            int total = 0;
            bool[,] window = new bool[3,3];

            for (int l = 0; l < file.Length; l++)
            {
                for (int c  = 0; c < file[l].Length; c++)
                {
                    int window_total = 5;
                    if (file[l][c] == '@')
                    { 
                        window = find_adjacent_loo_roll(file, l, c);

                        window_total = sum_up_each_window(window);
                    }
                   
                    if (window_total < 5)
                    {
                        Console.Write("X");
                        total++;
                    }
                    else
                    {
                        Console.Write($"{file[l][c]}");
                    }
                }

                Console.WriteLine("");
            }
            Console.WriteLine($"{total}");
        }

        
    }
}
