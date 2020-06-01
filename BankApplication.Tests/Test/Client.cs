using BankApplication.Data.DTOs;
using BankApplication.Data.Models;
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
    class Client
    {
        private readonly ClientService bankService;
        public Client()
        {
            bankService = new ClientService();

        }
        [Test, Category("API")]
        public async Task ShouldReturnAllClientAsync()
        {
            // Arrange 

            //Act
            var response = await bankService.GetClient();

            // Assert
            Assert.AreEqual(true, response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var studentResponse = JsonConvert.DeserializeObject<List<ClientDTO>>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(5, studentResponse.Count);
        }
        [Test, Category("API")]
        public async Task ShouldReturnSpecificClientAsync()
        {
            // Arrange 
            const int clientId = 1;

            //Act
            var response = await bankService.GetClient($"GetAll/{clientId}");

            // Assert
            Assert.AreEqual(true, response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var clientResponse = JsonConvert.DeserializeObject<ClientDTO>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(clientResponse);
            Assert.AreEqual(clientId, clientResponse.Id);
        }
        [Test, Category("API")]
        public async Task ShouldBeAbleToDeleteClientAsync()
        {
            // Arrange 
            const int clientId = 6;

            //Act
            var response = await bankService.DeleteClient($"RemoveClient/{clientId}");

            // Assert
            Assert.AreEqual(true, response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var deleteResponse = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual("true", deleteResponse);
        }

        [Test, Category("API")]
        public async Task ShouldBeAbleToUpdateClientAsync()
        {
            // Arrange 
            ClientDTO client = new ClientDTO()
            {
                Name = "Client",
                PhoneNumber = "Test",
                Mail = "client.test@mail.com",
                Type = ClientType.Business,
                AddressId = 1,
               
            };

            //Act
            var response = await bankService.UpdateClient(client, $"UpdateClient/6");

            // Assert
            Assert.AreEqual(true, response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var clientResponse = JsonConvert.DeserializeObject<ClientDTO>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual("test", clientResponse.AddressId);
            Assert.AreEqual("client.test@mail.com", clientResponse.Mail);

        }

        [Test, Category("API")]
        public async Task ShouldBeAbleToAddNewClient()
        {
            // Arrange 
            ClientDTO client = new ClientDTO()
            {
                Name = "Client",
                PhoneNumber = "Test",
                Mail = "client.test@mail.com",
                Type = ClientType.Business, 
                AddressId = 1,
            };

            //Act
            var response = await bankService.NewClient(client);

            // Assert
            Assert.AreEqual(true, response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var clientResponse = JsonConvert.DeserializeObject<ClientDTO>(await response.Content.ReadAsStringAsync());
           Assert.AreEqual("test", clientResponse.PhoneNumber);
            Assert.AreEqual("client.test@mail.com", clientResponse.Mail);
        }
    }
}
