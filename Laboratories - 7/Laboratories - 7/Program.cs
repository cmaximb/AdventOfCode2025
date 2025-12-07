using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratories___7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] file = File.ReadAllLines("input.txt");
            int start_pos = file[0].IndexOf('S');

            var chars = file[1].ToCharArray();
            chars[start_pos] = '|';
            file[1] = new string(chars);

            int output = 0;

            for (int i = 2; i < file.Length; i += 2) {
                for (int j = 0; j < file[i].Length; j++) {
                    if (file[i][j] == '^' && file[i - 1][j] == '|') {
                        char[] line = file[i + 1].ToCharArray();
                        line[j + 1] = '|';
                        line[j - 1] = '|';
                        file[i + 1] = new string(line);

                        output++;
                    }
                    else if (file[i - 1][j] == '|') {
                        char[] line = file[i + 1].ToCharArray();
                        line[j] = '|';
                        file[i + 1] = new string(line);
                    }
                }
            }

            StreamWriter streamWriter = new StreamWriter("output.txt");

            foreach (string l in file) {
                streamWriter.WriteLine(l);
            }

            streamWriter.Close();

            Console.WriteLine(output);
        }
    }
}
