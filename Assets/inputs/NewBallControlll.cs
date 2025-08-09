using UnityEngine;
using Controllers.Input;
using Dreamteck.Forever;

namespace Game
{
    public class NewBallControll : MonoBehaviour
    {
        private InputSystem _inputSystem;

        [SerializeField] private Runner _basicRunner;
        [SerializeField] private float _slideSpeed = 10f;
        [SerializeField] private float _controllerSlow = 10f;
        [SerializeField] private Animator _animator;

        [SerializeField] private float _preJumpDelay = 0.5f;   
        [SerializeField] private float _jumpHeight = 3f;
        [SerializeField] private float _jumpDuration = 0.5f;

        private float _verticalOffset = 0f;
        private bool _isJumping = false;

        private const string RunningBool = "IsRunning";
        private const string JumpTrigger = "JumpTrigger";

        private Vector2 _targetVector;
        private float _addValue;
        private const int LevelWidth = 13;

        private void Awake()
        {
            _inputSystem = new();
            _animator.SetBool(RunningBool, true);
            _inputSystem.SubscribeEvents();
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _inputSystem.MovementRecieved += OnMovementRecieved;
            _inputSystem.MovementEnd += OnMovementEnded;
            _inputSystem.Jump += OnJumpPressed;
        }

        private void UnsubscribeEvents()
        {
            _inputSystem.MovementRecieved -= OnMovementRecieved;
            _inputSystem.MovementEnd -= OnMovementEnded;
            _inputSystem.Jump -= OnJumpPressed;
        }

        private void OnMovementRecieved(Vector2 movement)
        {
            _addValue = movement.x / _controllerSlow;
        }

        private void OnJumpPressed()
        {
            if (_isJumping) return;

            
            _animator.SetTrigger(JumpTrigger);

            
            StartCoroutine(JumpCoroutine());
        }

        private void OnMovementEnded()
        {
            _targetVector = _basicRunner.motion.offset;
            _addValue = 0;
        }

        private System.Collections.IEnumerator JumpCoroutine()
        {
            _isJumping = true;

            if (_preJumpDelay > 0f)
                yield return new WaitForSeconds(_preJumpDelay);

            float t = 0f;
            while (t < _jumpDuration)
            {
                t += Time.deltaTime;
                float n = Mathf.Clamp01(t / _jumpDuration); 
                _verticalOffset = 4f * _jumpHeight * n * (1f - n);
                yield return null;
            }

            _verticalOffset = 0f;
            _isJumping = false;
        }

        private void Update()
        {
            _targetVector = new Vector2(
                Mathf.Clamp(_targetVector.x + _addValue, -LevelWidth, LevelWidth),
                _verticalOffset
            );

            var finalOffset = Vector2.MoveTowards(
                _basicRunner.motion.offset,
                _targetVector,
                _slideSpeed * Time.deltaTime
            );

            _basicRunner.motion.offset = finalOffset;
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
            _inputSystem.Dispose();
        }
    }
}