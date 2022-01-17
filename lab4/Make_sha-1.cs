using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;


namespace Lab4
{
    class Make_sha_1
    {

        public static string GetHash(string str)
        {
            byte[] salt = GenerateSaltBites();
            byte[] sourceBytes = Encoding.UTF8.GetBytes(str);


            byte[] passwordWithSaltBytes = new byte[sourceBytes.Length + salt.Length];
            byte[] passwordWithSaltBytesOut = new byte[sourceBytes.Length];



            for (int i = 0; i < sourceBytes.Length; i++)
            {
                passwordWithSaltBytes[i] = sourceBytes[i];
            }
            for (int i = 0; i < sourceBytes.Length; i++)
            {
                passwordWithSaltBytesOut[i] = sourceBytes[i];
            }

            string result = BitConverter.ToString(passwordWithSaltBytesOut).Replace("-", string.Empty);
            for (int i = 0; i < salt.Length; i++)
            {
                passwordWithSaltBytes[sourceBytes.Length + i] = salt[i];

            }
            result = result + ";" + BitConverter.ToString(passwordWithSaltBytes).Replace("-", string.Empty);
            return result;
        }

        static private byte[] GenerateSaltBites()
        {
            var length = new byte[12];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(length);
            return length;
        }




    }
}