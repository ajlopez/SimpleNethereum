using System;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nethereum.Hex.HexTypes;

namespace SimpleNethereum.Tests
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        public void CreateCLientWithDefaultConnection()
        {
            var client = new Client();

            Assert.IsNotNull(client.Web3);
        }

        [TestMethod]
        public async Task GetAccounts()
        {
            var client = new Client();

            var accounts = await client.GetAccounts();

            Assert.IsNotNull(accounts);
            Assert.AreEqual(10, accounts.Length);
        }

        [TestMethod]
        public async Task GetBalance()
        {
            var client = new Client();

            var accounts = await client.GetAccounts();

            var balance = await client.GetBalance(accounts[0]);

            Assert.IsNotNull(balance);
            Assert.IsFalse(balance.Value.Equals(BigInteger.Zero));
        }
    }
}
