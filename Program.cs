using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace DCEprocessor
{
    class Program
    {
        public static void printm(List<Message> m, int interval = 100, bool includetime = true, bool includeauthor = true)
        {
            foreach (Message msg in m) { printm(msg, includetime, includeauthor); if (interval > 0) Thread.Sleep(interval); }
        }
        public static void printm(Message m, bool includetime = true, bool includeauthor = true)
        {
            Console.WriteLine($"{(includetime ? $"[{DateTimeOffset.FromUnixTimeSeconds(m.UnixTime):dd/MM/yyyy HH:mm:ss}] " : "")}{(includeauthor ? $"{m.Author.Name}: " : "")}{m.Content}");
        }
        static void Main(string[] args)
        {
            string[] files = Directory.GetFiles("input");
            List<Message> messages = new List<Message>();
            foreach (string filepath in files)
            {
                List<Message> fileMsgs = JsonConvert.DeserializeObject<Main>(File.ReadAllText(filepath)).Messages;
                messages.AddRange(fileMsgs);
            }
            foreach (Message m in messages)
            {
                m.UnixTime = DateTimeOffset.Parse(m.TimeSent).ToUnixTimeSeconds();
            }
            messages = messages.OrderBy(a => a.UnixTime).ToList();

            //From this point we can do anything with messages.
            ConsoleColor.Green.Print("[TEST 1] List all occurances of 'good'");
            printm(Presets.FindStr(messages, "good"), 500);
            ConsoleColor.Green.Print("[TEST 2] Count all occurances of 'bad'");
            Console.WriteLine($"There are {Presets.CalcStr(messages, "bad")} occurances of 'bad'");
            ConsoleColor.Green.Print("[TEST 3] Print first message");
            printm(messages[0]);
            long ts = 1615173180;
            ConsoleColor.Green.Print($"[TEST 4] Send all messages sent on {DateTimeOffset.FromUnixTimeSeconds(ts):dd/MM/yyyy HH:mm:ss} with the deviation of 300 seconds (5 minutes)");
            printm(Presets.ByTime(messages, ts, 1200));

            File.WriteAllText("final.json", JsonConvert.SerializeObject(messages));
        }
    }

    public partial class Main
    {
        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }
    }

    public partial class Message
    {

        [JsonProperty("isPinned")]
        public bool IsPinned { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("author")]
        public MessageAuthor Author { get; set; }

        [JsonProperty("timestamp")]
        public string TimeSent { get; set; }

        public long UnixTime { get; set; }
    }

    public partial class MessageAuthor
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("discriminator")]
        public string Discriminator { get; set; }

        [JsonProperty("isBot")]
        public bool IsBot { get; set; }
    }
}
