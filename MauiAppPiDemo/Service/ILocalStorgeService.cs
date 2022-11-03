

namespace MauiAppPiDemo.Service
{
    public interface ILocalStorgeService
    {
        Task<string> GetToken();

        Task SetToken(string token);

        Task SetData(string key, string data);

        Task<string> GetData(string key);
        Task RemoveData(string key);
    }
}
