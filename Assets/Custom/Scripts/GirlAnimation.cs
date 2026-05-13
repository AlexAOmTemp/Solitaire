using System;
using UnityEngine;

namespace Custom
{
    public class GirlAnimation : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float radius = 10f;          
        [SerializeField] private float rotationSpeed = 2f;    

        [Header("Scale")]
        [SerializeField] private float pulseAmplitude = 0.05f; 
        [SerializeField] private float pulseSpeed = 3f;        

        private Vector3 startPos;
        private Vector3 startScale;

        private void Awake()
        {
            startPos = transform.localPosition;
            startScale = transform.localScale;
        }

        private void Update()
        {
            float angle = Time.time * rotationSpeed;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            transform.localPosition = startPos + new Vector3(x, y, 0f);

            float scale = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmplitude;
            transform.localScale = startScale * scale;
        }
    }
}