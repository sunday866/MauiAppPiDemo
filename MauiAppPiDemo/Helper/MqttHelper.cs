using MQTTnet.Client;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet.Formatter;

namespace MauiAppPiDemo.Helper
{
    public static class MqttHelper
    {
        private static MqttFactory mqttFactory;
        private static IMqttClient mqttClient;
        private static MqttClientOptions mqttClientOptions;
        private static MqttClientSubscribeOptions mqttClientSubscribeOptions;
        static MqttHelper()
        {
            mqttFactory = new MqttFactory();
            mqttClient = mqttFactory.CreateMqttClient();
            mqttClientOptions = new MqttClientOptionsBuilder()
                                  .WithTcpServer("101.34.1.127", 1883)
              .WithCredentials("emqx", "Guohao866").WithProtocolVersion(MqttProtocolVersion.V500).Build();
            mqttClientSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
.WithTopicFilter(f => { f.WithTopic("mqttnet/samples/topic/1"); })
.Build();
        }

        public static async Task SendCmdAsync(string stringdata)
        {
            await mqttClient.PublishStringAsync("mqttnet/samples/topic/cmd", stringdata);
        }
        public static async Task Connect_Client_Using_WebSockets(Func<MqttApplicationMessageReceivedEventArgs, Task> callback)
        {
            /*
             * This sample creates a simple MQTT client and connects to a public broker using a WebSocket connection.
             * 
             * This is a modified version of the sample _Connect_Client_! See other sample for more details.
             */

            var response = await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            Console.WriteLine("The MQTT client is connected.");

            await Task.Delay(10000);

            mqttClient.ApplicationMessageReceivedAsync += callback;
            var response2 = await mqttClient.SubscribeAsync(mqttClientSubscribeOptions, CancellationToken.None);


        }
    }
}
