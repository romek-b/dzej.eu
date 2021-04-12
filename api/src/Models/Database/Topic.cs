using MongoDB.Bson;

namespace DzejEu.Api.Models.Database
{
    public class Topic 
    {
        public ObjectId Id { get; set; }
        
        public string Value { get; set; }
    }
}