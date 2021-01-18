using System;
using System.Collections.Generic;
using UnityEngine;

namespace Client.Objects
{
    public class WorldObject : MonoBehaviour
    {
        public List<Row> Rows;
        public List<GameCell> InitialGameCells;
        public Transform GameCellsContainer; 
    }
    
    [Serializable]
    public class Row
    {
        public List<FieldCell> Colomn;
    }
}
