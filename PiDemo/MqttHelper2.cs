using MQTTnet.Client;
using MQTTnet.Formatter;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiDemo
{
    public static class MqttHelper2
    {
        private static MqttFactory mqttFactory;
        private static IMqttClient mqttClient;
        private static MqttClientOptions mqttClientOptions;
        static MqttHelper2()
        {
            mqttFactory = new MqttFactory();
            mqttClient = mqttFactory.CreateMqttClient();
            mqttClientOptions = new MqttClientOptionsBuilder()
                                  .WithTcpServer("192.120.5.206", 1883)
              .WithCredentials("emqx", "Guohao866").WithProtocolVersion(MqttProtocolVersion.V500).Build();

        }
        public static async Task Connect_Client_Using_MQTTv5(Func<MqttApplicationMessageReceivedEventArgs, Task> callback)
        {
            var response = await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            Console.WriteLine("The MQTT client is connected.");

            response.DumpToConsole();

            var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => { f.WithTopic("mqttnet/samples/topic/cmd"); })
                .Build();


            mqttClient.ApplicationMessageReceivedAsync += callback;
            Console.WriteLine("start to SubscribeAsync.");
            var response2 = await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

            Console.WriteLine("MQTT client subscribed to topic.");

   
           response.DumpToConsole();

        }
        public static async Task PublishStringAsync(string stringdata)
        {
            await mqttClient.PublishStringAsync("mqttnet/samples/topic/1", stringdata);
        }
    }
}
