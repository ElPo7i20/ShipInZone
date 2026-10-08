using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Boundary
{
    public class BoundaryWarningUI : MonoBehaviour
    {
        [SerializeField] private LocationBoundary _boundary;
        [SerializeField] private GameObject _warningPanel;
        [SerializeField] private TMP_Text _warningText;

        private void OnEnable()
        {
            _boundary.OnWarningStarted += HandleWarningStarted;
            _boundary.OnWarningCancelled += HandleWarningCancelled;
            _boundary.OnCountdownChanged += HandleCountdownChanged;
            _boundary.OnLocationExit += HandleLocationExit;
        }
        private void OnDisable()
        {
            _boundary.OnWarningStarted -= HandleWarningStarted;
            _boundary.OnWarningCancelled -= HandleWarningCancelled;
            _boundary.OnCountdownChanged -= HandleCountdownChanged;
            _boundary.OnLocationExit -= HandleLocationExit;
        }

        private void HandleWarningStarted(float countdown)
        {
            _warningPanel.SetActive(true);
            _warningText.text = $"Вы покидаете игровую область\n{countdown:0}";
        }

        private void HandleWarningCancelled()
        {
            _warningPanel.SetActive(false);
        }

        private void HandleCountdownChanged(float remainingTime)
        {
            _warningText.text = $"Вы покидаете игровую область\n{remainingTime:0}";
        }

        private void HandleLocationExit()
        {
            Debug.Log("Location exited");
            _warningPanel.SetActive(false);
        }
    }
}
