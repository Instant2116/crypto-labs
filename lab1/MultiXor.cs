using System;
using System.Collections.Generic;
namespace lab1
{
    public static class MultiXor
    {

        
        public static SortedList<int, double> GetKeyLen(string line, bool IsOnLog, string logPath = "KeyLenLog.txt") //return shift and share of matches
        {
            System.IO.File.Create(logPath).Close();
            SortedList<int, double> results = new SortedList<int, double>();
            for (int i = 1; i < (line.Length - 1) / 2; i++)
            {

                //System.IO.File
                string shifted = line.Substring(i) + line.Substring(0, i);
                int freq = 0;
                for (int j = 0; j < line.Length - 1; j++)
                {
                    if (line[j] == shifted[j])
                    {
                        freq++;
                    }
                }
                int len = line.Length;
                double share = (double)freq / len;
                results.Add(i, share);
                if (IsOnLog)
                {
                    System.IO.File.AppendAllText(logPath, "i:\t" + i + "\n" + share + "\n");
                    Console.WriteLine("i:\t" + i);
                    Console.WriteLine(share);
                    Console.WriteLine();
                }
            }
            return results;
        }
    }
    
}
