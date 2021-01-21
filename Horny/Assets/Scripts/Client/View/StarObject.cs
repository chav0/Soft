using UnityEngine;
using UnityEngine.UI;

namespace Client.View
{
    public class StarObject : MonoBehaviour
    {
        [SerializeField] private Sprite _receivedStar;
        [SerializeField] private Sprite _emptyStar;
        [SerializeField] private Image _image;

        public void SetReceived(bool received)
        {
            _image.sprite = received ? _receivedStar : _emptyStar; 
        }
    }
}
