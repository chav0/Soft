using System;
using System.Collections.Generic;
using System.Linq;
using Client.Objects;
using Client.View;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Client.Scene
{
    public class UnityScene : IDisposable
    {
        private readonly Resources _resources;
        private readonly Screens _screens;
        private readonly List<GameObject> _createdObjects = new List<GameObject>();
        
        public UnityScene(Resources resources, Screens screens)
        {
            _resources = resources;
            _screens = screens;
        }

        public WorldObject CreateWorld(int worldId)
        {
            var prefab = _resources.WorldBlueprints.FirstOrDefault(blueprint => blueprint.WorldId == worldId)?.WorldObject;

            if (prefab == null)
                return null; 
            
            var worldObject = Object.Instantiate(prefab, _screens.WorldContainer); 
            _createdObjects.Add(worldObject.gameObject);
            return worldObject; 
        }

        public GameCell CreateGameCell()
        {
            var cellObject = Object.Instantiate(_resources.GameCellPrefab, _screens.WorldContainer); 
            _createdObjects.Add(cellObject.gameObject);
            return cellObject; 
        }

        public void Dispose()
        {
            foreach (var go in _createdObjects)
            {
                Object.Destroy(go);
            }
        }
    }
}