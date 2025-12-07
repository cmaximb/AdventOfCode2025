using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cephalapods___6
{
    public static class stringUpdates
    {
        public static string ConvertLine(this string input, int[] fieldSizes)
        {
            StringBuilder sb = new StringBuilder();

            int j = 0;
            for (int i = 0; i < input.Length;)
            {
                string field;
                try
                {
                    field = input.Substring(i, fieldSizes[j]);
                }
                catch
                {
                    field = input.Substring(i);
                }

                string fixedField = new string(
                    field.Select(ch => ch == ' ' ? '0' : ch).ToArray()
                );

                sb.Append(fixedField);

                i += fieldSizes[j];

                if (i < input.Length && input[i] == ' ')
                {
                    sb.Append(' ');
                    i++;
                }

                j++;
            }

            return sb.ToString();
        }

        internal class Program
        {
            static void Main(string[] args)
            {
                string[] file = File.ReadAllLines("input.txt");
                string[] separators = new string[] { " ", "  ", "   " };
                char[] oparators = new char[] { '*','+'};
                double[] line_split = file[0].Split(separators, StringSplitOptions.RemoveEmptyEntries).Select(x => double.Parse(x)).ToArray();
                string[] operations = file[file.Length - 1].Split(separators, StringSplitOptions.RemoveEmptyEntries);
                int[] fieldSizes = file[file.Length - 1].Split(oparators).Select(x => x.Length).Skip(1).ToArray();

                foreach (string line in file.Skip(1))
                {
                    if ("*+".Contains(line[0])) break;
                    int[] adders = line.Split(separators, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

                    line_split = line_split.Select((x, c) =>
                    {
                        if (operations[c] == "*")
                            x *= adders[c];
                        else if (operations[c] == "+")
                            x += adders[c];
                        return x;
                    }).ToArray();
                }
                double total = 0;

                for (int i = 0; i < file.Length - 1; i++)
                {
                    file[i] = file[i].ConvertLine(fieldSizes);
                }

                for (int i = 0; i < line_split.Length; i++)
                {
                    string[] adders = new string[4];
                    for (int j = 0; j < 4; j++)
                    {
                        string updated_line = new string(file[j].TakeWhile(x => x != ' ').ToArray());
                        adders[j] = updated_line;
                        if (file[j].Length > updated_line.Length + 1)
                            file[j] = file[j].Remove(0, updated_line.Length + 1);
                    }

                    string[] digits = new string[fieldSizes[i]];
                    foreach (var item in adders)
                    {
                        for (int j = 0; j < fieldSizes[i]; j++)
                        {
                            if (item.ToString().Length > j)
                            {
                                digits[j] = digits[j] + item.ToString()[j];
                            }
                        }
                    }

                    double count = 0;
                    if (operations[i] == "*")
                    {
                        count = 1;
                        foreach (var item in digits)
                        {
                            double tem = double.Parse(item);
                            while (tem % 10 == 0)
                                tem = tem / 10;

                            if (item != null)
                                count *= tem;
                        }
                    }
                    else if (operations[i] == "+")
                    {
                        count = 0;
                        foreach (var item in digits)
                        {
                            double tem = double.Parse(item);
                            while (tem % 10 == 0)
                                tem = tem / 10;

                            if (item != null)
                                count += tem;
                        }
                    }

                    total += count;
                }

                Console.WriteLine(total);
                total = 0;

                foreach (var count in line_split)
                    total += count;

                Console.WriteLine(total);
            }
        }
    }
}
