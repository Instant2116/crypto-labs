using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lab3
{
    class Lab3
    {
        public async Task<LcgCracker> LcgHack()
        {
            Client client = new Client();
            long[] temp = new long[3];
            for (int i = 0; i < 3; i++)
            {
                temp[i] = await client.Play(123456789, 1, 1, "Lcg");
            }
            long a1 = temp[0] - temp[1];

            long inverse_a1 = ModInverse(a1, Lcg.m);
            long a2 = temp[1] - temp[2];
            long a = (inverse_a1 * a2) % Lcg.m;

            Console.WriteLine("A :" + a);

            long c = (temp[1] - temp[0] * a) % Lcg.m;

            Console.WriteLine("C : " + c);

            LcgCracker cracked = new LcgCracker(a, c, temp[2]);
            var next = cracked.Next();
            var Bet = await client.Play(123456789, 1, next, "Lcg");
            if (Bet != next)
            {
                cracked = await LcgHack();
            }
            return cracked;
        }

        private long ModInverse(long n, long m)
        {
            if (m == 1) return 0;
            long m0 = m;
            long x = 1L;
            long y = 0L;

            while (n > 1)
            {
                var q = n / m;

                (n, m) = (m, n % m);
                (x, y) = (y, x - q * y);
            }

            return x < 0 ? x + m0 : x;

        }


        public async Task<MtCracker> MtHack(string mode)
        {
            Client client = new Client();
            List<long> History = new List<long>();
            for (int i = 0; i < 624; i++)
            {
                History.Add(await client.Play(123456789, 1, 1, mode));
            }
            List<long> used = new List<long>();
            for (int i = 0; i < Mt.N; i++)
            {
                long y = History[i];
                y = unBitshiftRightXor(y, Mt.L);
                y = unBitshiftLeftXor(y, Mt.T, Mt.C);
                y = unBitshiftLeftXor(y, Mt.S, Mt.B);
                y = unBitshiftRightXor(y, Mt.U);
                used.Add(y);
            }

            MtCracker cracked = new MtCracker(used.ToArray(), 624);
            var next = cracked.Next();
            Console.WriteLine(next);
            var a = await client.Play(123456789, 1, next, mode);

            return cracked;
        }

        private long unBitshiftRightXor(long value, int shift)
        {
            var result = value;

            for (var i = shift; i < Mt.W; i += shift)
            {
                result ^= value >> i;
            }
            return result;
        }

        private long unBitshiftLeftXor(long value, int shift, uint mask)
        {
            var window = (1 << shift) - 1;
            var result = value;

            for (var i = 0; i < Mt.W / shift; i++)
            {
                result ^= ((window & result) << shift) & mask;
                window <<= shift;
            }
            return result;
        }
    }
}