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
            Assert.IsTrue(accounts.Length >= 10);
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

        [TestMethod]
        public async Task TransferAndGetTransactionReceipt()
        {
            var client = new Client();

            var accounts = await client.GetAccounts();
            var hash = await client.Transfer(accounts[0], accounts[1], new HexBigInteger(new BigInteger(1000)), new HexBigInteger(new BigInteger(21000)), new HexBigInteger("0x00"));

            var receipt = await client.GetTransactionReceipt(hash);

            Assert.IsNotNull(receipt);
            Assert.AreEqual(hash, receipt.TransactionHash);
        }

        [TestMethod]
        public async Task TransferAndWaitTransactionReceipt()
        {
            var client = new Client();

            var accounts = await client.GetAccounts();
            var hash = await client.Transfer(accounts[0], accounts[1], new HexBigInteger(new BigInteger(1000)), new HexBigInteger(new BigInteger(21000)), new HexBigInteger("0x00"));

            var receipt = await client.WaitTransactionReceipt(hash, 20);

            Assert.IsNotNull(receipt);
            Assert.AreEqual(hash, receipt.TransactionHash);
        }


        [TestMethod]
        public async Task DeployContract()
        {
            var client = new Client();

            var accounts = await client.GetAccounts();
            var address = await client.DeployContract(
                "608060405234801561001057600080fd5b506001600081905550610118806100286000396000f3006080604052600436106053576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff1680631003e2d21460585780638ada066e146082578063d09de08a1460aa575b600080fd5b348015606357600080fd5b5060806004803603810190808035906020019092919050505060be565b005b348015608d57600080fd5b50609460d0565b6040518082815260200191505060405180910390f35b34801560b557600080fd5b5060bc60d9565b005b80600080828254019250508190555050565b60008054905090565b60008081548092919060010191905055505600a165627a7a72305820714d61f0c4226c78841d54f0f4cce2f77c60a98312537ec76fa9e8604af7c5670029",
                accounts[0], new HexBigInteger(BigInteger.Zero), new HexBigInteger(new BigInteger(1000000)), new HexBigInteger("0x00"));

            Assert.IsNotNull(address);
        }

    }
}
