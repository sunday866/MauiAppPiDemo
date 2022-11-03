// See https://aka.ms/new-console-template for more information
using MQTTnet.Client;
using PiDemo;
using UnitsNet;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                await MqttHelper2.Connect_Client_Using_MQTTv5(
                    new Func<MqttApplicationMessageReceivedEventArgs, Task>(CallbackAsync));
            });
            while (true)
            {
                Task.Run(async () =>
                {
                    await MqttHelper2.PublishStringAsync($"gh");
                });
                Console.WriteLine("Hello, World!");
                Thread.Sleep(1000);
            }
            
            
        }


        private static async Task CallbackAsync(MqttApplicationMessageReceivedEventArgs e)
        {


            await Task.CompletedTask;
        }
    }
}