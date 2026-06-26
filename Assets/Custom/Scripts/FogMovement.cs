using System;
using UnityEngine;

namespace Custom
{
    public class FogMovement : MonoBehaviour
    {
        [SerializeField] private RectTransform _startMarker;
        [SerializeField] private RectTransform _finishMarker;
        [SerializeField] private float _speed = 10f;
        
        private RectTransform _rectTransform;
        private float _direction;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _direction = _startMarker.anchoredPosition.x <= _finishMarker.anchoredPosition.x ? 1f : -1f;
        }

        private void Update()
        {
            var position = _rectTransform.anchoredPosition;
            _rectTransform.anchoredPosition = new Vector2(position.x + Time.deltaTime * _speed * _direction, position.y);
         
            if ( Mathf.Abs(_rectTransform.anchoredPosition.x - _finishMarker.anchoredPosition.x) < 0.1f)
                ToStartPos();
        }

        private void ToStartPos()
        {
            _rectTransform.anchoredPosition = _startMarker.anchoredPosition;
        }
    }
}