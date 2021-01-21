using System;
using System.Collections.Generic;

namespace Client
{
    [Serializable]
    public class PlayerProfile
    {
        public int LastCompletedWorld; 
        public List<WorldInfo> WorldInfos = new List<WorldInfo>();
    }

    [Serializable]
    public class WorldInfo
    {
        public int WorldId;
        public int Record;
        public int Stars; 
    }
}

