using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4
{
    class SuperRandomMegaPasswordGeneratorEver
    {
        private List<string> passwordsList = new List<string>();

        private char[] CapsLetters = {
            'A', 'B', 'C', 'D', 'E', 'F', 'G','H', 'I', 'J', 'K', 'L', 'M', 'N',
            'O', 'P', 'Q', 'R', 'S', 'T', 'U','V', 'W', 'X', 'Y', 'Z'
        };

        private char[] DownLetters = {
            'a', 'b', 'c', 'd', 'e', 'f', 'g','h', 'i', 'j', 'k', 'l', 'm', 'n',
            'o', 'p', 'q', 'r', 's', 't', 'u','v', 'w', 'x', 'y', 'z'
        };
        private char[] NumberLetters = {
            '0', '1', '2', '3', '4', '5', '6','7', '8', '9'
        };
        private int minPassLength = 8;
        private int maxPassLength = 16;

        public List<string> GeneratePass(int Count)
        {
            var GeneratedPasswords = new List<string>();
            Random random = new Random();
            for (int i = 0; i < Count; i++)
            {
                var len = random.Next(minPassLength, maxPassLength);

                string password = string.Empty;
                for (int j = 0; j < len; j++)
                {

                    var Letter = random.Next(3);
                    switch (Letter)
                    {

                        case 0:
                            password += addLetter(CapsLetters);
                            break;
                        case 1:
                            password += addLetter(DownLetters);
                            break;
                        case 2:
                            password += addLetter(NumberLetters);
                            break;

                    }
                }
                GeneratedPasswords.Add(password);


            }
            return GeneratedPasswords;
        }

        private char addLetter(char[] letters)
        {
            Random random = new Random();
            return letters[random.Next(0, letters.Length)]; ;
        }
    }
}