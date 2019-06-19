using System;
using System.IO;
using LiteDB;

namespace LiteDBDemo.Models
{
    public class Bot
    {
        [BsonId]
        public int BotId { get; set; }
        public string Nome { get; set; }

    }
}
