using GameManager.PauseGame;
using Player.Animation;
using UnityEngine;

namespace Player.Controller
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent (typeof(CapsuleCollider))]
    public class PlayerMover : MonoBehaviour
    {
        [Header("Speed Value")]
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxFrontSpeed;
        [SerializeField] private float _playerFrontAcceleration;

        [Header("Jump Value")]
        [SerializeField] private float _jumpForce;

        [Header("GroundCheck Raycast Value")]
        [SerializeField] private float _rayLength;
        [SerializeField] private float _originOffset;

        [Header("Ground Mask")]
        [SerializeField] private LayerMask _groundMask;

        private float _playerCurrentSpeed;
        private float _playerMass;
        private float _playerBodyRadius;
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
            _playerBodyRadius = gameObject.GetComponent<CapsuleCollider>().radius;
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
            Move();
            Jump();
        }

        private void Move()
        {
            CalcDirection();

            _rigidbody.velocity = new Vector3(_scaleDirection.x, _rigidbody.velocity.y, _scaleDirection.z);        }

        private void Jump()
        {
            if (_playerInputs.Player.Jump.ReadValue<float>() > 0 && GroundCheck())
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
                _playerCurrentSpeed = 0f;
                _scaleDirection = Vector3.zero;
                _rigidbody.velocity = new Vector3(0f, _rigidbody.velocity.y, 0f);

                return;
            }

            ChangeSpeed(inputsDirection);

            var moveDirection = transform.right * inputsDirection.x + transform.forward * inputsDirection.y;
            _scaleDirection = moveDirection * _playerMass * (_playerCurrentSpeed * Time.deltaTime);   
        }

        private void ChangeSpeed(Vector2 inputsDirection)
        {
            if (inputsDirection.y > 0 && inputsDirection.x == 0)
            {
                _playerCurrentSpeed += _playerFrontAcceleration * Time.deltaTime;
                _playerCurrentSpeed = Mathf.Clamp(_playerCurrentSpeed, _minSpeed, _maxFrontSpeed);
            }
            else
            {
                _playerCurrentSpeed = _minSpeed;
            }
        }

        private Vector3 CurrentPlayerPosition()
        {
            var curPosition = transform.position;
            curPosition.y += _originOffset;

            return curPosition;
        }

        private Vector3 BoxCastSizi()
        {
            var size = Vector3.one * _playerBodyRadius;
            return size;
        }

        private bool GroundCheck()
        {
            var halfSize = BoxCastSizi() / 2;
            var direction = -Vector3.up;
            var playerRotation = transform.rotation;

            _onGround = Physics.BoxCast(CurrentPlayerPosition(), halfSize, direction, playerRotation, _rayLength, _groundMask);

            return _onGround;
        }
    }
}