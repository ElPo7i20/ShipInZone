using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Boundary
{
    public class LocationBoundary : MonoBehaviour
    {
        [SerializeField] private Vector2 _size = new Vector2(20f, 20f);
        [SerializeField] private Transform _ship;
        [SerializeField] private float _warningDelay = 1f;
        [SerializeField] private float _exitCountdown = 5f;
        public event Action<float> OnWarningStarted;
        public event Action OnWarningCancelled;
        public event Action<float> OnCountdownChanged;
        public event Action OnLocationExit;
        private Coroutine _exitRoutine;
        private bool _isOutside;

        private void Update()
        {
            bool isOutside = !IsInside(_ship.position);

            if (isOutside && !_isOutside )
            {
                HandleExit();
            }
            else if (!isOutside && _isOutside)
            {
                HandleReturn();
            }
            _isOutside = isOutside;
        }
        private bool IsInside(Vector3 position)
        {
            Vector3 localPosition = transform.InverseTransformPoint(position);
            return Mathf.Abs(localPosition.x) <= _size.x * 0.5f && Mathf.Abs(localPosition.z) <= _size.y * 0.5f;
        }
        private void HandleExit()
        {
            if (_exitRoutine != null) return;
            _exitRoutine = StartCoroutine(ExitRoutine());
        }
        private void HandleReturn()
        {
            CancelExitRoutine();
        }
        private IEnumerator ExitRoutine()
        {
            yield return new WaitForSeconds(_warningDelay);
            if(!_isOutside)
            {
                _exitRoutine = null;
                yield break;
            }
            OnWarningStarted?.Invoke(_exitCountdown);
            float remainingTime = _exitCountdown;
            while (remainingTime > 0f)
            {
                if (!_isOutside)
                {
                    _exitRoutine = null;
                    yield break;
                }
                OnCountdownChanged?.Invoke(remainingTime);
                yield return new WaitForSeconds(1f);
                remainingTime -= 1f;
            }
            OnCountdownChanged?.Invoke(0f);
            OnLocationExit?.Invoke();
            _exitRoutine = null;
        }
        private void CancelExitRoutine()
        {
            if (_exitRoutine == null) return;
            StopCoroutine(_exitRoutine);
            _exitRoutine = null;
            OnWarningCancelled?.Invoke();
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(_size.x, 0f, _size.y));
        }
    }
}
