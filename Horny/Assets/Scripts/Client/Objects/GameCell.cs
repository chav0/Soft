using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Client.Objects
{
    public class GameCell : Cell
    {
        public GameCellColor CellColor;
        public FieldCell FieldCell; 
        public bool NeedDelete { get; set; }
        public int MergeCount { get; set; }

        [SerializeField] private Color _red;
        [SerializeField] private Color _blue;
        [SerializeField] private Color _green;
        [SerializeField] private TMP_Text _score;
        
        public Sequence Sequence { get; set; }
        private Sequence _sequenceAppearance;

        private void OnEnable()
        {
            transform.localScale = Vector3.zero;
            _sequenceAppearance?.Kill();
            _sequenceAppearance = DOTween.Sequence()
                .AppendInterval(.3f)
                .Append(transform.DOScale(Vector3.one, .2f)); 
        }

        public void MoveToFieldCell(int score)
        {
            var targetPosition = FieldCell.transform.position;
            var distance = (targetPosition - transform.position).magnitude; 

            Sequence?.Kill();
            Sequence = DOTween.Sequence()
                .Append(transform.DOMove(targetPosition, distance / 250f * 0.1f).SetEase(Ease.InOutQuart));
            if (score != 0)
            {
                _score.text = $"+{score}";
                _score.transform.localScale = Vector3.zero;
                var rect = _score.GetComponent<RectTransform>(); 
                rect.anchoredPosition = Vector2.zero;
                rect.gameObject.SetActive(true);
                Sequence.Append(_score.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack))
                    .Join(rect.DOAnchorPosY(150f, 1f).SetEase(Ease.OutBack))
                    .Join(transform.DOScale(1.1f, 0.2f))
                    .Insert(distance / 250f * 0.1f + 0.2f, transform.DOScale(1f, 0.15f))
                    .AppendCallback(() => rect.gameObject.SetActive(false));
            }

            Sequence.AppendCallback(() =>
            {
                if (NeedDelete) Destroy(gameObject);

                Sequence = null;
            });
        }

        public void Colorize(GameCellColor color)
        {
            CellColor = color;
            switch (color)
            {
                case GameCellColor.Blue: 
                    Image.color = _blue;
                    break;
                case GameCellColor.Red: 
                    Image.color = _red;
                    break;
                case GameCellColor.Green: 
                    Image.color = _green;
                    break;
            }
        }

        private void OnDestroy()
        {
            Sequence?.Kill();
            _sequenceAppearance?.Kill();
        }
    }

    public enum GameCellColor
    {
        Red,
        Green,
        Blue
    }
}