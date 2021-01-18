using System;
using System.Linq;
using Client.Model;
using Client.Objects;
using Client.Scene;
using UnityEngine;

namespace Client
{
    public class GameLogic
    {
        private readonly GameSettings _gameSettings;
        private readonly UnityScene _scene;
        private World _world;
        
        public GameLogic(GameSettings settings, UnityScene scene)
        {
            _gameSettings = settings; 
            _scene = scene;
        }
        
        public void Init(World world)
        {
            _world = world;
        }

        public World CreateNewWorld(int worldId)
        {
            var worldObject = _scene.CreateWorld(worldId); 
            var world = new World(worldObject);
            return world;
        }

        public bool CanCreateNewGameCell()
        {
            return _world.FieldCells.Any(x => x.GameCell == null); 
        }

        public GameCell CreateNewGameCell()
        {
            var emptyFieldCells = _world.FieldCells.Where(x => x.GameCell == null && x.State == FieldCellState.Active);
            var randomCell = UnityEngine.Random.Range(0, emptyFieldCells.Count());
            var fieldCell = emptyFieldCells.ElementAt(randomCell);
            var gameCell = _scene.CreateGameCell(); 
            var randomColor = UnityEngine.Random.Range(0, 3);
            gameCell.transform.SetParent(_world.WorldObject.GameCellsContainer);
            gameCell.transform.position = fieldCell.transform.position;
            gameCell.Colorize((GameCellColor) randomColor);
            gameCell.FieldCell = fieldCell;
            fieldCell.GameCell = gameCell; 
            return gameCell; 
        }

        public bool ApplyInput(GameInput input)
        {
            var moving = false;
            switch (input.Direction)
            {
                case Direction.Up:
                    moving |= MoveUp(); 
                    break;
                case Direction.Down:
                    moving |= MoveDown(); 
                    break;
                case Direction.Left:
                    moving |= MoveLeft(); 
                    break;
                case Direction.Right:
                    moving |= MoveRight(); 
                    break;
            }

            return moving; 
        }
        
        private bool MoveRight()
        {
            var moving = false;
            for (var i = 0; i < _world.Field.Count; i++)
            {
                var row = _world.Field[i];

                for (var j = row.Count - 2; j >= 0; j--)
                {
                    var fromCell = row[j]; 
                    
                    if (fromCell.GameCell == null)
                        continue;

                    if (fromCell.State == FieldCellState.Inactive)
                        continue;

                    var k = j;
                    while (k < row.Count - 1)
                    {
                        k++; 
                        var nextCell = row[k];
                        if (nextCell.GameCell != null)
                        {
                            if (fromCell.GameCell.CellColor == nextCell.GameCell.CellColor)
                            {
                                break;
                            }

                            k--; 
                            break;
                        }

                        if (nextCell.State == FieldCellState.Inactive)
                        {
                            k--; 
                            break;
                        }
                    }

                    if (k == j)
                        continue;

                    moving = true; 
                    var toCell = row[k];
                    if (toCell.GameCell != null)
                        MergeCell(fromCell, toCell, fromCell.GameCell);
                    else
                        MoveCell(fromCell, toCell, fromCell.GameCell);
                }
            }

            return moving; 
        }

        private bool MoveLeft()
        {
            var moving = false; 
            for (var i = 0; i < _world.Field.Count; i++)
            {
                var row = _world.Field[i];

                for (var j = 1; j < row.Count; j++)
                {
                    var fromCell = row[j]; 
                    
                    if (fromCell.GameCell == null)
                        continue;

                    if (fromCell.State == FieldCellState.Inactive)
                        continue;

                    var k = j;
                    while (k > 0)
                    {
                        k--; 
                        var nextCell = row[k];
                        if (nextCell.GameCell != null)
                        {
                            if (fromCell.GameCell.CellColor == nextCell.GameCell.CellColor)
                            {
                                break;
                            }

                            k++; 
                            break;
                        }

                        if (nextCell.State == FieldCellState.Inactive)
                        {
                            k++; 
                            break;
                        }
                    } 
                    
                    if (k == j)
                        continue;

                    moving = true; 
                    var toCell = row[k];
                    if (toCell.GameCell != null)
                        MergeCell(fromCell, toCell, fromCell.GameCell);
                    else
                        MoveCell(fromCell, toCell, fromCell.GameCell);
                }
            }

            return moving; 
        }
        
        private bool MoveUp()
        {
            var moving = false;
            var colomnsCount = _world.Field[0].Count; 
            for (var i = 0; i < colomnsCount; i++)
            {
                var index = i;
                var colomn = _world.Field.Select(x => x[index]).ToList();

                for (var j = 1; j < colomn.Count; j++)
                {
                    var fromCell = colomn[j]; 
                    
                    if (fromCell.GameCell == null)
                        continue;

                    if (fromCell.State == FieldCellState.Inactive)
                        continue;

                    var k = j;
                    while (k > 0)
                    {
                        k--; 
                        var nextCell = colomn[k];
                        if (nextCell.GameCell != null)
                        {
                            if (fromCell.GameCell.CellColor == nextCell.GameCell.CellColor)
                            {
                                break;
                            }

                            k++; 
                            break;
                        }

                        if (nextCell.State == FieldCellState.Inactive)
                        {
                            k++; 
                            break;
                        }
                    } 
                    
                    if (k == j)
                        continue;

                    moving = true; 
                    var toCell = colomn[k];
                    if (toCell.GameCell != null)
                        MergeCell(fromCell, toCell, fromCell.GameCell);
                    else
                        MoveCell(fromCell, toCell, fromCell.GameCell);
                }
            }

            return moving; 
        }
        
        private bool MoveDown()
        {
            var moving = false;
            var colomnsCount = _world.Field[0].Count; 
            for (var i = 0; i < colomnsCount; i++)
            {
                var index = i;
                var colomn = _world.Field.Select(x => x[index]).ToList();

                for (var j = colomn.Count - 2; j >= 0; j--)
                {
                    var fromCell = colomn[j]; 
                    
                    if (fromCell.GameCell == null)
                        continue;

                    if (fromCell.State == FieldCellState.Inactive)
                        continue;

                    var k = j;
                    while (k < colomn.Count - 1)
                    {
                        k++; 
                        var nextCell = colomn[k];
                        if (nextCell.GameCell != null)
                        {
                            if (fromCell.GameCell.CellColor == nextCell.GameCell.CellColor)
                            {
                                break;
                            }

                            k--; 
                            break;
                        }

                        if (nextCell.State == FieldCellState.Inactive)
                        {
                            k--; 
                            break;
                        }
                    } 
                    
                    if (k == j)
                        continue;

                    moving = true; 
                    var toCell = colomn[k];
                    if (toCell.GameCell != null)
                        MergeCell(fromCell, toCell, fromCell.GameCell);
                    else
                        MoveCell(fromCell, toCell, fromCell.GameCell);
                }
            }

            return moving; 
        }

        private void MergeCell(FieldCell from, FieldCell to, GameCell gameCell)
        {
            gameCell.NeedDelete = true;
            gameCell.FieldCell = to; 
            from.GameCell = null;

        } 

        private void MoveCell(FieldCell from, FieldCell to, GameCell gameCell)
        {
            to.GameCell = gameCell;
            from.GameCell = null;
            gameCell.FieldCell = to; 
        }
    }
}
