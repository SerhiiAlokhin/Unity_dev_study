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

        private Vector2 _targetVector;
        private float _addValue;
        private const int LevelWidth = 13;

        private void Awake()
        {
            _inputSystem = new();
            _inputSystem.SubscribeEvents();
            SubscribeEvents();

        }

        private void SubscribeEvents()
        {
            _inputSystem.MovementRecieved += OnMovementRecieved;
            _inputSystem.MovementEnd += OnMovementEnded;

        }

        private void UnsubscribeEvents()
        {
            _inputSystem.MovementRecieved -= OnMovementRecieved;
            _inputSystem.MovementEnd -= OnMovementEnded;
        }

        private void OnMovementRecieved(Vector2 movement)
        {
            Debug.Log("move " + movement);
            _addValue = movement.x / _controllerSlow;

            Debug.Log("_targetVector " + _targetVector);


        }

        private void OnMovementEnded()
        {
            _targetVector = _basicRunner.motion.offset;
            _addValue = 0;
        }
        private void Update()
        {
            _targetVector = new Vector2(Mathf.Clamp(_targetVector.x + _addValue, -LevelWidth, LevelWidth), 0);
            var finalOffset = Vector2.MoveTowards(_basicRunner.motion.offset, _targetVector, _slideSpeed * Time.deltaTime);
            _basicRunner.motion.offset = finalOffset;

            Debug.Log("Offset: " + _basicRunner.motion.offset);
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
            _inputSystem.Dispose();
        }
    }
}
