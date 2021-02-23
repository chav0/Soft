using System;
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
        [SerializeField] private Color _orange;
        [SerializeField] private Color _sky;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private float _speed; 

        private Sequence _sequence;
        private Sequence _sequenceAppearance;
        
        public bool IsMoving { get; private set; }

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
            _score.gameObject.SetActive(false);
            
            var targetPosition = FieldCell.transform.position;
            var distance = (targetPosition - transform.position).magnitude;

            if (distance > 0)
                IsMoving = true; 

            _sequence?.Kill();
            _sequence = DOTween.Sequence()
                .Append(transform.DOMove(targetPosition, distance * _speed).SetEase(Ease.InOutQuart))
                .AppendCallback(() => IsMoving = false);
            if (score != 0)
            {
                _score.text = $"+{score}";
                _score.transform.localScale = Vector3.zero;
                var rect = _score.GetComponent<RectTransform>(); 
                rect.anchoredPosition = Vector2.zero;
                _score.gameObject.SetActive(true);
                _sequence.Append(_score.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack))
                    .Join(rect.DOAnchorPosY(150f, 1f).SetEase(Ease.OutBack))
                    .Join(transform.DOScale(1.1f, 0.2f))
                    .Insert(distance * _speed + 0.2f, transform.DOScale(1f, 0.15f))
                    .AppendCallback(() =>
                    {
                        _score.gameObject.SetActive(false);
                    });
            }

            _sequence.AppendCallback(() =>
            {
                if (NeedDelete) Destroy(gameObject);

                _sequence = null;
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
                case GameCellColor.Orange:
                    Image.color = _orange;
                    break;
                case GameCellColor.Sky:
                    Image.color = _sky;
                    break;
            }
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
            _sequenceAppearance?.Kill();
        }
    }

    public enum GameCellColor
    {
        None,
        Red,
        Green,
        Blue,
        Orange,
        Sky
    }
}