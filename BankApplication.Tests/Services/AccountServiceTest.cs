using AutoMapper;
using BankApplication.Data.DTOs;
using BankApplication.Data.Models;
using BankApplication.Service.Repositories;
using BankApplication.Tests.Internal;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Tests.Services
{
    [TestFixture]
    class AccountServiceTest
    {
        private IAccountsRepository _service;
        private readonly IMapper _mapper;

        public AccountServiceTest()
        {
            var config = new MapperConfiguration(mc =>
            {
                mc.AddMaps("BankApplication.Data");
            });
            _mapper = config.CreateMapper();
        }

        [Test, Category("DB"), Category("Service")]
        public async Task GetById_Should_Return_Correct_Account()
        {
            // Arrange
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.AccountsService(dbContext, _mapper);
            var accountId = 1;

            // Act
            var actual = await _service.GetAccount(accountId);

            // Assert
            Assert.AreEqual(accountId, actual.Id);
        }

        [Test, Category("DB"), Category("Service")]
        public async Task GetById_Should_Return_Null_Account()
        {
            // Arrange
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.AccountsService(dbContext, _mapper);
            var accountId = 9;

            // Act
            var actual = await _service.GetAccount(accountId);

            // Assert
            Assert.IsNull(actual);
        }

        [Test, Category("DB"), Category("Service")]
        public async Task GetAccount_Should_Return_Correct_Count()
        {
            // Arrange
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.AccountsService(dbContext, _mapper);
            const int accountCount = 5;

            // Act
            var actual = _service.GetAccounts();

            // Assert
            Assert.AreEqual(accountCount, actual.Count());
        }

        [Test, Category("DB"), Category("Service")]
        public async Task ShouldBeAbleToAddClientAsync()
        {
            // Arrange 
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.AccountsService(dbContext, _mapper);
            AccountDTO account = new AccountDTO()
            {
                Name = "Client",
                Balance = 2,
                IsActive = true,
                Type = AccountType.CreditCard,
                ClientId = 1,
            };

            //Act
            var response = _service.SaveAccount(account);
            var item = dbContext.Clients.Find(response.Id);

            // Assert
           
            Assert.AreEqual(item.Name, response.Name);

        }

        [Test, Category("DB"), Category("Service")]
        public async Task ShouldBeAbleToDeleteAccountAsync()
        {
            // Arrange 
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.AccountsService(dbContext, _mapper);
            int accountId = 1;

            //Act
            var response = _service.DeleteAccount(accountId);

            // Assert
            Assert.IsTrue(response);
            Assert.AreEqual(4, dbContext.Accounts.Count());
            Assert.IsNull(dbContext.Accounts.Find(accountId));
        }

        [Test, Category("DB"), Category("Service")]
        public async Task ShouldNotToDeleteAccountAsync()
        {
            // Arrange 
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.AccountsService(dbContext, _mapper);
            int accountId = 9;

            //Act
            var response = _service.DeleteAccount(accountId);

            // Assert
            Assert.IsFalse(response);
            Assert.AreEqual(5, dbContext.Accounts.Count());
            Assert.IsNull(dbContext.Accounts.Find(accountId));
        }

        [Test, Category("DB"), Category("Service")]
        public async Task ShouldBeAbleToUpdateAccountAsync()
        {
            // Arrange 
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.AccountsService(dbContext, _mapper);
            Account accountEntity = new Account()
            {
                Name = "Client",
                Balance = 2,
                IsActive = true,
                Type = AccountType.CreditCard,
                ClientId = 1,
            };
            dbContext.Accounts.Add(accountEntity);
            dbContext.SaveChanges();

            AccountDTO accountDto = new AccountDTO()
            {
                Name = "Client",
                Balance = 2,
                IsActive = true,
                Type = AccountType.CreditCard,
                ClientId = 1,
            };

            //Act
            var response = _service.PutAccount(accountEntity.Id, accountDto);

            // Assert
            Assert.AreEqual(accountDto.Name, response.Name);

           
        }

        [Test, Category("DB"), Category("Service")]
        public async Task ShouldNotUpdateAccountAsync()
        {
            // Arrange 
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            _service = new Service.Service.AccountsService(dbContext, _mapper);
            AccountDTO accountDto = new AccountDTO()
            {
                Name = "Client",
                Balance = 2,
                IsActive = true,
                Type = AccountType.CreditCard,
                ClientId = 1,
            };

            //Act
            var ex = Assert.Throws<Exception>(() => _service.PutAccount(accountDto.Id, accountDto));

            // Assert
            Assert.That(ex.Message == "Account not found");
        }
    }
}
