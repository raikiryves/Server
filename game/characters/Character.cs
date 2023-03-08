using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenServer.game.characters
{
    [BsonIgnoreExtraElements]
    public class Character
    {
        [BsonId]
        public ObjectId id { get; set; }
        public string name { get; set; }
        public int level { get; set; }

        public int exp { get; set; }

        public int currentEnergy { get; set; }

        public int currentHp { get; set; }
    }
}
