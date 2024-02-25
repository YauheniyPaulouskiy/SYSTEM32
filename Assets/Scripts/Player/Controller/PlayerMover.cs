using GameManager.PauseGame;
using Player.Animation;
using UnityEngine;

namespace Player.Controller
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent (typeof(PlayerAnimation))]
    public class PlayerMover : MonoBehaviour
    {
        [Header("Speed Value")]
        [SerializeField] private float _initialFrontSpeed;
        [SerializeField] private float _maxFrontSpeed;
        [SerializeField] private float _playerFrontAcceleration;
        [SerializeField] private float _initialSideAndRearSpeed;

        [Header("Jump Value")]
        [SerializeField] private float _jumpForce;

        [Header("Raycast Value")]
        [SerializeField] private float _raycastMaxDistance;
        [SerializeField] private float _originOffset;

        [Header("Ground Mask")]
        [SerializeField] private LayerMask _groundMask;

        private float _speed;
        private float _playerMass;
        private Vector3 _scaleDirection;     
        private bool _onGround;


        private Rigidbody _rigidbody;
        private PlayerAnimation _playerAnimation;

        private PlayerInputs _playerInputs;
        
        #region [Initialization]
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _playerInputs = new PlayerInputs();
            _playerAnimation = GetComponent<PlayerAnimation>();
        } 

        private void OnEnable()
        {
            _playerInputs.Enable();
        }

        private void OnDisable()
        {
            _playerInputs.Disable();
        }
        #endregion

        private void Start()
        {
            _playerMass = _rigidbody.mass;
        }

        private void FixedUpdate()
        {
            if (PauseGame.instance._isPaused)
            {
                return;
            }

            ApplayGround();
        }

        private void ApplayGround()
        {
            if (GroundCheck())
            {
                Move();
                Jump();
            }
        }

        private void Move()
        {
            CalcDirection();

            _rigidbody.velocity = new Vector3(_scaleDirection.x, _rigidbody.velocity.y, _scaleDirection.z);        }

        private void Jump()
        {
            if (_playerInputs.Player.Jump.ReadValue<float>() > 0)
            {
                _rigidbody.AddForce(Vector3.up * _jumpForce * _playerMass, ForceMode.Impulse);
            }
        }

        private void StartAnimation(Vector2 direction)
        {
            _playerAnimation.MoveAnimation(direction);
        }

        private void CalcDirection()
        {
            var inputsDirection = _playerInputs.Player.Move.ReadValue<Vector2>();

            StartAnimation(inputsDirection);

            if (inputsDirection == Vector2.zero)
            {
                _speed = 0f;
                _scaleDirection = Vector3.zero;
                _rigidbody.velocity = new Vector3(0f, _rigidbody.velocity.y, 0f);

                return;
            }

            ChangeSpeed(inputsDirection);

            var moveDirection = transform.right * inputsDirection.x + transform.forward * inputsDirection.y;
            _scaleDirection = moveDirection * _playerMass * (_speed * Time.deltaTime);   
        }

        private void ChangeSpeed(Vector2 inputsDirection)
        {
            if (inputsDirection.y > 0 && inputsDirection.x == 0)
            {
                _speed += _playerFrontAcceleration * Time.deltaTime;
                _speed = Mathf.Clamp(_speed, _initialFrontSpeed, _maxFrontSpeed);
            }
            else
            {
                _speed = _initialSideAndRearSpeed;
            }
        }

        private bool GroundCheck()
        {
            var curPosition = transform.position;
            curPosition.y +=_originOffset;
            var direction = -Vector3.up;
            var maxDistance = _raycastMaxDistance + _originOffset;

            _onGround = Physics.Raycast(curPosition, direction, maxDistance, _groundMask);

            return _onGround;
        }
    }
}