using DG.Tweening;
using UnityEngine;

namespace Client.Objects
{
    public class GameCell : Cell
    {
        public GameCellColor CellColor;
        public FieldCell FieldCell; 
        public bool NeedDelete { get; set; }

        [SerializeField] private Color _red;
        [SerializeField] private Color _blue;
        [SerializeField] private Color _green;
        
        public Sequence Sequence { get; set; }
        private Sequence _sequenceAppearance;

        private void OnEnable()
        {
            transform.localScale = Vector3.zero;
            _sequenceAppearance?.Kill();
            _sequenceAppearance = DOTween.Sequence()
                .AppendInterval(.3f)
                .Append(transform.DOScale(Vector3.one, .3f)); 
        }

        public void MoveToFieldCell()
        {
            var targetPosition = FieldCell.transform.position;
            var distance = (targetPosition - transform.position).magnitude; 
            Sequence?.Kill();
            Sequence = DOTween.Sequence()
                .Append(transform.DOMove(targetPosition, distance / 100f * 0.1f).SetEase(Ease.InOutQuart))
                .AppendCallback(() =>
                {
                    if (NeedDelete)
                    {
                        Destroy(gameObject);
                    }

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