using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppPiDemo.Data
{
        public class MqttMessage
        {
            public string ClientId { get; set; }
            public Applicationmessage ApplicationMessage { get; set; }
            public bool ProcessingFailed { get; set; }
            public int ReasonCode { get; set; }
            public object[] ResponseUserProperties { get; set; }
            public object ResponseReasonString { get; set; }
            public bool IsHandled { get; set; }
            public bool AutoAcknowledge { get; set; }
            public object Tag { get; set; }
            public int PacketIdentifier { get; set; }
        }

        public class Applicationmessage
        {
            public object ContentType { get; set; }
            public object CorrelationData { get; set; }
            public bool Dup { get; set; }
            public int MessageExpiryInterval { get; set; }
            public string Payload { get; set; }
            public int PayloadFormatIndicator { get; set; }
            public int QualityOfServiceLevel { get; set; }
            public object ResponseTopic { get; set; }
            public bool Retain { get; set; }
            public object SubscriptionIdentifiers { get; set; }
            public string Topic { get; set; }
            public int TopicAlias { get; set; }
            public object UserProperties { get; set; }
        }
}
