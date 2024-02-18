using UnityEngine;

namespace Player.Controller
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : MonoBehaviour
    {
        [Header("Player Speed Value")]
        [SerializeField] private float _initialFrontSpeed;
        [SerializeField] private float _maxFrontSpeed;
        [SerializeField] private float _playerFrontAcceleration;
        [SerializeField] private float _initialSideAndRearSpeed;

        [Header("Player Character Controller")]
        [SerializeField] private CharacterController _playerCharacterController;

        private float _speed;

        private PlayerInputs _playerInputs;

        private void Awake()
        {
            _playerCharacterController = GetComponent<CharacterController>();
            _playerInputs = new PlayerInputs();
        } 

        private void OnEnable()
        {
            _playerInputs.Enable();
        }

        private void OnDisable()
        {
            _playerInputs.Disable();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            var inputsDirection = _playerInputs.Player.Move.ReadValue<Vector2>();

            if (inputsDirection == Vector2.zero)
            {
                _speed = 0f;
                return;
            }

            var moveDirection = transform.right * inputsDirection.x + transform.forward * inputsDirection.y;
            var scaleDirection = moveDirection * ChangeSpeed(inputsDirection) * Time.deltaTime;

            _playerCharacterController.Move(scaleDirection);
        }

        private float ChangeSpeed(Vector2 inputsDirection)
        {
            if (inputsDirection.y > 0 && inputsDirection.x == 0)
            {
                _speed += _playerFrontAcceleration * Time.deltaTime;
                _speed = Mathf.Clamp(_speed, _initialFrontSpeed, _maxFrontSpeed);
                return _speed;
            }
            else
            {
                _speed = _initialSideAndRearSpeed;
                return _speed;
            }
        }
    }
}