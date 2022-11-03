using Flurl.Http;
using Masa.Blazor;
using MauiAppPiDemo.Base;
using MauiAppPiDemo.Data;
using MauiAppPiDemo.Service;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net;

namespace MauiAppPiDemo.Pages
{

    public partial class Login
    {
        [Inject]
        public AgentAppSettings AppSettings { get; set; }
        [Inject]
        public IPopupService PopupService { get; set; }
        [Inject]
        private ILocalStorgeService _localStorgeService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        class Model
        {
            public string UserName { get; set; }

            public string Password { get; set; }
        }
        private Model _model = new();
        private async Task HandleOnValidSubmit()
        {
            if (_model.Password is null)
            {
                await PopupService.AlertAsync("请输入密码！");
                return;
            }

            await LoginInAsync(_model.UserName, _model.Password);

        }
        protected override async Task OnInitializedAsync()
        {
            var token = await _localStorgeService.GetToken();

        }

        private async Task LoginInAsync(string userName ,string password)
        {
            var url = $"{AppSettings.BaseApiUrl}/login";

            var res = await url.PostJsonAsync(new
            {
                username= userName,
                password= password,
            });
            if (res.StatusCode != (int)HttpStatusCode.OK)
            {
                var msg = await res.ResponseMessage.Content.ReadAsStringAsync() ?? "登录失败！";
                await PopupService.AlertAsync(msg);
                return;
            }

            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(await res.ResponseMessage.Content.ReadAsStringAsync());

            await _localStorgeService.SetToken(tokenResponse.token);
            Navigation.NavigateTo("/DeviceList");
        }
    }
}
