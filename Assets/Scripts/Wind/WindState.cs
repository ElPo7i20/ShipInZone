using System;
using UnityEngine;

namespace Assets.Scripts.Wind
{
    public class WindState : MonoBehaviour
    {
        [SerializeField, Range(0f, 360f)] private float _direction;

        [SerializeField, Range(0f, 1f)] private float _strength;

        public float Direction => _direction;
        public float Strength => _strength;

        public event Action<float, float> OnWindChanged;

        private void OnValidate()
        {
            _direction = Mathf.Repeat(_direction, 360f);
            _strength = Mathf.Clamp01(_strength);

            OnWindChanged?.Invoke(_direction, _strength);
        }

        public void SetStrength(float strength)
        {
            strength = Mathf.Clamp01(strength);

            if (Mathf.Approximately(_strength, strength))
                return;
            _strength = strength;
            OnWindChanged?.Invoke(_direction, strength);
        }
    }
}
