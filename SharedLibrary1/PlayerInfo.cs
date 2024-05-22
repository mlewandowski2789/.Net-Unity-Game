using System.Collections.Generic;

namespace SharedLibrary
{
    public class PlayerInfo
    {
        public int Id { get; set; }
        public float Health { get; set; }

        public List<BulletInfo> Bullets { get; set; } = new List<BulletInfo>();

        public float[] Position { get; set; } = new float[3];
        public float[] Velocity { get; set; } = new float[3];
        public float[] Rotation { get; set; } = new float[3];
    }
}


