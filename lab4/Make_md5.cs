using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Lab4
{
    public class Make_md5
    {
        public static string GetHash(string input)
        {
            var md5 = MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            string hash = sBuilder.ToString();

            return hash;
        }
    }
}