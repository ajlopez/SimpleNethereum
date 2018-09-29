using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
