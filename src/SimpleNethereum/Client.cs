using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNethereum
{
    public class Client
    {
        private Web3 web3;

        public Client()
        {
            this.web3 = new Web3("http://localhost:8545");
        }

        public Web3 Web3
        {
            get
            {
                return this.web3;
            }
        }

        public async Task<String[]> GetAccounts()
        {
            return await web3.Eth.Accounts.SendRequestAsync();
        }

        public async Task<HexBigInteger> GetBalance(string account)
        {
            return await web3.Eth.GetBalance.SendRequestAsync(account);
        }
    }
}
