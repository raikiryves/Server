using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenServer.game.props
{
    [BsonIgnoreExtraElements]
    public class PlayerProps
    {
       
        public int Exp { get; set; }
        public int Level { get; set; }
        public int Coins { get; set; }
        public int Stellarite { get; set; }

        public int Essence { get; set; }

        public PlayerProps() { 
            this.Exp= 0;
            this.Level = 1;
            this.Coins = 0;
            this.Stellarite = 0;
            this.Essence = 80;
        }
    }
}
