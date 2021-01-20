using System;
using Client.Model;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Client.View
{
    public class SwipeController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private int _swipeCount;
        private int _processedSwipeCount;
        private bool _dragging; 
        private Direction? _direction; 

        public GameInput GetInput()
        {
            if (!_dragging)
                return null;

            if (_processedSwipeCount >= _swipeCount)
                return null;

            if (_direction == null)
                return null;

            _processedSwipeCount = _swipeCount; 
            
            return new GameInput
            {
                Direction = _direction.Value
            };
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _swipeCount++;
            _dragging = false; 
            _direction = null; 
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.dragging && eventData.delta.magnitude > 10f)
            {
                _dragging = true; 
                var normalizedDirection = eventData.delta.normalized; 
                var rightAngle = Mathf.Abs(Vector2.Angle(normalizedDirection, Vector2.right));
                var leftAngle = Mathf.Abs(Vector2.Angle(normalizedDirection, Vector2.left)); 
                var upAngle = Mathf.Abs(Vector2.Angle(normalizedDirection, Vector2.up)); 
                var downAngle = Mathf.Abs(Vector2.Angle(normalizedDirection, Vector2.down));

                var minAngle = Mathf.Min(rightAngle, leftAngle, upAngle, downAngle);
                
                if (Math.Abs(rightAngle - minAngle) < 1)
                    _direction = Direction.Right;
                if (Math.Abs(leftAngle - minAngle) < 1)
                    _direction = Direction.Left;
                if (Math.Abs(upAngle - minAngle) < 1)
                    _direction = Direction.Up;
                if (Math.Abs(downAngle - minAngle) < 1)
                    _direction = Direction.Down;
            }
            else
            {
                _dragging = false; 
                _direction = null; 
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _dragging = false; 
            _direction = null; 
        }
    }
}