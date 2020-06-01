using BankApplication.Data.DTOs;
using BankApplication.Tests.Services;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Tests.Test
{
    [TestFixture]
    class Account
    {
        private readonly AccountService bankService;
        public Account()
        {
            bankService = new AccountService();

        }
        [Test, Category("API")]
        public async Task ShouldReturnAllAccountAsync()
        {
            // Arrange 

            //Act
            var response = await bankService.GetAccount();

            // Assert
            Assert.AreEqual(true, response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var accountResponse = JsonConvert.DeserializeObject<List<AccountDTO>>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(5, accountResponse.Count);
        }
        [Test, Category("API")]
        public async Task ShouldReturnSpecificAccountAsync()
        {
            // Arrange 
            const int accountId = 1;

            //Act
            var response = await bankService.GetAccount($"GetAll/{accountId}");

            // Assert
            Assert.AreEqual(true, response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var accountResponse = JsonConvert.DeserializeObject<AccountDTO>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(accountResponse);
            Assert.AreEqual(accountId, accountResponse.Id);
        }
        [Test, Category("API")]
        public async Task ShouldBeAbleToDeleteAccountAsync()
        {
            // Arrange 
            const int accountId = 6;

            //Act
            var response = await bankService.DeleteAccount($"RemoveAccount/{accountId}");

            // Assert
            Assert.AreEqual(true, response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var deleteResponse = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual("true", deleteResponse);
        }

        [Test, Category("API")]
        public async Task ShouldBeAbleToUpdateAccountAsync()
        {
            // Arrange 
            AccountDTO account = new AccountDTO()
            {
                Name = "Client",
                Balance = 2,
                IsActive = true,
                // Type = AType 
                ClientId = 1,

            };

            //Act
            var response = await bankService.UpdateAccount(account, $"UpdateAccount/6");

            // Assert
            Assert.AreEqual(true, response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var accountResponse = JsonConvert.DeserializeObject<AccountDTO>(await response.Content.ReadAsStringAsync());
            // Assert.AreEqual("0001", studentResponse.StudentIndex);
            Assert.AreEqual(4, accountResponse.Balance);

        }

        [Test, Category("API")]
        public async Task ShouldBeAbleToAddNewAccount()
        {
            // Arrange 
            AccountDTO account = new AccountDTO()
            {
                Name = "Client",
                Balance = 2,
                IsActive = true,
                // Type = AType 
                ClientId = 1,
            };

            //Act
            var response = await bankService.NewAccount(account);

            // Assert
            Assert.AreEqual(true, response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var accountResponse = JsonConvert.DeserializeObject<AccountDTO>(await response.Content.ReadAsStringAsync());
            //Assert.AreEqual("test", studentResponse.StudentIndex);
            Assert.AreEqual("true", accountResponse.IsActive);
        }
    }
}
