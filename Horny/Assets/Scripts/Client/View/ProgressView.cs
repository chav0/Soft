using UnityEngine;
using UnityEngine.UI;

namespace Client.View
{
    public class ProgressView : MonoBehaviour
    {
        [SerializeField] private Image _progress;
        [SerializeField] private StarObject[] _stars;

        private int _score; 

        public void SetProgress(int score, int firstScore, int secondScore, int thirdScore, int stars)
        {
            if (_score == score)
                return;

            _score = score; 
            
            for (var i = 0; i < _stars.Length; i++)
            {
                var star = _stars[i]; 
                star.SetReceived(i + 1 <= stars);
            }

            switch (stars)
            {
                case 0:
                    _progress.fillAmount = score / (float) firstScore * 0.5f; 
                    break;
                case 1:
                    _progress.fillAmount = (score - firstScore) / (float) (secondScore - firstScore) * 0.25f + 0.5f; 
                    break;
                case 2:
                    _progress.fillAmount = (score - secondScore) / (float) (thirdScore - secondScore) * 0.25f + 0.75f; 
                    break;
                case 3:
                    _progress.fillAmount = 1f; 
                    break;
            }
        }
    }
}
