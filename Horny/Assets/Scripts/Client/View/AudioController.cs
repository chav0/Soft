using UnityEngine;

namespace Client.View
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _click;
        [SerializeField] private AudioSource _swipe;
        [SerializeField] private AudioSource _win;
        [SerializeField] private AudioSource _lose;
        [SerializeField] private AudioSource _pointsUp;

        public void PlayClick()
        {
            _click.Play();
        }

        public void PlaySwipe()
        {
            _swipe.Play();
        }

        public void PlayWin()
        {
            _win.Play();
        }

        public void PlayLose()
        {
            _lose.Play();
        }

        public void PlayPointsUp()
        {
            _pointsUp.Play();
        }
    }
}
