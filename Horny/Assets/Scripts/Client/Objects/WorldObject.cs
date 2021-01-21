using System;
using System.Collections.Generic;
using UnityEngine;

namespace Client.Objects
{
    public class WorldObject : MonoBehaviour
    {
        public List<Row> Rows;
        public Transform GameCellsContainer;
        public int FirstStarScore;
        public int SecondStarScore;
        public int ThirdStarScore;
        public int SwipeCount; 
        public GameCellColor[] Colors; 
    }
    
    [Serializable]
    public class Row
    {
        public List<FieldCell> Colomn;
    }
}
