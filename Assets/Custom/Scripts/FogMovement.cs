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
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            var position = _rectTransform.anchoredPosition;
            _rectTransform.anchoredPosition = new Vector2(position.x + Time.deltaTime * _speed, position.y);
         
            if (  _rectTransform.anchoredPosition.x >= _finishMarker.anchoredPosition.x)
                ToStartPos();
        }

        private void ToStartPos()
        {
            _rectTransform.anchoredPosition = _startMarker.anchoredPosition;
        }
    }
}