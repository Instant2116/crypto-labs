using System;
namespace lab1
{
    public static class BitXor
    {
        public static void BruteForce(byte[] encoded, string logPath)
        {
            System.IO.File.Create(logPath).Close();
            for (int i = 0; i < 255; i++)
            {
                byte[] temp = Xor(encoded, (byte)i);

                string result = System.Text.Encoding.UTF8.GetString(temp);
                System.IO.File.AppendAllText(logPath, "i:\t" + i + "\n" + result + "\n");
                Console.WriteLine("i:\t" + i);
                Console.WriteLine(result);
                Console.WriteLine();
            }
        }
        public static byte[] Xor(byte[] encoded, byte key)
        {
            byte[] result = new byte[encoded.Length];
            for (int j = 0; j < encoded.Length; j++)
            {
                result[j] = (byte)(encoded[j] ^ key);
            }
            return result;
        }
    }
}
