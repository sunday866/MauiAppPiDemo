using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppPiDemo.Service
{
    public class SecureStorageService : ILocalStorgeService
    {
        public async Task<string> GetToken()
        {
            return await SecureStorage.GetAsync("Token");
        }

        public async Task SetToken(string token)
        {
            await SecureStorage.SetAsync("Token", token);
        }

        public async Task SetData(string key, string data)
        {
            await SecureStorage.SetAsync(key, data);
        }

        public async Task<string> GetData(string key)
        {
            return await SecureStorage.GetAsync(key);
        }

        public async Task RemoveData(string key)
        {
            SecureStorage.Remove(key);
            await Task.CompletedTask;
        }
    }
}
