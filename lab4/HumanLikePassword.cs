using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4
{
    class HumanLikePassword
    {
        private List<string> passwordsList = new List<string>();
        private Random random = new Random();
        ReadPassStatisitic RPSmany = new ReadPassStatisitic(@"D:\_vs2019\repos\crypto-labs\lab4\Doc\10-million-password-list-top-100000.txt");



        private static readonly int leftBound = 6;

        public List<string> GeneratePass(int amount)
        {
            var listPass = RPSmany.GeneratePass(amount);

            var GeneratedPasswords = new List<string>();

            for (var i = 0; i < amount; i++)
            {
                var j = random.Next(2);
                if (j == 0)
                {
                    GeneratedPasswords.Add(RandHumanPass());
                }
                else
                {
                    GeneratedPasswords.Add(RandHumanPass2(listPass[i]) + listPass[random.Next(amount)]);
                }
            }

            return GeneratedPasswords;
        }

        private string RandHumanPass()
        {
            List<string> Noun = new List<string>() { "Master", "Varior", "Lady", "Crutch", "fail", "Burger", "Wizard", "Capone", "Boss", "Chiken" };
            List<string> adjective = new List<string>() { "solid", "big", "nuclear", "empty", "tragic", "mad", "mineral", "polish", "negative", "mother's" };
            List<string> NumberLetters = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string password = string.Empty;
            password += addPice(adjective);
            password += addPice(Noun);
            password += addPice(NumberLetters);
            password += addPice(NumberLetters);
            password += addPice(NumberLetters);
            return password;
        }

        private string addPice(List<string> list)
        {
            int i = random.Next(list.Count);
            return list[i];
        }

        private string RandHumanPass2(string listPass)
        {
            listPass.Replace("O", "0");
            listPass.Replace("c", "C");
            listPass.Replace("l", "1");
            listPass.Replace("to", "2");
            listPass.Replace("for", "4");
            listPass.Replace("m", "M");
            listPass.Replace("S", "5");
            listPass.Replace("no", "9");
            listPass.Replace("B", "8");
            return listPass;



        }

    }
}