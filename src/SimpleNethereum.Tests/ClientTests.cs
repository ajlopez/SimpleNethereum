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

        [TestMethod]
        public async Task Transfer()
        {
            var client = new Client();

            var accounts = await client.GetAccounts();

            var balance0 = (await client.GetBalance(accounts[0])).Value;
            var balance1 = (await client.GetBalance(accounts[1])).Value;

            var hash = await client.Transfer(accounts[0], accounts[1], new HexBigInteger(new BigInteger(1000)), new HexBigInteger(new BigInteger(21000)), new HexBigInteger("0x00"));

            var newbalance0 = (await client.GetBalance(accounts[0])).Value;
            var newbalance1 = (await client.GetBalance(accounts[1])).Value;

            Assert.AreEqual(BigInteger.Subtract(balance0, new BigInteger(1000)), newbalance0);
            Assert.AreEqual(BigInteger.Add(balance1, new BigInteger(1000)), newbalance1);
        }
    }
}
