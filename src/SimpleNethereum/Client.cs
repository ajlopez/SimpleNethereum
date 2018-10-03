using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
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
            : this("http://localhost:8545")
        {
        }

        public Client(string host)
        {
            this.web3 = new Web3(host);
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

        public async Task<string> Transfer(string fromAccount, string toAccount, HexBigInteger value, HexBigInteger gas, HexBigInteger gasPrice)
        {
            TransactionInput ti = new TransactionInput(null, 
                toAccount, 
                fromAccount, 
                gas, gasPrice, value);
            
            return await web3.Eth.Transactions.SendTransaction.SendRequestAsync(ti);
        }

        public async Task<TransactionReceipt> GetTransactionReceipt(string transactionHash)
        {
            return await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
        }

        public async Task<TransactionReceipt> WaitTransactionReceipt(string transactionHash, int nseconds)
        {
            int count = 0;
            var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash, 1000);

            while (receipt == null && count < nseconds)
            {
                count++;
                await Task.Delay(1000);
                receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash, 1000);
            }

            return receipt;
        }

        public async Task<string> DeployContract(string code, string fromAccount, HexBigInteger value, HexBigInteger gas, HexBigInteger gasPrice)
        {
            TransactionInput ti = new TransactionInput(
                code,
                null,
                fromAccount,
                gas, gasPrice, value);

            var hash = await web3.Eth.Transactions.SendTransaction.SendRequestAsync(ti);
            var receipt = await this.WaitTransactionReceipt(hash, 20);

            return receipt == null ? null : receipt.ContractAddress;
        }
    }
}
