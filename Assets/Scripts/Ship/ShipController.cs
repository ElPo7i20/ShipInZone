using System;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class ShipController : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 90f;

        [SerializeField] private float _movementSpeed = 3f;

        private void Update()
        {
            HandleRotation();
            HandleMovement();
        }

        private void HandleRotation()
        {
            float rotationInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up, rotationInput * _rotationSpeed * Time.deltaTime);
        }

        private void HandleMovement()
        {
            float movementInput = Input.GetAxis("Vertical");
            transform.position += -(transform.forward) * movementInput * _movementSpeed * Time.deltaTime;
        }
    }
}
