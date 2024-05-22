using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    public class GameInfo
    {
        public int Id { get; set; }
        public List<PlayerInfo> Players { get; set; } = new List<PlayerInfo>();
    }
}

