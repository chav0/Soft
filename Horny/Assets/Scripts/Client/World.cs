using System;
using System.Collections.Generic;
using Client.Objects;

namespace Client
{
    public sealed class World : IDisposable
    {
        public readonly List<List<FieldCell>> Field; 
        public readonly List<GameCell> GameCells; 
        public readonly List<FieldCell> FieldCells; 
        public readonly WorldObject WorldObject; 
        public int WorldId { get; private set; }
        
        public World(WorldObject worldObject, int worldId)
        {
            WorldObject = worldObject;
            WorldId = worldId; 
            Field = new List<List<FieldCell>>();
            FieldCells = new List<FieldCell>();
            GameCells = new List<GameCell>();
            foreach (var row in WorldObject.Rows)
            {
                Field.Add(row.Colomn);
                FieldCells.AddRange(row.Colomn);
                foreach (var cell in row.Colomn)
                {
                    var gameCell = cell.GetComponentInChildren<GameCell>();
                    if (gameCell != null)
                    {
                        GameCells.Add(gameCell);
                        gameCell.transform.SetParent(WorldObject.GameCellsContainer);
                        gameCell.FieldCell = cell;
                        cell.GameCell = gameCell; 
                    }
                }
            }
        }
        
        public void Dispose()
        {
        }
    }
}
