using System;
using Client.Objects;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(fileName = "Resources", menuName = "Settings/Resources")]
    public class Resources : ScriptableObject
    {
        public WorldBlueprint[] WorldBlueprints;
        public GameCell GameCellPrefab; 
    }

    [Serializable]
    public class WorldBlueprint
    {
        public int WorldId;
        public WorldObject WorldObject; 
    }
}
