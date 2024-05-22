using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    public class GameInfo
    {
        public GameInfo() { }

        public GameInfo(GameInfo g)
        {
            Id = g.Id;
            Players = new List<PlayerInfo>(g.Players);
        }

        public int Id { get; set; }
        public List<PlayerInfo> Players { get; set; } = new List<PlayerInfo>();
    }
}

