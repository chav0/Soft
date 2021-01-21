using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.View
{
    public class ResultScreen : MonoBehaviour
    {
        public Button Menu;
        public Button NextLevel;
        public Button Restart; 
        [SerializeField] private TMP_Text _win; 
        [SerializeField] private TMP_Text _score; 
        [SerializeField] private TMP_Text _level;
        [SerializeField] private StarObject[] _stars;

        public void SetResults(int score, int stars, int worldId)
        {
            var win = stars > 0;

            _win.text = win ? "You won!" : "You lose!"; 
            NextLevel.gameObject.SetActive(win);
            Restart.gameObject.SetActive(!win);
            _score.text = $"Score: <color=#3561AC> {score} </color>";

            for (int i = 0; i < _stars.Length; i++)
            {
                _stars[i].SetReceived(i + 1 <= stars);
            }

            _level.text = $"World {worldId}"; 
        }
    }
}
