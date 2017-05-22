using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace GoMicro.Forex.Models
{
    public class CommonResult
    {
        public CommonResult(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
    }


    public class Fixer
    {
        [BsonId]
        public MongoDB.Bson.ObjectId _id { get; set; }
        public string @base { get; set; }
        public string date { get; set; }
        public Dictionary<string, double> rates { get; set; }
    }
}
