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
        
        public World(WorldObject worldObject)
        {
            WorldObject = worldObject; 
            Field = new List<List<FieldCell>>();
            FieldCells = new List<FieldCell>();
            foreach (var row in WorldObject.Rows)
            {
                Field.Add(row.Colomn);
                FieldCells.AddRange(row.Colomn);
            }

            GameCells = WorldObject.InitialGameCells; 
        }
        
        public void Dispose()
        {
        }
    }
}
