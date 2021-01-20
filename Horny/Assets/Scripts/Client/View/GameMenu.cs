using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.View
{
    public class GameMenu : MonoBehaviour
    {
        public Button Exit;
        public TMP_Text ScoreLbl; 
        public TMP_Text RecordLbl; 
        public SwipeController SwipeController;

        private int? _score;
        private int? _record; 

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
    }
}