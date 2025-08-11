using UnityEngine;
using System;
using UnityEngine.InputSystem;



namespace Controllers.Input
{
    public class InputSystem : IDisposable
    {
        private readonly Mycontroller _inputAction;

        public Mycontroller InputAction => _inputAction;

        public event Action<Vector2> MovementRecieved;
        public event Action MovementEnd;
        public event Action EscapeButtonPressed;
        public event Action Jump;

        public InputSystem()
        {
            _inputAction = new Mycontroller();
            _inputAction.Enable();

        }

        public void SubscribeEvents()
        {
            _inputAction.ball.movement.performed += OnMovementPerformed;
            _inputAction.ball.movement.canceled += OnMovementEnd;
            _inputAction.ball.movement.performed += OnEscapePressed;
            _inputAction.ball.Jump.performed += OnJump;
        }

        private void OnJump(InputAction.CallbackContext callbackContext) => Jump?.Invoke();

        private void OnMovementPerformed(InputAction.CallbackContext callbackContext) => MovementRecieved?.Invoke(callbackContext.ReadValue<Vector2>());

        private void OnMovementEnd(InputAction.CallbackContext callbackContext) => MovementEnd?.Invoke();

        private void OnEscapePressed(InputAction.CallbackContext callbackContext) => EscapeButtonPressed?.Invoke();

        public void Dispose()
        {
            _inputAction.ball.movement.performed -= OnMovementPerformed;
            _inputAction.ball.movement.canceled -= OnMovementEnd;
            _inputAction.ball.movement.performed -= OnEscapePressed;
            _inputAction.ball.Jump.performed -= OnJump;
        }

    }
}

