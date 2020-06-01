using BankApplication.Data.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Tests.Services
{
    class ClientService
    {
        static HttpClient client;
        private const string Url = "https://localhost:44370/api/Bank/";

        public ClientService()
        {
            client = new HttpClient { BaseAddress = new Uri(Url) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public ClientService(string baseUrl)
        {
            client = new HttpClient { BaseAddress = new Uri(baseUrl) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<HttpResponseMessage> GetClient(string path = "Get")
        {
            return await client.GetAsync(path);
        }
        public async Task<HttpResponseMessage> NewClient(ClientDTO cl, string path = "NewClient")
        {
            var json = JsonConvert.SerializeObject(cl);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            return await client.PostAsync(path, data);
        }
        public async Task<HttpResponseMessage> UpdateClient(ClientDTO cl, string path)
        {
            var json = JsonConvert.SerializeObject(cl);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            return await client.PutAsync(path, data);
        }

        public async Task<HttpResponseMessage> DeleteClient(string path)
        {
            return await client.DeleteAsync(path);
        }
    }
}

