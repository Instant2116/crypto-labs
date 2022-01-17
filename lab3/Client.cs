using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace lab3
{
    class Client
    {

        private static readonly HttpClient client = new HttpClient();

        private const string casinoURL = "";
        private Account player = new Account();
        public async Task CreateAcc(int id)
        {
            var response = await client.GetAsync($"{casinoURL}createacc?id={id}");
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            player = JsonConvert.DeserializeObject<Account>(await response.Content.ReadAsStringAsync());
        }

        public async Task<long> Play(int id, int bet, long number, string mode)
        {
            var response = await client.GetAsync($"{casinoURL}play{mode}?id={id}&bet={bet}&number={number}");
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            player = JsonConvert.DeserializeObject<BetAndPlay>(await response.Content.ReadAsStringAsync()).account;
            var output = JsonConvert.DeserializeObject<BetAndPlay>(await response.Content.ReadAsStringAsync()).realNumber;

            return output;
        }

        public class Account
        {
            public string id { get; set; }
            public decimal money { get; set; }
            public string deletionTime { get; set; }
        }

        public class BetAndPlay
        {
            public string message { get; set; }
            public Account account { get; set; }
            public long realNumber { get; set; }
        }
    }
}