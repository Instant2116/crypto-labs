using System;
using System.Text;
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

        public static string BruteForceWithHeuristics(string encoded, string[] heuristics, int keyLen = 3)
        {
            List<List<char>> groups = new List<List<char>>();
            for (int i = 0; i < keyLen; i++)
                groups.Add(new List<char>());

            for (int i = 0; i < encoded.Length; i += keyLen)
            {
                for (int j = 0; j < keyLen; j++)
                {
                    if (i + j < encoded.Length)
                        groups[j].Add(encoded[i + j]);
                }
            }
            double keyCombo = Math.Pow(255, keyLen);
            byte[] jk = Encoding.UTF8.GetBytes("L0l");
            for (int i = 0; i < keyCombo; i++)
            {
                string decoded = "";
                byte[] key = KeyGen(i, keyLen);

                for (int j = 0; j < groups[0].Count; j++)
                {
                    for (int n = 0; n < keyLen; n++)
                    {
                        if (j < groups[n].Count)
                            decoded += Convert.ToChar(Convert.ToByte(groups[n][j]) ^ key[n]);
                    }
                }
                bool all_components = false;
                foreach (string h in heuristics)
                {
                    if (decoded.Contains(h))
                    {
                        all_components = true;
                    }
                    else { all_components = false; break; }
                }
                if (all_components)
                    return decoded;
               
            }
            return null;
        }

        public static byte[] KeyGen(int seed, int keyLen)
        {
            byte[] key = new byte[keyLen];
            key = BitConverter.GetBytes(seed);
            return key;
        }
    }
    
}
