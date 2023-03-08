using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using HiddenServer.game.props;

namespace ServerApplication
{
    class Player
    {
        public int id;
        public string username;
        public PlayerProps props;
        public Vector3 position;
        public Quaternion rotation;

        public Player(int id, string name)
        {
            this.id = id;
            this.username = name;
            this.props = new PlayerProps();
        }

        public Player(int id,string name,Vector3 pos)
        {
            this.id = id;
            this.username = name;
            this.position = pos;
            this.rotation = Quaternion.Identity;
            this.props = new PlayerProps();
        }

    }
}
