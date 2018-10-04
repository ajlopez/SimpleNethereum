using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNethereum.Console
{
    class Program
    {
        static Client client;
        static string[] accounts;

        static void Main(string[] args)
        {
            if (args.Length > 0)
                client = new Client(args[0]);
            else
                client = new Client();

            DoWork().Wait();
        }

        async static Task DoWork()
        {
            while (true)
            {
                System.Console.Write("> ");
                try
                {
                    await ExecuteCommand(System.Console.ReadLine());
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
        }

        async static Task ExecuteCommand(string cmd)
        {
            var words = cmd.Split(' ');

            if (words.Length == 0)
                return;

            var verb = words[0].Trim().ToLower();

            if (verb == "accounts")
                await DoAccounts();
            else if (verb == "balance")
                await DoBalance(words[1]);
            else if (verb == "transfer")
                await DoTransfer(words[1], words[2], words[3]);
        }

        async static Task DoAccounts()
        {
            accounts = await client.GetAccounts();

            for (int k = 0; k < accounts.Length; k++)
                System.Console.WriteLine(accounts[k]);
        }

        async static Task DoTransfer(string fromAccount, string toAccount, string value)
        {
            await client.Transfer(accounts[int.Parse(fromAccount)], accounts[int.Parse(toAccount)], long.Parse(value), 21000L, 0L);
        }

        async static Task DoBalance(string account)
        {
            var balance = await client.GetBalance(accounts[int.Parse(account)]);

            System.Console.WriteLine(balance.HexValue);
        }
    }
}


