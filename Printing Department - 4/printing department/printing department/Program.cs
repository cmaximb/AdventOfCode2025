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
        static bool[,] find_adjacent_paper_roll(string[] file, int CenterX, int CenterY)
        {
            bool[,] win = new bool[3, 3];
            int[] offset = { -1, 0, 1 };

            for (int x = 0; x <= 2; x++)
            {
                for (int y = 0; y <= 2; y++)
                {
                    int x_off = CenterX + offset[x];
                    int y_off = CenterY + offset[y];

                    if (x_off >= 0 && y_off >= 0 && y_off < file.Length && x_off < file[0].Length && (x != 1 || y != 1))
                        win[x,y] = file[x_off][y_off] == '@';
                    else
                        win[x,y] = false;
                }
            }

            return win;
        }

        static int sum_up_each_window(bool[,] window)
        {
            int sum = 0;
            
            for (int y = 0; y <= 2; y++)
            {
                for (int x = 0; x <= 2; x++) 
                {
                    if (window[y,x])
                        sum += 1;
                }
            }

            return sum;
        }

        static void Main(string[] args)
        {
            string[] file = File.ReadAllLines("input.txt");
            int total = -1;
            bool[,] window = new bool[3,3];

            bool Part2 = true;
            int num_gone = 0;

            while (total != 0)
            {
                total = 0;

                string[] newFile = (string[])file.Clone();

                for (int l = 0; l < file.Length; l++)
                {
                    for (int c = 0; c < file[l].Length; c++)
                    {
                        int window_total = 4;

                        if (file[l][c] == '@')
                        {
                            window = find_adjacent_paper_roll(file, l, c);
                            window_total = sum_up_each_window(window);
                        }

                        if (window_total < 4)
                        {
                            newFile[l] = file[l].ReplaceAt(c, 'X');
                            total++;
                        }
                    } 
                }
                
                num_gone += total;

                file = newFile;

                if (!Part2)
                    break;
            }

            int output = 0;
            foreach (string line in file)
            {
                foreach (char c in line)
                {
                    if (c == 'X')
                        output++;
                }
            }

            // Console.WriteLine($"{num_gone}");
            Console.WriteLine($"{output}");
        }
    }

    public static class StringExtensions
    {
        public static string ReplaceAt(this string input, int index, char newChar)
        {
            char[] chars = input.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }
    }
}
