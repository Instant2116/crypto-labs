using System.Threading.Tasks;

namespace lab3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Lab3 huinya = new Lab3();
            Client client = new Client();
            await client.CreateAcc(123456789);
            var Lcg = await huinya.LcgHack();
            await client.Play(123456789, 10, Lcg.Next(), "Lcg");
            var Mt = await huinya.MtHack("BetterMt");

            await client.Play(123456789, 1000, Mt.Next(), "BetterMt");
        }
    }
}