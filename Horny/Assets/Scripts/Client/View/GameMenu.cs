using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.View
{
    public class GameMenu : MonoBehaviour
    {
        public Button Exit;
        public Button Restart;
        public TMP_Text ScoreLbl; 
        public TMP_Text RecordLbl; 
        public TMP_Text SwipeCountLbl; 
        public SwipeController SwipeController;
        public ProgressView ProgressView;

        private int? _score;
        private int? _record; 
        private int? _swipes; 

        public void SetScoreAndRecord(int score, int record)
        {
            if (_score != score)
            {
                _score = score;
                ScoreLbl.text = score.ToString(); 
            }

            if (_record != record)
            {
                _record = record;
                RecordLbl.text = record.ToString(); 
            }
        }

        public void SetSwipeCount(int swipes)
        {
            if (_swipes != swipes)
            {
                _swipes = swipes;
                SwipeCountLbl.text = swipes.ToString(); 
            }
        }
    }
}