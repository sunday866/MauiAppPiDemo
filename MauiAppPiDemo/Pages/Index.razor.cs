using Masa.Blazor;
using MauiAppPiDemo.Helper;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace MauiAppPiDemo.Pages
{
    public partial class Index
    {
        private string Textarea { get; set; }
        private async Task OnClick()
        {
            await MqttHelper.Connect_Client_Using_WebSockets(new Func<MqttApplicationMessageReceivedEventArgs, Task>(CallbackAsync));
        }

        private async Task LedOn(bool isOn)
        {
            var cmdStr = "Off";
            if (isOn)
            {
                cmdStr = "On";
            }
            await MqttHelper.SendCmdAsync(cmdStr);
        }

        public async Task CallbackAsync(MqttApplicationMessageReceivedEventArgs e)
        {
   
           await InvokeAsync(() =>
            {
                Textarea = $"{System.Text.Encoding.Default.GetString(e.ApplicationMessage.Payload)}";
                StateHasChanged();
            });
        }

    }
}
