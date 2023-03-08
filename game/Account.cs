﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HiddenServer.game
{

    [BsonIgnoreExtraElements]
    public  class Account
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]  
        public string Id { get; set; }
        public string username { get; set; }


        public Account(string username)
        {
           
            this.username = username;
        }
    }
}
