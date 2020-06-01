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
    class AccountService
    {
        static HttpClient client;
        private const string Url = "https://localhost:44370/api/Account/";

        public AccountService()
        {
            client = new HttpClient { BaseAddress = new Uri(Url) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public AccountService(string baseUrl)
        {
            client = new HttpClient { BaseAddress = new Uri(baseUrl) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<HttpResponseMessage> GetAccount(string path = "Get")
        {
            return await client.GetAsync(path);
        }
        public async Task<HttpResponseMessage> NewAccount(AccountDTO cl, string path = "NewAccount")
        {
            var json = JsonConvert.SerializeObject(cl);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            return await client.PostAsync(path, data);
        }
        public async Task<HttpResponseMessage> UpdateAccount(AccountDTO cl, string path)
        {
            var json = JsonConvert.SerializeObject(cl);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            return await client.PutAsync(path, data);
        }

        public async Task<HttpResponseMessage> DeleteAccount(string path)
        {
            return await client.DeleteAsync(path);
        }
    }
}

