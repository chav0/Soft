using System.Collections.Generic;
using UnityEngine;

namespace Client.View
{
    public class ChooseWorldMenu : MonoBehaviour
    {
        public WorldButton Prefab;
        public List<WorldButton> Buttons { get; } = new List<WorldButton>();

        public WorldButton CreateButton(WorldBlueprint worldPrefab)
        {
            var worldButton = Instantiate(Prefab, transform, false); 
            worldButton.WorldId.text = worldPrefab.WorldId.ToString();
            worldButton.gameObject.SetActive(true);
            Buttons.Add(worldButton);
            return worldButton; 
        }
    }
}