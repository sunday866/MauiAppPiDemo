
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Formatter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using MQTTnet.Extensions.WebSocket4Net;
using PiDemo;
using System.Device.Gpio;
using MQTTnet.Server;

namespace PiDemo
{
    public static class LedHelper
    {
        private static GpioController controller;
        static LedHelper()
        {
            controller = new GpioController();
            controller.OpenPin(16, PinMode.Output);

        }
        static void RedOn(bool isOn)
        {
            var msg = "LED is On";
            if (!isOn)
            {
                msg = "LED is Off";
            }
            Console.WriteLine(msg);
            controller.Write(16, ((isOn) ? PinValue.High : PinValue.Low));
        }
    }
}