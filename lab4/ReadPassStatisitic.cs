using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab4
{
    class ReadPassStatisitic
    {
        private List<string> passwordsList = new List<string>();

        public ReadPassStatisitic(string path)
        {

            string[] readLines = File.ReadAllLines(path);
            foreach (string s in readLines)
            {
                passwordsList.Add(s);
            }

        }

        public List<string> GeneratePass(int Count)
        {
            var GeneratedPasswords = new List<string>();
            Random random = new Random();

            for (int i = 0; i < Count; i++)
            {
                GeneratedPasswords.Add(passwordsList[random.Next(passwordsList.Count)]);
            }

            return GeneratedPasswords;
        }
    }
}