using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VCCorp.IG.Core.Helper;

namespace VCCorp.IG.Core.DTO.Kafka
{
    public class Kafka
    {
        // example https://docs.confluent.io/clients-confluent-kafka-dotnet/current/overview.html
        public static string _topicTableName = "datacollection-instagram-crawler";
        public static string _commentTableName = "datacollection-instagramcomment-crawler";
        private const string SERVER_LINK = "10.3.48.81:9092,10.3.48.90:9092,10.3.48.91:9092";

        /// <summary>
        /// Gui post INS
        /// </summary>
        /// <param name="messagejson"></param>
        /// <param name="timeOutSeconds"></param>
        /// <returns></returns>
        public static async Task<string> PutOnKafkaPostINS(string messagejson, double timeOutSeconds = 0)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = SERVER_LINK,
                ClientId = Dns.GetHostName(),
                Partitioner = Confluent.Kafka.Partitioner.Random,
                LingerMs = 100,
                BatchSize = 64 * 1024,
                Acks = Acks.Leader
            };

            CancellationToken token = default;
            var random = new Random();
            if (timeOutSeconds > 0)
            {
                token = ThreadHelper.GetCancellationToken(TimeSpan.FromSeconds(timeOutSeconds));
            }

            using (var producer = new ProducerBuilder<string, string>(config).Build())
            {
                var a = await producer.ProduceAsync(_topicTableName, new Message<string, string> { Key = random.Next().ToString(), Value = messagejson }, token);

                if (timeOutSeconds > 0 && token.IsCancellationRequested)
                {
                    //  $"Put on kafka take time too long: {messagejson.GetRangeOrRemain(0, 100)}".ConsoleWriteLine();
                }
                return a.Value + a.TopicPartition;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messagejson"></param>
        /// <param name="timeOutSeconds"></param>
        /// <returns></returns>
        public static async Task<string> PutOnKafkaCmtINS(string messagejson, double timeOutSeconds = 0)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = SERVER_LINK,
                ClientId = Dns.GetHostName(),
                Partitioner = Confluent.Kafka.Partitioner.Random,
                LingerMs = 100,
                BatchSize = 64 * 1024,
                Acks = Acks.Leader
            };

            CancellationToken token = default;
            var random = new Random();
            if (timeOutSeconds > 0)
            {
                token = ThreadHelper.GetCancellationToken(TimeSpan.FromSeconds(timeOutSeconds));
            }

            using (var producer = new ProducerBuilder<string, string>(config).Build())
            {
                var a = await producer.ProduceAsync(_commentTableName, new Message<string, string> { Key = random.Next().ToString(), Value = messagejson }, token);

                if (timeOutSeconds > 0 && token.IsCancellationRequested)
                {
                    //  $"Put on kafka take time too long: {messagejson.GetRangeOrRemain(0, 100)}".ConsoleWriteLine();
                }
                return a.Value + a.TopicPartition;
            }
        }
    }
}
