﻿using LiteDB;

namespace donniebot.classes
{
    public class ApiKey
    {
        public string Service { get; set; }
        public string Key { get; set; }
        [BsonId]
        public int Id { get; set; }
    }
}