
using Iot.Device.Hcsr04;
using System.Device.Gpio.Drivers;
using System.Device.Gpio;
using UnitsNet;

using SkiaSharp;
using Sang.IoT.SSD1306;
using PiDemo;
using System;
using MQTTnet.Client;

namespace PiDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                await MqttHelper2.Connect_Client_Using_MQTTv5(new Func<MqttApplicationMessageReceivedEventArgs, Task>(CallbackAsync));
            });

            using Hcsr04 sonar = new(17, 18);
            while (true)
            {
                if (sonar.TryGetDistance(out Length distance))
                {
                    Console.WriteLine($"Distance: {distance.Centimeters} cm");
                    Show($"{Math.Round(distance.Centimeters, 2)} cm");
                    Task.Run(async () =>
                    {
                        await MqttHelper2.PublishStringAsync($"{Math.Round(distance.Centimeters, 2)} cm");
                    });
                }
                else
                {
                    Console.WriteLine("Error reading sensor");
                }

                Thread.Sleep(1000);
            }
        }

        static void Show(string sss)
        {
            using (var oled = new SSD1306_128_64(1))
            {
                oled.Begin();
                //oled.Clear();

                using (var bitmap = new SKBitmap(128, 64, true))
                {
                    SKCanvas canvas = new SKCanvas(bitmap);
                    SKPaint paint = new SKPaint()
                    {
                        Color = new SKColor(255, 255, 255),
                        StrokeWidth = 1, //画笔宽度
                        Typeface = SKTypeface.FromFile("/home/pi/Documents/net-iot/SourceHanSansCN-Normal.otf"),
                        TextSize = 13, //字体大小
                        Style = SKPaintStyle.Fill,
                    };

                    canvas.DrawText(sss, 0, 13, paint);
                    paint.TextSize = 50;
                    //canvas.DrawText("Masa IOT", 0, 50, paint);
                    oled.Image(bitmap.Encode(SKEncodedImageFormat.Png, 100).ToArray());
                }


                oled.Display();
            }
        }
        static void RedOn(bool isOn)
        {
            Console.WriteLine("Blinking LED. Press Ctrl+C to end.");
            int pin = 16;
            using var controller = new GpioController();
            controller.OpenPin(pin, PinMode.Output);

            controller.Write(pin, ((isOn) ? PinValue.High : PinValue.Low));
        }
        public static async Task CallbackAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            
            var cmd = System.Text.Encoding.Default.GetString(e.ApplicationMessage.Payload);
            Console.WriteLine(cmd);
            if (cmd=="On")
            {
                RedOn(true);
            }
            else
            {
                RedOn(false);
            }
            await Task.CompletedTask;
        }
    }
}
