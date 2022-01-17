using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadPassStatisitic RPS25 = new ReadPassStatisitic(@"D:\_vs2019\repos\crypto-labs\lab4\Doc\25passwords.txt");
            ReadPassStatisitic RPSmany = new ReadPassStatisitic(@"D:\_vs2019\repos\crypto-labs\lab4\Doc\10-million-password-list-top-100000.txt");
            SuperRandomMegaPasswordGeneratorEver SRMPGE = new SuperRandomMegaPasswordGeneratorEver();
            HumanLikePassword HLP = new HumanLikePassword();

            List<string> allPasswords = new List<string>();
            allPasswords.AddRange(RPS25.GeneratePass(10000));
            allPasswords.AddRange(RPSmany.GeneratePass(75000));
            allPasswords.AddRange(SRMPGE.GeneratePass(5000));
            allPasswords.AddRange(HLP.GeneratePass(10000));

            var random = new Random();
            List<string> AllPasswords = allPasswords.OrderBy(x => random.Next()).ToList();
            var sha1Hashes = new List<string>();
            var md5Hashes = new List<string>();
            var BcryptHashes = new List<string>();
            foreach (var pas in AllPasswords)
            {
                sha1Hashes.Add((Make_sha_1.GetHash(pas)));
                md5Hashes.Add(Make_md5.GetHash(pas));
                //BcryptHashes.Add(BCrypt.Net.BCrypt.HashPassword(pas, 12));              

            }
            File.WriteAllLines("passwords.csv", AllPasswords);
            File.WriteAllLines("sha1hash.csv", sha1Hashes);
            File.WriteAllLines("md5hash.csv", md5Hashes);
            //File.WriteAllLines("BCrypthash.csv", md5Hashes);
        }

    }


}