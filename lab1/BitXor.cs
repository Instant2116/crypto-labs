using System;
namespace lab1
{
    public static class BitXor
    {
        public static void BruteForce(byte[] encoded, string logPath)
        {
            for (int i = 0; i < 255; i++)
            {
                byte[] temp = new byte[encoded.Length - 1];
                for (int j = 0; j < encoded.Length - 1; j++)
                {
                    temp[j] = (byte)(encoded[j] ^ i);
                }
                string result = System.Text.Encoding.UTF8.GetString(temp);
                System.IO.File.AppendAllText(logPath, "i:\t" + i + "\n" + result + "\n");
                Console.WriteLine("i:\t" + i);
                Console.WriteLine(result);
                Console.WriteLine();
            }
        }


    }
    
    
    
}
