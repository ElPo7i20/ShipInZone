using Assets.Scripts.Wind;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class WindUI : MonoBehaviour
    {
        [SerializeField] private WindState _windState;
        [SerializeField] private Transform _ship;
        [SerializeField] private RectTransform _windIndicator;
        [SerializeField] private TMP_Text _directionText;
        [SerializeField] private TMP_Text _strengthText;
        [SerializeField] private TMP_Text _relativeAngleText;

        private void OnEnable()
        {
            if (_windState != null)
            {
                _windState.OnWindChanged += HandleWindChanged;
            }
            UpdateUI();
        }

        private void OnDisable()
        {
            if (_windState != null)
            {
                _windState.OnWindChanged -= HandleWindChanged;
            }
        }

        private void Update()
        {
            UpdateRelativeAngle();
        }

        private void HandleWindChanged(float direction, float strength)
        {
            UpdateWindUI(direction, strength);
            UpdateRelativeAngle();
        }

        private void UpdateUI()
        {
            UpdateWindUI(_windState.Direction, _windState.Strength);
            UpdateRelativeAngle();
        }
        private void UpdateWindUI(float direction, float strength)
        {
            _directionText.text = $"Direction: {direction:0}°";
            _strengthText.text = $"Strength: {strength:0.00}";
            _windIndicator.localRotation = Quaternion.Euler(0f,0f, -(direction + 180f));
        }
        private void UpdateRelativeAngle()
        {
            if (_ship == null && _windState == null) return;

            float shipDirection = _ship.eulerAngles.y;
            float relativeAngle = Mathf.DeltaAngle(shipDirection, _windState.Direction);

            _relativeAngleText.text = $"Relative angle: {relativeAngle:+0;-0;0}°";
        }
    }
}
